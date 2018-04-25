using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DControl.Base;
using DF3DDraw;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DF3DEdit.Class;
using DF3DEdit.Service;
using DF3DEdit.Interface;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using DFDataConfig.Class;

namespace DF3DEdit.Frm
{
    public class FrmAddPoint : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(207, 453);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(5, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(197, 423);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "属性";
            this.gridColumn1.FieldName = "FN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "值";
            this.gridColumn2.FieldName = "FV";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "字段";
            this.gridColumn3.FieldName = "F";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(207, 453);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "基本属性";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(207, 453);
            this.layoutControlGroup2.Text = "基本属性";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(201, 427);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmAddPoint
            // 
            this.ClientSize = new System.Drawing.Size(207, 453);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Location = new System.Drawing.Point(10, 150);
            this.Name = "FrmAddPoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "点";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAddPoint_FormClosed);
            this.Load += new System.EventHandler(this.FrmAddPoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }
        
        public FrmAddPoint()
        {
            InitializeComponent();
        }

        public void SetBirthDay(DateTime dt)
        {
            this.time = dt;
        }

        private DF3DApplication app;
        private DrawTool _drawTool;
        private DateTime time;
        private DataTable _dt;
        private void FrmAddPoint_Load(object sender, EventArgs e)
        {
            app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null)
            {
                this.Enabled = false;
                return;
            }
            this._dt = new DataTable();
            this._dt.Columns.Add(new DataColumn("FN", Type.GetType("System.String")));
            this._dt.Columns.Add(new DataColumn("FV", Type.GetType("System.Object")));
            this._dt.Columns.Add(new DataColumn("F", Type.GetType("System.Object")));
            this.gridControl1.DataSource = this._dt;

            LoadProperty();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Point);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void FrmAddPoint_FormClosed(object sender, FormClosedEventArgs e)
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Close();
                this._drawTool.End();
            }
        }

        private void LoadProperty()
        {
            try
            {
                DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
                if (featureClassInfo == null) return;

                IFeatureClass fc = featureClassInfo.GetFeatureClass();
                if (fc == null) return;
                IFieldInfoCollection fiCol = fc.GetFields();
                if (fiCol == null) return;
                FacilityClass fac = featureClassInfo.GetFacilityClass();

                if (fac == null)
                {
                    for (int i = 0; i < fiCol.Count; i++)
                    {
                        IFieldInfo fi = fiCol.Get(i);
                        if (fi.Name == fc.FidFieldName) continue;

                        switch (fi.FieldType)
                        {
                            case gviFieldType.gviFieldBlob:
                            case gviFieldType.gviFieldGeometry:
                            case gviFieldType.gviFieldUnknown:
                                continue;
                            //case gviFieldType.gviFieldFloat:
                            //case gviFieldType.gviFieldDouble:                       
                            //    break;
                            //case gviFieldType.gviFieldFID:
                            //case gviFieldType.gviFieldUUID:
                            //case gviFieldType.gviFieldInt16:
                            //case gviFieldType.gviFieldInt32:
                            //case gviFieldType.gviFieldInt64:
                            //    break;
                            //case gviFieldType.gviFieldString:
                            //    break;
                            //case gviFieldType.gviFieldDate:
                            default:
                                DataRow dr = this._dt.NewRow();
                                dr["FN"] = string.IsNullOrEmpty(fi.Alias) ? fi.Name : fi.Alias;
                                dr["FV"] = null;
                                dr["F"] = fi;
                                this._dt.Rows.Add(dr);
                                break;
                        }
                    }
                }
                else
                {
                    List<DFDataConfig.Class.FieldInfo> list = fac.FieldInfoCollection;
                    if (list != null)
                    {
                        foreach (DFDataConfig.Class.FieldInfo fi1 in list)
                        {
                            if (!fi1.CanQuery) continue;
                            int index = fiCol.IndexOf(fi1.Name);
                            if (index == -1) continue;
                            IFieldInfo fi = fiCol.Get(index);
                            if (fi == null) continue;
                            switch (fi.FieldType)
                            {
                                case gviFieldType.gviFieldBlob:
                                case gviFieldType.gviFieldGeometry:
                                case gviFieldType.gviFieldUnknown:
                                    continue;
                                //case gviFieldType.gviFieldFloat:
                                //case gviFieldType.gviFieldDouble:                       
                                //    break;
                                //case gviFieldType.gviFieldFID:
                                //case gviFieldType.gviFieldUUID:
                                //case gviFieldType.gviFieldInt16:
                                //case gviFieldType.gviFieldInt32:
                                //case gviFieldType.gviFieldInt64:
                                //    break;
                                //case gviFieldType.gviFieldString:
                                //    break;
                                //case gviFieldType.gviFieldDate:
                                default:
                                    DataRow dr = this._dt.NewRow();
                                    dr["FN"] = fi1.ToString();
                                    dr["FV"] = null;
                                    dr["F"] = fi;
                                    this._dt.Rows.Add(dr);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
        }


        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                Clear();
            }
        }

        private void OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GetGeo() != null)
            {
                AddRecord();
            }
        }

        private Dictionary<DF3DFeatureClass, IRowBufferCollection> beforeRowBufferMap = new Dictionary<DF3DFeatureClass, IRowBufferCollection>();
        private void AddRecord()
        {
            try
            {
                this.beforeRowBufferMap.Clear();
                SelectCollection.Instance().Clear();
                DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
                if (featureClassInfo == null) return;
                IFeatureClass fc = featureClassInfo.GetFeatureClass();
                if (fc == null) return;
                IFeatureLayer fl = featureClassInfo.GetFeatureLayer();
                if (fl == null) return;
                IFieldInfoCollection fields = fc.GetFields();
                int indexGeo = fields.IndexOf(fl.GeometryFieldName);
                if (indexGeo == -1) return;
                IFieldInfo fiGeo = fields.Get(indexGeo);
                if (fiGeo == null || fiGeo.GeometryDef == null) return;

                IGeometry geo = this._drawTool.GetGeo();
                if (geo.GeometryType != gviGeometryType.gviGeometryPoint) return;
                IPoint pt = geo as IPoint;
                IPoint geoOut = pt.Clone2(fiGeo.GeometryDef.VertexAttribute) as IPoint;
                if(fiGeo.GeometryDef.HasZ) geoOut.SetCoords(pt.X, pt.Y, pt.Z, 0, 0);
                else geoOut.SetCoords(pt.X, pt.Y, 0, 0, 0);                

                IRowBufferCollection rowCol = new RowBufferCollection();
                IRowBufferFactory fac = new RowBufferFactory();
                IRowBuffer row = fac.CreateRowBuffer(fields);
                row.SetValue(indexGeo, geoOut);
                foreach (DataRow dr in this._dt.Rows)
                {
                    IFieldInfo fi = dr["F"] as IFieldInfo;
                    if (fi == null) continue;
                    string fn = fi.Name;
                    int index = fields.IndexOf(fn);
                    if (index != -1)
                    {
                        if (dr["FV"] == null) row.SetNull(index);
                        else
                        {
                            string strobj = dr["FV"].ToString();
                            bool bRes = false;
                            switch (fi.FieldType)
                            {
                                case gviFieldType.gviFieldBlob:
                                case gviFieldType.gviFieldGeometry:
                                case gviFieldType.gviFieldUnknown:
                                    break;
                                case gviFieldType.gviFieldFloat:
                                    float f;
                                    bRes = float.TryParse(strobj, out f);
                                    if (bRes)
                                    {
                                        row.SetValue(index, f);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                case gviFieldType.gviFieldDouble:
                                    double d;
                                    bRes = double.TryParse(strobj, out d);
                                    if (bRes)
                                    {
                                        row.SetValue(index, d);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                case gviFieldType.gviFieldFID:
                                case gviFieldType.gviFieldUUID:
                                case gviFieldType.gviFieldInt16:
                                    Int16 i16;
                                    bRes = Int16.TryParse(strobj, out i16);
                                    if (bRes)
                                    {
                                        row.SetValue(index, i16);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                case gviFieldType.gviFieldInt32:
                                    Int32 i32;
                                    bRes = Int32.TryParse(strobj, out i32);
                                    if (bRes)
                                    {
                                        row.SetValue(index, i32);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                case gviFieldType.gviFieldInt64:
                                    Int64 i64;
                                    bRes = Int64.TryParse(strobj, out i64);
                                    if (bRes)
                                    {
                                        row.SetValue(index, i64);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break; ;
                                case gviFieldType.gviFieldString:
                                    row.SetValue(index, strobj);
                                    break;
                                case gviFieldType.gviFieldDate:
                                    DateTime dt;
                                    bRes = DateTime.TryParse(strobj, out dt);
                                    if (bRes)
                                    {
                                        row.SetValue(index, dt);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                rowCol.Add(row);
                beforeRowBufferMap[featureClassInfo] = rowCol;
                UpdateDatabase();
            }
            catch (Exception ex)
            {
            }
        }
        private void UpdateDatabase()
        {
            DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
            if (featureClassInfo == null) return;
            IFeatureClass fc = featureClassInfo.GetFeatureClass();
            if (fc == null) return;
            int count = 1;
            EditParameters editParameters = new EditParameters(fc.Guid.ToString());
            editParameters.connectionInfo = CommonUtils.Instance().GetCurrentFeatureDataset().DataSource.ConnectionInfo.ToConnectionString();
            editParameters.datasetName = CommonUtils.Instance().GetCurrentFeatureDataset().Name;
            editParameters.geometryMap = beforeRowBufferMap;
            editParameters.nTotalCount = count;
            editParameters.TemproalTime = this.time;
            IBatcheEdit batcheEdit = BatchEditFactory.CreateBatchEdit(count);
            batcheEdit.BeginEdit();
            batcheEdit.DoWork(EditType.ET_INSERT_FEATURES, editParameters);
            batcheEdit.EndEdit();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle == -1) return;
                if (e.Column.FieldName == "FV")
                {
                    DataRow dr = this.gridView1.GetDataRow(e.RowHandle);
                    IFieldInfo fi = dr["F"] as IFieldInfo;
                    if (fi != null)
                    {
                        string strobj = e.Value.ToString();
                        bool bRes = false;
                        switch (fi.FieldType)
                        {
                            case gviFieldType.gviFieldBlob:
                            case gviFieldType.gviFieldGeometry:
                            case gviFieldType.gviFieldUnknown:
                                break;
                            case gviFieldType.gviFieldFloat:
                                float f;
                                bRes = float.TryParse(strobj, out f);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            case gviFieldType.gviFieldDouble:
                                double d;
                                bRes = double.TryParse(strobj, out d);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            case gviFieldType.gviFieldFID:
                            case gviFieldType.gviFieldUUID:
                            case gviFieldType.gviFieldInt16:
                                Int16 i16;
                                bRes = Int16.TryParse(strobj, out i16);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            case gviFieldType.gviFieldInt32:
                                Int32 i32;
                                bRes = Int32.TryParse(strobj, out i32);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            case gviFieldType.gviFieldInt64:
                                Int64 i64;
                                bRes = Int64.TryParse(strobj, out i64);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break; ;
                            case gviFieldType.gviFieldString:
                                dr["FV"] = strobj;
                                break;
                            case gviFieldType.gviFieldDate:
                                DateTime dt;
                                bRes = DateTime.TryParse(strobj, out dt);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            default:
                                break;
                        }
                        this.gridView1.RefreshRow(e.RowHandle);
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
