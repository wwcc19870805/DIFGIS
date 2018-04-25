/*---------------------------------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawArc3P.cs
			// 文件功能描述：给定圆周上的三点 p1+p2+p3，绘制弧\扇形区域
			//
			// 
			// 创建标识：YuanHY 20051226
            // 操作说明：按shift键，修改圆周上的三点坐标
			//           A键输入绝对坐标
            //     　　  R键输入相对坐标
		    //           P键平行尺
			//　　　　　 ESC键取消所有操作
			//           ENTER键、SPACEBAR键结束绘制
            //
			// 修改记录：增加平行尺功能									By YuanHY  20060308
            //           增加正交功能　　								By YuanHY  20060308
			//           在圆弧未形成之前，绘制一条直线以增强视觉效果 	By YuanHY  20060403	
			//           向框架状态栏传送提示信息						By YuanHY  20060615	
            // 修改标识：Modify by YuanHY20081112
            // 修改描述：增加了ESC键的操作切换到选择命令
-------------------------------------------------------------------------------------------*/
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
    /// DrawArc3P 的摘要说明。
    /// </summary>
    public class DrawArc3P:AbstractMapCommand
    {
		private IDFApplication m_App;
        private IMapControl2   m_MapControl;
        private IMap           m_FocusMap;
        private ILayer         m_CurrentLayer;
        private IActiveView    m_pActiveView;
		private IMapView       m_MapView = null;

        private IDisplayFeedback m_pFeedback;
        private INewLineFeedback m_pLineFeed;
    
        private bool          m_bInUse;
		public  static IPoint m_pPoint;
		public  static IPoint m_pAnchorPoint;
		private        IPoint m_pLastPoint;
        public  static bool   m_bModify;

        public  static IPoint m_pPoint1 = new PointClass();
        public  static IPoint m_pPoint2 = new PointClass();
        public  static IPoint m_pPoint3 = new PointClass();
		public  static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消

        private IPoint m_pCenterPoint   = new PointClass();
        private double m_Ca = 0;//圆心角

		private double   m_dblTolerance;   //固定容限值
		private ISegment m_pSegment = null;//平行尺方法修改锚点坐标时，捕捉到的边线的某个片断
		private bool     m_bKeyCodeP;      //是否按P键，开启平行尺
		private IPoint   m_BeginConstructParallelPoint;//开始平行尺，鼠标右击的点

        private IArray pLineArray = new ESRI.ArcGIS.esriSystem.ArrayClass();

		private EditContextMenu  m_editContextMenu;//右键菜单

		private IStatusBarService m_pStatusBarService;//状态栏服务

		private bool	isEnabled   = false;
		private string	strCaption  = "给定圆周上的三点，绘制弧/扇形区域";
		private string	strCategory = "编辑";

		private IEnvelope m_pEnvelope = new EnvelopeClass();


		public DrawArc3P()
		{	//右键菜单	
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
					if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)						isEnabled = true;
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
            //else
            //{
            //    //Modify by YuanHY20081112
            //    m_ToolbarManager = (m_App.Workbench as DefaultWorkbench).commandBarManager;
            //    ToolBase toolBase = m_ToolbarManager.Tools["2dmap.DFEditorTool.DrawArc3P"];
            //    if (m_MapView.CurrentTool == this)
            //    {
            //        if (((StateButtonTool)toolBase).Checked == false)
            //        {
            //            ((StateButtonTool)toolBase).Checked = true;//按下状态
            //            return;
            //        }
            //    }
            //}

            m_MapView.CurrentTool = this;
		   
			m_MapControl   = m_App.CurrentMapControl;            
            m_FocusMap     = m_MapControl.ActiveView.FocusMap;
			m_pActiveView  = (IActiveView)this.m_FocusMap;
            m_CurrentLayer = m_App.CurrentEditLayer;
					
			//获得状态栏的服务
			m_pStatusBarService = m_App.StatusBarService ;

			CurrentTool.m_CurrentToolName  = CurrentTool.CurrentToolName.drawArc3P;

			CommonFunction.MapRefresh(m_pActiveView);
            
			m_dblTolerance=CommonFunction.ConvertPixelsToMapUnits(m_pActiveView, 4);

			m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair ;

			m_pStatusBarService.SetStateMessage("提示：依次指定圆弧上的三点(A:绝对XY/R:相对XY/P:平行尺/ESC:取消/ENTER:结束/+shift:修改坐标)");

            //记录用户操作
            clsUserLog useLog = new clsUserLog();
            useLog.UserName = DFApplication.LoginUser;
            useLog.UserRoll = DFApplication.LoginSubSys;
            useLog.Operation = "绘制弧/扇形";
            useLog.LogTime = System.DateTime.Now;
            useLog.TableLog = (m_App.CurrentWorkspace as IFeatureWorkspace).OpenTable("WSGRI_LOG");
            useLog.setUserLog();

        }
    
        public override void UnExecute()
        {
            // TODO:  添加 DrawArc3P.UnExecute 实现
			m_pStatusBarService.SetStateMessage("就绪");

        }
	
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawArc3P.OnMouseDown 实现
            base.OnMouseDown (button, shift, x, y, mapX, mapY);

			m_pStatusBarService.SetStateMessage("依次指定: 圆弧上的三点(A:绝对XY/R:相对XY/P:平行尺/ESC:取消/ENTER:结束/+shift:修改坐标)");

			//以后可以删除之
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
				DrawArc3PMouseDown(m_pAnchorPoint,shift);
			}
			else
			{
				MessageBox.Show("超出地图范围");
			}				
			
        }

		private void DrawArc3PMouseDown(IPoint pPoint, int shift)
		{
			if (!m_bInUse)
			{							
				m_pFeedback = new NewLineFeedbackClass(); 
				m_pLineFeed = (INewLineFeedback)m_pFeedback;
				m_pLineFeed.Display = m_pActiveView.ScreenDisplay;

				pLineArray.Add(pPoint);

				if(pLineArray.Count==1)
				{
					m_pLineFeed.Start(pPoint); 
				}

				m_pLastPoint  = pPoint ;
                
				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,pPoint);
               
				if (pLineArray.Count == 2)
				{
					m_bInUse = true;
					m_pLineFeed.Stop();
					m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics ,null,m_pEnvelope);
				}
			}  
			else
			{
				if (pLineArray.Count >= 2)
				{
					EndDrawArc3P(pPoint,shift);
				}
			}

			m_pSegment = null;//清空捕捉到的片断

		}

		#region//右键菜单项是否可用
		private void toolbarsManagerToolsEnabledOrNot()
		{
			if(!m_bInUse)//零点
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
			
			if(pLineArray.Count ==1 )//一点
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
				m_editContextMenu.toolbarsManager.Tools["btnEnd"].SharedProps.Enabled        = false;
				m_editContextMenu.toolbarsManager.Tools["btnESC"].SharedProps.Enabled        = true;
			}
			else if (pLineArray.Count >1)
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
    
        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawArc3P.OnMouseMove 实现
            base.OnMouseMove (button, shift, x, y, mapX, mapY);

			
            m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
			m_pAnchorPoint = m_pPoint;			

			//+++++++++++++开始捕捉+++++++++++++++++++++			
			bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,null,m_pAnchorPoint);
           
			if (m_pLastPoint!= null)
			{
				//########################平行尺########################			
				CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_pPoint,ref m_pAnchorPoint);

				//&&&&&&&&&&&&&&&&&&&&&&&& 正 交 &&&&&&&&&&&&&&&&&&&&&&&
				CommonFunction.PositiveCross(m_pLastPoint,ref m_pAnchorPoint,m_App.CurrentConfig.cfgPositiveCross.IsPositiveCross ); 
			}

			if(pLineArray.Count >=1)
			{
				if( m_pFeedback != null)  m_pFeedback.Display = m_pActiveView.ScreenDisplay;
				m_pFeedback.MoveTo(m_pAnchorPoint);
			}

			if (m_bInUse)
            {
                pLineArray.Add(m_pAnchorPoint); 
                if (pLineArray.Count >= 3)
                {
                    IPoint pPoint1 = (IPoint)pLineArray.get_Element(0); 
                    IPoint pPoint2 = (IPoint)pLineArray.get_Element(1); 
                    IPoint pPoint3 = (IPoint)pLineArray.get_Element(pLineArray.Count-1 );

                    //计算圆心坐标 和 半径
                   double R;
                    m_pCenterPoint = CommonFunction.GetCenter_P123(pPoint1, pPoint2, pPoint3);
                    R = CommonFunction.GetDistance_P12(pPoint1,m_pCenterPoint);

                    //计算m_pCenterPoint到起点p1的方位角和到端点的方位角
                    double Ap01;
                    double Ap03;
                    double Ap12;
                    double Ap13;
                    Ap01=CommonFunction.GetAzimuth_P12(m_pCenterPoint,pPoint1);
                    Ap03=CommonFunction.GetAzimuth_P12(m_pCenterPoint,pPoint3);
                    Ap12=CommonFunction.GetAzimuth_P12(pPoint1,pPoint2);
                    Ap13=CommonFunction.GetAzimuth_P12(pPoint1,pPoint3);
                   
                    //计算圆心角
                    double dA;
                    dA = Ap13 - Ap12;
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

                    m_pLineFeed.Stop(); 
                    m_pLineFeed.Start(pPoint1); 
                    IPoint pm = new PointClass();
				
                    if (dA >= 0 && dA < Math.PI) 
                    {
                        for (int i = 0; i <= m_Ca/CommonFunction.DegToRad(5)-1; i++)
                        {			
                            pm.X = m_pCenterPoint.X + R * Math.Cos(Ap01+CommonFunction.DegToRad(5 * (i + 1)));
                            pm.Y = m_pCenterPoint.Y + R * Math.Sin(Ap01+CommonFunction.DegToRad(5 * (i + 1)));
                            m_pLineFeed.AddPoint(pm);
                        }
                    }
                    else if(dA >= Math.PI && dA < Math.PI * 2)
                    {
                        for (int i = 0; i <= m_Ca/CommonFunction.DegToRad(5)-1; i++)
                        {			
                            pm.X = m_pCenterPoint.X + R * Math.Cos(Ap01-CommonFunction.DegToRad(5 * (i + 1)));
                            pm.Y = m_pCenterPoint.Y + R * Math.Sin(Ap01-CommonFunction.DegToRad(5 * (i + 1)));
                            m_pLineFeed.AddPoint(pm);
                        }	
                    }			
                    m_pLineFeed.AddPoint(pPoint3);
                }
            }			

        }
    
        public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawArc3P.OnMouseUp 实现
            base.OnMouseUp (button, shift, x, y, mapX, mapY);
        }

        public override void OnBeforeScreenDraw(int hdc)
        {
            // TODO:  添加 DrawArc3P.OnBeforeScreenDraw 实现
            base.OnBeforeScreenDraw (hdc);
           
            if (m_pFeedback !=null && pLineArray.Count!=0)  
            {                
				if(pLineArray.Count>=0)
				{
					m_pLineFeed.MoveTo((IPoint)pLineArray.get_Element(pLineArray.Count-1));
				}

				if(pLineArray.Count>=2)
				{
					m_pLineFeed.Stop();
				}

            }    
        }

		private void EndDrawArc3P(IPoint pPoint,int shift)
		{
			IGeometry pGeom = null;
			IPolyline pPolyline;
			IPolygon  pPolygon;
        
			pLineArray.Add(pPoint);

			m_pLineFeed = (INewLineFeedback)m_pFeedback;
			if( m_pLineFeed != null) m_pLineFeed.Stop();
                      
			IPoint pFromPoint = (IPoint)pLineArray.get_Element(0);
			IPoint pTempPoint =( IPoint)pLineArray.get_Element(2);
			IPoint pToPoint = (IPoint)pLineArray.get_Element(pLineArray.Count-1 );
			IPoint pCenterPoint = CommonFunction.GetCenter_P123(pFromPoint, pTempPoint, pToPoint);

			m_pPoint1 = pFromPoint;
			m_pPoint2 = pTempPoint;
			m_pPoint3 = pToPoint;
     
			ICircularArc pArc=new CircularArcClass();
			pArc.PutCoords(pCenterPoint,pFromPoint,pToPoint,esriArcOrientation.esriArcMajor);
            
			if (m_bInUse)
			{        
				if (shift == 1)//若果按住shift健就弹出对话框，让用户修改圆周上的坐标值
				{
					frmCircle3P formCircle3P = new frmCircle3P();
					formCircle3P.ShowDialog();
   
					if( m_bModify)//修改坐标值了
					{
　　　　　　            pFromPoint = m_pPoint1;
						pToPoint = m_pPoint3;
						pCenterPoint = CommonFunction.GetCenter_P123(m_pPoint1, m_pPoint2, m_pPoint3);
						m_bModify = false;
					}
				}
                                          
				if ( m_Ca<Math.PI ) //圆心角小于π
				{
					pPolyline = CommonFunction.ArcToPolyline(pFromPoint, pCenterPoint, pToPoint,esriArcOrientation.esriArcMinor);
				}
				else //圆心角大于π
				{
					pPolyline = CommonFunction.ArcToPolyline(pFromPoint, pCenterPoint, pToPoint,esriArcOrientation.esriArcMajor);
				}

				switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
				{
					case  esriGeometryType.esriGeometryPolyline: 
						pGeom = (IGeometry)pPolyline;

						break;

					case  esriGeometryType.esriGeometryPolygon:                                           
						ILine  pCenterToFormPointLine = new LineClass();
						pCenterToFormPointLine.PutCoords(pCenterPoint, pToPoint);
						ILine  pToPointToCenterLine = new LineClass();
						pToPointToCenterLine.PutCoords(pFromPoint,pCenterPoint);
                                            
						ISegmentCollection pSegsPolyline;
						pSegsPolyline =(ISegmentCollection) pPolyline;   
                        
						object a = System.Reflection.Missing.Value; 
						object b = System.Reflection.Missing.Value; 
                       
						pSegsPolyline.AddSegment((ISegment)pCenterToFormPointLine, ref a, ref b);                                            
						pSegsPolyline.AddSegment((ISegment)pToPointToCenterLine, ref a, ref b);
                 
						pPolygon =  CommonFunction.PolylineToPolygon((IPolyline)pSegsPolyline);
						pGeom = (IGeometry)pPolygon;
                                           
						break;
  
				}//end switch

				m_pEnvelope = pGeom.Envelope;
				m_pEnvelope.Union(m_pCenterPoint.Envelope);
				if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);;
				CommonFunction.CreateFeature(m_App.Workbench, pGeom, m_FocusMap,m_CurrentLayer); 

				Reset();

				pLineArray.RemoveAll();
				m_bInUse = false;

			}//end else if (m_bInUse)     
		}

		public override void OnKeyDown(int keyCode, int shift)
		{
			// TODO:  添加 DrawArc3P.OnKeyDown 实现
			base.OnKeyDown (keyCode, shift);        

			if (keyCode == 65)//按A键,输入绝对坐标
			{    				
				frmAbsXYZ.m_pPoint = m_pAnchorPoint;
				frmAbsXYZ formXYZ = new frmAbsXYZ();
				formXYZ.ShowDialog();

				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                    
					DrawArc3PMouseDown(m_pAnchorPoint,0);
				}

				return;
			}

			if (keyCode == 82 && pLineArray.Count >0 )//按R键,输入相对坐标
			{    				
				IPoint tempPoint = new PointClass();
				tempPoint.X = m_pLastPoint.X;
				tempPoint.Y = m_pLastPoint.Y;
				frmRelaXYZ.m_pPoint = tempPoint;
				frmRelaXYZ formRelaXYZ = new frmRelaXYZ();
				formRelaXYZ.ShowDialog();

				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                   
					DrawArc3PMouseDown(m_pAnchorPoint,0);  
				}

				return;
			}

			if (keyCode == 80 && pLineArray.Count >0 )//按P键平行尺
			{							
				m_pSegment  = null;
				m_bKeyCodeP = true;							
				return;
			}

			if ((keyCode == 13 || keyCode == 32) && pLineArray.Count == 2)//按ENTER 键、SPACEBAR 键
			{    
				EndDrawArc3P(m_pAnchorPoint,shift);	
				return;		
			}

			if (keyCode == 27)//ESC 键，取消所有操作
			{
				Reset();

                this.Stop();

                WSGRI.DigitalFactory.Commands.ICommand command = DFApplication.Application.GetCommand("WSGRI.DigitalFactory.DF2DControl.cmdPan");
                if (command != null) command.Execute();

				return;
			}
		}

		#region//右键菜单点击事件
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
						DrawArc3PMouseDown(m_pAnchorPoint,0);
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
						DrawArc3PMouseDown(m_pAnchorPoint,0);  
					}

					break;

				case "平行(&P)...":
					m_pSegment  = null;
					m_bKeyCodeP = true;
					CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_BeginConstructParallelPoint,ref m_pAnchorPoint);

					break;

				case "完成(&E)":
					EndDrawArc3P(m_pAnchorPoint,0);

					break;
					
				case "取消(ESC)":
					Reset();

					break;

				default:

					break;
			}
			
		}
		#endregion

		private void Reset()
		{
			m_pFeedback = null;
			m_pLineFeed = null;   
			m_bInUse    = false;
			m_bModify   = false;
			m_bInputWindowCancel = true;

			if(m_pLastPoint != null)m_pLastPoint.SetEmpty();
			pLineArray.RemoveAll();

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
