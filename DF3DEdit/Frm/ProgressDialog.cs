using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DF3DEdit.Frm
{
    public class ProgressDialog : XtraForm
    {
        private System.ComponentModel.IContainer components;
        public ProgressBarControl progressBarControl;
        public System.Windows.Forms.Label labelTooltip;
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
            this.labelTooltip = new System.Windows.Forms.Label();
            this.progressBarControl.Properties.BeginInit();
            base.SuspendLayout();
            this.progressBarControl.Location = new System.Drawing.Point(12, 32);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Properties.ShowTitle = true;
            this.progressBarControl.Size = new System.Drawing.Size(320, 18);
            this.progressBarControl.TabIndex = 0;
            this.labelTooltip.Location = new System.Drawing.Point(12, 8);
            this.labelTooltip.Name = "labelTooltip";
            this.labelTooltip.Size = new System.Drawing.Size(330, 16);
            this.labelTooltip.TabIndex = 2;
            this.labelTooltip.Text = "初始化,请稍后...";
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(350, 100);
            base.ControlBox = false;
            base.Controls.Add(this.labelTooltip);
            base.Controls.Add(this.progressBarControl);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ProgressDialog";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "进度条";
            this.progressBarControl.Properties.EndInit();
            base.ResumeLayout(false);
        }
        public ProgressDialog()
        {
            this.InitializeComponent();
        }
    }
}
