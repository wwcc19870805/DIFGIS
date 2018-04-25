using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using DF2DCreate.Class;
using System.Windows.Forms;
using DFWinForms.Base;
using DFCommon.Class;
using System.Drawing;

namespace DF2DCreate.Command
{
    class CmdCreatePoint : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_FocusMap;

        private IElement element;
        private IPoint point;
        private ISimpleMarkerSymbol simpleMarkerSymbol;
        private IMarkerElement markerElement;
        private IGraphicsContainer pGraphicsContainer;
        private IRubberBand rubberBand = null;
        private IRgbColor pRGB;

        public override void Run(object sender, EventArgs e)
        {
            Map2DCommandManager.Push(this);            
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;

            m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;

        }



        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            {
                //控制符号的大小与不随地图缩放
                //                 rubberBand = new RubberPointClass();
                //                 point = rubberBand.TrackNew(this.m_pMapControl.ActiveView.ScreenDisplay, null) as IPoint;
                //                 IEnvelope pEnv = new EnvelopeClass();
                //                 pEnv.PutCoords(point.X, point.Y, point.X + 10, point.Y + 10);
                point = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                if (point == null) return;
                pRGB = new RgbColorClass();
                pRGB.Red = 255;
                pRGB.Green = 0;
                pRGB.Blue = 0;
                simpleMarkerSymbol = new SimpleMarkerSymbolClass();
                markerElement = new MarkerElementClass();
                element = (IElement)markerElement;
                element.Geometry = point;
                simpleMarkerSymbol.Size = SystemInfo.Instance.SymbolSize;
      
                simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                
                Color color = ColorTranslator.FromHtml(SystemInfo.Instance.LineColor);
                IColor pColor = new RgbColorClass();
                pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
                simpleMarkerSymbol.Color = pColor;           
                 markerElement.Symbol = simpleMarkerSymbol;
                 
                if (element != null)
                {
                    pGraphicsContainer = this.m_pMapControl.ActiveView as IGraphicsContainer;
                    pGraphicsContainer.AddElement(element, 0);
                    this.m_pMapControl.ActiveView.Refresh();

                }
                base.OnMouseDown(button, shift, x, y, mapX, mapY);
            }

        }
        public override void RestoreEnv()
        {
            Map2DCommandManager.Pop();
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);

        }
    }
}
