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

namespace DF3DData.UserControl
{
    public class UCEnumLabel : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ComboBoxEdit cmbField;
        private ColorEdit colorEditDefault;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn symbleCol;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn fieldValueCol;
        private SimpleButton btnAddAll;
        private SimpleButton btnDelAll;
        private SimpleButton btnAddValue;
        private CheckEdit checkEditDefault;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    
        public UCEnumLabel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.colorEditDefault = new DevExpress.XtraEditors.ColorEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.symbleCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.fieldValueCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddValue = new DevExpress.XtraEditors.SimpleButton();
            this.checkEditDefault = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEditDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmbField);
            this.layoutControl1.Controls.Add(this.colorEditDefault);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnAddAll);
            this.layoutControl1.Controls.Add(this.btnDelAll);
            this.layoutControl1.Controls.Add(this.btnAddValue);
            this.layoutControl1.Controls.Add(this.checkEditDefault);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(286, 463);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
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
            // colorEditDefault
            // 
            this.colorEditDefault.EditValue = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.colorEditDefault.Location = new System.Drawing.Point(62, 38);
            this.colorEditDefault.Name = "colorEditDefault";
            this.colorEditDefault.Properties.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.False;
            this.colorEditDefault.Properties.AllowMouseWheel = false;
            this.colorEditDefault.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.colorEditDefault.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colorEditDefault.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.colorEditDefault.Size = new System.Drawing.Size(212, 22);
            this.colorEditDefault.StyleController = this.layoutControl1;
            this.colorEditDefault.TabIndex = 2;
            this.colorEditDefault.Click += new System.EventHandler(this.colorEditDefault_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 90);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemColorEdit1});
            this.gridControl1.Size = new System.Drawing.Size(262, 361);
            this.gridControl1.TabIndex = 6;
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
            this.fieldValueCol});
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
            // fieldValueCol
            // 
            this.fieldValueCol.Caption = "字段值";
            this.fieldValueCol.FieldName = "ValueCol";
            this.fieldValueCol.Name = "fieldValueCol";
            this.fieldValueCol.OptionsColumn.AllowEdit = false;
            this.fieldValueCol.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.fieldValueCol.OptionsColumn.AllowIncrementalSearch = false;
            this.fieldValueCol.OptionsColumn.AllowMove = false;
            this.fieldValueCol.OptionsColumn.AllowShowHide = false;
            this.fieldValueCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.fieldValueCol.OptionsColumn.ReadOnly = true;
            this.fieldValueCol.OptionsColumn.ShowInCustomizationForm = false;
            this.fieldValueCol.OptionsFilter.AllowAutoFilter = false;
            this.fieldValueCol.OptionsFilter.AllowFilter = false;
            this.fieldValueCol.Visible = true;
            this.fieldValueCol.VisibleIndex = 1;
            // 
            // btnAddAll
            // 
            this.btnAddAll.Location = new System.Drawing.Point(12, 64);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(83, 22);
            this.btnAddAll.StyleController = this.layoutControl1;
            this.btnAddAll.TabIndex = 3;
            this.btnAddAll.Text = "添加所有";
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnDelAll
            // 
            this.btnDelAll.Location = new System.Drawing.Point(184, 64);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new System.Drawing.Size(90, 22);
            this.btnDelAll.StyleController = this.layoutControl1;
            this.btnDelAll.TabIndex = 5;
            this.btnDelAll.Text = "删除所有";
            this.btnDelAll.Click += new System.EventHandler(this.btnDelAll_Click);
            // 
            // btnAddValue
            // 
            this.btnAddValue.Location = new System.Drawing.Point(99, 64);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new System.Drawing.Size(81, 22);
            this.btnAddValue.StyleController = this.layoutControl1;
            this.btnAddValue.TabIndex = 4;
            this.btnAddValue.Text = "添加值";
            this.btnAddValue.Click += new System.EventHandler(this.btnAddValue_Click);
            // 
            // checkEditDefault
            // 
            this.checkEditDefault.EditValue = true;
            this.checkEditDefault.Location = new System.Drawing.Point(12, 38);
            this.checkEditDefault.Name = "checkEditDefault";
            this.checkEditDefault.Properties.Caption = "默认";
            this.checkEditDefault.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.checkEditDefault.Size = new System.Drawing.Size(46, 19);
            this.checkEditDefault.StyleController = this.layoutControl1;
            this.checkEditDefault.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem2,
            this.layoutControlItem7,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(286, 463);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.checkEditDefault;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(50, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnAddAll;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(266, 365);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.colorEditDefault;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(50, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(216, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnAddValue;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(87, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(85, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnDelAll;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(172, 52);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cmbField;
            this.layoutControlItem8.CustomizationFormText = "字段";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem8.Text = "字段";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.CustomizationFormText = "${res:View_Filed}";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(391, 26);
            this.layoutControlItem1.Text = "${res:View_Filed}";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(97, 14);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // UCEnumLabel
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCEnumLabel";
            this.Size = new System.Drawing.Size(286, 463);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEditDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private UCSimpleLabel _ucSimpleLabel;

        private void colorEditDefault_Click(object sender, EventArgs e)
        {
            FormLabelOrSymbol dlg = new FormLabelOrSymbol();
            dlg.Text = "标注";
            this._ucSimpleLabel = new UCSimpleLabel();
            this._ucSimpleLabel.Dock = DockStyle.Fill;
            dlg.AddUC(this._ucSimpleLabel);
            if (dlg.ShowDialog() == DialogResult.OK)
            {

            }

        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {

        }

        private void btnAddValue_Click(object sender, EventArgs e)
        {

        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {

        }
    }
}
