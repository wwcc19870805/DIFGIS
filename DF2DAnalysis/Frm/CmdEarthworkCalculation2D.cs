using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using DFWinForms.Service;
using DF2DData.Class;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geometry;
using System.Collections;
using ESRI.ArcGIS.Display;
using DFCommon.Class;
using System.Drawing;
using DevExpress.XtraEditors;
using DFWinForms.Class;
using DFDataConfig.Logic;
using DF2DPipe.Class;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;

namespace DF2DAnalysis.Commands
{
    class CmdEarthworkCalculation2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private IGeoDataset beforeGeo;
        private IGeoDataset afterGeo;
        private IGeometry geo;
        private IFeatureClass pFc;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            bool ready = true;
            if (app == null || app.Current2DMapControl == null) return;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            try
            {
                if (button == 1)
                {
                    ISimpleLineSymbol pLineSym = new SimpleLineSymbol();
                    IRgbColor pColor = new RgbColorClass();
                    pColor.Red = 255;
                    pColor.Green = 255;
                    pColor.Blue = 0;
                    pLineSym.Color = pColor;
                    pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
                    pLineSym.Width = 2;

                    ISimpleFillSymbol pFillSym = new SimpleFillSymbol();

                    pFillSym.Color = pColor;
                    pFillSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
                    pFillSym.Outline = pLineSym;

                    object symbol = pFillSym as object;
                    IRubberBand band = new RubberPolygonClass();
                    geo = band.TrackNew(m_Display, null);
                    app.Current2DMapControl.DrawShape(geo, ref symbol);
                    //WaitForm.Start("正在查询...", "请稍后");
                 }
#region  备用代码
                /*
                IGeoDataset pRasterGeo = GetDEM();
                IExtractionOp pExOp = new RasterExtractionOpClass();
                IPolygon polygon = geo as IPolygon;
                polygon.SpatialReference = pRasterGeo.SpatialReference;
                beforeGeo = pExOp.Polygon(pRasterGeo, polygon, true);

                IGeoDataset geoDatasetTemp = new RasterDatasetClass();
                geoDatasetTemp = beforeGeo;
                IRasterBandCollection rbCol = geoDatasetTemp as IRasterBandCollection;
                for (int i = 0; i < rbCol.Count;i++ )
                {
                    if (rbCol.Item(i) == null) continue;
                    IRasterBand rb = rbCol.Item(i);
                    ITable table = rb.AttributeTable;
                    ICursor cursor = table.Search(null, false);
                    IRow row ;
                    while ((row = cursor.NextRow()) != null)
                    {
                        row.set_Value(1,50);
                        row.Store();
                    }
                }
                afterGeo = geoDatasetTemp;

                ISurfaceOp surOp = new RasterSurfaceOpClass();
                IGeoDataset outputGeo = surOp.CutFill(beforeGeo, afterGeo, Type.Missing);*/
#endregion
                pFc = GetGCD();
                beforeGeo = GetBeforeGeo(pFc);


            }
            catch (System.Exception ex)
            {
            	
            }

        }

        private IFeatureClass GetGCD()
        {
            string path = Config.GetConfigValue("2DMdb");
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
            IFeatureDataset pFDs = pFWs.OpenFeatureDataset("GCD");
            if (pFDs == null) return null;
            IFeatureClass pFc = pFDs as IFeatureClass;
            return pFc;
        }
        private IGeoDataset GetDEM()
        {
            try
            {
                string path = Config.GetConfigValue("2DDEM");
                IWorkspaceFactory2 pWsF = new AccessWorkspaceFactoryClass();
                IRasterWorkspaceEx pRws = pWsF.OpenFromFile(path, 0) as IRasterWorkspaceEx;
                //IRasterDataset pRDs = new RasterDataset();
                //IRasterWorkspace pRws = pFws as IRasterWorkspace;
                IRasterDataset pRDs = pRws.OpenRasterDataset("DY_DEM");
                IGeoDataset pRGeo = pRDs as IGeoDataset;
                return pRGeo;
            }
            catch (System.Exception ex)
            {
                return null;
            }
            
        }
        private IGeoDataset GetBeforeGeo(IFeatureClass fc)
        {
            IInterpolationOp3 pInterpolationOp = new RasterInterpolationOpClass();

            IFields fields = fc.Fields;
            int index = fields.FindFieldByAliasName("高程");
            IField field = fields.get_Field(index);

            ISpatialFilter filter = new SpatialFilter();
            filter.Geometry = geo;
            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

            IFeatureClassDescriptor pFcd = new FeatureClassDescriptorClass();
            pFcd.Create(fc, filter, field.Name);

            //定义搜索半径
            IRasterRadius pRadius = new RasterRadiusClass();
            object Missing = Type.Missing;
            pRadius.SetVariable(12, ref Missing);

            //设置栅格图像的单位大小
            object cellSizeProvider = 5;
            IRasterAnalysisEnvironment pEnv = pInterpolationOp as IRasterAnalysisEnvironment;
            pEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref  cellSizeProvider);
            IGeoDataset outGeoDataset = pInterpolationOp.IDW(pFcd as IGeoDataset, 2, pRadius, ref Missing);
            return outGeoDataset;
        }
        //private IGeoDataset GetAfterGeo(IWorkspace ws,double h)
        //{
            
        //}
    }
}
