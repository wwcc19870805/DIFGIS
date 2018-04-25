using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

using DevExpress.XtraEditors;
using DF2DDataCheck.Frm;

namespace DF2DDataCheck.Frm
{
    public partial class frmProgress : XtraForm
    {
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.ProgressBar Pro;
       // private System.Windows.Forms.Timer m_Timer = new Timer();
       
        public frmProgress()
        {
            InitializeComponent();
        }
        public frmProgress(int Maximum)
        {
            InitializeComponent();
            this.Pro.Maximum = Maximum;
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /*protected override void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(Disposing);
        }
        */
        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblText = new System.Windows.Forms.Label();
            this.Pro = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.Location = new System.Drawing.Point(11, 51);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(469, 31);
            this.lblText.TabIndex = 0;
            // 
            // Pro
            // 
            this.Pro.Location = new System.Drawing.Point(11, 10);
            this.Pro.Maximum = 20;
            this.Pro.Name = "Pro";
            this.Pro.Size = new System.Drawing.Size(469, 30);
            this.Pro.TabIndex = 1;
            // 
            // frmProgress
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(498, 72);
            this.Controls.Add(this.Pro);
            this.Controls.Add(this.lblText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计算中....";
            this.Load += new System.EventHandler(this.frmProgress_Load);
            this.ResumeLayout(false);

        }
        #endregion

        #region "有效代码区"

        public void NextStep(string strInfo)
        {
            if (this.Pro.Value < this.Pro.Maximum)
            {
                Pro.Value = Pro.Value + 1;
            }
            else
            {
                Pro.Value = 0;
            }
            this.lblText.Text = strInfo;
            this.Update();
        }
        private void frmProgress_Load(object sender, EventArgs e)
        {
            //				m_Timer.Interval = 500;
            //				m_Timer.Start();
            //				m_Timer.Tick +=new EventHandler(m_Timer_Tick); 
        }
        #endregion
    }
}
