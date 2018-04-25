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
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System.Drawing;
using DFCommon.Class;

namespace DF2DCreate.Command
{
    class CmdCreatePolygon: AbstractMap2DCommand
    {
        
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_FocusMap;
        private IActiveView m_pActiveView;
        private IRubberBand rubberBand = null;
        private IGraphicsContainer pGraphicsContainer;


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
            m_FocusMap = m_pMapControl.ActiveView.FocusMap;
            m_pActiveView = (IActiveView)this.m_FocusMap;
            m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;

        }
       //绘制多边形
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            {
                rubberBand = new RubberPolygonClass();
                IGeometry polygon = rubberBand.TrackNew(this.m_pMapControl.ActiveView.ScreenDisplay,null);
                if (polygon != null)
                {
                    IFillShapeElement pElemFillShp;
                    ISimpleFillSymbol pSFSym;
                    IRgbColor pRGB;
                    IElement element = new PolygonElementClass();
                    element.Geometry = polygon;
                    pElemFillShp = (IFillShapeElement)element;
                    pRGB = new RgbColorClass();
                    pRGB.Red = 255;
                    pRGB.Green = 0;
                    pRGB.Blue = 0;
                    pSFSym = new SimpleFillSymbolClass();
                    Color color = ColorTranslator.FromHtml(SystemInfo.Instance.FillColor);
                    IColor pColor = new RgbColorClass();
                    pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
                    pSFSym.Color = pColor;
                    pSFSym.Style = esriSimpleFillStyle.esriSFSSolid;
                    pElemFillShp.Symbol = pSFSym;
                    pGraphicsContainer = this.m_pMapControl.ActiveView as IGraphicsContainer;
                    pGraphicsContainer.AddElement(element,0);
                    m_pActiveView.Refresh();
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
            m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;

        }
    }
}
