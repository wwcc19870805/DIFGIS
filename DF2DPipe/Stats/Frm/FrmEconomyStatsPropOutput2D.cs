using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DF2DPipe.Stats.UC;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using DF2DPipe.Class;

namespace DF2DPipe.Stats.Frm
{
    public partial class FrmEconomyStatsPropOutput2D : XtraForm
    {
        private UCEconomyStatsPropOutput2D ucStatsOutput1;
        private string _sysFieldName;
        private string _disName;
        private Dictionary<string, List<IFeature>> _dict;
        public FrmEconomyStatsPropOutput2D()
        {
            InitializeComponent();
        }
        public FrmEconomyStatsPropOutput2D(string sysFieldName,string disName,Dictionary<string,List<IFeature>> dict)
        {
            this._sysFieldName = sysFieldName;
            this._disName = disName;
            this._dict = dict;
            InitializeComponent();
        }
     
        
        private void InitializeComponent()
        {
            this.ucStatsOutput1 = new DF2DPipe.Stats.UC.UCEconomyStatsPropOutput2D();
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
            // FrmEconomyStatsPropOutput2D
            // 
            this.ClientSize = new System.Drawing.Size(597, 457);
            this.Controls.Add(this.ucStatsOutput1);
            this.MinimizeBox = false;
            this.Name = "FrmEconomyStatsPropOutput2D";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "区域属性信息";
            this.Load += new System.EventHandler(this.FrmEconomyStatsPropOutput2D_Load);
            this.ResumeLayout(false);

        }

        private void FrmEconomyStatsPropOutput2D_Load(object sender, EventArgs e)
        {
            ShowProperty showProp = new ShowProperty(_sysFieldName, _disName, _dict);
            this.ucStatsOutput1.SetData(showProp.GetDataTableByDistrictName());
            
        }

      
        
        

        
    }
}
