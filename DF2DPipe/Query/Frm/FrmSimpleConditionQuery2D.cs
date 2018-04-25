using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DF2DPipe.Frm;
using DFDataConfig.Class;
using DF2DData.Class;
using ESRI.ArcGIS.Geodatabase;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DFWinForms.Class;


namespace DF2DPipe.Frm
{
    public class FrmSimpleConditionQuery2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private GroupBox groupBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTreeList.TreeList treeLayer;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private IContainer components;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private SimpleButton btnQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private ComboBoxEdit cbValue;
        private ComboBoxEdit cbOperator;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private PictureEdit pictureEdit1;
        private LabelControl labelControl2;
        private ComboBoxEdit cbProperty;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;

        private List<DF2DFeatureClass> selectFeatureClass = new List<DF2DFeatureClass>();

        private Dictionary<DF2DFeatureClass,DataTable> queryDTByLayer = new Dictionary<DF2DFeatureClass,DataTable>();

        private Dictionary<string, string> aliaNameAndName = new Dictionary<string, string>();

        public Dictionary<DF2DFeatureClass,DataTable> QueryDTByLayer
        {
            get{return queryDTByLayer;}
        }

        public List<DF2DFeatureClass> SelectFeatureClass
        {
            get { return this.selectFeatureClass; }
        }

        public ComboBoxEdit CbProperty
        {
            get { return this.cbProperty; }
        }

        public ComboBoxEdit CbValue
        {
            get { return this.cbValue; }
        }

        public ComboBoxEdit CbOperator
        {
            get { return this.cbOperator; }
        }

