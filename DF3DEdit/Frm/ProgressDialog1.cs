using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICSharpCode.Core;

namespace DF3DEdit.Frm
{
    public class ProgressDialog1 : XtraForm
    {
        private IContainer components;
        public ProgressBarControl progressBarControl;
        public System.Windows.Forms.Label labelTooltip;
        public SimpleButton btnCancle;
        private BackgroundWorker _bgWorker;
        private System.Threading.ManualResetEvent _manualResult;
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
            this.btnCancle = new SimpleButton();
            this.labelTooltip = new System.Windows.Forms.Label();
            this.progressBarControl.Properties.BeginInit();
            base.SuspendLayout();
            this.progressBarControl.Location = new System.Drawing.Point(12, 32);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Properties.ShowTitle = true;
            this.progressBarControl.Size = new System.Drawing.Size(320, 18);
            this.progressBarControl.TabIndex = 0;
            this.btnCancle.Location = new System.Drawing.Point(123, 61);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(56, 21);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = StringParser.Parse("${res:feature_btn_cancel}");
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            this.labelTooltip.Location = new System.Drawing.Point(12, 8);
            this.labelTooltip.Name = "labelTooltip";
            this.labelTooltip.Size = new System.Drawing.Size(330, 16);
            this.labelTooltip.TabIndex = 2;
            this.labelTooltip.Text = StringParser.Parse("${res:feature_label_tooltip}");
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(350, 100);
            base.ControlBox = false;
            base.Controls.Add(this.labelTooltip);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.progressBarControl);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ProgressDialog";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = StringParser.Parse("${res:feature_dlg_title}");
            this.progressBarControl.Properties.EndInit();
            base.ResumeLayout(false);
        }
        public ProgressDialog1(BackgroundWorker bgWorker, System.Threading.ManualResetEvent manualResult)
        {
            this.InitializeComponent();
            this._bgWorker = bgWorker;
            this._manualResult = manualResult;
        }
        private void btnCancle_Click(object sender, System.EventArgs e)
        {
            this._manualResult.Reset();
            System.Windows.Forms.DialogResult dialogResult = XtraMessageBox.Show(StringParser.Parse("${res:feature_confirm_cancel}"), StringParser.Parse("${res:feature_alert_tooltip}"), System.Windows.Forms.MessageBoxButtons.OKCancel);
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this._manualResult.Set();
                this._bgWorker.CancelAsync();
                this.btnCancle.Enabled = false;
                return;
            }
            this._manualResult.Set();
        }
    }
}
