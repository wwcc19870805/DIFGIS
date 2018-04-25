/*---------------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawPolyline.cs
			// 文件功能描述：绘制多义线\任意形状边线的面
			//
			// 
			// 创建标识：YuanHY 20060102
            // 操作说明：U键回退
			//           A键输入绝对坐标
            //     　　  R键输入相对坐标
			//     　　  N键输入左折角
			//     　　  O键输入固定方向
            //　　　　　 F键输入长度＋方向
            //     　　  D键输入固定长度
			//　　　　　 P键平行尺            
			//           S键直角...	
			//　　　　　 C键封闭结束
            //           E键\Enter键\Space键结束            
			//           ESC键取消所有操作
            //           H键绘制圆弧
            //           L键绘制直线
            //           T键,绘制圆弧…… 切线
			// 修改记录：增加捕捉效果				By YuanHY  20060217
			//           增加直角封闭				By WangShM 20060307
			//           增加平行尺					By YuanHY  20060308
			//           增加正交功能　　			By YuanHY  20060308
			//           增加右键菜单　				By YuanHY  20060331 
			//           向框架状态栏传送提示信息	By YuanHY  20060615 
-----------------------------------------------------------------------*/

using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;

namespace DF2DEdit.CmdDraw
{
	/// <summary>
	/// DrawPolyline 的摘要说明。
	/// </summary>
    public class DrawPolyline : AbstractMap2DCommand
	{
        private DF2DApplication m_App;      
		private IMapControl2   m_MapControl;
		private IMap           m_FocusMap;
		private ILayer         m_CurrentLayer;
		private IActiveView    m_pActiveView;
		
		private IDisplayFeedback m_pFeedback;
		private INewLineFeedback m_pLineFeed;  
		private IDisplayFeedback m_pLastFeedback;
		private INewLineFeedback m_pLastLineFeed;

		public  static IPoint m_pPoint;
		public  static IPoint m_pAnchorPoint;
		private static IPoint m_pLastPoint;
        
		public static bool   m_bFixLength;//是否已固定长度
		public static double m_dblFixLength;
		public static bool   m_bFixDirection;//是否已固定方向
		public static double m_dblFixDirection;
		public static bool   m_bFixLeftCorner;//是否左折角
		public static double m_dbFixlLeftCorner;
		public static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消

		private bool   m_bKeyCodeP;//按P键平行线
		private bool   m_bkeyCodeS;//按S键直角封闭
		private IPoint m_BeginConstructParallelPoint;//开始平行尺，鼠标右击的点

		private bool   m_bInUse;
		private string m_drawState = "";//绘制状态
		private int    m_drawType = 0; //点的属性:=1直线上的点,＝2为圆弧上的点
		private IPoint m_pFromPoint;  //对于直线段则记录直线起点;对于圆弧则记录圆弧的起点
		private IPoint m_pMiddlePoint;//对于直线段则记录直线的中间点,对工作没有任何意义;对于圆弧则记录圆弧的起点
		private IPoint m_pToPoint;　　//对于直线段则记录直线止点;对于圆弧则记录圆弧的止点
		private static double TempTA; //切线的方位角
        
		private IArray m_pLineFeedArray = new ArrayClass();//画线上的点数组
		private IArray m_pUndoArray     = new ArrayClass();
		private double m_dblTolerance;

		private ISegment m_pSegment = null;//用平行尺方法修改锚点坐标时间捕捉到的线的某个节

		private bool	isEnabled   = false;
		private string	strCaption  = "绘制多义线/任意形状边线的面";
		private string	strCategory = "编辑";   
  
		private	IEnvelope  m_pEnvelope =  new EnvelopeClass();

        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;

            m_App = DF2DApplication.Application;
            if (m_App == null || m_App.Current2DMapControl == null) return;

            m_MapControl = m_App.Current2DMapControl;
            m_FocusMap = m_MapControl.ActiveView.FocusMap;
            m_pActiveView = (IActiveView)this.m_FocusMap;
            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            m_dblTolerance = Class.Common.ConvertPixelsToMapUnits(m_MapControl.ActiveView, 4);

            Class.Common.MapRefresh(m_pActiveView);
        }

		public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawPolyline.OnMouseDown 实现
			base.OnMouseDown (button, shift, x, y, mapX, mapY);

            m_CurrentLayer = Class.Common.CurEditLayer; 

