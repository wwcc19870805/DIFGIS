using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using DF2DPipe.Stats.Frm;
using System.Windows.Forms;
using System.Data;

namespace DF2DPipe.Stats.Command
{
    class CmdStatsByRoad : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
         public override void Run(object sender, EventArgs e)
        {
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;
            FrmStatsByRoad2D frmStatsByRoad2D = new FrmStatsByRoad2D(this.CommandName, "Road");
            if (frmStatsByRoad2D.ShowDialog() != DialogResult.OK) return;
            DataTable dt = frmStatsByRoad2D.DTTemp;
            DataTable dtstats = frmStatsByRoad2D.DTStats;
            FrmStatsByRoadOutput2D frmStatsByRoadOutput2D = new FrmStatsByRoadOutput2D();
            frmStatsByRoadOutput2D.SetData(dt);
            frmStatsByRoadOutput2D.SetStatsData(dtstats);
            frmStatsByRoadOutput2D.Show();
        }
    }
}
