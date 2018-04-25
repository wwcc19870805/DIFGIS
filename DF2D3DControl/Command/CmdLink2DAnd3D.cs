using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Geometry;
using Gvitech.CityMaker.RenderControl;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF3DControl.UserControl.View;
using DF2D3DControl.Class;
using DF2DControl.Base;
using DF3DControl.Base;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.Math;

namespace DF2D3DControl.Command
{
    public class CmdLink2DAnd3D : AbstractMapCommand
    {
        private bool b3To2;
        private bool b2To3;
        private double flyTime;
        public override void Run(object sender, System.EventArgs e)
        {
            IMap2DView map2DView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (map2DView == null) return;
            bool b2DBind = map2DView.Bind(this);
            if (!b2DBind) return;

            IMap3DView map3DView = UCService.GetContent(typeof(Map3DView)) as Map3DView;
            if (map3DView == null) return;
            bool b3DBind = map3DView.Bind(this);
            if (!b3DBind) return;
            b3To2 = false;
            b2To3 = false;
            _2DLink3D();
            if (DF3DApplication.Application != null && DF3DApplication.Application.Current3DMapControl != null)
            {
                DF3DApplication.Application.Current3DMapControl.InteractMode = gviInteractMode.gviInteractNormal;
                DF3DApplication.Application.Current3DMapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                flyTime = DF3DApplication.Application.Current3DMapControl.Camera.FlyTime;
                DF3DApplication.Application.Current3DMapControl.Camera.FlyTime = 0;
            }
        }
        
        public override void RestoreEnv()
        {
            IMap2DView map2DView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (map2DView == null) return;
            map2DView.UnBind(this);

            IMap3DView map3DView = UCService.GetContent(typeof(Map3DView)) as Map3DView;
            if (map3DView == null) return;
            map3DView.UnBind(this);
            if (DF3DApplication.Application != null && DF3DApplication.Application.Current3DMapControl != null)
            {
                DF3DApplication.Application.Current3DMapControl.InteractMode = gviInteractMode.gviInteractNormal;
                DF3DApplication.Application.Current3DMapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                DF3DApplication.Application.Current3DMapControl.Camera.FlyTime = flyTime;
            }
        }

        public override void OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            _2DLink3D();
        }

        public override void RcCameraUndoRedoStatusChanged(object sender, EventArgs e)
        {
            if (DF3DApplication.Application != null && DF3DApplication.Application.Current3DMapControl != null)
            {
                IVector3 pos = new Vector3();
                IEulerAngle ang = new EulerAngleClass();
                DF3DApplication.Application.Current3DMapControl.Camera.GetCamera(out pos, out ang);
                //int width = DF3DApplication.Application.Current3DMapControl.Width;
                //int height = DF3DApplication.Application.Current3DMapControl.Height;
                //Gvitech.CityMaker.FdeGeometry.IPoint pt = null;
                //IPickResult pr = DF3DApplication.Application.Current3DMapControl.Camera.ScreenToWorld(width / 2, height / 2, out pt);
                //if (pt != null && pr != null && pr.Type != gviObjectType.gviObjectSkyBox && pr.Type != gviObjectType.gviObjectNone)
                //{
                //    if (pos.Z > pt.Z) _3DLink2D(pt.X, pt.Y, pos.Z - pt.Z);
                //}
                //double height = DF3DApplication.Application.Current3DMapControl.Terrain.GetElevation(pos.X, pos.Y, gviGetElevationType.gviGetElevationFromDatabase);
                //if (pos.Z > height) 
                    _3DLink2D(pos.X, pos.Y, pos.Z);

            }
        }

        private void _2DLink3D()
        {
            if (b3To2 || b2To3)
            {
                b3To2 = false;
                b2To3 = false;
                return;
            }
            b2To3 = true;

            DF2DApplication app2D = DF2DApplication.Application;
            DF3DApplication app3D = DF3DApplication.Application;
            if (app2D == null || app3D  == null || app2D.Current2DMapControl == null || app3D.Current3DMapControl == null) return;
            ESRI.ArcGIS.Geometry.IEnvelope env = app2D.Current2DMapControl.Extent;
            ICamera camera3D = app3D.Current3DMapControl.Camera;
            Gvitech.CityMaker.Math.IEulerAngle angle = new Gvitech.CityMaker.Math.EulerAngle();
            Gvitech.CityMaker.Math.IVector3 pos = new Gvitech.CityMaker.Math.Vector3();
            camera3D.GetCamera(out pos, out angle);

            double ox, oy, oz;
            Link2DAnd3D._2DLink3D(env.XMin, env.XMax, env.YMin, env.YMax, Math.Abs(angle.Tilt * Math.PI / 180), camera3D.VerticalFieldOfView * Math.PI / 180,  out ox, out oy, out oz);
            pos.Set(ox, oy, oz);
            camera3D.SetCamera(pos, angle, gviSetCameraFlags.gviSetCameraNoFlags);
        }

        private void _3DLink2D(double x, double y, double z)
        {
            if (b3To2 || b2To3)
            {
                b3To2 = false;
                b2To3 = false;
                return;
            }
            b3To2 = true;

            DF2DApplication app2D = DF2DApplication.Application;
            DF3DApplication app3D = DF3DApplication.Application;
            if (app2D == null || app3D == null || app2D.Current2DMapControl == null || app3D.Current3DMapControl == null) return;
            ICamera camera3D = app3D.Current3DMapControl.Camera;
            Gvitech.CityMaker.Math.IEulerAngle angle = new Gvitech.CityMaker.Math.EulerAngle();
            Gvitech.CityMaker.Math.IVector3 pos = new Gvitech.CityMaker.Math.Vector3();
            camera3D.GetCamera(out pos, out angle);

            ESRI.ArcGIS.Geometry.IEnvelope env = app2D.Current2DMapControl.Extent;
            double width = env.XMax - env.XMin;
            double height = env.YMax - env.YMin;
            double rate = width / height;

            double xmin, ymin, xmax, ymax;
            Link2DAnd3D._3DLink2D(x, y, z, Math.Abs(angle.Tilt * Math.PI / 180), camera3D.VerticalFieldOfView * Math.PI / 180, rate, out xmin, out ymin, out xmax, out ymax);
            ESRI.ArcGIS.Geometry.IEnvelope env1 = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            env1.PutCoords(xmin, ymin, xmax, ymax);
            app2D.Current2DMapControl.Extent = env1;
        }
    }
}
