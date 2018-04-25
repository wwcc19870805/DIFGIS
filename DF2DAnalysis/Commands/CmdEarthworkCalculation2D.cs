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
using ESRI.ArcGIS.Analyst3D;
using DF2DAnalysis.Frm;
using System.Windows.Forms;

namespace DF2DAnalysis.Commands
{
    class CmdEarthworkCalculation2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private IScreenDisplay m_Display;
        private IGeoDataset beforeGeo;
        private IGeoDataset afterGeo;
        private IGeometry geo;
        private IFeatureClass pFc;
        private double h;
        private string surfaceArea;
        private string projectArea;
        private string dig;
        private string fill;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMouseDown(button, shift, x, y, mapX, mapY);
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
                DF2DApplication.Application.Current2DMapControl.DrawShape(geo, ref symbol);
                //WaitForm.Start("正在查询...", "请稍后");
            }
            if (geo == null) return;
            h = GetH(geo);
            pFc = GetGCD();
            AddHField(pFc);
            beforeGeo = GetBeforeGeo(pFc);
            afterGeo = GetAfterGeo(pFc);
            if (beforeGeo == null || afterGeo == null) return;
            SurfaceProcess(beforeGeo, afterGeo);

            FrmEarthworkCalculation2D dialog = new FrmEarthworkCalculation2D(surfaceArea, projectArea, dig, fill);
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.OK)
            {
                RestoreEnv();
            }
            //IRasterSurface rasterSurface = new RasterSurfaceClass();
            //rasterSurface.PutRaster((IRaster)beforeGeo, 0);
            //ISurface pSurface = rasterSurface as ISurface;

            //surfaceArea = pSurface.GetSurfaceArea(h, esriPlaneReferenceType.esriPlaneReferenceAbove).ToString(".###") + "㎡";
            //projectArea = pSurface.GetProjectedArea(h, esriPlaneReferenceType.esriPlaneReferenceAbove).ToString(".###") + "㎡";

            //ISurfaceOp SurfaceOp = new RasterSurfaceOpClass();
            //IGeoDataset outGeoDataset = SurfaceOp.CutFill(beforeGeo, afterGeo, Type.Missing);
            //IRasterBandCollection pRsBandCol = outGeoDataset as IRasterBandCollection;

            //double area = 0.0;//涉及面积
            //double volume = 0.0;//挖方方量
            //double volume1 = 0.0;//填方方量
            //double sumVolume = 0;//挖方总量
            //double sumVolume1 = 0;//填方总量
            //for (int i = 0; i < pRsBandCol.Count; i++)
            //{
            //    IRasterBand pBand = pRsBandCol.Item(0);
            //    ITable pRTable = pBand.AttributeTable;
            //    ICursor pCursor = pRTable.Search(null, true);
            //    获取像素块中的值
            //    IRow pRrow = pCursor.NextRow();
            //    while (pRrow != null)
            //    {
            //        area += Convert.ToDouble(pRrow.get_Value(pRrow.Fields.FindField("AREA")).ToString());
            //        volume = Convert.ToDouble(pRrow.get_Value(pRrow.Fields.FindField("VOLUME")).ToString());
            //        if (volume > 0)
            //        {
            //            sumVolume += volume;
            //        }
            //        else
            //        {
            //            sumVolume1 += volume;
            //        }
            //        pRrow = pCursor.NextRow();
            //    }
            //}
            //string dig = Math.Round(sumVolume, 3).ToString() + "m³";
            //string fill = Math.Round(sumVolume1, 3).ToString() + "m³";
            //FrmEarthworkCalculation2D dialog = new FrmEarthworkCalculation2D(surfaceArea, projectArea, dig, fill);
            //if (dialog.DialogResult == DialogResult.OK)
            //{
            //    RestoreEnv();
            //}
        }

        private void SurfaceProcess(IGeoDataset beforeGeo,IGeoDataset afterGeo)
        {
            IRasterSurface rasterSurface = new RasterSurfaceClass();
            rasterSurface.PutRaster((IRaster)beforeGeo, 0);
            ISurface pSurface = rasterSurface as ISurface;

            surfaceArea = pSurface.GetSurfaceArea(h, esriPlaneReferenceType.esriPlaneReferenceAbove).ToString(".###") + "㎡";
            projectArea = pSurface.GetProjectedArea(h, esriPlaneReferenceType.esriPlaneReferenceAbove).ToString(".###") + "㎡";

            ISurfaceOp SurfaceOp = new RasterSurfaceOpClass();
            IGeoDataset outGeoDataset = SurfaceOp.CutFill(beforeGeo, afterGeo, Type.Missing);
            IRasterBandCollection pRsBandCol = outGeoDataset as IRasterBandCollection;

            double area = 0.0;//涉及面积
            double volume = 0.0;//挖方方量
            double volume1 = 0.0;//填方方量
            double sumVolume = 0;//挖方总量
            double sumVolume1 = 0;//填方总量
            for (int i = 0; i < pRsBandCol.Count; i++)
            {
                IRasterBand pBand = pRsBandCol.Item(0);
                ITable pRTable = pBand.AttributeTable;
                ICursor pCursor = pRTable.Search(null, true);
                
                IRow pRrow = pCursor.NextRow();
                while (pRrow != null)
                {
                    area += Convert.ToDouble(pRrow.get_Value(pRrow.Fields.FindField("AREA")).ToString());
                    volume = Convert.ToDouble(pRrow.get_Value(pRrow.Fields.FindField("VOLUME")).ToString());
                    if (volume > 0)
                    {
                        sumVolume += volume;
                    }
                    else
                    {
                        sumVolume1 += volume;
                    }
                    pRrow = pCursor.NextRow();
                }
            }
            dig = Math.Round(sumVolume, 3).ToString() + "m³";
            fill = Math.Round(sumVolume1, 3).ToString() + "m³";
           
        }
       

        private void AddHField(IFeatureClass fc)
        {
            int index = fc.FindField("HEIGHT");
            if (index == -1)
            {
                IField Field = new FieldClass();
                IFieldEdit pFieldEdit = Field as IFieldEdit;
                pFieldEdit.Name_2 = "HEIGHT";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                fc.AddField(Field);
            }
            else
                return;
        }
        private IFeatureClass GetGCD()
        {
            string path = Config.GetConfigValue("2DMdbPipe");
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
            IFeatureClass pFc = pFWs.OpenFeatureClass("GCD");
            //IFeatureDataset pFDs = pFWs.OpenFeatureDataset("GCD");
            //if (pFDs == null) return null;
            //IFeatureClass pFc = pFDs as IFeatureClass;
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
            try
            {
                IInterpolationOp3 pInterpolationOp = new RasterInterpolationOpClass();

                IFields fields = fc.Fields;
                int index = fields.FindFieldByAliasName("高程");
                if (index == -1) return null;
                IField field = fields.get_Field(index);

                ISpatialFilter filter = new SpatialFilter();
                filter.Geometry = geo;
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                int count = fc.FeatureCount(filter);
                if (count == 0) return null;
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
            catch (System.Exception ex)
            {
                return null;
            }
            
        }

        private IGeoDataset GetAfterGeo(IFeatureClass fc)
        {
            try
            {
                ISpatialFilter pSpatialFilter = new SpatialFilter();
                pSpatialFilter.Geometry = geo;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                int count = fc.FeatureCount(pSpatialFilter);
                if (count == 0) return null;
                IFeatureCursor cursor = fc.Search(pSpatialFilter, true);
                IFeature feature;
                while ((feature = cursor.NextFeature()) != null)
                {
                    int index = feature.Fields.FindField("HEIGHT");
                    if (index == -1) continue;
                    feature.set_Value(index, h);
                    feature.Store();
                }

                IInterpolationOp3 pInterpolationOp = new RasterInterpolationOpClass();

                IFields fields = fc.Fields;
                int index1 = fields.FindField("HEIGHT");
                if (index1 == -1) return null;
                IField field = fields.get_Field(index1);

                IFeatureClassDescriptor pFcd = new FeatureClassDescriptorClass();
                pFcd.Create(fc, pSpatialFilter, field.Name);

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
            catch (System.Exception ex)
            {
                return null;
            }
          
        }

        private double  GetH( IGeometry geo)
        {
            IPointCollection pCol = geo as IPointCollection;
            double h = 0.0;
            for (int i = 0; i < pCol.PointCount; i++)
            {
                IPoint point = pCol.get_Point(i);
                if (point.Z < h) h = point.Z;
            }
            return h;
           
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            gc.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
    }
}
