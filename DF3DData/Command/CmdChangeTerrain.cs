using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DData.Frm;
using DFCommon.Class;

namespace DF3DData.Command
{
    public class CmdChangeTerrain: AbstractMap3DCommand
    {

        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = (DF3DApplication)this.Hook;
            if (app == null || app.Current3DMapControl == null) return;
            FormChangeTerrain dlg = new FormChangeTerrain();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                app.Current3DMapControl.Terrain.RegisterTerrain(dlg.ConnInfo, dlg.Pwd);
                if (app.Current3DMapControl.Terrain != null && app.Current3DMapControl.Terrain.IsRegistered)
                {
                    Config.SetConfigValue("3DTerrainConnStr", dlg.ConnInfo);
                    Config.SetConfigValue("3DTerrainPwd", dlg.Pwd);
                    app.Current3DMapControl.Terrain.FlyTo(Gvitech.CityMaker.RenderControl.gviTerrainActionCode.gviFlyToTerrain);
                }
                app.Workbench.UpdateMenu();
            }
        }
    }
}
