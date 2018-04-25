using DevExpress.XtraEditors;
using ICSharpCode.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DF3DEdit.Frm
{
    public class MyProgressBar : XtraForm
    {
        private System.ComponentModel.IContainer components;
        public ProgressBarControl progressBarControl;
        public System.Windows.Forms.Label labelTooltip;
        public SimpleButton btnCancel;
        public LabelControl labelProgressValue;
        public bool CallbackCancel;
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
            this.progressBarControl = new ProgressBarControl();
            this.btnCancel = new SimpleButton();
            this.labelTooltip = new System.Windows.Forms.Label();
            this.labelProgressValue = new LabelControl();
            this.progressBarControl.Properties.BeginInit();
            base.SuspendLayout();
            this.progressBarControl.Location = new System.Drawing.Point(12, 32);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Properties.ShowTitle = true;
            this.progressBarControl.Size = new System.Drawing.Size(289, 18);
            this.progressBarControl.TabIndex = 0;
            this.btnCancel.Location = new System.Drawing.Point(123, 61);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 21);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = StringParser.Parse("${res:View_Cancel}");
            this.btnCancel.Click += new System.EventHandler(this.btnCancle_Click);
            this.labelTooltip.Location = new System.Drawing.Point(12, 8);
            this.labelTooltip.Name = "labelTooltip";
            this.labelTooltip.Size = new System.Drawing.Size(289, 16);
            this.labelTooltip.TabIndex = 2;
            this.labelTooltip.Text = StringParser.Parse("${res:View_LabelTooltip}");
            this.labelProgressValue.Location = new System.Drawing.Point(117, 33);
            this.labelProgressValue.Name = "labelProgressValue";
            this.labelProgressValue.Size = new System.Drawing.Size(70, 14);
            this.labelProgressValue.TabIndex = 3;
            this.labelProgressValue.Text = "labelControl1";
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(313, 100);
            base.ControlBox = false;
            base.Controls.Add(this.labelProgressValue);
            base.Controls.Add(this.labelTooltip);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.progressBarControl);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MyProgressBar";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = StringParser.Parse("${res:View_Title}");
            this.progressBarControl.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        public MyProgressBar()
        {
            this.InitializeComponent();
            this.progressBarControl.Properties.Minimum = 0;
            this.progressBarControl.Properties.Maximum = 100;
            this.progressBarControl.Properties.ProgressViewStyle = (DevExpress.XtraEditors.Controls.ProgressViewStyle)0;
            this.progressBarControl.Position = 0;
            base.ShowInTaskbar = false;
            this.labelProgressValue.Visible = false;
            this.progressBarControl.Visible = false;
            this.progressBarControl.Properties.PercentView = true;
        }
        private void btnCancle_Click(object sender, System.EventArgs e)
        {
            this.CallbackCancel = true;
            this.btnCancel.Enabled = false;
        }
    }
}
