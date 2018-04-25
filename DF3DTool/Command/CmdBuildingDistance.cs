using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;

namespace DF3DTool.Command
{
    public class CmdBuildingDistance : AbstractMap3DCommand
    {
        public override void  Run(object sender, EventArgs e)
        {
            DF3DApplication app = (DF3DApplication)this.Hook;
            if (app == null || app.Workbench == null) return;
        }

        public override void RestoreEnv()
        {
        }
    }
}
