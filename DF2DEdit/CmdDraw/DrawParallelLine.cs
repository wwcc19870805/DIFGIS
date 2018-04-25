/*-------------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawParallelLine.cs
			// 文件功能描述：绘制平行线(多线)
			//
			// 
			// 创建标识：YuanHY 20060305
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
			// 修改记录：增加平行尺					By YuanHY  20060308
			//           增加正交功能　　			By YuanHY  20060308
			//           增加右键菜单　　			By YuanHY  20060330
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
	/// DrawParallelLine 的摘要说明。
	/// </summary>
	public class DrawParallelLine: AbstractMapCommand
	{
		private IDFApplication m_App;      
		private IMapControl2   m_MapControl;
		private IMap           m_FocusMap;
		private ILayer         m_CurrentLayer;
		private IActiveView    m_pActiveView;
		private IMapView       m_MapView = null;

		private IDisplayFeedback m_pFeedback			= new NewLineFeedbackClass();
		private IDisplayFeedback m_pLeftParFeedback		= new NewLineFeedbackClass();
		private IDisplayFeedback m_pLeftParNextFeedback = new NewLineFeedbackClass();
		private IDisplayFeedback m_pRightParFeedback	= new NewLineFeedbackClass();
		private IDisplayFeedback m_pRightParNextFeedback= new NewLineFeedbackClass();
    
		private INewLineFeedback m_pLineFeed;            //主线
		private INewLineFeedback m_pLeftParLineFeed;     //右从线第已经形成的部分
		private INewLineFeedback m_pLeftParNextLineFeed; //右从线第将要形成的部分
		private INewLineFeedback m_pRightParLineFeed;    //右从线第已经形成的部分
		private INewLineFeedback m_pRightParNextLineFeed;//右从线第将要形成的部分

		private IDisplayFeedback m_pLastFeedback;
		private INewLineFeedback m_pLastLineFeed;

		private bool   m_bInUse;     //是否开始主线的绘制
		private IPoint m_pFirstPoint;//主线第一个点
		private IPoint m_pLastPoint; //倒数第一个点
		private IPoint m_pLast2Point;//主线倒数第二个点
		private IPoint m_pLast3Point;//主线倒数第二个点

		public static IPoint m_pPoint;
		public static IPoint m_pAnchorPoint;
		public static bool   m_bFixLength;   //是否已固定长度
		public static double m_dblFixLength;
		public static bool   m_bFixDirection;//是否已固定方向
		public static double m_dblFixDirection;
		public static bool   m_bFixLeftCorner;//是否左折角
		public static double m_dbFixlLeftCorner;
       
		public static int locationFlag;//绘制平行线的位置关系，＝１在主线的左边绘制从；
        　　　　　　　　　　　　　　　 //                      ＝２在主线的右边绘制从线；
									   //                      ＝３在主线的两边绘制分别绘制两条从线
		public  static double dblDeparture ;              //主线从线的间的宽度
		public  static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消
		private bool          m_bParStart;                //标识绘制从线是否开始

		private IPoint m_pLeftParFirstPoint = new PointClass();//右从线的第一个点
		private IPoint m_pLeftParLastPoint  = new PointClass();//右从线的倒数第一个点
		private IPoint m_pLeftParLast2Point = new PointClass();//右从线的倒数第二个点
		private IPoint m_pRightParFirstPoint= new PointClass();//右从线的第一个点
		private IPoint m_pRightParLastPoint = new PointClass();//右从线的倒数第一个点
		private IPoint m_pRightParLast2Point= new PointClass();//右从线的倒数第二个点

		private IArray m_pUndoArray      = new ArrayClass();
		private IArray m_pUndoArrayLeft  = new ArrayClass();
		private IArray m_pUndoArrayRight = new ArrayClass();
		
		private double   m_dblTolerance;     //固定容限值
		private bool     m_bkeyCodeS;        //是否按S键，开启直角...
		private ISegment m_pSegment = null;  //平行尺方法修改锚点坐标时，捕捉到的边线的某个片断
		private bool     m_bKeyCodeP;        //是否按P键，开启平行尺
		private IPoint   m_BeginConstructParallelPoint;//开始平行尺，鼠标右击的点

		private EditContextMenu  m_editContextMenu;//右键菜单

		private IStatusBarService m_pStatusBarService;//状态栏信息服务

		private bool	isEnabled   = false;
		private string	strCaption  = "绘制平行线(多线)";
		private string	strCategory = "编辑";

		private IEnvelope m_pEnvelope = new EnvelopeClass();
	  
		public DrawParallelLine()
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
					if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)						
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

			CurrentTool.m_CurrentToolName  = CurrentTool.CurrentToolName.drawParallelLine;

			CommonFunction.MapRefresh(m_pActiveView);
            
			m_dblTolerance=CommonFunction.ConvertPixelsToMapUnits(m_MapControl.ActiveView, 4);

			m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair ;

			frmParallel formParallelLine = new frmParallel();
			formParallelLine.ShowDialog();

			m_pStatusBarService.SetStateMessage("提示：U:回退/A:绝对XY/R:相对XY/N:左折角/O:方位角/F:长度＋方向/D:长度/P:平行尺/S:直角.../C:封闭结束/Enter:结束/ESC:取消");//向状态栏传送提示信息

            //记录用户操作
            clsUserLog useLog = new clsUserLog();
            useLog.UserName = DFApplication.LoginUser;
            useLog.UserRoll = DFApplication.LoginSubSys;
            useLog.Operation = "绘制平行线";
            useLog.LogTime = System.DateTime.Now;
            useLog.TableLog = (m_App.CurrentWorkspace as IFeatureWorkspace).OpenTable("WSGRI_LOG");
            useLog.setUserLog();

		}
    
		public override void UnExecute()
		{
			// TODO:  添加 DrawParallelLine.UnExecute 实现
			m_pStatusBarService.SetStateMessage("就绪");

		}
    
		public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawParallelLine.OnMouseDown 实现
			base.OnMouseDown (button, shift, x, y, mapX, mapY);  
          
			m_pStatusBarService.SetStateMessage("提示：U:回退/A:绝对XY/R:相对XY/N:左折角/O:方位角/F:长度＋方向/D:长度/P:平行尺/S:直角.../C:封闭结束/Enter:结束/ESC:取消");//向状态栏传送提示信息
 
			m_CurrentLayer = ((IDFApplication)this.Hook).CurrentEditLayer;
		
			if(((IFeatureLayer)m_App.CurrentEditLayer).FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
			{
				System.Windows.Forms.MessageBox.Show("目标图层不是线类型！","绘制平行线", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
					
				return;
			}

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
				DrawParallelLineMouseDown(m_pAnchorPoint);
			
			}
			else
			{
				MessageBox.Show("超出地图范围");
			}	

			if ( m_bkeyCodeS == true)
				{				
					EndDrawParallelLine_Colse();
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
			else if(2<=m_pUndoArray.Count && m_pUndoArray.Count<3)
			{
				m_editContextMenu.toolbarsManager.Tools["btnUndo"].SharedProps.Enabled       = true;
				m_editContextMenu.toolbarsManager.Tools["btnLeftCorner"].SharedProps.Enabled = true;
				m_editContextMenu.toolbarsManager.Tools["btnFixAzim"].SharedProps.Enabled    = true; 
				m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled	 = true;
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
				//m_editContextMenu.toolbarsManager.Tools["btnFixLength"].SharedProps.Enabled  = true;
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

		private void DrawParallelLineMouseDown(IPoint pPoint)
		{
			IPoint tempPointLeft  = new PointClass();
			IPoint tempPointRight = new PointClass();
		           
			if(!m_bInUse)//如果命令没有使用
			{ 
				m_bInUse = true;
				m_pLineFeed = (INewLineFeedback)m_pFeedback;
				m_pLineFeed.Start(pPoint);
				m_pUndoArray.Add(pPoint);

				m_pLast3Point = pPoint;
				m_pLast2Point = pPoint;
				m_pLastPoint  = pPoint;
				m_pFirstPoint = pPoint;

				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,pPoint);
                
				if( m_pFeedback != null)  m_pFeedback.Display = m_pActiveView.ScreenDisplay;

			}
			else//若果命令正使用中
			{
				m_pLineFeed = (INewLineFeedback)m_pFeedback;

				if( (m_bFixLength ==true ) && ( m_bFixDirection == false) )//以给定一个长度值
				{
					m_bFixLength = false;
				}  
				else if( (m_bFixLength == false) && ( m_bFixDirection ==true ))//以给定一个固定方向值
				{
					m_bFixDirection = false;
				}

				if (m_bFixLeftCorner)
				{
					m_bFixLeftCorner = false;
				}

				m_pLineFeed.AddPoint(pPoint);
				
				m_pUndoArray.Add(pPoint);              
	
				m_pLast3Point = m_pLast2Point ;			
				m_pLast2Point = m_pLastPoint; 
				m_pLastPoint  = pPoint;
            
				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,pPoint);

				double tempParA;  
				double tempParA23;  
				double tempParA21; 

				if (!m_bParStart)//鼠标点击第二下，开始绘制从线的第一个点
				{
　                  m_bParStart = true;
                    
					tempParA = CommonFunction.GetAzimuth_P12(m_pFirstPoint,pPoint);
         
					if (locationFlag == 1 || locationFlag == 3)//在左边
					{  
						m_pLeftParFirstPoint.X =  m_pFirstPoint.X + dblDeparture*Math.Cos((tempParA + Math.PI/2));
						m_pLeftParFirstPoint.Y =  m_pFirstPoint.Y + dblDeparture*Math.Sin((tempParA + Math.PI/2)); 
                    
						m_pLeftParLineFeed     = (INewLineFeedback)m_pLeftParFeedback;
						m_pLeftParLineFeed.Start(m_pLeftParFirstPoint);

						m_pUndoArrayLeft.Add(m_pLeftParFirstPoint);      

						CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pLeftParFirstPoint);
                 
						if( m_pLeftParFeedback != null)  m_pLeftParFeedback.Display = m_pActiveView.ScreenDisplay;
         
					}
					if (locationFlag == 2 || locationFlag == 3)//在右边
					{  
						m_pRightParFirstPoint.X = m_pFirstPoint.X + dblDeparture*Math.Cos((tempParA + 3*Math.PI/2));
						m_pRightParFirstPoint.Y = m_pFirstPoint.Y + dblDeparture*Math.Sin((tempParA + 3*Math.PI/2)); 
                    
						m_pRightParLineFeed = (INewLineFeedback)m_pRightParFeedback;
						m_pRightParLineFeed.Start(m_pRightParFirstPoint);
						
						m_pUndoArrayRight.Add(m_pRightParFirstPoint);  

						CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pRightParFirstPoint);
                 
						if( m_pRightParFeedback != null)  m_pRightParFeedback.Display = m_pActiveView.ScreenDisplay;
					} 

				}
				else//鼠标点击第三、四...下
				{
					tempParA23 = CommonFunction.GetAzimuth_P12(m_pLast2Point,m_pLast3Point);
					tempParA21 = CommonFunction.GetAzimuth_P12(m_pLast2Point,pPoint);

					double dist;
					dist=dblDeparture/Math.Sin((tempParA21-tempParA23)/2);

					if (locationFlag == 1 || locationFlag == 3)//在左边
					{   //计算左从线倒数第二个点的坐标
						m_pLeftParLast2Point.X = m_pLast2Point.X + dist*Math.Cos(tempParA23+(tempParA21-tempParA23)/2 - Math.PI);
						m_pLeftParLast2Point.Y = m_pLast2Point.Y + dist*Math.Sin(tempParA23+(tempParA21-tempParA23)/2 - Math.PI); 
             
						m_pLeftParLineFeed = (INewLineFeedback)m_pLeftParFeedback;
						m_pLeftParLineFeed.AddPoint(m_pLeftParLast2Point);
					
						tempPointLeft.X = m_pLeftParLast2Point.X ;
						tempPointLeft.Y = m_pLeftParLast2Point.Y ;
						m_pUndoArrayLeft.Add(tempPointLeft);
				   
						CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pLeftParLast2Point);

					}
					if (locationFlag == 2 || locationFlag == 3)//在右边
					{   //计算右从线倒数第二个点的坐标
						m_pRightParLast2Point.X =  m_pLast2Point.X + dist*Math.Cos(tempParA23+(tempParA21-tempParA23)/2);
						m_pRightParLast2Point.Y =  m_pLast2Point.Y + dist*Math.Sin(tempParA23+(tempParA21-tempParA23)/2); 
             
						m_pRightParLineFeed = (INewLineFeedback)m_pRightParFeedback;
						m_pRightParLineFeed.AddPoint(m_pRightParLast2Point);

						tempPointRight.X  = m_pRightParLast2Point.X ;
						tempPointRight.Y  = m_pRightParLast2Point.Y ;
						m_pUndoArrayRight.Add(tempPointRight);
                 
						CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pRightParLast2Point);
					}
				}	
			}

			DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray,ref m_pUndoArrayLeft,ref m_pUndoArrayRight);

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
     
		private void DrawParallelLineMouseMove(IPoint pPoint)
		{
			if (CurrentTool.m_CurrentToolName != CurrentTool.CurrentToolName.drawParallelLine ) return;
		
			double dx, dy;
			double tempA;

			if(m_bFixDirection && m_bInputWindowCancel == false)  //此处固定m_pAnchorPoint使其在一个固定方向上
			{
				double  tempDis;
				double  dx1;
				double  dy1;
               
				dx = m_pPoint.X - m_pLastPoint.X;
				dy = m_pPoint.Y - m_pLastPoint.Y;
				tempA = CommonFunction.azimuth (m_pLastPoint,m_pPoint);
				tempDis = CommonFunction.GetDistance_P12(m_pLastPoint,m_pPoint);
				dx1 = tempDis * Math.Cos(m_dblFixDirection * Math.PI / 180);
				dy1 = tempDis * Math.Sin(m_dblFixDirection * Math.PI / 180);
            
				if( m_dblFixDirection >= 0 && m_dblFixDirection < 90 )
				{
					if (tempA >= 90 + m_dblFixDirection && tempA < 270 + m_dblFixDirection )
					{
						dx1 = -dx1;
						dy1 = -dy1;
					}
				}
				else if( m_dblFixDirection >= 90 && m_dblFixDirection < 270 )
				{
					if( tempA >= m_dblFixDirection - 90 && tempA < m_dblFixDirection + 90 )
					{
					}
					else
					{
						dx1 = -dx1;
						dy1 = -dy1;
					}
				}
				else
				{
					if(tempA >= m_dblFixDirection - 270 && tempA < m_dblFixDirection - 90)
					{
						dx1 = -dx1;
						dy1 = -dy1;
					}    
				}
    
				dx = m_pLastPoint.X + dx1;
				dy = m_pLastPoint.Y + dy1;

				m_pPoint.PutCoords( dx, dy);

			}
			else if( m_bFixLength && m_bInputWindowCancel == false )// 以给定一个长度值
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
			else if(m_bFixLeftCorner && m_pUndoArray.Count>1 && m_bInputWindowCancel == false)//给定左折角
			{
				//计算最后一段的方位角
				double TempTA = CommonFunction.GetAzimuth_P12((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count - 2),(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count - 1)); 
　　　　　　　　//计算将要形成的一段的方位角
				tempA =(180 + CommonFunction.RadToDeg(TempTA)) - m_dbFixlLeftCorner;
                
				if (m_dbFixlLeftCorner>360) m_dbFixlLeftCorner = m_dbFixlLeftCorner - 360;

				if (m_dbFixlLeftCorner != tempA)
				{
					m_pPoint=CommonFunction.GetOnePoint_FormPointMousePointFixDirection(m_pLastPoint, m_pPoint, tempA);
				}
			}  
    
			m_pAnchorPoint = m_pPoint;           
			//+++++++++++++开始捕捉+++++++++++++++++++++			
			bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,(IGeometry)m_pLastPoint,m_pAnchorPoint);
             
			if(m_bInUse)
			{
				//########################平行尺########################			
				CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_pPoint,ref m_pAnchorPoint);

				//&&&&&&&&&&&&&&&&&&&&&&&& 正 交 &&&&&&&&&&&&&&&&&&&&&&&
				CommonFunction.PositiveCross(m_pLastPoint,ref m_pAnchorPoint,m_App.CurrentConfig.cfgPositiveCross.IsPositiveCross ); 
	
				m_pFeedback.MoveTo(m_pAnchorPoint);

				double tempParA12;//主线最后一个点到倒数第二个点的方位角
				double tempParA10;//主线最后一个点到即鼠标位置的方位角
				tempParA12 = CommonFunction.GetAzimuth_P12(m_pLastPoint,m_pLast2Point);
				tempParA10 = CommonFunction.GetAzimuth_P12(m_pLastPoint,m_pAnchorPoint);

				double dist;//主线最后一个点到从线即将形成的点的距离
				dist=dblDeparture/Math.Sin((tempParA10-tempParA12)/2);

				if (locationFlag == 1 || locationFlag == 3)//在左边
				{  
					//计算左从线倒数第二个点的坐标
					if (m_pUndoArray.Count == 1)//起点时
					{
						m_pLeftParLast2Point.X = m_pLastPoint.X + dblDeparture*Math.Cos(tempParA10 + Math.PI/2);
						m_pLeftParLast2Point.Y = m_pLastPoint.Y + dblDeparture*Math.Sin(tempParA10 + Math.PI/2);  
					}
					else
					{
						m_pLeftParLast2Point.X = m_pLastPoint.X + dist*Math.Cos(tempParA12+(tempParA10-tempParA12)/2 - Math.PI);
						m_pLeftParLast2Point.Y = m_pLastPoint.Y + dist*Math.Sin(tempParA12+(tempParA10-tempParA12)/2 - Math.PI);  
					}
					//计算左从线倒数第一个点的坐标
					m_pLeftParLastPoint.X  = m_pAnchorPoint.X + dblDeparture*Math.Cos(tempParA10 + Math.PI/2);
					m_pLeftParLastPoint.Y  = m_pAnchorPoint.Y + dblDeparture*Math.Sin(tempParA10 + Math.PI/2); 
　　　　　　　　
					//显示回馈左从线已经形成的部分
					m_pLeftParLineFeed = (INewLineFeedback)m_pLeftParFeedback;
					m_pLeftParLineFeed.MoveTo(m_pLeftParLast2Point);
　　　　　　　　
					//显示回馈左从线将要形成的部分
					m_pLeftParNextLineFeed =(INewLineFeedback)m_pLeftParNextFeedback;       
					if( m_pLeftParNextLineFeed != null) m_pLeftParNextLineFeed.Stop();
					m_pLeftParNextLineFeed.Start(m_pLeftParLast2Point);  
					if( m_pLeftParNextLineFeed != null) m_pLeftParNextLineFeed.Display = m_pActiveView.ScreenDisplay;
					m_pLeftParNextLineFeed.Refresh(m_MapControl.hWnd);
					m_pLeftParNextLineFeed.MoveTo(m_pLeftParLastPoint);
				}
            
				if (locationFlag == 2 || locationFlag == 3)//在右边
				{  
					//计算右从线倒数第二个点的坐标
					if (m_pUndoArray.Count == 1)//起点时
					{
						m_pRightParLast2Point.X = m_pLastPoint.X + dblDeparture*Math.Cos(tempParA10 - Math.PI/2);
						m_pRightParLast2Point.Y = m_pLastPoint.Y + dblDeparture*Math.Sin(tempParA10 - Math.PI/2);  
					}
					else
					{
						m_pRightParLast2Point.X = m_pLastPoint.X + dist*Math.Cos(tempParA12+(tempParA10-tempParA12)/2);
						m_pRightParLast2Point.Y = m_pLastPoint.Y + dist*Math.Sin(tempParA12+(tempParA10-tempParA12)/2);  
					}
					//计算右从线倒数第一个点的坐标
					m_pRightParLastPoint.X  = m_pAnchorPoint.X + dblDeparture*Math.Cos(tempParA10 + 3*Math.PI/2);
					m_pRightParLastPoint.Y  = m_pAnchorPoint.Y + dblDeparture*Math.Sin(tempParA10 + 3*Math.PI/2); 

					//显示回馈右从线已经形成的部分
					m_pRightParLineFeed = (INewLineFeedback)m_pRightParFeedback;
					m_pRightParLineFeed.MoveTo(m_pRightParLast2Point);
　　　　　　　　
					//显示回馈右从线将要形成的部分
					m_pRightParNextLineFeed =(INewLineFeedback)m_pRightParNextFeedback;       
					if( m_pRightParNextLineFeed != null) m_pRightParNextLineFeed.Stop();
					m_pRightParNextLineFeed.Start(m_pRightParLast2Point);  
					if( m_pRightParNextLineFeed != null)  m_pRightParNextLineFeed.Display = m_pActiveView.ScreenDisplay;
					m_pRightParNextLineFeed.Refresh(m_MapControl.hWnd);
					m_pRightParNextLineFeed.MoveTo(m_pRightParLastPoint);
				}
			}
			
		}

		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawParallelLine.OnMouseMove 实现
			base.OnMouseMove (button, shift, x, y, mapX, mapY);
			
			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			if (m_bkeyCodeS == true)//若S键按下，则生成直角
			{
				m_pPoint = CommonFunction.SquareEnd((IPoint)m_pUndoArray.get_Element(0),(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1),m_pPoint);
			}

			DrawParallelLineMouseMove(m_pPoint); 

			if( m_pLastFeedback != null) m_pLastLineFeed.MoveTo(m_pPoint);//显示回馈封闭主线
			
		}
    
		public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawParallelLine.OnDoubleClick 实现
			base.OnDoubleClick (button, shift, x, y, mapX, mapY);   
					
			IPoint pPoint;            
			pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
		
			EndDrawParallelLine();
		}
  
		private void EndDrawParallelLine()
		{
			IPoint tempPointLeft  = new PointClass();
			IPoint tempPointRight = new PointClass();
	
			IGeometry pGeom = null;
			IGeometry pLeftParGeom = null;
			IGeometry pRightParGeom = null;

			IPolyline pPolyline;
			IPolyline pLeftParPolyLine;
			IPolyline pRightParPolyLine;

			IPointCollection pPointCollection;
			IPointCollection pLeftParPointCollection;
			IPointCollection pRightParPointCollection;          


			if(m_bInUse)
			{
				if( m_pFeedback is INewLineFeedback)
				{
					pPolyline = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pUndoArray);
					
					pPointCollection =(IPointCollection) pPolyline;
                
					if(pPointCollection.PointCount < 2)
					{
						MessageBox.Show("线上必须有两个点!");
					}
					else
					{
						pGeom = (IGeometry)pPointCollection;
						m_pEnvelope = pGeom.Envelope;
						CommonFunction.CreateFeature(m_App.Workbench, pGeom, m_FocusMap, m_CurrentLayer);
					}

					if (locationFlag==1 || locationFlag == 3)//在左边
					{  
						tempPointLeft.X  = m_pLeftParLastPoint.X ;
						tempPointLeft.Y  = m_pLeftParLastPoint.Y ;
						m_pUndoArrayLeft.Add(tempPointLeft);
                      
						//停止显示回馈，存储几何形体
						pLeftParPolyLine = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pUndoArrayLeft);
						
						pLeftParPointCollection =(IPointCollection) pLeftParPolyLine;
						pLeftParGeom = (IGeometry)pLeftParPointCollection;
						m_pEnvelope.Union(pLeftParGeom.Envelope);
                        m_pEnvelope.Expand(10,10,false);
						CommonFunction.CreateFeature(m_App.Workbench, pLeftParGeom, m_FocusMap, m_CurrentLayer);

					}
					if (locationFlag==2 || locationFlag == 3)//在右边
					{  
						tempPointRight.X  = m_pRightParLastPoint.X ;
						tempPointRight.Y  = m_pRightParLastPoint.Y ;
						m_pUndoArrayRight.Add(tempPointRight);
                        
						//停止显示回馈，存储几何形体                
						pRightParPolyLine = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pUndoArrayRight);
						
						pRightParPointCollection =(IPointCollection) pRightParPolyLine;
						pRightParGeom = (IGeometry)pRightParPointCollection;
						m_pEnvelope.Union(pRightParGeom.Envelope);
                        m_pEnvelope.Expand(10, 10, false);
						CommonFunction.CreateFeature(m_App.Workbench, pRightParGeom, m_FocusMap, m_CurrentLayer);
					}
 
				}

			}
           
			Reset();

		}

		private void EndDrawParallelLine_ByESC()
		{
			IPoint tempPoint = new PointClass();
			tempPoint.X = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).X ;
			tempPoint.Y = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Y ;
			if(m_pUndoArray.Count>=3)
			{
				m_pLast2Point =((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-3));
			}
			m_pLastPoint =((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-2));

			double tempParA12;
			double tempParA10;
			tempParA12 = CommonFunction.GetAzimuth_P12(m_pLastPoint,m_pLast2Point);
			tempParA10 = CommonFunction.GetAzimuth_P12(m_pLastPoint,tempPoint);

			double dist;
			dist=dblDeparture/Math.Sin((tempParA10-tempParA12)/2);

			if (locationFlag==1 || locationFlag == 3)//在左边
			{  
				//计算左从线倒数第二个点的坐标
				if(m_pUndoArray.Count>=3)
				{
					m_pLeftParLast2Point.X =  m_pLastPoint.X + dist*Math.Cos(tempParA12+(tempParA10-tempParA12)/2 - Math.PI);
					m_pLeftParLast2Point.Y =  m_pLastPoint.Y + dist*Math.Sin(tempParA12+(tempParA10-tempParA12)/2 - Math.PI);  
				}
				//计算左从线倒数第一个点的坐标
				m_pLeftParLastPoint.X =  tempPoint.X + dblDeparture*Math.Cos(tempParA10 + Math.PI/2);
				m_pLeftParLastPoint.Y =  tempPoint.Y + dblDeparture*Math.Sin(tempParA10 + Math.PI/2); 
				
			}
			if (locationFlag==2 || locationFlag == 3)//在右边
			{  
				//计算右从线倒数第二个点的坐标
				if(m_pUndoArray.Count>=3)
				{
					m_pRightParLast2Point.X =  m_pLastPoint.X + dist*Math.Cos(tempParA12+(tempParA10-tempParA12)/2);
					m_pRightParLast2Point.Y =  m_pLastPoint.Y + dist*Math.Sin(tempParA12+(tempParA10-tempParA12)/2);  
				}
				//计算右从线倒数第一个点的坐标
				m_pRightParLastPoint.X =  tempPoint.X + dblDeparture*Math.Cos(tempParA10 +3* Math.PI/2);
				m_pRightParLastPoint.Y =  tempPoint.Y + dblDeparture*Math.Sin(tempParA10 +3* Math.PI/2); 
			        
			}
				
			EndDrawParallelLine();  
		}

		private void EndDrawParallelLine_ByEnterSpaceButton()
		{
			IPoint tempPointLeft  = new PointClass();
			IPoint tempPointRight = new PointClass();
	
			IGeometry pGeom = null;
			IGeometry pLeftParGeom = null;
			IGeometry pRightParGeom = null;

			IPolyline pPolyline;
			IPolyline pLeftParPolyLine;
			IPolyline pRightParPolyLine;

			IPointCollection pPointCollection;
			IPointCollection pLeftParPointCollection;
			IPointCollection pRightParPointCollection;          


			if(m_bInUse)
			{
				if( m_pFeedback is INewLineFeedback)
				{
					pPolyline = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pUndoArray);
					
					pPointCollection =(IPointCollection) pPolyline;
                
					if(pPointCollection.PointCount < 2)
					{
						MessageBox.Show("线上必须有两个点!");
					}
					else
					{
						pGeom = (IGeometry)pPointCollection;
						CommonFunction.CreateFeature(m_App.Workbench, pGeom, m_FocusMap, m_CurrentLayer);
					}

					if (locationFlag==1 || locationFlag == 3)//在左边
					{                        
						m_pUndoArrayLeft.Add(m_pLeftParLast2Point);
						pLeftParPolyLine = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pUndoArrayLeft);
						
						pLeftParPointCollection =(IPointCollection) pLeftParPolyLine;
						pLeftParGeom = (IGeometry)pLeftParPointCollection;
						CommonFunction.CreateFeature(m_App.Workbench, pLeftParGeom, m_FocusMap, m_CurrentLayer);

					}
					if (locationFlag==2 || locationFlag == 3)//在右边
					{                        
						m_pUndoArrayRight.Add(m_pRightParLast2Point);               
						pRightParPolyLine = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pUndoArrayRight);
						
						pRightParPointCollection =(IPointCollection) pRightParPolyLine;
						pRightParGeom = (IGeometry)pRightParPointCollection;
						CommonFunction.CreateFeature(m_App.Workbench, pRightParGeom, m_FocusMap, m_CurrentLayer);
					}
 
				}

			}
           
			Reset();
		}
	
		private void EndDrawParallelLine_Colse()
		{
			IPoint tempPoint = new PointClass();
			tempPoint.X = ((IPoint)m_pUndoArray.get_Element(0)).X ;
			tempPoint.Y = ((IPoint)m_pUndoArray.get_Element(0)).Y ;
			m_pLast2Point =((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-2));
			m_pLastPoint =((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1));

			m_pUndoArray.Add(tempPoint);

			double tempParA12;
			double tempParA10;
			tempParA12 = CommonFunction.GetAzimuth_P12(m_pLastPoint,m_pLast2Point);
			tempParA10 = CommonFunction.GetAzimuth_P12(m_pLastPoint,tempPoint);

			double dist;
			dist=dblDeparture/Math.Sin((tempParA10-tempParA12)/2);

			if (locationFlag==1 || locationFlag == 3)//在左边
			{  
				//计算左从线倒数第二个点的坐标
				m_pLeftParLast2Point.X =  m_pLastPoint.X + dist*Math.Cos(tempParA12+(tempParA10-tempParA12)/2 - Math.PI);
				m_pLeftParLast2Point.Y =  m_pLastPoint.Y + dist*Math.Sin(tempParA12+(tempParA10-tempParA12)/2 - Math.PI);  
			
				//计算左从线倒数第一个点的坐标
				m_pLeftParLastPoint.X =  tempPoint.X + dblDeparture*Math.Cos(tempParA10 + Math.PI/2);
				m_pLeftParLastPoint.Y =  tempPoint.Y + dblDeparture*Math.Sin(tempParA10 + Math.PI/2); 
				
				m_pUndoArrayLeft.Add(m_pLeftParLast2Point);
			}
			if (locationFlag==2 || locationFlag == 3)//在右边
			{  
				//计算右从线倒数第二个点的坐标
				m_pRightParLast2Point.X =  m_pLastPoint.X + dist*Math.Cos(tempParA12+(tempParA10-tempParA12)/2);
				m_pRightParLast2Point.Y =  m_pLastPoint.Y + dist*Math.Sin(tempParA12+(tempParA10-tempParA12)/2);  
					
				//计算右从线倒数第一个点的坐标
				m_pRightParLastPoint.X =  tempPoint.X + dblDeparture*Math.Cos(tempParA10 +3* Math.PI/2);
				m_pRightParLastPoint.Y =  tempPoint.Y + dblDeparture*Math.Sin(tempParA10 +3* Math.PI/2); 
				m_pUndoArrayRight.Add(m_pRightParLast2Point);
			}
				
			EndDrawParallelLine();    
		}
		private void Undo()//回退
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
			if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);

			if(m_pUndoArray.Count<=2)
			{
				Reset();
				return;
			}

			IEnumElement  pEnumElement;
			IEnvelope pEnvelope = new EnvelopeClass();
			IPoint pPoint = new PointClass();
			if(m_pUndoArray.Count != 0)//主
			{
				pPoint = (IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1);
				pEnvelope =CommonFunction.NewRect(pPoint,m_dblTolerance);			
				pEnumElement = m_MapControl.ActiveView.GraphicsContainer.LocateElementsByEnvelope(pEnvelope);	
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

				m_pUndoArray.Remove(m_pUndoArray.Count-1); //删除数组中最后一个点 

			}

			if (locationFlag == 1 || locationFlag == 3)//左边
			{
				if(m_pUndoArrayRight !=null && m_pUndoArrayLeft.Count>0)
				{
					pPoint = (IPoint)m_pUndoArrayLeft.get_Element(m_pUndoArrayLeft.Count-1);
					pEnvelope =CommonFunction.NewRect(pPoint,m_dblTolerance);			
					pEnumElement = m_MapControl.ActiveView.GraphicsContainer.LocateElementsByEnvelope(pEnvelope);
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
				}
                
				m_pUndoArrayLeft.Remove(m_pUndoArrayLeft.Count-1);

				//m_pLeftParNextLineFeed.Stop();

			}

			if (locationFlag == 2 || locationFlag == 3)//右边
			{
				if(m_pUndoArrayRight !=null && m_pUndoArrayRight.Count>0)
				{
					pPoint = (IPoint)m_pUndoArrayRight.get_Element(m_pUndoArrayRight.Count-1);
					pEnvelope =CommonFunction.NewRect(pPoint,m_dblTolerance);			
					pEnumElement = m_MapControl.ActiveView.GraphicsContainer.LocateElementsByEnvelope(pEnvelope);
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

				}

				m_pUndoArrayRight.Remove(m_pUndoArrayRight.Count-1);

				//m_pRightParNextLineFeed.Stop();
				
			}
    
			//屏幕刷新
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics,null,m_pEnvelope);
			m_pActiveView.ScreenDisplay.UpdateWindow();

     
			if (m_pUndoArray.Count>=2)
			{                
				DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray,ref m_pUndoArrayLeft,ref m_pUndoArrayRight);
				
				m_pLastPoint=(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1);
				
				if(m_pUndoArray.Count ==2)
				{
					m_pLast2Point=(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-2);

				}
				else if(m_pUndoArray.Count >=3)
				{
					m_pLast2Point=(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-2);
					m_pLast3Point=(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-3);
				}
				//主
				m_pFeedback = new NewLineFeedbackClass(); 
				m_pLineFeed =(NewLineFeedback)m_pFeedback;
				m_pLineFeed.Display = m_pActiveView.ScreenDisplay;
				if (m_pLineFeed !=null) m_pLineFeed.Stop();
				m_pLineFeed.Start(m_pLastPoint);
				m_pLineFeed.MoveTo(m_pPoint);
				if (locationFlag == 1 || locationFlag == 3)//左边
				{
					m_pLeftParFeedback = new NewLineFeedbackClass();
					m_pLeftParLineFeed =(NewLineFeedback)m_pLeftParFeedback;
					m_pLeftParLineFeed.Display = m_pActiveView.ScreenDisplay;
					if (m_pLeftParLineFeed !=null) m_pLeftParLineFeed.Stop();
					m_pLeftParLineFeed.Start((IPoint)m_pUndoArrayLeft.get_Element(m_pUndoArrayLeft.Count-1));
				}
				if (locationFlag == 2 || locationFlag == 3)//右
				{
					m_pRightParFeedback = new NewLineFeedbackClass();
					m_pRightParLineFeed =(NewLineFeedback)m_pRightParFeedback;
					m_pRightParLineFeed.Display = m_pActiveView.ScreenDisplay;
					if (m_pRightParLineFeed !=null) m_pRightParLineFeed.Stop();
					m_pRightParLineFeed.Start((IPoint)m_pUndoArrayRight.get_Element(m_pUndoArrayRight.Count-1));
				}
			}
			else 
			{   
				Reset(); //复位
			}

			DrawParallelLineMouseMove(m_pAnchorPoint);
           
		}

		
		#region//将SegmentCollection显示到屏幕
		public void DisplaypSegmentColToScreen( IMapControl2 MapControl,ref IArray PointArray,ref IArray PointArrayLeft,ref IArray PointArrayRight)
		{            	
			//主线
			ISegmentCollection pPolylineCol;
			pPolylineCol = new PolylineClass();
			ISegmentCollection  pSegmentCollection = CommonFunction.MadeSegmentCollection(ref PointArray);
			pPolylineCol.AddSegmentCollection(pSegmentCollection);
	
			m_pActiveView.ScreenDisplay.ActiveCache = (short)esriScreenCache.esriNoScreenCache; 
			ISimpleLineSymbol pLineSym = new SimpleLineSymbolClass();
			pLineSym.Color = CommonFunction.GetRgbColor(0,0,0);
             
			m_pActiveView.ScreenDisplay.StartDrawing(m_pActiveView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
			m_pActiveView.ScreenDisplay.SetSymbol((ISymbol)pLineSym);      
			m_pActiveView.ScreenDisplay.DrawPolyline((IPolyline)pPolylineCol);     //主线
			if (locationFlag == 1 || locationFlag == 3)//左从线
			{
				ISegmentCollection pPolylineColLeft;
				pPolylineColLeft = new PolylineClass();
				ISegmentCollection  pSegmentCollectionLeft = CommonFunction.MadeSegmentCollection(ref PointArrayLeft);
				pPolylineColLeft.AddSegmentCollection(pSegmentCollectionLeft);	
				m_pActiveView.ScreenDisplay.DrawPolyline((IPolyline)pPolylineColLeft); //左从线			
			}
			if (locationFlag == 2 || locationFlag == 3)//右从线
			{
				ISegmentCollection pPolylineColRight;
				pPolylineColRight = new PolylineClass();
				ISegmentCollection  pSegmentCollectionRight = CommonFunction.MadeSegmentCollection(ref PointArrayRight);
				pPolylineColRight.AddSegmentCollection(pSegmentCollectionRight);
				m_pActiveView.ScreenDisplay.DrawPolyline((IPolyline)pPolylineColRight);//右从线
			}
			
			m_pActiveView.ScreenDisplay.FinishDrawing();
            
		}
		#endregion
       		
		public override void OnBeforeScreenDraw(int hdc)
		{
			base.OnBeforeScreenDraw (hdc);
           
			if(m_pUndoArray.Count !=0)
			{	
				if (m_pLineFeed != null)  m_pLineFeed.MoveTo(m_pLastPoint);
				if (m_pLeftParLineFeed != null)  m_pLeftParNextLineFeed.Stop();
				if (m_pRightParLineFeed!= null)  m_pRightParNextLineFeed.Stop();

			}      
		}

		public override void OnAfterScreenDraw(int hdc)
		{
			base.OnAfterScreenDraw (hdc);

			DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray,ref m_pUndoArrayLeft,ref m_pUndoArrayRight);
		}
		
		private void Reset()
		{
			m_bInUse        = false;
			m_bParStart     = false;
			m_bFixDirection = false;
			m_bFixLength    = false;
            m_bkeyCodeS     = false;
			m_bInputWindowCancel = true;

			m_pUndoArray.RemoveAll();    //清空回退数组
			m_pUndoArrayLeft.RemoveAll();
			m_pUndoArrayRight.RemoveAll();

			m_pPoint      = null;
			m_pFirstPoint = null;//主线第一个点
			m_pLastPoint  = null;//倒数第一个点
			m_pLast2Point = null;//主线倒数第二个点
			m_pLast3Point = null;//主线倒数第二个点
			m_pAnchorPoint= null;

			if (m_pLineFeed != null)             m_pLineFeed.Stop();
			if (m_pLeftParLineFeed != null)	     m_pLeftParLineFeed.Stop();//右从线第已经形成的部分
			if (m_pLeftParNextLineFeed != null)	 m_pLeftParNextLineFeed.Stop();//右从线第将要形成的部分
			if (m_pRightParLineFeed != null)     m_pRightParLineFeed.Stop();//右从线第已经形成的部分
			if (m_pRightParNextLineFeed != null) m_pRightParNextLineFeed.Stop();//右从线第将要形成的部分	
			m_pLastFeedback = null;//封闭主线显示回馈

			m_pActiveView.FocusMap.ClearSelection();  
			m_pActiveView.GraphicsContainer.DeleteAllElements();//删除创建的地图元素
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新

			m_pEnvelope = null;
		
			m_pStatusBarService.SetStateMessage("就绪");

		}

		public override void OnKeyDown(int keyCode, int shift)
        {
            // TODO:  添加 DrawPoint.OnKeyDown 实现
            base.OnKeyDown (keyCode, shift);			
			
			if (keyCode == 85 && m_bInUse)//按U键,回退
			{
				Undo();

				return;
			}

			if (keyCode == 78 && m_bInUse)//按N键,输入折角方向
			{               
				frmLeftCorner fromFixLeftCorner = new frmLeftCorner();	 
				fromFixLeftCorner.ShowDialog(); 
                
				return;

			}

			if ((keyCode == 69 || keyCode == 13 || keyCode == 32) && m_bParStart && m_pUndoArray.Count >=2)//按E键结束绘制
			{				              
				EndDrawParallelLine_ByESC();

				return;
			}
			
			if (keyCode == 79 && m_bInUse)//按(O)orientation键输入方向
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
				IPoint tempPoint = new PointClass();
				tempPoint.X = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).X ;
				tempPoint.Y = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Y ;
			   
                frmLengthAzim.m_pPoint = tempPoint;
				frmLengthAzim fromDistAzim = new frmLengthAzim();  
                fromDistAzim.ShowDialog();  
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{    
					DrawParallelLineMouseDown(m_pAnchorPoint);
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
					DrawParallelLineMouseDown(m_pAnchorPoint);
				}
				
				return;

			}

			if (keyCode == 82 && m_bInUse)//按R键输入相对坐标
			{                      
				IPoint tempPoint = new PointClass();
				tempPoint.X = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).X ;
				tempPoint.Y = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Y ;

				frmRelaXYZ.m_pPoint = tempPoint;
				frmRelaXYZ formRelaXYZ = new frmRelaXYZ();
				formRelaXYZ.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{      
					DrawParallelLineMouseDown(m_pAnchorPoint);
				}

				return;
               
			}         

			if (keyCode == 80 && m_bInUse)//按P键,平行尺
			{							
				m_pSegment = null;
				m_bKeyCodeP = true;
							
				return;
			}

			if (keyCode == 83 && m_pUndoArray.Count >=2)//按S键,生成直角
			{				
				m_bkeyCodeS = true;

				//封闭主线显示回馈
				m_pLastFeedback = new NewLineFeedbackClass();					
				m_pLastLineFeed = (INewLineFeedback)m_pLastFeedback;				
				m_pLastLineFeed.Start((IPoint)m_pUndoArray.get_Element(0));  
				if( m_pLastFeedback != null)  m_pLastFeedback.Display = m_pActiveView.ScreenDisplay;
	
				return;
			}

			if (keyCode == 67 && m_pUndoArray.Count >=3)//按C键,封闭结束绘制
			{			       
				EndDrawParallelLine_Colse();
				
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
			IPoint tempPoint = new PointClass();
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
					tempPoint.X = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).X ;
					tempPoint.Y = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Y ;
			   
					frmLengthAzim.m_pPoint = tempPoint;
					frmLengthAzim fromDistAzim = new frmLengthAzim();  
					fromDistAzim.ShowDialog();  
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{    
						DrawParallelLineMouseDown(m_pAnchorPoint);
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
						DrawParallelLineMouseDown(m_pAnchorPoint);
					}

					break;

				case "相对坐标(&R)...":
					tempPoint.X = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).X ;
					tempPoint.Y = ((IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1)).Y ;

					frmRelaXYZ.m_pPoint = tempPoint;
					frmRelaXYZ formRelaXYZ = new frmRelaXYZ();
					formRelaXYZ.ShowDialog();
					if(m_bInputWindowCancel == false)//若用户没用取消输入
					{      
						DrawParallelLineMouseDown(m_pAnchorPoint);
					}

					break;

				case "平行(&P)...":
					m_pSegment = null;
					m_bKeyCodeP = true;	
					CommonFunction.ParallelRule(ref m_bKeyCodeP,m_pActiveView,m_dblTolerance,ref m_pSegment, m_pLastPoint,m_BeginConstructParallelPoint,ref m_pAnchorPoint);

					break;

				case "直角(&S)...":
					m_bkeyCodeS = true;

					//封闭主线显示回馈
					m_pLastFeedback = new NewLineFeedbackClass();					
					m_pLastLineFeed = (INewLineFeedback)m_pLastFeedback;				
					m_pLastLineFeed.Start((IPoint)m_pUndoArray.get_Element(0));  
					if( m_pLastFeedback != null)  m_pLastFeedback.Display = m_pActiveView.ScreenDisplay;
					  
					break;

				case "封闭完成(&C)":                    
					EndDrawParallelLine_Colse();				  

					break;

				case "完成(&E)":
					EndDrawParallelLine_ByESC();

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
			// TODO:  添加 DrawParallelLine.Deactivate 实现
			return base.Deactivate ();

		}

		public override void Stop()
		{
			//this.Reset();
			base.Stop();
		}
	}
}
