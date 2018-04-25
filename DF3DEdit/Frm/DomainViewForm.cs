using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using ICSharpCode.Core;
using DF3DEdit.UC;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DF3DEdit.Frm
{
    public class DomainViewForm : XtraForm
    {
        private System.ComponentModel.IContainer components;
        private UCDomainEdit domainEdit1;
        public DomainViewForm(IDataSource ds)
        {
            this.InitializeComponent();
            this.domainEdit1.BandDataSource(ds);
            this.domainEdit1.DomainInfoReadOnly = false;
            this.domainEdit1.DomainPropReadOnly = false;
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
            this.domainEdit1 = new UCDomainEdit();
            base.SuspendLayout();
            this.domainEdit1.Docked = System.Windows.Forms.DockStyle.None;
            this.domainEdit1.Location = new System.Drawing.Point(0, 0);
            this.domainEdit1.Name = "domainEdit1";
            this.domainEdit1.Size = new System.Drawing.Size(391, 409);
            this.domainEdit1.TabIndex = 0;
            base.ShowInTaskbar = false;
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(392, 410);
            base.Controls.Add(this.domainEdit1);
            base.Name = "DomainViewForm";
            this.Text = "属性域";
            base.ResumeLayout(false);
        }
    }
}
