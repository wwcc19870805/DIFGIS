using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

using WSGRI.DigitalFactory.Base;
using WSGRI.DigitalFactory.Gui.Views;
using WSGRI.DigitalFactory.Commands; 
using WSGRI.DigitalFactory.DFEditorLib;
using WSGRI.DigitalFactory.DFFunction;
/*----------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：SelectFeature.cs
			// 文件功能描述：选择要素
			//
			// 
			// 创建标识：YuanHY 20060109
            // 操作说明：点击或拉框选择
            //　　　　　    
----------------------------------------------------------------*/
namespace WSGRI.DigitalFactory.DFEditorTool
{
	/// <summary>
	/// ModifyPropertyCopy 的摘要说明。
	/// </summary>
	public class SelectFeature:AbstractMapCommand
	{
		private IDFApplication m_App;
		private IMap           m_FocusMap;
		private ILayer         m_CurrentLayer;
		private IActiveView    m_pActiveView;
		private IMapView       m_MapView = null;

		private IPoint m_pPoint;
		private bool   m_bIsUse;
		private INewEnvelopeFeedback  m_pFeedbackEnve; //矩形框显示回馈

		public override string Caption
		{
			get
			{
				return "SelectFeature";
			}
			set
			{
				
			}
		}

		public override void Execute()
		{
			if (!(this.Hook is IDFApplication))
			{
				return;
			}
			else
			{
				m_App = (IDFApplication)this.Hook;
			}
           
			m_MapView = m_App.Workbench.GetView(typeof(MapView)) as IMapView;
			m_pActiveView = m_App.CurrentMapControl.ActiveView;
			m_FocusMap = m_pActiveView.FocusMap;
			if (m_MapView == null)
			{
				return;
			}
			else
			{
				//绑定事件
				m_MapView.CurrentTool = this;
			}
    
			CurrentTool.m_CurrentToolName  = CurrentTool.CurrentToolName.selectFeature;
			
			CommonFunction.MapRefresh(m_pActiveView);

		}
          
		public override void UnExecute()
		{
			// TODO:  添加 DrawLine.UnExecute 实现

		}

		public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawRectBorder2P.OnMouseDown 实现
			base.OnMouseDown (button, shift, x, y, mapX, mapY);

			if (CurrentTool.m_CurrentToolName == CurrentTool.CurrentToolName.selectFeature) 
			{
				m_CurrentLayer = m_App.CurrentEditLayer;
			}
			else
			{
				return;
			}			

			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			m_bIsUse = true;

		}

		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 ModifyPropertyMatch.OnMouseMove 实现
			base.OnMouseMove (button, shift, x, y, mapX, mapY);

			if(!m_bIsUse) return;
           
			if (m_pFeedbackEnve == null ) 
			{
				m_pFeedbackEnve = new NewEnvelopeFeedbackClass();
				m_pFeedbackEnve.Display = m_pActiveView.ScreenDisplay;
				m_pFeedbackEnve.Start(m_pPoint);
			}

			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
			m_pFeedbackEnve.MoveTo(m_pPoint);
		}
	
		public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 ModifyPropertyMatch.OnMouseUp 实现
			base.OnMouseUp (button, shift, x, y, mapX, mapY);

			if (m_bIsUse)
			{
				IGeometry pEnv;
				m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection , null, null);
				if (m_pFeedbackEnve !=null)
				{
					pEnv = m_pFeedbackEnve.Stop();
					m_FocusMap.SelectByShape(pEnv, null, false);
				}
				else
				{
					IEnvelope pRect ;
					double dblConst ;
					dblConst =CommonFunction.ConvertPixelsToMapUnits(m_pActiveView,8);//    '8个象素大小
					pRect = new EnvelopeClass();
					pRect.XMin = m_pPoint.X - dblConst; // 调整边界的宽度为16个象素大小
					pRect.YMin = m_pPoint.Y - dblConst;
					pRect.XMax = m_pPoint.X + dblConst;
					pRect.YMax = m_pPoint.Y + dblConst;
					m_FocusMap.SelectByShape(pRect,null, false);

				}

				m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
				m_pFeedbackEnve = null;
				m_bIsUse = false;
			}

		}
	}
}

