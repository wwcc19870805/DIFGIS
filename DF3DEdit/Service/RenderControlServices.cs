using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Base;

namespace DF3DEdit.Service
{
    public class RenderControlServices
    {
        private static RenderControlServices _RS;
        public static RenderControlServices Instance()
        {
            if (RenderControlServices._RS == null)
            {
                RenderControlServices._RS = new RenderControlServices();
            }
            return RenderControlServices._RS;
        }

        private bool isTextureEditing;
        public bool IsTextureEditing
        {
            get
            {
                return this.isTextureEditing;
            }
            set
            {
                this.isTextureEditing = value;
            }
        }

        public void SetCamera(double x, double y, double z, double heading, double tilt, double roll)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;

            if (app.Current3DMapControl.Camera.FlyTime != 3.0)
            {
                app.Current3DMapControl.Camera.FlyTime = 3.0;
            }
            app.Current3DMapControl.Viewport.ActiveView = 0;
            IVector3 vector = new Vector3Class();
            vector.Set(x, y, z);
            IEulerAngle eulerAngle = new EulerAngleClass();
            eulerAngle.Heading = heading;
            eulerAngle.Tilt = tilt;
            eulerAngle.Roll = roll;
            app.Current3DMapControl.Camera.SetCamera(vector, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
        }


    }
}
