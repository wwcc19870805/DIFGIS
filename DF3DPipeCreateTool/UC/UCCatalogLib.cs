using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeCore;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DFDataConfig.Class;
using System.Reflection;
using Gvitech.CityMaker.Common;
using System.IO;
using System.Runtime.InteropServices;
using DF3DPipeCreateTool.Frm;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.XtraLayout.Utils;
using Gvitech.CityMaker.FdeDataInterop;
using DFCommon.Class;
using DFWinForms.Class;

namespace DF3DPipeCreateTool.UC
{
    public class UCCatalogLib : XtraUserControl
    {

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCatalogLib));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnImportShp = new DevExpress.XtraEditors.SimpleButton();
            this.cmbFacilityClassUnderOrOver = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnLaunchDFDataConfig = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.treeCatalog = new DevExpress.XtraTreeList.TreeList();
            this.tl_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_Object = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gridFields = new DevExpress.XtraGrid.GridControl();
            this.gvFields = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteStyle = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditStyle = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreateStyle = new DevExpress.XtraEditors.SimpleButton();
            this.gridStyles = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.colName = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutViewField_colName = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colStyleType = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colComment = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colThumbnail = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.layoutViewField_colPicture = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colStyleInfo = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.teFacilityClassName = new DevExpress.XtraEditors.TextEdit();
            this.cmbFacilityClass3DStyle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbFacilityClassTopo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbFacilityClassType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFacilityClassUnderOrOver.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeCatalog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStyles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFacilityClassName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFacilityClass3DStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFacilityClassTopo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFacilityClassType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnImportShp);
            this.layoutControl1.Controls.Add(this.cmbFacilityClassUnderOrOver);
            this.layoutControl1.Controls.Add(this.btnLaunchDFDataConfig);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.treeCatalog);
            this.layoutControl1.Controls.Add(this.gridFields);
            this.layoutControl1.Controls.Add(this.btnDeleteStyle);
            this.layoutControl1.Controls.Add(this.btnEditStyle);
            this.layoutControl1.Controls.Add(this.btnCreateStyle);
            this.layoutControl1.Controls.Add(this.gridStyles);
            this.layoutControl1.Controls.Add(this.teFacilityClassName);
            this.layoutControl1.Controls.Add(this.cmbFacilityClass3DStyle);
            this.layoutControl1.Controls.Add(this.cmbFacilityClassTopo);
            this.layoutControl1.Controls.Add(this.cmbFacilityClassType);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(545, 457);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnImportShp
            // 
            this.btnImportShp.Location = new System.Drawing.Point(355, 134);
            this.btnImportShp.Name = "btnImportShp";
            this.btnImportShp.Size = new System.Drawing.Size(180, 22);
            this.btnImportShp.StyleController = this.layoutControl1;
            this.btnImportShp.TabIndex = 13;
            this.btnImportShp.Text = "导入shp匹配字段类型";
            this.btnImportShp.Click += new System.EventHandler(this.btnImportShp_Click);
            // 
            // cmbFacilityClassUnderOrOver
            // 
            this.cmbFacilityClassUnderOrOver.Location = new System.Drawing.Point(247, 82);
            this.cmbFacilityClassUnderOrOver.Name = "cmbFacilityClassUnderOrOver";
            this.cmbFacilityClassUnderOrOver.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFacilityClassUnderOrOver.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFacilityClassUnderOrOver.Size = new System.Drawing.Size(118, 22);
            this.cmbFacilityClassUnderOrOver.StyleController = this.layoutControl1;
            this.cmbFacilityClassUnderOrOver.TabIndex = 3;
            // 
            // btnLaunchDFDataConfig
            // 
            this.btnLaunchDFDataConfig.Location = new System.Drawing.Point(172, 285);
            this.btnLaunchDFDataConfig.Name = "btnLaunchDFDataConfig";
            this.btnLaunchDFDataConfig.Size = new System.Drawing.Size(363, 22);
            this.btnLaunchDFDataConfig.StyleController = this.layoutControl1;
            this.btnLaunchDFDataConfig.TabIndex = 7;
            this.btnLaunchDFDataConfig.Text = "启动设施类管理程序";
            this.btnLaunchDFDataConfig.Click += new System.EventHandler(this.btnLaunchDFDataConfig_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(365, 428);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(173, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // treeCatalog
            // 
            this.treeCatalog.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeCatalog.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeCatalog.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeCatalog.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeCatalog.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
            this.treeCatalog.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeCatalog.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tl_Name,
            this.tl_Object});
            this.treeCatalog.Location = new System.Drawing.Point(10, 30);
            this.treeCatalog.Name = "treeCatalog";
            this.treeCatalog.OptionsBehavior.Editable = false;
            this.treeCatalog.OptionsView.ShowColumns = false;
            this.treeCatalog.OptionsView.ShowRoot = false;
            this.treeCatalog.Size = new System.Drawing.Size(152, 417);
            this.treeCatalog.TabIndex = 0;
            this.treeCatalog.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeCatalog_FocusedNodeChanged);
            this.treeCatalog.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeCatalog_MouseUp);
            // 
            // tl_Name
            // 
            this.tl_Name.Caption = "名称";
            this.tl_Name.FieldName = "Name";
            this.tl_Name.MinWidth = 49;
            this.tl_Name.Name = "tl_Name";
            this.tl_Name.OptionsColumn.AllowEdit = false;
            this.tl_Name.OptionsColumn.AllowSort = false;
            this.tl_Name.OptionsFilter.AllowFilter = false;
            this.tl_Name.Visible = true;
            this.tl_Name.VisibleIndex = 0;
            this.tl_Name.Width = 130;
            // 
            // tl_Object
            // 
            this.tl_Object.Caption = "Object";
            this.tl_Object.FieldName = "Object";
            this.tl_Object.Name = "tl_Object";
            // 
            // gridFields
            // 
            this.gridFields.Location = new System.Drawing.Point(172, 160);
            this.gridFields.MainView = this.gvFields;
            this.gridFields.Margin = new System.Windows.Forms.Padding(0);
            this.gridFields.Name = "gridFields";
            this.gridFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRadioGroup1,
            this.repositoryItemSpinEdit1,
            this.repositoryItemComboBox1});
            this.gridFields.Size = new System.Drawing.Size(363, 121);
            this.gridFields.TabIndex = 6;
            this.gridFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFields});
            // 
            // gvFields
            // 
            this.gvFields.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn4});
            this.gvFields.GridControl = this.gridFields;
            this.gvFields.IndicatorWidth = 28;
            this.gvFields.Name = "gvFields";
            this.gvFields.OptionsSelection.MultiSelect = true;
            this.gvFields.OptionsView.ColumnAutoWidth = false;
            this.gvFields.OptionsView.ShowGroupPanel = false;
            this.gvFields.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvFields_MouseDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "字段名称";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 86;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "字段别名";
            this.gridColumn2.FieldName = "Alias";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 65;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "类型";
            this.gridColumn6.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn6.FieldName = "FieldType";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 88;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.DropDownRows = 15;
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBox1_SelectedIndexChanged);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "长度";
            this.gridColumn7.ColumnEdit = this.repositoryItemSpinEdit1;
            this.gridColumn7.FieldName = "Length";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 38;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.IsFloatValue = false;
            this.repositoryItemSpinEdit1.Mask.EditMask = "N00";
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            this.repositoryItemSpinEdit1.EditValueChanged += new System.EventHandler(this.repositoryItemSpinEdit1_EditValueChanged);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "可否为空";
            this.gridColumn8.ColumnEdit = this.repositoryItemRadioGroup1;
            this.gridColumn8.FieldName = "Nullable";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 76;
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Columns = 2;
            this.repositoryItemRadioGroup1.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("true", "是"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("false", "否")});
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            this.repositoryItemRadioGroup1.SelectedIndexChanged += new System.EventHandler(this.repositoryItemRadioGroup1_SelectedIndexChanged);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "系统字段名称";
            this.gridColumn5.FieldName = "SystemName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 81;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "系统字段别名";
            this.gridColumn3.FieldName = "SystemAlias";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            this.gridColumn3.Width = 82;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "字段信息";
            this.gridColumn4.FieldName = "FieldInfo";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // btnDeleteStyle
            // 
            this.btnDeleteStyle.Enabled = false;
            this.btnDeleteStyle.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteStyle.Image")));
            this.btnDeleteStyle.Location = new System.Drawing.Point(509, 389);
            this.btnDeleteStyle.Name = "btnDeleteStyle";
            this.btnDeleteStyle.Size = new System.Drawing.Size(26, 22);
            this.btnDeleteStyle.StyleController = this.layoutControl1;
            this.btnDeleteStyle.TabIndex = 11;
            this.btnDeleteStyle.ToolTip = "删除风格";
            this.btnDeleteStyle.Click += new System.EventHandler(this.btnDeleteStyle_Click);
            // 
            // btnEditStyle
            // 
            this.btnEditStyle.Enabled = false;
            this.btnEditStyle.Image = ((System.Drawing.Image)(resources.GetObject("btnEditStyle.Image")));
            this.btnEditStyle.Location = new System.Drawing.Point(509, 363);
            this.btnEditStyle.Name = "btnEditStyle";
            this.btnEditStyle.Size = new System.Drawing.Size(26, 22);
            this.btnEditStyle.StyleController = this.layoutControl1;
            this.btnEditStyle.TabIndex = 10;
            this.btnEditStyle.ToolTip = "编辑风格";
            this.btnEditStyle.Click += new System.EventHandler(this.btnEditStyle_Click);
            // 
            // btnCreateStyle
            // 
            this.btnCreateStyle.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateStyle.Image")));
            this.btnCreateStyle.Location = new System.Drawing.Point(509, 337);
            this.btnCreateStyle.Name = "btnCreateStyle";
            this.btnCreateStyle.Size = new System.Drawing.Size(26, 22);
            this.btnCreateStyle.StyleController = this.layoutControl1;
            this.btnCreateStyle.TabIndex = 9;
            this.btnCreateStyle.ToolTip = "创建设施风格";
            this.btnCreateStyle.Click += new System.EventHandler(this.btnCreateStyle_Click);
            // 
            // gridStyles
            // 
            this.gridStyles.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridStyles.Location = new System.Drawing.Point(172, 337);
            this.gridStyles.MainView = this.layoutView1;
            this.gridStyles.Name = "gridStyles";
            this.gridStyles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemTextEdit1});
            this.gridStyles.Size = new System.Drawing.Size(333, 84);
            this.gridStyles.TabIndex = 8;
            this.gridStyles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.FieldValue.Options.UseTextOptions = true;
            this.layoutView1.Appearance.FieldValue.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutView1.Appearance.FieldValue.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutView1.Appearance.FocusedCardCaption.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.layoutView1.Appearance.FocusedCardCaption.Options.UseForeColor = true;
            this.layoutView1.Appearance.SelectedCardCaption.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.layoutView1.Appearance.SelectedCardCaption.Options.UseForeColor = true;
            this.layoutView1.Appearance.SelectionFrame.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.layoutView1.Appearance.SelectionFrame.Options.UseForeColor = true;
            this.layoutView1.CardCaptionFormat = "{2}";
            this.layoutView1.CardHorzInterval = 6;
            this.layoutView1.CardMinSize = new System.Drawing.Size(93, 98);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.colName,
            this.colStyleType,
            this.colThumbnail,
            this.colStyleInfo});
            this.layoutView1.GridControl = this.gridStyles;
            this.layoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn1_1,
            this.layoutViewField_colComment,
            this.layoutViewField_colName});
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.AllowExpandCollapse = false;
            this.layoutView1.OptionsBehavior.AllowPanCards = false;
            this.layoutView1.OptionsBehavior.Editable = false;
            this.layoutView1.OptionsBehavior.ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.layoutView1.OptionsCustomization.AllowFilter = false;
            this.layoutView1.OptionsCustomization.AllowSort = false;
            this.layoutView1.OptionsHeaderPanel.EnableCarouselModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableColumnModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableMultiColumnModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableMultiRowModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnablePanButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableRowModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableSingleModeButton = false;
            this.layoutView1.OptionsItemText.TextToControlDistance = 0;
            this.layoutView1.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
            this.layoutView1.OptionsView.CardsAlignment = DevExpress.XtraGrid.Views.Layout.CardsAlignment.Near;
            this.layoutView1.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutView1.OptionsView.ShowCardExpandButton = false;
            this.layoutView1.OptionsView.ShowFieldHints = false;
            this.layoutView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.layoutView1.OptionsView.ShowHeaderPanel = false;
            this.layoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            this.layoutView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.layoutView1_MouseUp);
            // 
            // colName
            // 
            this.colName.Caption = "风格名称";
            this.colName.ColumnEdit = this.repositoryItemTextEdit1;
            this.colName.FieldName = "Name";
            this.colName.LayoutViewField = this.layoutViewField_colName;
            this.colName.Name = "colName";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // layoutViewField_colName
            // 
            this.layoutViewField_colName.EditorPreferredWidth = 91;
            this.layoutViewField_colName.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colName.Name = "layoutViewField_colName";
            this.layoutViewField_colName.Size = new System.Drawing.Size(102, 89);
            this.layoutViewField_colName.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colName.TextToControlDistance = 0;
            this.layoutViewField_colName.TextVisible = false;
            // 
            // colStyleType
            // 
            this.colStyleType.Caption = "风格类型";
            this.colStyleType.FieldName = "StyleType";
            this.colStyleType.LayoutViewField = this.layoutViewField_colComment;
            this.colStyleType.Name = "colStyleType";
            // 
            // layoutViewField_colComment
            // 
            this.layoutViewField_colComment.EditorPreferredWidth = 10;
            this.layoutViewField_colComment.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colComment.Name = "layoutViewField_colComment";
            this.layoutViewField_colComment.Size = new System.Drawing.Size(102, 89);
            this.layoutViewField_colComment.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_colComment.TextToControlDistance = 0;
            // 
            // colThumbnail
            // 
            this.colThumbnail.Caption = "缩略图";
            this.colThumbnail.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colThumbnail.FieldName = "Thumbnail";
            this.colThumbnail.LayoutViewField = this.layoutViewField_colPicture;
            this.colThumbnail.Name = "colThumbnail";
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // layoutViewField_colPicture
            // 
            this.layoutViewField_colPicture.EditorPreferredWidth = 87;
            this.layoutViewField_colPicture.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colPicture.Name = "layoutViewField_colPicture";
            this.layoutViewField_colPicture.Size = new System.Drawing.Size(91, 76);
            this.layoutViewField_colPicture.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colPicture.TextToControlDistance = 0;
            this.layoutViewField_colPicture.TextVisible = false;
            // 
            // colStyleInfo
            // 
            this.colStyleInfo.Caption = "风格信息";
            this.colStyleInfo.FieldName = "StyleInfo";
            this.colStyleInfo.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.colStyleInfo.Name = "colStyleInfo";
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(102, 89);
            this.layoutViewField_layoutViewColumn1_1.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_layoutViewColumn1_1.TextToControlDistance = 0;
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colPicture});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 0;
            this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // teFacilityClassName
            // 
            this.teFacilityClassName.Location = new System.Drawing.Point(247, 30);
            this.teFacilityClassName.Name = "teFacilityClassName";
            this.teFacilityClassName.Size = new System.Drawing.Size(288, 22);
            this.teFacilityClassName.StyleController = this.layoutControl1;
            this.teFacilityClassName.TabIndex = 1;
            // 
            // cmbFacilityClass3DStyle
            // 
            this.cmbFacilityClass3DStyle.Location = new System.Drawing.Point(444, 82);
            this.cmbFacilityClass3DStyle.Name = "cmbFacilityClass3DStyle";
            this.cmbFacilityClass3DStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFacilityClass3DStyle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFacilityClass3DStyle.Size = new System.Drawing.Size(91, 22);
            this.cmbFacilityClass3DStyle.StyleController = this.layoutControl1;
            this.cmbFacilityClass3DStyle.TabIndex = 4;
            // 
            // cmbFacilityClassTopo
            // 
            this.cmbFacilityClassTopo.Location = new System.Drawing.Point(247, 56);
            this.cmbFacilityClassTopo.Name = "cmbFacilityClassTopo";
            this.cmbFacilityClassTopo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFacilityClassTopo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFacilityClassTopo.Size = new System.Drawing.Size(288, 22);
            this.cmbFacilityClassTopo.StyleController = this.layoutControl1;
            this.cmbFacilityClassTopo.TabIndex = 2;
            // 
            // cmbFacilityClassType
            // 
            this.cmbFacilityClassType.Location = new System.Drawing.Point(247, 134);
            this.cmbFacilityClassType.Name = "cmbFacilityClassType";
            this.cmbFacilityClassType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFacilityClassType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFacilityClassType.Size = new System.Drawing.Size(104, 22);
            this.cmbFacilityClassType.StyleController = this.layoutControl1;
            this.cmbFacilityClassType.TabIndex = 5;
            this.cmbFacilityClassType.SelectedIndexChanged += new System.EventHandler(this.cmbFacilityClassType_SelectedIndexChanged);
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
            this.emptySpaceItem1,
            this.layoutControlItem11,
            this.layoutControlGroup5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(545, 457);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "设施目录树";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(162, 447);
            this.layoutControlGroup2.Text = "设施目录树";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeCatalog;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(156, 421);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "设施类信息";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem17,
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(162, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(373, 104);
            this.layoutControlGroup3.Text = "设施类信息";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teFacilityClassName;
            this.layoutControlItem2.CustomizationFormText = "设施类名称：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(367, 26);
            this.layoutControlItem2.Text = "设施类名称：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbFacilityClass3DStyle;
            this.layoutControlItem5.CustomizationFormText = "三维化方式：";
            this.layoutControlItem5.Location = new System.Drawing.Point(197, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(170, 26);
            this.layoutControlItem5.Text = "三维化方式：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.cmbFacilityClassUnderOrOver;
            this.layoutControlItem17.CustomizationFormText = "地下/地下：";
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(197, 26);
            this.layoutControlItem17.Text = "地下/地下：";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbFacilityClassTopo;
            this.layoutControlItem4.CustomizationFormText = "拓扑层规则：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(367, 26);
            this.layoutControlItem4.Text = "所属拓扑层：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 14);
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "设施风格";
            this.layoutControlGroup4.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup4.ExpandButtonVisible = true;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.emptySpaceItem2});
            this.layoutControlGroup4.Location = new System.Drawing.Point(162, 307);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(373, 114);
            this.layoutControlGroup4.Text = "设施风格";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridStyles;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(337, 88);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnCreateStyle;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(337, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnEditStyle;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(337, 26);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnDeleteStyle;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(337, 52);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(337, 78);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(30, 10);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(162, 421);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(196, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnSave;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(358, 421);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(177, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "字段信息";
            this.layoutControlGroup5.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup5.ExpandButtonVisible = true;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem12,
            this.layoutControlItem7,
            this.layoutControlItem13});
            this.layoutControlGroup5.Location = new System.Drawing.Point(162, 104);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(373, 203);
            this.layoutControlGroup5.Text = "设施字段信息";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbFacilityClassType;
            this.layoutControlItem3.CustomizationFormText = "设施类类型：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem3.Text = "设施类类型：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btnLaunchDFDataConfig;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 151);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(367, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gridFields;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(367, 125);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btnImportShp;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(183, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(184, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // UCCatalogLib
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCCatalogLib";
            this.Size = new System.Drawing.Size(545, 457);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbFacilityClassUnderOrOver.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeCatalog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStyles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFacilityClassName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFacilityClass3DStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFacilityClassTopo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFacilityClassType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            this.ResumeLayout(false);

        }

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private TextEdit teFacilityClassName;
        private ComboBoxEdit cmbFacilityClass3DStyle;
        private ComboBoxEdit cmbFacilityClassTopo;
        private ComboBoxEdit cmbFacilityClassType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private SimpleButton btnDeleteStyle;
        private SimpleButton btnEditStyle;
        private SimpleButton btnCreateStyle;
        private DevExpress.XtraGrid.GridControl gridFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFields;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.GridControl gridStyles;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colStyleType;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colThumbnail;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colStyleInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraTreeList.TreeList treeCatalog;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tl_Name;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;

        private ContextMenuStrip _menuRoot;
        private ContextMenuStrip _menuFacClassGroup;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tl_Object;
        private ContextMenuStrip _menuFacClass;
        private List<TopoClass> _allTCs;
        private Dictionary<string, string> _dictTS;
        private Dictionary<string, string> _dictLT;
        private DataTable _dtFacStyles;
        private DataTable _dtFacFields;
        private SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private bool _bCreateGroup;
        private SimpleButton btnLaunchDFDataConfig;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colName;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colComment;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colPicture;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private IContainer components;
        private ComboBoxEdit cmbFacilityClassUnderOrOver;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private SimpleButton btnImportShp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private bool _bCreateFacClass;

        public UCCatalogLib()
        {
            InitializeComponent();
            this._menuRoot = new System.Windows.Forms.ContextMenuStrip();
            this._menuFacClassGroup = new System.Windows.Forms.ContextMenuStrip();
            this._menuFacClass = new System.Windows.Forms.ContextMenuStrip();
            ToolStripItem toolStripItem = this._menuRoot.Items.Add("创建设施类组");
            toolStripItem.Click += new System.EventHandler(this.itemCreateGroup_Click);
            this._menuRoot.Items.Add(new ToolStripSeparator());
            toolStripItem = this._menuRoot.Items.Add("创建设施类");
            toolStripItem.Click += new System.EventHandler(this.itemCreateFacClass_Click);

            toolStripItem = this._menuFacClassGroup.Items.Add("创建设施类组");
            toolStripItem.Click += new System.EventHandler(this.itemCreateGroup_Click);
            toolStripItem = this._menuFacClassGroup.Items.Add("删除设施类组");
            toolStripItem.Click += new System.EventHandler(this.itemDeleteGroup_Click);
            this._menuFacClassGroup.Items.Add(new ToolStripSeparator());
            toolStripItem = this._menuFacClassGroup.Items.Add("创建设施类");
            toolStripItem.Click += new System.EventHandler(this.itemCreateFacClass_Click);

            toolStripItem = this._menuFacClass.Items.Add("删除设施类");
            toolStripItem.Click += new System.EventHandler(this.itemDeleteFacClass_Click);
            toolStripItem = this._menuFacClass.Items.Add("同步到管线库");
            toolStripItem.Click += new System.EventHandler(this.itemSyncToPipeLib_Click);
            // 设施类风格
            this._dtFacStyles = new System.Data.DataTable("FacStyles");
            this._dtFacStyles.Columns.Add("Name", typeof(string));
            this._dtFacStyles.Columns.Add("StyleType", typeof(string));
            this._dtFacStyles.Columns.Add("Thumbnail", typeof(object));
            this._dtFacStyles.Columns.Add("StyleInfo", typeof(object));
            this.gridStyles.DataSource = this._dtFacStyles;
            // 设施类字段
            this._dtFacFields = new System.Data.DataTable("FacFields");
            this._dtFacFields.Columns.Add("Name", typeof(string));
            this._dtFacFields.Columns.Add("Alias", typeof(string));
            this._dtFacFields.Columns.Add("SystemName", typeof(string));
            this._dtFacFields.Columns.Add("SystemAlias", typeof(string));
            this._dtFacFields.Columns.Add("FieldType", typeof(string));
            this._dtFacFields.Columns.Add("Length", typeof(string));
            this._dtFacFields.Columns.Add("Nullable", typeof(string));
            this._dtFacFields.Columns.Add("FieldInfo", typeof(object));
            this.gridFields.DataSource = this._dtFacFields;
            // 字段类型
            this.repositoryItemComboBox1.Items.Add(gviFieldType.gviFieldString);
            this.repositoryItemComboBox1.Items.Add(gviFieldType.gviFieldDouble);
            this.repositoryItemComboBox1.Items.Add(gviFieldType.gviFieldInt32);
            this.repositoryItemComboBox1.Items.Add(gviFieldType.gviFieldDate);
            this.repositoryItemComboBox1.Items.Add(gviFieldType.gviFieldFloat);
            this.repositoryItemComboBox1.Items.Add(gviFieldType.gviFieldInt16);
            this.repositoryItemComboBox1.Items.Add(gviFieldType.gviFieldInt64);
        }
        private IDataSource _ds;
        private IDataSource _dsPipe;

        public void Init()
        {
            try
            {
                this._ds = DF3DPipeCreateApp.App.TemplateLib;
                if (this._ds == null) { this.Enabled = false; return; }
                this._dsPipe = DF3DPipeCreateApp.App.PipeLib;
                if (this._dsPipe == null) { this.Enabled = false; return; }
                WaitForm.Start("正在加载数据...", "请稍后");
                //加载所有的设施类
                this.cmbFacilityClassType.Properties.Items.Clear();
                List<FacilityClass> allFCs = FacilityClassManager.Instance.GetAllFacilityClass();
                if (allFCs != null)
                {
                    foreach (FacilityClass fc in allFCs)
                    {
                        this.cmbFacilityClassType.Properties.Items.Add(fc);
                    }
                }

                //加载所有的三维化方式
                this.cmbFacilityClass3DStyle.Properties.Items.Clear();
                _dictTS = GetEnumItemDesc(typeof(TurnerStyle));
                foreach (KeyValuePair<string, string> kv in _dictTS)
                {
                    this.cmbFacilityClass3DStyle.Properties.Items.Add(kv.Value);
                }
                //加载地上/地下 类型
                this.cmbFacilityClassUnderOrOver.Properties.Items.Clear();
                _dictLT = GetEnumItemDesc(typeof(LocationType));
                foreach (KeyValuePair<string, string> kv in _dictLT)
                {
                    this.cmbFacilityClassUnderOrOver.Properties.Items.Add(kv.Value);
                }

                //加载所有的拓扑层
                this.cmbFacilityClassTopo.Properties.Items.Clear();
                _allTCs = GetAllTopoLayers();
                if (_allTCs != null)
                {
                    foreach (TopoClass tc in _allTCs)
                    {
                        this.cmbFacilityClassTopo.Properties.Items.Add(tc);
                    }
                }
                // 构建设施目录树
                this.treeCatalog.Nodes.Clear();
                List<FacClass> list = GetNodesByPCode("-1", true);
                BuildTree(list, null);
                List<FacClass> list1 = GetNodesByPCode("-1", false);
                foreach (FacClass fc in list1)
                {
                    TreeListNode node = this.treeCatalog.AppendNode(new object[] { fc.Name, fc }, null);
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

        public static string GetEnumDesc(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }

        public static Dictionary<string, string> GetEnumItemDesc(Type enumType)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            System.Reflection.FieldInfo[] fieldinfos = enumType.GetFields();
            foreach (System.Reflection.FieldInfo field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    dic.Add(field.Name, ((DescriptionAttribute)objs[0]).Description);
                }
            }
            return dic;
        }

        private TopoClass GetTopoClassByObjectId(string objectId)
        {
            if (_allTCs == null) return null;
            foreach (TopoClass tc in _allTCs)
            {
                if (tc.ObjectId == objectId)
                {
                    return tc;
                }
            }
            return null;
        }

        private List<TopoClass> GetAllTopoLayers()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TopoManage");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "1=1"
                };
                cursor = oc.Search(filter, true);
                List<TopoClass> list = new List<TopoClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    TopoClass tc = new TopoClass();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) tc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ObjectId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ObjectId"));
                        if (obj != null) tc.ObjectId = obj.ToString();
                    }
                    if (row.FieldIndex("TopoLayerName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoLayerName"));
                        if (obj != null) tc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("Tolerance") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Tolerance"));
                        if (obj != null) tc.Tolerance = double.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ToleranceZ") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ToleranceZ"));
                        if (obj != null) tc.ToleranceZ = double.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("IgnoreZ") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("IgnoreZ"));
                        if (obj != null) tc.IgnoreZ = obj.ToString() == "1" ? true : false;
                    }
                    if (row.FieldIndex("TopoTableName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoTableName"));
                        if (obj != null) tc.TopoTable = obj.ToString();
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) tc.Comment = obj.ToString();
                    }
                    list.Add(tc);
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

        private List<FacClass> GetNodesByPCode(string pcode, bool bGroup)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilter();
                if (bGroup) filter.WhereClause = "PCode = '" + pcode + "' and FacilityType = 'UnKnown'";
                else filter.WhereClause = "PCode = '" + pcode + "' and FacilityType <> 'UnKnown'";
                filter.PostfixClause = "ORDER BY OrderBy asc";

                cursor = oc.Search(filter, false);
                List<FacClass> list = new List<FacClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    FacClass fc = new FacClass();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("Code") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Code"));
                        if (obj != null) fc.Code = obj.ToString();
                    }
                    if (row.FieldIndex("PCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("PCode"));
                        if (obj != null) fc.PCode = obj.ToString();
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("TopoLayerId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoLayerId"));
                        if (obj != null) fc.TopoLayerId = obj.ToString();
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
                    fc.IsGroup = bGroup;
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

        private void BuildTree(List<FacClass> list, TreeListNode pnode)
        {
            if (list == null || list.Count == 0) return;
            foreach (FacClass fc in list)
            {
                TreeListNode node = this.treeCatalog.AppendNode(new object[] { fc.Name, fc }, pnode);
                List<FacClass> list1 = GetNodesByPCode(fc.Code, true);
                BuildTree(list1, node);
                List<FacClass> list2 = GetNodesByPCode(fc.Code, false);
                BuildTree(list2, node);
            }
            this.treeCatalog.ExpandAll();
        }

        private List<FacStyleClass> GetFacStyleByFacClassCode(string fcCode)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
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

        private void LoadStyleData()
        {
            this._dtFacStyles.Clear();
            if (this.treeCatalog.FocusedNode == null) return;

            object obj = this.treeCatalog.FocusedNode.GetValue("Object");
            if (obj is FacClass)
            {
                FacClass fc = obj as FacClass;
                List<FacStyleClass> list = GetFacStyleByFacClassCode(fc.Code);

                foreach (FacStyleClass fsc in list)
                {
                    DataRow dr = this._dtFacStyles.NewRow();
                    dr["Name"] = fsc.Name;
                    dr["StyleType"] = fsc.Type;
                    dr["Thumbnail"] = fsc.Thumbnail;
                    dr["StyleInfo"] = fsc;
                    this._dtFacStyles.Rows.Add(dr);
                }
            }
        }

        private CMFieldConfig GetFieldInfoEx(string code, DFDataConfig.Class.FieldInfo fi)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FieldConfig");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "FacClassCode='" + code + "' and Name = '" + fi.Name + "'"
                };
                cursor = oc.Search(filter, true);
                if ((row = cursor.NextRow()) != null)
                {
                    CMFieldConfig fc = new CMFieldConfig(fi.Name, fi.Alias, fi.SystemName, fi.SystemAlias);
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fc.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("FieldType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FieldType"));
                        if (obj != null)
                        {
                            gviFieldType ts = 0;
                            if (Enum.TryParse<gviFieldType>(obj.ToString(), out ts))
                                fc.FieldType = ts;
                            else fc.FieldType = gviFieldType.gviFieldUnknown;
                        }
                    }
                    if (row.FieldIndex("Length") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Length"));
                        if (obj != null)
                        {
                            int temp;
                            if (int.TryParse(obj.ToString(), out temp))
                                fc.Length = temp;
                            else fc.Length = 0;
                        }
                    }
                    if (row.FieldIndex("Nullable") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Nullable"));
                        if (obj != null)
                        {
                            int temp;
                            if (int.TryParse(obj.ToString(), out temp))
                                fc.Nullable = temp > 0 ? true : false;
                            else fc.Nullable = true;
                        }
                    }

                    return fc;
                }
                else
                {
                    CMFieldConfig fc = new CMFieldConfig(fi.Name, fi.Alias, fi.SystemName, fi.SystemAlias);
                    fc.Nullable = true;
                    fc.Length = 254;
                    fc.FieldType = gviFieldType.gviFieldString;
                    return fc;
                }
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

        private void LoadFieldData()
        {
            this._dtFacFields.Clear();
            object obj = this.cmbFacilityClassType.EditValue;
            if (obj != null && obj is FacilityClass)
            {
                FacilityClass facC = obj as FacilityClass;
                foreach (DFDataConfig.Class.FieldInfo fi in facC.FieldInfoCollection)
                {
                    DataRow dr = this._dtFacFields.NewRow();
                    dr["Name"] = fi.Name;
                    dr["Alias"] = fi.Alias;
                    dr["SystemName"] = fi.SystemName;
                    dr["SystemAlias"] = fi.SystemAlias;
                    CMFieldConfig cmfc = null;
                    if (this.treeCatalog.FocusedNode != null)
                    {
                        object temp = this.treeCatalog.FocusedNode.GetValue("Object");
                        if (temp is FacClass)
                        {
                            FacClass fc = temp as FacClass;
                            cmfc = GetFieldInfoEx(fc.Code, fi);
                        }
                    }
                    if (cmfc == null)
                    {
                        dr["FieldType"] = gviFieldType.gviFieldString;
                        dr["Length"] = 254;
                        dr["Nullable"] = "true";
                        dr["FieldInfo"] = fi;
                    }
                    else
                    {
                        dr["FieldType"] = cmfc.FieldType;
                        dr["Length"] = cmfc.Length;
                        dr["Nullable"] = cmfc.Nullable ? "true" : "false";
                        dr["FieldInfo"] = cmfc;
                    }
                    this._dtFacFields.Rows.Add(dr);
                }
            }
        }

        private void treeCatalog_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                TreeList control = sender as TreeList;
                TreeListHitInfo info = control.CalcHitInfo(e.Location);
                if (info != null)
                {
                    TreeListNode node = info.Node;
                    this.treeCatalog.SetFocusedNode(node);
                    if ((control != null) && (((e.Button == MouseButtons.Right) && (Control.ModifierKeys == Keys.None)) && (control.State == TreeListState.Regular)))
                    {
                        if (node == null)
                        {
                            this._menuRoot.Show(control, new Point(e.X, e.Y));
                        }
                        else
                        {
                            object obj = node.GetValue("Object");
                            if (obj != null && obj is FacClass)
                            {
                                FacClass fc = obj as FacClass;
                                if (fc.IsGroup) this._menuFacClassGroup.Show(control, new Point(e.X, e.Y));
                                else this._menuFacClass.Show(control, new Point(e.X, e.Y));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void treeCatalog_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            LoadStyleData();
            LoadFieldData();
            if (this.treeCatalog.FocusedNode == null)
            {
                this.layoutControlGroup3.Enabled = false;
                this.layoutControlGroup4.Enabled = false;
                this.layoutControlGroup5.Enabled = false;
                this.teFacilityClassName.Text = "";
                this.cmbFacilityClass3DStyle.Text = "";
                this.cmbFacilityClassUnderOrOver.Text = "";
                this.cmbFacilityClassTopo.Text = "";
                this.cmbFacilityClassType.Text = "";
                return;
            }

            object obj = this.treeCatalog.FocusedNode.GetValue("Object");
            if (obj == null || !(obj is FacClass)) return;

            this.btnSave.Text = "保存";

            FacClass fc = obj as FacClass;
            if (fc.IsGroup)
            {
                this.cmbFacilityClass3DStyle.Enabled = false;
                this.cmbFacilityClassUnderOrOver.Enabled = false;
                this.cmbFacilityClassTopo.Enabled = false;
                this.cmbFacilityClassType.Enabled = false;
                this.layoutControlGroup3.Enabled = true;
                this.layoutControlGroup4.Enabled = false;
                this.layoutControlGroup5.Enabled = false;
            }
            else
            {
                this.cmbFacilityClass3DStyle.Enabled = true;
                this.cmbFacilityClassUnderOrOver.Enabled = true;
                this.cmbFacilityClassTopo.Enabled = true;
                this.cmbFacilityClassType.Enabled = false;
                this.layoutControlGroup3.Enabled = true;
                this.layoutControlGroup4.Enabled = true;
                this.layoutControlGroup5.Enabled = true;
            }

            this.teFacilityClassName.Text = fc.Name;
            this.cmbFacilityClassType.SelectedItem = fc.FacilityType;
            if (this._dictTS.ContainsKey(fc.TurnerStyle.ToString()))
                this.cmbFacilityClass3DStyle.SelectedItem = _dictTS[fc.TurnerStyle.ToString()];
            else this.cmbFacilityClass3DStyle.SelectedItem = TurnerStyle.Unknown.ToString();
            if (this._dictLT.ContainsKey(fc.LocationType.ToString()))
                this.cmbFacilityClassUnderOrOver.SelectedItem = _dictLT[fc.LocationType.ToString()];
            else this.cmbFacilityClassUnderOrOver.SelectedItem = LocationType.UnKnown.ToString();
            this.cmbFacilityClassTopo.SelectedItem = GetTopoClassByObjectId(fc.TopoLayerId);
        }

        private void itemCreateGroup_Click(object sender, EventArgs e)
        {
            this._dtFacStyles.Clear();
            this._dtFacFields.Clear();

            this.teFacilityClassName.Text = "";
            this.teFacilityClassName.Focus();
            this.cmbFacilityClassType.SelectedItem = FacilityClassManager.Instance.GetFacilityClassByName("UnKnown");
            if (this._dictTS.ContainsKey(TurnerStyle.Unknown.ToString()))
                this.cmbFacilityClass3DStyle.SelectedItem = this._dictTS[TurnerStyle.Unknown.ToString()];
            if (this._dictLT.ContainsKey(LocationType.UnKnown.ToString()))
                this.cmbFacilityClassUnderOrOver.SelectedItem = this._dictLT[LocationType.UnKnown.ToString()];
            this.cmbFacilityClassTopo.Text = "";

            this.cmbFacilityClass3DStyle.Enabled = false;
            this.cmbFacilityClassUnderOrOver.Enabled = false;
            this.cmbFacilityClassTopo.Enabled = false;
            this.cmbFacilityClassType.Enabled = false;
            this.layoutControlGroup3.Enabled = true;
            this.layoutControlGroup4.Enabled = false;
            this.layoutControlGroup5.Enabled = false;

            this._bCreateGroup = true;
            this._bCreateFacClass = false;
            this.btnSave.Text = "创建";
        }

        private bool RecursiveDeleteNodes(TreeListNodes list)
        {
            if (list == null) return true;
            foreach (TreeListNode node in list)
            {
                object obj = node.GetValue("Object");
                if (obj == null || !(obj is FacClass)) return false;
                FacClass fc = obj as FacClass;
                if (fc.IsGroup)
                {
                    if (RecursiveDeleteNodes(node.Nodes))
                    {
                        if (DeleteFacClass(fc))
                        {
                        }
                        else return false;
                    }
                    else return false;
                }
                else
                {
                    FacClassReg reg = GetFacClassRegByFacClassCode(fc.Code);
                    if (reg != null)
                    {
                        DeleteFeatureClass(reg.DataSetName, reg.FcName);
                        DeleteFacClassReg(reg);
                    }
                    if (DeleteFacClass(fc))
                    {
                        if (DeleteFacStyleClassByFacClassCode(fc.Code) && DeleteFieldConfig(fc.Code))
                        {

                        }
                        else return false;
                    }
                    else return false;
                }
            }
            return true;
        }

        private bool DeleteFacStyleClassByFacClassCode(string code)
        {
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityStyle");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("FacClassCode = '{0}'", code);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void itemDeleteGroup_Click(object sender, EventArgs e)
        {
            if (this.treeCatalog.FocusedNode == null) return;
            object obj = this.treeCatalog.FocusedNode.GetValue("Object");
            if (obj == null || !(obj is FacClass)) return;
            FacClass fc = obj as FacClass;
            if (RecursiveDeleteNodes(this.treeCatalog.FocusedNode.Nodes))
            {
                if (DeleteFacClass(fc))
                {
                    this.treeCatalog.DeleteNode(this.treeCatalog.FocusedNode);
                    this.treeCatalog.SetFocusedNode(null);
                }
                else XtraMessageBox.Show("删除设施类组失败！", "提示");
            }
            else XtraMessageBox.Show("删除设施类组失败！", "提示");
        }

        private void itemCreateFacClass_Click(object sender, EventArgs e)
        {
            this._dtFacStyles.Clear();
            this._dtFacFields.Clear();

            this.teFacilityClassName.Text = "";
            this.teFacilityClassName.Focus();
            this.cmbFacilityClassType.Text = "";
            this.cmbFacilityClass3DStyle.Text = "";
            this.cmbFacilityClassUnderOrOver.Text = "";
            this.cmbFacilityClassTopo.Text = "";

            this.layoutControlGroup3.Enabled = true;
            this.layoutControlGroup4.Enabled = false;
            this.layoutControlGroup5.Enabled = true;
            this.cmbFacilityClass3DStyle.Enabled = true;
            this.cmbFacilityClassUnderOrOver.Enabled = true;
            this.cmbFacilityClassTopo.Enabled = true;
            this.cmbFacilityClassType.Enabled = true;

            this._bCreateGroup = false;
            this._bCreateFacClass = true;
            this.btnSave.Text = "创建";
        }


        private List<CMFieldConfig> GetFieldsConfig(string code)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FieldConfig");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = string.Format("FacClassCode = '{0}'", code),
                    PostfixClause = "order by OrderBy asc"
                };
                cursor = oc.Search(filter, true);
                List<CMFieldConfig> list = new List<CMFieldConfig>();
                while ((row = cursor.NextRow()) != null)
                {
                    CMFieldConfig fc = new CMFieldConfig();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fc.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("Alias") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Alias"));
                        if (obj != null) fc.Alias = obj.ToString();
                    }
                    if (row.FieldIndex("FieldType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FieldType"));
                        if (obj != null)
                        {
                            gviFieldType ts = 0;
                            if (Enum.TryParse<gviFieldType>(obj.ToString(), out ts))
                                fc.FieldType = ts;
                            else fc.FieldType = gviFieldType.gviFieldUnknown;
                        }
                    }
                    if (row.FieldIndex("Length") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Length"));
                        if (obj != null) fc.Length = Convert.ToInt32(obj.ToString());
                    }
                    if (row.FieldIndex("Nullable") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Nullable"));
                        if (obj != null) fc.Nullable = obj.ToString() == "0" ? false : true;
                    }
                    list.Add(fc);
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
        private IFieldInfoCollection CreateFieldInfoCollection(List<CMFieldConfig> list, string facType)
        {
            if (list == null) return null;
            IFieldInfoCollection infos = new FieldInfoCollection();

            IFieldInfo newVal = null;
            foreach (CMFieldConfig cmfc in list)
            {
                newVal = new FieldInfoClass();
                newVal.Name = cmfc.Name;
                newVal.Alias = cmfc.Alias;
                newVal.FieldType = cmfc.FieldType;
                newVal.Nullable = cmfc.Nullable;
                if (newVal.FieldType == gviFieldType.gviFieldString)
                {
                    newVal.Length = cmfc.Length;
                }
                if (infos.IndexOf(newVal.Name) == -1)
                {
                    infos.Add(newVal);
                }
                if (cmfc.SystemName == "Diameter")
                {
                    newVal = new FieldInfoClass
                    {
                        Name = cmfc.Name + "1",
                        Alias = "横截面宽",
                        FieldType = gviFieldType.gviFieldDouble
                    };
                    if (infos.IndexOf(newVal.Name) == -1)
                    {
                        infos.Add(newVal);
                    }
                    newVal = new FieldInfoClass
                    {
                        Name = cmfc.Name + "2",
                        Alias = "横截面高",
                        FieldType = gviFieldType.gviFieldDouble
                    };
                    if (infos.IndexOf(newVal.Name) == -1)
                    {
                        infos.Add(newVal);
                    }
                }

            }
            newVal = new FieldInfoClass
            {
                Name = "FacilityId",
                Alias = "设施编号",
                FieldType = gviFieldType.gviFieldString,
                Length = 50
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "GroupId",
                Alias = "逻辑组ID",
                FieldType = gviFieldType.gviFieldInt32,
                RegisteredRenderIndex = true
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "StyleId",
                Alias = "风格编号",
                FieldType = gviFieldType.gviFieldString,
                Length = 50
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "ModelName",
                Alias = "模型名称",
                FieldType = gviFieldType.gviFieldString,
                Length = 80
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Metadata",
                Alias = "Metadata",
                FieldType = gviFieldType.gviFieldBlob
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Geometry",
                Alias = "三维空间列",
                RegisteredRenderIndex = true,
                FieldType = gviFieldType.gviFieldGeometry
            };
            IGeometryDef def = new GeometryDefClass
            {
                GeometryColumnType = gviGeometryColumnType.gviGeometryColumnModelPoint,
                HasZ = true
            };
            newVal.GeometryDef = def;
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Shape",
                Alias = "二维空间列",
                RegisteredRenderIndex = true,
                FieldType = gviFieldType.gviFieldGeometry
            };
            gviGeometryColumnType gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnUnknown;
            switch (facType)
            {
                case "PipeNode":
                    gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnPoint;
                    break;
                case "PipeLine":
                    gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnPolyline;
                    break;
                case "PipeBuild1":
                    gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnPolygon;
                    break;
                case "PipeBuild":
                    gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnPolyline;
                    break;
            }
            IGeometryDef def2 = new GeometryDefClass
            {
                GeometryColumnType = gviGeometryColumnUnknown,
                HasZ = true
            };
            newVal.GeometryDef = def2;
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "FootPrint",
                Alias = "投影二维",
                RegisteredRenderIndex = true,
                FieldType = gviFieldType.gviFieldGeometry
            };
            IGeometryDef def3 = new GeometryDefClass
            {
                GeometryColumnType = gviGeometryColumnUnknown,
                HasZ = false
            };
            newVal.GeometryDef = def3;
            infos.Add(newVal);
            return infos;
        }
        private bool CreateFacClassReg(FacClass fc)
        {
            if (fc == null || fc.FacilityType == null) return false;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IFeatureDataSet fdsActuality = this._dsPipe.OpenFeatureDataset("DataSet_GEO_Actuality");
                if (fdsActuality == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return false;

                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                if (row != null)
                {
                    row.SetValue(row.FieldIndex("FacClassCode"), fc.Code);
                    row.SetValue(row.FieldIndex("Name"), fc.Name);
                    row.SetValue(row.FieldIndex("FacilityType"), fc.FacilityType.Name);
                    row.SetValue(row.FieldIndex("LocationType"), fc.LocationType.ToString());
                    row.SetValue(row.FieldIndex("TurnerStyle"), fc.TurnerStyle.ToString());
                    row.SetValue(row.FieldIndex("Comment"), fc.Comment);
                    string fcName = string.Format("FC_{0}_{1}", (int)DataLifeCyle.Actuality, fc.Code);
                    List<CMFieldConfig> fieldConfig = GetFieldsConfig(fc.Code);
                    IFieldInfoCollection fielInfoCol = CreateFieldInfoCollection(fieldConfig, fc.FacilityType.Name);
                    IFeatureClass featureClass = fdsActuality.CreateFeatureClass(fcName, fielInfoCol);
                    if (featureClass == null) return false;
                    featureClass.AliasName = fc.Name;
                    featureClass.LockType = gviLockType.gviLockExclusiveSchema;
                    IGridIndexInfo indexInfo = new GridIndexInfoClass
                    {
                        L1 = 500.0,
                        L2 = 2000.0,
                        L3 = 10000.0,
                        GeoColumnName = "Geometry"
                    };
                    featureClass.AddSpatialIndex(indexInfo);
                    indexInfo.GeoColumnName = "Shape";
                    featureClass.AddSpatialIndex(indexInfo);
                    indexInfo.GeoColumnName = "FootPrint";
                    featureClass.AddSpatialIndex(indexInfo);
                    IRenderIndexInfo info2 = new RenderIndexInfoClass
                    {
                        L1 = 500.0,
                        GeoColumnName = "Geometry"
                    };
                    featureClass.AddRenderIndex(info2);
                    info2.GeoColumnName = "Shape";
                    featureClass.AddRenderIndex(info2);
                    info2.GeoColumnName = "FootPrint";
                    featureClass.AddRenderIndex(info2);
                    featureClass.LockType = gviLockType.gviLockSharedSchema;
                    row.SetValue(row.FieldIndex("FeatureClassId"), featureClass.Guid.ToString());
                    row.SetValue(row.FieldIndex("DataSetName"), "DataSet_GEO_Actuality");
                    row.SetValue(row.FieldIndex("FcName"), featureClass.Name);
                    row.SetValue(row.FieldIndex("DataType"), DataLifeCyle.Actuality.ToString());
                    cursor.InsertRow(row);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                WaitForm.SetCaption("创建管线库中的设施要素类【" + fc.Name + "】失败！");
                return false;
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
        private bool DeleteFacClassReg(FacClassReg reg)
        {
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("FacClassCode = '{0}'", reg.FacClassCode);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool DeleteFeatureClass(string dataSetName, string fcName)
        {
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset(dataSetName);
                if (fds == null) return false;
                return fds.DeleteByName(fcName);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private FacClassReg GetFacClassRegByFacClassCode(string code)
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
                filter.WhereClause = "FacClassCode='" + code + "'";

                cursor = oc.Search(filter, false);
                if ((row = cursor.NextRow()) != null)
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
                    return fc;
                }
                return null;
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
        private void itemSyncToPipeLib_Click(object sender, EventArgs e)
        {
            if (this.treeCatalog.FocusedNode == null) return;
            object obj = this.treeCatalog.FocusedNode.GetValue("Object");
            if (obj == null || !(obj is FacClass)) return;
            FacClass fc = obj as FacClass;
            FacClassReg reg = GetFacClassRegByFacClassCode(fc.Code);
            WaitForm.Start("正在同步...", " 请稍后");
            if (reg != null)
            {
                DeleteFeatureClass(reg.DataSetName, reg.FcName);
                DeleteFacClassReg(reg);
            }
            if (!CreateFacClassReg(fc))
                XtraMessageBox.Show("同步失败！", "提示");
            WaitForm.Stop();
        }

        private void itemDeleteFacClass_Click(object sender, EventArgs e)
        {
            if (this.treeCatalog.FocusedNode == null) return;
            object obj = this.treeCatalog.FocusedNode.GetValue("Object");
            if (obj == null || !(obj is FacClass)) return;
            FacClass fc = obj as FacClass;
            WaitForm.Start("正在删除...", " 请稍后");
            FacClassReg reg = GetFacClassRegByFacClassCode(fc.Code);
            if (reg != null)
            {
                DeleteFeatureClass(reg.DataSetName, reg.FcName);
                DeleteFacClassReg(reg);
            }
            if (DeleteFacClass(fc))
            {
                if (DeleteFacStyleClassByFacClassCode(fc.Code) && DeleteFieldConfig(fc.Code))
                {
                }
                this.treeCatalog.DeleteNode(this.treeCatalog.FocusedNode);
                this.treeCatalog.SetFocusedNode(null);
            }
            else
            {
                XtraMessageBox.Show("删除设施类失败！", "提示");
            }
            WaitForm.Stop();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.teFacilityClassName.Text.Trim()))
            {
                XtraMessageBox.Show("请输入设施类名称！", "提示");
                this.teFacilityClassName.Focus();
                return;
            }
            if (this._bCreateGroup || this._bCreateFacClass)//创建
            {
                FacClass fc = new FacClass();
                if (this._bCreateGroup)
                {
                    fc.IsGroup = true;
                    fc.Name = this.teFacilityClassName.Text.Trim();
                    fc.Code = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                    fc.LocationType = LocationType.UnKnown;
                    fc.FacilityType = FacilityClassManager.Instance.GetFacilityClassByName("UnKnown");
                    fc.TurnerStyle = TurnerStyle.Unknown;
                    if (this.treeCatalog.FocusedNode == null)
                    {
                        fc.PCode = "-1";
                    }
                    else
                    {
                        object obj = this.treeCatalog.FocusedNode.GetValue("Object");
                        if (obj is FacClass)
                        {
                            FacClass parentfc = obj as FacClass;
                            if (parentfc.IsGroup)
                            {
                                fc.PCode = parentfc.Code;
                            }
                        }
                    }
                }
                if (this._bCreateFacClass)
                {
                    fc.IsGroup = false;
                    fc.Name = this.teFacilityClassName.Text.Trim();
                    fc.Code = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                    fc.FacilityType = this.cmbFacilityClassType.EditValue as FacilityClass;
                    foreach (KeyValuePair<string, string> kv in this._dictLT)
                    {
                        if (kv.Value == this.cmbFacilityClassUnderOrOver.EditValue.ToString())
                        {
                            LocationType ts = 0;
                            if (Enum.TryParse<LocationType>(kv.Key, out ts))
                                fc.LocationType = ts;
                            else fc.LocationType = 0;
                            break;
                        }
                    }
                    foreach (KeyValuePair<string, string> kv in this._dictTS)
                    {
                        if (kv.Value == this.cmbFacilityClass3DStyle.EditValue.ToString())
                        {
                            TurnerStyle ts = 0;
                            if (Enum.TryParse<TurnerStyle>(kv.Key, out ts))
                                fc.TurnerStyle = ts;
                            else fc.TurnerStyle = 0;
                            break;
                        }
                    }
                    if (this.cmbFacilityClassTopo.EditValue is TopoClass)
                    {
                        TopoClass tc = this.cmbFacilityClassTopo.EditValue as TopoClass;
                        fc.TopoLayerId = tc.ObjectId;
                    }
                    if (this.treeCatalog.FocusedNode == null)
                    {
                        fc.PCode = "-1";
                    }
                    else
                    {
                        object obj = this.treeCatalog.FocusedNode.GetValue("Object");
                        if (obj is FacClass)
                        {
                            FacClass parentfc = obj as FacClass;
                            if (parentfc.IsGroup)
                            {
                                fc.PCode = parentfc.Code;
                            }
                        }
                    }
                }
                string strMsg = fc.IsGroup ? "设施类组" : "设施类";
                if (InsertFacClass(fc))
                {
                    if (!fc.IsGroup)
                    {
                        foreach (DataRow dr in this._dtFacFields.Rows)
                        {
                            InsertFieldConfig(dr["FieldInfo"], fc.Code);
                        }
                    }
                    TreeListNode node = this.treeCatalog.AppendNode(new object[] { fc.Name, fc }, this.treeCatalog.FocusedNode);
                    this.treeCatalog.SetFocusedNode(node);
                    this.btnSave.Text = "保存";
                    if (!fc.IsGroup)
                    {
                        this._bCreateFacClass = !this._bCreateFacClass;
                        this.layoutControlGroup4.Enabled = true;
                    }
                    if (fc.IsGroup) this._bCreateGroup = !this._bCreateFacClass;
                }
                else
                {
                    XtraMessageBox.Show("创建" + strMsg + "失败！", "提示");
                    this.btnSave.Text = "创建";
                }
            }
            else//编辑
            {
                if (this.treeCatalog.FocusedNode == null) return;
                object obj = this.treeCatalog.FocusedNode.GetValue("Object");
                if (obj == null || !(obj is FacClass)) return;
                FacClass fc = obj as FacClass;
                fc.Name = this.teFacilityClassName.Text.Trim();
                fc.FacilityType = this.cmbFacilityClassType.EditValue as FacilityClass;
                foreach (KeyValuePair<string, string> kv in this._dictTS)
                {
                    if (kv.Value == this.cmbFacilityClass3DStyle.EditValue.ToString())
                    {
                        TurnerStyle ts = 0;
                        if (Enum.TryParse<TurnerStyle>(kv.Key, out ts))
                            fc.TurnerStyle = ts;
                        else fc.TurnerStyle = 0;
                        break;
                    }
                }
                foreach (KeyValuePair<string, string> kv in this._dictLT)
                {
                    if (kv.Value == this.cmbFacilityClassUnderOrOver.EditValue.ToString())
                    {
                        LocationType ts = 0;
                        if (Enum.TryParse<LocationType>(kv.Key, out ts))
                            fc.LocationType = ts;
                        else fc.LocationType = 0;
                        break;
                    }
                }
                if (this.cmbFacilityClassTopo.EditValue is TopoClass)
                {
                    TopoClass tc = this.cmbFacilityClassTopo.EditValue as TopoClass;
                    fc.TopoLayerId = tc.ObjectId;
                }
                string strMsg = fc.IsGroup ? "设施类组" : "设施类";
                if (UpdateFacClass(fc))
                {
                    if (!fc.IsGroup)
                    {
                        if (DeleteFieldConfig(fc.Code))
                        {
                            foreach (DataRow dr in this._dtFacFields.Rows)
                            {
                                InsertFieldConfig(dr["FieldInfo"], fc.Code);
                            }
                        }
                    }
                    this.treeCatalog.FocusedNode.SetValue("Name", fc.Name);
                    this.treeCatalog.FocusedNode.SetValue("Object", fc);
                    XtraMessageBox.Show("更新" + strMsg + "成功！", "提示");
                }
                else
                {
                    XtraMessageBox.Show("更新" + strMsg + "失败！", "提示");
                }
            }
        }

        private bool InsertFieldConfig(object obj, string code)
        {
            if (obj == null || !(obj is DFDataConfig.Class.FieldInfo)) return false;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FieldConfig");
                if (oc == null) return false;
                DFDataConfig.Class.FieldInfo fi = obj as DFDataConfig.Class.FieldInfo;
                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                row.SetValue(row.FieldIndex("Name"), fi.Name);
                row.SetValue(row.FieldIndex("Alias"), fi.Alias);
                row.SetValue(row.FieldIndex("StdFieldName"), fi.SystemName);
                row.SetValue(row.FieldIndex("FacClassCode"), code);
                if (obj is CMFieldConfig)
                {
                    CMFieldConfig cmfc = obj as CMFieldConfig;
                    row.SetValue(row.FieldIndex("FieldType"), cmfc.FieldType);
                    row.SetValue(row.FieldIndex("Length"), cmfc.Length);
                    row.SetValue(row.FieldIndex("Nullable"), cmfc.Nullable ? 1 : 0);
                }
                cursor.InsertRow(row);
                return true;
            }
            catch (Exception ex)
            {
                return false;
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

        private bool InsertFacClass(FacClass fc)
        {
            if (fc == null) return false;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return false;

                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                row.SetValue(row.FieldIndex("Name"), fc.Name);
                row.SetValue(row.FieldIndex("Code"), fc.Code);
                row.SetValue(row.FieldIndex("PCode"), fc.PCode);
                row.SetValue(row.FieldIndex("FacilityType"), fc.FacilityType.Name);
                row.SetValue(row.FieldIndex("TurnerStyle"), fc.TurnerStyle.ToString());
                row.SetValue(row.FieldIndex("TopoLayerId"), fc.TopoLayerId);
                row.SetValue(row.FieldIndex("LocationType"), fc.LocationType.ToString());
                cursor.InsertRow(row);
                fc.Id = cursor.LastInsertId;
                return true;
            }
            catch (Exception ex)
            {
                return false;
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

        private bool DeleteFieldConfig(string code)
        {
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FieldConfig");
                if (oc == null) return false;
                IQueryFilter filter = new QueryFilterClass()
                {
                    WhereClause = "FacClassCode='" + code + "'"
                };
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool UpdateFieldConfig(object obj, string code)
        {
            if (obj == null || !(obj is CMFieldConfig)) return false;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FieldConfig");
                if (oc == null) return false;
                CMFieldConfig cmfc = obj as CMFieldConfig;
                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "FacClassCode='" + code + "' and Name = '" + cmfc.Name + "'"
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    row.SetValue(row.FieldIndex("Alias"), cmfc.Alias);
                    row.SetValue(row.FieldIndex("StdFieldName"), cmfc.SystemName);
                    row.SetValue(row.FieldIndex("FieldType"), cmfc.FieldType.ToString());
                    row.SetValue(row.FieldIndex("Length"), cmfc.Length);
                    row.SetValue(row.FieldIndex("Nullable"), cmfc.Nullable ? 1 : 0);
                    cursor.UpdateRow(row);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
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

        private bool UpdateFacClass(FacClass fc)
        {
            if (fc == null) return false;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return false;
                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("Code = '{0}'", fc.Code)
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    row.SetValue(row.FieldIndex("Name"), fc.Name);
                    row.SetValue(row.FieldIndex("Code"), fc.Code);
                    row.SetValue(row.FieldIndex("PCode"), fc.PCode);
                    row.SetValue(row.FieldIndex("FacilityType"), fc.FacilityType.Name);
                    row.SetValue(row.FieldIndex("TurnerStyle"), fc.TurnerStyle.ToString());
                    row.SetValue(row.FieldIndex("TopoLayerId"), fc.TopoLayerId);
                    row.SetValue(row.FieldIndex("LocationType"), fc.LocationType.ToString());
                    cursor.UpdateRow(row);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
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
        private bool DeleteFacClass(FacClass fc)
        {
            if (fc == null) return false;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("Code = '{0}'", fc.Code);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void cmbFacilityClassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFieldData();
        }

        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);
        private void btnLaunchDFDataConfig_Click(object sender, EventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "DFDataConfig.exe"))
                WinExec(AppDomain.CurrentDomain.BaseDirectory + "DFDataConfig.exe", 1);
        }

        private void btnCreateStyle_Click(object sender, EventArgs e)
        {
            object obj = this.cmbFacilityClassType.EditValue;
            if (this.treeCatalog.FocusedNode == null) return;
            FacClass facC = this.treeCatalog.FocusedNode.GetValue("Object") as FacClass;
            if (facC == null) return;

            if (obj is FacilityClass)
            {
                FacilityClass fc = obj as FacilityClass;
                StyleType styleType;
                if (!Enum.TryParse<StyleType>(fc.Name + "Style", out styleType)) return;
                bool res = false;
                switch (styleType)
                {
                    case StyleType.PipeLineStyle:
                        FrmPipeLineStyle dlg1 = new FrmPipeLineStyle();
                        dlg1.Style.FacClassCode = facC.Code;
                        if (dlg1.ShowDialog() == DialogResult.OK)
                        {
                            DataRow dr = this._dtFacStyles.NewRow();
                            dr["Name"] = dlg1.Style.Name;
                            dr["StyleType"] = dlg1.Style.Type;
                            dr["Thumbnail"] = dlg1.Style.Thumbnail;
                            dr["StyleInfo"] = dlg1.Style;
                            this._dtFacStyles.Rows.Add(dr);
                            res = true;
                        }
                        break;
                    case StyleType.PipeNodeStyle:
                        FrmPipeNodeStyle dlg2 = new FrmPipeNodeStyle();
                        dlg2.Style.FacClassCode = facC.Code;
                        if (dlg2.ShowDialog() == DialogResult.OK)
                        {
                            DataRow dr = this._dtFacStyles.NewRow();
                            dr["Name"] = dlg2.Style.Name;
                            dr["StyleType"] = dlg2.Style.Type;
                            dr["Thumbnail"] = dlg2.Style.Thumbnail;
                            dr["StyleInfo"] = dlg2.Style;
                            this._dtFacStyles.Rows.Add(dr);
                            res = true;
                        }
                        break;
                    case StyleType.PipeBuildStyle:
                        FrmPipeBuildStyle dlg3 = new FrmPipeBuildStyle();
                        dlg3.Style.FacClassCode = facC.Code;
                        if (dlg3.ShowDialog() == DialogResult.OK)
                        {
                            DataRow dr = this._dtFacStyles.NewRow();
                            dr["Name"] = dlg3.Style.Name;
                            dr["StyleType"] = dlg3.Style.Type;
                            dr["Thumbnail"] = dlg3.Style.Thumbnail;
                            dr["StyleInfo"] = dlg3.Style;
                            this._dtFacStyles.Rows.Add(dr);
                            res = true;
                        }
                        break;
                }
            }
        }

        private void btnEditStyle_Click(object sender, EventArgs e)
        {
            int index = this.layoutView1.FocusedRowHandle;
            if (index < 0) return;
            DataRow dr = this.layoutView1.GetDataRow(index);
            if (dr == null) return;
            FacStyleClass styleInfo = (FacStyleClass)dr["StyleInfo"];
            if (styleInfo == null) return;
            bool bRes = false;
            switch (styleInfo.Type)
            {
                case StyleType.PipeLineStyle:
                    FrmPipeLineStyle dlg1 = new FrmPipeLineStyle(styleInfo as PipeLineStyleClass);
                    if (dlg1.ShowDialog() == DialogResult.OK)
                    {
                        bRes = true;
                    }
                    break;
                case StyleType.PipeNodeStyle:
                    FrmPipeNodeStyle dlg2 = new FrmPipeNodeStyle(styleInfo as PipeNodeStyleClass);
                    if (dlg2.ShowDialog() == DialogResult.OK)
                    {
                        bRes = true;
                    }
                    break;
                case StyleType.PipeBuildStyle:
                    FrmPipeBuildStyle dlg3 = new FrmPipeBuildStyle(styleInfo as PipeBuildStyleClass);
                    if (dlg3.ShowDialog() == DialogResult.OK)
                    {
                        bRes = true;
                    }
                    break;
            }
            if (bRes)
            {
                DataRow dr1 = this.layoutView1.GetDataRow(this.layoutView1.FocusedRowHandle);
                if (dr1 == null) return;
                dr1["Name"] = styleInfo.Name;
                dr1["StyleType"] = styleInfo.Type;
                dr1["Thumbnail"] = styleInfo.Thumbnail;
                dr1["StyleInfo"] = styleInfo;
                this.layoutView1.RefreshData();
            }
        }

        private void btnDeleteStyle_Click(object sender, EventArgs e)
        {
            int index = this.layoutView1.FocusedRowHandle;
            if (index < 0) return;
            DataRow dr = this.layoutView1.GetDataRow(index);
            if (dr == null) return;
            FacStyleClass style = dr["StyleInfo"] as FacStyleClass;
            if (style == null) return;
            if (DeleteFacStyleClass(style))
                this._dtFacStyles.Rows.Remove(dr);
            else XtraMessageBox.Show("删除失败！", "提示");
        }

        private bool DeleteFacStyleClass(FacStyleClass style)
        {
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityStyle");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("ObjectId = '{0}'", style.ObjectId);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void layoutView1_MouseUp(object sender, MouseEventArgs e)
        {
            LayoutViewHitInfo layoutViewHitInfo = this.layoutView1.CalcHitInfo(e.Location);
            if (layoutViewHitInfo != null)
            {
                int rowhandle = layoutViewHitInfo.RowHandle;
                if (rowhandle >= 0)
                {
                    this.btnEditStyle.Enabled = true;
                    this.btnDeleteStyle.Enabled = true;
                }
                else
                {
                    this.btnEditStyle.Enabled = false;
                    this.btnDeleteStyle.Enabled = false;
                }
            }
        }

        private void repositoryItemRadioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = this.gvFields.GetDataRow(this.gvFields.FocusedRowHandle);
            if (dr == null || dr["FieldInfo"] == null) return;
            RadioGroup rg = sender as RadioGroup;
            dr["Nullable"] = rg.EditValue;
            if (dr["FieldInfo"] != null && dr["FieldInfo"] is CMFieldConfig)
            {
                CMFieldConfig cmfc = dr["FieldInfo"] as CMFieldConfig;
                cmfc.Nullable = rg.EditValue.ToString() == "true" ? true : false;
                dr["FieldInfo"] = cmfc;
            }
            if (dr["FieldInfo"] != null && dr["FieldInfo"] is DFDataConfig.Class.FieldInfo)
            {
                DFDataConfig.Class.FieldInfo fi = dr["FieldInfo"] as DFDataConfig.Class.FieldInfo;
                CMFieldConfig cmfc = new CMFieldConfig(fi.Name, fi.Alias, fi.SystemName, fi.SystemAlias);
                cmfc.FieldType = gviFieldType.gviFieldString;
                cmfc.Nullable = rg.EditValue.ToString() == "true" ? true : false;
                dr["FieldInfo"] = cmfc;
            }

            this.gvFields.RefreshRow(this.gvFields.FocusedRowHandle);
        }

        private void repositoryItemSpinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            DataRow dr = this.gvFields.GetDataRow(this.gvFields.FocusedRowHandle);
            if (dr == null || dr["FieldInfo"] == null) return;
            SpinEdit rg = sender as SpinEdit;
            dr["Length"] = rg.EditValue;
            if (dr["FieldInfo"] != null && dr["FieldInfo"] is CMFieldConfig)
            {
                CMFieldConfig cmfc = dr["FieldInfo"] as CMFieldConfig;
                if (dr["FieldType"].ToString() == "gviFieldString")
                {
                    cmfc.Length = int.Parse(rg.EditValue.ToString());
                }
                else
                {
                    cmfc.Length = 0;
                    dr["Length"] = 0;
                }
                dr["FieldInfo"] = cmfc;
            }
            if (dr["FieldInfo"] != null && dr["FieldInfo"] is DFDataConfig.Class.FieldInfo)
            {
                DFDataConfig.Class.FieldInfo fi = dr["FieldInfo"] as DFDataConfig.Class.FieldInfo;
                CMFieldConfig cmfc = new CMFieldConfig(fi.Name, fi.Alias, fi.SystemName, fi.SystemAlias);
                cmfc.FieldType = gviFieldType.gviFieldString;
                if (dr["FieldType"].ToString() == "gviFieldString")
                {
                    cmfc.Length = int.Parse(rg.EditValue.ToString());
                }
                else
                {
                    cmfc.Length = 0;
                    dr["Length"] = 0;
                }
                dr["FieldInfo"] = cmfc;
            }

            this.gvFields.RefreshRow(this.gvFields.FocusedRowHandle);
        }

        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = this.gvFields.GetDataRow(this.gvFields.FocusedRowHandle);
            if (dr == null || dr["FieldInfo"] == null) return;
            ComboBoxEdit rg = sender as ComboBoxEdit;
            dr["FieldType"] = rg.EditValue;
            if (dr["FieldInfo"] != null && dr["FieldInfo"] is CMFieldConfig)
            {
                CMFieldConfig cmfc = dr["FieldInfo"] as CMFieldConfig;
                cmfc.FieldType = (gviFieldType)Enum.Parse(typeof(gviFieldType), rg.EditValue.ToString());
                if (dr["FieldType"].ToString() == "gviFieldString")
                {
                    dr["Length"] = 0;
                    cmfc.Length = 0;
                }
                else
                {
                    dr["Length"] = cmfc.Length;
                }
                dr["FieldInfo"] = cmfc;
            }
            if (dr["FieldInfo"] != null && dr["FieldInfo"] is DFDataConfig.Class.FieldInfo)
            {
                DFDataConfig.Class.FieldInfo fi = dr["FieldInfo"] as DFDataConfig.Class.FieldInfo;
                CMFieldConfig cmfc = new CMFieldConfig(fi.Name, fi.Alias, fi.SystemName, fi.SystemAlias);
                cmfc.FieldType = gviFieldType.gviFieldString;
                cmfc.FieldType = (gviFieldType)Enum.Parse(typeof(gviFieldType), rg.EditValue.ToString());
                if (dr["FieldType"].ToString() == "gviFieldString")
                {
                    dr["Length"] = 0;
                    cmfc.Length = 0;
                }
                else
                {
                    dr["Length"] = cmfc.Length;
                }
                dr["FieldInfo"] = cmfc;
            }
            this.gvFields.RefreshRow(this.gvFields.FocusedRowHandle);
        }

        private void gvFields_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = this.gvFields.CalcHitInfo(e.X, e.Y);
            if (info == null) return;
            if (info.RowHandle < 0) return;
            DataRow dr = this.gvFields.GetDataRow(info.RowHandle);
            if (dr != null && dr["FieldInfo"] != null)
            {
                if (dr["FieldInfo"] is DFDataConfig.Class.FieldInfo)
                {
                    if (dr["FieldType"].ToString() != "gviFieldString" && info.Column.FieldName == "Length")
                    {
                        info.Column.OptionsColumn.AllowEdit = false;
                        return;
                    }
                    else if (dr["FieldType"].ToString() == "gviFieldString" && info.Column.FieldName == "Length")
                    {
                        info.Column.OptionsColumn.AllowEdit = true;
                        return;
                    }
                }
            }
        }

        private void btnImportShp_Click(object sender, EventArgs e)
        {
            if (this._dtFacFields == null || this._dtFacFields.Rows.Count == 0)
            {
                XtraMessageBox.Show("字段列表信息为空，请查看！", "提示");
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Shape File(*.shp)|*.shp",
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                if (!File.Exists(fileName))
                {
                    XtraMessageBox.Show("文件不存在，请查看！", "提示");
                    return;
                }
                try
                {
                    IDataInteropFactory interopFact = new DataInteropFactoryClass();
                    IPropertySet ps = new PropertySetClass();
                    ps.SetProperty("FILENAME", fileName);
                    IDataInterop di = interopFact.CreateDataInterop(gviDataConnectionType.gviOgrConnectionShp, ps);
                    if (di == null) return;
                    IFieldInfoCollection fields = di.LayersInfo[0].FieldInfos;
                    if (fields == null || fields.Count == 0) return;

                    foreach (DataRow dr in this._dtFacFields.Rows)
                    {
                        if (dr["FieldInfo"] != null)
                        {
                            if (dr["FieldInfo"] is CMFieldConfig)
                            {
                                string name = dr["Name"].ToString();
                                int index = fields.IndexOf(name);
                                if (index != -1)
                                {
                                    CMFieldConfig cmfc = dr["FieldInfo"] as CMFieldConfig;
                                    IFieldInfo fi = fields.Get(index);
                                    dr["FieldType"] = fi.FieldType;
                                    dr["Length"] = fi.Length;
                                    dr["Nullable"] = fi.Nullable ? "true" : "false";
                                    cmfc.FieldType = fi.FieldType;
                                    cmfc.Length = fi.Length;
                                    cmfc.Nullable = fi.Nullable;
                                    dr["FieldInfo"] = cmfc;
                                }
                            }
                            else if (dr["FieldInfo"] is DFDataConfig.Class.FieldInfo)
                            {
                                string name = dr["Name"].ToString();
                                int index = fields.IndexOf(name);
                                if (index != -1)
                                {
                                    DFDataConfig.Class.FieldInfo dffi = dr["FieldInfo"] as DFDataConfig.Class.FieldInfo;
                                    CMFieldConfig cmfc = new CMFieldConfig(dffi.Name, dffi.Alias, dffi.SystemName, dffi.SystemAlias);
                                    IFieldInfo fi = fields.Get(index);
                                    dr["FieldType"] = fi.FieldType;
                                    dr["Length"] = fi.Length;
                                    dr["Nullable"] = fi.Nullable ? "true" : "false";
                                    cmfc.FieldType = fi.FieldType;
                                    cmfc.Length = fi.Length;
                                    cmfc.Nullable = fi.Nullable;
                                    dr["FieldInfo"] = cmfc;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("读取shp数据出错！", "提示");
                }
            }
        }
    }
}
