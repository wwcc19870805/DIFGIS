using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using DFCommon.Class;
using Gvitech.CityMaker.Math;

namespace DF3DScan.Command
{
    public class CmdInitView3D : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            string str = Config.GetConfigValue("InitView3D");
            bool bHave = true;
            if (string.IsNullOrEmpty(str))
            {
                bHave = false;
            }
            string[] arrstr = str.Split(';');
            if (arrstr == null || arrstr.Length != 6) bHave = false;
            else
            {
                double x, y, z, heading, tilt, roll;
                bool bRes = double.TryParse(arrstr[0], out x);
                if (!bRes) bHave = false;
                bRes = double.TryParse(arrstr[1], out y);
                if (!bRes) bHave &= false;
                bRes = double.TryParse(arrstr[2], out z);
                if (!bRes) bHave &= false;
                bRes = double.TryParse(arrstr[3], out heading);
                if (!bRes) bHave &= false;
                bRes = double.TryParse(arrstr[4], out tilt);
                if (!bRes) bHave &= false;
                bRes = double.TryParse(arrstr[5], out roll);
                if (!bRes) bHave &= false;
                if (bHave)
                {
                    IVector3 v3 = new Vector3Class();
                    IEulerAngle ang = new EulerAngle();
                    v3.Set(x, y, z);
                    ang.Set(heading, tilt, roll);
                    app.Current3DMapControl.Camera.SetCamera(v3, ang, Gvitech.CityMaker.RenderControl.gviSetCameraFlags.gviSetCameraNoFlags);
                }
            }
            if (!bHave && app.Current3DMapControl.Terrain.IsRegistered)
            {
                app.Current3DMapControl.Terrain.FlyTo(Gvitech.CityMaker.RenderControl.gviTerrainActionCode.gviFlyToTerrain);
            }
        }
    }
}
