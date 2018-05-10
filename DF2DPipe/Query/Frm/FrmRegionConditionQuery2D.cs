using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DF2DData.Class;
using DFWinForms.Class;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;

namespace DF2DPipe.Frm
{
    
    public class FrmRegionConditionQuery2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ListBoxControl listBoxControlValues;
        private ListBoxControl listBoxControlProperty;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraTreeList.TreeList treeLayer;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private SimpleButton btnQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private RichTextBox teExpression;
        private CheckEdit ceSQL;
        private SimpleButton btnClear;
        private SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private SimpleButton btnBracket;
        private SimpleButton btnAnd;
        private SimpleButton btnOr;
        private SimpleButton btnEqual;
        private SimpleButton btnLess;
        private SimpleButton btnGreater;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.ComponentModel.IContainer components;
        private SimpleButton btnLike;
        private SimpleButton btnLessEqual;
        private SimpleButton btnGreaterEqual;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;

        private List<DF2DFeatureClass> selectFeatureClass = new List<DF2DFeatureClass>();

        private Dictionary<DF2DFeatureClass, DataTable> queryDTByLayer = new Dictionary<DF2DFeatureClass, DataTable>();

        private Dictionary<string, string> aliaNameAndName = new Dictionary<string, string>();

        private Dictionary<DF2DFeatureClass, IFeatureClass> spatialQueryLine = new Dictionary<DF2DFeatureClass, IFeatureClass>();
        private Dictionary<DF2DFeatureClass, IFeatureClass> spatialQueryNode = new Dictionary<DF2DFeatureClass, IFeatureClass>();


        public Dictionary<DF2DFeatureClass, DataTable> QueryDTByLayer
        {
            get { return queryDTByLayer; }
        }

