using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using DevExpress.XtraBars;

namespace DF3DScan.Command
{
    public class CmdHideElevation : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (app.Current3DMapControl.Terrain.IsRegistered)
            {
                app.Current3DMapControl.Terrain.DemAvailable = false;
            }
        }

        public override void RestoreEnv()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (app.Current3DMapControl.Terrain.IsRegistered)
            {
                app.Current3DMapControl.Terrain.DemAvailable = true;
            }
        }
    }
}
