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
    public class FrmPipeLineStatsOutput : XtraForm
    {
        private SplitContainerControl splitContainerControl1;
        private UCPipeLineStatsOutput ucPipeLineStatsOutput1;
        private UCPipeLineStatsChart ucPipeLineStatsChart1;
    
        public FrmPipeLineStatsOutput()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucPipeLineStatsOutput1 = new DF3DPipe.Stats.UC.UCPipeLineStatsOutput();
            this.ucPipeLineStatsChart1 = new DF3DPipe.Stats.UC.UCPipeLineStatsChart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ucPipeLineStatsOutput1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ucPipeLineStatsChart1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(974, 482);
            this.splitContainerControl1.SplitterPosition = 401;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ucPipeLineStatsOutput1
            // 
            this.ucPipeLineStatsOutput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPipeLineStatsOutput1.Location = new System.Drawing.Point(0, 0);
            this.ucPipeLineStatsOutput1.Name = "ucPipeLineStatsOutput1";
            this.ucPipeLineStatsOutput1.Size = new System.Drawing.Size(401, 482);
            this.ucPipeLineStatsOutput1.TabIndex = 0;
            // 
            // ucPipeLineStatsChart1
            // 
            this.ucPipeLineStatsChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPipeLineStatsChart1.Location = new System.Drawing.Point(0, 0);
            this.ucPipeLineStatsChart1.Name = "ucPipeLineStatsChart1";
            this.ucPipeLineStatsChart1.Size = new System.Drawing.Size(568, 482);
            this.ucPipeLineStatsChart1.TabIndex = 0;
            // 
            // FrmPipeLineStatsOutput
            // 
            this.ClientSize = new System.Drawing.Size(974, 482);
            this.Controls.Add(this.splitContainerControl1);
            this.MinimizeBox = false;
            this.Name = "FrmPipeLineStatsOutput";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "统计结果";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        public void SetData(DataTable dt)
        {
            this.ucPipeLineStatsOutput1.SetData(dt);
            this.ucPipeLineStatsChart1.SetData(dt);
        }

        public void SetData1(DataTable dt)
        {
            this.ucPipeLineStatsOutput1.SetData1(dt);
            this.ucPipeLineStatsChart1.SetData1(dt);
        }
    }
}
