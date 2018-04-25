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
using DF3DPipeCreateTool.Service;
using DF3DPipeCreateTool.Class;
using DevExpress.XtraEditors.Controls;
using DFDataConfig.Logic;
using Gvitech.CityMaker.Resource;
using DF3DPipeCreateTool.UC;

namespace DF3DEdit.Frm
{
    public class FrmAddPipeNode : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private ImageComboBoxEdit cmbStyle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private IContainer components;
        private ComboBoxEdit cmbClassify;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddPipeNode));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbClassify = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbStyle = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClassify.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmbClassify);
            this.layoutControl1.Controls.Add(this.cmbStyle);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(207, 453);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbClassify
            // 
            this.cmbClassify.Location = new System.Drawing.Point(65, 2);
            this.cmbClassify.Name = "cmbClassify";
            this.cmbClassify.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbClassify.Properties.DropDownRows = 10;
            this.cmbClassify.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbClassify.Size = new System.Drawing.Size(140, 22);
            this.cmbClassify.StyleController = this.layoutControl1;
            this.cmbClassify.TabIndex = 0;
            // 
            // cmbStyle
            // 
            this.cmbStyle.Location = new System.Drawing.Point(65, 28);
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStyle.Properties.DropDownRows = 10;
            this.cmbStyle.Properties.LargeImages = this.imageCollection1;
            this.cmbStyle.Size = new System.Drawing.Size(140, 50);
            this.cmbStyle.StyleController = this.layoutControl1;
            this.cmbStyle.TabIndex = 1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(5, 105);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(197, 343);
            this.gridControl1.TabIndex = 2;
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
            this.layoutControlGroup2,
            this.layoutControlItem2,
            this.layoutControlItem3});
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
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 80);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(207, 373);
            this.layoutControlGroup2.Text = "基本属性";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(201, 347);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbStyle;
            this.layoutControlItem2.CustomizationFormText = "选择样式：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(207, 54);
            this.layoutControlItem2.Text = "选择样式：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbClassify;
            this.layoutControlItem3.CustomizationFormText = "选择类型：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem3.Text = "选择类型：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // FrmAddPipeNode
            // 
            this.ClientSize = new System.Drawing.Size(207, 453);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Location = new System.Drawing.Point(10, 150);
            this.Name = "FrmAddPipeNode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "管点";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAddPipeNode_FormClosed);
            this.Load += new System.EventHandler(this.FrmAddPipeNode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbClassify.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }
        
        public FrmAddPipeNode()
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
        private void FrmAddPipeNode_Load(object sender, EventArgs e)
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
            LoadClassify();
            LoadProperty();
            LoadStyle();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Point);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void FrmAddPipeNode_FormClosed(object sender, FormClosedEventArgs e)
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
        private string _classifyName;
        private void LoadClassify()
        {
            try
            {
                this.cmbClassify.Properties.Items.Clear();

                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc != null)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc != null)
                    {
                        MajorClass mc = LogicDataStructureManage3D.Instance.GetMajorClassByDFFeatureClassID(fc.GuidString);
                        if (mc != null && mc.SubClasses != null)
                        {
                            this._classifyName = mc.ClassifyField;
                            foreach (SubClass sc in mc.SubClasses)
                            {
                                this.cmbClassify.Properties.Items.Add(sc);
                            }
                            if (this.cmbClassify.Properties.Items.Count > 0) this.cmbClassify.SelectedIndex = 0;
                            else this.cmbClassify.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private string _surfHName;
        private string _topHName;
        private string _bottomHName;
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
                        if (fi == null) continue;
                        if (fi.Name == fc.FidFieldName) continue;
                        if (fi.Name == this._classifyName) continue;
                        if (fi.Name.ToLower() == "groupid") continue;
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
                                if (fi.Name.ToLower() == "floorheigh")
                                {
                                    this._surfHName = fi.Name;
                                    dr["FV"] = "1";
                                }
                                if (fi.Name.ToLower() == "topheight")
                                {
                                    this._topHName = fi.Name;
                                    dr["FV"] = "1";
                                }
                                if (fi.Name.ToLower() == "bottomheig")
                                {
                                    this._bottomHName = fi.Name;
                                    dr["FV"] = "0";
                                }
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
                            if (fi.Name == this._classifyName) continue;
                            if (fi.Name.ToLower() == "groupid") continue;
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
                                    if (fi1.SystemName == "SurfHeight")
                                    {
                                        this._surfHName = fi1.Name;
                                        dr["FV"] = "1";
                                    }
                                    if (fi1.SystemName == "TopHeight")
                                    {
                                        this._topHName = fi1.Name;
                                        dr["FV"] = "1";
                                    }
                                    if (fi1.SystemName == "BottomHeight")
                                    {
                                        this._bottomHName = fi1.Name;
                                        dr["FV"] = "0";
                                    }
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

        private FacClassReg reg;
        private TopoClass tc;
        private void LoadStyle()
        {
            try
            {
                this.cmbStyle.Properties.Items.Clear();

                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc != null)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc != null)
                    {
                        reg = FacilityInfoService.GetFacClassRegByFeatureClassID(fc.Guid.ToString());
                        if (reg != null)
                        {
                            tc = FacilityInfoService.GetTopoClassByFacClassCode(reg.FacClassCode);
                            List<FacStyleClass> list1 = FacilityInfoService.GetFacStyleByFacClassCode(reg.FacClassCode);
                            if (list1 != null)
                            {
                                foreach (FacStyleClass fsc in list1)
                                {
                                    int imageIndex = this.imageCollection1.Images.Add(fsc.Thumbnail);
                                    ImageComboBoxItem item = new ImageComboBoxItem();
                                    item.Value = fsc;
                                    item.Description = fsc.ToString();
                                    item.ImageIndex = imageIndex;
                                    this.cmbStyle.Properties.Items.Add(item);
                                }
                                for (int i = 0; i < this.cmbStyle.Properties.Items.Count; i++)
                                {
                                    this.cmbStyle.Properties.Items[i].ImageIndex = i;
                                }
                                if (this.cmbStyle.Properties.Items.Count > 0) this.cmbStyle.SelectedIndex = 0;
                                else this.cmbStyle.SelectedIndex = -1;
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
                try
                {
                    AddRecord();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private Dictionary<DF3DFeatureClass, IRowBufferCollection> beforeRowBufferMap = new Dictionary<DF3DFeatureClass, IRowBufferCollection>();
        private void AddRecord()
        {
            try
            {
                this.beforeRowBufferMap.Clear();
                SelectCollection.Instance().Clear();
                if (reg == null || tc == null) return;
                FacStyleClass style = this.cmbStyle.EditValue as FacStyleClass;
                if (style == null) return;
                SubClass sc = this.cmbClassify.EditValue as SubClass;

                DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
                if (featureClassInfo == null) return;
                IFeatureClass fc = featureClassInfo.GetFeatureClass();
                if (fc == null) return;
                IResourceManager manager = fc.FeatureDataSet as IResourceManager;
                if (manager == null) return;
                IFeatureLayer fl = featureClassInfo.GetFeatureLayer();
                if (fl == null) return;
                IFieldInfoCollection fields = fc.GetFields();
                int indexOid = fields.IndexOf(fc.FidFieldName);
                if (indexOid == -1) return;
                int mpindex = fields.IndexOf(fl.GeometryFieldName);
                if (mpindex == -1) return;
                int indexShape = fields.IndexOf("Shape");
                if (indexShape == -1) return;
                int indexFootPrint = fields.IndexOf("FootPrint");
                if (indexFootPrint == -1) return;
                int indexStyleId = fields.IndexOf("StyleId");
                if (indexStyleId == -1) return;
                int indexFacilityId = fields.IndexOf("FacilityId");
                if (indexFacilityId == -1) return;

                IFieldInfo fiShape = fields.Get(indexShape);
                if (fiShape == null || fiShape.GeometryDef == null) return;

                IGeometry geo = this._drawTool.GetGeo();
                if (geo.GeometryType != gviGeometryType.gviGeometryPoint) return;
                IPoint pt = geo as IPoint;
                IPoint geoOut = pt.Clone2(fiShape.GeometryDef.VertexAttribute) as IPoint;
                if (fiShape.GeometryDef.HasZ) geoOut.SetCoords(pt.X, pt.Y, pt.Z, 0, 0);
                else geoOut.SetCoords(pt.X, pt.Y, 0, 0, 0);

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "1=1";
                filter.ResultBeginIndex = 0;
                filter.ResultLimit = 1;
                filter.PostfixClause = "ORDER BY " + fc.FidFieldName + " desc";
                IFdeCursor cursor = null;
                cursor = fc.Search(filter, false);
                IRowBuffer rowtemp = cursor.NextRow();
                int oid = 0;
                if (rowtemp != null)
                {
                    oid = int.Parse(rowtemp.GetValue(indexOid).ToString());
                }
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }

                IRowBufferFactory fac = new RowBufferFactory();
                IRowBuffer row = fac.CreateRowBuffer(fields);
                row.SetValue(indexOid, oid + 1);
                row.SetValue(indexShape, geoOut);
                row.SetValue(indexFootPrint, geoOut.Clone2(gviVertexAttribute.gviVertexAttributeNone));
                row.SetValue(indexStyleId, style.ObjectId);
                row.SetValue(indexFacilityId, BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant());
                if (sc != null)
                {
                    int indexClassify = fields.IndexOf(this._classifyName);
                    int indexGroupId = fields.IndexOf("GroupId");
                    if (indexClassify != -1 && indexGroupId != -1)
                    {
                        row.SetValue(indexClassify, sc.Name);
                        row.SetValue(indexGroupId, sc.GroupId);
                    }
                }
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
                Fac plf = new PipeNodeFac(reg, style, row, tc);

                IModelPoint mp = null;
                IModel finemodel = null;
                IModel simplemodel = null;
                string name = "";
                if (UCAuto3DCreate.RebuildModel(plf, style, out mp, out finemodel, out simplemodel, out name))
                {
                    if (finemodel == null || mp == null) return;
                    mp.ModelEnvelope = finemodel.Envelope;
                    row.SetValue(mpindex, mp);
                    //if (mc != null)
                    //{
                    //    if (indexClassify != -1 && indexGroupid != -1)
                    //    {

                    //    }
                    //}
                    bool bRes = false;
                    if (!string.IsNullOrEmpty(mp.ModelName))
                    {
                        if (!manager.ModelExist(mp.ModelName))
                        {
                            if (manager.AddModel(mp.ModelName, finemodel, simplemodel))
                            {
                                bRes = true;
                            }
                        }
                        else
                        {
                            if (manager.UpdateModel(mp.ModelName, finemodel) && manager.UpdateSimplifiedModel(mp.ModelName, simplemodel))
                            {
                                bRes = true;
                            }
                        }
                    }
                    if (!bRes) return;
                    IRowBufferCollection rowCol = new RowBufferCollection();
                    rowCol.Add(row);
                    beforeRowBufferMap[featureClassInfo] = rowCol;
                    UpdateDatabase();                    
                    app.Current3DMapControl.FeatureManager.RefreshFeatureClass(fc);
                }
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
                                    if (this._surfHName == fi.Name)
                                    {
                                        dr["FV"] = "1";
                                    }
                                    else if (this._topHName == fi.Name) 
                                    {
                                        dr["FV"] = "1";
                                    }
                                    else if (this._bottomHName == fi.Name)
                                    {
                                        dr["FV"] = "0";
                                    }
                                    else dr["FV"] = null;
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
