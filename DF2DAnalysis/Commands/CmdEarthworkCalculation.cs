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
using DF2DAnalysis.UC;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;

namespace DF2DAnalysis.Commands
{
    class CmdEarthworkCalculation : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private IScreenDisplay m_Display;
        private IGeoDataset beforeGeo;
        private IGeoDataset afterGeo;
        private IGeometry m_Geo;
        private IFeatureClass pFc;
        private IRubberBand rubberBand = null;
        private IGraphicsContainer pGraphicsContainer;
        private double h;
        private string surfaceArea;
        private string projectArea;
        private string dig;
        private string fill;

        private DockPanel _dockPanel;
        private int _height = 600;
        private UIDockPanel _uPanel;
        private int _width = 350;
        private UCEarthworkCalculation _uc;
        private IGraphicsContainer _gc;

        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                Map2DCommandManager.Push(this);
                IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
                if (mapView == null) return;
                bool bBind = mapView.Bind(this);
                if (!bBind) return;
                DF2DApplication app = DF2DApplication.Application;
                if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
                //app.Workbench.SetMenuEnable(false);
                m_ActiveView = app.Current2DMapControl.ActiveView;
                m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
                _gc = app.Current2DMapControl.ActiveView.GraphicsContainer;
                _gc.DeleteAllElements();
                app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;

