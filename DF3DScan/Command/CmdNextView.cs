using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;

namespace DF3DScan.Command
{
    public class CmdNextView : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Current3DMapControl.Camera == null || app.Workbench == null) return;
            app.Current3DMapControl.Camera.Redo();
        }
    }
}
