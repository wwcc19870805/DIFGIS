
/*----------------------------------------------------------------------
            // Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawRectRelative2P.cs
			// 文件功能描述：给定矩形对角坐标 + 矩形的宽度，绘制矩形线\面
			//
			// 
			// 创建标识：YuanHY 20051226
            // 操作说明：U键回退
			//   		 F键输入长度+方位角
			//     　　  D键输入固定长度
            //     　　  O键输入固定方向
            //           B键输入输入边长
			//           A键输入绝对坐标
            //     　　  R键输入相对坐标
			//           E键\Enter键\Space键结束 
			//           ESC键取消所有操作            
            //修改记录： 增加回退功能				By YuanHY  20060104  
			//           增加平行尺功能				By YuanHY  20060309
            //           增加正交功能　　			By YuanHY  20060309　
			//           增加右键菜单　　			By YuanHY  20060330 
			//           向框架状态栏传送提示信息	By YuanHY  20060615 　　　    
------------------------------------------------------------------------*/

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
	/// DrawRectRelative2P 的摘要说明。
	/// </summary>
	public class DrawRectRelative2P:AbstractMapCommand
	{
        private IDFApplication m_App;      
		private IMapControl2   m_MapControl;
        private IMap           m_FocusMap;
        private ILayer         m_CurrentLayer;
		private IMapView       m_MapView = null;
 
        private IActiveView      m_pActiveView;
        private IDisplayFeedback m_pFeedback;
        private INewLineFeedback m_pLineFeed;

        private bool          m_bInUse;
        public  static IPoint m_pPoint;
        public  static IPoint m_pAnchorPoint;
        private IPoint        m_pLastPoint;

        private int           m_mouseDownCount;
        public  static bool   m_bFixLength;   //是否已固定长度
        public  static double m_dblFixLength;
        public  static bool   m_bFixDirection;//是否已固定方向
        public  static double m_dblFixDirection;
        public  static bool   m_bFixSideLength;//是否固定边长
        public  static double m_dblSideLength;
		public  static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消
     
		private double m_dblTolerance;     //固定容限值
		private ISegment m_pSegment = null;//平行尺方法修改锚点坐标时，捕捉到的边线的某个片断
		private bool m_bKeyCodeP;          //是否按P键，开启平行尺
		private IPoint   m_BeginConstructParallelPoint;//开始平行尺，鼠标右击的点

        private IArray m_pUndoArray      = new ArrayClass();
        private IArray m_pSavePointArray = new ArrayClass();

		private EditContextMenu  m_editContextMenu;//右键菜单

		private IStatusBarService m_pStatusBarService;//状态栏信息服务

		private bool	isEnabled   = false;
		private string	strCaption  = "给定矩形对角坐标 + 矩形的宽度，绘制矩形线/面";
		private string	strCategory = "编辑"; 

		private IEnvelope m_pEnvelope = new EnvelopeClass();
  
        public DrawRectRelative2P()
		{   //右键菜单	
			m_editContextMenu = new EditContextMenu();
			m_editContextMenu.toolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(toolManager_ToolClick);           
        
			//获得状态栏的服务
			//m_pStatusBarService = (IStatusBarService)ServiceManager.Services.GetService(typeof(WSGRI.DigitalFactory.Services.UltraStatusBarService));

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

			CurrentTool.m_CurrentToolName = CurrentTool.CurrentToolName.drawRectRelative2P;

			CommonFunction.MapRefresh(m_pActiveView);
	         
			m_dblTolerance=CommonFunction.ConvertPixelsToMapUnits(m_MapControl.ActiveView, 4);

			m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair ;

			m_pStatusBarService.SetStateMessage("依次指定:1.对角线上两点;2.宽度。(U:回退/F:长度＋方向/D:长度/O:方位角/B:边长/A:绝对XY/R:相对XY/Enter:结束/ESC:取消)");

            //记录用户操作
            clsUserLog useLog = new clsUserLog();
            useLog.UserName = DFApplication.LoginUser;
            useLog.UserRoll = DFApplication.LoginSubSys;
            useLog.Operation = "绘制矩形";
            useLog.LogTime = System.DateTime.Now;
            useLog.TableLog = (m_App.CurrentWorkspace as IFeatureWorkspace).OpenTable("WSGRI_LOG");
            useLog.setUserLog();

        }
    
        public override void UnExecute()
        {
            // TODO:  添加 DrawRectRelative2P.UnExecute 实现
			m_pStatusBarService.SetStateMessage("就绪");

        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawRectRelative2P.OnMouseDown 实现
            base.OnMouseDown (button, shift, x, y, mapX, mapY);

			m_pStatusBarService.SetStateMessage("提示：依次指定对角线上两点、一条边的宽度。(U:回退/F:长度＋方向/D:长度/O:方位角/B:边长/A:绝对XY/R:相对XY/Enter:结束/ESC:取消)");
	
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
				 DrawRectRelative2PMouseDown(m_pAnchorPoint);
			}
			else
			{
				Reset();
				MessageBox.Show("超出地图范围");
			}
		}

		//右键菜单项是否可用
		private void toolbarsManagerToolsEnabledOrNot()
		{
			if(m_pUndoArray.Count==0)//零点，零段
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
			else if(m_pUndoArray.Count<2)//一点，零段
			{
				m_editContextMenu.toolbarsManager.Tools["btnUndo"].SharedProps.Enabled       = true;
				m_editContextMenu.toolbarsManager.Tools["btnLeftCorner"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnFixAzim"].SharedProps.Enabled    = true; 
				m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled	 = true;
				m_editContextMenu.toolbarsManager.Tools["btnSideLength"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnLengthAzim"].SharedProps.Enabled = true;
				m_editContextMenu.toolbarsManager.Tools["btnAbsXYZ"].SharedProps.Enabled     = true; 
				m_editContextMenu.toolbarsManager.Tools["btnRelaXYZ"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnParllel"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnRt"].SharedProps.Enabled         = false; 
				m_editContextMenu.toolbarsManager.Tools["btnColse"].SharedProps.Enabled      = false;
				m_editContextMenu.toolbarsManager.Tools["btnEnd"].SharedProps.Enabled        = false;
				m_editContextMenu.toolbarsManager.Tools["btnESC"].SharedProps.Enabled        = true;
			}
			else if(m_pUndoArray.Count==2)//
			{
				m_editContextMenu.toolbarsManager.Tools["btnUndo"].SharedProps.Enabled       = true;
				m_editContextMenu.toolbarsManager.Tools["btnLeftCorner"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnFixAzim"].SharedProps.Enabled    = true; 
				m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled	 = false;
				m_editContextMenu.toolbarsManager.Tools["btnSideLength"].SharedProps.Enabled = true;
				m_editContextMenu.toolbarsManager.Tools["btnLengthAzim"].SharedProps.Enabled = true;
				m_editContextMenu.toolbarsManager.Tools["btnAbsXYZ"].SharedProps.Enabled     = true; 
				m_editContextMenu.toolbarsManager.Tools["btnRelaXYZ"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnParllel"].SharedProps.Enabled    = false;
				m_editContextMenu.toolbarsManager.Tools["btnRt"].SharedProps.Enabled         = false; 
				m_editContextMenu.toolbarsManager.Tools["btnColse"].SharedProps.Enabled      = false;
				m_editContextMenu.toolbarsManager.Tools["btnEnd"].SharedProps.Enabled        = true;
				m_editContextMenu.toolbarsManager.Tools["btnESC"].SharedProps.Enabled        = true;
			}
			
		}

        private void DrawRectRelative2PMouseDown(IPoint pPoint)
        {
			IPoint tempPoint = new PointClass();   

			m_mouseDownCount = m_mouseDownCount + 1;

            if (m_mouseDownCount < 3) //点击鼠标次数小于3时
            {  
                //绘制线  
                if (m_bInUse == false)
                {
                    m_pFeedback = new NewLineFeedbackClass();						
                    m_pLineFeed =(INewLineFeedback)m_pFeedback;
					m_pLineFeed.Start(pPoint);

                    tempPoint.X = pPoint.X;
                    tempPoint.Y = pPoint.Y;                  

                    CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,pPoint);
                    m_pUndoArray.Add(tempPoint);//将第一个点保存到变量
                   
                    m_pLastPoint = pPoint;
                    m_bInUse = true;
                }
                else
                {
                    m_pLineFeed = (INewLineFeedback)m_pFeedback;
					tempPoint.X = m_pAnchorPoint.X;
					tempPoint.Y = m_pAnchorPoint.Y;
                    m_pLineFeed.AddPoint(tempPoint);

                    CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,tempPoint);
                    m_pUndoArray.Add(tempPoint); //将第二个点保存到变量

                }

                if ( m_pFeedback != null )  m_pFeedback.Display = m_pActiveView.ScreenDisplay;

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
            }//m_mouseDownCount < 3
            else if(m_mouseDownCount == 3) //点击鼠标次数等于3后,停止绘制
            {     
				EndDrawRectRelative2P();              

            }//m_mouseDownCount = 3

			m_pSegment = null;//清空捕捉到的片断

        }

		private void EndDrawRectRelative2P()
		{
			IGeometry pGeom = null;
			IPolyline pPolyline;
			IPolygon  pPolygon;
			IPointCollection pPointCollection;
　　　　　　　　
			switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
			{
				case  esriGeometryType.esriGeometryPolyline:
					pPolyline = m_pLineFeed.Stop();
					pPointCollection =(IPointCollection) pPolyline;
					pGeom = (IGeometry)pPointCollection;                          
					break;

				case esriGeometryType.esriGeometryPolygon:
					pPolyline = m_pLineFeed.Stop();
					pPolygon  = CommonFunction.PolylineToPolygon(pPolyline);
					pPointCollection =(IPointCollection) pPolygon;
					pGeom = (IGeometry)pPointCollection;                      
					break;

				default:
					break;

			}// end switch 

			m_pEnvelope = pGeom.Envelope;
			if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);;

			CommonFunction.CreateFeature(m_App.Workbench,pGeom, m_FocusMap, m_CurrentLayer);
			Reset();

					
		}
            
        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawRectRelative2P.OnMouseMove 实现
            base.OnMouseMove (button, shift, x, y, mapX, mapY);

			
			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			m_pAnchorPoint = m_pPoint;
			//+++++++++++++开始捕捉+++++++++++++++++++++			
			bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,(IGeometry)m_pLastPoint,m_pAnchorPoint);
		
			if (m_bInUse == true)
            {
				//########################平行尺########################			
				CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_pPoint,ref m_pAnchorPoint);

				//&&&&&&&&&&&&&&&&&&&&&&&& 正 交 &&&&&&&&&&&&&&&&&&&&&&&
				CommonFunction.PositiveCross(m_pLastPoint,ref m_pAnchorPoint,m_App.CurrentConfig.cfgPositiveCross.IsPositiveCross ); 
		
                if(m_mouseDownCount ==1 )//计数器小于2时
                {                   
                    double dx, dy;
                    double tempA;

                    if(m_bFixDirection && m_bInputWindowCancel == false)//固定m_pAnchorPoint使其在一个固定方向上
                    {
                        m_pPoint = CommonFunction.GetTwoPoint_FormPointMousePointFixDirection(m_pLastPoint,m_pPoint,m_dblFixDirection);
						m_pAnchorPoint = m_pPoint;
					}
                    else if( m_bFixLength && m_bInputWindowCancel == false)//以给定一个长度值
                    {
                        m_dblFixDirection =  CommonFunction.GetAzimuth_P12(m_pLastPoint,m_pPoint);

                        tempA = CommonFunction.azimuth(m_pLastPoint,  m_pPoint);
						dx = m_dblFixLength * Math.Cos((90 - tempA) * Math.PI / 180);
						dy = m_dblFixLength * Math.Sin((90 - tempA) * Math.PI / 180);
    
                        //计算更正后的锚点坐标值
                        dx = m_pLastPoint.X + dx;
                        dy = m_pLastPoint.Y + dy;
    
                        m_pPoint.PutCoords(dx, dy);

						m_pAnchorPoint = m_pPoint;
                
                    }                     
    
					m_pFeedback.MoveTo(m_pAnchorPoint);	
                         		
                }
                else if (m_mouseDownCount == 2)
                {
                    m_pLineFeed.Stop();
      
                    double RelativeLength = CommonFunction.GetDistance_P12((IPoint)m_pUndoArray.get_Element(0),(IPoint)m_pUndoArray.get_Element(1));

					if(m_bFixDirection && m_bInputWindowCancel == false)  //固定m_pAnchorPoint使其在一个固定方向上
					{
						m_pPoint = CommonFunction.GetTwoPoint_FormPointMousePointFixDirection(m_pLastPoint,m_pPoint,m_dblFixDirection);
						m_pAnchorPoint = m_pPoint;
					}                 
					else if (!m_bFixSideLength)//获取矩形一边长
					{
                        m_dblSideLength =CommonFunction.GetDistance_P12(m_pAnchorPoint,(IPoint)m_pUndoArray.get_Element(1));
                        if (m_dblSideLength > RelativeLength)
                        {
                            m_dblSideLength = RelativeLength;
                        }
                    }
					else if (m_bInputWindowCancel == false)//按B键，用户输入边长，修正m_dblSideLength值
					{
                        if (m_dblSideLength > RelativeLength)
                        {
                            m_dblSideLength = RelativeLength;
                        }          			
					}
											
					//判断鼠标是否位于P1到P2方向的右边
					bool bRight = false;
					bRight = CommonFunction.GetRectP0_Right((IPoint)m_pUndoArray.get_Element(0), (IPoint)m_pUndoArray.get_Element(1),m_pAnchorPoint);
                    //获取矩形另两点坐标
                    m_pSavePointArray.RemoveAll();
					m_pSavePointArray = CommonFunction.GetPointRectangleOfRelative_Length((IPoint)m_pUndoArray.get_Element(0), (IPoint)m_pUndoArray.get_Element(1), m_dblSideLength, bRight); 
			
					//绘制矩形
					m_pLineFeed.Start((IPoint)m_pUndoArray.get_Element(0));
					m_pLineFeed.AddPoint((IPoint)m_pSavePointArray.get_Element(0));			  
					m_pLineFeed.AddPoint((IPoint)m_pUndoArray.get_Element(1));									 
					m_pLineFeed.AddPoint((IPoint)m_pSavePointArray.get_Element(1));							
					m_pLineFeed.AddPoint((IPoint)m_pUndoArray.get_Element(0));

                    m_pSavePointArray.Insert(0,(IPoint)m_pUndoArray.get_Element(0));
                    m_pSavePointArray.Insert(2,(IPoint)m_pUndoArray.get_Element(1));
                    m_pSavePointArray.Add((IPoint)m_pSavePointArray.get_Element(0));

                }//m_mouseDownCount == 2
                    
            }
                
        }

        //回退操作
        private void  Undo()
        {
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
			if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);;

            m_pUndoArray.Remove(m_pUndoArray.Count-1);//删除数组中最后一个点  
            m_mouseDownCount--;
            
            //屏幕刷新
            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground,null,m_pEnvelope);
            m_pActiveView.ScreenDisplay.UpdateWindow();
       
            //开始作复位工作
            if (m_pUndoArray.Count!=0)
            {                
                CommonFunction.DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray);
   
                m_pLastPoint=(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1);               

                m_pFeedback = new NewLineFeedbackClass(); 
                m_pLineFeed =(NewLineFeedback)m_pFeedback;
                m_pLineFeed.Display = m_pActiveView.ScreenDisplay;
                if (m_pLineFeed !=null) m_pLineFeed.Stop();
                m_pLineFeed.Start(m_pLastPoint);
   
				m_MapControl.ActiveView.GraphicsContainer.DeleteAllElements();
				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pLastPoint);      
				m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新
  
            }
            else 
            {  //复位
			   m_pFeedback.MoveTo(m_pAnchorPoint);
               Reset();
            }
        }

        public override void OnBeforeScreenDraw(int hdc)
        {
            // TODO:  添加 DrawRectRelative2P.OnBeforeScreenDraw 实现
            base.OnBeforeScreenDraw (hdc);           
//            if (m_pLineFeed !=null)  
//            {
//                m_pLineFeed.Stop();              
//            }
        }

        public override void OnAfterScreenDraw(int hdc)
        {
            // TODO:  添加 DrawRectRelative2P.OnAfterScreenDraw 实现
            base.OnAfterScreenDraw (hdc);
            CommonFunction.DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray);
        }

		private void Reset()
		{	
			m_pActiveView.GraphicsContainer.DeleteAllElements(); 
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新

			m_pEnvelope = null;
			m_bInUse = false;
			m_pUndoArray.RemoveAll();//清空回退数组 
			m_pLineFeed = null;
			m_mouseDownCount = 0; 
			m_bFixSideLength=false;
			m_bInputWindowCancel = true;

			m_pStatusBarService.SetStateMessage("就绪");

		}
     
        public override void OnKeyDown(int keyCode, int shift)
        {
            // TODO:  添加 DrawRectRelative2P.OnKeyDown 实现
            base.OnKeyDown (keyCode, shift);
         
			if (keyCode == 85 && m_mouseDownCount==1)//按U键,回退
			{
				Undo();

				return;
			}
		
			if (keyCode == 70 && m_mouseDownCount==1)//按F键,输入长度+方位角
			{  
				frmLengthAzim.m_pPoint = m_pLastPoint;
				frmLengthAzim fromLengthDirect = new frmLengthAzim();                   
				fromLengthDirect.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{ 
					DrawRectRelative2PMouseDown(m_pAnchorPoint);
				}

				return;
			}

			if (keyCode == 68 && m_mouseDownCount==1)//按D键,输入固定长度
			{
				frmFixLength fromFixLength = new frmFixLength();
				fromFixLength.ShowDialog();    
             
				return;
			}
			
			if (keyCode == 79 && m_mouseDownCount==1)//按(O)orientation键,输入方向
			{     
				frmFixAzim fromFixAzim = new frmFixAzim();
				fromFixAzim.ShowDialog();     

				return;
			}

			if (keyCode == 66 && m_mouseDownCount==2)//按B键,输入边长
			{
				frmFixSideLength fromFixSideLength = new frmFixSideLength();
				fromFixSideLength.ShowDialog();

				return;
			}   

			if (keyCode == 65 && m_mouseDownCount==1)//按A键,输入绝对坐标
			{       
				frmAbsXYZ.m_pPoint = m_pAnchorPoint;
				frmAbsXYZ formXYZ = new frmAbsXYZ();
				formXYZ.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{ 
					DrawRectRelative2PMouseDown(m_pAnchorPoint);
				}

				return;
			}

			if (keyCode == 82 && m_mouseDownCount==1)//按R键,输入相对坐标
			{
				frmRelaXYZ.m_pPoint = m_pLastPoint;
				frmRelaXYZ formRelaXYZ = new frmRelaXYZ();
				formRelaXYZ.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{ 
					DrawRectRelative2PMouseDown(m_pAnchorPoint);  
				}

				return;
			}		

			if (keyCode == 80 && m_mouseDownCount==1)//按P键,平行尺
			{							
				m_pSegment  = null;
				m_bKeyCodeP = true;
						
				return;
			}

			if ((keyCode == 69 || keyCode == 13 || keyCode == 32) &&  m_mouseDownCount==2)//按E键、ENTER 键、SPACEBAR 键结束绘制
			{
				EndDrawRectRelative2P();
                
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
								
					break;

				case "长度+方位角(&F)..":
					frmLengthAzim.m_pPoint = m_pLastPoint;
					frmLengthAzim fromLengthDirect = new frmLengthAzim();                   
					fromLengthDirect.ShowDialog();
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{ 
						DrawRectRelative2PMouseDown(m_pAnchorPoint);
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

				case "矩形边长(&B)...":
					frmFixSideLength fromFixSideLength = new frmFixSideLength();
					fromFixSideLength.ShowDialog();

					break;				

				case "绝对坐标(&A)...":
					frmAbsXYZ.m_pPoint = m_pAnchorPoint;
					frmAbsXYZ formXYZ = new frmAbsXYZ();
					formXYZ.ShowDialog();
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{ 
						DrawRectRelative2PMouseDown(m_pAnchorPoint);
					}

					break;

				case "相对坐标(&R)...":
					frmRelaXYZ.m_pPoint = m_pLastPoint;
					frmRelaXYZ formRelaXYZ = new frmRelaXYZ();
					formRelaXYZ.ShowDialog();
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{ 
						DrawRectRelative2PMouseDown(m_pAnchorPoint);  
					}

					break;

				case "平行(&P)...":
					m_pSegment  = null;
					m_bKeyCodeP = true;
					CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_BeginConstructParallelPoint,ref m_pAnchorPoint);

					break;

				case "直角(&S)...":
				
					break;

				case "封闭完成(&C)":

					break;

				case "完成(&E)":
					EndDrawRectRelative2P();

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