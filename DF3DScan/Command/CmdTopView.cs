using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;

namespace DF3DScan.Command
{
    class CmdTopView : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            Gvitech.CityMaker.Math.IVector3 vect = new Gvitech.CityMaker.Math.Vector3();
            Gvitech.CityMaker.Math.IEulerAngle angle = new Gvitech.CityMaker.Math.EulerAngle();
            app.Current3DMapControl.Camera.GetCamera(out vect, out angle);
            angle.Set(angle.Heading, -90, angle.Roll);
            app.Current3DMapControl.Camera.SetCamera(vect, angle, Gvitech.CityMaker.RenderControl.gviSetCameraFlags.gviSetCameraNoFlags);
        }
    }
}
