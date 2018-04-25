using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;

namespace DF3DTool.Command
{
    public class CmdGroundDistance : AbstractMap3DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            Map3DCommandManager.Push(this);
            DF3DApplication app = (DF3DApplication)this.Hook;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractMeasurement;
            app.Current3DMapControl.MeasurementMode = gviMeasurementMode.gviMeasureGroundDistance;

        }

        public override void RestoreEnv()
        {
            DF3DApplication app = (DF3DApplication)this.Hook;
            if (app == null || app.Workbench == null) return;
            app.Workbench.BarPerformClick("Wander3D");
        }
    }
}
