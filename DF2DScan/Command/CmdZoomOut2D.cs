using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFWinForms.Service;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DF2DData.Class;
using DFDataConfig.Class;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using DevExpress.XtraBars.Docking;
using DFWinForms.Class;
using DF2DScan.UserControl;

namespace DF2DScan.Command
{
   public class CmdZoomOut2D : AbstractMap2DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;

            if (button == 1)
            {
                IScreenDisplay pDisplay = app.Current2DMapControl.ActiveView.ScreenDisplay;
                IActiveView pActiveView = app.Current2DMapControl.ActiveView;

                IRubberBand band = new RubberEnvelopeClass();
                IGeometry geo = band.TrackNew(pDisplay, null);
                if (geo != null)
                {

                    IEnvelope env = geo as IEnvelope;
                    if (env.Width < app.Current2DMapControl.FullExtent.Width && env.Height < app.Current2DMapControl.FullExtent.Height
                        && pActiveView.Extent.Width < app.Current2DMapControl.FullExtent.Width && pActiveView.Extent.Height < app.Current2DMapControl.FullExtent.Height)
                    {
                        double widthRadio = pActiveView.Extent.Width / env.Width;
                        double hightRadio = pActiveView.Extent.Height / env.Height;
                        double multiple = widthRadio > hightRadio ? widthRadio : hightRadio;
                        IEnvelope extentEnv = pActiveView.Extent;
                        IPoint centerPoint = new PointClass();
                        centerPoint.PutCoords((env.XMax + env.XMin) / 2, (env.YMax + env.YMin) / 2);
                        extentEnv.CenterAt(centerPoint);
                        extentEnv.Expand(multiple, multiple, true);
                        pActiveView.Extent = extentEnv;

                    }
                    else
                    {
                        pActiveView.Extent = app.Current2DMapControl.FullExtent;
                    }

                    pActiveView.Refresh();
                }
            }
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            Map2DCommandManager.Pop();
        }
    }
}
