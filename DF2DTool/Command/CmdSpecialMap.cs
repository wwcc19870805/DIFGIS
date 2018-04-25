using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using DF2DTool.Frm;

namespace DF2DTool.Command
{
    public class CmdSpecialMap : AbstractMap2DCommand
    {
        
        public override void Run(object sender, System.EventArgs e)
        {
            DF2DApplication app = DF2DApplication.Application;
            IMap map = app.Current2DMapControl.Map;
            FrmSpecialMap dialog = new FrmSpecialMap(map);
            dialog.ShowDialog();
        }
    }
}