			//检查点是否超出地图范围
			if(Class.Common.PointIsOutMap(m_CurrentLayer,m_pAnchorPoint) == true)
			{
				DrawPolylineMouseDown(m_pAnchorPoint,m_drawState); 
	
				m_pSegment = null;
			}
			else
			{
				MessageBox.Show("超出地图范围");
			}	
			           
		}
		
		public void DrawPolylineMouseDown( IPoint pPoint,string drawState)
		{   
            m_pLastPoint  = pPoint;
        
			if (m_drawState =="")  m_drawState = "Line_Line";//默认是绘制直线
            
			//将直线段两点坐标或圆弧的三点坐标（起点+中点+止点）存入回退数组中,用于回退操作和最好的存储工作
			if (m_pLineFeedArray.Count > 2)
			{
				m_pFromPoint    = ((IPoint)m_pLineFeedArray.get_Element(0)) ;         
				m_pMiddlePoint  = ((IPoint)m_pLineFeedArray.get_Element((int)(m_pLineFeedArray.Count/2)));                 
				m_pToPoint      = m_pAnchorPoint;

				if (m_drawType != 0) //将点添加到UnDo数组中
				{
					AddPointUndoArray(m_pFromPoint, m_drawType,ref m_pUndoArray);
					if (m_drawType == 2)//若为圆弧则，存入圆弧中点坐标，该坐标在绘制圆弧时计算而得
					{
						AddPointUndoArray(m_pMiddlePoint, m_drawType,ref m_pUndoArray);
					}
					AddPointUndoArray(m_pToPoint, m_drawType,ref m_pUndoArray);
				}

				switch(m_drawType)//计算切线方位角;
				{
					case 1:
						TempTA = CommonFunction.GetAzimuth_P12(m_pFromPoint, m_pToPoint); 
						break;
					case 2:
						TempTA = CommonFunction.GetTangentLineAzi(m_pFromPoint, m_pMiddlePoint, m_pToPoint);                     
						break;
				}
　　
				m_pLineFeed.Stop();
				m_pLineFeedArray =new ArrayClass();
				m_pLineFeedArray.RemoveAll(); //清空绘图数组中所有元素	
  　            m_bInUse = false;

				DisplaypSegmentColToScreen(m_MapControl, ref m_pUndoArray);//可以刷新屏幕了
			}

			//开始m_pLastLineFeed复位工作
			if ((m_pUndoArray.Count!=0)&&(((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
			{                
				if (m_pLastLineFeed != null) m_pLastLineFeed.Stop();
				m_pLastFeedback = new NewLineFeedbackClass();
				m_pLastLineFeed = (INewLineFeedback)m_pLastFeedback;
				m_pLastLineFeed.Start(((PointStruct)m_pUndoArray.get_Element(0)).Point);
			}

			if (!m_bInUse )//如果命令没有使用
			{
				m_pFeedback = new NewLineFeedbackClass(); 
				m_pLineFeed =(NewLineFeedback)m_pFeedback;
				m_pLineFeed.Display = m_pActiveView.ScreenDisplay;
 
				IPoint tempPoint = new PointClass();
				tempPoint.X  = m_pAnchorPoint.X;
				tempPoint.Y  = m_pAnchorPoint.Y; 
               
				m_pLineFeedArray.Add(tempPoint);
                   
				switch (m_drawState)
				{				
					case "Line_Line"://绘制直线        
						m_drawType = 1;   
						break;
					case "Line_Arc": //由绘制直线-圆弧              
						m_drawType = 2;         						
						break;
					case "Arc_Arc":  //绘制圆弧-圆弧                       
						m_drawType = 2;                							
						break;
					case "Arc_TLine"://绘制圆弧-切线    
						m_drawType = 1;            
						break;                    
					default:
						break;
				}//end switch	

				m_bInUse = true;          

			}
    
			if( (m_bFixLength ==true ) && ( m_bFixDirection == false) )//可以给定一个长度值
			{
				m_bFixLength = false;
			}  
			else if( (m_bFixLength == false) && ( m_bFixDirection ==true ))//可以给定一个固定方向值
			{
				m_bFixDirection = false;
			}
			else if ( (m_bFixLength == true) && ( m_bFixDirection ==true ))
			{
				m_bFixLength = false;
				m_bFixDirection = false;
			}

			if (m_bFixLeftCorner)
			{
				m_bFixLeftCorner=false;
			}

            CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pAnchorPoint);

			if (m_pUndoArray.Count > 1)
			{
				DisplaypSegmentColToScreen(m_MapControl, ref m_pUndoArray);//可以刷新屏幕了
			}	

			if (m_bkeyCodeS == true) //直角结束
			{
				m_bkeyCodeS = false;
				if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline )
				{
					m_pLastLineFeed.Stop();
				}		
			}

			m_pSegment = null;
			
		}
	
		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawPolyline.OnMouseMove 实现
			base.OnMouseMove (button, shift, x, y, mapX, mapY);

			m_MapView.CurrentTool =this;
			
			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y); //获取鼠标坐标	 
         
			if (m_bkeyCodeS == true)//按S键生成直角
			{
				IPoint pStartPoint = new PointClass();
				IPoint pEndPoint   = new PointClass();
				pStartPoint=((PointStruct)m_pUndoArray.get_Element(0)).Point;
				pEndPoint=((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Point;
				m_pPoint = CommonFunction.SquareEnd(pStartPoint,pEndPoint,m_pPoint);
			}

			double dx, dy;
			double tempA;
			IPoint p0;//圆心

			if(m_bFixDirection && m_bInputWindowCancel == false)  //此处固定m_pAnchorPoint使其在一个固定方向上
			{
				m_pPoint=CommonFunction.GetTwoPoint_FormPointMousePointFixDirection(m_pToPoint,m_pPoint,m_dblFixDirection);
			}
			else if(m_bFixLength && m_bInputWindowCancel == false)// 给定一个长度值
			{
				m_dblFixDirection =  CommonFunction.GetAzimuth_P12(m_pToPoint,m_pPoint);

				tempA = CommonFunction.azimuth(m_pToPoint,  m_pPoint);
				dx = m_dblFixLength * Math.Cos(tempA * Math.PI / 180);
				dy = m_dblFixLength * Math.Sin(tempA * Math.PI / 180);
    
				//计算更正后的锚点坐标值
				dx = m_pToPoint.X + dx;
				dy = m_pToPoint.Y + dy;
    
				m_pPoint.PutCoords( dx, dy);                
			}
			else if (m_bFixLeftCorner && m_bInputWindowCancel == false )//给定左折角
			{
				tempA =(180 + CommonFunction.RadToDeg(TempTA)) - m_dbFixlLeftCorner;
                
				if (m_dbFixlLeftCorner>360) m_dbFixlLeftCorner = m_dbFixlLeftCorner-360;

				if (m_dbFixlLeftCorner!=tempA)
				{
					m_pPoint=CommonFunction.GetOnePoint_FormPointMousePointFixDirection(m_pToPoint,m_pPoint,tempA);
				}
			}             
           
			m_pAnchorPoint= m_pPoint;
			       
			//+++++++++++++开始捕捉+++++++++++++++++++++			
			bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,(IGeometry)m_pLastPoint,m_pAnchorPoint);
	
			if (m_bInUse)//如果命令正在使用
			{	
				//########################平行尺########################			
				CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_pPoint,ref m_pAnchorPoint);

				//&&&&&&&&&&&&&&&&&&&&&&&& 正 交 &&&&&&&&&&&&&&&&&&&&&&&
				CommonFunction.PositiveCross(m_pLastPoint,ref m_pAnchorPoint,m_App.CurrentConfig.cfgPositiveCross.IsPositiveCross ); 
	
				switch (m_drawState)
				{				
					case "Line_Line"://绘制直线        
						m_drawType = 1;              
						m_pLineFeedArray.Add(m_pAnchorPoint); //将点保存到变量		
						m_pLineFeed.Stop(); 
						m_pLineFeed.Start((IPoint)m_pLineFeedArray.get_Element(0)); 
						m_pFeedback.MoveTo(m_pAnchorPoint);
						break;
					case "Line_Arc": //绘制直线-圆弧                       
						m_drawType = 2;
						p0 = CommonFunction.GetCenterL(m_pFromPoint, m_pToPoint, m_pAnchorPoint); //获取圆心坐标                        
						DrawArc_FromPCenterPToPTa(m_pToPoint,p0,m_pAnchorPoint,TempTA);//起点+圆心+端点+通过起点的切线方位角画弧				
						break;
					case "Arc_Arc":  //绘制圆弧-圆弧                        
						m_drawType = 2;          		
						p0 = CommonFunction.GetCenterC(m_pFromPoint, m_pMiddlePoint, m_pToPoint, m_pAnchorPoint); //获取圆心坐标  
						DrawArc_FromPCenterPToPTa(m_pToPoint,p0,m_pAnchorPoint,TempTA);//起点+圆心+端点+通过起点的切线方位角画弧      
						break;
					case "Arc_TLine"://绘制圆弧-切线                       
						m_drawType = 1;             
						double d = CommonFunction.GetDistance_P12(m_pToPoint, m_pAnchorPoint);   //计算鼠标点到弧端点距离 
						//计算切线方向上距离d的点坐标
						IPoint tPoint = new PointClass();
						tPoint.X = m_pToPoint.X + d * Math.Cos(TempTA);
						tPoint.Y = m_pToPoint.Y + d * Math.Sin(TempTA);                      
						m_pLineFeedArray.Add(tPoint); //将点保存到变量		
						m_pAnchorPoint = tPoint;
						m_pLineFeed.Stop(); 
						m_pLineFeed.Start((IPoint)m_pLineFeedArray.get_Element(0)); 
						m_pFeedback.MoveTo(m_pAnchorPoint);                    
						break;
					default:                      
						break;
				}//end switch
	
			}//end if (m_bInUse)

			if ((m_pUndoArray.Count > 1) && (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon || m_bkeyCodeS == true))
			{
				if( m_pLastFeedback != null)  m_pLastFeedback.Display = m_pActiveView.ScreenDisplay;
				m_pLastFeedback.MoveTo(m_pAnchorPoint);
			}

		}
	
		public void EndDrawPolyline()
		{
			if (CurrentTool.m_CurrentToolName  != CurrentTool.CurrentToolName.drawPolyline ) return;
         
	        IGeometry pGeom = null;
			IPolyline pPolyline;
			IPolygon pPolygon;
			IPointCollection pPointCollection;

			//画线时，若点击了S键执行直角...，向数组添加结束点
			if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline && m_bkeyCodeS == true)
			{
				m_drawType = 1;
				m_pFromPoint = ((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count - 1)).Point;
				m_pToPoint = ((PointStruct)m_pUndoArray.get_Element(0)).Point;
				AddPointUndoArray(m_pFromPoint, m_drawType, ref m_pUndoArray);	
				AddPointUndoArray(m_pToPoint, m_drawType, ref m_pUndoArray);　
			}
    
			pPolyline =(IPolyline)MadeSegmentCollection(ref m_pUndoArray);
                         
			if(m_bInUse)
			{            
				switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
				{
					case  esriGeometryType.esriGeometryPolyline:
						pPointCollection =(IPointCollection)pPolyline; 

//						((ITopologicalOperator)pPolyline).Simplify();

						if(pPointCollection.PointCount < 2)
						{
							MessageBox.Show("线上必须有两个点!");
						}
						else
						{
							pGeom = (IGeometry)pPointCollection;                          
						}
						break;
					case  esriGeometryType.esriGeometryPolygon:                     
						pPolyline = (IPolyline)pPolyline;		                        
						pPolygon= CommonFunction.PolylineToPolygon(pPolyline);

//						((ITopologicalOperator)pPolygon).Simplify();

						pPointCollection =(IPointCollection) pPolygon;
						pGeom = (IGeometry)pPointCollection;
						if(pPointCollection.PointCount < 3)
						{
							MessageBox.Show("面上必须有三个点!");
						}
						else
						{
							pGeom = (IGeometry)pPointCollection;                            
						}
						break;
					default:
						break;
             
				}//end switch

				m_pEnvelope = pGeom.Envelope;
				if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);; 

				CommonFunction.CreateFeature(m_App.Workbench, pGeom, m_FocusMap,m_CurrentLayer);
				
                   
				Reset();//复位 
                     
			} 

			m_pSegment = null;//清空捕捉到的片断

		}
	
		public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawPolyline.OnDoubleClick 实现
			base.OnDoubleClick (button, shift, x, y, mapX, mapY);
    
			EndDrawPolyline();

		}
        
		public override void OnBeforeScreenDraw(int hdc)
		{
			// TODO:  添加 DrawPolyline.OnBeforeScreenDraw 实现
			base.OnBeforeScreenDraw (hdc);
            
			if (m_pUndoArray.Count !=0)
			{
				IPoint pStartPoint = new PointClass();
				IPoint pEndPoint   = new PointClass();         
				pStartPoint = ((PointStruct)m_pUndoArray.get_Element(0)).Point;
				pEndPoint   = ((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count -1)).Point;
    
				if (m_pLineFeed !=null)  m_pLineFeed.MoveTo(pEndPoint);
				if (m_pLastLineFeed !=null)  m_pLastLineFeed.MoveTo(pStartPoint);
			}

		}

		public override void OnAfterScreenDraw(int hdc)
		{
			// TODO:  添加 DrawPolyline.OnAfterScreenDraw 实现
			base.OnAfterScreenDraw (hdc);
			DisplaypSegmentColToScreen(m_MapControl, ref m_pUndoArray);//可以刷新屏幕了
		}

		//将SegmentCollection显示到屏幕
		private  void DisplaypSegmentColToScreen( IMapControl2 MapControl,ref IArray PointArray)
		{            
			IActiveView pActiveView = MapControl.ActiveView;
			ISegmentCollection pPolylineCol;
			pPolylineCol = new PolylineClass();
			ISegmentCollection  pSegmentCollection = MadeSegmentCollection(ref PointArray);
			pPolylineCol.AddSegmentCollection(pSegmentCollection);
     
			pActiveView.ScreenDisplay.ActiveCache = (short)esriScreenCache.esriNoScreenCache; 
			ISimpleLineSymbol pLineSym = new SimpleLineSymbolClass();
			pLineSym.Color=CommonFunction.GetRgbColor(0,0,0);
             
			pActiveView.ScreenDisplay.StartDrawing(m_pActiveView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
			pActiveView.ScreenDisplay.SetSymbol((ISymbol)pLineSym);      
			pActiveView.ScreenDisplay.DrawPolyline((IPolyline)pPolylineCol);
			pActiveView.ScreenDisplay.FinishDrawing();
		}
       
		//将数组里的坐标构造SegmentCollection
		private ISegmentCollection MadeSegmentCollection(ref IArray PointArray)
		{
			ISegment pSegment;
			ISegmentCollection pSegmentCollection= new PolylineClass();
			IPoint fromPoint;
			IPoint toPoint;
			IPoint middlePoint;

			object a = System.Reflection.Missing.Value;  
			object b = System.Reflection.Missing.Value;
            
			for (int i = 0; i< PointArray.Count; i++)
			{
				PointStruct pointStruct;
				pointStruct=(PointStruct)PointArray.get_Element(i);

				if (pointStruct.Type == 1)
				{	//获取线起点和端点
					fromPoint = ((PointStruct)PointArray.get_Element(i)).Point; 
					toPoint   = ((PointStruct)PointArray.get_Element(i+1)).Point;                     
					pSegment = new LineClass();
					pSegment = MadeLineSeg_2Point(fromPoint,toPoint);  
					pSegmentCollection.AddSegment(pSegment,ref a,ref b);

					i = i + 1;
				}
				else if (pointStruct.Type == 2)
				{   //获取弧起点、中点、端点
					fromPoint   = ((PointStruct)PointArray.get_Element(i)).Point;  
					middlePoint = ((PointStruct)PointArray.get_Element(i+1)).Point; 
					toPoint     = ((PointStruct)PointArray.get_Element(i+2)).Point;
					pSegment = new CircularArcClass();
					pSegment = MadeArcSeg_3Point(fromPoint, middlePoint,toPoint);
					pSegmentCollection.AddSegment(pSegment,ref a,ref b);

					i = i + 2;
				}
 
			}// end for

			return pSegmentCollection;

		}

		//三点法构造弧段的Segment
		private ISegment MadeArcSeg_3Point( IPoint pPoint1, IPoint pPoint2, IPoint pPoint3)
		{               
			IConstructCircularArc pArc = new CircularArcClass();
			pArc.ConstructThreePoints(pPoint1, pPoint2, pPoint3,true);
			return (ISegment)pArc;      
		}
		//两点构造线段的Segment
		private ISegment MadeLineSeg_2Point( IPoint pPoint1, IPoint pPoint2)
		{               
			ILine pLine = new LineClass();
			pLine.PutCoords(pPoint1, pPoint2);
			return (ISegment)pLine;     
		}        
            
		//起点+圆心+端点+通过起点的切线方位角(用于讨论 is major or minor?)画弧
		private void DrawArc_FromPCenterPToPTa(IPoint pFromPoint, IPoint p0, IPoint pToPoint, double TA)
		{	            
			double R;//计算半径
			R = CommonFunction.GetDistance_P12(pFromPoint,p0);

			//计算p0到起点p1的方位角和到端点的方位角
			double Ap01;
			double Ap03;					
			Ap01=CommonFunction.GetAzimuth_P12(p0,pFromPoint);
			Ap03=CommonFunction.GetAzimuth_P12(p0,pToPoint);			
           
			double Ap13; //计算起点到端点的方位角	
			Ap13=CommonFunction.GetAzimuth_P12(pFromPoint,pToPoint);
			//讨论计算圆心角
			double m_Ca = 0;
			double dA;
			dA = Ap13 - TA;
			if (dA < 0)
			{
				dA = dA + Math.PI * 2; 
			}
			if (dA >= 0 && dA < Math.PI) 
			{
				m_Ca = Ap03 - Ap01;
			}
			else if(dA >= Math.PI && dA < Math.PI * 2)
			{
				m_Ca = Ap01 - Ap03;	
			}
			if (m_Ca<0)
			{
				m_Ca = m_Ca + 2 * Math.PI; 
			}

			//计算端点坐标
			pToPoint.X =p0.X + R*Math.Cos(Ap03);
			pToPoint.Y =p0.Y + R*Math.Sin(Ap03);
					
			m_pLineFeed.Stop(); 
			m_pLineFeed.Start(pFromPoint);
			m_pLineFeedArray.RemoveAll(); 
			m_pLineFeedArray.Add(pFromPoint);

			IPoint pTempPoint = new PointClass();			
			if (dA >= 0 && dA < Math.PI) 
			{
				for (int i = 0; i <= m_Ca/CommonFunction.DegToRad(5)-1; i++)
				{			
					pTempPoint.X = p0.X + R * Math.Cos(Ap01 + CommonFunction.DegToRad(5 * (i + 1)));
					pTempPoint.Y = p0.Y + R * Math.Sin(Ap01 + CommonFunction.DegToRad(5 * (i + 1)));
					m_pLineFeed.AddPoint(pTempPoint);
					m_pLineFeedArray.Add(pTempPoint); 
				}
			}
			else if(dA >= Math.PI && dA < Math.PI * 2)
			{
				for (int i = 0; i <= m_Ca/CommonFunction.DegToRad(5)-1; i++)
				{			
					pTempPoint.X = p0.X + R * Math.Cos(Ap01 - CommonFunction.DegToRad(5 * (i + 1)));
					pTempPoint.Y = p0.Y + R * Math.Sin(Ap01 - CommonFunction.DegToRad(5 * (i + 1)));
					m_pLineFeed.AddPoint(pTempPoint);
					m_pLineFeedArray.Add(pTempPoint);
				}	
			}	
		
			m_pLineFeed.AddPoint(pToPoint);

			m_pLineFeedArray.Add(pToPoint);

		}

		//回退操作
		private void  Undo()
		{			
			//删除数组中最后一组点          
			int count = m_pUndoArray.Count;
			if(count==0)
			{
				Reset();
				return;
			}

			#region 计算最小刷新的矩形
			IArray pTempArray = new ArrayClass();
			for(int i=0; i<m_pUndoArray.Count; i++)
			{
				pTempArray.Add(((PointStruct)m_pUndoArray.get_Element(i)).Point);
			}
			if(pTempArray.Count >2)
			{
				m_pEnvelope = CommonFunction.GetMinEnvelopeOfTheArray(pTempArray);
			}
			else if(pTempArray.Count ==2)
			{
				IPoint pTempPoint = new PointClass();
				m_pEnvelope = CommonFunction.GetMinEnvelopeOfTheArray(pTempArray);
				m_pEnvelope.Union(m_pPoint.Envelope);
			}
			if(m_pEnvelope != null) m_pEnvelope.Expand(10,10,false);
			pTempArray.RemoveAll();
			#endregion

			PointStruct pointStruct =(PointStruct)m_pUndoArray.get_Element(count-1);

			IPoint pPoint0 = new PointClass();
			pPoint0 = pointStruct.Point;
			IEnvelope enve = new EnvelopeClass();
			enve =CommonFunction.NewRect(pPoint0,m_dblTolerance);

			IEnumElement  pEnumElement = m_MapControl.ActiveView.GraphicsContainer.LocateElementsByEnvelope(enve);
			if (pEnumElement != null)
			{
				pEnumElement.Reset();
				IElement pElement = pEnumElement.Next();

				while(pElement!=null)
				{
					m_MapControl.ActiveView.GraphicsContainer.DeleteElement(pElement);
					pElement = pEnumElement.Next();
				}
			}
            
			if (pointStruct.Type == 1)
			{
				m_pUndoArray.Remove(m_pUndoArray.Count-1);
				m_pUndoArray.Remove(m_pUndoArray.Count-1);
			}
			else if (pointStruct.Type == 2)
			{
				m_pUndoArray.Remove(m_pUndoArray.Count-1);
				m_pUndoArray.Remove(m_pUndoArray.Count-1);
				m_pUndoArray.Remove(m_pUndoArray.Count-1);
			}

			//屏幕刷新
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics , null, m_pEnvelope);//视图刷新
			m_pActiveView.ScreenDisplay.UpdateWindow();
       
			//开始作复位工作
			m_pLineFeedArray.RemoveAll();
			if (m_pUndoArray.Count!=0)
			{    
				m_pLastPoint = ((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Point;

				DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray);

				m_drawState="Line_Line";//默认回退之后，绘制直线
      
				IPoint pPoint = new PointClass();
				pPoint=((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Point;               
				m_pLineFeedArray.Add(pPoint);　　　　　　　　

				m_pFeedback = new NewLineFeedbackClass(); 
				m_pLineFeed =(NewLineFeedback)m_pFeedback;
				m_pLineFeed.Display = m_pActiveView.ScreenDisplay;
				if (m_pLineFeed !=null) m_pLineFeed.Stop();
				m_pLineFeed.Start(pPoint);
				m_pLineFeed.MoveTo(m_pPoint);
              
				//调整m_pFromPoint、m_pMiddlePoint、m_pToPoint
				if (((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Type == 1)//若数组最后一段为线段
				{
					m_pFromPoint =((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-2)).Point; 
					m_pToPoint =((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Point;
				}
				else if (((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Type == 2)//若数组最后一段为圆弧
				{
					m_pFromPoint =((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-3)).Point; 
					m_pMiddlePoint =((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-2)).Point; 
					m_pToPoint =((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Point;
				}

			}
			else //if (m_pUndoArray.Count = 0)
			{  
				Reset(); //复位
			}
           
		}
        
		//将点添加到m_pUnDoArray数组
		private void AddPointUndoArray(IPoint pPoint, double drawType,ref IArray pUndoArray)
		{
			PointStruct pointStruct = new PointStruct();
			pointStruct.Point = pPoint;

			pointStruct.Type = (int)drawType;
           
			pUndoArray.Add(pointStruct);
       
			return;            
		}    
      
		//定义一种数据结构，方便将信息存入数组中
		struct PointStruct
		{
			private IPoint point;
			private int type;
			public IPoint Point
			{
				get 
				{
					return point;
				}
				set 
				{
					point = value;
				}
			}
			public int Type
			{
				get 
				{
					return type;
				}
				set 
				{
					type = value;
				}
			}
		}

		private void Reset()
		{
			m_MapControl.ActiveView.FocusMap.ClearSelection();  
			
			m_pActiveView.GraphicsContainer.DeleteAllElements(); 			
		
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground , null, m_pEnvelope);//视图刷新

			m_pStatusBarService.SetStateMessage("就绪");

			m_bInputWindowCancel = true;
			m_bInUse    = false;
			m_bkeyCodeS = false;//按S直角封闭
			m_drawState = "";
			if(m_pLastPoint != null) m_pLastPoint.SetEmpty();;
			m_pLineFeedArray.RemoveAll();//清空绘图数组
			m_pUndoArray.RemoveAll();    //清空回退数组 
			m_pEnvelope = null;
			if(m_pLineFeed     !=null) m_pLineFeed  = null;
			if(m_pLastLineFeed !=null) m_pLastLineFeed.Stop();

		}
        
		public override void OnKeyDown(int keyCode, int shift)
		{
			// TODO:  添加 DrawPolyline.OnKeyDown 实现
			base.OnKeyDown (keyCode, shift);

			IPoint tempPoint   = new PointClass();
			tempPoint.X = m_pLastPoint.X;
			tempPoint.Y = m_pLastPoint.Y;

			if (keyCode == 72)//按H键,绘制圆弧
			{
				if (m_drawType ==1)
				{
					m_drawState="Line_Arc";//绘制直线……圆弧
				}
				else if (m_drawType ==2)
				{
					m_drawState="Arc_Arc";//绘制圆弧……圆弧
				} 

				return;
			}

			if (keyCode == 76)//按L键,绘制直线
			{
                
				if (m_drawType ==1)
				{
					m_drawState="Line_Line";;//绘制直线……直线
				}
				else if (m_drawType ==2)
				{
					m_drawState="Line_Line";//绘制圆弧……直线
				} 
 
				return;
			}

			if (keyCode == 84)//按T键,绘制圆弧……切线
			{ 
				m_drawState="Arc_TLine"; 
 
				return;
			}
           
			if (keyCode == 85)//按U键,回退
			{
				Undo();

				return;
			}

			if (keyCode == 78 && m_pUndoArray.Count>=2)//按N键,输入左折角
			{    
				frmLeftCorner fromFixLeftCorner = new frmLeftCorner();
				fromFixLeftCorner.ShowDialog(); 
 
				return;  
			}

			if (keyCode == 79 && m_bInUse)//按(O)orientation键,输入方向
			{    
				frmFixAzim fromFixAzim = new frmFixAzim();  
				fromFixAzim.ShowDialog();
  
				return;  
			}

			if (keyCode == 68 && m_bInUse)//按D键,输入固定长度
			{ 
				frmFixLength fromFixLength = new frmFixLength();	
				fromFixLength.ShowDialog(); 

				return;   
			}


			if (keyCode == 70 && m_bInUse)//按F键,输入长度+方位角
			{  
				frmLengthAzim.m_pPoint = tempPoint;
				frmLengthAzim fromLengthDirect = new frmLengthAzim();     
				fromLengthDirect.ShowDialog();
                    
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                    
					DrawPolylineMouseDown(m_pAnchorPoint,m_drawState);
				}

				return;
			}

			if (keyCode == 65 )//按A键,输入绝对坐标
			{       
				frmAbsXYZ.m_pPoint = m_pAnchorPoint;
				frmAbsXYZ formXYZ = new frmAbsXYZ();
				formXYZ.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                                        
					DrawPolylineMouseDown(m_pAnchorPoint,m_drawState); 
				}

				return;
			}

			if (keyCode == 82 && m_bInUse)//按R键,输入相对坐标
			{ 
				frmRelaXYZ.m_pPoint = tempPoint;// m_pToPoint;
				frmRelaXYZ formRelaXYZ = new frmRelaXYZ();    
				formRelaXYZ.ShowDialog();
                
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                    
					DrawPolylineMouseDown(m_pAnchorPoint,m_drawState); 
				}

				return;
			}

			if (keyCode == 80 && m_bInUse)//按P键,生成平行线
			{							
				m_pSegment = null;
				m_bKeyCodeP = true;
							
				return;
			}

			if (keyCode == 83 && m_pUndoArray.Count>=2)//按S键,生成直角
			{
				m_bkeyCodeS = true;
				if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline )
				{
					m_pLastFeedback = new NewLineFeedbackClass();					
					m_pLastLineFeed = (INewLineFeedback)m_pLastFeedback;
					IPoint pStartPoint = ((PointStruct)m_pUndoArray.get_Element(0)).Point;
					m_pLastLineFeed.Start(pStartPoint);  
				}		  
	
				return;
			}

			if (keyCode == 67 && m_pUndoArray.Count>=4)//按C键,封闭结束绘制
			{			
				IPoint pStartPoint = new PointClass();
				IPoint pEndPoint   = new PointClass();
				pStartPoint=((PointStruct)m_pUndoArray.get_Element(0)).Point;
				pEndPoint=((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Point;

				AddPointUndoArray(pEndPoint, 1, ref m_pUndoArray);
				AddPointUndoArray(pStartPoint, 1, ref m_pUndoArray);

				EndDrawPolyline();			 

				return;
			}


			if ((keyCode == 69 || keyCode == 13 || keyCode == 32) && m_bInUse)//按E键、ENTER 键、SPACEBAR 键结束绘制
			{
				EndDrawPolyline();

				return;
			}	
		
			if (keyCode == 27 )//ESC 键，取消所有操作
			{
				Reset();

                this.Stop();
                WSGRI.DigitalFactory.Commands.ICommand command = DFApplication.Application.GetCommand("WSGRI.DigitalFactory.DF2DControl.cmdPan");
                if (command != null) command.Execute();

				return;
			}

		}

		//右键菜单点击事件
		private void toolManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
		{
			IPoint pStartPoint = new PointClass();
			IPoint pEndPoint   = new PointClass();
			IPoint tempPoint   = new PointClass();
			tempPoint.X = m_pLastPoint.X;
			tempPoint.Y = m_pLastPoint.Y;

			string strItemName = e.Tool.SharedProps.Caption.ToString();
			
			switch (strItemName)
			{
				case "键回退(&U)":
					Undo();

					break;

				case "输入左折角(&N)...":
					frmLeftCorner fromFixLeftCorner = new frmLeftCorner();
					fromFixLeftCorner.ShowDialog(); 	
				
					break;

				case "长度+方位角(&F)..":
					frmLengthAzim.m_pPoint = tempPoint;
					frmLengthAzim fromLengthDirect = new frmLengthAzim();     
					fromLengthDirect.ShowDialog();
                    
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{                    
						DrawPolylineMouseDown(m_pAnchorPoint,m_drawState);
					}

					break;

				case "输入方位角(&O)...":
					frmFixAzim fromFixAzim = new frmFixAzim();  
					fromFixAzim.ShowDialog();

					break;

				case "输入长度(&D)...":
					frmFixLength fromFixLength = new frmFixLength();	
					fromFixLength.ShowDialog(); 

					break;

				case "绝对坐标(&A)...":
					frmAbsXYZ.m_pPoint = m_pAnchorPoint;
					frmAbsXYZ formXYZ = new frmAbsXYZ();
					formXYZ.ShowDialog();
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{                                        
						DrawPolylineMouseDown(m_pAnchorPoint,m_drawState); 
					}

					break;

				case "相对坐标(&R)...":
					frmRelaXYZ.m_pPoint = tempPoint;
					frmRelaXYZ formRelaXYZ = new frmRelaXYZ();    
					formRelaXYZ.ShowDialog();
                
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{                    
						DrawPolylineMouseDown(m_pAnchorPoint,m_drawState); 
					}

					break;

				case "平行(&P)...":
					m_pSegment = null;
					m_bKeyCodeP = true;
					CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_BeginConstructParallelPoint,ref m_pAnchorPoint);

					break;

				case "直角(&S)...":
					m_bkeyCodeS = true;
					if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline )
					{
						m_pLastFeedback = new NewLineFeedbackClass();					
						m_pLastLineFeed = (INewLineFeedback)m_pLastFeedback;
						pStartPoint = ((PointStruct)m_pUndoArray.get_Element(0)).Point;
						m_pLastLineFeed.Start(pStartPoint);  
					}		

					break;

				case "封闭完成(&C)":
					pStartPoint=((PointStruct)m_pUndoArray.get_Element(0)).Point;
					pEndPoint=((PointStruct)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Point;

					AddPointUndoArray(pEndPoint, 1, ref m_pUndoArray);
					AddPointUndoArray(pStartPoint, 1, ref m_pUndoArray);

					EndDrawPolyline();

					break;

				case "完成(&E)":
					EndDrawPolyline();

					break;

				case "取消(ESC)":
					Reset();

					break;

				default:

					break;
			}
			
		}
	
		public override bool Deactivate()
		{
			// TODO:  添加 DrawPolyline.Deactivate 实现
			//EndDrawPolyline();
		    return base.Deactivate();

		}
        public override void Stop()
        {
           // this.Reset();
            base.Stop();
        }

	}
}
