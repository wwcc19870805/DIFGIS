using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DData.Frm;
using Gvitech.CityMaker.FdeCore;

namespace DF3DData.UserControl
{
    public class UCRangeSymbol : XtraUserControl
    {
        private ToolStripMenuItem ToolStripMenuItemDelete;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ToolStripMenuItemEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn symbleCol;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn minValueCol;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn maxValueCol;
        private DevExpress.XtraGrid.Columns.GridColumn typeValueCol;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private CheckEdit checkEditDefault;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ColorEdit colorEditDefault;
        private SpinEdit spinEditRang;
        private ComboBoxEdit cmbField;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;

        private gviGeometryColumnType _geoType;
        private UCModelPointSymbol _ucModelPointSymbol;
        private UCPointSymbol _ucPointSymbol;
        private UCPolylineSymbol _ucPolylineSymbol;
        private UCPolygonSymbol _ucPolygonSymbol;

        public UCRangeSymbol(gviGeometryColumnType geoType)
        {
            InitializeComponent();
            this._geoType = geoType;
        }

        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.ToolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.symbleCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.minValueCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.maxValueCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.typeValueCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkEditDefault = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.colorEditDefault = new DevExpress.XtraEditors.ColorEdit();
            this.spinEditRang = new DevExpress.XtraEditors.SpinEdit();
            this.cmbField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorEditDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Size = new System.Drawing.Size(181, 22);
            this.ToolStripMenuItemDelete.Text = "${res:View_Delete}";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEdit,
            this.ToolStripMenuItemDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(182, 48);
            // 
            // ToolStripMenuItemEdit
            // 
            this.ToolStripMenuItemEdit.Name = "ToolStripMenuItemEdit";
            this.ToolStripMenuItemEdit.Size = new System.Drawing.Size(181, 22);
            this.ToolStripMenuItemEdit.Text = "${res:View_Delete}";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(266, 320);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 90);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemColorEdit1,
            this.repositoryItemSpinEdit1,
            this.repositoryItemComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(262, 316);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.gridView1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.CornflowerBlue;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.CornflowerBlue;
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.symbleCol,
            this.minValueCol,
            this.maxValueCol,
            this.typeValueCol});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // symbleCol
            // 
            this.symbleCol.Caption = "符号";
            this.symbleCol.ColumnEdit = this.repositoryItemColorEdit1;
            this.symbleCol.FieldName = "ColorCol";
            this.symbleCol.Name = "symbleCol";
            this.symbleCol.OptionsColumn.AllowEdit = false;
            this.symbleCol.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.symbleCol.OptionsColumn.AllowIncrementalSearch = false;
            this.symbleCol.OptionsColumn.AllowMove = false;
            this.symbleCol.OptionsColumn.AllowShowHide = false;
            this.symbleCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.symbleCol.OptionsColumn.ReadOnly = true;
            this.symbleCol.OptionsColumn.ShowInCustomizationForm = false;
            this.symbleCol.OptionsColumn.ShowInExpressionEditor = false;
            this.symbleCol.OptionsFilter.AllowAutoFilter = false;
            this.symbleCol.OptionsFilter.AllowFilter = false;
            this.symbleCol.Visible = true;
            this.symbleCol.VisibleIndex = 0;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.repositoryItemColorEdit1.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.ReadOnly = true;
            // 
            // minValueCol
            // 
            this.minValueCol.Caption = "下限";
            this.minValueCol.ColumnEdit = this.repositoryItemSpinEdit1;
            this.minValueCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.minValueCol.FieldName = "minValueCol";
            this.minValueCol.Name = "minValueCol";
            this.minValueCol.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.minValueCol.OptionsColumn.AllowIncrementalSearch = false;
            this.minValueCol.OptionsColumn.AllowMove = false;
            this.minValueCol.OptionsColumn.AllowShowHide = false;
            this.minValueCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.minValueCol.OptionsColumn.ShowInCustomizationForm = false;
            this.minValueCol.OptionsFilter.AllowAutoFilter = false;
            this.minValueCol.OptionsFilter.AllowFilter = false;
            this.minValueCol.Visible = true;
            this.minValueCol.VisibleIndex = 1;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // maxValueCol
            // 
            this.maxValueCol.Caption = "上限";
            this.maxValueCol.ColumnEdit = this.repositoryItemSpinEdit1;
            this.maxValueCol.FieldName = "maxValueCol";
            this.maxValueCol.Name = "maxValueCol";
            this.maxValueCol.Visible = true;
            this.maxValueCol.VisibleIndex = 2;
            // 
            // typeValueCol
            // 
            this.typeValueCol.Caption = "区间类型";
            this.typeValueCol.ColumnEdit = this.repositoryItemComboBox1;
            this.typeValueCol.FieldName = "typeValueCol";
            this.typeValueCol.Name = "typeValueCol";
            this.typeValueCol.Visible = true;
            this.typeValueCol.VisibleIndex = 3;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.checkEditDefault;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // checkEditDefault
            // 
            this.checkEditDefault.EditValue = true;
            this.checkEditDefault.Location = new System.Drawing.Point(12, 64);
            this.checkEditDefault.Name = "checkEditDefault";
            this.checkEditDefault.Properties.Caption = "默认";
            this.checkEditDefault.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.checkEditDefault.Size = new System.Drawing.Size(69, 19);
            this.checkEditDefault.StyleController = this.layoutControl1;
            this.checkEditDefault.TabIndex = 2;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.colorEditDefault);
            this.layoutControl1.Controls.Add(this.checkEditDefault);
            this.layoutControl1.Controls.Add(this.spinEditRang);
            this.layoutControl1.Controls.Add(this.cmbField);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(286, 418);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // colorEditDefault
            // 
            this.colorEditDefault.EditValue = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.colorEditDefault.Location = new System.Drawing.Point(85, 64);
            this.colorEditDefault.Name = "colorEditDefault";
            this.colorEditDefault.Properties.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.False;
            this.colorEditDefault.Properties.AllowMouseWheel = false;
            this.colorEditDefault.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.colorEditDefault.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colorEditDefault.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.colorEditDefault.Size = new System.Drawing.Size(189, 22);
            this.colorEditDefault.StyleController = this.layoutControl1;
            this.colorEditDefault.TabIndex = 3;
            this.colorEditDefault.Click += new System.EventHandler(this.colorEditDefault_Click);
            // 
            // spinEditRang
            // 
            this.spinEditRang.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditRang.Location = new System.Drawing.Point(39, 38);
            this.spinEditRang.Name = "spinEditRang";
            this.spinEditRang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditRang.Properties.IsFloatValue = false;
            this.spinEditRang.Properties.Mask.EditMask = "N00";
            this.spinEditRang.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spinEditRang.Size = new System.Drawing.Size(235, 22);
            this.spinEditRang.StyleController = this.layoutControl1;
            this.spinEditRang.TabIndex = 1;
            // 
            // cmbField
            // 
            this.cmbField.Location = new System.Drawing.Point(39, 12);
            this.cmbField.Name = "cmbField";
            this.cmbField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbField.Size = new System.Drawing.Size(235, 22);
            this.cmbField.StyleController = this.layoutControl1;
            this.cmbField.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(286, 418);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbField;
            this.layoutControlItem1.CustomizationFormText = "字段";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem1.Text = "字段";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinEditRang;
            this.layoutControlItem2.CustomizationFormText = "段数";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem2.Text = "段数";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.colorEditDefault;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(73, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(193, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // UCRangeSymbol
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCRangeSymbol";
            this.Size = new System.Drawing.Size(286, 418);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colorEditDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        private void colorEditDefault_Click(object sender, EventArgs e)
        {
            FormLabelOrSymbol dlg = new FormLabelOrSymbol();
            dlg.Text = "符号";
            switch (this._geoType)
            {
                case gviGeometryColumnType.gviGeometryColumnPoint:
                    this._ucPointSymbol = new UCPointSymbol();
                    this._ucPointSymbol.Dock = DockStyle.Fill;
                    dlg.AddUC(this._ucPointSymbol);
                    break;
                case gviGeometryColumnType.gviGeometryColumnPolygon:
                    this._ucPolygonSymbol = new UCPolygonSymbol();
                    this._ucPolygonSymbol.Dock = DockStyle.Fill;
                    dlg.AddUC(this._ucPolygonSymbol);
                    break;
                case gviGeometryColumnType.gviGeometryColumnPolyline:
                    this._ucPolylineSymbol = new UCPolylineSymbol();
                    this._ucPolylineSymbol.Dock = DockStyle.Fill;
                    dlg.AddUC(this._ucPolylineSymbol);
                    break;
                case gviGeometryColumnType.gviGeometryColumnModelPoint:
                    this._ucModelPointSymbol = new UCModelPointSymbol();
                    this._ucModelPointSymbol.Dock = DockStyle.Fill;
                    dlg.AddUC(this._ucModelPointSymbol);
                    break;
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {

            }

        }
    }
}
