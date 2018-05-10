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

namespace DF2DAnalysis.Commands
{
    public class CmdFireHydrantSearch2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private IScreenDisplay m_Display;
        private DF2DApplication app;
        private IGraphicsContainer pGraphicsContainer;
        private IFeatureClass fc;
        private IFeature feature;
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
                IGeometry geo = app.Current2DMapControl.TrackCircle();
                if (geo != null)
                {
                    AddCircleElement(geo, this.m_ActiveView);
                    this.m_ActiveView.Refresh();
                }
                WaitForm.Start("正在查询，请稍后...");
                FacilityClass facc = FacilityClassManager.Instance.GetFacilityClassByName("PipeNode");
                if (facc == null) return;
                DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName("Additional");
                if (fi == null) return;

                string[] fc2d = facc.Fc2D.Split(';');
                foreach (string fcID in fc2d)
                {
                    DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fcID);
                    IFeatureClass fctemp = dffc.GetFeatureClass();
                    if (fctemp.AliasName.Contains("上水"))
                    {
                        fc = fctemp;
                        break;
                    }
                }
                if (fc == null) return;
                int index = fc.Fields.FindField(fi.Name);
                if (index == -1) return;
                ISpatialFilter filter = new SpatialFilter();
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                filter.Geometry = geo;
                filter.WhereClause = fi.Name + "='" + "消火栓' OR " + fi.Name + "='" + "消防栓'";
                IFeatureCursor cursor = fc.Search(filter, false);
                int count = fc.FeatureCount(filter);
                if (count == 0) { XtraMessageBox.Show("所选区域内无消防栓！"); WaitForm.Stop(); return; }
                while ((feature = cursor.NextFeature()) != null)
                {
                    string Filename = Application.StartupPath + @"\..\Resource\Images\Icon\fireHydrant.png";
                    double xfea, yfea;
                    xfea = (feature.Extent.XMax + feature.Extent.XMin) / 2;
                    yfea = (feature.Extent.YMax + feature.Extent.YMin) / 2;
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(xfea, yfea);
                    IPictureMarkerSymbol pPictureMarkerSymbol = new PictureMarkerSymbolClass();
                    pPictureMarkerSymbol.Size = 50;
                    pPictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPicturePNG, Filename);
                    IMarkerElement pMarkerElement = new MarkerElementClass();
                    pMarkerElement.Symbol = pPictureMarkerSymbol as IMarkerSymbol;
                    IElement pElement = (IElement)pMarkerElement;
                    pElement.Geometry = pPoint;
                    pGraphicsContainer.AddElement(pElement, 0);
                }
                this.m_ActiveView.Refresh();
                WaitForm.Stop();
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                return;
            }
           
     
        }
     

        private void AddCircleElement(IGeometry pCircularArc, IActiveView pAV)
        {
            IFillShapeElement pElemFillShp;
            IElement pElem;
            ISimpleFillSymbol pSFSym;
            IGraphicsContainer pGraphicsContainer;
            pElem = new CircleElementClass();
            pElem.Geometry = (IGeometry)pCircularArc;
            pElemFillShp = (IFillShapeElement)pElem;
            pSFSym = new SimpleFillSymbolClass();
            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.FillColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            pSFSym.Color = pColor;
            pSFSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;

            ISimpleLineSymbol pSLSym = new SimpleLineSymbol();
            pSLSym.Style = esriSimpleLineStyle.esriSLSSolid;
            pSLSym.Width = 1;
            pSLSym.Color = pColor;

            pSFSym.Outline = pSLSym;

            pElemFillShp.Symbol = pSFSym;

            pGraphicsContainer = pAV as IGraphicsContainer;
            pGraphicsContainer.AddElement(pElem, 0);

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
