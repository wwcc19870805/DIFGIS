using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;

namespace DF3DPipeCreateTool.Frm
{
    public class FormChangeTerrain : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private TextEdit te_Pwd;
        private SimpleButton sbtn_Browser;
        private TextEdit te_Path;
        private DevExpress.XtraLayout.LayoutControlItem lb_Path;
        private DevExpress.XtraLayout.LayoutControlItem lb_Browser;
        private DevExpress.XtraLayout.LayoutControlItem lb_Pwd;
        private TextEdit te_DataSet;
        private TextEdit te_DataSource;
        private TextEdit te_ServerAddr;
        private DevExpress.XtraLayout.LayoutControlItem lb_Server;
        private DevExpress.XtraLayout.LayoutControlItem lb_DataSource;
        private DevExpress.XtraLayout.LayoutControlItem lb_DataSet;
        private SimpleButton sbtn_Cancel;
        private SimpleButton sbtn_OK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private SpinEdit te_Port;
        private DevExpress.XtraLayout.LayoutControlItem lb_Port;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    
        public FormChangeTerrain()
        {
            InitializeComponent();
            this.radioGroup1.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.te_Port = new DevExpress.XtraEditors.SpinEdit();
            this.sbtn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.te_DataSet = new DevExpress.XtraEditors.TextEdit();
            this.te_DataSource = new DevExpress.XtraEditors.TextEdit();
            this.te_ServerAddr = new DevExpress.XtraEditors.TextEdit();
            this.te_Pwd = new DevExpress.XtraEditors.TextEdit();
            this.sbtn_Browser = new DevExpress.XtraEditors.SimpleButton();
            this.te_Path = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Path = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Browser = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Pwd = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Server = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_DataSource = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_DataSet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Port = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_Port.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_DataSet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_DataSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ServerAddr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Pwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Path.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Path)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Browser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Pwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Server)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_DataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Port)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.te_Port);
            this.layoutControl1.Controls.Add(this.sbtn_Cancel);
            this.layoutControl1.Controls.Add(this.sbtn_OK);
            this.layoutControl1.Controls.Add(this.te_DataSet);
            this.layoutControl1.Controls.Add(this.te_DataSource);
            this.layoutControl1.Controls.Add(this.te_ServerAddr);
            this.layoutControl1.Controls.Add(this.te_Pwd);
            this.layoutControl1.Controls.Add(this.sbtn_Browser);
            this.layoutControl1.Controls.Add(this.te_Path);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(268, 235);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // te_Port
            // 
            this.te_Port.EditValue = new decimal(new int[] {
            8040,
            0,
            0,
            0});
            this.te_Port.Location = new System.Drawing.Point(87, 71);
            this.te_Port.Name = "te_Port";
            this.te_Port.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.te_Port.Properties.IsFloatValue = false;
            this.te_Port.Properties.Mask.EditMask = "N00";
            this.te_Port.Properties.MaxValue = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.te_Port.Size = new System.Drawing.Size(169, 22);
            this.te_Port.StyleController = this.layoutControl1;
            this.te_Port.TabIndex = 14;
            // 
            // sbtn_Cancel
            // 
            this.sbtn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtn_Cancel.Location = new System.Drawing.Point(136, 201);
            this.sbtn_Cancel.Name = "sbtn_Cancel";
            this.sbtn_Cancel.Size = new System.Drawing.Size(120, 22);
            this.sbtn_Cancel.StyleController = this.layoutControl1;
            this.sbtn_Cancel.TabIndex = 13;
            this.sbtn_Cancel.Text = "取消";
            this.sbtn_Cancel.Click += new System.EventHandler(this.sbtn_Cancel_Click);
            // 
            // sbtn_OK
            // 
            this.sbtn_OK.Location = new System.Drawing.Point(12, 201);
            this.sbtn_OK.Name = "sbtn_OK";
            this.sbtn_OK.Size = new System.Drawing.Size(120, 22);
            this.sbtn_OK.StyleController = this.layoutControl1;
            this.sbtn_OK.TabIndex = 12;
            this.sbtn_OK.Text = "确定";
            this.sbtn_OK.Click += new System.EventHandler(this.sbtn_OK_Click);
            // 
            // te_DataSet
            // 
            this.te_DataSet.Location = new System.Drawing.Point(87, 123);
            this.te_DataSet.Name = "te_DataSet";
            this.te_DataSet.Size = new System.Drawing.Size(169, 22);
            this.te_DataSet.StyleController = this.layoutControl1;
            this.te_DataSet.TabIndex = 11;
            // 
            // te_DataSource
            // 
            this.te_DataSource.Location = new System.Drawing.Point(87, 97);
            this.te_DataSource.Name = "te_DataSource";
            this.te_DataSource.Size = new System.Drawing.Size(169, 22);
            this.te_DataSource.StyleController = this.layoutControl1;
            this.te_DataSource.TabIndex = 10;
            // 
            // te_ServerAddr
            // 
            this.te_ServerAddr.Location = new System.Drawing.Point(87, 45);
            this.te_ServerAddr.Name = "te_ServerAddr";
            this.te_ServerAddr.Size = new System.Drawing.Size(169, 22);
            this.te_ServerAddr.StyleController = this.layoutControl1;
            this.te_ServerAddr.TabIndex = 8;
            // 
            // te_Pwd
            // 
            this.te_Pwd.Location = new System.Drawing.Point(87, 175);
            this.te_Pwd.Name = "te_Pwd";
            this.te_Pwd.Properties.PasswordChar = '*';
            this.te_Pwd.Size = new System.Drawing.Size(169, 22);
            this.te_Pwd.StyleController = this.layoutControl1;
            this.te_Pwd.TabIndex = 7;
            // 
            // sbtn_Browser
            // 
            this.sbtn_Browser.Location = new System.Drawing.Point(233, 149);
            this.sbtn_Browser.Name = "sbtn_Browser";
            this.sbtn_Browser.Size = new System.Drawing.Size(23, 22);
            this.sbtn_Browser.StyleController = this.layoutControl1;
            this.sbtn_Browser.TabIndex = 6;
            this.sbtn_Browser.Text = "...";
            this.sbtn_Browser.Click += new System.EventHandler(this.sbtn_Browser_Click);
            // 
            // te_Path
            // 
            this.te_Path.Location = new System.Drawing.Point(87, 149);
            this.te_Path.Name = "te_Path";
            this.te_Path.Size = new System.Drawing.Size(142, 22);
            this.te_Path.StyleController = this.layoutControl1;
            this.te_Path.TabIndex = 5;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(12, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "地形文件"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "网络服务")});
            this.radioGroup1.Size = new System.Drawing.Size(244, 29);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 4;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lb_Path,
            this.lb_Browser,
            this.lb_Pwd,
            this.lb_Server,
            this.lb_DataSource,
            this.lb_DataSet,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.lb_Port});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(268, 235);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(248, 33);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lb_Path
            // 
            this.lb_Path.Control = this.te_Path;
            this.lb_Path.CustomizationFormText = "路径:";
            this.lb_Path.Location = new System.Drawing.Point(0, 137);
            this.lb_Path.Name = "lb_Path";
            this.lb_Path.Size = new System.Drawing.Size(221, 26);
            this.lb_Path.Text = "路径:";
            this.lb_Path.TextSize = new System.Drawing.Size(72, 14);
            // 
            // lb_Browser
            // 
            this.lb_Browser.Control = this.sbtn_Browser;
            this.lb_Browser.CustomizationFormText = "lb_Browser";
            this.lb_Browser.Location = new System.Drawing.Point(221, 137);
            this.lb_Browser.Name = "lb_Browser";
            this.lb_Browser.Size = new System.Drawing.Size(27, 26);
            this.lb_Browser.Text = "lb_Browser";
            this.lb_Browser.TextSize = new System.Drawing.Size(0, 0);
            this.lb_Browser.TextToControlDistance = 0;
            this.lb_Browser.TextVisible = false;
            // 
            // lb_Pwd
            // 
            this.lb_Pwd.Control = this.te_Pwd;
            this.lb_Pwd.CustomizationFormText = "密码：";
            this.lb_Pwd.Location = new System.Drawing.Point(0, 163);
            this.lb_Pwd.Name = "lb_Pwd";
            this.lb_Pwd.Size = new System.Drawing.Size(248, 26);
            this.lb_Pwd.Text = "密码：";
            this.lb_Pwd.TextSize = new System.Drawing.Size(72, 14);
            // 
            // lb_Server
            // 
            this.lb_Server.Control = this.te_ServerAddr;
            this.lb_Server.CustomizationFormText = "服务器地址：";
            this.lb_Server.Location = new System.Drawing.Point(0, 33);
            this.lb_Server.Name = "lb_Server";
            this.lb_Server.Size = new System.Drawing.Size(248, 26);
            this.lb_Server.Text = "服务器地址：";
            this.lb_Server.TextSize = new System.Drawing.Size(72, 14);
            // 
            // lb_DataSource
            // 
            this.lb_DataSource.Control = this.te_DataSource;
            this.lb_DataSource.CustomizationFormText = "数据源：";
            this.lb_DataSource.Location = new System.Drawing.Point(0, 85);
            this.lb_DataSource.Name = "lb_DataSource";
            this.lb_DataSource.Size = new System.Drawing.Size(248, 26);
            this.lb_DataSource.Text = "数据源：";
            this.lb_DataSource.TextSize = new System.Drawing.Size(72, 14);
            // 
            // lb_DataSet
            // 
            this.lb_DataSet.Control = this.te_DataSet;
            this.lb_DataSet.CustomizationFormText = "数据集：";
            this.lb_DataSet.Location = new System.Drawing.Point(0, 111);
            this.lb_DataSet.Name = "lb_DataSet";
            this.lb_DataSet.Size = new System.Drawing.Size(248, 26);
            this.lb_DataSet.Text = "数据集：";
            this.lb_DataSet.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.sbtn_OK;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 189);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(124, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.sbtn_Cancel;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(124, 189);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(124, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // lb_Port
            // 
            this.lb_Port.Control = this.te_Port;
            this.lb_Port.CustomizationFormText = "端口号：";
            this.lb_Port.Location = new System.Drawing.Point(0, 59);
            this.lb_Port.Name = "lb_Port";
            this.lb_Port.Size = new System.Drawing.Size(248, 26);
            this.lb_Port.Text = "端口号：";
            this.lb_Port.TextSize = new System.Drawing.Size(72, 14);
            // 
            // FormChangeTerrain
            // 
            this.AcceptButton = this.sbtn_OK;
            this.CancelButton = this.sbtn_Cancel;
            this.ClientSize = new System.Drawing.Size(268, 235);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormChangeTerrain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加地形";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_Port.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_DataSet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_DataSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ServerAddr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Pwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Path.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Path)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Browser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Pwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Server)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_DataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Port)).EndInit();
            this.ResumeLayout(false);

        }
        private string _connInfo;
        public string ConnInfo
        {
            get { return this._connInfo; }
        }
        public string Pwd
        {
            get { return this.te_Pwd.Text; }
        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    this.lb_Browser.Visibility = LayoutVisibility.Always;
                    this.lb_Path.Visibility = LayoutVisibility.Always;
                    this.lb_Server.Visibility = LayoutVisibility.Never;
                    this.lb_Port.Visibility = LayoutVisibility.Never;
                    this.lb_DataSource.Visibility = LayoutVisibility.Never;
                    this.lb_DataSet.Visibility = LayoutVisibility.Never;
                    break;
                case 1:
                    this.lb_Browser.Visibility = LayoutVisibility.Never;
                    this.lb_Path.Visibility = LayoutVisibility.Never;
                    this.lb_Server.Visibility = LayoutVisibility.Always;
                    this.lb_Port.Visibility = LayoutVisibility.Always;
                    this.lb_DataSource.Visibility = LayoutVisibility.Always;
                    this.lb_DataSet.Visibility = LayoutVisibility.Always;
                    break;
            }
        }

        private void sbtn_Browser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Filter = "Terrain File(*.ted)|*.ted";
            dlg.DefaultExt = ".ted";
            if (dlg.ShowDialog() == DialogResult.OK)
                this.te_Path.Text = dlg.FileName;
        }

        private void sbtn_OK_Click(object sender, EventArgs e)
        {
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    if (!System.IO.File.Exists(this.te_Path.Text))
                    {
                        XtraMessageBox.Show("请检查文件路径是否正确！", "提示");
                        return;
                    }
                    this._connInfo = this.te_Path.Text;
                    break;
                case 1:
                    if (string.IsNullOrEmpty(this.te_ServerAddr.Text))
                    {
                        XtraMessageBox.Show("服务器地址不能为空！", "提示");
                        return;
                    }
                    if (string.IsNullOrEmpty(this.te_DataSource.Text))
                    {
                        XtraMessageBox.Show("数据源不能为空！", "提示");
                        return;
                    }
                    if (string.IsNullOrEmpty(this.te_DataSet.Text))
                    {
                        XtraMessageBox.Show("数据集不能为空！", "提示");
                        return;
                    }
                    this._connInfo = this.te_DataSource.Text + ":" + this.te_DataSet.Text + "@" + this.te_ServerAddr.Text + ":" + this.te_Port.Text;
                    break;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void sbtn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
