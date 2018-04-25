using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using DFDataConfig.Class;
using Gvitech.CityMaker.Common;
using System.IO;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.FdeGeometry;
using DFCommon.Class;
using DFWinForms.Class;

namespace DF3DPipeCreateTool.UC
{
    public class UCPipeSet : XtraUserControl
    {
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcFacClassType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStyleMap;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.GridControl gridStyleMap;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ComboBoxEdit cbxStyleField;
        private SplitContainerControl splitContainerControl1;
        private GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridFieldMap;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFieldMap;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcbxFieldMap;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private GroupControl groupControl2;
        private LabelControl lblB3;
        private LabelControl lblB2;
        private LabelControl labelControl6;
        private LabelControl lblA3;
        private LabelControl lblA2;
        private LabelControl labelControl1;
        private SimpleButton btnConfirm;
        private CheckEdit chkSelectAll;
        private DevExpress.XtraTreeList.TreeList treeLayer;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcTempLayer;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcFacClass;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcbxFacilityClass;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcRecordCount;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcTempLayerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcbxStyleMap;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcFacClassMap;

        private void InitializeComponent()
        {
            this.tcFacClassType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gvStyleMap = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcbxStyleMap = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridStyleMap = new DevExpress.XtraGrid.GridControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbxStyleField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridFieldMap = new DevExpress.XtraGrid.GridControl();
            this.gvFieldMap = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcbxFieldMap = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lblB3 = new DevExpress.XtraEditors.LabelControl();
            this.lblB2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblA3 = new DevExpress.XtraEditors.LabelControl();
            this.lblA2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.chkSelectAll = new DevExpress.XtraEditors.CheckEdit();
            this.treeLayer = new DevExpress.XtraTreeList.TreeList();
            this.tcTempLayerName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcFacClass = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rcbxFacilityClass = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.tcRecordCount = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcTempLayer = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcFacClassMap = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvStyleMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcbxStyleMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStyleMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStyleField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFieldMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFieldMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcbxFieldMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcbxFacilityClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // tcFacClassType
            // 
            this.tcFacClassType.Caption = "设施类类型";
            this.tcFacClassType.FieldName = "tcFacClassType";
            this.tcFacClassType.Name = "tcFacClassType";
            this.tcFacClassType.OptionsColumn.AllowEdit = false;
            this.tcFacClassType.OptionsColumn.AllowFocus = false;
            this.tcFacClassType.OptionsColumn.AllowSort = false;
            this.tcFacClassType.Visible = true;
            this.tcFacClassType.VisibleIndex = 2;
            this.tcFacClassType.Width = 102;
            // 
            // gvStyleMap
            // 
            this.gvStyleMap.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gvStyleMap.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvStyleMap.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.gvStyleMap.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
            this.gvStyleMap.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvStyleMap.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvStyleMap.GridControl = this.gridStyleMap;
            this.gvStyleMap.Name = "gvStyleMap";
            this.gvStyleMap.OptionsView.ShowColumnHeaders = false;
            this.gvStyleMap.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "分类名称";
            this.gridColumn1.FieldName = "StyleField";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "风格名称";
            this.gridColumn2.ColumnEdit = this.rcbxStyleMap;
            this.gridColumn2.FieldName = "FacilityStyle";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // rcbxStyleMap
            // 
            this.rcbxStyleMap.AutoHeight = false;
            this.rcbxStyleMap.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcbxStyleMap.DropDownRows = 15;
            this.rcbxStyleMap.Name = "rcbxStyleMap";
            this.rcbxStyleMap.SelectedIndexChanged += new System.EventHandler(this.rcbxStyleMap_SelectedIndexChanged);
            // 
            // gridStyleMap
            // 
            this.gridStyleMap.Location = new System.Drawing.Point(373, 71);
            this.gridStyleMap.MainView = this.gvStyleMap;
            this.gridStyleMap.Name = "gridStyleMap";
            this.gridStyleMap.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcbxStyleMap});
            this.gridStyleMap.Size = new System.Drawing.Size(203, 88);
            this.gridStyleMap.TabIndex = 11;
            this.gridStyleMap.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStyleMap});
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbxStyleField);
            this.layoutControl1.Controls.Add(this.splitContainerControl1);
            this.layoutControl1.Controls.Add(this.btnConfirm);
            this.layoutControl1.Controls.Add(this.chkSelectAll);
            this.layoutControl1.Controls.Add(this.treeLayer);
            this.layoutControl1.Controls.Add(this.gridStyleMap);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(584, 540);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbxStyleField
            // 
            this.cbxStyleField.Location = new System.Drawing.Point(438, 28);
            this.cbxStyleField.Name = "cbxStyleField";
            this.cbxStyleField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxStyleField.Properties.DropDownRows = 15;
            this.cbxStyleField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxStyleField.Size = new System.Drawing.Size(138, 22);
            this.cbxStyleField.StyleController = this.layoutControl1;
            this.cbxStyleField.TabIndex = 13;
            this.cbxStyleField.SelectedIndexChanged += new System.EventHandler(this.cbxStyleField_SelectedIndexChanged);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(373, 189);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(203, 317);
            this.splitContainerControl1.SplitterPosition = 105;
            this.splitContainerControl1.TabIndex = 12;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridFieldMap);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(203, 207);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "字段映射关系";
            // 
            // gridFieldMap
            // 
            this.gridFieldMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFieldMap.Location = new System.Drawing.Point(2, 22);
            this.gridFieldMap.MainView = this.gvFieldMap;
            this.gridFieldMap.Name = "gridFieldMap";
            this.gridFieldMap.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcbxFieldMap});
            this.gridFieldMap.Size = new System.Drawing.Size(199, 183);
            this.gridFieldMap.TabIndex = 0;
            this.gridFieldMap.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFieldMap});
            // 
            // gvFieldMap
            // 
            this.gvFieldMap.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gvFieldMap.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvFieldMap.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.gvFieldMap.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
            this.gvFieldMap.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvFieldMap.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn6});
            this.gvFieldMap.GridControl = this.gridFieldMap;
            this.gvFieldMap.Name = "gvFieldMap";
            this.gvFieldMap.OptionsView.ShowGroupPanel = false;
            this.gvFieldMap.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvFieldMap_FocusedRowChanged);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "原始数据字段";
            this.gridColumn4.FieldName = "FromFieldName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "设施类字段";
            this.gridColumn5.ColumnEdit = this.rcbxFieldMap;
            this.gridColumn5.FieldName = "ToFieldName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // rcbxFieldMap
            // 
            this.rcbxFieldMap.AutoHeight = false;
            this.rcbxFieldMap.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcbxFieldMap.Name = "rcbxFieldMap";
            this.rcbxFieldMap.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.rcbxFieldMap.SelectedIndexChanged += new System.EventHandler(this.rcbxFieldMap_SelectedIndexChanged);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "FromField";
            this.gridColumn3.FieldName = "FromField";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "ToField";
            this.gridColumn6.FieldName = "ToField";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.lblB3);
            this.groupControl2.Controls.Add(this.lblB2);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.lblA3);
            this.groupControl2.Controls.Add(this.lblA2);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(203, 105);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "字段信息";
            // 
            // lblB3
            // 
            this.lblB3.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblB3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblB3.Location = new System.Drawing.Point(2, 87);
            this.lblB3.Name = "lblB3";
            this.lblB3.Size = new System.Drawing.Size(60, 13);
            this.lblB3.TabIndex = 18;
            this.lblB3.Text = "字段类型：";
            // 
            // lblB2
            // 
            this.lblB2.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblB2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblB2.Location = new System.Drawing.Point(2, 74);
            this.lblB2.Name = "lblB2";
            this.lblB2.Size = new System.Drawing.Size(52, 13);
            this.lblB2.TabIndex = 17;
            this.lblB2.Text = "字段别名:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl6.Location = new System.Drawing.Point(2, 61);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(65, 13);
            this.labelControl6.TabIndex = 15;
            this.labelControl6.Text = "设施类字段";
            // 
            // lblA3
            // 
            this.lblA3.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblA3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblA3.Location = new System.Drawing.Point(2, 48);
            this.lblA3.Name = "lblA3";
            this.lblA3.Size = new System.Drawing.Size(60, 13);
            this.lblA3.TabIndex = 13;
            this.lblA3.Text = "字段类型：";
            // 
            // lblA2
            // 
            this.lblA2.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblA2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblA2.Location = new System.Drawing.Point(2, 35);
            this.lblA2.Name = "lblA2";
            this.lblA2.Size = new System.Drawing.Size(60, 13);
            this.lblA2.TabIndex = 12;
            this.lblA2.Text = "字段别名：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(2, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "原始数据字段";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(431, 513);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(148, 22);
            this.btnConfirm.StyleController = this.layoutControl1;
            this.btnConfirm.TabIndex = 9;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Location = new System.Drawing.Point(8, 487);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Properties.Caption = "全选\\取消全选";
            this.chkSelectAll.Size = new System.Drawing.Size(355, 19);
            this.chkSelectAll.StyleController = this.layoutControl1;
            this.chkSelectAll.TabIndex = 6;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // treeLayer
            // 
            this.treeLayer.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.treeLayer.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeLayer.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeLayer.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
            this.treeLayer.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeLayer.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tcTempLayerName,
            this.tcFacClass,
            this.tcFacClassType,
            this.tcRecordCount,
            this.tcTempLayer,
            this.tcFacClassMap});
            this.treeLayer.Location = new System.Drawing.Point(8, 28);
            this.treeLayer.Name = "treeLayer";
            this.treeLayer.OptionsView.ShowCheckBoxes = true;
            this.treeLayer.OptionsView.ShowRoot = false;
            this.treeLayer.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcbxFacilityClass});
            this.treeLayer.Size = new System.Drawing.Size(355, 455);
            this.treeLayer.TabIndex = 4;
            this.treeLayer.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeLayer_FocusedNodeChanged);
            // 
            // tcTempLayerName
            // 
            this.tcTempLayerName.Caption = "临时图层";
            this.tcTempLayerName.FieldName = "tcTempLayerName";
            this.tcTempLayerName.MinWidth = 32;
            this.tcTempLayerName.Name = "tcTempLayerName";
            this.tcTempLayerName.OptionsColumn.AllowEdit = false;
            this.tcTempLayerName.Visible = true;
            this.tcTempLayerName.VisibleIndex = 0;
            this.tcTempLayerName.Width = 83;
            // 
            // tcFacClass
            // 
            this.tcFacClass.Caption = "设施类";
            this.tcFacClass.ColumnEdit = this.rcbxFacilityClass;
            this.tcFacClass.FieldName = "tcFacClass";
            this.tcFacClass.Name = "tcFacClass";
            this.tcFacClass.Visible = true;
            this.tcFacClass.VisibleIndex = 1;
            this.tcFacClass.Width = 85;
            // 
            // rcbxFacilityClass
            // 
            this.rcbxFacilityClass.AutoHeight = false;
            this.rcbxFacilityClass.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcbxFacilityClass.DropDownRows = 27;
            this.rcbxFacilityClass.Name = "rcbxFacilityClass";
            this.rcbxFacilityClass.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.rcbxFacilityClass.SelectedIndexChanged += new System.EventHandler(this.rcbxFacilityClass_SelectedIndexChanged);
            // 
            // tcRecordCount
            // 
            this.tcRecordCount.Caption = "记录数";
            this.tcRecordCount.FieldName = "tcRecordCount";
            this.tcRecordCount.Name = "tcRecordCount";
            this.tcRecordCount.OptionsColumn.AllowEdit = false;
            this.tcRecordCount.Visible = true;
            this.tcRecordCount.VisibleIndex = 3;
            this.tcRecordCount.Width = 67;
            // 
            // tcTempLayer
            // 
            this.tcTempLayer.Caption = "临时图层";
            this.tcTempLayer.FieldName = "tcTempLayer";
            this.tcTempLayer.MinWidth = 32;
            this.tcTempLayer.Name = "tcTempLayer";
            this.tcTempLayer.OptionsColumn.AllowEdit = false;
            this.tcTempLayer.OptionsColumn.AllowFocus = false;
            this.tcTempLayer.OptionsColumn.AllowSort = false;
            this.tcTempLayer.Width = 111;
            // 
            // tcFacClassMap
            // 
            this.tcFacClassMap.Caption = "对照表";
            this.tcFacClassMap.FieldName = "FacClassMap";
            this.tcFacClassMap.Name = "tcFacClassMap";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(584, 540);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnConfirm;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(426, 508);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(152, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 508);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(426, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "选择图层";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(365, 508);
            this.layoutControlGroup2.Text = "选择图层";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeLayer;
            this.layoutControlItem1.CustomizationFormText = "选择图层";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(359, 459);
            this.layoutControlItem1.Text = "选择图层";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkSelectAll;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 459);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(359, 23);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "风格设置";
            this.layoutControlGroup3.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup3.ExpandButtonVisible = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem7});
            this.layoutControlGroup3.Location = new System.Drawing.Point(365, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(213, 161);
            this.layoutControlGroup3.Text = "风格设置";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridStyleMap;
            this.layoutControlItem2.CustomizationFormText = "风格映射关系";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(207, 109);
            this.layoutControlItem2.Text = "风格指定关系";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cbxStyleField;
            this.layoutControlItem7.CustomizationFormText = "分类字段：";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem7.Text = "分类字段：";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Default;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "字段设置";
            this.layoutControlGroup4.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup4.ExpandButtonVisible = true;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup4.Location = new System.Drawing.Point(365, 161);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(213, 347);
            this.layoutControlGroup4.Text = "字段设置";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.splitContainerControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(207, 321);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // UCPipeSet
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCPipeSet";
            this.Size = new System.Drawing.Size(584, 540);
            ((System.ComponentModel.ISupportInitialize)(this.gvStyleMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcbxStyleMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStyleMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbxStyleField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFieldMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFieldMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcbxFieldMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcbxFacilityClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }
        private class FacClassMap
        {
            public DataTable dtStyleMap;
            public DataTable dtFieldMap;
            public IFeatureClass fcFrom;
            public IFeatureClass fcTo;
            public string groupFieldName;
            public FacClassMap()
            {
                this.dtStyleMap = new DataTable();
                this.dtFieldMap = new DataTable();
            }
        }
        private IDataSource _dsTemp;
        private IDataSource _dsPipe;
        private IDataSource _dsTemplate;
        private DataTable _dtStyleMap;
        private DataTable _dtFieldMap;
        
        public UCPipeSet()
        {
            InitializeComponent();
            this._dtStyleMap = new DataTable("StyleMap");
            this._dtStyleMap.Columns.Add("StyleField", typeof(string));
            this._dtStyleMap.Columns.Add("FacilityStyle", typeof(object));
            this.gridStyleMap.DataSource = this._dtStyleMap;

            this._dtFieldMap = new DataTable("FieldMap");
            this._dtFieldMap.Columns.Add("FromFieldName", typeof(string));
            this._dtFieldMap.Columns.Add("ToFieldName", typeof(string));
            this._dtFieldMap.Columns.Add("FromField", typeof(object));
            this._dtFieldMap.Columns.Add("ToField", typeof(object));
            this.gridFieldMap.DataSource = this._dtFieldMap;
        }

        public void Init()
        {
            try
            {
                this._dsTemp = DF3DPipeCreateApp.App.TempLib;
                if (this._dsTemp == null) { this.Enabled = false; return; }
                this._dsPipe = DF3DPipeCreateApp.App.PipeLib;
                if (this._dsPipe == null) { this.Enabled = false; return; }
                this._dsTemplate = DF3DPipeCreateApp.App.TemplateLib;
                if (this._dsTemplate == null) { this.Enabled = false; return; }

                WaitForm.Start("正在加载数据...", "请稍后");

                this.treeLayer.ClearNodes();
                List<IFeatureClass> list = GetAllTempFeatureClass();
                if (list != null)
                {
                    foreach (IFeatureClass fc in list)
                    {
                        IQueryFilter filter = new QueryFilter();
                        filter.WhereClause = "1=1";
                        int count = fc.GetCount(filter);
                        object[] nodeData = new object[6];
                        nodeData[0] = fc.AliasName;
                        nodeData[3] = count;
                        nodeData[4] = fc;
                        FacClassMap fcm = new FacClassMap();
                        fcm.fcFrom = fc;
                        nodeData[5] = fcm; 
                        this.treeLayer.AppendNode(nodeData, null);
                    }
                }

                this.rcbxFacilityClass.Items.Clear();
                List<FacClassReg> list1 = GetAllFacClassReg();
                foreach (FacClassReg fc in list1)
                {
                    this.rcbxFacilityClass.Items.Add(fc);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                WaitForm.Stop();
            }
        }

        private List<IFeatureClass> GetAllTempFeatureClass()
        {
            if (this._dsTemp == null) return null;
            try
            {
                string[] fdsNames = this._dsTemp.GetFeatureDatasetNames();
                if (fdsNames != null)
                {
                    List<IFeatureClass> list = new List<IFeatureClass>();
                    foreach (string fdsName in fdsNames)
                    {
                        IFeatureDataSet fds = this._dsTemp.OpenFeatureDataset(fdsName);
                        string[] fcNames = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                        if (fcNames != null)
                        {
                            foreach (string fcName in fcNames)
                            {
                                IFeatureClass fc = fds.OpenFeatureClass(fcName);
                                list.Add(fc);
                            }
                        }
                    }
                    return list;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<FacClassReg> GetAllFacClassReg()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "1=1";

                cursor = oc.Search(filter, false);
                List<FacClassReg> list = new List<FacClassReg>();
                while ((row = cursor.NextRow()) != null)
                {
                    FacClassReg fc = new FacClassReg();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fc.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("DataSetName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("DataSetName"));
                        if (obj != null) fc.DataSetName = obj.ToString();
                    }
                    if (row.FieldIndex("FeatureClassId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FeatureClassId"));
                        if (obj != null) fc.FeatureClassId = obj.ToString();
                    }
                    if (row.FieldIndex("FcName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FcName"));
                        if (obj != null) fc.FcName = obj.ToString();
                    }
                    if (row.FieldIndex("DataType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("DataType"));
                        if (obj != null)
                        {
                            DataLifeCyle ts = 0;
                            if (Enum.TryParse<DataLifeCyle>(obj.ToString(), out ts))
                                fc.DataType = ts;
                            else fc.DataType = 0;
                        }
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) fc.Comment = obj.ToString();
                    }
                    if (row.FieldIndex("TurnerStyle") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TurnerStyle"));
                        if (obj != null)
                        {
                            TurnerStyle ts = 0;
                            if (Enum.TryParse<TurnerStyle>(obj.ToString(), out ts))
                                fc.TurnerStyle = ts;
                            else fc.TurnerStyle = 0;
                        }
                    }
                    if (row.FieldIndex("FacilityType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacilityType"));
                        if (obj != null) fc.FacilityType = FacilityClassManager.Instance.GetFacilityClassByName(obj.ToString());
                    }
                    if (row.FieldIndex("LocationType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("LocationType"));
                        if (obj != null)
                        {
                            LocationType lt = 0;
                            if (Enum.TryParse<LocationType>(obj.ToString(), out lt))
                                fc.LocationType = lt;
                            else fc.LocationType = 0;
                        }
                    }
                    list.Add(fc);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
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
        }

        private void rcbxFacilityClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._dtFieldMap != null)
            {
                foreach (DataRow row in this._dtFieldMap.Rows)
                {
                    row["ToFieldName"] = "";
                    row["ToField"] = null;
                }
            }
            if (this.treeLayer.FocusedNode == null) return;
            ComboBoxEdit edit = sender as ComboBoxEdit;
            FacClassReg fcr = edit.SelectedItem as FacClassReg;
            if (fcr == null) return;
            this.treeLayer.FocusedNode.SetValue("tcFacClassType", fcr.FacilityType);
            // 字段信息
            this.rcbxFieldMap.Items.Clear();
            IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset(fcr.DataSetName);
            if (fds != null)
            {
                IFeatureClass fc = fds.OpenFeatureClass(fcr.FcName);
                if (fc != null)
                {
                    IFieldInfoCollection allFields = fc.GetFields();
                    for (int i = 0; i < allFields.Count; i++)
                    {
                        IFieldInfo fi = allFields.Get(i);
                        this.rcbxFieldMap.Items.Add(fi.Name);
                    }
                    foreach (DataRow row in this._dtFieldMap.Rows)
                    {
                        object obj = row["FromFieldName"];
                        if (obj == null) continue;
                        for (int i = 0; i < allFields.Count;i++ )
                        {
                            IFieldInfo fi = allFields.Get(i);
                            if (fi.Name == obj.ToString())
                            {
                                row["ToFieldName"] = fi.Name;
                                row["ToField"] = fi;
                                break;
                            }
                        }
                    }

                    object objfcm = this.treeLayer.FocusedNode.GetValue("FacClassMap");
                    if (objfcm != null && objfcm is FacClassMap)
                    {
                        FacClassMap fcm = objfcm as FacClassMap;
                        fcm.fcTo = fc;
                        fcm.dtFieldMap.Clear();
                        fcm.dtFieldMap = this._dtFieldMap.Copy();
                    }
                }
            }
            // 样式信息
            this.rcbxStyleMap.Items.Clear();
            List<FacStyleClass> list1 = GetFacStyleByFacClassCode(fcr.FacClassCode);
            if (list1 != null)
            {
                ImageCollection imageList = new ImageCollection();
                this.rcbxStyleMap.LargeImages = imageList;
                imageList.ImageSize = new Size(48, 48);
                foreach (FacStyleClass fsc in list1)
                {
                    int imageIndex = imageList.Images.Add(fsc.Thumbnail);
                    ImageComboBoxItem item = new ImageComboBoxItem();
                    item.Value = fsc;
                    item.Description = fsc.ToString();
                    item.ImageIndex = imageIndex;
                    this.rcbxStyleMap.Items.Add(item);
                }
            }
        }

        private void rcbxFieldMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.treeLayer.FocusedNode == null) return;
            object obj = this.treeLayer.FocusedNode.GetValue("tcFacClass");
            if (obj != null && obj is FacClassReg)
            {
                FacClassReg fcr = obj as FacClassReg;
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset(fcr.DataSetName);
                if (fds != null)
                {
                    IFeatureClass fc = fds.OpenFeatureClass(fcr.FcName);
                    if (fc != null)
                    {
                        IFieldInfoCollection allFields = fc.GetFields();
                        ComboBoxEdit edit = sender as ComboBoxEdit;
                        for (int i = 0; i < allFields.Count; i++)
                        {
                            IFieldInfo fi = allFields.Get(i);
                            if (fi.Name == edit.Text)
                            {
                                this.lblB2.Text = "字段别名：" + fi.Alias;
                                this.lblB3.Text = "字段类型：" + fi.FieldType.ToString();
                                DataRow dr = this.gvFieldMap.GetDataRow(this.gvFieldMap.FocusedRowHandle);
                                if (dr != null)
                                {
                                    dr["ToFieldName"] = fi.Name;
                                    dr["ToField"] = fi;
                                    this.gvFieldMap.RefreshData();
                                }

                                object objfcm = this.treeLayer.FocusedNode.GetValue("FacClassMap");
                                if (objfcm != null && objfcm is FacClassMap)
                                {
                                    FacClassMap fcm = objfcm as FacClassMap;
                                    fcm.fcTo = fc;
                                    fcm.dtFieldMap.Clear();
                                    fcm.dtFieldMap = this._dtFieldMap.Copy();
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void rcbxStyleMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = this.gvStyleMap.GetDataRow(this.gvStyleMap.FocusedRowHandle);
            if (dr != null)
            {
                ImageComboBoxEdit edit = sender as ImageComboBoxEdit;
                if (edit != null)
                {
                    dr["FacilityStyle"] = edit.EditValue;
                    this.gvStyleMap.RefreshData();
                }
            }
            if (this.treeLayer.FocusedNode != null)
            {
                object objfcm = this.treeLayer.FocusedNode.GetValue("FacClassMap");
                if (objfcm != null && objfcm is FacClassMap)
                {
                    FacClassMap fcm = objfcm as FacClassMap;
                    fcm.dtStyleMap.Clear();
                    fcm.dtStyleMap = this._dtStyleMap.Copy();
                }
            }
        }
   
        private List<FacStyleClass> GetFacStyleByFacClassCode(string fcCode)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityStyle");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = string.Format("FacClassCode = '{0}'", fcCode)
                };
                cursor = oc.Search(filter, true);
                List<FacStyleClass> list = new List<FacStyleClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    StyleType type;
                    FacStyleClass fs = null;
                    if (row.FieldIndex("StyleType") >= 0 && Enum.TryParse<StyleType>(row.GetValue(row.FieldIndex("StyleType")).ToString(), out type))
                    {
                        Dictionary<string, string> dictionary = null;
                        if (row.FieldIndex("StyleInfo") >= 0)
                        {
                            object obj = row.GetValue(row.FieldIndex("StyleInfo"));
                            if (obj != null)
                            {
                                IBinaryBuffer buffer2 = row.GetValue(row.FieldIndex("StyleInfo")) as IBinaryBuffer;
                                if (buffer2 != null)
                                {
                                    dictionary = JsonTool.JsonToObject<Dictionary<string, string>>(Encoding.UTF8.GetString(buffer2.AsByteArray()));
                                }
                            }
                        }
                        switch (type)
                        {
                            case StyleType.PipeNodeStyle:
                                fs = new PipeNodeStyleClass(dictionary);
                                break;
                            case StyleType.PipeLineStyle:
                                fs = new PipeLineStyleClass(dictionary);
                                break;
                            case StyleType.PipeBuildStyle:
                                fs = new PipeBuildStyleClass(dictionary);
                                break;
                        }
                    }
                    if (fs == null) continue;
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fs.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ObjectId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ObjectId"));
                        if (obj != null) fs.ObjectId = obj.ToString();
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fs.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fs.Name = obj.ToString();
                    }
                    int index = row.FieldIndex("Thumbnail");
                    if (index != -1 && !row.IsNull(index))
                    {
                        IBinaryBuffer b = row.GetValue(index) as IBinaryBuffer;
                        if (row != null)
                        {
                            MemoryStream stream = new MemoryStream(b.AsByteArray());
                            fs.Thumbnail = Image.FromStream(stream);
                        }
                    }
                    list.Add(fs);
                }
                return list;
            }
            catch (Exception exception)
            {
                return null;
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
        }

        private void treeLayer_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            this.cbxStyleField.Properties.Items.Clear();
            this.cbxStyleField.Text = "";
            this._dtFieldMap.Clear();
            this._dtStyleMap.Clear();
            this.rcbxFieldMap.Items.Clear();
            this.rcbxStyleMap.Items.Clear();

            if (this.treeLayer.FocusedNode == null) return;
            object obj = this.treeLayer.FocusedNode.GetValue("tcTempLayer");
            if (obj != null && obj is IFeatureClass)
            {
                IFeatureClass fc = obj as IFeatureClass;
                //
                IFieldInfoCollection allFields = fc.GetFields();
                if (allFields == null || allFields.Count == 0) return;
                for (int i = 0; i < allFields.Count; i++)
                {
                    IFieldInfo fi = allFields.Get(i);
                    this.cbxStyleField.Properties.Items.Add(fi.Name);
                    DataRow dr = this._dtFieldMap.NewRow();
                    dr["FromFieldName"] = fi.Name;
                    dr["FromField"] = fi;
                    this._dtFieldMap.Rows.Add(dr);
                }
            }
            //已有信息加载
            object objfcm = this.treeLayer.FocusedNode.GetValue("FacClassMap");
            if (objfcm != null && objfcm is FacClassMap)
            {
                FacClassMap fcm = objfcm as FacClassMap;
                // 字段信息
                this.rcbxFieldMap.Items.Clear();
                if (fcm.fcTo != null)
                {
                    IFieldInfoCollection allFields = fcm.fcTo.GetFields();
                    for (int i = 0; i < allFields.Count; i++)
                    {
                        IFieldInfo fi = allFields.Get(i);
                        this.rcbxFieldMap.Items.Add(fi.Name);
                    }
                }
                if (fcm.dtFieldMap != null && fcm.dtFieldMap.Rows.Count > 0)
                {
                    this._dtFieldMap.Clear();
                    foreach (DataRow dr in fcm.dtFieldMap.Rows)
                    {
                        DataRow row = this._dtFieldMap.NewRow();
                        row["FromField"] = dr["FromField"];
                        row["FromFieldName"] = dr["FromFieldName"];
                        row["ToField"] = dr["ToField"];
                        row["ToFieldName"] = dr["ToFieldName"];
                        this._dtFieldMap.Rows.Add(row);
                    }
                }
                // 风格信息
                this.cbxStyleField.Text = fcm.groupFieldName;
                this.rcbxStyleMap.Items.Clear();
                object objfc = this.treeLayer.FocusedNode.GetValue("tcFacClass");
                if(objfc != null && objfc is FacClassReg)
                {
                    FacClassReg facC = objfc as FacClassReg;
                    List<FacStyleClass> list1 = GetFacStyleByFacClassCode(facC.FacClassCode);
                    if (list1 != null)
                    {
                        ImageCollection imageList = new ImageCollection();
                        this.rcbxStyleMap.LargeImages = imageList;
                        imageList.ImageSize = new Size(48, 48);
                        foreach (FacStyleClass fsc in list1)
                        {
                            int imageIndex = imageList.Images.Add(fsc.Thumbnail);
                            ImageComboBoxItem item = new ImageComboBoxItem();
                            item.Value = fsc;
                            item.Description = fsc.ToString();
                            item.ImageIndex = imageIndex;
                            this.rcbxStyleMap.Items.Add(item);
                        }
                    }
                }

                if (fcm.dtStyleMap != null && fcm.dtStyleMap.Rows.Count > 0)
                {
                    this._dtStyleMap.Clear();
                    int icount = 0;
                    foreach (DataRow dr in fcm.dtStyleMap.Rows)
                    {
                        DataRow row = this._dtStyleMap.NewRow();
                        row["StyleField"] = dr["StyleField"];
                        row["FacilityStyle"] = dr["FacilityStyle"];
                        if (dr["FacilityStyle"] != null && dr["FacilityStyle"] is FacStyleClass)
                        {
                            FacStyleClass fsc = dr["FacilityStyle"] as FacStyleClass;
                            foreach (ImageComboBoxItem imgitem in this.rcbxStyleMap.Items)
                            {
                                if (imgitem.Value != null & imgitem.Value is FacStyleClass)
                                {
                                    if (fsc.ObjectId == (imgitem.Value as FacStyleClass).ObjectId)
                                    {
                                        this.gvStyleMap.SetRowCellValue(icount, gridColumn2, imgitem);
                                        break;
                                    }
                                }
                            }
                        }
                        this.gvStyleMap.RefreshRowCell(icount, gridColumn2);
                        icount++;
                        this._dtStyleMap.Rows.Add(row);
                    }
                }
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSelectAll.Checked) this.treeLayer.CheckAll();
            else this.treeLayer.UncheckAll();
        }

        private void gvFieldMap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = this.gvFieldMap.GetDataRow(this.gvFieldMap.FocusedRowHandle);
            if (dr != null)
            {
                IFieldInfo fiFrom = dr["FromField"] as IFieldInfo;
                if (fiFrom != null)
                {
                    this.lblA2.Text = "字段别名：" + fiFrom.Alias;
                    this.lblA3.Text = "字段类型：" + fiFrom.FieldType.ToString();
                }
                else
                {
                    this.lblA2.Text = "字段别名：";
                    this.lblA3.Text = "字段类型：";
                }
                IFieldInfo fiTo = dr["ToField"] as IFieldInfo;
                if (fiTo != null)
                {
                    this.lblB2.Text = "字段别名：" + fiTo.Alias;
                    this.lblB3.Text = "字段类型：" + fiFrom.FieldType.ToString();
                }
                else
                {
                    this.lblB2.Text = "字段别名：";
                    this.lblB3.Text = "字段类型：";
                }
            }

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<TreeListNode> list = this.treeLayer.GetAllCheckedNodes();
            if (list != null && list.Count > 0)
            {
                WaitForm.Start("启动管线指定...", "请稍后", new Size(340, 50));
                foreach (TreeListNode node in list)
                {
                    object objFacClassMap = node.GetValue("FacClassMap");
                    if (objFacClassMap != null && objFacClassMap is FacClassMap)
                    {
                        FacClassMap fcm = objFacClassMap as FacClassMap;
                        WaitForm.SetCaption("正在进行【" + fcm.fcFrom.AliasName + "】数据指定...", "请稍后");
                        MoveData(fcm.fcFrom, fcm.fcTo, fcm.dtFieldMap, fcm.dtStyleMap, fcm.groupFieldName);
                    }
                }
                WaitForm.Stop();
            }
            else
            {
                XtraMessageBox.Show("请勾选需要做设施指定的图层！", "提示");
            }
        }

        private bool MoveData(IFeatureClass fcFrom, IFeatureClass fcTo, DataTable dtFieldMap, DataTable dtStyleMap, string groupFieldName)
        {
            if (fcFrom == null || fcTo == null) return false;

            IFdeCursor cursorFrom = null;
            IRowBuffer rowFrom = null;
            IFdeCursor cursorTo = null;
            IRowBuffer rowTo = null;
            try
            {
                // 删除目标要素类所有数据
                IQueryFilter filterTo = new QueryFilterClass();
                filterTo.WhereClause = "1=1";
                WaitForm.SetCaption("正在删除【" + (string.IsNullOrEmpty(fcTo.AliasName) ? fcTo.Name : fcTo.AliasName) + "】数据...");
                fcTo.Delete(filterTo);

                IFieldInfoCollection allFITo = fcTo.GetFields();
                IQueryFilter filterFrom = new QueryFilterClass();
                filterFrom.WhereClause = "1=1";
                int count = fcFrom.GetCount(filterFrom);
                if (count == 0) return true;
                int icount = 0;
                int isuccesscount = 0;
                cursorFrom = fcFrom.Search(filterFrom, true);
                while ((rowFrom = cursorFrom.NextRow()) != null)
                {
                    rowTo = fcTo.CreateRowBuffer();
                    bool bHave = false;
                    #region 字段设置
                    foreach (DataRow dr in dtFieldMap.Rows)
                    {
                        IFieldInfo fieldFrom = dr["FromField"] as IFieldInfo;
                        IFieldInfo fieldTo = dr["ToField"] as IFieldInfo;
                        if (fieldFrom == null || fieldTo == null || fieldFrom.FieldType != fieldTo.FieldType) continue;
                        
                        if (fieldFrom.FieldType == gviFieldType.gviFieldGeometry && fieldTo.FieldType == gviFieldType.gviFieldGeometry)
                        {
                            if (fieldFrom.GeometryDef.GeometryColumnType != fieldTo.GeometryDef.GeometryColumnType) continue;
                        }
                        string fieldNameFrom = dr["FromFieldName"].ToString();
                        string fieldNameTo = dr["ToFieldName"].ToString();
                        int indexFrom = rowFrom.FieldIndex(fieldNameFrom);
                        int indexTo = allFITo.IndexOf(fieldNameTo);
                        if (indexFrom >= 0 && indexTo >= 0)
                        {
                            if (!rowFrom.IsNull(indexFrom)) rowTo.SetValue(indexTo, rowFrom.GetValue(indexFrom));
                            else
                            {
                                if (fieldTo.Nullable)//字段可为空，设置为空
                                    rowTo.SetValue(indexTo, null);
                                else// 不能为空
                                {
                                    switch (fieldTo.FieldType)
                                    {
                                        case gviFieldType.gviFieldDate:
                                            rowTo.SetValue(indexTo, DateTime.Now);
                                            break;
                                        case gviFieldType.gviFieldUUID:
                                        case gviFieldType.gviFieldDouble:
                                        case gviFieldType.gviFieldFID:
                                        case gviFieldType.gviFieldFloat:
                                        case gviFieldType.gviFieldInt16:
                                        case gviFieldType.gviFieldInt32:
                                        case gviFieldType.gviFieldInt64:
                                            rowTo.SetValue(indexTo, 1);
                                            break;
                                        case gviFieldType.gviFieldString:
                                            rowTo.SetValue(indexTo, "null");
                                            break;
                                        default:
                                            continue;
                                    }

                                }
                            }
                            bHave = true;
                        }
                        if (fieldNameFrom == groupFieldName)
                        {
                            int styleIndex = rowTo.FieldIndex("StyleId");
                            if (styleIndex >= 0)
                            {
                                rowTo.SetValue(styleIndex, "-1");
                                object styleFieldValue = rowFrom.GetValue(rowFrom.FieldIndex(fieldNameFrom));
                                if (styleFieldValue != null) 
                                {
                                    foreach (DataRow drStyle in dtStyleMap.Rows)
                                    {
                                        string styleField = drStyle["StyleField"].ToString();
                                        if (styleField == styleFieldValue.ToString())
                                        {
                                            object item = drStyle["FacilityStyle"];
                                            if (item != null && item is FacStyleClass)
                                            {
                                                FacStyleClass fsc = item as FacStyleClass; 
                                                rowTo.SetValue(styleIndex, fsc.ObjectId);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    if (bHave)
                    {
                        int indexTo = allFITo.IndexOf("FacilityId");
                        if (indexTo >= 0) rowTo.SetValue(indexTo, BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant());
                        int indexFrom = rowFrom.FieldIndex("Geometry");
                        if (indexFrom >= 0)
                        {
                            IGeometry geometry = rowFrom.GetValue(indexFrom) as IGeometry;
                            if (geometry != null)
                            {
                                indexTo = allFITo.IndexOf("Shape");
                                if (indexTo >= 0) rowTo.SetValue(indexTo, geometry.Clone2(gviVertexAttribute.gviVertexAttributeZ));
                                indexTo = allFITo.IndexOf("FootPrint");
                                if (indexTo >= 0) rowTo.SetValue(indexTo, geometry.Clone2(gviVertexAttribute.gviVertexAttributeNone));
                            }
                        }
                        cursorTo = fcTo.Insert();
                        cursorTo.InsertRow(rowTo);
                        isuccesscount++;
                    }
                    if (cursorTo != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(cursorTo);
                        cursorTo = null;
                    }
                    if (rowTo != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(rowTo);
                        rowTo = null;
                    } 
                    icount++;
                    WaitForm.SetCaption("正在进行【" + fcFrom.AliasName + "】数据指定(" + isuccesscount + "/" + icount + "/" + count + ")...");
                }
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("【" + fcFrom.AliasName + "】数据指定出错。\r\n错误信息：" + ex.Message, "提示");
                return false;
            }
            finally
            {
                if (cursorFrom != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursorFrom);
                    cursorFrom = null;
                }
                if (rowFrom != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(rowFrom);
                    rowFrom = null;
                }
                if (cursorTo != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursorTo);
                    cursorTo = null;
                }
                if (rowTo != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(rowTo);
                    rowTo = null;
                }
            }
        }

        private void cbxStyleField_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._dtStyleMap.Rows.Clear();
            object objStyleField = this.cbxStyleField.EditValue;
            if (objStyleField == null) return;
            string styleFieldName = objStyleField.ToString();
            if (this.treeLayer.FocusedNode == null) return;
            object obj = this.treeLayer.FocusedNode.GetValue("tcTempLayer");
            if (obj != null && obj is IFeatureClass && !string.IsNullOrEmpty(styleFieldName))
            {
                try
                {
                    IFeatureClass fc = obj as IFeatureClass;
                    IQueryFilter filter = new QueryFilter();
                    filter.SubFields = styleFieldName;
                    filter.PostfixClause = "Group By " + styleFieldName;
                    IFdeCursor cursor = null;
                    IRowBuffer row = null;
                    cursor = fc.Search(filter, false);
                    while ((row = cursor.NextRow()) != null)
                    {
                        if (!row.IsNull(0))
                        {
                            DataRow dr = this._dtStyleMap.NewRow();
                            dr["StyleField"] = row.GetValue(0).ToString();
                            this._dtStyleMap.Rows.Add(dr);
                        }
                    }
                    object objFacClassMap = this.treeLayer.FocusedNode.GetValue("FacClassMap");
                    if (objFacClassMap != null && objFacClassMap is FacClassMap)
                    {
                        FacClassMap fcm = objFacClassMap as FacClassMap;
                        fcm.groupFieldName = this.cbxStyleField.Text;
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

    }
}