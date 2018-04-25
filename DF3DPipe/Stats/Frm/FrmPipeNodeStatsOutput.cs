using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DPipe.Stats.UC;

namespace DF3DPipe.Stats.Frm
{
    public class FrmPipeNodeStatsOutput : XtraForm
    {
        private SplitContainerControl splitContainerControl1;
        private UCPipeNodeStatsOutput ucPipeNodeStatsOutput1;
        private UCPipeNodeStatsChart ucPipeNodeStatsChart1;
    
        public FrmPipeNodeStatsOutput()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucPipeNodeStatsOutput1 = new DF3DPipe.Stats.UC.UCPipeNodeStatsOutput();
            this.ucPipeNodeStatsChart1 = new DF3DPipe.Stats.UC.UCPipeNodeStatsChart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ucPipeNodeStatsOutput1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ucPipeNodeStatsChart1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(974, 482);
            this.splitContainerControl1.SplitterPosition = 400;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ucPipeNodeStatsOutput1
            // 
            this.ucPipeNodeStatsOutput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPipeNodeStatsOutput1.Location = new System.Drawing.Point(0, 0);
            this.ucPipeNodeStatsOutput1.Name = "ucPipeNodeStatsOutput1";
            this.ucPipeNodeStatsOutput1.Size = new System.Drawing.Size(400, 482);
            this.ucPipeNodeStatsOutput1.TabIndex = 0;
            // 
            // ucPipeNodeStatsChart1
            // 
            this.ucPipeNodeStatsChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPipeNodeStatsChart1.Location = new System.Drawing.Point(0, 0);
            this.ucPipeNodeStatsChart1.Name = "ucPipeNodeStatsChart1";
            this.ucPipeNodeStatsChart1.Size = new System.Drawing.Size(569, 482);
            this.ucPipeNodeStatsChart1.TabIndex = 0;
            // 
            // FrmPipeNodeStatsOutput
            // 
            this.ClientSize = new System.Drawing.Size(974, 482);
            this.Controls.Add(this.splitContainerControl1);
            this.MinimizeBox = false;
            this.Name = "FrmPipeNodeStatsOutput";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "统计结果";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public void SetData(DataTable dt)
        {
            this.ucPipeNodeStatsOutput1.SetData(dt);
            this.ucPipeNodeStatsChart1.SetData(dt);
        }

        public void SetData1(DataTable dt)
        {
            this.ucPipeNodeStatsOutput1.SetData1(dt);
            this.ucPipeNodeStatsChart1.SetData1(dt);
        }
    }
}
