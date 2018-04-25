using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DFWinForms.Class;
using DF2DData.Class;
using DevExpress.XtraTreeList.Nodes;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DControl.Base;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using DF2DPipe.Class;
using ICSharpCode.Core;
using System.Data;
using DevExpress.XtraBars.Docking;
using DF2DPipe.Query.UC;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Display;


namespace DF2DPipe.Query.Command
{
    public class CmdRectQuery : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private DockPanel _dockPanel;
        private int _height = 200;
        private UIDockPanel _uPanel;
        private int _width = 400;
        private UCPropertyInfo2D _ucPropInfo2D;
        public Dictionary<string, DataTable> _dict = new Dictionary<string, DataTable>();

        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(false);
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;




        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            this._dict.Clear();
            bool ready = true;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(true);
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
                    IRubberBand band = new RubberRectangularPolygonClass();
                    IGeometry pGeo = band.TrackNew(m_Display, null);
                    app.Current2DMapControl.DrawShape(pGeo, ref symbol);
                    WaitForm.Start("正在查询...", "请稍后");
                    if (pGeo.IsEmpty)
                    {
                        IPoint searchPoint = new PointClass();
                        searchPoint.PutCoords(mapX, mapY);
                        pGeo = PublicFunction.DoBuffer(searchPoint, PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                        //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                    }
                    if (ready)
                    {

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
                                        if (facc.Name != "PipeLine") continue;
                                        if (fc == null || pGeo == null) continue;
                                        ISpatialFilter pSpatialFilter = new SpatialFilter();
                                        pSpatialFilter.Geometry = pGeo;
                                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                        string whereClause = UpOrDown.DecorateWhereClasuse(fc) + mc.ClassifyField + " =  '" + sc.Name + "'";
                                        
                                        pSpatialFilter.WhereClause = whereClause;
                                        pFeatureCursor = fc.Search(pSpatialFilter, false);
                                        if (pFeatureCursor == null) continue;
                                        DataTable dt = new DataTable();
                                        dt.TableName = facc.Name;
                                        DataColumn oidcol = new DataColumn();
                                        oidcol.ColumnName = "oid";
                                        oidcol.Caption = "ID";
                                        dt.Columns.Add(oidcol);
                                        foreach (DFDataConfig.Class.FieldInfo fitemp in facc.FieldInfoCollection)
                                        {
                                            if (!fitemp.CanQuery) continue;
                                            DataColumn col = new DataColumn();
                                            col.ColumnName = fitemp.Name;
                                            col.Caption = fitemp.Alias;
                                            dt.Columns.Add(col);
                                        }
                                        while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                        {
                                            DataRow dtRow = dt.NewRow();
                                            dtRow["oid"] = pFeature.get_Value(pFeature.Fields.FindField("OBJECTID"));
                                            foreach (DataColumn col in dt.Columns)
                                            {
                                                int index1 = pFeature.Fields.FindField(col.ColumnName);
                                                if (index1 < 0) continue;
                                                object obj1 = pFeature.get_Value(index1);
                                                string str = "";
                                                if (obj1 != null)
                                                {
                                                    IField field = pFeature.Fields.get_Field(index1);
                                                    switch (field.Type)
                                                    {
                                                        case esriFieldType.esriFieldTypeBlob:
                                                        case esriFieldType.esriFieldTypeGeometry:
                                                        case esriFieldType.esriFieldTypeRaster:
                                                            continue;
                                                        case esriFieldType.esriFieldTypeDouble:

                                                            double d;
                                                            if (double.TryParse(obj1.ToString(), out d))
                                                            {
                                                                str = d.ToString("0.00");
                                                            }
                                                            break;
                                                        default:
                                                            str = obj1.ToString();
                                                            break;
                                                    }
                                                }
                                                dtRow[col.ColumnName] = str;
                                            }
                                            dt.Rows.Add(dtRow);
                                            
                                        }
                                        if (dt.Rows.Count > 0) this._dict.Add(sc.Name, dt);

                                    }
                                }
                            }
                        }                    
                        WaitForm.Stop();
                        try
                        {
                            this._uPanel = new UIDockPanel("查询结果", "查询结果", this.Location, this._width, this._height);
                            this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                            this._dockPanel.Visibility = DockVisibility.Visible;
                            this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                            this._dockPanel.Width = this._width;
                            this._dockPanel.Height = this._height;
                            if (this._ucPropInfo2D == null) this._ucPropInfo2D = new UCPropertyInfo2D();
                            this._ucPropInfo2D.Dock = System.Windows.Forms.DockStyle.Fill;
                            this._uPanel.RegisterEvent(new PanelClose(this.Close));
                            this._dockPanel.Controls.Add(this._ucPropInfo2D);
                            this._ucPropInfo2D.Init();
                            this._ucPropInfo2D.SetPropertyInfo(this._dict);
                        }
                        catch 
                        {
                        }

                    }
                }
            }
            catch 
            {
            }
            finally
            {
                WaitForm.Stop();
            }


        }


        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
            if (mapView == null) return;
            mapView.UnBind(this);
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
