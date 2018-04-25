/*----------------------------------------------------------------
            // Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：cmdSelByPolygon.cs
            // 文件功能描述：多边形选择
            //               
            // 
            // 创建标识：LuoXuan
            //
            // 修改描述：

----------------------------------------------------------------*/
using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;

namespace DF2DEdit.BaseEdit
{
	/// <summary>
	/// SelByPolygon 的摘要说明。
	/// </summary>
    public class cmdSelByPolygon : AbstractMap2DCommand
	{
        private DF2DApplication m_App;
        private IMapControl2 m_MapControl;
        private IActiveView m_ActiveView;
        private IScreenDisplay m_Display;
        private IFeatureLayer m_pCurEditLayer;

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

            this.m_ActiveView = m_MapControl.ActiveView;
            this.m_Display = m_ActiveView.ScreenDisplay;
            this.m_pCurEditLayer = Class.Common.CurEditLayer as IFeatureLayer;
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            if (button==1)
            {
                int dis;
                IPolygon pPoly;
                IRubberBand pRubberPoly = new RubberPolygonClass();
                pPoly = (IPolygon)pRubberPoly.TrackNew(m_Display,null);

                IFeatureSelection pFeaSel = m_pCurEditLayer as IFeatureSelection;
                dis = Class.SelectionEnv.System_Selection_Environment(m_ActiveView).SearchTolerance;

                IGeometry pGeoSel = Class.Common.DoBuffer(pPoly, dis);
                ISpatialFilter pSpaFilter = new SpatialFilter();
                pSpaFilter.Geometry = pGeoSel;
                pSpaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                if (shift == 1)
                {
                    pFeaSel.CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;
                    //Class.SelectionEnv.System_Selection_Environment(m_ActiveView).CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;
                }
                else
                {
                    pFeaSel.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
                    this.m_ActiveView.FocusMap.ClearSelection();
                }
                pFeaSel.SelectFeatures(pSpaFilter, pFeaSel.CombinationMethod, false);

                //if (Class.SelectionEnv.System_Selection_Environment(m_ActiveView).CombinationMethod == 0)//new selection
                //{
                //    this.m_ActiveView.FocusMap.ClearSelection();
                //}
                //m_ActiveView.FocusMap.SelectByShape(Class.Common.DoBuffer(pPoly, dis), Class.SelectionEnv.System_Selection_Environment(m_ActiveView), false);
                m_App.Workbench.UpdateMenu();

                if (m_ActiveView.FocusMap.SelectionCount > 0)
                {
                    m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                }
                else
                {

                    m_ActiveView.Refresh();
                }
            }
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
        }

	}
}
