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
    public class cmdSelByCircle : AbstractMap2DCommand
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
            if (button == 1)
            {
                int dis;
                IRubberBand pRubberBand;
                pRubberBand = new RubberCircleClass();
                IGeometry pCircle;
                ISegmentCollection pSegColl = new PolygonClass();

                pCircle = pRubberBand.TrackNew(m_Display, null);
                object a = Type.Missing;
                pSegColl.AddSegment((ISegment)pCircle, ref a, ref a);
                dis = Class.SelectionEnv.System_Selection_Environment(m_ActiveView).SearchTolerance;
                IFeatureSelection pFeaSel = m_pCurEditLayer as IFeatureSelection;

                IGeometry pGeoSel = Class.Common.DoBuffer((IPolygon)pSegColl, dis);
                ISpatialFilter pSpaFilter = new SpatialFilter();
                pSpaFilter.Geometry = pGeoSel;
                pSpaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                if (shift == 1)
                {
                    pFeaSel.CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;

                    //Class.SelectionEnv.System_Selection_Environment(m_ActiveView).CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;
                }

                if (Class.SelectionEnv.System_Selection_Environment(m_ActiveView).CombinationMethod == 0)//new selection
                {
                    this.m_ActiveView.FocusMap.ClearSelection();
                }
                else
                {
                    pFeaSel.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
                    this.m_ActiveView.FocusMap.ClearSelection();
                }
                pFeaSel.SelectFeatures(pSpaFilter, pFeaSel.CombinationMethod, false);

                //m_ActiveView.FocusMap.SelectByShape(Class.Common.DoBuffer(pLine, dis), Class.SelectionEnv.System_Selection_Environment(m_ActiveView), false);

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

        public override void OnKeyDown(int keyCode, int shift)
        {
            //if (keyCode==(int)Keys.Delete)
            //{
            //    PublicFunction.DeleteFeatureSelection(m_MapControl);
            //}
            //base.OnKeyDown (keyCode, shift);


            //if (keyCode == 27)//ESC 键，取消所有操作
            //{
            //    this.Stop();

            //    WSGRI.DigitalFactory.Commands.ICommand command = DFApplication.Application.GetCommand("WSGRI.DigitalFactory.DF2DControl.cmdPan");
            //    if (command != null) command.Execute();
            //    return;
            //}
        }

    }
}
