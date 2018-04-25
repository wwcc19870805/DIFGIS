using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DF2DPipe.Stats.UC;
using System.Data;

namespace DF2DPipe.Stats.Frm
{
    public partial class FrmPipeLineStatsOutput2D : XtraForm
    {
        private UCPipeLineStatsOutput ucStatsOutput1;
        public FrmPipeLineStatsOutput2D()
        {
            InitializeComponent();
        }
     
        
        private void InitializeComponent()
        {
            this.ucStatsOutput1 = new DF2DPipe.Stats.UC.UCPipeLineStatsOutput();
            this.SuspendLayout();
            // 
            // ucStatsOutput1
            // 
            this.ucStatsOutput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStatsOutput1.Location = new System.Drawing.Point(0, 0);
            this.ucStatsOutput1.Name = "ucStatsOutput1";
            this.ucStatsOutput1.Size = new System.Drawing.Size(597, 457);
            this.ucStatsOutput1.TabIndex = 0;
            // 
            // FrmPipeLineStatsOutput2D
            // 
            this.ClientSize = new System.Drawing.Size(597, 457);
            this.Controls.Add(this.ucStatsOutput1);
            this.MinimizeBox = false;
            this.Name = "FrmPipeLineStatsOutput2D";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统计结果";
            this.ResumeLayout(false);

        }

        public void SetData(DataTable dt)
        {
            this.ucStatsOutput1.SetData(dt);
        }
        public void SetData1(DataTable dt)
        {
            this.ucStatsOutput1.SetData1(dt);
        }
        //public void SetData2(DataTable dt)
        //{
        //    this.ucStatsOutput1.SetData2(dt);
        //}
        //public void SetStatsData(DataTable dt)
        //{
        //    this.ucStatsOutput1.SetStatsData(dt);
        //}

        
        

        
    }
}
