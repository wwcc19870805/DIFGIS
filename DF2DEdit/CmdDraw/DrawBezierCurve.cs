/*--------------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawBeizerCurve.cs
			// 文件功能描述：绘制贝塞尔曲线\曲线边的面
			//
			// 
			// 创建标识：YuanHY 20051226
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
            //修改记录： 增加回退功能				By YuanHY  20060104  
			//           增加捕捉效果				By YuanHY  20060217
			//           增加正交功能				By YuanHY  20060308
			//           增加平行尺					By YuanHY  20060309
			//           增加直角...				By YuanHY  20060330
			//           增加右键菜单　				By YuanHY  20060330  
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
using ESRI.ArcGIS.SystemUI;

using WSGRI.DigitalFactory.Commands;
using WSGRI.DigitalFactory.Gui.Views;
using WSGRI.DigitalFactory.Gui;
using WSGRI.DigitalFactory.Base;
using WSGRI.DigitalFactory.DFEditorLib;
using WSGRI.DigitalFactory.Services;
using WSGRI.DigitalFactory.DFFunction;

using ICSharpCode.Core.Services;

namespace WSGRI.DigitalFactory.DFEditorTool
{
	/// <summary>
	/// DrawBeizerCurve 的摘要说明。
	/// </summary>
	public class DrawBezierCurve:AbstractMapCommand
	{
		private IDFApplication m_App;
		private IMapControl2   m_MapControl;
		private IMap           m_FocusMap;
		private ILayer         m_CurrentLayer;
		private IActiveView    m_pActiveView; 
		private IMapView       m_MapView = null;

		private IDisplayFeedback        m_pFeedback;
		private IDisplayFeedback        m_pLastFeedback;
		private INewBezierCurveFeedback m_pBezierCurveFeed;
		private INewLineFeedback        m_pLastLineFeed;

		private bool          m_bInUse;
		public  static IPoint m_pPoint;
		public  static IPoint m_pAnchorPoint;
		private IPoint        m_pLastPoint;

		public static bool   m_bFixLength;//是否已固定长度
		public static double m_dblFixLength;
		public static bool   m_bFixDirection;//是否已固定方向
		public static double m_dblFixDirection;
		public static bool   m_bFixLeftCorner;//是否左折角
		public static double m_dbFixlLeftCorner;
		public static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消

		private double   m_dblTolerance;   //固定容限值
		private bool     m_bkeyCodeS;      //是否按S键，开启直角...
		private ISegment m_pSegment = null;//平行尺方法修改锚点坐标时，捕捉到的边线的某个片断
		private bool     m_bKeyCodeP;  
		private IPoint   m_BeginConstructParallelPoint;//开始平行尺，鼠标右击的点

		private IArray m_pUndoArray = new ArrayClass();

		private EditContextMenu  m_editContextMenu;//右键菜单

		private IStatusBarService m_pStatusBarService;//状态栏服务

		private bool	isEnabled   = false;
		private string	strCaption  = "绘制贝塞尔曲线/曲线边的面";
		private string	strCategory = "编辑";

		private IEnvelope m_pEnvelope = null;


		public DrawBezierCurve()
		{	//右键菜单	
			m_editContextMenu = new EditContextMenu();
			m_editContextMenu.toolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(toolManager_ToolClick);
		
		}

		#region 类的属性
		public override bool IsEnabled
		{
			get 
			{
				isEnabled = false;

				m_App = (IDFApplication)this.Hook;

				if (m_App == null)   return false;
				IMapView mapView     = null;
				mapView = (IMapView)m_App.Workbench.GetView(typeof(MapView));
				IFeatureLayer pFeatureLayer = (IFeatureLayer)m_App.CurrentEditLayer;
                if(m_App.CurrentEditLayer is IGeoFeatureLayer)
                {
					if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline
						||pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
						isEnabled = true;
				}
				return isEnabled;
			}
			set 
			{
				isEnabled = value;
			}
		}

		public override string Caption 
		{
			get
			{
				return strCaption;
			}
			set
			{
				strCaption = value ;
			}
		}

		public override string Category 
		{
			get
			{
				return strCategory;
			}
			set
			{
				strCategory = value ;
			}
		}
		#endregion  
		
		public override void Execute()
		{
			base.Execute();
			if (!(this.Hook is IDFApplication))
			{
				return;
			}
			else
			{
				m_App = (IDFApplication)this.Hook;
			}
           
			m_MapView = m_App.Workbench.GetView(typeof(MapView)) as IMapView;
			if (m_MapView == null)
			{
				return;
			}
	
            m_MapView.CurrentTool = this;

			m_MapControl   = m_App.CurrentMapControl;            
			m_FocusMap     = m_MapControl.ActiveView.FocusMap;
			m_pActiveView  = (IActiveView)this.m_FocusMap;
			m_CurrentLayer = m_App.CurrentEditLayer;
			m_pStatusBarService = m_App.StatusBarService;//获得状态服务

			CurrentTool.m_CurrentToolName  = CurrentTool.CurrentToolName.drawBezier;

			CommonFunction.MapRefresh(m_pActiveView);
     
			m_dblTolerance=CommonFunction.ConvertPixelsToMapUnits(m_MapControl.ActiveView, 4);

			m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair ;

			m_pStatusBarService.SetStateMessage("提示:U:回退/A:绝对XY/R:相对XY/N:左折角/O:方位角/F:长度＋方向/D:长度/P:平行尺/S:直角.../C:封闭结束/Enter:结束/ESC:取消");//向状态栏传送提示信息

            //记录用户操作
            clsUserLog useLog = new clsUserLog();
            useLog.UserName = DFApplication.LoginUser;
            useLog.UserRoll = DFApplication.LoginSubSys;
            useLog.Operation = "绘制曲线";
            useLog.LogTime = System.DateTime.Now;
            useLog.TableLog = (m_App.CurrentWorkspace as IFeatureWorkspace).OpenTable("WSGRI_LOG");
            useLog.setUserLog();

		}
    
		public override void UnExecute()
		{
			// TODO:  添加 DrawBeizerCurve.UnExecute 实现
			m_pStatusBarService.SetStateMessage("就绪");

		}
     
		public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawBeizerCurve.OnMouseDown 实现
			base.OnMouseDown (button, shift, x, y, mapX, mapY);

			m_pStatusBarService.SetStateMessage("提示:U:回退/A:绝对XY/R:相对XY/N:左折角/O:方位角/F:长度＋方向/D:长度/P:平行尺/S:直角.../C:封闭结束/Enter:结束/ESC:取消");//向状态栏传送提示信息
           
			m_CurrentLayer = ((IDFApplication)this.Hook).CurrentEditLayer;
		
			//内容菜单
			if (button==2)
			{
				//记录点的坐标，用于平行尺功能
				m_BeginConstructParallelPoint = m_pAnchorPoint;
		
				toolbarsManagerToolsEnabledOrNot();
				m_editContextMenu.ActiveEditContextMenu("drawPopupMenuTool",WSGRI.DigitalFactory.Gui.DefaultWorkbench.ActiveForm);

				return;

			}
			//检查点是否超出地图范围
			if(CommonFunction.PointIsOutMap(m_CurrentLayer,m_pAnchorPoint) == true)
			{
				DrawBezierCurveMouseDown(m_pAnchorPoint); 
			}
			else
			{
				MessageBox.Show("超出地图范围");
			}	
			

		}

		#region//右键菜单项是否可用
		private void toolbarsManagerToolsEnabledOrNot()
		{
			if(m_pUndoArray.Count==0)
			{
				m_editContextMenu.toolbarsManager.Tools["btnUndo"].SharedProps.Enabled       = false;
				m_editContextMenu.toolbarsManager.Tools["btnLeftCorner"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnFixAzim"].SharedProps.Enabled    = false; 
				m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled	 = false;
				m_editContextMenu.toolbarsManager.Tools["btnSideLength"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnLengthAzim"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnAbsXYZ"].SharedProps.Enabled     = true; 
				m_editContextMenu.toolbarsManager.Tools["btnRelaXYZ"].SharedProps.Enabled    = false;
				m_editContextMenu.toolbarsManager.Tools["btnParllel"].SharedProps.Enabled    = false;
				m_editContextMenu.toolbarsManager.Tools["btnRt"].SharedProps.Enabled         = false; 
				m_editContextMenu.toolbarsManager.Tools["btnColse"].SharedProps.Enabled      = false;
				m_editContextMenu.toolbarsManager.Tools["btnEnd"].SharedProps.Enabled        = false;
				m_editContextMenu.toolbarsManager.Tools["btnESC"].SharedProps.Enabled        = false;

			}
			else if(0<m_pUndoArray.Count && m_pUndoArray.Count<2)
			{
				m_editContextMenu.toolbarsManager.Tools["btnUndo"].SharedProps.Enabled       = true;
				m_editContextMenu.toolbarsManager.Tools["btnLeftCorner"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnFixAzim"].SharedProps.Enabled    = true; 
				m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled	 = true;
				m_editContextMenu.toolbarsManager.Tools["btnLengthAzim"].SharedProps.Enabled = true;
				m_editContextMenu.toolbarsManager.Tools["btnAbsXYZ"].SharedProps.Enabled     = true; 
				m_editContextMenu.toolbarsManager.Tools["btnRelaXYZ"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnParllel"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnRt"].SharedProps.Enabled         = false; 
				m_editContextMenu.toolbarsManager.Tools["btnColse"].SharedProps.Enabled      = false;
				m_editContextMenu.toolbarsManager.Tools["btnEnd"].SharedProps.Enabled        = false;
				m_editContextMenu.toolbarsManager.Tools["btnESC"].SharedProps.Enabled        = true;
			}
			else if(2<=m_pUndoArray.Count && m_pUndoArray.Count<3)
			{
				m_editContextMenu.toolbarsManager.Tools["btnUndo"].SharedProps.Enabled       = true;
				m_editContextMenu.toolbarsManager.Tools["btnLeftCorner"].SharedProps.Enabled = true;
				m_editContextMenu.toolbarsManager.Tools["btnFixAzim"].SharedProps.Enabled    = true; 
				m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled	 = true;
				m_editContextMenu.toolbarsManager.Tools["btnSideLength"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnLengthAzim"].SharedProps.Enabled = true;
				m_editContextMenu.toolbarsManager.Tools["btnAbsXYZ"].SharedProps.Enabled     = true; 
				m_editContextMenu.toolbarsManager.Tools["btnRelaXYZ"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnParllel"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnRt"].SharedProps.Enabled         = true; 
				m_editContextMenu.toolbarsManager.Tools["btnColse"].SharedProps.Enabled      = false;
				m_editContextMenu.toolbarsManager.Tools["btnEnd"].SharedProps.Enabled        = true;
				m_editContextMenu.toolbarsManager.Tools["btnESC"].SharedProps.Enabled        = true;

			}
			else if(m_pUndoArray.Count>=3)
			{
				//m_editContextMenu.toolbarsManager.Tools["btnUndo"].SharedProps.Enabled       = true;
				//m_editContextMenu.toolbarsManager.Tools["btnLeftCorner"].SharedProps.Enabled = true;
				//m_editContextMenu.toolbarsManager.Tools["btnFixAzim"].SharedProps.Enabled    = true; 
				//m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled	 = true;
				//m_editContextMenu.toolbarsManager.Tools["btnLengthAzim"].SharedProps.Enabled = true;
				//m_editContextMenu.toolbarsManager.Tools["btnAbsXYZ"].SharedProps.Enabled     = true; 
				//m_editContextMenu.toolbarsManager.Tools["btnRelaXYZ"].SharedProps.Enabled    = true;
				//m_editContextMenu.toolbarsManager.Tools["btnParllel"].SharedProps.Enabled    = true;
				//m_editContextMenu.toolbarsManager.Tools["btnRt"].SharedProps.Enabled         = true; 
				  m_editContextMenu.toolbarsManager.Tools["btnColse"].SharedProps.Enabled      = true;
				//m_editContextMenu.toolbarsManager.Tools["btnEnd"].SharedProps.Enabled        = true;
				//m_editContextMenu.toolbarsManager.Tools["btnESC"].SharedProps.Enabled        = true;

			}
		}
		#endregion
		private void DrawBezierCurveMouseDown(IPoint pPoint )
		{          
			if(!m_bInUse)//如果命令没有使用
			{ 
				m_bInUse = true;

				m_pUndoArray.Add(pPoint);

				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,pPoint);

				m_pFeedback = new NewBezierCurveFeedbackClass();                
				m_pBezierCurveFeed = (INewBezierCurveFeedback)m_pFeedback;
				m_pBezierCurveFeed.Start(pPoint);
				if( m_pFeedback != null)  m_pFeedback.Display = m_pActiveView.ScreenDisplay;

				if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
				{　
					//若当前图层是面层，则显示回馈开始点到鼠标点的线段
					m_pLastFeedback = new NewLineFeedbackClass();
					m_pLastLineFeed = (INewLineFeedback)m_pLastFeedback;
					m_pLastLineFeed.Start(pPoint);
					if( m_pLastFeedback != null)  m_pLastFeedback.Display = m_pActiveView.ScreenDisplay;
				}

			}
			else//若果命令正使用中
			{
				m_pBezierCurveFeed = (INewBezierCurveFeedback)m_pFeedback;            
				m_pBezierCurveFeed.AddPoint(pPoint);
      
				IPoint tempPoint = new PointClass();
				tempPoint.X  = pPoint.X;
				tempPoint.Y  = pPoint.Y;              
				m_pUndoArray.Add(tempPoint);
				UpdataBezierCurveFeed(m_MapControl,ref m_pUndoArray);//刷新屏幕

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
			}

			m_pLastPoint = pPoint;

			if (m_bkeyCodeS == true) //直角结束
			{
				m_bkeyCodeS = false;
				if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline )
				{
					m_pLastLineFeed.Stop();
				}		
			}

			m_pSegment = null;//清空捕捉到的片断

		}

		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawBeizerCurve.OnMouseMove 实现
			base.OnMouseMove (button, shift, x, y, mapX, mapY);

			
			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			if (m_bkeyCodeS == true)//按S键生成直角
			{
				m_pPoint = CommonFunction.SquareEnd((IPoint)m_pUndoArray.get_Element(0),(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1),m_pPoint);
			}

			double dx, dy;
			double tempA;

			if(m_bFixDirection && m_bInputWindowCancel == false)  //此处固定m_pAnchorPoint使其在一个固定方向上
			{
				m_pPoint = CommonFunction.GetTwoPoint_FormPointMousePointFixDirection(m_pLastPoint,m_pPoint,m_dblFixDirection);
			}
			else if(m_bFixLength && m_bInputWindowCancel == false)//以给定一个长度值
			{
				m_dblFixDirection =  CommonFunction.GetAzimuth_P12(m_pLastPoint,m_pPoint);

				tempA = CommonFunction.azimuth(m_pLastPoint,  m_pPoint);
				dx = m_dblFixLength * Math.Cos(tempA * Math.PI / 180);
				dy = m_dblFixLength * Math.Sin(tempA * Math.PI / 180);
    
				//计算更正后的锚点坐标值
				dx = m_pLastPoint.X + dx;
				dy = m_pLastPoint.Y + dy;
    
				m_pPoint.PutCoords( dx, dy);                
			}
			else if (m_bFixLeftCorner && m_pUndoArray.Count>1&& m_bInputWindowCancel == false)//给定左折角
			{
				//计算最后一段的方位角
				double TempTA = CommonFunction.GetAzimuth_P12((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count - 2),(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count - 1)); 
　　　　　　　　//计算将要形成的一段的方位角
				tempA =(180 + CommonFunction.RadToDeg(TempTA)) - m_dbFixlLeftCorner;
                
				if (m_dbFixlLeftCorner>360) m_dbFixlLeftCorner = m_dbFixlLeftCorner - 360;

				if (m_dbFixlLeftCorner!=tempA)
				{
					m_pPoint=CommonFunction.GetOnePoint_FormPointMousePointFixDirection(m_pLastPoint, m_pPoint, tempA);
				}
			}    
    
			m_pAnchorPoint = m_pPoint;

			//+++++++++++++开始捕捉+++++++++++++++++++++			
			bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,(IGeometry)m_pLastPoint,m_pAnchorPoint);
               
			if(! m_bInUse)	return ;

			//########################平行尺########################			
			CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_pPoint,ref m_pAnchorPoint);

			//&&&&&&&&&&&&&&&&&&&&&&&& 正 交 &&&&&&&&&&&&&&&&&&&&&&&
			CommonFunction.PositiveCross(m_pLastPoint,ref m_pAnchorPoint,m_App.CurrentConfig.cfgPositiveCross.IsPositiveCross ); 

			m_pFeedback.MoveTo(m_pAnchorPoint);

			if((m_pUndoArray.Count > 1) && ((((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon) || m_bkeyCodeS == true))
			{
				if( m_pLastFeedback != null)  m_pLastFeedback.Display = m_pActiveView.ScreenDisplay;
				m_pLastFeedback.MoveTo(m_pAnchorPoint);
			}
         
		}
                
		public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawBeizerCurve.OnDoubleClick 实现
			base.OnDoubleClick (button, shift, x, y, mapX, mapY);

			EndDrawBezierCurve(); 
		}

		private void EndDrawBezierCurve()
		{            
			IGeometry pGeom = null;
			IPolyline pPolyline;  
			IPolygon pPolygon;
			IPointCollection pPointCollection;
          
			if(m_bInUse)
			{
				switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
				{
					case  esriGeometryType.esriGeometryPolyline:
						m_pBezierCurveFeed = (INewBezierCurveFeedback)m_pFeedback;
						m_pFeedback.MoveTo((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count -1 ));;
						pPolyline = m_pBezierCurveFeed.Stop();

						((ITopologicalOperator)pPolyline).Simplify();

						pPointCollection =(IPointCollection) pPolyline;                    
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
						m_pBezierCurveFeed = (INewBezierCurveFeedback)m_pFeedback;
						m_pBezierCurveFeed.AddPoint((IPoint)m_pUndoArray.get_Element(0));
						pPolyline = m_pBezierCurveFeed.Stop();						                      
						pPolygon= CommonFunction.PolylineToPolygon((IPolyline)pPolyline);

						((ITopologicalOperator)pPolygon).Simplify();

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

				CommonFunction.CreateFeature(m_App.Workbench, pGeom, m_FocusMap, m_CurrentLayer);

			}

			Reset();//复位

			m_bInUse = false;

		}

		//回退操作
		private void  Undo()
		{
			if (m_pUndoArray.Count==0) return;

			if(m_pUndoArray.Count >1)
			{
				m_pEnvelope = CommonFunction.GetMinEnvelopeOfTheArray(m_pUndoArray);
			}
			else if(m_pUndoArray.Count ==1)
			{
				IPoint pTempPoint = new PointClass();
				pTempPoint.X = (m_pUndoArray.get_Element(0) as Point).X;
				pTempPoint.Y = (m_pUndoArray.get_Element(0) as Point).Y;

				m_pEnvelope.Width  = Math.Abs(m_pPoint.X - pTempPoint.X);
				m_pEnvelope.Height = Math.Abs(m_pPoint.Y - pTempPoint.Y);

				pTempPoint.X = (pTempPoint.X + m_pPoint.X)/2;
				pTempPoint.Y = (pTempPoint.Y + m_pPoint.Y)/2;

				m_pEnvelope.CenterAt(pTempPoint);				

			}
			if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);

			IPoint pPoint = new PointClass();
			pPoint = (IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1);
			IEnvelope enve = new EnvelopeClass();
			enve =CommonFunction.NewRect(pPoint,m_dblTolerance);

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
			m_MapControl.ActiveView.Refresh();

			m_pUndoArray.Remove(m_pUndoArray.Count-1);//删除数组中最后一个点 

			//屏幕刷新
			//m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground,null,null);

			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_pEnvelope);//视图刷新

			m_pActiveView.ScreenDisplay.UpdateWindow();                          
                    
			//开始作复位工作
			if (m_pUndoArray.Count!=0)
			{                
				UpdataBezierCurveFeed(m_MapControl, ref m_pUndoArray);
				m_pBezierCurveFeed.MoveTo(m_pAnchorPoint);
			}
			else
			{   
				Reset(); //复位
			}
		}

		//更新m_pBezierCurveFeed
		public void UpdataBezierCurveFeed(IMapControl2 MapControl, ref IArray pUndoArray)
		{
			if (m_pBezierCurveFeed !=null) m_pBezierCurveFeed.Stop();
          
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground,null,null);
			m_pActiveView.ScreenDisplay.UpdateWindow();                      
          
			m_pBezierCurveFeed.Start((IPoint)m_pUndoArray.get_Element(0));
			CommonFunction.DrawPointSMSSquareSymbol(MapControl,(IPoint)pUndoArray.get_Element(0));
			for (int i = 0; i< pUndoArray.Count;i++)
			{
				m_pBezierCurveFeed.AddPoint((IPoint)pUndoArray.get_Element(i));  
				CommonFunction.DrawPointSMSSquareSymbol(MapControl,(IPoint)pUndoArray.get_Element(i));     
			} 
		}

		public override void OnBeforeScreenDraw(int hdc)
		{
			// TODO:  添加 DrawBeizerCurve.OnBeforeScreenDraw 实现
			base.OnBeforeScreenDraw (hdc);
           
			if(m_pUndoArray.Count !=0)
			{
				IPoint pStartPoint = new PointClass();
				IPoint pEndPoint = new PointClass();
				pStartPoint = (IPoint)m_pUndoArray.get_Element(0);
				pEndPoint = (IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count -1);

				if (m_pBezierCurveFeed !=null)  m_pBezierCurveFeed.MoveTo(pEndPoint);
				if (m_pLastLineFeed !=null)  m_pLastLineFeed.MoveTo(pStartPoint);
			}   

		}

		public override void OnAfterScreenDraw(int hdc)
		{
			// TODO:  添加 DrawBeizerCurve.OnAfterScreenDraw 实现
			base.OnAfterScreenDraw (hdc);
		}

		//将SegmentCollection显示到屏幕
		private  void DisplaypSegmentColToScreen( IMapControl2 MapControl,ref IArray pUndoArray)
		{
			IActiveView pActiveView = MapControl.ActiveView;
			IPolyline pPolyline;
			INewBezierCurveFeedback pBezierCurveFeed;
			IDisplayFeedback pFeedback;
			pFeedback = new NewBezierCurveFeedbackClass();                
			pBezierCurveFeed = (INewBezierCurveFeedback)pFeedback;

			pBezierCurveFeed.Start((IPoint)m_pUndoArray.get_Element(0));
			for (int i = 0; i< m_pUndoArray.Count-1;i++)
			{
				pBezierCurveFeed.AddPoint((IPoint)m_pUndoArray.get_Element(i));                       
			}
			pPolyline = pBezierCurveFeed.Stop();

			IPointCollection pPointCollection;
			pPointCollection=(IPointCollection)pPolyline;
   
			pActiveView.ScreenDisplay.ActiveCache = (short)esriScreenCache.esriNoScreenCache; 
			ISimpleLineSymbol pLineSym = new SimpleLineSymbolClass();
			pLineSym.Color=CommonFunction.GetRgbColor(0,0,0);
             
			pActiveView.ScreenDisplay.StartDrawing(m_pActiveView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
			pActiveView.ScreenDisplay.SetSymbol((ISymbol)pLineSym);      
			pActiveView.ScreenDisplay.DrawPolyline(pPolyline);
			pActiveView.ScreenDisplay.FinishDrawing();

			for(int i=0; i<pPointCollection.PointCount; i++)
			{
				CommonFunction.DrawPointSMSSquareSymbol(MapControl,pPointCollection.get_Point(i));
			}
		}
		
		private void Reset()
		{
			m_pActiveView.FocusMap.ClearSelection(); 			
			m_pActiveView.GraphicsContainer.DeleteAllElements();
	        m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground , null, m_pEnvelope);//视图刷新
			m_pStatusBarService.SetStateMessage("就绪");

			m_bInUse = false;
			if(m_pLastPoint != null)m_pLastPoint.SetEmpty();
			m_pUndoArray.RemoveAll();//清空回退数组 
			m_pBezierCurveFeed =null;
			m_pLastLineFeed    =null;
			m_bInputWindowCancel = true;
			m_pEnvelope = null;
   
		}

		public override void OnKeyDown(int keyCode, int shift)
		{
			// TODO:  添加 DrawBeizerCurve.OnKeyDown 实现
			base.OnKeyDown (keyCode, shift);
         
					
			if (keyCode == 85 && m_bInUse)//按U键,回退
			{
				Undo();

				return;
			}

			if (keyCode == 78 && m_pUndoArray.Count>=2 )//按N键输入折角方向
			{
				frmLeftCorner fromFixLeftCorner = new frmLeftCorner();
				fromFixLeftCorner.ShowDialog();

				return ;   
			}

			if (keyCode == 79 && m_bInUse)//按(O)orientation键输入固定方向
			{  
				frmFixAzim fromFixAzim = new frmFixAzim();
				fromFixAzim.ShowDialog();   

				return ;
			}

			if (keyCode == 68 && m_bInUse)//按D键输入固定长度
			{  
				frmFixLength fromFixLength = new frmFixLength(); 
				fromFixLength.ShowDialog();

				return ; 
			}

			if (keyCode == 70 && m_bInUse)//按F键输入长度+方位角
			{  
				frmLengthAzim.m_pPoint = m_pLastPoint;
				frmLengthAzim fromLengthDirect = new frmLengthAzim(); 
				fromLengthDirect.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{       
					DrawBezierCurveMouseDown(m_pAnchorPoint);
				}

				return;
			}

			if (keyCode == 65 )//按A键输入绝对坐标
			{       
				frmAbsXYZ.m_pPoint = m_pAnchorPoint;
				frmAbsXYZ formXYZ = new frmAbsXYZ();
				formXYZ.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{        
					DrawBezierCurveMouseDown(m_pAnchorPoint);
				}

				return;
			}

			if (keyCode == 82 && m_bInUse)//按R键输入相对坐标
			{     
				frmRelaXYZ.m_pPoint = m_pLastPoint;
				frmRelaXYZ formRelaXYZ = new frmRelaXYZ(); 
				formRelaXYZ.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{       
					DrawBezierCurveMouseDown(m_pAnchorPoint); 
				}
  
				return;
			}
				
            
			if ((keyCode == 69 || keyCode == 13 || keyCode == 32) && m_bInUse && m_pUndoArray.Count>=2)//按E键、ENTER 键、SPACEBAR 键结束绘制
			{
				EndDrawBezierCurve();

				return;
                  
			}
			
			if (keyCode == 83 && m_pUndoArray.Count>=2)//按S键生成直角
			{	
				m_bkeyCodeS = true;

				if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline )
				{
					m_pLastFeedback = new NewLineFeedbackClass();					
					m_pLastLineFeed = (INewLineFeedback)m_pLastFeedback;				
					m_pLastLineFeed.Start((IPoint)m_pUndoArray.get_Element(0));  
				}		  
				
				return;
			}
       
			if (keyCode == 80 && m_bInUse)//按P键平行尺
			{							
				m_pSegment = null;
				m_bKeyCodeP = true;		
					
				return;
			}	

			if (keyCode == 67 && m_pUndoArray.Count>=3)//按C键封闭结束绘制
			{
				if(m_bInUse)
				{
					m_pUndoArray.Add((IPoint)m_pUndoArray.get_Element(0));

					EndDrawBezierCurve();
				}

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
					frmLengthAzim.m_pPoint = m_pLastPoint;
					frmLengthAzim fromLengthDirect = new frmLengthAzim(); 
					fromLengthDirect.ShowDialog();
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{       
						DrawBezierCurveMouseDown(m_pAnchorPoint);
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
						DrawBezierCurveMouseDown(m_pAnchorPoint);
					}

					break;

				case "相对坐标(&R)...":
					frmRelaXYZ.m_pPoint = m_pLastPoint;
					frmRelaXYZ formRelaXYZ = new frmRelaXYZ(); 
					formRelaXYZ.ShowDialog();
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{       
						DrawBezierCurveMouseDown(m_pAnchorPoint); 
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
						m_pLastLineFeed.Start((IPoint)m_pUndoArray.get_Element(0));  
					}		  
			
					break;

				case "封闭完成(&C)":
					if(m_bInUse)
					{
						m_pUndoArray.Add((IPoint)m_pUndoArray.get_Element(0));

						EndDrawBezierCurve();
					}  

					break;

				case "完成(&E)":
					EndDrawBezierCurve();

					break;

				case "取消(ESC)":
					Reset();

					break;

				default:

					break;
			}
			
		}

		public override void Stop()
		{
			//this.Reset();
			base.Stop();
		}

	
	}

}
