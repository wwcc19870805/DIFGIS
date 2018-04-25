using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace DF3DEdit.Frm
{
    public class MergeDlg : XtraForm
    {
        private int fid = -1;
        private System.ComponentModel.IContainer components = null;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private SimpleButton btnCancle;
        private SimpleButton btnOk;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem4;
        private EmptySpaceItem emptySpaceItem1;
        private EmptySpaceItem emptySpaceItem2;
        private EmptySpaceItem emptySpaceItem3;
        private LabelControl labelControl1;
        private LayoutControlItem layoutControlItem5;
        private ListBoxControl lstBox;
        private LayoutControlItem layoutControlItem6;
        public int Fid
        {
            get
            {
                return this.fid;
            }
        }
        public MergeDlg(System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int, string>> selectionList)
        {
            this.InitializeComponent();
            this.lstBox.DataSource = selectionList;
            this.lstBox.DisplayMember = "Value";
            this.lstBox.ValueMember = "Key";
        }
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.fid = ((System.Collections.Generic.KeyValuePair<int, string>)this.lstBox.SelectedItem).Key;
            base.Close();
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
            this.layoutControl1 = new LayoutControl();
            this.lstBox = new ListBoxControl();
            this.labelControl1 = new LabelControl();
            this.btnCancle = new SimpleButton();
            this.btnOk = new SimpleButton();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.layoutControlItem3 = new LayoutControlItem();
            this.layoutControlItem4 = new LayoutControlItem();
            this.emptySpaceItem1 = new EmptySpaceItem();
            this.emptySpaceItem2 = new EmptySpaceItem();
            this.emptySpaceItem3 = new EmptySpaceItem();
            this.layoutControlItem5 = new LayoutControlItem();
            this.layoutControlItem6 = new LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.lstBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).BeginInit();
            base.SuspendLayout();
            this.layoutControl1.Controls.Add(this.lstBox);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btnCancle);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(286, 340);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            this.lstBox.Appearance.ForeColor = System.Drawing.Color.FromArgb(192, 192, 255);
            this.lstBox.Appearance.Options.UseForeColor = true;
            this.lstBox.Location = new System.Drawing.Point(12, 30);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(262, 272);
            this.lstBox.StyleController = this.layoutControl1;
            this.lstBox.TabIndex = 9;
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(252, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "提示:模型合并不支持撤销操作,请小心使用!";
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(176, 306);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(64, 22);
            this.btnCancle.StyleController = this.layoutControl1;
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "取消";
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(46, 306);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[]
			{
				this.layoutControlItem3, 
				this.layoutControlItem4, 
				this.emptySpaceItem1, 
				this.emptySpaceItem2, 
				this.emptySpaceItem3, 
				this.layoutControlItem5, 
				this.layoutControlItem6
			});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(286, 340);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            this.layoutControlItem3.Control = this.btnOk;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(34, 294);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(68, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            this.layoutControlItem4.Control = this.btnCancle;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(164, 294);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(68, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 294);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(34, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(232, 294);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(34, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(102, 294);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(62, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.Control = this.labelControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(266, 18);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            this.layoutControlItem6.Control = this.lstBox;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(266, 276);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(286, 340);
            base.Controls.Add(this.layoutControl1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            base.Name = "MergeDlg";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模型合并";
            ((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.lstBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).EndInit();
            base.ResumeLayout(false);
        }
    }
}