                this._uPanel = new UIDockPanel("开挖分析", "开挖分析", this.Location, this._width, this._height);
                this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                this._dockPanel.Visibility = DockVisibility.Visible;
                this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                this._dockPanel.Width = this._width;
                this._dockPanel.Height = this._height;
                this._uc = new UCEarthworkCalculation();
                this._uc.Dock = System.Windows.Forms.DockStyle.Top;
                this._uPanel.RegisterEvent(new PanelClose(this.Close));
                this._dockPanel.Controls.Add(this._uc);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            if (_uc.DrawShape == 1)
            {
                //rubberBand = new RubberCircleClass();
                //IGeometry geo = rubberBand.TrackNew(this.m_ActiveView.ScreenDisplay, null);
                DF2DApplication app = DF2DApplication.Application;
                IGeometry geo = app.Current2DMapControl.TrackCircle();
                if (geo != null)
                {
                    AddCircleElement(geo, this.m_ActiveView);
                    this.m_ActiveView.Refresh();                   
                    m_Geo = geo;
                }
               

            }
            else if (_uc.DrawShape == 2)
            {
                rubberBand = new RubberRectangularPolygonClass();
                IGeometry geo = rubberBand.TrackNew(m_Display, null);
                if (geo != null)
                {
                    AddRectangleElement(geo, this.m_ActiveView);
                    this.m_ActiveView.Refresh();
                    m_Geo = geo;
                }
            }
            else
            {
                rubberBand = new RubberPolygonClass();
                IGeometry geo = rubberBand.TrackNew(m_Display, null);
                if (geo != null)
                {
                    AddPolygonElement(geo, this.m_ActiveView);
                    this.m_ActiveView.Refresh();
                    m_Geo = geo;
                }
            }
            try
            {
                WaitForm.Start("正在分析...","请稍后");
                //h = _uc.H;
                pFc = GetGCD();
                h = GetH(m_Geo, pFc);               
                AddHField(pFc);
                beforeGeo = GetBeforeGeo(pFc, m_Geo);
                if (beforeGeo == null) { WaitForm.Stop(); _gc.DeleteAllElements(); return; }
                afterGeo = GetAfterGeo(pFc, m_Geo);
                if (afterGeo == null) { WaitForm.Stop(); _gc.DeleteAllElements(); return; }
                SurfaceProcess(beforeGeo, afterGeo);
                WaitForm.Stop();
                this._uc.SurfaceArea = "地表面积：" + surfaceArea;
                this._uc.ProjectArea = "投影面积：" + projectArea;
                this._uc.Dig = "挖方量：" + dig;
                this._uc.Fill = "填方量：" + fill;
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
            }
            
        }
        private void SurfaceProcess(IGeoDataset beforeGeo, IGeoDataset afterGeo)
        {
            try
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
                fill = Math.Round(Math.Abs(sumVolume1), 3).ToString() + "m³";
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
            }
            
          

        }


        private void AddHField(IFeatureClass fc)
        {
            FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("GCD");
            DFDataConfig.Class.FieldInfo  fi = fac.GetFieldInfoBySystemName("Height");
            if (fi == null) return;

            int index = fc.FindField(fi.Name);
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
            //string path = Config.GetConfigValue("2DMdbPipe");
            //IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            //IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
            //IFeatureClass pFc = pFWs.OpenFeatureClass("GCD");           
            //return pFc;
            FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("GCD");
            string[] arrayFc2D = fac.Fc2D.Split(';');
            foreach (string fcID in arrayFc2D)
            {
                DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fcID);
                if (dffc == null) continue;
                IFeatureClass fc = dffc.GetFeatureClass();
                return fc;
            }
            return null;
            
        }

        private IGeoDataset GetBeforeGeo(IFeatureClass fc,IGeometry geo)
        {
            try
            {
                WaitForm.SetCaption("正在生成栅格表面...");
                IInterpolationOp3 pInterpolationOp = new RasterInterpolationOpClass();

                IFields fields = fc.Fields;
                FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("GCD");
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("Altitude");
                if (fi == null) return null;

                int index = fields.FindField(fi.Name);
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
                WaitForm.Stop();
            }

        }

        private IGeoDataset GetAfterGeo(IFeatureClass fc,IGeometry geo)
        {
            try
            {
                WaitForm.SetCaption("正在计算填挖表面...");
                ISpatialFilter pSpatialFilter = new SpatialFilter();
                pSpatialFilter.Geometry = geo;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                int count = fc.FeatureCount(pSpatialFilter);
                if (count == 0) return null;
                IFeatureCursor cursor = fc.Search(pSpatialFilter, true);
                IFeature feature;
                FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("GCD");
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("Height");
                if (fi == null) return null;
                while ((feature = cursor.NextFeature()) != null)
                {
                    int index = feature.Fields.FindField(fi.Name);
                    if (index == -1) continue;
                    feature.set_Value(index, h);
                    feature.Store();
                }

                IInterpolationOp3 pInterpolationOp = new RasterInterpolationOpClass();

                //IFields fields = fc.Fields;
                //int index1 = fields.FindField("HEIGHT");
                //if (index1 == -1) return null;
                //IField field = fields.get_Field(index1);

                IFeatureClassDescriptor pFcd = new FeatureClassDescriptorClass();
                pFcd.Create(fc, pSpatialFilter, fi.Name);

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
                WaitForm.Stop();
            }

        }
        private void AddRectangleElement(IGeometry geo, IActiveView pAV)
        {
            IElement pElem = new RectangleElement();
            pElem.Geometry = geo;
            

            ISimpleFillSymbol pSFSym = new SimpleFillSymbol();
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

            IFillShapeElement pElemFillShp = pElem as IFillShapeElement;
            pElemFillShp.Symbol = pSFSym;
            
            pGraphicsContainer = pAV as IGraphicsContainer;
            pGraphicsContainer.AddElement(pElem, 0);
        }

        private void AddPolygonElement(IGeometry geo, IActiveView pAV)
        {
            IElement pElem = new PolygonElement();
            pElem.Geometry = geo;


            ISimpleFillSymbol pSFSym = new SimpleFillSymbol();
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

            IFillShapeElement pElemFillShp = pElem as IFillShapeElement;
            pElemFillShp.Symbol = pSFSym;

            pGraphicsContainer = pAV as IGraphicsContainer;
            pGraphicsContainer.AddElement(pElem, 0);
        }
        private void AddCircleElement(IGeometry pCircularArc, IActiveView pAV)
        {
            IFillShapeElement pElemFillShp;
            IElement pElem;
            ISimpleFillSymbol pSFSym;
            //ISegmentCollection pSegColl = new PolygonClass();
            //object missing = Type.Missing;
            //ISegment segement = (ISegment)pCircularArc;
            //pSegColl.AddSegment(segement, missing, missing);
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

        private double GetH(IGeometry geo,IFeatureClass fc)
        {
            double h = 0.0;
            if (this._uc.DrawShape == 1)
            {
                if (this._uc.PointType == 1||this._uc.PointType == 2)
                {
                    WaitForm.Stop();
                    XtraMessageBox.Show("所选参考点无高程值,高程值取0");
                    WaitForm.Start("参考点高程设置成功");
                }
                else
                {
                    ISpatialFilter pSpatialFilter = new SpatialFilter();
                    pSpatialFilter.Geometry = geo;
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    int count = fc.FeatureCount(pSpatialFilter);
                    if (count == 0) return 0;
                    //{ WaitForm.Stop(); XtraMessageBox.Show("所画区域无高程点，高程值取0"); WaitForm.Start("参考点高程设置成功"); }
                    IFeatureCursor cursor = fc.Search(pSpatialFilter, true);
                    IFeature feature;                    
                    int index = fc.Fields.FindFieldByAliasName("高程");
                    if (index == -1) return h;
                    while ((feature = cursor.NextFeature()) != null)
                    {
                        double temp = Convert.ToDouble(feature.get_Value(index));
                        if (temp == null) continue;
                        if (temp < h) h = temp;
                    }
                    h = h - this._uc.Depth;
                    
                }
            }
            else 
            {
                h =  this._uc.H;
            }
            return h;
            WaitForm.SetCaption("参考点高程设置成功");
        }

        private void Close()
        {
            this.RestoreEnv();
        }
        public override void RestoreEnv()
        {
            if (this._uc != null)
            {
                this._uc.RestoreEnv();
                this._uc.Dispose();
                this._uc = null;
            }
            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(true);
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            gc.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            WaitForm.Stop();
            Map2DCommandManager.Pop();
        }

        private System.Drawing.Point Location
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }
    }
}
