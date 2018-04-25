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
using DFWinForms.Class;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DevExpress.XtraTreeList.Nodes;
using DFDataConfig.Class;
using DFDataConfig.Logic;
using DFCommon.Class;

namespace DF3DPipe.Stats.Frm
{
    public class FrmPropertyStatsByDiameter : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnCancel;
        private SimpleButton btnStats;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraTreeList.TreeList treelist;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private IContainer components;
        private LabelControl labelControl3;
        private LabelControl labelControl2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private SimpleButton btnUnSelectAll;
        private SimpleButton btnSelectAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private SimpleButton btnAddRow;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private PictureEdit pictureEdit2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private SpinEdit spWMin;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private SpinEdit spWMax;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private SpinEdit spHMax;
        private SpinEdit spHMin;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPropertyStatsByDiameter));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.spHMax = new DevExpress.XtraEditors.SpinEdit();
            this.spWMax = new DevExpress.XtraEditors.SpinEdit();
            this.spHMin = new DevExpress.XtraEditors.SpinEdit();
            this.spWMin = new DevExpress.XtraEditors.SpinEdit();
            this.btnAddRow = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnUnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.treelist = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnStats = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spHMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spWMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spHMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spWMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spHMax);
            this.layoutControl1.Controls.Add(this.spWMax);
            this.layoutControl1.Controls.Add(this.spHMin);
            this.layoutControl1.Controls.Add(this.spWMin);
            this.layoutControl1.Controls.Add(this.btnAddRow);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnUnSelectAll);
            this.layoutControl1.Controls.Add(this.btnSelectAll);
            this.layoutControl1.Controls.Add(this.treelist);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnStats);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(485, 417);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // spHMax
            // 
            this.spHMax.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spHMax.Location = new System.Drawing.Point(419, 142);
            this.spHMax.Name = "spHMax";
            this.spHMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spHMax.Properties.IsFloatValue = false;
            this.spHMax.Properties.Mask.EditMask = "N00";
            this.spHMax.Size = new System.Drawing.Size(58, 22);
            this.spHMax.StyleController = this.layoutControl1;
            this.spHMax.TabIndex = 6;
            // 
            // spWMax
            // 
            this.spWMax.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spWMax.Location = new System.Drawing.Point(266, 142);
            this.spWMax.Name = "spWMax";
            this.spWMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spWMax.Properties.IsFloatValue = false;
            this.spWMax.Properties.Mask.EditMask = "N00";
            this.spWMax.Size = new System.Drawing.Size(62, 22);
            this.spWMax.StyleController = this.layoutControl1;
            this.spWMax.TabIndex = 4;
            // 
            // spHMin
            // 
            this.spHMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spHMin.Location = new System.Drawing.Point(419, 116);
            this.spHMin.Name = "spHMin";
            this.spHMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spHMin.Properties.IsFloatValue = false;
            this.spHMin.Properties.Mask.EditMask = "N00";
            this.spHMin.Size = new System.Drawing.Size(58, 22);
            this.spHMin.StyleController = this.layoutControl1;
            this.spHMin.TabIndex = 5;
            // 
            // spWMin
            // 
            this.spWMin.AllowDrop = true;
            this.spWMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spWMin.Location = new System.Drawing.Point(266, 116);
            this.spWMin.Name = "spWMin";
            this.spWMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spWMin.Properties.IsFloatValue = false;
            this.spWMin.Properties.Mask.EditMask = "N00";
            this.spWMin.Size = new System.Drawing.Size(62, 22);
            this.spWMin.StyleController = this.layoutControl1;
            this.spWMin.TabIndex = 3;
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(182, 171);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(298, 22);
            this.btnAddRow.StyleController = this.layoutControl1;
            this.btnAddRow.TabIndex = 7;
            this.btnAddRow.Text = "添    加";
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(182, 197);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.repositoryItemSpinEdit2,
            this.repositoryItemButtonEdit1,
            this.repositoryItemSpinEdit3,
            this.repositoryItemSpinEdit4});
            this.gridControl1.Size = new System.Drawing.Size(298, 189);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "宽最小值";
            this.gridColumn4.ColumnEdit = this.repositoryItemSpinEdit1;
            this.gridColumn4.FieldName = "WMin";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 61;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            this.repositoryItemSpinEdit1.EditValueChanged += new System.EventHandler(this.repositoryItemSpinEdit1_EditValueChanged);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "宽最大值";
            this.gridColumn5.ColumnEdit = this.repositoryItemSpinEdit2;
            this.gridColumn5.FieldName = "WMax";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 56;
            // 
            // repositoryItemSpinEdit2
            // 
            this.repositoryItemSpinEdit2.AutoHeight = false;
            this.repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
            this.repositoryItemSpinEdit2.EditValueChanged += new System.EventHandler(this.repositoryItemSpinEdit2_EditValueChanged);
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "高最小值";
            this.gridColumn6.ColumnEdit = this.repositoryItemSpinEdit3;
            this.gridColumn6.FieldName = "HMin";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 60;
            // 
            // repositoryItemSpinEdit3
            // 
            this.repositoryItemSpinEdit3.AutoHeight = false;
            this.repositoryItemSpinEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit3.Name = "repositoryItemSpinEdit3";
            this.repositoryItemSpinEdit3.EditValueChanged += new System.EventHandler(this.repositoryItemSpinEdit3_EditValueChanged);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "高最大值";
            this.gridColumn7.ColumnEdit = this.repositoryItemSpinEdit4;
            this.gridColumn7.FieldName = "HMax";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 60;
            // 
            // repositoryItemSpinEdit4
            // 
            this.repositoryItemSpinEdit4.AutoHeight = false;
            this.repositoryItemSpinEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit4.Name = "repositoryItemSpinEdit4";
            this.repositoryItemSpinEdit4.EditValueChanged += new System.EventHandler(this.repositoryItemSpinEdit4_EditValueChanged);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "操作";
            this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit1;
            this.gridColumn8.FieldName = "oper";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 43;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit1.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "删除", null, null, true)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // btnUnSelectAll
            // 
            this.btnUnSelectAll.Location = new System.Drawing.Point(88, 364);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new System.Drawing.Size(84, 22);
            this.btnUnSelectAll.StyleController = this.layoutControl1;
            this.btnUnSelectAll.TabIndex = 2;
            this.btnUnSelectAll.Text = "全不选";
            this.btnUnSelectAll.Click += new System.EventHandler(this.btnUnSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(5, 364);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(79, 22);
            this.btnSelectAll.StyleController = this.layoutControl1;
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // treelist
            // 
            this.treelist.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treelist.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treelist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.NodeObject});
            this.treelist.Location = new System.Drawing.Point(5, 93);
            this.treelist.Name = "treelist";
            this.treelist.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treelist.OptionsView.ShowCheckBoxes = true;
            this.treelist.OptionsView.ShowColumns = false;
            this.treelist.OptionsView.ShowIndicator = false;
            this.treelist.OptionsView.ShowVertLines = false;
            this.treelist.Size = new System.Drawing.Size(167, 267);
            this.treelist.StateImageList = this.imageCollection1;
            this.treelist.TabIndex = 0;
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(336, 393);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStats
            // 
            this.btnStats.Location = new System.Drawing.Point(179, 393);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(153, 22);
            this.btnStats.StyleController = this.layoutControl1;
            this.btnStats.TabIndex = 9;
            this.btnStats.Text = "统计";
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.pictureEdit2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(481, 64);
            this.panelControl1.TabIndex = 5;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(159, 7);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit2.Size = new System.Drawing.Size(48, 48);
            this.pictureEdit2.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(264, 32);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "labelControl3";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(232, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "属性统计";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(485, 417);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(485, 68);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "图层树";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 68);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(177, 323);
            this.layoutControlGroup2.Text = "图层树";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treelist;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(171, 271);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnSelectAll;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 271);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnUnSelectAll;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(83, 271);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(88, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "查询条件";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem10,
            this.layoutControlGroup4,
            this.layoutControlGroup5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(177, 68);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(308, 323);
            this.layoutControlGroup3.Text = "查询条件";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gridControl1;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(302, 193);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnAddRow;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(302, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(153, 78);
            this.layoutControlGroup4.Text = "管线截面宽";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spWMin;
            this.layoutControlItem5.CustomizationFormText = "最小值(mm)：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem5.Text = "最小值(mm)：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(78, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.spWMax;
            this.layoutControlItem6.CustomizationFormText = "最大值(mm)：";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem6.Text = "最大值(mm)：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(78, 14);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11,
            this.layoutControlItem12});
            this.layoutControlGroup5.Location = new System.Drawing.Point(153, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(149, 78);
            this.layoutControlGroup5.Text = "管线截面高";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.spHMin;
            this.layoutControlItem11.CustomizationFormText = "最小值(mm)：";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem11.Text = "最小值(mm)：";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(78, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.spHMax;
            this.layoutControlItem12.CustomizationFormText = "最大值(mm)：";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem12.Text = "最大值(mm)：";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(78, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 391);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(177, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCancel;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(334, 391);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(151, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnStats;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(177, 391);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(157, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "最小埋深(m)";
            this.gridColumn1.FieldName = "MinDepth";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 411;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "最大埋深(m)";
            this.gridColumn2.FieldName = "MaxDepth";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 400;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "操作";
            this.gridColumn3.FieldName = "Oper";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 204;
            // 
            // FrmPropertyStatsByDiameter
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(485, 417);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmPropertyStatsByDiameter";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmPropertyQueryByDepth_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spHMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spWMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spHMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spWMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        private DataTable _dt;
        private string _facType;
        public FrmPropertyStatsByDiameter(string title, string facType)
        {
            InitializeComponent();
            this.Text = title;
            this.labelControl3.Text = title;
            this._facType = facType;
            this._dt = new DataTable();
            this._dt.Columns.AddRange(new DataColumn[] { new DataColumn("WMin"), new DataColumn("WMax"), new DataColumn("HMin"), new DataColumn("HMax") });
            this.gridControl1.DataSource = this._dt;
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
            foreach (LogicGroup lg in LogicDataStructureManage3D.Instance.RootLogicGroups)
            {
                TreeListNode node = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias,lg }, null);
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

            foreach (MajorClass mc in LogicDataStructureManage3D.Instance.RootMajorClasses)
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

        private void FrmPropertyQueryByDepth_Load(object sender, EventArgs e)
        {
            BuildTree();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.treelist.CheckAll();
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            this.treelist.UncheckAll();
        }

        private void repositoryItemSpinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            int focusedRowHandle = this.gridView1.FocusedRowHandle;
            if (focusedRowHandle == -1) return;
            SpinEdit sp = sender as SpinEdit;
            if (sp.Value < 0) sp.Value = 0;
            DataRow dr = this.gridView1.GetDataRow(focusedRowHandle);
            dr["WMin"] = sp.Value;
            this.gridView1.RefreshRow(focusedRowHandle);
        }

        private void repositoryItemSpinEdit2_EditValueChanged(object sender, EventArgs e)
        {
            int focusedRowHandle = this.gridView1.FocusedRowHandle;
            if (focusedRowHandle == -1) return;
            SpinEdit sp = sender as SpinEdit;
            if (sp.Value < 0) sp.Value = 0;
            DataRow dr = this.gridView1.GetDataRow(focusedRowHandle);
            dr["WMax"] = sp.Value;
            this.gridView1.RefreshRow(focusedRowHandle);
        }

        private void repositoryItemSpinEdit3_EditValueChanged(object sender, EventArgs e)
        {
            int focusedRowHandle = this.gridView1.FocusedRowHandle;
            if (focusedRowHandle == -1) return;
            SpinEdit sp = sender as SpinEdit;
            if (sp.Value < 0) sp.Value = 0;
            DataRow dr = this.gridView1.GetDataRow(focusedRowHandle);
            dr["HMin"] = sp.Value;
            this.gridView1.RefreshRow(focusedRowHandle);
        }

        private void repositoryItemSpinEdit4_EditValueChanged(object sender, EventArgs e)
        {
            int focusedRowHandle = this.gridView1.FocusedRowHandle;
            if (focusedRowHandle == -1) return;
            SpinEdit sp = sender as SpinEdit;
            if (sp.Value < 0) sp.Value = 0;
            DataRow dr = this.gridView1.GetDataRow(focusedRowHandle);
            dr["HMax"] = sp.Value;
            this.gridView1.RefreshRow(focusedRowHandle);
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (this.spWMin.Value - this.spWMax.Value > 0)
            {
                XtraMessageBox.Show("管线截面宽最小值不能大于最大值", "提示");
                return;
            }
            if (this.spHMin.Value - this.spHMax.Value > 0)
            {
                XtraMessageBox.Show("管线截面高最小值不能大于最大值", "提示");
                return;
            }
            DataRow dr = this._dt.NewRow();
            dr["WMin"] = this.spWMin.Value;
            dr["WMax"] = this.spWMax.Value;
            dr["HMin"] = this.spHMin.Value;
            dr["HMax"] = this.spHMax.Value;
            this._dt.Rows.Add(dr);
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.Controls.EditorButton btn = e.Button;
            switch (btn.Caption)
            {
                case "删除":
                    int focusedRowHandle = this.gridView1.FocusedRowHandle;
                    if (focusedRowHandle == -1) return;
                    DataRow dr = this.gridView1.GetDataRow(focusedRowHandle);
                    this._dt.Rows.Remove(dr);
                    this.gridView1.RefreshRow(focusedRowHandle);
                    break;
            }
        }

        private DataTable DoStats()
        {
            List<TreeListNode> list = this.treelist.GetAllCheckedNodes();
            if (list == null)
            {
                return null;
            }

            DataTable dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),
                                new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});

            foreach (TreeListNode node in list)
            {
                object obj = node.GetValue("NodeObject");
                if (obj != null && obj is SubClass)
                {
                    SubClass sc = obj as SubClass;
                    if (sc.Parent == null) continue;
                    string[] arrFc3DId = sc.Parent.Fc3D.Split(';');
                    if (arrFc3DId == null) continue;
                    double subclasslength = 0.0;
                    int indexStart = dttemp.Rows.Count;
                    foreach (string fc3DId in arrFc3DId)
                    {
                        DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                        if (dffc == null) continue;
                        FacilityClass facc = dffc.GetFacilityClass();
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null || facc == null || facc.Name != this._facType) continue;
                        DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength");
                        if (fiPipeLength == null) continue;
                        int indexPipeLength = fc.GetFields().IndexOf(fiPipeLength.Name);
                        if (indexPipeLength == -1) continue;
                        DFDataConfig.Class.FieldInfo fi1 = facc.GetFieldInfoBySystemName("Diameter1");
                        DFDataConfig.Class.FieldInfo fi2 = facc.GetFieldInfoBySystemName("Diameter2");
                        if (fi1 == null || fi2 == null) continue;
                        int index1 = fc.GetFields().IndexOf(fi1.Name);
                        int index2 = fc.GetFields().IndexOf(fi2.Name);
                        if (index1 == -1 || index2 == -1) continue;
                        IFieldInfo fcfi1 = fc.GetFields().Get(index1);
                        switch (fcfi1.FieldType)
                        {
                            case gviFieldType.gviFieldBlob:
                            case gviFieldType.gviFieldGeometry:
                            case gviFieldType.gviFieldUnknown:
                                continue;
                        }
                        IFieldInfo fcfi2 = fc.GetFields().Get(index2);
                        switch (fcfi2.FieldType)
                        {
                            case gviFieldType.gviFieldBlob:
                            case gviFieldType.gviFieldGeometry:
                            case gviFieldType.gviFieldUnknown:
                                continue;
                        }

                        IQueryFilter filter = new QueryFilter();
                        filter.SubFields = fiPipeLength.Name;
                        foreach (DataRow dr1 in this._dt.Rows)
                        {
                            string widemin = dr1["WMin"].ToString();
                            string widemax = dr1["WMax"].ToString();
                            string heightmin = dr1["HMin"].ToString();
                            string heightmax = dr1["HMax"].ToString();
                            string strValue = "";
                            string clause = "";
                            if (heightmax == heightmin)
                            {
                                strValue = "直径:" + widemin + "~" + widemax;
                                clause = "(" + fcfi1.Name + " <= " + widemax + " and " + fcfi1.Name + " >= " + widemin + ") and " + fcfi2.Name + "=0 or " + fcfi2.Name + " is null";
                            }
                            else
                            {
                                strValue = "宽:" + widemin + "~" + widemax + " 高:" + heightmin + "~" + heightmax;
                                clause = "(" + fcfi1.Name + " <= " + widemax + " and " + fcfi1.Name + " >= " + widemin + ") and (" + fcfi2.Name + " <= " + heightmax + " and " + fcfi2.Name + " >= " + heightmin + ") and " + fcfi2.Name + " <> 0 and " + fcfi2.Name + " is not null";
                            }
                            //if (heightmax == "" || heightmin == "")
                            //{
                            //    clause += fcfi1.Name + " <= " + widemax + " and " + fcfi1.Name + " >= " + widemin;
                            //}
                            //else if (widemax == "" || widemin == "")
                            //{
                            //    clause += '(' + fcfi2.Name + " <= " + heightmax + " and " + fcfi2.Name + " >= " + heightmin + " and " + fcfi2.Name + " > " + "0" + ')' + " or " + '(' + fcfi1.Name + " <= " + heightmax + " and " + fcfi1.Name + " >= " + heightmin + " and " + fcfi2.Name + " = " + "0" + ')';
                            //}
                            //else
                            //{
                            //    clause += '(' + fcfi1.Name + " <= " + widemax + " and " + fcfi1.Name + " >= " + widemin + ')' + " and " + '(' + '(' + fcfi2.Name + " <= " + heightmax + " and " + fcfi2.Name + " >= " + heightmin + " and " + fcfi2.Name + " > " + "0" + ')' + " or " + '(' + fcfi1.Name + " <= " + heightmax + " and " + fcfi1.Name + " >= " + heightmin + " and " + fcfi2.Name + " = " + "0" + ')' + ')';
                            //}
                            filter.WhereClause = "GroupId = " + sc.GroupId + " and ( " + clause + " )";
                            int count = fc.GetCount(filter);
                            if (count == 0) continue;
                            bool bHave = false;
                            double subfieldlength = 0.0;
                            int loop = (int)Math.Ceiling(count / 800.0);
                            for (int k = 1; k <= loop; k++)
                            {
                                if (k == 1)
                                {
                                    filter.ResultBeginIndex = 0;
                                }
                                else
                                {
                                    filter.ResultBeginIndex = (k - 1) * 800;
                                }
                                filter.ResultLimit = 800;
                                IFdeCursor cursor = null;
                                IRowBuffer row = null;
                                #region
                                try
                                {
                                    cursor = fc.Search(filter, true);
                                    while ((row = cursor.NextRow()) != null)
                                    {
                                        if (!row.IsNull(0))
                                        {
                                            object tempobj = row.GetValue(0);
                                            double dtemp = 0.0;
                                            if (tempobj != null && double.TryParse(tempobj.ToString(), out dtemp))
                                            {
                                                bHave = true;
                                                subfieldlength += dtemp;
                                            }
                                        }
                                    }
                                }
                                catch { }
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
                            }
                            if (bHave)
                            {
                                DataRow dr = dttemp.NewRow();
                                dr["PIPELINETYPE"] = sc;
                                dr["FIELDNAME"] = "规格";
                                dr["PVALUE"] = strValue;
                                subclasslength += subfieldlength;
                                dr["LENGTH"] = subfieldlength.ToString("0.00");
                                dttemp.Rows.Add(dr);
                            }
                        }
                    }
                    int indexEnd = dttemp.Rows.Count;
                    for (int i = indexStart; i < indexEnd; i++)
                    {
                        DataRow dr = dttemp.Rows[i];
                        dr["TOTALLENGTH"] = subclasslength.ToString("0.00");
                    }
                }
            }
            return dttemp;
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            try
            {
                WaitForm.Start("正在统计...", "请稍后", false);
                DataTable dt = DoStats();
                if (dt == null || dt.Rows.Count == 0)
                {
                    WaitForm.Stop();
                    XtraMessageBox.Show("统计结果为空！", "提示");
                    return;
                }
                WaitForm.Stop();
                FrmPipeLineStatsOutput dlg = new FrmPipeLineStatsOutput();
                dlg.SetData(dt);
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                WaitForm.Stop();
                XtraMessageBox.Show("统计出错！", "提示");
            }
        }


    }
}
