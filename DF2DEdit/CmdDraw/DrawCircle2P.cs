/*-------------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawCircle2P.cs
			// 文件功能描述：给定圆直径两端点坐标 p1+p2，绘制圆\圆形区域
			//
			// 
			// 创建标识：YuanHY 20051226
            // 操作说明：按shift键，修改圆直径两端点坐标
    		//           A键输入绝对坐标
            //     　　  R键输入相对坐标
			//           P键平行尺
			//　　　　　 ESC键取消所有操作
			//           ENTER键、SPACEBAR键结束绘制
            //
			// 修改记录：增加平行尺功能				By YuanHY  20060309
            //           增加正交功能　　			By YuanHY  20060309　　
			//           向框架状态栏传送提示信息	By YuanHY  20060615 　　　    
-----------------------------------------------------------------------*/
using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
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
	/// DrawCircle2P 的摘要说明。
	/// </summary>
	public class DrawCircle2P:AbstractMapCommand
	{

		private IDFApplication m_App;        
        private IMapControl2   m_MapControl;
        private IMap           m_FocusMap;
        private ILayer         m_CurrentLayer;
        private IActiveView    m_pActiveView;
		private IMapView       m_MapView = null;

        private IDisplayFeedback   m_pFeedback;
        private INewCircleFeedback m_pCircleFeed;

        private bool m_bFirst;
        private bool m_bSecond;

		public  static IPoint m_pPoint;
		public  static IPoint m_pAnchorPoint;
		private IPoint m_pLastPoint;
        public  static bool m_bModify;

        public  static IPoint m_pPoint1 = new PointClass();
        public  static IPoint m_pPoint2 = new PointClass();
		public  static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消

        private IPoint m_pCenterPoint   = new PointClass();

		private double   m_dblTolerance;     //固定容限值
		private ISegment m_pSegment = null;  //平行尺方法修改锚点坐标时，捕捉到的边线的某个片断
		private bool     m_bKeyCodeP;		 //是否按P键，开启平行尺
		private IPoint   m_BeginConstructParallelPoint;//开始平行尺，鼠标右击的点

		private EditContextMenu  m_editContextMenu;//右键菜单

		private IStatusBarService m_pStatusBarService;//状态栏服务

		private bool	isEnabled   = false;
		private string	strCaption  = "给定圆直径两端点坐标，绘制圆/圆形区域";
		private string	strCategory = "编辑";

		private IEnvelope m_pEnvelope = new EnvelopeClass();

		public DrawCircle2P()
		{
			//右键菜单	
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

			CurrentTool.m_CurrentToolName  = CurrentTool.CurrentToolName.drawCircle2P;

			CommonFunction.MapRefresh(m_pActiveView);
           
			m_dblTolerance=CommonFunction.ConvertPixelsToMapUnits(m_MapControl.ActiveView, 4);

			m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair ;

			m_pStatusBarService.SetStateMessage("提示：依次指定圆直径上的1.一点;2.另一点。(A:绝对XY/R:相对XY/P:平行尺/ESC:取消/ENTER:结束/+shift:修改坐标)");

            //记录用户操作
            clsUserLog useLog = new clsUserLog();
            useLog.UserName = DFApplication.LoginUser;
            useLog.UserRoll = DFApplication.LoginSubSys;
            useLog.Operation = "绘制圆形";
            useLog.LogTime = System.DateTime.Now;
            useLog.TableLog = (m_App.CurrentWorkspace as IFeatureWorkspace).OpenTable("WSGRI_LOG");
            useLog.setUserLog();

		}
    
        public override void UnExecute()
        {
            // TODO:  添加 DrawCircle2P.UnExecute 实现
			m_pStatusBarService.SetStateMessage("就绪");

        }  

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawCircle2P.OnMouseDown 实现
            base.OnMouseDown (button, shift, x, y, mapX, mapY);

			m_pStatusBarService.SetStateMessage("依次指定圆直径上:1.一点;2.另一点。(A:绝对XY/R:相对XY/P:平行尺/ESC:取消/ENTER:结束/+shift:修改坐标)");

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
				DrawCircle2PMouseDown(m_pAnchorPoint,shift); 
			}
			else
			{
				MessageBox.Show("超出地图范围");
			}	
			           

        }

		#region//右键菜单项是否可用

		private void toolbarsManagerToolsEnabledOrNot()
		{
			if(!m_bFirst)//零点
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
			else //一点
			{
				m_editContextMenu.toolbarsManager.Tools["btnUndo"].SharedProps.Enabled       = false;
				m_editContextMenu.toolbarsManager.Tools["btnLeftCorner"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnFixAzim"].SharedProps.Enabled    = false; 
				m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled	 = false;
				m_editContextMenu.toolbarsManager.Tools["btnSideLength"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnLengthAzim"].SharedProps.Enabled = false;
				m_editContextMenu.toolbarsManager.Tools["btnAbsXYZ"].SharedProps.Enabled     = true; 
				m_editContextMenu.toolbarsManager.Tools["btnRelaXYZ"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnParllel"].SharedProps.Enabled    = true;
				m_editContextMenu.toolbarsManager.Tools["btnRt"].SharedProps.Enabled         = false; 
				m_editContextMenu.toolbarsManager.Tools["btnColse"].SharedProps.Enabled      = false;
				m_editContextMenu.toolbarsManager.Tools["btnEnd"].SharedProps.Enabled        = true;
				m_editContextMenu.toolbarsManager.Tools["btnESC"].SharedProps.Enabled        = true;
			}
			
		}
		#endregion
       
		private void DrawCircle2PMouseDown(IPoint pPoint,int shift)
		{
			IGeometry pGeom = null;
			IPolyline pPolyline;
			IPolygon pPolygon;

			if(!m_bFirst) //如果命令没有使用
			{
 　　　　       m_pPoint1 = pPoint;
				m_bFirst  = true;
				m_bSecond = false;

				m_pFeedback = new NewCircleFeedbackClass();

				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pPoint1);
			}
			else if (!m_bSecond)//如果命令正在使用
			{
				m_pPoint2 = pPoint;
				m_bFirst = false;

				m_pCenterPoint = CommonFunction.GetCircleCenter_P12(m_pPoint1,pPoint);
                       
				m_pCircleFeed = (NewCircleFeedbackClass)m_pFeedback;
				m_pCircleFeed.Display = m_pActiveView.ScreenDisplay;             
				m_pCircleFeed.Stop();
				m_pCircleFeed.Start(m_pCenterPoint);
				m_pFeedback.MoveTo(pPoint);

				ICircularArc pCircularArc = new CircularArcClass();
				pCircularArc = m_pCircleFeed.Stop();
				m_pCenterPoint = pCircularArc.CenterPoint;

				if (shift == 1)//若果按住shift健就弹出对话框，让用户修改圆周上的坐标值
				{
					frmCircle2P formCircle2P = new frmCircle2P();
					formCircle2P.ShowDialog();

					if( m_bModify)//修改坐标值了
					{
　　　　　　            //计算圆心坐标
						m_pCenterPoint = CommonFunction.GetCircleCenter_P12(m_pPoint1, m_pPoint2);       
						m_bModify = false;   
					}
				}

				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pPoint2);

				switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
				{
					case  esriGeometryType.esriGeometryPolyline:  
						pPolyline = CommonFunction.ArcToPolyline(pCircularArc.FromPoint, pCircularArc.CenterPoint, pCircularArc.FromPoint,esriArcOrientation.esriArcClockwise);
						pGeom = (IGeometry)pPolyline;
						break;
					case esriGeometryType.esriGeometryPolygon:
						pPolyline = CommonFunction.ArcToPolyline(pCircularArc.FromPoint, pCircularArc.CenterPoint, pCircularArc.FromPoint,esriArcOrientation.esriArcClockwise);
						pPolygon  =  CommonFunction.PolylineToPolygon(pPolyline);
						pGeom = (IGeometry)pPolygon;
						break;
					default:
						break;

				}//end switch

				m_pEnvelope = pGeom.Envelope;
				if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);;
				CommonFunction.CreateFeature(m_App.Workbench, pGeom, m_FocusMap, m_CurrentLayer);  
								　　   
				Reset();
              
			}//end if(!m_bSecond)

			m_pLastPoint = pPoint;

			m_pSegment  = null;

		}
        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawCircle2P.OnMouseMove 实现
            base.OnMouseMove (button, shift, x, y, mapX, mapY);

			
            m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
			
			m_pAnchorPoint = m_pPoint;

			//+++++++++++++开始捕捉+++++++++++++++++++++	
			if(m_pLastPoint!= null)
			{
				if(m_pLastPoint.IsEmpty) 
				{
					bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,null,m_pAnchorPoint);
				}
			}
			else
			{
				if(!m_pLastPoint.IsEmpty) 
				{
					bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,m_pLastPoint,m_pAnchorPoint);
				}  
			}

            if (m_bFirst)
            { 
				//########################平行尺########################			
				CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_pPoint,ref m_pAnchorPoint);
				
				//&&&&&&&&&&&&&&&&&&&&&&&& 正 交 &&&&&&&&&&&&&&&&&&&&&&&
				CommonFunction.PositiveCross(m_pLastPoint,ref m_pAnchorPoint,m_App.CurrentConfig.cfgPositiveCross.IsPositiveCross ); 		
			
                m_pCenterPoint = CommonFunction.GetCircleCenter_P12(m_pPoint1,m_pAnchorPoint);
                       
                m_pCircleFeed = (NewCircleFeedbackClass)m_pFeedback;
                m_pCircleFeed.Display = m_pActiveView.ScreenDisplay;
             
                m_pCircleFeed.Stop();
                m_pCircleFeed.Start(m_pCenterPoint);
                m_pFeedback.MoveTo(m_pAnchorPoint);
            }



        }

        public override void OnBeforeScreenDraw(int hdc)
        {
            // TODO:  添加 DrawCircle2P.OnBeforeScreenDraw 实现
            base.OnBeforeScreenDraw (hdc);
           
            if (m_pFeedback != null)  
            {
                m_pFeedback.MoveTo(m_pCenterPoint);              
            }    
        }

		public override void OnKeyDown(int keyCode, int shift)
		{
			// TODO:  添加 DrawCircle2P.OnKeyDown 实现
			base.OnKeyDown (keyCode, shift);
			
			if (keyCode == 65)//按A键,输入绝对坐标
			{    				
				frmAbsXYZ.m_pPoint = m_pAnchorPoint;
				frmAbsXYZ formXYZ = new frmAbsXYZ();
				formXYZ.ShowDialog();

				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                    
					DrawCircle2PMouseDown(m_pAnchorPoint,0);
				}

				return;
			}

			if (keyCode == 82 && m_bFirst)//按R键,输入相对坐标
			{    				
				IPoint tempPoint = new PointClass();
				tempPoint.X = m_pLastPoint.X;
				tempPoint.Y = m_pLastPoint.Y;
				frmRelaXYZ.m_pPoint = tempPoint;
				frmRelaXYZ formRelaXYZ = new frmRelaXYZ();
				formRelaXYZ.ShowDialog();

				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                   
					DrawCircle2PMouseDown(m_pAnchorPoint,0);  
				}

				return;
			}

			if (keyCode == 80 && m_bFirst)//按P键平行尺
			{							
				m_pSegment  = null;
				m_bKeyCodeP = true;	
						
				return;
			}
			
			if ((keyCode == 13 || keyCode == 32) && m_bFirst)//按ENTER 键、SPACEBAR 键
			{     
				DrawCircle2PMouseDown(m_pAnchorPoint,shift);

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
				case "绝对坐标(&A)...":
					frmAbsXYZ.m_pPoint = m_pAnchorPoint;
					frmAbsXYZ formXYZ = new frmAbsXYZ();
					formXYZ.ShowDialog();

					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{                    
						DrawCircle2PMouseDown(m_pAnchorPoint,0);
					}

					break;

				case "相对坐标(&R)...":
					IPoint tempPoint = new PointClass();
					tempPoint.X = m_pLastPoint.X;
					tempPoint.Y = m_pLastPoint.Y;
					frmRelaXYZ.m_pPoint = tempPoint;
					frmRelaXYZ formRelaXYZ = new frmRelaXYZ();
					formRelaXYZ.ShowDialog();

					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{                   
						DrawCircle2PMouseDown(m_pAnchorPoint,0);  
					}

					break;

				case "平行(&P)...":
					m_pSegment  = null;
					m_bKeyCodeP = true;
					CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_BeginConstructParallelPoint,ref m_pAnchorPoint);

					break;

				case "完成(&E)":
					DrawCircle2PMouseDown(m_pAnchorPoint,0);

					break;
					
				case "取消(ESC)":
					Reset();

					break;

				default:

					break;
			}
			
		}

		private void Reset()
		{
			m_bFirst  = false;
			m_bSecond = false;
			m_bInputWindowCancel = true;
            m_pCircleFeed = null;
			m_pFeedback   = null;
			if(m_pLastPoint != null) m_pLastPoint.SetEmpty();;
			m_pSegment  = null;
			m_pActiveView.GraphicsContainer.DeleteAllElements();
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新
			m_pEnvelope = null;
		
			m_pStatusBarService.SetStateMessage("就绪");

		}

		public override void Stop()
		{
			//this.Reset();
			base.Stop();
		}

    }
}
