/*----------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：CalculateCorner.cs
			// 文件功能描述：计算夹角
			//
			// 
			// 创建标识：YuanHY 20060614
            // 操作步骤：1、点击命令按纽；
			//           2、；
			//			 3、双击、或点击右键\回车\空格键，弹出对话框;
			//           4、点击确定按钮删除生成的地图元素。　　　　　　
			// 操作说明：ESC键 取消所有操作
　
		    // 修改标识：Modify by YuanHY20081112
            // 修改描述：增加了ESC键的操作　 　　    
----------------------------------------------------------------*/
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
	/// CalculateCorner 的摘要说明。
	/// </summary>
	public class CalculateCorner:AbstractMapCommand
	{
		private IDFApplication m_App;        
		private IMapControl2   m_MapControl;
		private IMap           m_FocusMap;
		private IActiveView    m_pActiveView;
		private IMapView       m_MapView = null;

		private IDisplayFeedback m_pFeedback;
		private INewLineFeedback m_pLineFeed;

		private bool m_bInUse;

		public  static IPoint m_pPoint;
		public  static IPoint m_pAnchorPoint;
		private        IPoint m_pLastPoint;
		private 	   IPoint m_pPoint1 = new PointClass();
		private        IPoint m_pPoint2 = new PointClass();

		private IArray   m_pRecordPointArray = new ArrayClass();

		const string CRLF = "\r\n";

		private IStatusBarService m_pStatusBarService;

		//private bool	isEnabled   = true;
		private string	strCaption  = "计算夹角";
		private string	strCategory = "工具";


		public CalculateCorner()
		{
			//获得状态栏的服务
			//m_pStatusBarService = (IStatusBarService)ServiceManager.Services.GetService(typeof(WSGRI.DigitalFactory.Services.UltraStatusBarService));

		}
   
		#region 类的属性
//		public override bool IsEnabled 
//		{
//			get 
//			{
//				return isEnabled;
//			}
//			set 
//			{
//				isEnabled = value;
//			}
//		}

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
			else
			
            m_MapView.CurrentTool = this;

			m_MapControl   = m_App.CurrentMapControl;            
			m_FocusMap     = m_MapControl.ActiveView.FocusMap;
			m_pActiveView  = (IActiveView)this.m_FocusMap;
			m_pStatusBarService = m_App.StatusBarService;//获得状态服务

			CurrentTool.m_CurrentToolName  = CurrentTool.CurrentToolName.CalculateCorner;

			CommonFunction.MapRefresh(m_pActiveView);
                  
		}
    
		public override void UnExecute()
		{
			// TODO:  添加 CalculateCorner.UnExecute 实现
			m_pStatusBarService.SetStateMessage("就绪");

		}  

		public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 CalculateCorner.OnMouseDown 实现
			base.OnMouseDown (button, shift, x, y, mapX, mapY);

			CalculateCornerMouseDown(m_pAnchorPoint);            

		}
      
		private void CalculateCornerMouseDown(IPoint pPoint)
		{
			IGeometry pGeom = null;

			if(!m_bInUse) //如果命令没有使用
			{
 　　　　       m_pPoint1 = pPoint;
				m_pLastPoint = pPoint;
				m_pRecordPointArray.Add(m_pPoint1);
				m_bInUse  = true;

				m_pFeedback = new NewLineFeedbackClass(); 
				m_pLineFeed = (INewLineFeedback)m_pFeedback;
				m_pLineFeed.Start(pPoint);
				if( m_pFeedback != null)  m_pFeedback.Display = m_pActiveView.ScreenDisplay;

				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pPoint1);
			}
			else //如果命令正在使用
			{
				m_pPoint2 = pPoint;
				m_pRecordPointArray.Add(pPoint);
				m_bInUse = true;

				m_pLineFeed.AddPoint(pPoint);
			
				CommonFunction.DrawPointSMSSquareSymbol(m_MapControl,m_pPoint2);
				if(m_pRecordPointArray.Count>2)
				{
					IPolyline pPolyline;	
					pPolyline =(IPolyline)CommonFunction.MadeSegmentCollection(ref m_pRecordPointArray);
					pGeom = (IGeometry)pPolyline;  
					CommonFunction.AddElement(m_MapControl,pGeom);  

					double dblZimuth = CommonFunction.GetAngleZuo_P123(m_pPoint1,(IPoint)m_pRecordPointArray.get_Element(1), m_pPoint2);
					dblZimuth = CommonFunction.RadToDeg(dblZimuth);

					System.Windows.Forms.DialogResult result;
					string  strResult = "三点夹角:" + dblZimuth.ToString(".#####") + "(°′″)";
					strResult = strResult + CRLF;
					strResult = strResult + "第一点坐标 X=" + m_pPoint1.X.ToString(".###")  + "Y=" + m_pPoint1.Y.ToString(".###"); 
					strResult = strResult + CRLF;
					strResult = strResult + "第二点坐标 X=" + ((IPoint)m_pRecordPointArray.get_Element(1)).X.ToString(".###")  + "Y=" + ((IPoint)m_pRecordPointArray.get_Element(1)).Y.ToString(".###"); 
					strResult = strResult + CRLF;
					strResult = strResult + "第三点坐标 X=" + m_pPoint2.X.ToString(".###")  + "Y=" + m_pPoint2.Y.ToString(".###"); 
            
					result = MessageBox.Show(strResult, "三点夹角计算",	MessageBoxButtons.OK,MessageBoxIcon.Information);

					if(result == DialogResult.OK)
					{
						Reset();//复位;
					}	
				}
		              
			}
		}
		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 CalculateCorner.OnMouseMove 实现
			base.OnMouseMove (button, shift, x, y, mapX, mapY);

            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
		
			m_pStatusBarService.SetStateMessage("提示：依次点击三点");

			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
  
			m_pAnchorPoint = m_pPoint;           
			//+++++++++++++开始捕捉+++++++++++++++++++++			
			bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,(IGeometry)m_pLastPoint,m_pAnchorPoint);
			
			if(!m_bInUse) return;
		
			m_pFeedback.MoveTo(m_pAnchorPoint);

		}

		public override void OnBeforeScreenDraw(int hdc)
		{
			// TODO:  添加 CalculateCorner.OnBeforeScreenDraw 实现
			base.OnBeforeScreenDraw (hdc);
           
			if (m_pFeedback != null)  
			{
				m_pFeedback.MoveTo(m_pPoint1);              
			}    
		}

		public override void OnKeyDown(int keyCode, int shift)
		{
			// TODO:  添加 CalculateCorner.OnKeyDown 实现
			base.OnKeyDown (keyCode, shift);
			
		
			if ((keyCode == 13 || keyCode == 32) && m_bInUse)//按ENTER 键、SPACEBAR 键
			{     
				CalculateCornerMouseDown(m_pAnchorPoint);

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

		private void Reset()
		{
			m_bInUse  = false;
			m_pFeedback  = null;
			m_pLastPoint = null;
			m_pPoint1 = null;
			m_pPoint2 = null;
			m_pRecordPointArray.RemoveAll();

			m_pActiveView.GraphicsContainer.DeleteAllElements();
            //m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_MapControl.ActiveView.Extent);//视图刷新
            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_MapControl.ActiveView.Extent);//视图刷新		
		
			m_pStatusBarService.SetStateMessage("就绪");

		}

		public override void Stop()
		{
			//this.Reset();
			base.Stop();
		}

	}
}
