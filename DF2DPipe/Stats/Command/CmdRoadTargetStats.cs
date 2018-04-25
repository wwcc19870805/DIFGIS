using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DPipe.Stats.Frm;

namespace DF2DPipe.Stats.Command
{
    class CmdRoadTargetStats : AbstractMap2DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmEconomyStatsPolygonByDistrict2D dialog = new FrmEconomyStatsPolygonByDistrict2D("Road");
            dialog.ShowDialog();

        }
    }
}
