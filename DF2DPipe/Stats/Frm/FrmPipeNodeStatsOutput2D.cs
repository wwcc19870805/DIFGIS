using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF2DPipe.Stats.UC;

namespace DF2DPipe.Stats.Frm
{
    public partial class FrmPipeNodeStatsOutput2D :  XtraForm
    {
        private UCPipeNodeStatsOutput2D ucPipeNodeStatsOutput1;
    
        public FrmPipeNodeStatsOutput2D()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ucPipeNodeStatsOutput1 = new DF2DPipe.Stats.UC.UCPipeNodeStatsOutput2D();
            this.SuspendLayout();
            // 
            // ucPipeNodeStatsOutput1
            // 
            this.ucPipeNodeStatsOutput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPipeNodeStatsOutput1.Location = new System.Drawing.Point(0, 0);
            this.ucPipeNodeStatsOutput1.Name = "ucPipeNodeStatsOutput1";
            this.ucPipeNodeStatsOutput1.Size = new System.Drawing.Size(581, 419);
            this.ucPipeNodeStatsOutput1.TabIndex = 0;
            // 
            // FrmPipeNodeStatsOutput
            // 
            this.ClientSize = new System.Drawing.Size(581, 419);
            this.Controls.Add(this.ucPipeNodeStatsOutput1);
            this.MinimizeBox = false;
            this.Name = "FrmPipeNodeStatsOutput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统计结果";
            this.ResumeLayout(false);

        }

        public void SetData(DataTable dt)
        {
            this.ucPipeNodeStatsOutput1.SetData(dt);
        }
        public void SetData1(DataTable dt)
        {
            this.ucPipeNodeStatsOutput1.SetData1(dt);
        }
        public void SetStatsData(DataTable dt)
        {
            this.ucPipeNodeStatsOutput1.SetStatsData(dt);
        }
    }
}
