using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using DevExpress.XtraLayout.Utils;
using DF3DData.Class;

namespace DF3DData.Frm
{
    public class FormAdd3DDatabase : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private ComboBoxEdit cbe_DSName;
        private SimpleButton sbtn_Connection;
        private TextEdit te_Pwd;
        private TextEdit te_Username;
        private TextEdit te_Instance;
        private ComboBoxEdit cbe_Type;
        private DevExpress.XtraLayout.LayoutControlItem lb_type;
        private DevExpress.XtraLayout.LayoutControlItem lb_Instance;
        private DevExpress.XtraLayout.LayoutControlItem lb_Port;
        private DevExpress.XtraLayout.LayoutControlItem lb_User;
        private DevExpress.XtraLayout.LayoutControlItem lb_Pwd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private SimpleButton sbtn_Quit;
        private SimpleButton sbtn_OK;
        private CheckEdit cke_SaveParameter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private TextEdit te_ServerOrPath;
        private DevExpress.XtraLayout.LayoutControlItem lb_Server;
        private SimpleButton sbtn_Browser;
        private DevExpress.XtraLayout.LayoutControlItem lb_Browser;
        private SpinEdit te_PortNo;
        private TextEdit te_DSName;
        private DevExpress.XtraLayout.LayoutControlItem lb_Dsrc1;
        private DevExpress.XtraLayout.LayoutControlItem lb_Dsrc;
    
