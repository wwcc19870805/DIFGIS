using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DScan.Class;
using ICSharpCode.Core;
using DevExpress.XtraLayout;
using System.Collections;

namespace DF3DScan.Frm
{
    public class FormCreateLocation : XtraForm
    {
        private Hashtable _groupNameTable;
        private HashSet<string> locationNames;
        private System.ComponentModel.IContainer components;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private ComboBoxEdit cbGroups;
        private LayoutControlItem itemGroups;
        private TextEdit txtLocationName;
        private LayoutControlItem itemLocationName;
        private EmptySpaceItem emptySpaceItem1;
        private MemoEdit txtComment;
        private LayoutControlItem itemComment;
        private EmptySpaceItem emptySpaceItem2;
        private EmptySpaceItem emptySpaceItem3;
        private SpinEdit txtDuration;
        private LayoutControlItem itemDuration;
        private EmptySpaceItem emptySpaceItem4;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        public string GroupName
        {
            get
            {
                return this.cbGroups.Text;
            }
        }
        public string LocationName
        {
            get
            {
                return this.txtLocationName.Text;
            }
        }
        public string Comment
        {
            get
            {
                return this.txtComment.Text;
            }
        }
        public double Duration
        {
            get
            {
                return double.Parse(this.txtDuration.Value.ToString());
            }
        }
        public FormCreateLocation(Hashtable groupNameTable, string groupName, bool isInsert)
        {
            this.InitializeComponent();
            this._groupNameTable = groupNameTable;
            foreach (object current in groupNameTable.Keys)
            {
                this.cbGroups.Properties.Items.Add(current.ToString());
                if (current.ToString() == groupName)
                {
                    this.cbGroups.SelectedIndex = this.cbGroups.Properties.Items.Count - 1;
                }
            }
            this.locationNames = (HashSet<string>)groupNameTable[groupName];
            int num = this.locationNames.Count + 1;
            string text = "新建特定场景" + num.ToString();
            while (this.locationNames.Contains(text))
            {
                num++;
                text = "新建特定场景" + num.ToString();
            }
            this.txtLocationName.Text = text;
            this.cbGroups.Enabled = !isInsert;
        }
        private void cbGroups_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string text = this.cbGroups.Text;
            this.locationNames = (HashSet<string>)this._groupNameTable[text];
            int num = this.locationNames.Count + 1;
            string text2 = "新建特定场景" + num.ToString();
            while (this.locationNames.Contains(text2))
            {
                num++;
                text2 = "新建特定场景" + num.ToString();
            }
            this.txtLocationName.Text = text2;
        }
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (this.txtLocationName.Text.Trim() == "")
            {
                XtraMessageBox.Show("新建特定场景名称不能为空！", "提示");
                this.txtLocationName.Text = "";
                this.txtLocationName.Focus();
                base.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            if (this.locationNames.Contains(this.txtLocationName.Text))
            {
                XtraMessageBox.Show("新建特定场景名称已存在！", "提示");
                this.txtLocationName.Focus();
                base.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtLocationName = new DevExpress.XtraEditors.TextEdit();
            this.cbGroups = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtDuration = new DevExpress.XtraEditors.SpinEdit();
            this.txtComment = new DevExpress.XtraEditors.MemoEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.itemGroups = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemLocationName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.itemComment = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemDuration = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbGroups.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemLocationName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.txtLocationName);
            this.layoutControl1.Controls.Add(this.cbGroups);
            this.layoutControl1.Controls.Add(this.txtDuration);
            this.layoutControl1.Controls.Add(this.txtComment);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(288, 273);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 239);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(125, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtLocationName
            // 
            this.txtLocationName.Location = new System.Drawing.Point(99, 51);
            this.txtLocationName.Name = "txtLocationName";
            this.txtLocationName.Properties.MaxLength = 100;
            this.txtLocationName.Size = new System.Drawing.Size(177, 22);
            this.txtLocationName.StyleController = this.layoutControl1;
            this.txtLocationName.TabIndex = 1;
            this.txtLocationName.ToolTip = "特定场景名称字符数范围:1-100";
            // 
            // cbGroups
            // 
            this.cbGroups.Location = new System.Drawing.Point(99, 12);
            this.cbGroups.Name = "cbGroups";
            this.cbGroups.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbGroups.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbGroups.Size = new System.Drawing.Size(177, 22);
            this.cbGroups.StyleController = this.layoutControl1;
            this.cbGroups.TabIndex = 0;
            this.cbGroups.SelectedIndexChanged += new System.EventHandler(this.cbGroups_SelectedIndexChanged);
            // 
            // txtDuration
            // 
            this.txtDuration.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txtDuration.Location = new System.Drawing.Point(99, 196);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDuration.Properties.MaxValue = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.txtDuration.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtDuration.Size = new System.Drawing.Size(177, 22);
            this.txtDuration.StyleController = this.layoutControl1;
            this.txtDuration.TabIndex = 3;
            this.txtDuration.ToolTip = "播放间隔必须是一个在1和65535之间的整数!";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(99, 91);
            this.txtComment.Name = "txtComment";
            this.txtComment.Properties.MaxLength = 256;
            this.txtComment.Size = new System.Drawing.Size(177, 89);
            this.txtComment.StyleController = this.layoutControl1;
            this.txtComment.TabIndex = 2;
            this.txtComment.ToolTip = "说明字符个数应在0-256之间";
            this.txtComment.UseOptimizedRendering = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(141, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.itemGroups,
            this.itemLocationName,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.itemComment,
            this.emptySpaceItem3,
            this.emptySpaceItem4,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.itemDuration});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(288, 273);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // itemGroups
            // 
            this.itemGroups.Control = this.cbGroups;
            this.itemGroups.CustomizationFormText = "特定场景组";
            this.itemGroups.Location = new System.Drawing.Point(0, 0);
            this.itemGroups.Name = "itemGroups";
            this.itemGroups.Size = new System.Drawing.Size(268, 26);
            this.itemGroups.Text = "新建特定场景组";
            this.itemGroups.TextSize = new System.Drawing.Size(84, 14);
            // 
            // itemLocationName
            // 
            this.itemLocationName.Control = this.txtLocationName;
            this.itemLocationName.CustomizationFormText = "特定场景名称";
            this.itemLocationName.Location = new System.Drawing.Point(0, 39);
            this.itemLocationName.Name = "itemLocationName";
            this.itemLocationName.Size = new System.Drawing.Size(268, 26);
            this.itemLocationName.Text = "特定场景名称";
            this.itemLocationName.TextSize = new System.Drawing.Size(84, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(268, 13);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 65);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(268, 14);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // itemComment
            // 
            this.itemComment.Control = this.txtComment;
            this.itemComment.CustomizationFormText = "说明";
            this.itemComment.Location = new System.Drawing.Point(0, 79);
            this.itemComment.Name = "itemComment";
            this.itemComment.Size = new System.Drawing.Size(268, 93);
            this.itemComment.Text = "说明";
            this.itemComment.TextSize = new System.Drawing.Size(84, 14);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 172);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(268, 12);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 210);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(268, 17);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnOK;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 227);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(129, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(129, 227);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(139, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // itemDuration
            // 
            this.itemDuration.Control = this.txtDuration;
            this.itemDuration.CustomizationFormText = "itemDuration";
            this.itemDuration.Location = new System.Drawing.Point(0, 184);
            this.itemDuration.Name = "itemDuration";
            this.itemDuration.Size = new System.Drawing.Size(268, 26);
            this.itemDuration.Text = "播放间隔(秒)";
            this.itemDuration.TextSize = new System.Drawing.Size(84, 14);
            // 
            // FormCreateLocation
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(288, 273);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(290, 288);
            this.Name = "FormCreateLocation";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建特定场景";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLocationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbGroups.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemLocationName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemDuration)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

    }
}
