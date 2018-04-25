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
using DFDataConfig.Logic;
using DFDataConfig.Class;
using Gvitech.CityMaker.FdeCore;
using DFWinForms.Class;
using DF3DData.Class;
using Gvitech.CityMaker.FdeGeometry;
using DFCommon.Class;

namespace DF3DPipe.Query.Frm
{
    public class FrmCompoundConditionQuery : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ListBoxControl listBoxControlValues;
        private ListBoxControl listBoxControlProperty;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraTreeList.TreeList treelist;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
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
        private SimpleButton btnUnSelectAll;
        private SimpleButton btnSelectAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
    

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCompoundConditionQuery));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnUnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
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
            this.treelist = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.listBoxControlValues = new DevExpress.XtraEditors.ListBoxControl();
            this.listBoxControlProperty = new DevExpress.XtraEditors.ListBoxControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
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
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceSQL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnUnSelectAll);
            this.layoutControl1.Controls.Add(this.btnSelectAll);
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
            this.layoutControl1.Controls.Add(this.treelist);
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
            // btnUnSelectAll
            // 
            this.btnUnSelectAll.Location = new System.Drawing.Point(99, 416);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new System.Drawing.Size(90, 22);
            this.btnUnSelectAll.StyleController = this.layoutControl1;
            this.btnUnSelectAll.TabIndex = 3;
            this.btnUnSelectAll.Text = "全不选";
            this.btnUnSelectAll.Click += new System.EventHandler(this.btnUnSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(5, 416);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(90, 22);
            this.btnSelectAll.StyleController = this.layoutControl1;
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(403, 254);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(69, 22);
            this.btnLike.StyleController = this.layoutControl1;
            this.btnLike.TabIndex = 14;
            this.btnLike.Text = "like";
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnLessEqual
            // 
            this.btnLessEqual.Location = new System.Drawing.Point(314, 228);
            this.btnLessEqual.Name = "btnLessEqual";
            this.btnLessEqual.Size = new System.Drawing.Size(85, 22);
            this.btnLessEqual.StyleController = this.layoutControl1;
            this.btnLessEqual.TabIndex = 10;
            this.btnLessEqual.Text = "<=";
            this.btnLessEqual.Click += new System.EventHandler(this.btnLessEqual_Click);
            // 
            // btnGreaterEqual
            // 
            this.btnGreaterEqual.Location = new System.Drawing.Point(403, 202);
            this.btnGreaterEqual.Name = "btnGreaterEqual";
            this.btnGreaterEqual.Size = new System.Drawing.Size(69, 22);
            this.btnGreaterEqual.StyleController = this.layoutControl1;
            this.btnGreaterEqual.TabIndex = 8;
            this.btnGreaterEqual.Text = ">=";
            this.btnGreaterEqual.Click += new System.EventHandler(this.btnGreaterEqual_Click);
            // 
            // btnBracket
            // 
            this.btnBracket.Location = new System.Drawing.Point(403, 228);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new System.Drawing.Size(69, 22);
            this.btnBracket.StyleController = this.layoutControl1;
            this.btnBracket.TabIndex = 11;
            this.btnBracket.Text = "()";
            this.btnBracket.Click += new System.EventHandler(this.btnBracket_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(314, 254);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(85, 22);
            this.btnAnd.StyleController = this.layoutControl1;
            this.btnAnd.TabIndex = 13;
            this.btnAnd.Text = "and";
            this.btnAnd.Click += new System.EventHandler(this.btnAnd_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(233, 254);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(77, 22);
            this.btnOr.StyleController = this.layoutControl1;
            this.btnOr.TabIndex = 12;
            this.btnOr.Text = "or";
            this.btnOr.Click += new System.EventHandler(this.btnOr_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(233, 202);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(77, 22);
            this.btnEqual.StyleController = this.layoutControl1;
            this.btnEqual.TabIndex = 6;
            this.btnEqual.Text = "=";
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // btnLess
            // 
            this.btnLess.Location = new System.Drawing.Point(233, 228);
            this.btnLess.Name = "btnLess";
            this.btnLess.Size = new System.Drawing.Size(77, 22);
            this.btnLess.StyleController = this.layoutControl1;
            this.btnLess.TabIndex = 9;
            this.btnLess.Text = "<";
            this.btnLess.Click += new System.EventHandler(this.btnLess_Click);
            // 
            // btnGreater
            // 
            this.btnGreater.Location = new System.Drawing.Point(314, 202);
            this.btnGreater.Name = "btnGreater";
            this.btnGreater.Size = new System.Drawing.Size(85, 22);
            this.btnGreater.StyleController = this.layoutControl1;
            this.btnGreater.TabIndex = 7;
            this.btnGreater.Text = ">";
            this.btnGreater.Click += new System.EventHandler(this.btnGreater_Click);
            // 
            // teExpression
            // 
            this.teExpression.Enabled = false;
            this.teExpression.Location = new System.Drawing.Point(202, 329);
            this.teExpression.Name = "teExpression";
            this.teExpression.Size = new System.Drawing.Size(296, 80);
            this.teExpression.TabIndex = 17;
            this.teExpression.Text = "";
            // 
            // ceSQL
            // 
            this.ceSQL.Location = new System.Drawing.Point(317, 303);
            this.ceSQL.Name = "ceSQL";
            this.ceSQL.Properties.Caption = "编辑SQL";
            this.ceSQL.Size = new System.Drawing.Size(181, 19);
            this.ceSQL.StyleController = this.layoutControl1;
            this.ceSQL.TabIndex = 16;
            this.ceSQL.CheckedChanged += new System.EventHandler(this.ceSQL_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(202, 303);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(111, 22);
            this.btnClear.StyleController = this.layoutControl1;
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 413);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(143, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(202, 413);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(149, 22);
            this.btnQuery.StyleController = this.layoutControl1;
            this.btnQuery.TabIndex = 18;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // treelist
            // 
            this.treelist.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treelist.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treelist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.NodeObject});
            this.treelist.Location = new System.Drawing.Point(5, 54);
            this.treelist.Name = "treelist";
            this.treelist.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treelist.OptionsView.ShowCheckBoxes = true;
            this.treelist.OptionsView.ShowColumns = false;
            this.treelist.OptionsView.ShowIndicator = false;
            this.treelist.OptionsView.ShowVertLines = false;
            this.treelist.Size = new System.Drawing.Size(184, 358);
            this.treelist.StateImageList = this.imageCollection1;
            this.treelist.TabIndex = 1;
            this.treelist.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treelist_AfterCheckNode);
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
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Group.png");
            this.imageCollection1.Images.SetKeyName(1, "FeatureLayer_model.png");
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
            this.radioGroup1.Size = new System.Drawing.Size(184, 25);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // listBoxControlValues
            // 
            this.listBoxControlValues.Location = new System.Drawing.Point(349, 25);
            this.listBoxControlValues.Name = "listBoxControlValues";
            this.listBoxControlValues.Size = new System.Drawing.Size(152, 147);
            this.listBoxControlValues.StyleController = this.layoutControl1;
            this.listBoxControlValues.TabIndex = 5;
            this.listBoxControlValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxControlValues_MouseDoubleClick);
            // 
            // listBoxControlProperty
            // 
            this.listBoxControlProperty.Location = new System.Drawing.Point(199, 25);
            this.listBoxControlProperty.Name = "listBoxControlProperty";
            this.listBoxControlProperty.Size = new System.Drawing.Size(140, 147);
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
            this.layoutControlGroup6,
            this.layoutControlGroup7});
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
            this.layoutControlItem4,
            this.layoutControlItem19,
            this.layoutControlItem20});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(194, 443);
            this.layoutControlGroup2.Text = "图层树";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.radioGroup1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(188, 29);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.treelist;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(188, 362);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.btnSelectAll;
            this.layoutControlItem19.CustomizationFormText = "layoutControlItem19";
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 391);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem19.Text = "layoutControlItem19";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextToControlDistance = 0;
            this.layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.btnUnSelectAll;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
            this.layoutControlItem20.Location = new System.Drawing.Point(94, 391);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem20.Text = "layoutControlItem20";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextToControlDistance = 0;
            this.layoutControlItem20.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "字段";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(194, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(150, 177);
            this.layoutControlGroup3.Text = "字段";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.listBoxControlProperty;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(144, 151);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "字段值";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup6.Location = new System.Drawing.Point(344, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(162, 177);
            this.layoutControlGroup6.Text = "字段值";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.listBoxControlValues;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(156, 151);
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
            this.emptySpaceItem6,
            this.layoutControlGroup5});
            this.layoutControlGroup7.Location = new System.Drawing.Point(194, 177);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(312, 266);
            this.layoutControlGroup7.Text = "操作符";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnGreater;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(115, 0);
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
            this.emptySpaceItem3.Location = new System.Drawing.Point(277, 0);
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
            this.layoutControlItem13.Size = new System.Drawing.Size(81, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnAnd;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(115, 52);
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
            this.layoutControlItem15.Location = new System.Drawing.Point(204, 26);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.btnLessEqual;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(115, 26);
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
            this.layoutControlItem18.Location = new System.Drawing.Point(204, 52);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(73, 26);
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
            this.layoutControlItem12.Size = new System.Drawing.Size(81, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.btnGreaterEqual;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(204, 0);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(73, 26);
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
            this.layoutControlItem11.Size = new System.Drawing.Size(81, 26);
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
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "表达式";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 78);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(306, 162);
            this.layoutControlGroup5.Text = "表达式";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teExpression;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(300, 84);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnQuery;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 110);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(153, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(153, 110);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnClear;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(115, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.ceSQL;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(115, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(185, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // FrmCompoundConditionQuery
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(506, 443);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmCompoundConditionQuery";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "复合查询";
            this.Load += new System.EventHandler(this.FrmCompoundConditionQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceSQL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }
        public FrmCompoundConditionQuery()
        {
            InitializeComponent();
        }

        private string _sysFieldName;
        private string _facType;
        public string FacType
        {
            get { return this._facType; }
        }
        private Dictionary<SubClass, string> _dict;
        public Dictionary<SubClass, string> Dict
        {
            get { return this._dict; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private void RecursiveBuildTree(TreeListNode parentNode, List<LogicGroup> listLG)
        {
            foreach (LogicGroup lg in listLG)
            {
                TreeListNode node = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, parentNode);
                node.StateImageIndex = 0;
                RecursiveBuildTree(node, lg.LogicGroups);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                    mcnode.StateImageIndex = 0;
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible3D) continue;
                        TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 1;
                    }
                }
            }
        }

        private void BuildTree()
        {
            if (this._resRootLogicGroups != null)
                foreach (LogicGroup lg in this._resRootLogicGroups)
                {
                    TreeListNode node = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, null);
                    node.StateImageIndex = 0;
                    RecursiveBuildTree(node, lg.LogicGroups);
                    foreach (MajorClass mc in lg.MajorClasses)
                    {
                        TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                        mcnode.StateImageIndex = 0;
                        foreach (SubClass sc in mc.SubClasses)
                        {
                            if (!sc.Visible3D) continue;
                            TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                            scnode.StateImageIndex = 1;
                        }
                    }
                }

            if (this._resRootMajorClasses != null)
                foreach (MajorClass mc in this._resRootMajorClasses)
                {
                    TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, null);
                    mcnode.StateImageIndex = 0;
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible3D) continue;
                        TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 1;
                    }
                }
        }

        public void SetData(List<LogicGroup> resRootLogicGroups, List<MajorClass> resRootMajorClasses, IGeometry geo)
        {
            this._resRootLogicGroups = resRootLogicGroups;
            this._resRootMajorClasses = resRootMajorClasses;
            this._geo = geo;
        }

        private List<LogicGroup> _resRootLogicGroups;
        private List<MajorClass> _resRootMajorClasses;
        private IGeometry _geo;
        public IGeometry Geo
        {
            get { return this._geo; }
        }

        private void FrmCompoundConditionQuery_Load(object sender, EventArgs e)
        {
            BuildTree();
            this._dict = new Dictionary<SubClass, string>();
            radioGroup1_SelectedIndexChanged(null, null);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxControlProperty.Items.Clear();
            this.teExpression.Text = "";
            if (this.radioGroup1.SelectedIndex == 0)
            {
                FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("PipeLine");
                if (fac != null)
                {
                    this._facType = fac.Name;
                    foreach (DFDataConfig.Class.FieldInfo fi in fac.FieldInfoCollection)
                    {
                        if (fi.CanQuery) this.listBoxControlProperty.Items.Add(fi);
                    }
                }
            }
            else
            {
                FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("PipeNode");
                if (fac != null)
                {
                    this._facType = fac.Name;
                    foreach (DFDataConfig.Class.FieldInfo fi in fac.FieldInfoCollection)
                    {
                        if (fi.CanQuery) this.listBoxControlProperty.Items.Add(fi);
                    }
                }
            }
            treelist_AfterCheckNode(null, null);
        }

        private void DoQuery()
        {
            if (this.treelist.GetAllCheckedNodes() != null)
            {
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc3DId = sc.Parent.Fc3D.Split(';');
                        if (arrFc3DId == null) continue;
                        foreach (string fc3DId in arrFc3DId)
                        {
                            DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            //string pipetype = this.radioGroup1.SelectedIndex == 0 ? "PipeLine" : "PipeNode";
                            if (fc == null || facc == null || facc.Name != this._facType) continue;
                            string strWhereClause = "";
                            if(string.IsNullOrEmpty(this.teExpression.Text.Trim()))
                            {
                                strWhereClause = "GroupId = " + sc.GroupId;
                            }
                            else
                            {
                                strWhereClause = "GroupId = " + sc.GroupId + " and (" + this.teExpression.Text + ")";
                            }
                            this._dict.Add(sc, strWhereClause);
                        }
                    }
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this._dict != null) this._dict.Clear();
            try
            {
                WaitForm.Start("正在查询...", "请稍后", false);
                if (_dict != null) _dict.Clear();
                DoQuery();
                if (_dict == null || _dict.Count == 0)
                {
                    WaitForm.Stop();
                    XtraMessageBox.Show("查询结果为空！", "提示");
                    return;
                }
                WaitForm.Stop();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                WaitForm.Stop();
                XtraMessageBox.Show("查询出错！", "提示");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.teExpression.Clear();
        }

        private void ceSQL_CheckedChanged(object sender, EventArgs e)
        {
            this.teExpression.Enabled = this.ceSQL.Checked;
        }

        private void listBoxControlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._sysFieldName = "";
            if (this.listBoxControlProperty.SelectedItem != null)
            {
                DFDataConfig.Class.FieldInfo fi = this.listBoxControlProperty.SelectedItem as DFDataConfig.Class.FieldInfo;
                this._sysFieldName = fi.SystemName;
            }
            treelist_AfterCheckNode(null, null);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            this.teExpression.Text += " = ";
        }

        private void btnGreater_Click(object sender, EventArgs e)
        {
            this.teExpression.Text += " > ";
        }

        private void btnGreaterEqual_Click(object sender, EventArgs e)
        {
            this.teExpression.Text += " >= ";
        }

        private void btnLess_Click(object sender, EventArgs e)
        {
            this.teExpression.Text += " < ";
        }

        private void btnLessEqual_Click(object sender, EventArgs e)
        {
            this.teExpression.Text += " <= ";
        }

        private void btnBracket_Click(object sender, EventArgs e)
        {
            this.teExpression.Text = " (" + this.teExpression.Text + ") ";
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            this.teExpression.Text += " OR ";
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            this.teExpression.Text += " AND ";
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            this.teExpression.Text += " LIKE ";
        }

        private void treelist_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            this.listBoxControlValues.Items.Clear();
            if (string.IsNullOrEmpty(this._sysFieldName)) return;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                WaitForm.Start("正在加载列表...", "请稍后");
                HashSet<string> list = new HashSet<string>();
                bool bBreak = false;
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string cacheType = sc.Parent.Name + "_" + sc.GroupId + "_3D_" + this._sysFieldName;
                        object objCache = CacheHelper.GetCache(cacheType);
                        if (objCache != null && objCache is HashSet<string>)
                        {
                            HashSet<string> temphs = objCache as HashSet<string>;
                            foreach (string tempstr in temphs) { list.Add(tempstr); }
                            continue;
                        }

                        HashSet<string> listsc = new HashSet<string>(); 
                        string[] arrFc3DId = sc.Parent.Fc3D.Split(';');
                        if (arrFc3DId == null) continue;
                        foreach (string fc3DId in arrFc3DId)
                        {
                            DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                            if (dffc == null) continue;
                            FacilityClass facClass = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            //string pipetype = this.radioGroup1.SelectedIndex == 0 ? "PipeLine" : "PipeNode";
                            if (fc == null || facClass == null || facClass.Name != this._facType) continue;
                            DFDataConfig.Class.FieldInfo fi = facClass.GetFieldInfoBySystemName(this._sysFieldName);
                            if (fi == null) continue;
                            IFieldInfoCollection fiCol = fc.GetFields();
                            int index = fiCol.IndexOf(fi.Name);
                            if (index < 0) continue;
                            Gvitech.CityMaker.FdeCore.FieldInfo gfi = (Gvitech.CityMaker.FdeCore.FieldInfo)fiCol.Get(index);
                            IQueryFilter filter = new QueryFilter();
                            filter.SubFields = gfi.Name;
                            filter.ResultBeginIndex = 0;
                            filter.ResultLimit = 1;
                            while (true)
                            {
                                string strTempClause = gfi.Name + " is not null and ";
                                string fClause = strTempClause;
                                foreach (string strtemp in listsc)
                                {
                                    fClause += gfi.Name + " <> " + strtemp + " and ";
                                }
                                fClause = fClause.Substring(0, fClause.Length - 5);
                                filter.WhereClause = "GroupId = " + sc.GroupId + " and " + fClause;

                                cursor = fc.Search(filter, true);
                                if ((row = cursor.NextRow()) != null)
                                {
                                    if (row.IsNull(0)) break;
                                    object temp = row.GetValue(0);
                                    string strtemp = "";
                                    switch (gfi.FieldType)
                                    {
                                        case gviFieldType.gviFieldFID:
                                        case gviFieldType.gviFieldFloat:
                                        case gviFieldType.gviFieldDouble:
                                        case gviFieldType.gviFieldInt16:
                                        case gviFieldType.gviFieldInt32:
                                        case gviFieldType.gviFieldInt64:
                                            strtemp = temp.ToString();
                                            break;
                                        case gviFieldType.gviFieldDate:
                                        case gviFieldType.gviFieldString:
                                        case gviFieldType.gviFieldUUID:
                                            strtemp = "'" + temp.ToString() + "'";
                                            break;
                                        case gviFieldType.gviFieldBlob:
                                        case gviFieldType.gviFieldGeometry:
                                        case gviFieldType.gviFieldUnknown:
                                        default:
                                            continue;
                                    }
                                    if (temp != null)
                                    {
                                        list.Add(strtemp);
                                        listsc.Add(strtemp);
                                        if (list.Count > 10)
                                        {
                                            bBreak = true;
                                            break;// 列举10个
                                        }
                                    }
                                }
                                else break;
                            }
                            if (bBreak) break;
                        }
                        CacheHelper.SetCache(cacheType, listsc);
                    }
                    if (bBreak) break;
                }
                foreach (string str2 in list)
                {
                    //if (!(string.IsNullOrEmpty(str2)))
                    //{
                    this.listBoxControlValues.Items.Add(str2);
                    //}
                }
            }
            catch (Exception ex)
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
                WaitForm.Stop();
            }

        }

        private void listBoxControlProperty_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.None)
            {
                if (this.listBoxControlProperty.SelectedItem != null && this.listBoxControlProperty.SelectedItem is DFDataConfig.Class.FieldInfo)
                {
                    this.teExpression.Text += " " + (this.listBoxControlProperty.SelectedItem as DFDataConfig.Class.FieldInfo).Name + " ";
                }
            }
        }

        private void listBoxControlValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.None)
            {
                if (this.listBoxControlValues.SelectedItem != null)
                {
                    this.teExpression.Text += " " + this.listBoxControlValues.SelectedItem.ToString() + " ";
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.treelist.CheckAll();
            treelist_AfterCheckNode(null, null);
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            this.treelist.UncheckAll();
            treelist_AfterCheckNode(null, null);
        }

    }
}
