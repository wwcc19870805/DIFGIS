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
    public class FrmMyInfo : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private LabelControl lcInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private TextEdit teEmail;
        private TextEdit tePhone;
        private TextEdit teDep;
        private TextEdit teCmp;
        private TextEdit teUserName;
        private TextEdit teUserID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        AuthService authService;
    
        public FrmMyInfo()
        {
            InitializeComponent();
            authService = new AuthService();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.teEmail = new DevExpress.XtraEditors.TextEdit();
            this.tePhone = new DevExpress.XtraEditors.TextEdit();
            this.teDep = new DevExpress.XtraEditors.TextEdit();
            this.teCmp = new DevExpress.XtraEditors.TextEdit();
            this.teUserName = new DevExpress.XtraEditors.TextEdit();
            this.teUserID = new DevExpress.XtraEditors.TextEdit();
            this.lcInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCmp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teEmail);
            this.layoutControl1.Controls.Add(this.tePhone);
            this.layoutControl1.Controls.Add(this.teDep);
            this.layoutControl1.Controls.Add(this.teCmp);
            this.layoutControl1.Controls.Add(this.teUserName);
            this.layoutControl1.Controls.Add(this.teUserID);
            this.layoutControl1.Controls.Add(this.lcInfo);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(194, 184);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teEmail
            // 
            this.teEmail.Enabled = false;
            this.teEmail.Location = new System.Drawing.Point(41, 132);
            this.teEmail.Name = "teEmail";
            this.teEmail.Size = new System.Drawing.Size(151, 22);
            this.teEmail.StyleController = this.layoutControl1;
            this.teEmail.TabIndex = 15;
            // 
            // tePhone
            // 
            this.tePhone.Enabled = false;
            this.tePhone.Location = new System.Drawing.Point(41, 106);
            this.tePhone.Name = "tePhone";
            this.tePhone.Size = new System.Drawing.Size(151, 22);
            this.tePhone.StyleController = this.layoutControl1;
            this.tePhone.TabIndex = 14;
            // 
            // teDep
            // 
            this.teDep.Enabled = false;
            this.teDep.Location = new System.Drawing.Point(41, 80);
            this.teDep.Name = "teDep";
            this.teDep.Size = new System.Drawing.Size(151, 22);
            this.teDep.StyleController = this.layoutControl1;
            this.teDep.TabIndex = 13;
            // 
            // teCmp
            // 
            this.teCmp.Enabled = false;
            this.teCmp.Location = new System.Drawing.Point(41, 54);
            this.teCmp.Name = "teCmp";
            this.teCmp.Size = new System.Drawing.Size(151, 22);
            this.teCmp.StyleController = this.layoutControl1;
            this.teCmp.TabIndex = 12;
            // 
            // teUserName
            // 
            this.teUserName.Enabled = false;
            this.teUserName.Location = new System.Drawing.Point(41, 28);
            this.teUserName.Name = "teUserName";
            this.teUserName.Size = new System.Drawing.Size(151, 22);
            this.teUserName.StyleController = this.layoutControl1;
            this.teUserName.TabIndex = 11;
            // 
            // teUserID
            // 
            this.teUserID.Enabled = false;
            this.teUserID.Location = new System.Drawing.Point(41, 2);
            this.teUserID.Name = "teUserID";
            this.teUserID.Size = new System.Drawing.Size(151, 22);
            this.teUserID.StyleController = this.layoutControl1;
            this.teUserID.TabIndex = 10;
            // 
            // lcInfo
            // 
            this.lcInfo.Location = new System.Drawing.Point(2, 158);
            this.lcInfo.Name = "lcInfo";
            this.lcInfo.Size = new System.Drawing.Size(41, 14);
            this.lcInfo.StyleController = this.layoutControl1;
            this.lcInfo.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(143, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(49, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "退  出";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(92, 158);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(47, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(194, 184);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnOK;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(90, 156);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(51, 28);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCancel;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(141, 156);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(53, 28);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(45, 156);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(45, 28);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lcInfo;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 156);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(45, 28);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teUserID;
            this.layoutControlItem1.CustomizationFormText = "用户：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(194, 26);
            this.layoutControlItem1.Text = "用户：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teUserName;
            this.layoutControlItem2.CustomizationFormText = "姓名：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(194, 26);
            this.layoutControlItem2.Text = "姓名：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teCmp;
            this.layoutControlItem3.CustomizationFormText = "单位;";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(194, 26);
            this.layoutControlItem3.Text = "单位：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.teDep;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(194, 26);
            this.layoutControlItem7.Text = "部门：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.tePhone;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(194, 26);
            this.layoutControlItem8.Text = "电话：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teEmail;
            this.layoutControlItem9.CustomizationFormText = "邮箱：";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(194, 26);
            this.layoutControlItem9.Text = "邮箱：";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(36, 14);
            // 
            // FrmMyInfo
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(194, 184);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmMyInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "我的信息";
            this.Load += new System.EventHandler(this.FrmMyInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCmp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FrmMyInfo_Load(object sender, EventArgs e)
        {
            DataTable dtMy = authService.queryMyInfo();
            if (dtMy == null || dtMy.Rows.Count == 0) return;
            DataRow dr = dtMy.Rows[0];
            //string[] info = new string[] { "UserID", "UserName", "Company", "DepName", "Phone", "Email" };
            this.teUserID.Text = dr["UserID"].ToString();
            this.teUserName.Text = dr["UserName"].ToString();
            this.teCmp.Text = dr["Company"].ToString();
            this.teDep.Text = dr["DepName"].ToString();
            this.tePhone.Text = dr["Phone"].ToString();
            this.teEmail.Text = dr["Email"].ToString();
        }
    }
}
