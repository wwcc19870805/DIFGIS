using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using System.Drawing;
using DFCommon.Class;

namespace DF2DCreate.Command
{
    class CmdCreateRectangle : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;

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
        }
        #region 基类操作

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {

            {
                IEnvelope envelope = this.m_pMapControl.TrackRectangle();
                if (envelope == null) return;
                IRectangleElement rectangleElement = new RectangleElementClass();
               IElement element = (IElement)rectangleElement;
                element.Geometry = envelope as IGeometry;
                ISimpleFillSymbol pSFSym;                
                IFillShapeElement pElemFillShp;
                pElemFillShp = (IFillShapeElement)element;               
                pSFSym = new SimpleFillSymbolClass();
                Color color = ColorTranslator.FromHtml(SystemInfo.Instance.FillColor);
                IColor pColor = new RgbColorClass();
                pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
                pSFSym.Color = pColor;
                pSFSym.Style = esriSimpleFillStyle.esriSFSSolid;
                pElemFillShp.Symbol = pSFSym;

                this.m_pMapControl.ActiveView.GraphicsContainer.AddElement(element, 0);
                this.m_pMapControl.ActiveView.Refresh();
            }
            base.OnMouseDown(button, shift, x, y, mapX, mapY);
        }

        #endregion
        public override void RestoreEnv()
        {

            Map2DCommandManager.Pop();
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);

        }
    }
}
