using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Base;
using Gvitech.CityMaker.Math;
using DFCommon.Class;

namespace DF3DControl.Command
{
    public class SaveInitView3DCommand: AbstractMap3DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            DF3DApplication app = (DF3DApplication)this.Hook;
            if (app == null || app.Current3DMapControl == null) return;
            IVector3 v3 = new Vector3Class();
            IEulerAngle ang = new EulerAngle();
            app.Current3DMapControl.Camera.GetCamera(out v3, out ang);
            string str = v3.X + ";" + v3.Y + ";" + v3.Z + ";" + ang.Heading + ";" + ang.Tilt + ";" + ang.Roll;
            Config.SetConfigValue("InitView3D", str);

        }
    }
}
