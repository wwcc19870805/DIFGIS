using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;

namespace DF3DScan.Command
{
    public class CmdHeavyRain : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            for (int i = 0; i < 4; i++) app.Current3DMapControl.ObjectManager.GetSkyBox(i).Weather = Gvitech.CityMaker.RenderControl.gviWeatherType.gviWeatherHeavyRain;
        }
    }
}
