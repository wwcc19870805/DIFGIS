/*---------------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawPoint.cs
			// 文件功能描述：绘制点
			//
			// 
			// 创建标识：LuoXuan
            // 操作说明：A键输入绝对坐标
     
			// 修改记录：
-----------------------------------------------------------------------*/
using System;
using System.Windows.Forms ;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DEdit.Form;
using DevExpress.XtraEditors;

namespace DF2DEdit.CmdDraw
{
	/// <summary>
	/// DrawPoint 的摘要说明。
	/// </summary>

    public class DrawPoint : AbstractMap2DCommand
	{
        private DF2DApplication m_App;
        private IMapControl2 m_MapControl;
        private IActiveView m_ActiveView;

        private IMap           m_FocusMap;
		private IActiveView    m_pActiveView;
        private ILayer         m_CurrentLayer;

        public  static IPoint  m_pPoint;
		private IPoint         m_pLastPoint ;
		public  static bool    m_bInputWindowCancel = true;//标识输入窗体是否被取消

		private IEnvelope m_pEnvelope = new EnvelopeClass();

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

            Class.Common.MapRefresh(m_pActiveView);
            m_App.Workbench.SetStatusInfo("快捷键提示：A:输入绝对XY坐标");
        }
         
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawPoint.OnMouseDown 实现
            base.OnMouseDown (button, shift, x, y, mapX, mapY);
            //m_App.Workbench.SetStatusInfo("快捷键提示：A:输入绝对XY坐标");
    
		　	m_CurrentLayer = Class.Common.CurEditLayer;
            m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			//检查点是否超出地图范围
			if(Class.Common.PointIsOutMap(m_CurrentLayer,m_pPoint) == true)
			{
				EndDrawPoint(m_pPoint); 
			}
			else
			{
                XtraMessageBox.Show("超出地图范围");
			}	
			

        }

		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawPoint.OnMouseMove 实现
            //base.OnMouseMove (button, shift, x, y, mapX, mapY);

			
            //m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
         
            ////+++++++++++++开始捕捉+++++++++++++++++++++			
            //bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,null,m_pPoint);
			
		}

        public  void EndDrawPoint(IPoint pPoint )
        {
            ILayer pLayer;
            IActiveView pActiveView;

            pActiveView =(IActiveView)m_FocusMap;
            pLayer = m_CurrentLayer;
    
            if (pLayer == null)  return;

            if(!(pLayer is IFeatureLayer)) return;
    
            IFeatureLayer pFeatureLayer =(IFeatureLayer) pLayer;
    
            if(pFeatureLayer.FeatureClass == null) return;

            if(pFeatureLayer.FeatureClass.FeatureType ==  esriFeatureType.esriFTAnnotation) return;
           
            if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint) return;

            Class.Common.CreateFeature(pPoint, m_FocusMap, m_CurrentLayer);

			m_pLastPoint = pPoint;

			m_pEnvelope = pPoint.Envelope;
			if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(2,2,false);

			Reset();
            m_App.Workbench.UpdateMenu();
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            Map2DCommandManager.Pop();

            Reset();
        }

		private void Reset()
		{
			m_pLastPoint = null;			
			m_pActiveView.FocusMap.ClearSelection();  
			m_pActiveView.GraphicsContainer.DeleteAllElements();//删除创建的地图元素

			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新
            m_App.Workbench.SetStatusInfo("就绪");
		}

        public override void OnKeyDown(int keyCode, int shift)
        {
            // TODO:  添加 DrawPoint.OnKeyDown 实现
            base.OnKeyDown(keyCode, shift);

            if (keyCode == 65)//按A键输入绝对坐标
            {
                frmAbsXYZ.m_pPoint = m_pPoint;
                frmAbsXYZ formXYZ = new frmAbsXYZ();
                formXYZ.ShowDialog();
                if (m_bInputWindowCancel == false)//若用户没用取消输入
                {
                    EndDrawPoint(m_pPoint);
                }

                return;
            }

            if (keyCode == 27)//ESC 键，取消所有操作
            {
                Reset();
                DF2DApplication.Application.Workbench.BarPerformClick("Pan");
                return;
            }

        }
  	}

}
