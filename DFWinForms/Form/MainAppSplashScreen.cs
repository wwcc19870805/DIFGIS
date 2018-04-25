using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors;
using System.IO;
using DFCommon.Class;

namespace DFWinForms.Form
{
    public class MainAppSplashScreen : SplashScreen
    {
        private IContainer components;
        private LabelControl lblDescription;
        private PictureEdit peMainBackGround;

        // Methods
        public MainAppSplashScreen()
        {
            this.InitializeComponent();
            try
            {
                this.lblDescription.Parent = this.peMainBackGround;
                string path = Config.GetConfigValue("SplashPic");
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    path = Application.StartupPath + @"\..\Resource\Images\Login\Splash.png";
                    if (File.Exists(path))
                    {
                        this.peMainBackGround.Image = Image.FromFile(path);
                    }
                }
                else if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    this.peMainBackGround.Image = Image.FromFile(path);
                }
            }
            catch (Exception ex) { }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainAppSplashScreen));
            this.peMainBackGround = new DevExpress.XtraEditors.PictureEdit();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.peMainBackGround.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // peMainBackGround
            // 
            this.peMainBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peMainBackGround.EditValue = ((object)(resources.GetObject("peMainBackGround.EditValue")));
            this.peMainBackGround.Location = new System.Drawing.Point(0, 0);
            this.peMainBackGround.Name = "peMainBackGround";
            this.peMainBackGround.Properties.AllowFocused = false;
            this.peMainBackGround.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peMainBackGround.Properties.Appearance.Options.UseBackColor = true;
            this.peMainBackGround.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peMainBackGround.Properties.ShowMenu = false;
            this.peMainBackGround.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peMainBackGround.Size = new System.Drawing.Size(520, 300);
            this.peMainBackGround.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(28, 274);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(208, 14);
            this.lblDescription.TabIndex = 12;
            this.lblDescription.Text = "系统正在启动，请稍候......                ";
            // 
            // MainAppSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 300);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.peMainBackGround);
            this.Name = "MainAppSplashScreen";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Black;
            ((System.ComponentModel.ISupportInitialize)(this.peMainBackGround.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            string str = arg.ToString();
            this.lblDescription.Text = str;
        }

        public enum SplashScreenCommand
        {
        }
    }
}
