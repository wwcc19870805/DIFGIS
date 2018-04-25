using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using ICSharpCode.Core;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DF3DEdit.Frm
{
    public class CreateDBIndexForm : XtraForm
    {
        private System.ComponentModel.IContainer components;
        private LabelControl lc_IndexName;
        private TextEdit te_IndexName;
        private GroupControl groupControl1;
        private SimpleButton sbtn_Next;
        private SimpleButton sbtn_Prev;
        private SimpleButton sbtn_Remove;
        private SimpleButton sbtn_Add;
        private ListBoxControl lbc_FieldsSelected;
        private LabelControl labelControl3;
        private LabelControl labelControl2;
        private ListBoxControl lbc_FieldsAvailable;
        private SimpleButton sbtn_OK;
        private SimpleButton sbtn_Cancel;
        private string Fid = "";
        private string indexName = "";
        private System.Collections.ArrayList selectFields = new System.Collections.ArrayList();
        private ListBoxControl lbcIndexName = new ListBoxControl();
        public System.Collections.ArrayList SelectFields
        {
            get
            {
                return this.selectFields;
            }
        }
        public string IndexName
        {
            get
            {
                return this.indexName;
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
            this.lc_IndexName = new DevExpress.XtraEditors.LabelControl();
            this.te_IndexName = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.sbtn_Next = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_Remove = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_Add = new DevExpress.XtraEditors.SimpleButton();
            this.lbc_FieldsSelected = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lbc_FieldsAvailable = new DevExpress.XtraEditors.ListBoxControl();
            this.sbtn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_Prev = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.te_IndexName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_FieldsSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_FieldsAvailable)).BeginInit();
            this.SuspendLayout();
            // 
            // lc_IndexName
            // 
            this.lc_IndexName.Location = new System.Drawing.Point(40, 18);
            this.lc_IndexName.Name = "lc_IndexName";
            this.lc_IndexName.Size = new System.Drawing.Size(24, 14);
            this.lc_IndexName.TabIndex = 0;
            this.lc_IndexName.Text = "名称";
            // 
            // te_IndexName
            // 
            this.te_IndexName.Location = new System.Drawing.Point(95, 15);
            this.te_IndexName.Name = "te_IndexName";
            this.te_IndexName.Size = new System.Drawing.Size(238, 22);
            this.te_IndexName.TabIndex = 1;
            this.te_IndexName.TextChanged += new System.EventHandler(this.te_IndexName_TextChanged);
            this.te_IndexName.Validating += new System.ComponentModel.CancelEventHandler(this.te_IndexName_Validating);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.sbtn_Next);
            this.groupControl1.Controls.Add(this.sbtn_Prev);
            this.groupControl1.Controls.Add(this.sbtn_Remove);
            this.groupControl1.Controls.Add(this.sbtn_Add);
            this.groupControl1.Controls.Add(this.lbc_FieldsSelected);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lbc_FieldsAvailable);
            this.groupControl1.Location = new System.Drawing.Point(0, 41);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(376, 262);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "有效字段";
            // 
            // sbtn_Next
            // 
            this.sbtn_Next.Image = global::DF3DEdit.Properties.Resources.next;
            this.sbtn_Next.Location = new System.Drawing.Point(345, 135);
            this.sbtn_Next.Name = "sbtn_Next";
            this.sbtn_Next.Size = new System.Drawing.Size(26, 23);
            this.sbtn_Next.TabIndex = 7;
            this.sbtn_Next.Click += new System.EventHandler(this.sbtn_Next_Click);
            // 
            // sbtn_Remove
            // 
            this.sbtn_Remove.Location = new System.Drawing.Point(160, 135);
            this.sbtn_Remove.Name = "sbtn_Remove";
            this.sbtn_Remove.Size = new System.Drawing.Size(26, 23);
            this.sbtn_Remove.TabIndex = 5;
            this.sbtn_Remove.Text = "<<";
            this.sbtn_Remove.Click += new System.EventHandler(this.sbtn_Remove_Click);
            // 
            // sbtn_Add
            // 
            this.sbtn_Add.Location = new System.Drawing.Point(160, 78);
            this.sbtn_Add.Name = "sbtn_Add";
            this.sbtn_Add.Size = new System.Drawing.Size(26, 23);
            this.sbtn_Add.TabIndex = 4;
            this.sbtn_Add.Text = ">>";
            this.sbtn_Add.Click += new System.EventHandler(this.sbtn_Add_Click);
            // 
            // lbc_FieldsSelected
            // 
            this.lbc_FieldsSelected.Appearance.BackColor2 = System.Drawing.Color.SkyBlue;
            this.lbc_FieldsSelected.Appearance.Options.UseBackColor = true;
            this.lbc_FieldsSelected.Location = new System.Drawing.Point(192, 46);
            this.lbc_FieldsSelected.Name = "lbc_FieldsSelected";
            this.lbc_FieldsSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbc_FieldsSelected.Size = new System.Drawing.Size(141, 160);
            this.lbc_FieldsSelected.TabIndex = 3;
            this.lbc_FieldsSelected.SelectedIndexChanged += new System.EventHandler(this.FieldsSelected_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(210, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "选择字段";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(26, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "有效字段";
            // 
            // lbc_FieldsAvailable
            // 
            this.lbc_FieldsAvailable.Appearance.BackColor2 = System.Drawing.Color.SkyBlue;
            this.lbc_FieldsAvailable.Appearance.Options.UseBackColor = true;
            this.lbc_FieldsAvailable.Location = new System.Drawing.Point(13, 46);
            this.lbc_FieldsAvailable.Name = "lbc_FieldsAvailable";
            this.lbc_FieldsAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbc_FieldsAvailable.Size = new System.Drawing.Size(141, 160);
            this.lbc_FieldsAvailable.TabIndex = 0;
            this.lbc_FieldsAvailable.SelectedIndexChanged += new System.EventHandler(this.lbc_FieldsAvailable_SelectedIndexChanged);
            // 
            // sbtn_OK
            // 
            this.sbtn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbtn_OK.Location = new System.Drawing.Point(73, 315);
            this.sbtn_OK.Name = "sbtn_OK";
            this.sbtn_OK.Size = new System.Drawing.Size(75, 23);
            this.sbtn_OK.TabIndex = 5;
            this.sbtn_OK.Text = "确定";
            this.sbtn_OK.Click += new System.EventHandler(this.stbn_OK_Click);
            // 
            // sbtn_Cancel
            // 
            this.sbtn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtn_Cancel.Location = new System.Drawing.Point(222, 315);
            this.sbtn_Cancel.Name = "sbtn_Cancel";
            this.sbtn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.sbtn_Cancel.TabIndex = 6;
            this.sbtn_Cancel.Text = "取消";
            this.sbtn_Cancel.Click += new System.EventHandler(this.sbtn_Cancel_Click);
            // 
            // sbtn_Prev
            // 
            this.sbtn_Prev.Image = global::DF3DEdit.Properties.Resources.prev;
            this.sbtn_Prev.Location = new System.Drawing.Point(345, 78);
            this.sbtn_Prev.Name = "sbtn_Prev";
            this.sbtn_Prev.Size = new System.Drawing.Size(26, 23);
            this.sbtn_Prev.TabIndex = 6;
            this.sbtn_Prev.Click += new System.EventHandler(this.sbtn_Prev_Click);
            // 
            // CreateDBIndexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 350);
            this.Controls.Add(this.sbtn_Cancel);
            this.Controls.Add(this.sbtn_OK);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.te_IndexName);
            this.Controls.Add(this.lc_IndexName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateDBIndexForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建索引";
            ((System.ComponentModel.ISupportInitialize)(this.te_IndexName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_FieldsSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_FieldsAvailable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public CreateDBIndexForm()
        {
            this.InitializeComponent();
        }
        public CreateDBIndexForm(IFieldInfoCollection fields, ListBoxControl lbc)
            : this()
        {
            for (int i = 0; i < fields.Count; i++)
            {
                IFieldInfo fieldInfo = fields.Get(i);
                if (fieldInfo.FieldType == gviFieldType.gviFieldFID)
                {
                    this.Fid = fieldInfo.Name;
                }
                if (fieldInfo.FieldType != gviFieldType.gviFieldGeometry)
                {
                    this.lbc_FieldsAvailable.Items.Add(fieldInfo.Name);
                }
            }
            this.lbcIndexName = lbc;
        }
        private void sbtn_Add_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.lbc_FieldsAvailable.SelectedItems.Count; i++)
            {
                string text = this.lbc_FieldsAvailable.SelectedItems[i].ToString();
                if (!this.lbc_FieldsSelected.Items.Contains(text))
                {
                    this.lbc_FieldsSelected.Items.Add(text);
                }
            }
        }
        private void sbtn_Remove_Click(object sender, System.EventArgs e)
        {
            int count = this.lbc_FieldsSelected.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                this.lbc_FieldsSelected.Items.Remove(this.lbc_FieldsSelected.SelectedItems[0]);
            }
        }
        private void sbtn_Prev_Click(object sender, System.EventArgs e)
        {
            if (this.lbc_FieldsSelected.SelectedItems.Count > 0)
            {
                int num = this.lbc_FieldsSelected.SelectedIndices[0];
                if (num == 0)
                {
                    return;
                }
                object itemValue = this.lbc_FieldsSelected.GetItemValue(num);
                this.lbc_FieldsSelected.SetItemValue(this.lbc_FieldsSelected.GetItemValue(num - 1), num);
                this.lbc_FieldsSelected.SetItemValue(itemValue, num - 1);
                this.lbc_FieldsSelected.SelectedIndex = num - 1;
            }
        }
        private void sbtn_Next_Click(object sender, System.EventArgs e)
        {
            if (this.lbc_FieldsSelected.SelectedIndices.Count > 0)
            {
                int num = this.lbc_FieldsSelected.SelectedIndices[0];
                if (num == this.lbc_FieldsSelected.Items.Count - 1)
                {
                    return;
                }
                object itemValue = this.lbc_FieldsSelected.GetItemValue(num);
                this.lbc_FieldsSelected.SetItemValue(this.lbc_FieldsSelected.GetItemValue(num + 1), num);
                this.lbc_FieldsSelected.SetItemValue(itemValue, num + 1);
                this.lbc_FieldsSelected.SelectedIndex = num + 1;
            }
        }
        private void te_IndexName_TextChanged(object sender, System.EventArgs e)
        {
            this.indexName = this.te_IndexName.Text;
        }
        private void te_IndexName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.lbcIndexName.Items.Contains(this.te_IndexName.Text))
            {
                this.te_IndexName.ToolTip = "索引重复";
            }
            if (this.te_IndexName.Text.Length > 32)
            {
                this.te_IndexName.SelectAll();
                this.te_IndexName.ErrorText = "索引名称不能超过32个字符";
                e.Cancel = true;
            }
        }
        private void stbn_OK_Click(object sender, System.EventArgs e)
        {
            if (this.indexName.Equals("") || this.lbcIndexName.Items.Contains(this.indexName))
            {
                XtraMessageBox.Show("无效的索引名称或名称重复");
                base.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if (this.indexName.Equals("") || (this.lbc_FieldsSelected.Items.Count == 1 && this.Fid.Equals(this.lbc_FieldsSelected.Text)))
            {
                XtraMessageBox.Show("无法创建只有FID列的属性索引");
                base.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            for (int i = 0; i < this.lbc_FieldsSelected.Items.Count; i++)
            {
                this.selectFields.Add(this.lbc_FieldsSelected.Items[i].ToString());
            }
        }
        private void sbtn_Cancel_Click(object sender, System.EventArgs e)
        {
            base.Close();
        }
        private void FieldsSelected_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void lbc_FieldsAvailable_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
    }
}
