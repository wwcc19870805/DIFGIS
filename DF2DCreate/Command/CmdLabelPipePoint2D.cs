using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using DFWinForms.Service;
using ESRI.ArcGIS.Geometry;
using System.Drawing;
using DFCommon.Class;
using DF2DData.Class;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using DF2DData.Class;
using System.Windows.Forms;
using DFWinForms.Class;
using DevExpress.XtraEditors;
using DF2DCreate.Class;
using DF2DPipe.Class;
using DFDataConfig.Logic;

namespace DF2DCreate.Command
{
    class CmdLabelPipePoint2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private IScreenDisplay m_Display;
        private DF2DApplication app;
        private IGraphicsContainer pGraphicsContainer;
        public override void Run(object sender, EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null || app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(false);
            m_ActiveView = app.Current2DMapControl.ActiveView;
            m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            pGraphicsContainer = app.Current2DMapControl.ActiveView.GraphicsContainer;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            try
            {
                IRubberBand band = new RubberRectangularPolygonClass();
                IGeometry pGeo = band.TrackNew(m_Display, null);
                if (pGeo.IsEmpty)
                {
                    IPoint searchPoint = new PointClass();
                    searchPoint.PutCoords(mapX, mapY);
                    pGeo = DF2DPipe.Class.PublicFunction.DoBuffer(searchPoint, DF2DPipe.Class.PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                    //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                }
                WaitForm.Start("正在查询，请稍后...");
                foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                {
                    foreach (MajorClass mc in lg.MajorClasses)
                    {
                        foreach (SubClass sc in mc.SubClasses)
                        {
                            if (!sc.Visible2D) continue;
                            string[] arrFc2DId = mc.Fc2D.Split(';');
                            if (arrFc2DId == null) continue;
                            IFeatureCursor pFeatureCursor = null;
                            IFeature pFeature = null;
                            foreach (string fc2DId in arrFc2DId)
                            {
                                DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                if (dffc == null) continue;
                                IFeatureClass fc = dffc.GetFeatureClass();
                                FacilityClass facc = dffc.GetFacilityClass();
                                if (facc.Name != "PipeNode") continue;
                                if (fc == null || pGeo == null) continue;
                                ISpatialFilter pSpatialFilter = new SpatialFilter();
                                pSpatialFilter.Geometry = pGeo;
                                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                pFeatureCursor = fc.Search(pSpatialFilter, false);
                                if (pFeatureCursor == null) continue;
                                while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                {
                                    IPoint pPoint = (IPoint)pFeature.Shape;
                                    string strText = "X:" + pPoint.Y.ToString("F2") + "\n" + "Y:" + pPoint.X.ToString("F2");
                                    AddCallout(pPoint, strText);
                                }
                            }
                        }
                    }
                }
                app.Current2DMapControl.ActiveView.Refresh();
                WaitForm.Stop();
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
            }
        }

        public void AddCallout(IPoint pPoint, string strText)
        {
            IActiveView pActiveView;
            IGraphicsContainer pGraphicsContainer;
            IPoint pPntText = new PointClass();
            pPntText.PutCoords(pPoint.X + 3, pPoint.Y + 3);
            IElement pElement;
            IFormattedTextSymbol pTextSymbol = new TextSymbolClass();
            ITextElement pTextElement = new TextElementClass();
            pElement = (IElement)pTextElement;
            pTextElement.Text = strText;
            pElement.Geometry = pPntText;

            IRgbColor pRgbClr = new RgbColorClass();
            pRgbClr.Red = 255;
            pRgbClr.Blue = 255;
            pRgbClr.Green = 255;

            ISimpleFillSymbol pSmplFill = new SimpleFillSymbolClass();
            pSmplFill.Color = pRgbClr;
            pSmplFill.Style = esriSimpleFillStyle.esriSFSHollow;

            IBalloonCallout pBllnCallout = new BalloonCalloutClass();
            pBllnCallout.Style = esriBalloonCalloutStyle.esriBCSRoundedRectangle;
            pBllnCallout.Symbol = pSmplFill;
            pBllnCallout.LeaderTolerance = 5;
            pBllnCallout.AnchorPoint = pPoint;

            pRgbClr.Red = 255;
            pRgbClr.Blue = 0;
            pRgbClr.Green = 0;

            pTextSymbol.Background = (ITextBackground)pBllnCallout;
            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            pTextSymbol.Color = pColor;
            pTextSymbol.Size = SystemInfo.Instance.TextSize;
            pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            pTextElement.Symbol = pTextSymbol;
            pGraphicsContainer = m_ActiveView.GraphicsContainer;
            pGraphicsContainer.AddElement(pElement, 1);

            //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        public override void RestoreEnv()
        {
            try
            {
                IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
                if (mapView == null) return;
                if (app == null || app.Current2DMapControl == null || app.Workbench == null) return;
                pGraphicsContainer.DeleteAllElements();
                app.Current2DMapControl.ActiveView.Refresh();
                mapView.UnBind(this);
                Map2DCommandManager.Pop();
            }
            catch (System.Exception ex)
            {

            }

        }
    }
}
