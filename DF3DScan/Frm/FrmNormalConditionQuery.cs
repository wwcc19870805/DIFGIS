using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DData.Class;
using DFDataConfig.Class;
using DFWinForms.LogicTree;
using Gvitech.CityMaker.FdeCore;
using DFCommon.Class;

namespace DF3DScan.Frm
{
    public class FrmNormalConditionQuery : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnCancel;
        private SimpleButton btnQuery;
        private ListBoxControl lbcCondition;
        private ListBoxControl lbcFieldValues;
        private TextEdit teFieldValue;
        private ComboBoxEdit cmbField;
        private ComboBoxEdit cmbLayer;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.lbcCondition = new DevExpress.XtraEditors.ListBoxControl();
            this.lbcFieldValues = new DevExpress.XtraEditors.ListBoxControl();
            this.teFieldValue = new DevExpress.XtraEditors.TextEdit();
            this.cmbField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcFieldValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFieldValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnQuery);
            this.layoutControl1.Controls.Add(this.lbcCondition);
            this.layoutControl1.Controls.Add(this.lbcFieldValues);
            this.layoutControl1.Controls.Add(this.teFieldValue);
            this.layoutControl1.Controls.Add(this.cmbField);
            this.layoutControl1.Controls.Add(this.cmbLayer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(332, 348);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(243, 324);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(150, 324);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(89, 22);
            this.btnQuery.StyleController = this.layoutControl1;
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查  询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lbcCondition
            // 
            this.lbcCondition.Items.AddRange(new object[] {
            "等于",
            "不等于",
            "相似于",
            "大于",
            "大于等于",
            "小于",
            "小于等于"});
            this.lbcCondition.Location = new System.Drawing.Point(17, 153);
            this.lbcCondition.Name = "lbcCondition";
            this.lbcCondition.Size = new System.Drawing.Size(93, 152);
            this.lbcCondition.StyleController = this.layoutControl1;
            this.lbcCondition.TabIndex = 2;
            this.lbcCondition.SelectedIndexChanged += new System.EventHandler(this.lbcCondition_SelectedIndexChanged);
            // 
            // lbcFieldValues
            // 
            this.lbcFieldValues.Location = new System.Drawing.Point(120, 205);
            this.lbcFieldValues.Name = "lbcFieldValues";
            this.lbcFieldValues.Size = new System.Drawing.Size(195, 100);
            this.lbcFieldValues.StyleController = this.layoutControl1;
            this.lbcFieldValues.TabIndex = 4;
            this.lbcFieldValues.SelectedIndexChanged += new System.EventHandler(this.lbcFieldValues_SelectedIndexChanged);
            // 
            // teFieldValue
            // 
            this.teFieldValue.Location = new System.Drawing.Point(120, 153);
            this.teFieldValue.Name = "teFieldValue";
            this.teFieldValue.Properties.NullValuePrompt = "(请输入一个值)";
            this.teFieldValue.Size = new System.Drawing.Size(195, 22);
            this.teFieldValue.StyleController = this.layoutControl1;
            this.teFieldValue.TabIndex = 3;
            // 
            // cmbField
            // 
            this.cmbField.Location = new System.Drawing.Point(77, 104);
            this.cmbField.Name = "cmbField";
            this.cmbField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbField.Properties.DropDownRows = 10;
            this.cmbField.Properties.NullValuePrompt = "(请选择一个字段)";
            this.cmbField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbField.Size = new System.Drawing.Size(241, 22);
            this.cmbField.StyleController = this.layoutControl1;
            this.cmbField.TabIndex = 1;
            this.cmbField.SelectedIndexChanged += new System.EventHandler(this.cmbField_SelectedIndexChanged);
            // 
            // cmbLayer
            // 
            this.cmbLayer.Location = new System.Drawing.Point(77, 34);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLayer.Properties.DropDownRows = 10;
            this.cmbLayer.Properties.NullValuePrompt = "(请选择一个图层)";
            this.cmbLayer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbLayer.Size = new System.Drawing.Size(241, 22);
            this.cmbLayer.StyleController = this.layoutControl1;
            this.cmbLayer.TabIndex = 0;
            this.cmbLayer.SelectedIndexChanged += new System.EventHandler(this.cmbLayer_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup6,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(332, 348);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "查询条件：";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlItem3,
            this.layoutControlGroup5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 70);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(332, 252);
            this.layoutControlGroup2.Text = "条件设置";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "字段值：";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(103, 26);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(205, 52);
            this.layoutControlGroup3.Text = "字段值：";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teFieldValue;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(199, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "字段值列表:";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup4.Location = new System.Drawing.Point(103, 78);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(205, 130);
            this.layoutControlGroup4.Text = "字段值列表:";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lbcFieldValues;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(199, 104);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbField;
            this.layoutControlItem3.CustomizationFormText = "查询字段：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(308, 26);
            this.layoutControlItem3.Text = "查询字段：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "查询条件";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(103, 182);
            this.layoutControlGroup5.Text = "查询条件";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lbcCondition;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(97, 156);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "图层选择";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(332, 70);
            this.layoutControlGroup6.Text = "图层选择";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbLayer;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(308, 26);
            this.layoutControlItem1.Text = "查询图层：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnQuery;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(148, 322);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(241, 322);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 322);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(148, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmNormalConditionQuery
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(332, 348);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmNormalConditionQuery";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "条件查询";
            this.Load += new System.EventHandler(this.FrmNormalConditionQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcFieldValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFieldValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        public FrmNormalConditionQuery()
        {
            InitializeComponent();
        }

        private void FrmNormalConditionQuery_Load(object sender, EventArgs e)
        {
            try
            {
                this.cmbLayer.Properties.Items.Clear();
                this.lbcCondition.SelectedIndex = 0; oper = " = ";
                List<DF3DFeatureClass> list = DF3DFeatureClassManager.Instance.GetAllFeatureClass();
                if (list != null)
                {
                    foreach (DF3DFeatureClass dffc in list)
                    {
                        FacilityClass fac = dffc.GetFacilityClass();
                        if (fac != null && (fac.Name == "PipeLine" || fac.Name == "PipeNode" || fac.Name == "PipeBuild" || fac.Name == "PipeBuild1")) break;
                        IBaseLayer treelayer = dffc.GetTreeLayer();
                        if (treelayer != null && treelayer.Visible == false) continue;
                        this.cmbLayer.Properties.Items.Add(dffc);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.cmbLayer.SelectedItem == null)
            {
                XtraMessageBox.Show("请选择一个图层", "提示");
                return;
            }
            if (this.cmbField.SelectedItem == null)
            {
                XtraMessageBox.Show("请选择一个字段", "提示");
                return;
            }
            query();
        }

        private ISpatialFilter _filter;
        public ISpatialFilter Filter
        {
            get { return this._filter; }
        }
        private DF3DFeatureClass _dffc;
        public DF3DFeatureClass DFFC
        {
            get { return this._dffc; }
        }
        private int _total;
        public int Total
        {
            get { return this._total; }
        }

        private void query()
        {
            if (this.cmbLayer.SelectedItem == null) return;
            DF3DFeatureClass dffc = this.cmbLayer.SelectedItem as DF3DFeatureClass;
            IFeatureClass fc = dffc.GetFeatureClass();
            if (fc == null) return;
            ISpatialFilter filter = new SpatialFilter();
            #region
            if (this.cmbField.SelectedItem == null) filter.WhereClause = "1=1";
            else
            {
                if (this.cmbField.SelectedItem is DFDataConfig.Class.FieldInfo)
                {
                    DFDataConfig.Class.FieldInfo fi = this.cmbField.SelectedItem as DFDataConfig.Class.FieldInfo;
                    if (oper == " like ") filter.WhereClause = fi.Name + " like '%" + this.teFieldValue.Text + "%'";
                    else
                    {
                        if (fi.DataType.ToLower() == "decimal")
                            filter.WhereClause = fi.Name + oper + this.teFieldValue.Text;
                        else
                            filter.WhereClause = fi.Name + oper + "'" + this.teFieldValue.Text + "'";
                    }
                }
                else filter.WhereClause = "1!=1";
            }
            #endregion
            int count = fc.GetCount(filter);
            if (count == 0)
            {
                XtraMessageBox.Show("查询结果为空", "提示");
                return;
            }
            this._dffc = dffc;
            this._filter = filter;
            this._total = count;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cmbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbField.Properties.Items.Clear();
            this.cmbField.SelectedIndex = -1;
            this.teFieldValue.Text = null;
            this.lbcFieldValues.Items.Clear();
            if (this.cmbLayer.SelectedItem == null) return;
            DF3DFeatureClass dffc = this.cmbLayer.SelectedItem as DF3DFeatureClass;
            IFeatureClass fc = dffc.GetFeatureClass();
            if (fc == null) return;
            FacilityClass fac = dffc.GetFacilityClass();
            if (fac == null)
            {
                IFieldInfoCollection fiCol = fc.GetFields();
                if (fiCol != null)
                {
                    for (int i = 0; i < fiCol.Count; i++)
                    {
                        IFieldInfo fi = fiCol.Get(i);
                        if (fi.FieldType == gviFieldType.gviFieldUnknown || fi.FieldType == gviFieldType.gviFieldGeometry || fi.FieldType == gviFieldType.gviFieldBlob) continue;
                        string dataType = "";
                        switch (fi.FieldType)
                        {
                            case gviFieldType.gviFieldDouble:
                            case gviFieldType.gviFieldFID:
                            case gviFieldType.gviFieldFloat:
                            case gviFieldType.gviFieldInt16:
                            case gviFieldType.gviFieldInt32:
                            case gviFieldType.gviFieldInt64:
                            case gviFieldType.gviFieldUUID:
                                dataType = "Decimal";
                                break;
                            case gviFieldType.gviFieldDate:
                            case gviFieldType.gviFieldString:
                                dataType = "String";
                                break;
                            default:
                                break;
                        }
                        DFDataConfig.Class.FieldInfo fi1 = new DFDataConfig.Class.FieldInfo(fi.Name, fi.Alias, "", "", true, false, false, dataType);
                        this.cmbField.Properties.Items.Add(fi1);
                    }
                }
            }
            else
            {
                List<DFDataConfig.Class.FieldInfo> list = fac.FieldInfoCollection;
                if (list != null)
                {
                    foreach (DFDataConfig.Class.FieldInfo fi in list)
                    {
                        if (!fi.CanQuery) continue;
                        this.cmbField.Properties.Items.Add(fi);
                    }
                }
            }
        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.lbcFieldValues.Items.Clear();
                this.teFieldValue.Text = "";
                if (this.cmbField.SelectedItem == null) return;
                if (this.cmbLayer.SelectedItem == null) return;
                DF3DFeatureClass dffc = this.cmbLayer.SelectedItem as DF3DFeatureClass;
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) return;
                string fieldName = "";
                if (this.cmbField.SelectedItem is DFDataConfig.Class.FieldInfo)
                {
                    fieldName = (this.cmbField.SelectedItem as DFDataConfig.Class.FieldInfo).Name;
                }
                if (fieldName == "") return;

                HashSet<string> list = new HashSet<string>();
                string cacheType = fc.GuidString + "_3D_" + fieldName;
                object objCache = CacheHelper.GetCache(cacheType);
                if (objCache != null && objCache is HashSet<string>)
                {
                    HashSet<string> temphs = objCache as HashSet<string>;
                    foreach (string str in temphs)
                    {
                        this.lbcFieldValues.Items.Add(str);
                    }
                    return;
                }
                #region
                IRowBuffer row = null;
                IFdeCursor cursor = null;
                try
                {
                    int index = fc.GetFields().IndexOf(fieldName);
                    bool bString = true;
                    if (index != -1)
                    {
                        IFieldInfo fi = fc.GetFields().Get(index);
                        switch (fi.FieldType)
                        {
                            case gviFieldType.gviFieldDouble:
                            case gviFieldType.gviFieldFID:
                            case gviFieldType.gviFieldFloat:
                            case gviFieldType.gviFieldInt16:
                            case gviFieldType.gviFieldInt32:
                            case gviFieldType.gviFieldInt64:
                            case gviFieldType.gviFieldUUID:
                                bString = false;
                                break;
                            case gviFieldType.gviFieldDate:
                            case gviFieldType.gviFieldString:
                                bString = true;
                                break;
                            default:
                                break;
                        }
                    }
                    IQueryFilter filter = new QueryFilter();
                    filter.SubFields = fieldName;
                    filter.ResultBeginIndex = 0;
                    filter.ResultLimit = 1;
                    while (true)
                    {
                        string strTempClause = fieldName + " is not null and ";
                        string fClause = strTempClause;
                        foreach (string strtemp in list)
                        {
                            if (bString) fClause += fieldName + " <> '" + strtemp + "' and ";
                            else fClause += fieldName + " <> " + strtemp + " and ";
                        }
                        fClause = fClause.Substring(0, fClause.Length - 5);
                        filter.WhereClause = fClause;

                        cursor = fc.Search(filter, true);
                        if ((row = cursor.NextRow()) != null)
                        {
                            if (row.IsNull(0)) break;
                            object temp = row.GetValue(0);
                            string strtemp =  temp.ToString();                                           
                            if (temp != null)
                            {
                                list.Add(strtemp);
                                if (list.Count > 10)
                                {
                                    break;// 列举10个
                                }
                            }
                            if (cursor != null)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                cursor = null;
                            }
                        }
                        else break;
                    }
                    CacheHelper.SetCache(cacheType, list);
                }
                catch (Exception Exception)
                {
                }
                finally
                {
                    if (cursor != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                        cursor = null;
                    }
                    if (row != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                        row = null;
                    }
                }
                #endregion
                foreach (string str in list)
                {
                    this.lbcFieldValues.Items.Add(str);
                }
            }
            catch (Exception Exception)
            {
            }
        }

        private string oper;
        private void lbcCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.lbcCondition.Text)
            {
                case "等于":
                    oper = " = ";
                    break;
                case "不等于":
                    oper = " != ";
                    break;
                case "相似于":
                    oper = " like ";
                    break;
                case "大于":
                    oper = " > ";
                    break;
                case "大于等于":
                    oper = " >= ";
                    break;
                case "小于":
                    oper = " < ";
                    break;
                case "小于等于":
                    oper = " <= ";
                    break;
            }
        }

        private void lbcFieldValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.teFieldValue.Text = this.lbcFieldValues.Text;
        }

    }
}
