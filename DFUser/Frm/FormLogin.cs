using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DFCommon.Class;
using System.Windows.Forms;
using System.IO;
using DFUser.Class;
using System.Xml;

namespace DFUser.Frm
{
    public class FormLogin : XtraForm
    {
        private Label label1;
        private ComboBoxEdit cmb_user;
        private TextEdit textEdit1;
        private TextEdit txt_Password;
        private LabelControl labelControl3;
        private LabelControl labelControl2;
        private PictureBox focusPic;
        private LabelControl lbl_tip;
        private SimpleButton btn_Cancel;
        private DevExpress.Utils.ImageCollection btImgList;
        private IContainer components;
        private PictureBox normalPic;
        private SimpleButton btn_OK;
        private CheckEdit chk_Remenber;

        public FormLogin()
        {
            InitializeComponent();
            try
            {
                string path = Config.GetConfigValue("LoginPic");
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    path = Application.StartupPath + @"\..\Resource\Images\Login\Login.png";
                    if (File.Exists(path))
                    {
                        base.BackgroundImageStore = Image.FromFile(path);
                    }
                }
                else if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    base.BackgroundImageStore = Image.FromFile(path);
                }
                loadUser();
            }
            catch (Exception ex) { }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_user = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.txt_Password = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.focusPic = new System.Windows.Forms.PictureBox();
            this.lbl_tip = new DevExpress.XtraEditors.LabelControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btImgList = new DevExpress.Utils.ImageCollection();
            this.normalPic = new System.Windows.Forms.PictureBox();
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.chk_Remenber = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_user.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.focusPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btImgList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normalPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Remenber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(240, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 43;
            this.label1.Text = "记住用户名";
            // 
            // cmb_user
            // 
            this.cmb_user.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_user.EditValue = "admin";
            this.cmb_user.Location = new System.Drawing.Point(278, 98);
            this.cmb_user.Name = "cmb_user";
            this.cmb_user.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmb_user.Properties.Appearance.Options.UseFont = true;
            this.cmb_user.Properties.AutoHeight = false;
            this.cmb_user.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cmb_user.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_user.Size = new System.Drawing.Size(173, 25);
            this.cmb_user.TabIndex = 37;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(278, 100);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AutoHeight = false;
            this.textEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textEdit1.Properties.PasswordChar = '*';
            this.textEdit1.Size = new System.Drawing.Size(173, 25);
            this.textEdit1.TabIndex = 39;
            // 
            // txt_Password
            // 
            this.txt_Password.EditValue = "admin";
            this.txt_Password.Location = new System.Drawing.Point(278, 147);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Properties.AutoHeight = false;
            this.txt_Password.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txt_Password.Properties.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(173, 25);
            this.txt_Password.TabIndex = 38;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl3.Location = new System.Drawing.Point(228, 150);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(51, 14);
            this.labelControl3.TabIndex = 36;
            this.labelControl3.Text = "密   码：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl2.Location = new System.Drawing.Point(228, 101);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "用户名：";
            // 
            // focusPic
            // 
            this.focusPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.focusPic.Image = ((System.Drawing.Image)(resources.GetObject("focusPic.Image")));
            this.focusPic.Location = new System.Drawing.Point(219, 94);
            this.focusPic.Name = "focusPic";
            this.focusPic.Size = new System.Drawing.Size(246, 42);
            this.focusPic.TabIndex = 33;
            this.focusPic.TabStop = false;
            // 
            // lbl_tip
            // 
            this.lbl_tip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbl_tip.Location = new System.Drawing.Point(228, 272);
            this.lbl_tip.Name = "lbl_tip";
            this.lbl_tip.Size = new System.Drawing.Size(0, 14);
            this.lbl_tip.TabIndex = 32;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Cancel.Appearance.BackColor2 = System.Drawing.Color.CornflowerBlue;
            this.btn_Cancel.Appearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Cancel.Appearance.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_Cancel.Appearance.Options.UseBackColor = true;
            this.btn_Cancel.Appearance.Options.UseBorderColor = true;
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseForeColor = true;
            this.btn_Cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(376, 197);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(89, 35);
            this.btn_Cancel.TabIndex = 42;
            this.btn_Cancel.Text = "退出";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btImgList
            // 
            this.btImgList.ImageSize = new System.Drawing.Size(268, 35);
            this.btImgList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("btImgList.ImageStream")));
            this.btImgList.Images.SetKeyName(0, "TextLoseFocus.png");
            this.btImgList.Images.SetKeyName(1, "TextOnFocus.png");
            this.btImgList.Images.SetKeyName(2, "BtnLogin.png");
            // 
            // normalPic
            // 
            this.normalPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.normalPic.Image = ((System.Drawing.Image)(resources.GetObject("normalPic.Image")));
            this.normalPic.Location = new System.Drawing.Point(219, 142);
            this.normalPic.Name = "normalPic";
            this.normalPic.Size = new System.Drawing.Size(246, 42);
            this.normalPic.TabIndex = 34;
            this.normalPic.TabStop = false;
            // 
            // btn_OK
            // 
            this.btn_OK.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_OK.Appearance.BackColor2 = System.Drawing.Color.CornflowerBlue;
            this.btn_OK.Appearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btn_OK.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btn_OK.Appearance.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_OK.Appearance.Options.UseBackColor = true;
            this.btn_OK.Appearance.Options.UseBorderColor = true;
            this.btn_OK.Appearance.Options.UseFont = true;
            this.btn_OK.Appearance.Options.UseForeColor = true;
            this.btn_OK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_OK.Location = new System.Drawing.Point(221, 197);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(89, 35);
            this.btn_OK.TabIndex = 41;
            this.btn_OK.Text = "登录";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // chk_Remenber
            // 
            this.chk_Remenber.Location = new System.Drawing.Point(219, 240);
            this.chk_Remenber.Name = "chk_Remenber";
            this.chk_Remenber.Properties.Caption = "";
            this.chk_Remenber.Size = new System.Drawing.Size(19, 19);
            this.chk_Remenber.TabIndex = 40;
            // 
            // FormLogin
            // 
            this.AcceptButton = this.btn_OK;
            this.Appearance.BackColor = System.Drawing.Color.SlateGray;
            this.Appearance.Options.UseBackColor = true;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(520, 301);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_user);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.focusPic);
            this.Controls.Add(this.lbl_tip);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.normalPic);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.chk_Remenber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.cmb_user.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.focusPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btImgList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normalPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Remenber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private bool DoLogin(string userid, string pwd,out string msg)
        {
            try
            {
                msg = "";
                string sql1 = "select * from UserInfo where UserID='" + userid + "'";
                DataTable dt1 = new DataTable();
                bool bRet1 = DB.Instance.queryDB(sql1, ref dt1);
                if (bRet1)
                {
                    if (dt1.Rows.Count == 0)
                    {
                        msg = "该用户不存在，请联系管理员";
                        return false;
                    }
                }
                else
                {
                    msg = "数据库查询错误";
                    return false;
                }
                string sql = "select UserName,IsSuperAdmin from UserInfo where UserID='" + userid +
                    "' and Pwd='" + pwd + "'";
                DataTable dt = new DataTable();
                bool bRet = DB.Instance.queryDB(sql, ref dt);
                if (bRet && dt.Rows.Count == 1)
                {
                    UserInfo.Instance.UserID = userid;
                    UserInfo.Instance.Pwd = pwd;
                    UserInfo.Instance.UserName = dt.Rows[0]["UserName"].ToString();
                    UserInfo.Instance.IsSuperAdmin = Convert.ToBoolean(dt.Rows[0]["IsSuperAdmin"]);

                    //UserInfo.Instance.IsSuperAdmin = 
                    return true;
                }
                else
                {
                    msg = "用户名或者密码错误";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "网络错误";
                return false;
            }
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            string str = this.cmb_user.Text.Trim();
            string str2 = this.txt_Password.Text.Trim();
            if (string.IsNullOrEmpty(str))
            {
                this.cmb_user.Focus();
                this.lbl_tip.Text = "请输入用户名";
                return;
            }
            else if (string.IsNullOrEmpty(str2))
            {
                this.txt_Password.Focus();
                this.lbl_tip.Text = "请输入密码";
                return;
            }
            else
            {
                string isNet = Config.GetConfigValue("IsNet");
                if (isNet.ToLower() == "true")
                {
                    //string res = WebApp.CallWebHandle(url, new string[] { "userid", "pwd" }, new string[] { this.cmb_user.Text, this.txt_Password.Text });
                    string msg = "";
                    bool res = this.DoLogin(this.cmb_user.Text, this.txt_Password.Text, out msg);
                    if (res)
                    {
                        if(!UserInfo.Instance.IsSuperAdmin) AddInReplace.Replace(UserInfo.Instance.UserID);
                        else
                        {
                            AddInReplace.Copy();
                        }
                        if (this.chk_Remenber.Checked) RememberUser();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        this.lbl_tip.Text = msg;
                        return;
                    }
                }
                else
                {
                    AddInReplace.Copy();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private void loadUser()
        {
            string localDataPath = SystemInfo.Instance.LocalDataPath;
            string userInfoPath = Path.Combine(localDataPath, "User\\user.xml");
            if (!File.Exists(userInfoPath)) return;
            XmlDocument doc = new XmlDocument();
            doc.Load(userInfoPath);
            XmlNode root = doc.SelectSingleNode("UserInfo");
            cmb_user.Properties.Items.Clear();
            foreach (XmlElement node in root.ChildNodes)
            {
                if (node.Attributes == null) continue ;
                string id = node.GetAttribute("id");
                cmb_user.Properties.Items.Add(id);
            }
            cmb_user.SelectedIndex = 0;
        }
        private void RememberUser()
        {
            try
            {
                string localDataPath = SystemInfo.Instance.LocalDataPath;
                string userPath = localDataPath + "User\\";
                if (!Directory.Exists(userPath))
                {
                    Directory.CreateDirectory(userPath);
                }
                if (!Directory.Exists(userPath)) return;
                string userInfoPath = Path.Combine(localDataPath, "User\\user.xml");
                if (File.Exists(userInfoPath))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(userInfoPath);
                    XmlNode root = doc.SelectSingleNode("UserInfo");
                    XmlElement node = doc.CreateElement("User");
                    node.SetAttribute("id", cmb_user.Text);
                    //node.SetAttribute("pwd", txt_Password.Text);
                    root.AppendChild(node);
                    doc.Save(userInfoPath);

                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    XmlNode root = doc.CreateElement("UserInfo");
                    doc.AppendChild(root);
                    XmlElement node = doc.CreateElement("User");
                    node.SetAttribute("id", cmb_user.Text);
                    //node.SetAttribute("pwd", txt_Password.Text);
                    root.AppendChild(node);
                    doc.Save(userInfoPath);
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