        public FormAdd3DDatabase()
        {
            InitializeComponent();
            this.cbe_Type.SelectedIndex = 0;
            this.connInfo = new ConnectionInfo();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.te_DSName = new DevExpress.XtraEditors.TextEdit();
            this.sbtn_Browser = new DevExpress.XtraEditors.SimpleButton();
            this.te_ServerOrPath = new DevExpress.XtraEditors.TextEdit();
            this.sbtn_Quit = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.cke_SaveParameter = new DevExpress.XtraEditors.CheckEdit();
            this.cbe_DSName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.sbtn_Connection = new DevExpress.XtraEditors.SimpleButton();
            this.te_Pwd = new DevExpress.XtraEditors.TextEdit();
            this.te_Username = new DevExpress.XtraEditors.TextEdit();
            this.te_Instance = new DevExpress.XtraEditors.TextEdit();
            this.cbe_Type = new DevExpress.XtraEditors.ComboBoxEdit();
            this.te_PortNo = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lb_type = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Port = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_User = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Pwd = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Dsrc = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Server = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Dsrc1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Instance = new DevExpress.XtraLayout.LayoutControlItem();
            this.lb_Browser = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_DSName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ServerOrPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cke_SaveParameter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_DSName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Pwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Username.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Instance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_Type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_PortNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_type)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_User)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Pwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Dsrc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Server)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Dsrc1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Instance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Browser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.te_DSName);
            this.layoutControl1.Controls.Add(this.sbtn_Browser);
            this.layoutControl1.Controls.Add(this.te_ServerOrPath);
            this.layoutControl1.Controls.Add(this.sbtn_Quit);
            this.layoutControl1.Controls.Add(this.sbtn_OK);
            this.layoutControl1.Controls.Add(this.cke_SaveParameter);
            this.layoutControl1.Controls.Add(this.cbe_DSName);
            this.layoutControl1.Controls.Add(this.sbtn_Connection);
            this.layoutControl1.Controls.Add(this.te_Pwd);
            this.layoutControl1.Controls.Add(this.te_Username);
            this.layoutControl1.Controls.Add(this.te_Instance);
            this.layoutControl1.Controls.Add(this.cbe_Type);
            this.layoutControl1.Controls.Add(this.te_PortNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(305, 231);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // te_DSName
            // 
            this.te_DSName.Location = new System.Drawing.Point(66, 142);
            this.te_DSName.Name = "te_DSName";
            this.te_DSName.Size = new System.Drawing.Size(183, 22);
            this.te_DSName.StyleController = this.layoutControl1;
            this.te_DSName.TabIndex = 6;
            // 
            // sbtn_Browser
            // 
            this.sbtn_Browser.Location = new System.Drawing.Point(253, 142);
            this.sbtn_Browser.Name = "sbtn_Browser";
            this.sbtn_Browser.Size = new System.Drawing.Size(23, 22);
            this.sbtn_Browser.StyleController = this.layoutControl1;
            this.sbtn_Browser.TabIndex = 2;
            this.sbtn_Browser.Text = "...";
            this.sbtn_Browser.Click += new System.EventHandler(this.sbtn_Browser_Click);
            // 
            // te_ServerOrPath
            // 
            this.te_ServerOrPath.Location = new System.Drawing.Point(66, 38);
            this.te_ServerOrPath.Name = "te_ServerOrPath";
            this.te_ServerOrPath.Size = new System.Drawing.Size(210, 22);
            this.te_ServerOrPath.StyleController = this.layoutControl1;
            this.te_ServerOrPath.TabIndex = 1;
            // 
            // sbtn_Quit
            // 
            this.sbtn_Quit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtn_Quit.Location = new System.Drawing.Point(144, 246);
            this.sbtn_Quit.Name = "sbtn_Quit";
            this.sbtn_Quit.Size = new System.Drawing.Size(132, 22);
            this.sbtn_Quit.StyleController = this.layoutControl1;
            this.sbtn_Quit.TabIndex = 12;
            this.sbtn_Quit.Text = "取消";
            this.sbtn_Quit.Click += new System.EventHandler(this.sbtn_Quit_Click);
            // 
            // sbtn_OK
            // 
            this.sbtn_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtn_OK.Location = new System.Drawing.Point(12, 246);
            this.sbtn_OK.Name = "sbtn_OK";
            this.sbtn_OK.Size = new System.Drawing.Size(128, 22);
            this.sbtn_OK.StyleController = this.layoutControl1;
            this.sbtn_OK.TabIndex = 11;
            this.sbtn_OK.Text = "确定";
            this.sbtn_OK.Click += new System.EventHandler(this.sbtn_OK_Click);
            // 
            // cke_SaveParameter
            // 
            this.cke_SaveParameter.Location = new System.Drawing.Point(144, 220);
            this.cke_SaveParameter.Name = "cke_SaveParameter";
            this.cke_SaveParameter.Properties.Caption = "保存密码";
            this.cke_SaveParameter.Size = new System.Drawing.Size(132, 19);
            this.cke_SaveParameter.StyleController = this.layoutControl1;
            this.cke_SaveParameter.TabIndex = 9;
            this.cke_SaveParameter.CheckedChanged += new System.EventHandler(this.cke_SaveParameter_CheckedChanged);
            // 
            // cbe_DSName
            // 
            this.cbe_DSName.Location = new System.Drawing.Point(66, 194);
            this.cbe_DSName.Name = "cbe_DSName";
            this.cbe_DSName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbe_DSName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbe_DSName.Size = new System.Drawing.Size(210, 22);
            this.cbe_DSName.StyleController = this.layoutControl1;
            this.cbe_DSName.TabIndex = 8;
            this.cbe_DSName.TextChanged += new System.EventHandler(this.cbe_DSName_TextChanged);
            // 
            // sbtn_Connection
            // 
            this.sbtn_Connection.Location = new System.Drawing.Point(12, 220);
            this.sbtn_Connection.Name = "sbtn_Connection";
            this.sbtn_Connection.Size = new System.Drawing.Size(128, 22);
            this.sbtn_Connection.StyleController = this.layoutControl1;
            this.sbtn_Connection.TabIndex = 10;
            this.sbtn_Connection.Text = "连接";
            this.sbtn_Connection.Click += new System.EventHandler(this.sbtn_Connection_Click);
            // 
            // te_Pwd
            // 
            this.te_Pwd.Location = new System.Drawing.Point(66, 168);
            this.te_Pwd.Name = "te_Pwd";
            this.te_Pwd.Properties.PasswordChar = '*';
            this.te_Pwd.Size = new System.Drawing.Size(210, 22);
            this.te_Pwd.StyleController = this.layoutControl1;
            this.te_Pwd.TabIndex = 7;
            // 
            // te_Username
            // 
            this.te_Username.Location = new System.Drawing.Point(66, 116);
            this.te_Username.Name = "te_Username";
            this.te_Username.Size = new System.Drawing.Size(210, 22);
            this.te_Username.StyleController = this.layoutControl1;
            this.te_Username.TabIndex = 5;
            // 
            // te_Instance
            // 
            this.te_Instance.Location = new System.Drawing.Point(66, 64);
            this.te_Instance.Name = "te_Instance";
            this.te_Instance.Size = new System.Drawing.Size(210, 22);
            this.te_Instance.StyleController = this.layoutControl1;
            this.te_Instance.TabIndex = 3;
            // 
            // cbe_Type
            // 
            this.cbe_Type.Location = new System.Drawing.Point(66, 12);
            this.cbe_Type.Name = "cbe_Type";
            this.cbe_Type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbe_Type.Properties.Items.AddRange(new object[] {
            "File",
            "MySql",
            "Oracle",
            "Microsoft SQLServer",
            "Citymaker Service"});
            this.cbe_Type.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbe_Type.Size = new System.Drawing.Size(210, 22);
            this.cbe_Type.StyleController = this.layoutControl1;
            this.cbe_Type.TabIndex = 0;
            this.cbe_Type.SelectedIndexChanged += new System.EventHandler(this.cbe_Type_SelectedIndexChanged);
            // 
            // te_PortNo
            // 
            this.te_PortNo.EditValue = new decimal(new int[] {
            8040,
            0,
            0,
            0});
            this.te_PortNo.Location = new System.Drawing.Point(66, 90);
            this.te_PortNo.Name = "te_PortNo";
            this.te_PortNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.te_PortNo.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.te_PortNo.Properties.IsFloatValue = false;
            this.te_PortNo.Properties.Mask.EditMask = "N00";
            this.te_PortNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.te_PortNo.Properties.MaxValue = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.te_PortNo.Size = new System.Drawing.Size(210, 22);
            this.te_PortNo.StyleController = this.layoutControl1;
            this.te_PortNo.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lb_type,
            this.lb_Port,
            this.lb_User,
            this.lb_Pwd,
            this.lb_Dsrc,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.lb_Server,
            this.lb_Dsrc1,
            this.lb_Instance,
            this.lb_Browser,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(288, 280);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lb_type
            // 
            this.lb_type.Control = this.cbe_Type;
            this.lb_type.CustomizationFormText = "类型：";
            this.lb_type.Location = new System.Drawing.Point(0, 0);
            this.lb_type.Name = "lb_type";
            this.lb_type.Size = new System.Drawing.Size(268, 26);
            this.lb_type.Text = "类型：";
            this.lb_type.TextSize = new System.Drawing.Size(51, 14);
            // 
            // lb_Port
            // 
            this.lb_Port.Control = this.te_PortNo;
            this.lb_Port.CustomizationFormText = "端口：";
            this.lb_Port.Location = new System.Drawing.Point(0, 78);
            this.lb_Port.Name = "lb_Port";
            this.lb_Port.Size = new System.Drawing.Size(268, 26);
            this.lb_Port.Text = "端口：";
            this.lb_Port.TextSize = new System.Drawing.Size(51, 14);
            // 
            // lb_User
            // 
            this.lb_User.Control = this.te_Username;
            this.lb_User.CustomizationFormText = "用户名：";
            this.lb_User.Location = new System.Drawing.Point(0, 104);
            this.lb_User.Name = "lb_User";
            this.lb_User.Size = new System.Drawing.Size(268, 26);
            this.lb_User.Text = "用户名：";
            this.lb_User.TextSize = new System.Drawing.Size(51, 14);
            // 
            // lb_Pwd
            // 
            this.lb_Pwd.Control = this.te_Pwd;
            this.lb_Pwd.CustomizationFormText = "密码：";
            this.lb_Pwd.Location = new System.Drawing.Point(0, 156);
            this.lb_Pwd.Name = "lb_Pwd";
            this.lb_Pwd.Size = new System.Drawing.Size(268, 26);
            this.lb_Pwd.Text = "密码：";
            this.lb_Pwd.TextSize = new System.Drawing.Size(51, 14);
            // 
            // lb_Dsrc
            // 
            this.lb_Dsrc.Control = this.cbe_DSName;
            this.lb_Dsrc.CustomizationFormText = "数据源：";
            this.lb_Dsrc.Location = new System.Drawing.Point(0, 182);
            this.lb_Dsrc.Name = "lb_Dsrc";
            this.lb_Dsrc.Size = new System.Drawing.Size(268, 26);
            this.lb_Dsrc.Text = "数据源：";
            this.lb_Dsrc.TextSize = new System.Drawing.Size(51, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cke_SaveParameter;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(132, 208);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(136, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            this.layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.sbtn_OK;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 234);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(132, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.sbtn_Quit;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(132, 234);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(136, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // lb_Server
            // 
            this.lb_Server.Control = this.te_ServerOrPath;
            this.lb_Server.CustomizationFormText = "服务器IP:";
            this.lb_Server.Location = new System.Drawing.Point(0, 26);
            this.lb_Server.Name = "lb_Server";
            this.lb_Server.Size = new System.Drawing.Size(268, 26);
            this.lb_Server.Text = "服务器IP:";
            this.lb_Server.TextSize = new System.Drawing.Size(51, 14);
            // 
            // lb_Dsrc1
            // 
            this.lb_Dsrc1.Control = this.te_DSName;
            this.lb_Dsrc1.CustomizationFormText = "路径：";
            this.lb_Dsrc1.Location = new System.Drawing.Point(0, 130);
            this.lb_Dsrc1.Name = "lb_Dsrc1";
            this.lb_Dsrc1.Size = new System.Drawing.Size(241, 26);
            this.lb_Dsrc1.Text = "路径：";
            this.lb_Dsrc1.TextSize = new System.Drawing.Size(51, 14);
            // 
            // lb_Instance
            // 
            this.lb_Instance.Control = this.te_Instance;
            this.lb_Instance.CustomizationFormText = "实例：";
            this.lb_Instance.Location = new System.Drawing.Point(0, 52);
            this.lb_Instance.Name = "lb_Instance";
            this.lb_Instance.Size = new System.Drawing.Size(268, 26);
            this.lb_Instance.Text = "实例：";
            this.lb_Instance.TextSize = new System.Drawing.Size(51, 14);
            // 
            // lb_Browser
            // 
            this.lb_Browser.Control = this.sbtn_Browser;
            this.lb_Browser.CustomizationFormText = "layoutControlItem12";
            this.lb_Browser.Location = new System.Drawing.Point(241, 130);
            this.lb_Browser.Name = "lb_Browser";
            this.lb_Browser.Size = new System.Drawing.Size(27, 26);
            this.lb_Browser.Text = "lb_Browser";
            this.lb_Browser.TextSize = new System.Drawing.Size(0, 0);
            this.lb_Browser.TextToControlDistance = 0;
            this.lb_Browser.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sbtn_Connection;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 208);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(132, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            this.layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // FormAddDB
            // 
            this.AcceptButton = this.sbtn_OK;
            this.CancelButton = this.sbtn_Quit;
            this.ClientSize = new System.Drawing.Size(305, 231);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAddDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "加载数据库";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_DSName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ServerOrPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cke_SaveParameter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_DSName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Pwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Username.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Instance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_Type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_PortNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_type)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_User)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Pwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Dsrc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Server)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Dsrc1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Instance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_Browser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        private IConnectionInfo connInfo;
        public IConnectionInfo ConnInfo
        {
            get { return connInfo; }
        }

        private void ClearControls()
        {
            this.te_Instance.Text = "";
            this.te_ServerOrPath.Text = "";
            this.te_PortNo.Text = ""; 
            this.te_Username.Text = "";
            this.te_Pwd.Text = "";
            this.te_DSName.Text = "";
            this.cbe_DSName.Text = "";
            this.cbe_DSName.Properties.Items.Clear();
        }

        private void sbtn_Connection_Click(object sender, EventArgs e)
        {

        }

        private void cbe_DSName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cke_SaveParameter_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void sbtn_OK_Click(object sender, EventArgs e)
        {
            switch(this.cbe_Type.Text)
            {
                case "File":
                    this.connInfo.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                    this.connInfo.Database = this.te_DSName.Text;
                    this.connInfo.Password = this.te_Pwd.Text;
                    break;
                case "MySql":
                    this.connInfo.ConnectionType = gviConnectionType.gviConnectionMySql5x;
                    this.connInfo.Server = this.te_ServerOrPath.Text;
                    this.connInfo.Port = (uint)this.te_PortNo.Value;
                    this.connInfo.UserName = this.te_Username.Text;
                    this.connInfo.Password = this.te_Pwd.Text;
                    this.connInfo.Database = this.cbe_DSName.Text;
                    break;
                case "Oracle":
                    this.connInfo.ConnectionType = gviConnectionType.gviConnectionOCI11;
                    this.connInfo.Instance = this.te_Instance.Text;
                    this.connInfo.Server = this.te_ServerOrPath.Text;
                    this.connInfo.Port = (uint)this.te_PortNo.Value;
                    this.connInfo.UserName = this.te_Username.Text;
                    this.connInfo.Password = this.te_Pwd.Text;
                    this.connInfo.Database = this.cbe_DSName.Text;
                    break;
                case "Microsoft SQLServer":
                    this.connInfo.ConnectionType = gviConnectionType.gviConnectionMSClient;
                    this.connInfo.Instance = this.te_Instance.Text;
                    this.connInfo.Server = this.te_ServerOrPath.Text;
                    this.connInfo.Port = (uint)this.te_PortNo.Value;
                    this.connInfo.UserName = this.te_Username.Text;
                    this.connInfo.Password = this.te_Pwd.Text;
                    this.connInfo.Database = this.cbe_DSName.Text;
                    break;
                case "Citymaker Service":
                    this.connInfo.ConnectionType = gviConnectionType.gviConnectionCms7Http;
                    this.connInfo.Server = this.te_ServerOrPath.Text;
                    this.connInfo.Port = (uint)this.te_PortNo.Value;
                    this.connInfo.UserName = this.te_Username.Text;
                    this.connInfo.Password = this.te_Pwd.Text;
                    this.connInfo.Database = this.te_DSName.Text;
                    break;
            }
            if (!DataUtils.CanConnection(this.connInfo))
            {
                XtraMessageBox.Show("打开数据失败，请检查网络或连接参数！", "提示");
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void sbtn_Quit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cbe_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ClearControls();
            if (this.cbe_Type.Text == "File")
            {
                this.lb_Dsrc1.Text = "路径：";
                this.lb_Instance.Visibility = LayoutVisibility.Never;
                this.lb_Server.Visibility = LayoutVisibility.Never;
                this.lb_Port.Visibility = LayoutVisibility.Never;
                this.lb_User.Visibility = LayoutVisibility.Never;
                this.lb_Pwd.Visibility = LayoutVisibility.Always;
                this.lb_Dsrc.Visibility = LayoutVisibility.Never;
                this.lb_Dsrc1.Visibility = LayoutVisibility.Always;
                this.lb_Browser.Visibility = LayoutVisibility.Always;
                return;
            }
            if (this.cbe_Type.Text == "MySql")
            {
                this.lb_Instance.Visibility = LayoutVisibility.Never;
                this.lb_Server.Visibility = LayoutVisibility.Always;
                this.lb_Port.Visibility = LayoutVisibility.Always;
                this.lb_User.Visibility = LayoutVisibility.Always;
                this.lb_Pwd.Visibility = LayoutVisibility.Always;
                this.lb_Dsrc.Visibility = LayoutVisibility.Always;
                this.lb_Dsrc1.Visibility = LayoutVisibility.Never;
                this.lb_Browser.Visibility = LayoutVisibility.Never;
                return;
            }
            if (this.cbe_Type.Text == "Oracle" || this.cbe_Type.Text == "Microsoft SQLServer")
            {
                this.lb_Instance.Visibility = LayoutVisibility.Always;
                this.lb_Server.Visibility = LayoutVisibility.Always;
                this.lb_Port.Visibility = LayoutVisibility.Always;
                this.lb_User.Visibility = LayoutVisibility.Always;
                this.lb_Pwd.Visibility = LayoutVisibility.Always;
                this.lb_Dsrc.Visibility = LayoutVisibility.Always;
                this.lb_Dsrc1.Visibility = LayoutVisibility.Never;
                this.lb_Browser.Visibility = LayoutVisibility.Never;
                return;
            }

            if (this.cbe_Type.Text == "Citymaker Service")
            {
                this.lb_Dsrc1.Text = "服务名称：";
                this.lb_Instance.Visibility = LayoutVisibility.Never;
                this.lb_Server.Visibility = LayoutVisibility.Always;
                this.lb_Port.Visibility = LayoutVisibility.Always;
                this.lb_User.Visibility = LayoutVisibility.Never;
                this.lb_Pwd.Visibility = LayoutVisibility.Always;
                this.lb_Dsrc.Visibility = LayoutVisibility.Never;
                this.lb_Dsrc1.Visibility = LayoutVisibility.Always;
                this.lb_Browser.Visibility = LayoutVisibility.Never;
                return;
            }
        }

        private void sbtn_Browser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Filter = "FDB File(*.fdb,*.sdb)|*.fdb;*.sdb";
            if (dlg.ShowDialog() == DialogResult.OK)
                this.te_DSName.Text = dlg.FileName;
        }


    }
}
