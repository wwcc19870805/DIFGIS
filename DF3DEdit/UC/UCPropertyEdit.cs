using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DEdit.Frm;
using DF3DData.Class;
using DF3DEdit.Service;
using DF3DEdit.Class;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;

namespace DF3DEdit.UC
{
    public class UCPropertyEdit : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private LabelControl labelControl3;
        private LabelControl labelControl2;
        private LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.labelControl3);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(266, 487);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(5, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(256, 377);
            this.gridControl1.TabIndex = 1;
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
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
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
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 465);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.StyleController = this.layoutControl1;
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "字段类型：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 447);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "字段别名：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 429);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "字段名称：";
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(266, 487);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "属性信息";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(266, 487);
            this.layoutControlGroup2.Text = "属性信息";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "字段信息";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 381);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(260, 80);
            this.layoutControlGroup3.Text = "字段信息";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labelControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(254, 18);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelControl2;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(254, 18);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.labelControl3;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 36);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(254, 18);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(260, 381);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // UCPropertyEdit
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCPropertyEdit";
            this.Size = new System.Drawing.Size(266, 487);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        private System.DateTime _time = System.DateTime.Now;
        private DataTable _dt;
        public UCPropertyEdit()
        {
            InitializeComponent();

            this._dt = new DataTable();
            this._dt.Columns.Add(new DataColumn("FN", Type.GetType("System.String")));
            this._dt.Columns.Add(new DataColumn("FV", Type.GetType("System.Object")));
            this._dt.Columns.Add(new DataColumn("F", Type.GetType("System.Object")));
            this.gridControl1.DataSource = this._dt;

            LoadProperty();
            InitData();
            SelectCollection.Instance().SelectionChangedEvent += new Delegate.SelectionChangedEventHandler(UCPropertyEdit_SelectionChangedEvent);
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
        private void InitData()
        {
            try
            {
                foreach (DataRow dr in this._dt.Rows)
                {
                    dr["FV"] = null;
                }
                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc == null) return;

                int count = SelectCollection.Instance().GetCount(false);
                if (count == 1)
                {
                    HashMap hm = SelectCollection.Instance().GetSelectGeometrys();
                    if (hm != null && hm.Count == 1)
                    {
                        IRowBufferCollection rowBufferCollection = hm[dffc] as IRowBufferCollection;
                        if (rowBufferCollection.Count == 1)
                        {
                            IRowBuffer rowBuffer = rowBufferCollection.Get(0);
                            foreach (DataRow dr in this._dt.Rows)
                            {
                                IFieldInfo fi = dr["F"] as IFieldInfo;
                                if (fi == null) continue;
                                int index = rowBuffer.FieldIndex(fi.Name);
                                if (index != -1)
                                {
                                    dr["FV"] = rowBuffer.GetValue(index);
                                }
                            }
                        }
                    }
                }
                this.gridView1.RefreshData();
            }
            catch (Exception ex) { }
        }

        private void UCPropertyEdit_SelectionChangedEvent()
        {
            InitData();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle == -1) return;
            DataRow dr = this.gridView1.GetDataRow(e.FocusedRowHandle);
            IFieldInfo fi = dr["F"] as IFieldInfo;
            this.labelControl1.Text = "字段名称：" + fi.Name;
            this.labelControl2.Text = "字段别名：" + fi.Alias;
            this.labelControl3.Text = "字段类型：" + fi.FieldType.ToString();
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
                    //if (this._connInfo != null && this._connInfo.GetFeatureClass().HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
                    //{
                    //    using (DateSettingDialog dateSettingDialog = new DateSettingDialog())
                    //    {
                    //        if (dateSettingDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    //        {
                    //            return;
                    //        }
                    //        this._time = dateSettingDialog.Time;
                    //    }
                    //}
                    SelectCollection.Instance().PropertyTableSelectionChanged(dr);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