        public FrmSimpleConditionQuery2D()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSimpleConditionQuery2D));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbValue = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbOperator = new DevExpress.XtraEditors.ComboBoxEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.treeLayer = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cbProperty = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbValue);
            this.layoutControl1.Controls.Add(this.cbOperator);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnQuery);
            this.layoutControl1.Controls.Add(this.treeLayer);
            this.layoutControl1.Controls.Add(this.groupBox1);
            this.layoutControl1.Controls.Add(this.cbProperty);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(391, 370);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbValue
            // 
            this.cbValue.Location = new System.Drawing.Point(202, 307);
            this.cbValue.Name = "cbValue";
            this.cbValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbValue.Size = new System.Drawing.Size(184, 22);
            this.cbValue.StyleController = this.layoutControl1;
            this.cbValue.TabIndex = 12;
            // 
            // cbOperator
            // 
            this.cbOperator.EditValue = "=";
            this.cbOperator.Location = new System.Drawing.Point(202, 264);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbOperator.Properties.Items.AddRange(new object[] {
            "=",
            "!=",
            ">",
            ">=",
            "<",
            "<=",
            "LIKE"});
            this.cbOperator.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbOperator.Size = new System.Drawing.Size(184, 22);
            this.cbOperator.StyleController = this.layoutControl1;
            this.cbOperator.TabIndex = 11;
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "管线";
            this.radioGroup1.Location = new System.Drawing.Point(202, 103);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("管线", "管线"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("管点", "管点")});
            this.radioGroup1.Size = new System.Drawing.Size(184, 71);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 9;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(295, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(199, 336);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(92, 22);
            this.btnQuery.StyleController = this.layoutControl1;
            this.btnQuery.TabIndex = 8;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // treeLayer
            // 
            this.treeLayer.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeLayer.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeLayer.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.NodeObject,
            this.NodeType});
            this.treeLayer.Location = new System.Drawing.Point(5, 103);
            this.treeLayer.Name = "treeLayer";
            this.treeLayer.OptionsView.ShowCheckBoxes = true;
            this.treeLayer.OptionsView.ShowColumns = false;
            this.treeLayer.OptionsView.ShowIndicator = false;
            this.treeLayer.OptionsView.ShowVertLines = false;
            this.treeLayer.Size = new System.Drawing.Size(187, 252);
            this.treeLayer.StateImageList = this.imageCollection1;
            this.treeLayer.TabIndex = 6;
            this.treeLayer.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeLayer_AfterCheckNode);
            this.treeLayer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeLayer_MouseUp);
            // 
            // NodeName
            // 
            this.NodeName.Caption = "名称";
            this.NodeName.FieldName = "NodeName";
            this.NodeName.MinWidth = 49;
            this.NodeName.Name = "NodeName";
            this.NodeName.OptionsColumn.AllowEdit = false;
            this.NodeName.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.NodeName.Visible = true;
            this.NodeName.VisibleIndex = 0;
            // 
            // NodeObject
            // 
            this.NodeObject.Caption = "对象";
            this.NodeObject.FieldName = "NodeObject";
            this.NodeObject.Name = "NodeObject";
            this.NodeObject.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Object;
            // 
            // NodeType
            // 
            this.NodeType.Caption = "类型";
            this.NodeType.FieldName = "NodeType";
            this.NodeType.Name = "NodeType";
            this.NodeType.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Integer;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "FeatureLayer_line.png");
            this.imageCollection1.Images.SetKeyName(1, "FeatureLayer_point.png");
            this.imageCollection1.Images.SetKeyName(2, "FeatureLayer_polygon.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureEdit1);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 74);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(99, 19);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new System.Drawing.Size(48, 48);
            this.pictureEdit1.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(178, 36);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 17);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "简单条件查询";
            // 
            // cbProperty
            // 
            this.cbProperty.Location = new System.Drawing.Point(202, 221);
            this.cbProperty.Name = "cbProperty";
            this.cbProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbProperty.Properties.PopupSizeable = true;
            this.cbProperty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbProperty.Size = new System.Drawing.Size(184, 22);
            this.cbProperty.StyleController = this.layoutControl1;
            this.cbProperty.TabIndex = 10;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.emptySpaceItem5,
            this.layoutControlGroup5,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(391, 370);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupBox1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(391, 78);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "图层数";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 78);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(197, 282);
            this.layoutControlGroup3.Text = "图层数";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.treeLayer;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(191, 256);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup4.Location = new System.Drawing.Point(197, 78);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(194, 101);
            this.layoutControlGroup4.Text = "查询类型";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.radioGroup1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(188, 75);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 360);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(391, 10);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "条件设置";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup5.Location = new System.Drawing.Point(197, 179);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(194, 155);
            this.layoutControlGroup5.Text = "条件设置";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cbProperty;
            this.layoutControlItem6.CustomizationFormText = "查询属性";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(188, 43);
            this.layoutControlItem6.Text = "查询属性";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cbOperator;
            this.layoutControlItem7.CustomizationFormText = "运算符";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 43);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(188, 43);
            this.layoutControlItem7.Text = "运算符";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cbValue;
            this.layoutControlItem8.CustomizationFormText = "属性值";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 86);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(188, 43);
            this.layoutControlItem8.Text = "属性值";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnQuery;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(197, 334);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(96, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCancel;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(293, 334);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "图层树";
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 68);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(218, 339);
            this.layoutControlGroup2.Text = "图层树";
            // 
            // FrmSimpleConditionQuery2D
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(391, 370);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmSimpleConditionQuery2D";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "简单条件查询";
            this.Load += new System.EventHandler(this.FrmSimpleConditionQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void DoQuery()
        {
            string property = this.cbProperty.SelectedItem.ToString();
            string propName = null;
            foreach (KeyValuePair<string, string> v in aliaNameAndName)
            {
                if (v.Key == property)
                    propName = v.Value;
            }
            string value = this.CbValue.Text;
            string opera = this.CbOperator.SelectedItem.ToString();

            if (this.selectFeatureClass != null && propName != null && value != null && opera != null)
            {
                foreach (DF2DFeatureClass dffc in selectFeatureClass)
                {

                    DF2DPipe.Class.Query query = new DF2DPipe.Class.Query();

                    DataTable dt = query.DoQuery(propName, opera, value, dffc);
                    queryDTByLayer.Add(dffc, dt);
                }
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                WaitForm.Start("正在查询...", "请稍后", false);
                DoQuery();
                WaitForm.Stop();
                //if (_dt == null || _dt.Rows.Count == 0)
                //{
                //    XtraMessageBox.Show("查询结果为空！", "提示");
                //    return;
                //}
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("查询出错！", "提示");
                WaitForm.Stop();
            }


        }

        private void LoadData()
        {
            this.treeLayer.Nodes.Clear();
            List<DF2DFeatureClass> list;
            if (this.radioGroup1.SelectedIndex == 0)
            {
                list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
                if (list == null) return;
                foreach (DF2DFeatureClass dffc in list)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null) continue;
                    TreeListNode node = this.treeLayer.AppendNode(new object[] { fc.AliasName, dffc }, null);
                    node.StateImageIndex = 0;
                }
            }
            else
            {
                list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
                if (list == null) return;
                foreach (DF2DFeatureClass dffc in list)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null) continue;
                    TreeListNode node = this.treeLayer.AppendNode(new object[] { fc.AliasName, dffc }, null);
                    node.StateImageIndex = 1;
                }
            }
            
            
            this.treeLayer.Refresh();


        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FrmSimpleConditionQuery_Load(object sender, EventArgs e)   
        {
            List<DF2DFeatureClass> list;
            if (this.radioGroup1.SelectedIndex == 0)
            {
                list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
                if (list == null) return;
                foreach (DF2DFeatureClass dffc in list)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null) continue;
                    TreeListNode node = this.treeLayer.AppendNode(new object[] { fc.AliasName, dffc }, null);
                    node.StateImageIndex = 0;
                }
            }
            else
            {
                list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
                if (list == null) return;
                foreach (DF2DFeatureClass dffc in list)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null) continue;
                    TreeListNode node = this.treeLayer.AppendNode(new object[] { fc.AliasName, dffc }, null);
                    node.StateImageIndex = 1;
                }
            }

            
            this.treeLayer.Refresh();
        }

        private void cbProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void treeLayer_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left && System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.None)
                {
                    TreeListHitInfo treeListHitInfo = this.treeLayer.CalcHitInfo(e.Location);
                    if (treeListHitInfo.HitInfoType == HitInfoType.Cell)
                    {
                        this.treeLayer.SetFocusedNode(treeListHitInfo.Node);
                    }
                    if (treeListHitInfo.Node != null)
                    {
                        object obj = treeListHitInfo.Node.GetValue("NodeObject");
                        if (obj == null) return;
                        //
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void treeLayer_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            this.cbProperty.Properties.Items.Clear();
            this.selectFeatureClass.Clear();
            try
            {
                List<string> list = new List<string>();
                List<TreeListNode> treeListNodes = this.treeLayer.GetAllCheckedNodes();
                foreach (TreeListNode node in treeListNodes)
                {
                    
                    object obj = node.GetValue("NodeObject");
                    if (obj is DF2DFeatureClass)
                    {
                        DF2DFeatureClass dffc = obj as DF2DFeatureClass;
                        IFeatureClass fc = dffc.GetFeatureClass();

                        selectFeatureClass.Add(dffc);
                        FacilityClass fac = dffc.GetFacilityClass();

                        if (fc == null || dffc == null) continue;
                        IFields fiCol = fc.Fields;
                        if (aliaNameAndName.Count > 0 )
                        {
                            for (int i = 0; i < fiCol.FieldCount; i++)
                            {
                                string str = fiCol.get_Field(i).AliasName;
                                string str2 = fiCol.get_Field(i).Name;
                                //aliaNameAndName.Add(str, str2);
                                FieldInfo fi = fac.GetFieldInfoByName(str2);
                                if (fi == null) continue;
                                else if (fi.CanQuery)
                                    list.Add(str);



                            }
                            

                        }
                        else
                        {
                            for (int i = 0; i < fiCol.FieldCount; i++)
                            {
                                string str = fiCol.get_Field(i).AliasName;
                                string str2 = fiCol.get_Field(i).Name;
                                aliaNameAndName.Add(str, str2);
                                FieldInfo fi = fac.GetFieldInfoByName(str2);
                                if (fi == null) continue;
                                else if (fi.CanQuery)
                                    list.Add(str);



                            }
                        }
                        

                    }
                    
                }
                if (list.Count > 0)
                {
                    foreach (string str2 in from v in list.Distinct<string>() orderby v select v)
                    {
                        if (!string.IsNullOrEmpty(str2))
                        {
                            this.cbProperty.Properties.Items.Add(str2);
                        }
                    }
                }
            }

                
            catch (System.Exception ex)
            {
            	
            }
            //try
            //{
            //    List<string> list = new List<string>();
            //    foreach (TreeListNode node in this.treeLayer.GetAllCheckedNodes())
            //    {
            //        object obj = node.GetValue("NodeObject");
            //        if (obj is DF2DFeatureClass)
            //        {
            //            DF2DFeatureClass dffc = obj as DF2DFeatureClass;
            //            IFeatureClass fc = dffc.GetFeatureClass();
            //            FacilityClass fac = dffc.GetFacilityClass();
            //            if (fc == null || fac == null) continue;
            //            DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName();
                        
            //            IFields fiCol = fc.Fields;
            //            int index = fiCol.FindField(fi.Name);
            //            if (index < 0) continue;
            //            IField pField = fiCol.get_Field(index);
            //            IQueryFilter filter = new QueryFilterClass();
            //            filter.SubFields = pField.Name;

            //            IFeatureCursor ftCursor = fc.Search(filter, true);
            //            IFeature pfeature = ftCursor.NextFeature();

            //            while (pfeature != null)
            //            {
            //                string temp = pfeature.get_Value(index).ToString();
            //                if (temp != null) list.Add(temp);
            //                pfeature = ftCursor.NextFeature();
            //            }


            //        }

            //    }
               

            //}
            //catch (System.Exception ex)
            //{

            //}
        }

    }
}
