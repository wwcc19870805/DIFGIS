using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;

namespace DF3DScan.Command
{
    class CmdStereoView : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = gviViewportMode.gviViewportStereoAnaglyph;
        }

        public override void RestoreEnv()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
        }
    }
}