        public List<DF2DFeatureClass> SelectFeatureClass
        {
            get { return this.selectFeatureClass; }
        }

        

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegionConditionQuery2D));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnLike = new DevExpress.XtraEditors.SimpleButton();
            this.btnLessEqual = new DevExpress.XtraEditors.SimpleButton();
            this.btnGreaterEqual = new DevExpress.XtraEditors.SimpleButton();
            this.btnBracket = new DevExpress.XtraEditors.SimpleButton();
            this.btnAnd = new DevExpress.XtraEditors.SimpleButton();
            this.btnOr = new DevExpress.XtraEditors.SimpleButton();
            this.btnEqual = new DevExpress.XtraEditors.SimpleButton();
            this.btnLess = new DevExpress.XtraEditors.SimpleButton();
            this.btnGreater = new DevExpress.XtraEditors.SimpleButton();
            this.teExpression = new System.Windows.Forms.RichTextBox();
            this.ceSQL = new DevExpress.XtraEditors.CheckEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.treeLayer = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.listBoxControlValues = new DevExpress.XtraEditors.ListBoxControl();
            this.listBoxControlProperty = new DevExpress.XtraEditors.ListBoxControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceSQL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnLike);
            this.layoutControl1.Controls.Add(this.btnLessEqual);
            this.layoutControl1.Controls.Add(this.btnGreaterEqual);
            this.layoutControl1.Controls.Add(this.btnBracket);
            this.layoutControl1.Controls.Add(this.btnAnd);
            this.layoutControl1.Controls.Add(this.btnOr);
            this.layoutControl1.Controls.Add(this.btnEqual);
            this.layoutControl1.Controls.Add(this.btnLess);
            this.layoutControl1.Controls.Add(this.btnGreater);
            this.layoutControl1.Controls.Add(this.teExpression);
            this.layoutControl1.Controls.Add(this.ceSQL);
            this.layoutControl1.Controls.Add(this.btnClear);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnQuery);
            this.layoutControl1.Controls.Add(this.treeLayer);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.listBoxControlValues);
            this.layoutControl1.Controls.Add(this.listBoxControlProperty);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(506, 443);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(396, 77);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(76, 22);
            this.btnLike.StyleController = this.layoutControl1;
            this.btnLike.TabIndex = 23;
            this.btnLike.Text = "like";
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnLessEqual
            // 
            this.btnLessEqual.Location = new System.Drawing.Point(307, 51);
            this.btnLessEqual.Name = "btnLessEqual";
            this.btnLessEqual.Size = new System.Drawing.Size(85, 22);
            this.btnLessEqual.StyleController = this.layoutControl1;
            this.btnLessEqual.TabIndex = 22;
            this.btnLessEqual.Text = "<=";
            this.btnLessEqual.Click += new System.EventHandler(this.btnLessEqual_Click);
            // 
            // btnGreaterEqual
            // 
            this.btnGreaterEqual.Location = new System.Drawing.Point(396, 25);
            this.btnGreaterEqual.Name = "btnGreaterEqual";
            this.btnGreaterEqual.Size = new System.Drawing.Size(76, 22);
            this.btnGreaterEqual.StyleController = this.layoutControl1;
            this.btnGreaterEqual.TabIndex = 21;
            this.btnGreaterEqual.Text = ">=";
            this.btnGreaterEqual.Click += new System.EventHandler(this.btnGreaterEqual_Click);
            // 
            // btnBracket
            // 
            this.btnBracket.Location = new System.Drawing.Point(396, 51);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new System.Drawing.Size(76, 22);
            this.btnBracket.StyleController = this.layoutControl1;
            this.btnBracket.TabIndex = 20;
            this.btnBracket.Text = "()";
            this.btnBracket.Click += new System.EventHandler(this.btnBracket_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(307, 77);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(85, 22);
            this.btnAnd.StyleController = this.layoutControl1;
            this.btnAnd.TabIndex = 19;
            this.btnAnd.Text = "and";
            this.btnAnd.Click += new System.EventHandler(this.btnAnd_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(227, 77);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(76, 22);
            this.btnOr.StyleController = this.layoutControl1;
            this.btnOr.TabIndex = 18;
            this.btnOr.Text = "or";
            this.btnOr.Click += new System.EventHandler(this.btnOr_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(227, 25);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(76, 22);
            this.btnEqual.StyleController = this.layoutControl1;
            this.btnEqual.TabIndex = 17;
            this.btnEqual.Text = "=";
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // btnLess
            // 
            this.btnLess.Location = new System.Drawing.Point(227, 51);
            this.btnLess.Name = "btnLess";
            this.btnLess.Size = new System.Drawing.Size(76, 22);
            this.btnLess.StyleController = this.layoutControl1;
            this.btnLess.TabIndex = 16;
            this.btnLess.Text = "<";
            this.btnLess.Click += new System.EventHandler(this.btnLess_Click);
            // 
            // btnGreater
            // 
            this.btnGreater.Location = new System.Drawing.Point(307, 25);
            this.btnGreater.Name = "btnGreater";
            this.btnGreater.Size = new System.Drawing.Size(85, 22);
            this.btnGreater.StyleController = this.layoutControl1;
            this.btnGreater.TabIndex = 15;
            this.btnGreater.Text = ">";
            this.btnGreater.Click += new System.EventHandler(this.btnGreater_Click);
            // 
            // teExpression
            // 
            this.teExpression.Enabled = false;
            this.teExpression.Location = new System.Drawing.Point(193, 298);
            this.teExpression.Name = "teExpression";
            this.teExpression.Size = new System.Drawing.Size(308, 114);
            this.teExpression.TabIndex = 14;
            this.teExpression.Text = "";
            // 
            // ceSQL
            // 
            this.ceSQL.Location = new System.Drawing.Point(393, 246);
            this.ceSQL.Name = "ceSQL";
            this.ceSQL.Properties.Caption = "编辑SQL";
            this.ceSQL.Size = new System.Drawing.Size(108, 19);
            this.ceSQL.StyleController = this.layoutControl1;
            this.ceSQL.TabIndex = 13;
            this.ceSQL.CheckedChanged += new System.EventHandler(this.ceSQL_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(220, 246);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 22);
            this.btnClear.StyleController = this.layoutControl1;
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(359, 419);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(145, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(190, 419);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(165, 22);
            this.btnQuery.StyleController = this.layoutControl1;
            this.btnQuery.TabIndex = 9;
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
            this.treeLayer.Location = new System.Drawing.Point(5, 63);
            this.treeLayer.Name = "treeLayer";
            this.treeLayer.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeLayer.OptionsView.ShowCheckBoxes = true;
            this.treeLayer.OptionsView.ShowColumns = false;
            this.treeLayer.OptionsView.ShowIndicator = false;
            this.treeLayer.OptionsView.ShowVertLines = false;
            this.treeLayer.Size = new System.Drawing.Size(178, 227);
            this.treeLayer.StateImageList = this.imageCollection1;
            this.treeLayer.TabIndex = 7;
            this.treeLayer.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeLayer_AfterCheckNode);
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
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "管线";
            this.radioGroup1.Location = new System.Drawing.Point(5, 25);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("管线", "管线"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("管点", "管点")});
            this.radioGroup1.Size = new System.Drawing.Size(178, 34);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 10;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // listBoxControlValues
            // 
            this.listBoxControlValues.Location = new System.Drawing.Point(193, 129);
            this.listBoxControlValues.Name = "listBoxControlValues";
            this.listBoxControlValues.Size = new System.Drawing.Size(308, 87);
            this.listBoxControlValues.StyleController = this.layoutControl1;
            this.listBoxControlValues.TabIndex = 5;
            this.listBoxControlValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxControlValues_MouseDoubleClick);
            // 
            // listBoxControlProperty
            // 
            this.listBoxControlProperty.Location = new System.Drawing.Point(5, 320);
            this.listBoxControlProperty.Name = "listBoxControlProperty";
            this.listBoxControlProperty.Size = new System.Drawing.Size(178, 118);
            this.listBoxControlProperty.StyleController = this.layoutControl1;
            this.listBoxControlProperty.TabIndex = 4;
            this.listBoxControlProperty.SelectedIndexChanged += new System.EventHandler(this.listBoxControlProperty_SelectedIndexChanged);
            this.listBoxControlProperty.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxControlProperty_MouseDoubleClick);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.layoutControlGroup6,
            this.layoutControlGroup7,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(506, 443);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "图层数";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(188, 295);
            this.layoutControlGroup2.Text = "图层数";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.radioGroup1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(182, 38);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.treeLayer;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(182, 231);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "字段";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 295);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(188, 148);
            this.layoutControlGroup3.Text = "字段";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.listBoxControlProperty;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(182, 122);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "操作";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup4.Location = new System.Drawing.Point(188, 221);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(318, 52);
            this.layoutControlGroup4.Text = "操作";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnClear;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(27, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(95, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.ceSQL;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(200, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(112, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(122, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(78, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(27, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "表达式";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9});
            this.layoutControlGroup5.Location = new System.Drawing.Point(188, 273);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(318, 144);
            this.layoutControlGroup5.Text = "表达式";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teExpression;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(312, 118);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "字段值";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup6.Location = new System.Drawing.Point(188, 104);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(318, 117);
            this.layoutControlGroup6.Text = "字段值";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.listBoxControlValues;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(312, 91);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.CustomizationFormText = "操作符";
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.emptySpaceItem3,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem12,
            this.layoutControlItem16,
            this.layoutControlItem11,
            this.emptySpaceItem6});
            this.layoutControlGroup7.Location = new System.Drawing.Point(188, 0);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(318, 104);
            this.layoutControlGroup7.Text = "操作符";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnGreater;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(114, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(283, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(29, 78);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(29, 78);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(29, 78);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btnOr;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(34, 52);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnAnd;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(114, 52);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.btnBracket;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(203, 26);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.btnLessEqual;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(114, 26);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.btnLike;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(203, 52);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem18.Text = "layoutControlItem18";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btnEqual;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(34, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.btnGreaterEqual;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(203, 0);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnLess;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(34, 26);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem6.MaxSize = new System.Drawing.Size(34, 78);
            this.emptySpaceItem6.MinSize = new System.Drawing.Size(34, 78);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(34, 78);
            this.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnQuery;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(188, 417);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(169, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(357, 417);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(149, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // FrmRegionConditionQuery2D
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(506, 443);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmRegionConditionQuery2D";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "复合条件查询";
            this.Load += new System.EventHandler(this.FrmCompoundConditionQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceSQL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

            }
        //public FrmRegionConditionQuery2D()
        //{
        //    InitializeComponent();
        //}
        public FrmRegionConditionQuery2D(Dictionary<DF2DFeatureClass,IFeatureClass> sql,Dictionary<DF2DFeatureClass,IFeatureClass> sqn)
        {
            this.spatialQueryLine = sql;
            this.spatialQueryNode = sqn;
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FrmCompoundConditionQuery_Load(object sender, EventArgs e)
        {
            //List<DF2DFeatureClass> list;
            if (this.radioGroup1.SelectedIndex == 0)
            {
                if (spatialQueryLine == null) return;
                foreach (KeyValuePair<DF2DFeatureClass,IFeatureClass> s in spatialQueryLine)
                {

                    ESRI.ArcGIS.Geodatabase.IFeatureClass fc = s.Key.GetFeatureClass();
                    if (fc == null) continue;
                    TreeListNode node = this.treeLayer.AppendNode(new object[] { fc.AliasName, s.Key }, null);
                    node.StateImageIndex = 0;
                }
                
            }
            else
            {
                if (spatialQueryNode == null) return;
                foreach (KeyValuePair<DF2DFeatureClass,IFeatureClass> v in spatialQueryNode)
                {
                    ESRI.ArcGIS.Geodatabase.IFeatureClass fc = v.Key.GetFeatureClass();
                    if (fc == null) continue;
                    TreeListNode node = this.treeLayer.AppendNode(new object[] { fc.AliasName, v.Key }, null);
                    node.StateImageIndex = 1;
                }
               
            }


            this.treeLayer.Refresh();
        }


        private void LoadData()
        {
            this.treeLayer.Nodes.Clear();
            if (this.radioGroup1.SelectedIndex == 0)
            {
                if (spatialQueryLine == null) return;
                foreach (KeyValuePair<DF2DFeatureClass, IFeatureClass> s in spatialQueryLine)
                {

                    ESRI.ArcGIS.Geodatabase.IFeatureClass fc = s.Key.GetFeatureClass();
                    if (fc == null) continue;
                    TreeListNode node = this.treeLayer.AppendNode(new object[] { fc.AliasName, s.Key }, null);
                    node.StateImageIndex = 0;
                }

            }
            else
            {
                if (spatialQueryNode == null) return;
                foreach (KeyValuePair<DF2DFeatureClass, IFeatureClass> v in spatialQueryNode)
                {
                    ESRI.ArcGIS.Geodatabase.IFeatureClass fc = v.Key.GetFeatureClass();
                    if (fc == null) continue;
                    TreeListNode node = this.treeLayer.AppendNode(new object[] { fc.AliasName, v.Key }, null);
                    node.StateImageIndex = 1;
                }

            }


            this.treeLayer.Refresh();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
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
        private void DoQuery()
        {
            if (fieldName == null) return;
            string whereClause = teExpression.Text;
            if (whereClause == null) return;
            if (this.selectFeatureClass != null&&this.radioGroup1.SelectedIndex == 0)
            {
                foreach (DF2DFeatureClass dffc in selectFeatureClass)
                {
                    IFeatureClass fc = null;
                    foreach (KeyValuePair<DF2DFeatureClass, IFeatureClass> k in spatialQueryLine)
                    {
                        if (k.Key == dffc)
                        {
                            fc = k.Value;
                        }
                    }

                    DF2DPipe.Class.Query query = new DF2DPipe.Class.Query();

                    DataTable dt = query.DoQuery(whereClause, fc,dffc);
                    queryDTByLayer.Add(dffc, dt);
                }
            }
            else
            {
                foreach (DF2DFeatureClass dffc in selectFeatureClass)
                {
                    IFeatureClass fc = null;
                    foreach (KeyValuePair<DF2DFeatureClass, IFeatureClass> k in spatialQueryNode)
                    {
                        if (k.Key == dffc)
                        {
                            fc = k.Value;
                        }
                    }

                    DF2DPipe.Class.Query query = new DF2DPipe.Class.Query();

                    DataTable dt = query.DoQuery(whereClause, fc, dffc);
                    queryDTByLayer.Add(dffc, dt);
                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.teExpression.Clear();
            this.expression = null;
        }

        private void ceSQL_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void listBoxControlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxControlProperty.Items.Count == 0) return;
            this.listBoxControlValues.Items.Clear();
            string currentAliaName = this.listBoxControlProperty.SelectedItem.ToString();
            string currentName = null;
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> v in aliaNameAndName)
            {
                if (v.Key == currentAliaName)
                {
                    currentName = v.Value;
                }
            }
            if (currentName == null) return;

            foreach (DF2DFeatureClass dffc in this.selectFeatureClass)
            {

                IFeatureClass fc = null;
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    foreach (KeyValuePair<DF2DFeatureClass, IFeatureClass> k in spatialQueryLine)
                    {
                        if (k.Key == dffc)
                        {
                            fc = k.Value;
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<DF2DFeatureClass, IFeatureClass> k in spatialQueryNode)
                    {
                        if (k.Key == dffc)
                        {
                            fc = k.Value;
                        }
                    }
                }
               

                if (fc == null) return;
                IFields ficol = fc.Fields;
                int index = ficol.FindField(currentName);
                IQueryFilter filter = new QueryFilterClass();
                filter.SubFields = currentName;

                IFeatureCursor ftCursor = fc.Search(filter, true);
                IFeature pFeature = ftCursor.NextFeature();
                while (pFeature != null)
                {
                    string temp = pFeature.get_Value(index).ToString();
                    if (temp != null) list.Add(temp);
                    pFeature = ftCursor.NextFeature();
                }


            }
            if (list.Count > 0)
            {
                foreach (string str2 in from v in list.Distinct<string>() orderby v select v)
                {
                    if (!string.IsNullOrEmpty(str2))
                    {
                        this.listBoxControlValues.Items.Add(str2);
                    }
                }
            }





        }


        private void treeLayer_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            this.listBoxControlValues.Items.Clear();
            this.listBoxControlProperty.Items.Clear();


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
                        if (aliaNameAndName.Count > 0)
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
                            this.listBoxControlProperty.Items.Add(str2);
                        }
                    }
                }

            }


            catch (System.Exception ex)
            {

            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            expression += btnEqual.Text;
            this.teExpression.Text = expression;
        }

        private void btnGreater_Click(object sender, EventArgs e)
        {
            expression += btnGreater.Text;
            this.teExpression.Text = expression;
        }

        private void btnGreaterEqual_Click(object sender, EventArgs e)
        {
            expression += btnGreaterEqual.Text;
            this.teExpression.Text = expression;
        }

        private void btnLess_Click(object sender, EventArgs e)
        {
            expression += btnLess.Text;
            this.teExpression.Text = expression;
        }

        private void btnLessEqual_Click(object sender, EventArgs e)
        {
            expression += btnLessEqual.Text;
            this.teExpression.Text = expression;
        }

        private void btnBracket_Click(object sender, EventArgs e)
        {
            expression += btnBracket.Text;
            this.teExpression.Text = expression;
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            expression += btnOr.Text;
            this.teExpression.Text = expression;
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            expression += btnAnd.Text;
            this.teExpression.Text = expression;
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            expression += btnLike.Text;
            this.teExpression.Text = expression;
        }
        string expression = null;
        string fieldName = null;
        private void listBoxControlProperty_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string currentAliaName = this.listBoxControlProperty.SelectedItem.ToString();
            string currentName = null;
            foreach (KeyValuePair<string, string> v in aliaNameAndName)
            {
                if (v.Key == currentAliaName)
                {
                    currentName = v.Value;
                }
            }
            if (currentName == null) return;
            fieldName = currentName;
            expression += currentName;
            this.teExpression.Text = expression;

        }

        private void listBoxControlValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string currentValue = this.listBoxControlValues.SelectedItem.ToString();
            expression += "'" + currentValue + "'";
            this.teExpression.Text = expression;
        }


        }
    }

