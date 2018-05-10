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
using DF2DData.UserControl.Pad;
using DF2DData.Tree;
using DF2DPipe.Query.Frm;

namespace DF2DPipe.Query.Command
{
    public class CmdIdentifyQuery : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private DockPanel _dockPanel;
        private int _height = 200;
        private UIDockPanel _uPanel;
        private int _width = 300;
        private UCPropertyInfo2D _ucPropInfo2D;
        private TreeList treelist;
        public Dictionary<string, DataTable> _dict = new Dictionary<string, DataTable>();

        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            Layer2DTreePad wsPad = (Layer2DTreePad)UCService.GetContent(typeof(Layer2DTreePad));
            this.treelist = wsPad.TreeList;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            app.Workbench.SetMenuEnable(false);           
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
            FrmMajorClass.Instance.Show();
  
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            this._dict.Clear();
            m_ActiveView = app.Current2DMapControl.ActiveView;
            if(this.m_ActiveView.FocusMap.FeatureSelection != null)
                this.m_ActiveView.FocusMap.ClearSelection();
            bool ready = false;
            bool haveone = false;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            app.Workbench.SetMenuEnable(true);
            IGeometry pGeo = null;
            try
            {
                if (button == 1)
                {
                    WaitForm.Start("正在查询...", "请稍后");
                    PointClass searchPoint = new PointClass();
                    searchPoint.PutCoords(mapX, mapY);
                    pGeo = PublicFunction.DoBuffer(searchPoint,
                        PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                    if (pGeo == null) return;
                    ready = true;
                    if (ready == true)
                    {                      
                        //foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                        //{
                            IFeatureCursor pFeatureCursor = null;
                            IFeature pFeature = null;
                            ISpatialFilter pSpatialFilter = new SpatialFilter();
                            IFeatureClass fc;
                            DF2DFeatureClass dffc;
                            //foreach (MajorClass mc in lg.MajorClasses)
                            foreach (MajorClass mc in FrmMajorClass.Instance.MajorClasses)
                            {
                                foreach (SubClass sc in mc.SubClasses)
                                {                                 
                                    if (!sc.Visible2D) continue;
                                    string[] arrFc2DId = mc.Fc2D.Split(';');
                                    if (arrFc2DId == null) continue;
                                 
                                    foreach (string fc2DId in arrFc2DId)
                                    {
                                        dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                        if (dffc == null) continue;
                                        fc = dffc.GetFeatureClass();
                                        FacilityClass facc = dffc.GetFacilityClass();
                                        if (facc.Name != "PipeLine") continue;
                                        if (fc == null || pGeo == null) continue;
                                        pSpatialFilter = new SpatialFilter();
                                        pSpatialFilter.Geometry = pGeo;
                                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                        string whereClause = UpOrDown.DecorateWhereClasuse(fc) + mc.ClassifyField + " =  '" + sc.Name + "'";
                                        pSpatialFilter.WhereClause = whereClause;
                                        pFeatureCursor = fc.Search(pSpatialFilter, false);
                                        if (pFeatureCursor == null) continue;
                                        pFeature = pFeatureCursor.NextFeature();
                                        if (pFeature == null)  continue;
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
                                       
                                        DataRow dtRow = dt.NewRow();
                                        dtRow["oid"] = pFeature.get_Value(pFeature.Fields.FindField("OBJECTID"));
                                        foreach (DataColumn col in dt.Columns)
                                        {
                                            int index1 = pFeature.Fields.FindField(col.ColumnName);
                                            if (index1 < 0) continue;
                                            object obj1 = GetFieldValueByIndex(pFeature, index1);
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
                                        if (dt.Rows.Count > 0) this._dict[sc.Name] = dt;
                                        haveone = true;
                                        break;
                                    }
                                    if (haveone) break;
                                }
                                if (haveone) break;                    
                            }
                        }
                        #region  查询建筑物
                        if (!haveone)
                        {
                            FacilityClass facBuild = FacilityClassManager.Instance.GetFacilityClassByName("Building");
                            if (facBuild != null)
                            {
                                haveone = HaveOne(facBuild, pGeo);
                            }                           
                            if (!haveone)
                            {
                                FacilityClass facStruct = FacilityClassManager.Instance.GetFacilityClassByName("Structure");
                                if (facStruct != null)
                                {
                                    haveone = HaveOne(facStruct, pGeo);
                                }
                            }
                        }
                       
                        #endregion
                        try
                        {
                            if (!haveone)
                            {
                                WaitForm.Stop();
                                XtraMessageBox.Show("未选中要素，请重新选择", "提示");
                                return;
                            } 
                            this._uPanel = new UIDockPanel("查询结果", "查询结果", this.Location, this._width, this._height);
                            this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                            this._dockPanel.Visibility = DockVisibility.Visible;
                            this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                            this._dockPanel.Width = this._width;
                            this._dockPanel.Height = this._height;
                            if(this._ucPropInfo2D == null) this._ucPropInfo2D = new UCPropertyInfo2D();
                            this._ucPropInfo2D.Dock = System.Windows.Forms.DockStyle.Fill;
                            this._uPanel.RegisterEvent(new PanelClose(this.Close));
                            this._dockPanel.Controls.Add(this._ucPropInfo2D);
                            this._ucPropInfo2D.Init();
                            this._ucPropInfo2D.SetPropertyInfo(this._dict);
                            WaitForm.Stop();                     
                        }
                        catch 
                        {
                            WaitForm.Stop();
                        }
                    }
                //}
            }
            catch 
            {
                WaitForm.Stop();
            }
            finally
            {

            }
        }

        private bool HaveOne(FacilityClass facc,IGeometry pGeo)
        {
            bool haveone = false;
            string[] fc2D = facc.Fc2D.Split(';');
            if (fc2D.Length == 0) return false;
            string facID = fc2D[0];
            DF2DFeatureClass dffacc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(facID);
            //如果图层不显示，则不查询
            TreeNodeComLayer treeLayer = dffacc.GetTreeLayer();
            if (treeLayer == null || !treeLayer.Visible) return false;

            IFeatureClass fc = dffacc.GetFeatureClass();
            ISpatialFilter filter = new SpatialFilter();
            filter = new SpatialFilter();
            filter.Geometry = pGeo;
            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor cursor = fc.Search(filter, false);
            IFeature feature = cursor.NextFeature();
            if (feature != null)
            {
                haveone = true;
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

                DataRow dtRow = dt.NewRow();
                dtRow["oid"] = feature.get_Value(feature.Fields.FindField("OBJECTID"));
                foreach (DataColumn col in dt.Columns)
                {
                    int index1 = feature.Fields.FindField(col.ColumnName);
                    if (index1 < 0) continue;
                    object obj1 = GetFieldValueByIndex(feature,index1);
                    string str = "";
                    if (obj1 != null)
                    {
                        IField field = feature.Fields.get_Field(index1);
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
                if (dt.Rows.Count > 0) this._dict[facc.Alias] = dt;
            }
                return haveone;
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

        private object GetFieldValueByIndex(IFeature feature, int index)
        {
            object obj = null;
            if (index == -1) return null;
            IDomain domain = feature.Fields.get_Field(index).Domain;
            if (domain != null && domain.Type == esriDomainType.esriDTCodedValue)
            {
                ICodedValueDomain pCD = domain as ICodedValueDomain;
                for (int i = 0; i < pCD.CodeCount; i++)
                {
                    if (object.Equals(pCD.get_Value(i), feature.get_Value(index)))
                    {
                        obj = pCD.get_Name(i);
                    }
                }
            }
            else
            {
                obj = feature.get_Value(index).ToString();
            }
            return obj;
        }
    }
}
