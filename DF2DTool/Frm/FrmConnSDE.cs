using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;

namespace DF2DTool.Frm
{
    public partial class FrmConnSDE : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.CheckEdit ceVersion;
        private DevExpress.XtraEditors.ComboBoxEdit cbVersion;
        private DevExpress.XtraEditors.SimpleButton btnTestConn;
        private DevExpress.XtraEditors.TextEdit teUserPwd;
        private DevExpress.XtraEditors.TextEdit teUserName;
        private DevExpress.XtraEditors.TextEdit teDatabase;
        private DevExpress.XtraEditors.TextEdit teInstance;
        private DevExpress.XtraEditors.TextEdit teServer;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;

        private IWorkspace pWorkspace;
        private string m_Version = "";
        private DialogResult m_result;
    
        public FrmConnSDE()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.ceVersion = new DevExpress.XtraEditors.CheckEdit();
            this.cbVersion = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnTestConn = new DevExpress.XtraEditors.SimpleButton();
            this.teUserPwd = new DevExpress.XtraEditors.TextEdit();
            this.teUserName = new DevExpress.XtraEditors.TextEdit();
            this.teDatabase = new DevExpress.XtraEditors.TextEdit();
            this.teInstance = new DevExpress.XtraEditors.TextEdit();
            this.teServer = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teInstance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl6);
            this.layoutControl1.Controls.Add(this.labelControl5);
            this.layoutControl1.Controls.Add(this.labelControl4);
            this.layoutControl1.Controls.Add(this.labelControl3);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.ceVersion);
            this.layoutControl1.Controls.Add(this.cbVersion);
            this.layoutControl1.Controls.Add(this.btnTestConn);
            this.layoutControl1.Controls.Add(this.teUserPwd);
            this.layoutControl1.Controls.Add(this.teUserName);
            this.layoutControl1.Controls.Add(this.teDatabase);
            this.layoutControl1.Controls.Add(this.teInstance);
            this.layoutControl1.Controls.Add(this.teServer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(284, 313);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(5, 233);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.StyleController = this.layoutControl1;
            this.labelControl6.TabIndex = 19;
            this.labelControl6.Text = "版本名称：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 155);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.StyleController = this.layoutControl1;
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "密   码：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 129);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.StyleController = this.layoutControl1;
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "用户名：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.StyleController = this.layoutControl1;
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "数据库：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "服   务：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "服务器：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(146, 285);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(2, 285);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(140, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ceVersion
            // 
            this.ceVersion.Location = new System.Drawing.Point(5, 259);
            this.ceVersion.Name = "ceVersion";
            this.ceVersion.Properties.Caption = "保存版本";
            this.ceVersion.Size = new System.Drawing.Size(274, 19);
            this.ceVersion.StyleController = this.layoutControl1;
            this.ceVersion.TabIndex = 11;
            // 
            // cbVersion
            // 
            this.cbVersion.EditValue = "SDE.DEFAULT";
            this.cbVersion.Location = new System.Drawing.Point(69, 233);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbVersion.Size = new System.Drawing.Size(210, 22);
            this.cbVersion.StyleController = this.layoutControl1;
            this.cbVersion.TabIndex = 10;
            this.cbVersion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbVersion_MouseDown);
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(189, 181);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(90, 22);
            this.btnTestConn.StyleController = this.layoutControl1;
            this.btnTestConn.TabIndex = 9;
            this.btnTestConn.Text = "测试连接";
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // teUserPwd
            // 
            this.teUserPwd.EditValue = "wkgis";
            this.teUserPwd.Location = new System.Drawing.Point(57, 155);
            this.teUserPwd.Name = "teUserPwd";
            this.teUserPwd.Properties.PasswordChar = '*';
            this.teUserPwd.Size = new System.Drawing.Size(222, 22);
            this.teUserPwd.StyleController = this.layoutControl1;
            this.teUserPwd.TabIndex = 8;
            // 
            // teUserName
            // 
            this.teUserName.EditValue = "sa";
            this.teUserName.Location = new System.Drawing.Point(57, 129);
            this.teUserName.Name = "teUserName";
            this.teUserName.Size = new System.Drawing.Size(222, 22);
            this.teUserName.StyleController = this.layoutControl1;
            this.teUserName.TabIndex = 7;
            // 
            // teDatabase
            // 
            this.teDatabase.EditValue = "SDE";
            this.teDatabase.Location = new System.Drawing.Point(57, 77);
            this.teDatabase.Name = "teDatabase";
            this.teDatabase.Size = new System.Drawing.Size(222, 22);
            this.teDatabase.StyleController = this.layoutControl1;
            this.teDatabase.TabIndex = 6;
            // 
            // teInstance
            // 
            this.teInstance.EditValue = "sde:sqlserver:192.168.1.102";
            this.teInstance.Location = new System.Drawing.Point(57, 51);
            this.teInstance.Name = "teInstance";
            this.teInstance.Size = new System.Drawing.Size(222, 22);
            this.teInstance.StyleController = this.layoutControl1;
            this.teInstance.TabIndex = 5;
            // 
            // teServer
            // 
            this.teServer.EditValue = "192.168.1.102";
            this.teServer.Location = new System.Drawing.Point(57, 25);
            this.teServer.Name = "teServer";
            this.teServer.Size = new System.Drawing.Size(222, 22);
            this.teServer.StyleController = this.layoutControl1;
            this.teServer.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 313);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem13});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(284, 104);
            this.layoutControlGroup2.Text = "连接参数";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teServer;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(52, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(226, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teInstance;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(52, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(226, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teDatabase;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(52, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(226, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.labelControl1;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.labelControl2;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.labelControl3;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.layoutControlItem14,
            this.layoutControlItem15});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 104);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(284, 104);
            this.layoutControlGroup3.Text = "账号";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teUserName;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(52, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(226, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teUserPwd;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(52, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(226, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnTestConn;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(184, 52);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 52);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(184, 26);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(184, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(184, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.labelControl4;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.labelControl5;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem16});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 208);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(284, 75);
            this.layoutControlGroup4.Text = "版本";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cbVersion;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(64, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(214, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.ceVersion;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(278, 23);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.labelControl6;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnOK;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 283);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(144, 30);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnCancel;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(144, 283);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(140, 30);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // FrmConnSDE
            // 
            this.ClientSize = new System.Drawing.Size(284, 313);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmConnSDE";
            this.ShowIcon = false;
            this.Text = "连接SDE";
            this.Load += new System.EventHandler(this.FrmConnSDE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teInstance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.ResumeLayout(false);

        }

        public IPropertySet ConnectionProperty
        {
            get
            {
                return GetConnectionProperty();
            }
        }

        public string VersionName
        {
            get
            {
                return m_Version;
            }
            set
            {
                m_Version = value;
                cbVersion.Text = m_Version;
            }

        }

        public DialogResult Result
        {
            get
            {
                return m_result;
            }
        }

        /// <summary>
        /// 保存配置信息到***.sde文件
        /// </summary>
        private void writePropertySet()
        {
        }

        /// <summary>
        /// 从***.sde文件读配置信息
        /// </summary>
        private void readPropertySet()
        {
        }
        /// <summary>
        /// 打开连接
        /// </summary>
        private IWorkspace getWorkspace()
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

            //IWorkspaceFactory pSdeWorkspaceFactory = new SdeWorkspaceFactoryClass();
            try
            {
                return workspaceFactory.Open(GetConnectionProperty(), 0);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("SDE连接参数错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
            return null;
        }
        /// <summary>
        /// 得到连接属性
        /// </summary>
        /// <param name="bSaveUserAndPass"></param>
        /// <param name="bSaveVersion"></param>
        /// <returns></returns>
        private IPropertySet GetConnectionProperty()//bool bSaveUserAndPass, bool bSaveVersion)
        {
            string str;

            IPropertySet pPropertySet = new PropertySetClass();

            str = this.teServer.Text.Trim();
            pPropertySet.SetProperty("SERVER", str);

            str = this.teInstance.Text.Trim();
            pPropertySet.SetProperty("INSTANCE", str);

            str = this.teDatabase.Text.Trim();
            if (str.Length >= 0)
            {
                pPropertySet.SetProperty("DATABASE", str);
            }

            str = this.teUserName.Text.Trim();
            pPropertySet.SetProperty("USER", str);

            str = this.teUserPwd.Text.Trim();
            pPropertySet.SetProperty("PASSWORD", str);

            pPropertySet.SetProperty("AUTHENTICATION_MODE", "DBMS");


            //if (m_Version == "")
            //{
            //    m_Version = "SDE.DEFAULT";
            //}
            //pPropertySet.SetProperty("VERSION", m_Version);

            return pPropertySet;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_result = this.btnOK.DialogResult;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_result = this.btnCancel.DialogResult;
            this.Close();
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            pWorkspace = null;
            try
            {

                pWorkspace = getWorkspace();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
            if (pWorkspace != null)
            {
                XtraMessageBox.Show("成功打开连接！");
                btnTestConn.Enabled = false;
                cbVersion.Enabled = true;
            }
        }

        private void FrmConnSDE_Load(object sender, EventArgs e)
        {
            if (this.m_Version == "")
            {
                this.VersionName = "SDE.DEFAULT";
            }
        }

        private void cbVersion_MouseDown(object sender, MouseEventArgs e)
        {
            pWorkspace = getWorkspace();
            if (pWorkspace != null)
            {
                //this.cbVersion.Properties. = pWorkspace as IVersionedWorkspace;
            }
        }

      

    }
}
