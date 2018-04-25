using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using DF3DScan.Class;

namespace DF3DScan.Frm
{
    public class AttributeEditDlg : XtraForm
    {
        private System.ComponentModel.IContainer components;
        private LayoutControl layoutControl1;
        private SimpleButton btnOK;
        private TextEdit txtInputValue;
        private SimpleButton btnCancel;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutText;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private EmptySpaceItem emptySpaceItem2;
        private EmptySpaceItem emptySpaceItem3;
        private EmptySpaceItem emptySpaceItem1;
        private SpinEdit spinEdit;
        private LayoutControlItem layoutSpin;
        private DateEdit dateEdit;
        private LayoutControlItem layoutDate;
        private DXValidationProvider dxValidationProvider1;
        private ComboBoxEdit cmdEdit;
        private LayoutControlItem layoucombox;
        private string _fieldName;
        private CustomValidationRule _role;
        public string InputValue
        {
            get
            {
                if (this._fieldName == "FlyTime")
                {
                    return this.spinEdit.Text;
                }
                if (this._fieldName == "FlyMode")
                {
                    return this.cmdEdit.Text;
                }
                return "";
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dateEdit = new DevExpress.XtraEditors.DateEdit();
            this.spinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtInputValue = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutText = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutSpin = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoucombox = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmdEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInputValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSpin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoucombox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdEdit);
            this.layoutControl1.Controls.Add(this.dateEdit);
            this.layoutControl1.Controls.Add(this.spinEdit);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.txtInputValue);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(367, 78);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(299, 12);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmdEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmdEdit.Size = new System.Drawing.Size(56, 22);
            this.cmdEdit.StyleController = this.layoutControl1;
            this.cmdEdit.TabIndex = 3;
            // 
            // dateEdit
            // 
            this.dateEdit.EditValue = null;
            this.dateEdit.Location = new System.Drawing.Point(213, 12);
            this.dateEdit.Name = "dateEdit";
            this.dateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit.Size = new System.Drawing.Size(55, 22);
            this.dateEdit.StyleController = this.layoutControl1;
            this.dateEdit.TabIndex = 2;
            // 
            // spinEdit
            // 
            this.spinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit.Location = new System.Drawing.Point(126, 12);
            this.spinEdit.Name = "spinEdit";
            this.spinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit.Size = new System.Drawing.Size(56, 22);
            this.spinEdit.StyleController = this.layoutControl1;
            this.spinEdit.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(233, 38);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(53, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(100, 38);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtInputValue
            // 
            this.txtInputValue.Location = new System.Drawing.Point(39, 12);
            this.txtInputValue.Name = "txtInputValue";
            this.txtInputValue.Size = new System.Drawing.Size(56, 22);
            this.txtInputValue.StyleController = this.layoutControl1;
            this.txtInputValue.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutText,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.emptySpaceItem1,
            this.layoutSpin,
            this.layoutDate,
            this.layoucombox});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(367, 78);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutText
            // 
            this.layoutText.Control = this.txtInputValue;
            this.layoutText.CustomizationFormText = "输入";
            this.layoutText.Location = new System.Drawing.Point(0, 0);
            this.layoutText.Name = "layoutText";
            this.layoutText.Size = new System.Drawing.Size(87, 26);
            this.layoutText.Text = "标注";
            this.layoutText.TextSize = new System.Drawing.Size(24, 14);
            this.layoutText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOK;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(88, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(64, 32);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(221, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(57, 32);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(88, 32);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(152, 26);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(69, 32);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(278, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(69, 32);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutSpin
            // 
            this.layoutSpin.Control = this.spinEdit;
            this.layoutSpin.CustomizationFormText = "输入";
            this.layoutSpin.Location = new System.Drawing.Point(87, 0);
            this.layoutSpin.Name = "layoutSpin";
            this.layoutSpin.Size = new System.Drawing.Size(87, 26);
            this.layoutSpin.Text = "输入";
            this.layoutSpin.TextSize = new System.Drawing.Size(24, 14);
            this.layoutSpin.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutDate
            // 
            this.layoutDate.Control = this.dateEdit;
            this.layoutDate.CustomizationFormText = "输入";
            this.layoutDate.Location = new System.Drawing.Point(174, 0);
            this.layoutDate.Name = "layoutDate";
            this.layoutDate.Size = new System.Drawing.Size(86, 26);
            this.layoutDate.Text = "输入";
            this.layoutDate.TextSize = new System.Drawing.Size(24, 14);
            this.layoutDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoucombox
            // 
            this.layoucombox.Control = this.cmdEdit;
            this.layoucombox.CustomizationFormText = "输入";
            this.layoucombox.Location = new System.Drawing.Point(260, 0);
            this.layoucombox.Name = "layoucombox";
            this.layoucombox.Size = new System.Drawing.Size(87, 26);
            this.layoucombox.Text = "输入";
            this.layoucombox.TextSize = new System.Drawing.Size(24, 14);
            this.layoucombox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // AttributeEditDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(367, 78);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(373, 106);
            this.MinimumSize = new System.Drawing.Size(367, 78);
            this.Name = "AttributeEditDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性编辑";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmdEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInputValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSpin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoucombox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }
        public AttributeEditDlg(string fieldName, double maxValue)
        {
            this.InitializeComponent();
            this.dxValidationProvider1.ValidationMode = ValidationMode.Manual;
            this.dxValidationProvider1.SetIconAlignment(this.spinEdit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxValidationProvider1.SetIconAlignment(this.cmdEdit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this._fieldName = fieldName;
            this._role = new CustomValidationRule(fieldName);
            if (fieldName == "FlyTime")
            {
                this.layoutSpin.Visibility = LayoutVisibility.Always;
                this.spinEdit.Properties.MinValue = 0m;
                this.spinEdit.Properties.MaxValue = (decimal)maxValue;
                this.dxValidationProvider1.SetValidationRule(this.spinEdit, this._role);
                return;
            }
            if (fieldName == "FlyMode")
            {
                this.layoucombox.Visibility = LayoutVisibility.Always;
                this.cmdEdit.Properties.Items.Add("Smooth");
                this.cmdEdit.Properties.Items.Add("Bounce");
                this.cmdEdit.Properties.Items.Add("Linear");
                this.cmdEdit.Properties.CycleOnDblClick = true;
                this.cmdEdit.SelectedItem = "Smooth";
                this.cmdEdit.DoubleClick += new EventHandler(this.cmdEdit_DoubleClick);
                this.dxValidationProvider1.SetValidationRule(this.cmdEdit, this._role);
            }
        }
        private void cmdEdit_DoubleClick(object sender, System.EventArgs e)
        {
            ComboBoxEdit comboBoxEdit = sender as ComboBoxEdit;
            if (comboBoxEdit.Text == "Smooth")
            {
                comboBoxEdit.EditValue = "Bounce";
                return;
            }
            if (comboBoxEdit.Text == "Bounce")
            {
                comboBoxEdit.EditValue = "Linear";
                return;
            }
            if (comboBoxEdit.Text == "Linear")
            {
                comboBoxEdit.EditValue = "Smooth";
            }
        }
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
            {
                base.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }
    }
}
