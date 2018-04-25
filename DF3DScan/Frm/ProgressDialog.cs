using DevExpress.XtraEditors;
using Gvitech.CityMaker.RenderControl;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace DF3DScan.Frm
{
    public class ProgressDialog : XtraForm
    {
        private System.ComponentModel.IContainer components;
        public ProgressBarControl progressBarControl;
        public System.Windows.Forms.Label labelTooltip;
        public SimpleButton btnCancle;
        private ICameraTour _tour;
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
            this.progressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnCancle = new DevExpress.XtraEditors.SimpleButton();
            this.labelTooltip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarControl
            // 
            this.progressBarControl.Location = new System.Drawing.Point(12, 32);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Properties.ShowTitle = true;
            this.progressBarControl.Size = new System.Drawing.Size(289, 18);
            this.progressBarControl.TabIndex = 0;
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(123, 61);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(56, 21);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // labelTooltip
            // 
            this.labelTooltip.Location = new System.Drawing.Point(12, 12);
            this.labelTooltip.Name = "labelTooltip";
            this.labelTooltip.Size = new System.Drawing.Size(289, 14);
            this.labelTooltip.TabIndex = 2;
            this.labelTooltip.Text = "初始化,请稍候...";
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 100);
            this.ControlBox = false;
            this.Controls.Add(this.labelTooltip);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.progressBarControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "进度条";
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        public ProgressDialog(ICameraTour tour)
        {
            this.InitializeComponent();
            this._tour = tour;
        }
        private void btnCancle_Click(object sender, System.EventArgs e)
        {
            this._tour.CancelExport();
            base.Close();
        }
    }
}
