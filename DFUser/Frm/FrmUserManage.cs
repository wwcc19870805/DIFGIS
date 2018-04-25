using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DFUser.Class;

namespace DFUser.Frm
{
    public class FrmUserManage : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private ComboBoxEdit cmbDep;
        private TextEdit tePwd;
        private TextEdit teUserName;
        private TextEdit teUserID;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private SimpleButton btnOK;
        private SimpleButton btnCancel;
        private TextEdit teEmail;
        private TextEdit tePhone;
        private ComboBoxEdit cmbRole;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private LabelControl lcInfo;
        private ComboBoxEdit cbName;
        private ComboBoxEdit cbDeplist;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;

        AuthService authService;
        Dictionary<string, string> dicDep;
        Dictionary<string, string> dicDepUser;
        Dictionary<string, string> dicRoleID;
        string msg = string.Empty;
    
        public FrmUserManage()
        {
            InitializeComponent();
            authService = new AuthService();          
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbDeplist = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lcInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.teEmail = new DevExpress.XtraEditors.TextEdit();
            this.tePhone = new DevExpress.XtraEditors.TextEdit();
            this.cmbRole = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbDep = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tePwd = new DevExpress.XtraEditors.TextEdit();
            this.teUserName = new DevExpress.XtraEditors.TextEdit();
            this.teUserID = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbDeplist.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbName);
            this.layoutControl1.Controls.Add(this.cbDeplist);
            this.layoutControl1.Controls.Add(this.lcInfo);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.teEmail);
            this.layoutControl1.Controls.Add(this.tePhone);
            this.layoutControl1.Controls.Add(this.cmbRole);
            this.layoutControl1.Controls.Add(this.cmbDep);
            this.layoutControl1.Controls.Add(this.tePwd);
            this.layoutControl1.Controls.Add(this.teUserName);
            this.layoutControl1.Controls.Add(this.teUserID);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(330, 196);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbName
            // 
            this.cbName.Location = new System.Drawing.Point(170, 39);
            this.cbName.Name = "cbName";
            this.cbName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbName.Size = new System.Drawing.Size(155, 22);
            this.cbName.StyleController = this.layoutControl1;
            this.cbName.TabIndex = 16;
            this.cbName.SelectedIndexChanged += new System.EventHandler(this.cbName_SelectedIndexChanged_1);
            // 
            // cbDeplist
            // 
            this.cbDeplist.Location = new System.Drawing.Point(5, 39);
            this.cbDeplist.Name = "cbDeplist";
            this.cbDeplist.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbDeplist.Size = new System.Drawing.Size(161, 22);
            this.cbDeplist.StyleController = this.layoutControl1;
            this.cbDeplist.TabIndex = 15;
            this.cbDeplist.SelectedIndexChanged += new System.EventHandler(this.cbDeplist_SelectedIndexChanged);
            // 
            // lcInfo
            // 
            this.lcInfo.Location = new System.Drawing.Point(161, 146);
            this.lcInfo.Name = "lcInfo";
            this.lcInfo.Size = new System.Drawing.Size(167, 22);
            this.lcInfo.StyleController = this.layoutControl1;
            this.lcInfo.TabIndex = 14;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(84, 172);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(198, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "退  出";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // teEmail
            // 
            this.teEmail.Location = new System.Drawing.Point(48, 146);
            this.teEmail.Name = "teEmail";
            this.teEmail.Size = new System.Drawing.Size(109, 22);
            this.teEmail.StyleController = this.layoutControl1;
            this.teEmail.TabIndex = 11;
            // 
            // tePhone
            // 
            this.tePhone.Location = new System.Drawing.Point(208, 120);
            this.tePhone.Name = "tePhone";
            this.tePhone.Size = new System.Drawing.Size(120, 22);
            this.tePhone.StyleController = this.layoutControl1;
            this.tePhone.TabIndex = 10;
            // 
            // cmbRole
            // 
            this.cmbRole.Location = new System.Drawing.Point(208, 94);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbRole.Size = new System.Drawing.Size(120, 22);
            this.cmbRole.StyleController = this.layoutControl1;
            this.cmbRole.TabIndex = 9;
            // 
            // cmbDep
            // 
            this.cmbDep.Location = new System.Drawing.Point(48, 120);
            this.cmbDep.Name = "cmbDep";
            this.cmbDep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDep.Size = new System.Drawing.Size(110, 22);
            this.cmbDep.StyleController = this.layoutControl1;
            this.cmbDep.TabIndex = 8;
            // 
            // tePwd
            // 
            this.tePwd.Location = new System.Drawing.Point(48, 94);
            this.tePwd.Name = "tePwd";
            this.tePwd.Properties.PasswordChar = '*';
            this.tePwd.Size = new System.Drawing.Size(110, 22);
            this.tePwd.StyleController = this.layoutControl1;
            this.tePwd.TabIndex = 7;
            // 
            // teUserName
            // 
            this.teUserName.Location = new System.Drawing.Point(208, 68);
            this.teUserName.Name = "teUserName";
            this.teUserName.Size = new System.Drawing.Size(120, 22);
            this.teUserName.StyleController = this.layoutControl1;
            this.teUserName.TabIndex = 6;
            // 
            // teUserID
            // 
            this.teUserID.Location = new System.Drawing.Point(48, 68);
            this.teUserID.Name = "teUserID";
            this.teUserID.Size = new System.Drawing.Size(110, 22);
            this.teUserID.StyleController = this.layoutControl1;
            this.teUserID.TabIndex = 5;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(2, 2);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "添加用户"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "修改用户"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "删除用户")});
            this.radioGroup1.Size = new System.Drawing.Size(326, 30);
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
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem2,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(330, 196);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(330, 34);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teUserID;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 66);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(160, 26);
            this.layoutControlItem2.Text = "用户*：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(43, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teUserName;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(160, 66);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(170, 26);
            this.layoutControlItem3.Text = "姓名：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(43, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tePwd;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 92);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(160, 26);
            this.layoutControlItem4.Text = "密码*：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(43, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbDep;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 118);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(160, 26);
            this.layoutControlItem5.Text = "部门：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(43, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cmbRole;
            this.layoutControlItem6.CustomizationFormText = "角色：";
            this.layoutControlItem6.Location = new System.Drawing.Point(160, 92);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(170, 26);
            this.layoutControlItem6.Text = "角色：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(43, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.tePhone;
            this.layoutControlItem7.CustomizationFormText = "电话：";
            this.layoutControlItem7.Location = new System.Drawing.Point(160, 118);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(170, 26);
            this.layoutControlItem7.Text = "电话：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(43, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teEmail;
            this.layoutControlItem8.CustomizationFormText = "邮箱：";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(159, 26);
            this.layoutControlItem8.Text = "邮箱：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(43, 14);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 170);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(82, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnCancel;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(196, 170);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(134, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnOK;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(82, 170);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(114, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.lcInfo;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(159, 144);
            this.layoutControlItem11.MaxSize = new System.Drawing.Size(171, 26);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(171, 26);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(171, 26);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12,
            this.layoutControlItem13});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(330, 32);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.cbDeplist;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(165, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.cbName;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(165, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(159, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // FrmUserManage
            // 
            this.ClientSize = new System.Drawing.Size(330, 196);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmUserManage";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.FrmUserManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbDeplist.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            this.ResumeLayout(false);

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    this.teUserID.Enabled = true;
                    this.cbDeplist.Enabled = false;
                    this.cbName.Enabled = false;

                    break;
                case 1:
                    this.teUserID.Enabled = false;
                    this.cbDeplist.Enabled = true;
                    this.cbName.Enabled = true;

                    break;
                case 2:
                    this.teUserID.Enabled = false;
                    this.cbDeplist.Enabled = true;
                    this.cbName.Enabled = true;
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (this.radioGroup1.SelectedIndex == 0)
            {
                AddUser();
                this.lcInfo.Text = msg;
                //if(AddUser())
                //XtraMessageBox.Show("添加用户成功", "提示");
            }
            else if (this.radioGroup1.SelectedIndex == 1)
            {
                EditUser();
                this.lcInfo.Text = msg;
                //if (EditUser()) XtraMessageBox.Show("修改用户信息成功", "提示");
                //else
                //    XtraMessageBox.Show("修改用户信息失败", "提示");
            }
            else
            {
                DeleteUser();
                this.lcInfo.Text = msg;
                //if (DeleteUser()) XtraMessageBox.Show("删除用户成功", "提示");
                //else
                //    XtraMessageBox.Show("删除用户失败", "提示");

            }
            cbDeplist_SelectedIndexChanged(null, null);
                
        }
        private bool DeleteUser()
        {
            return authService.deleteUser(teUserID.Text,out msg);
            this.lcInfo.Text = msg;
        }
        private bool EditUser()
        {
            string roleID = dicRoleID[cmbRole.Text];
            string depID = dicDep[cmbDep.Text];
            return authService.editUser(teUserID.Text, tePwd.Text, teUserName.Text, roleID, null, depID, tePhone.Text, teEmail.Text, null, "CS",out msg);
            this.lcInfo.Text = msg;
        }
        private bool AddUser()
        {
            //if (this.teUserID.Text == null || this.tePwd.Text == null)
            //{
            //    XtraMessageBox.Show("用户名或密码不能为空", "提示");
            //    return false;
            //}
            string roleID = dicRoleID[cmbRole.Text];
            string depID = dicDep[cmbDep.Text];
            return authService.addUser(teUserID.Text, tePwd.Text, teUserName.Text, roleID, null, depID, tePhone.Text, teEmail.Text, null, "CS",out msg);
            this.lcInfo.Text = msg;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FrmUserManage_Load(object sender, EventArgs e)
        {
            //InitMyInfo();
            this.cbDeplist.Enabled = false;
            this.cbName.Enabled = false;
            dicDep = new Dictionary<string, string>();
            dicRoleID = new Dictionary<string, string>();
            dicDepUser = new Dictionary<string, string>();
            DataTable dtDep = authService.queryAllDeps();
            if (dtDep == null) return;
            foreach (DataRow dr in dtDep.Rows)
            {
                this.cmbDep.Properties.Items.Add(dr["DepName"].ToString());
                this.cbDeplist.Properties.Items.Add(dr["DepName"].ToString());
                dicDep[dr["DepName"].ToString()] = dr["DepID"].ToString();

            }
            this.cmbDep.SelectedIndex = 0;
            this.cbDeplist.SelectedIndex = 0;
            this.cmbRole.Properties.Items.Add("超级管理员");
            dicRoleID["超级管理员"] = "IsSuperAdmin";
            DataTable dtRole = authService.queryAllRoles("2D3D", "CS");
            if (dtRole == null) return;
            foreach (DataRow dr in dtRole.Rows)
            {
                this.cmbRole.Properties.Items.Add(dr["RoleName"].ToString());
                dicRoleID[dr["RoleName"].ToString()] = dr["RoleID"].ToString();
            }

            this.cmbRole.SelectedIndex = 0;
        }
      
       
     
        private void cbDeplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dicDepUser == null) return;
            string depID = dicDep[cbDeplist.SelectedItem.ToString()];
            if(depID == null)return;
            DataTable dt = authService.queryUsersByDepID(depID);
            if (dt == null || dt.Rows.Count == 0) return;
            this.cbName.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                this.cbName.Properties.Items.Add(dr["UserName"].ToString());
                dicDepUser[dr["UserName"].ToString()] = dr["UserID"].ToString();
            }   
            this.cbName.SelectedIndex = 0;
        }
   
        private void cbName_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string userId = dicDepUser[cbName.SelectedItem.ToString()];
            DataTable dt = authService.queryUserInfoByUserID(userId);
            if (dt == null) return;
            if (dt.Rows.Count == 0) return;
            DataRow dr = dt.Rows[0];
            this.teUserID.Text = dr["UserID"].ToString();
            this.teUserName.Text = dr["UserName"].ToString();
            this.tePwd.Text = dr["Pwd"].ToString();
            this.tePhone.Text = dr["Phone"].ToString();
            this.teEmail.Text = dr["Email"].ToString();
            string roleID = dr["RoleIDCS"].ToString();
            string depID = dr["DepID"].ToString();

        }
    }
}
