using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System.Drawing;
using DFCommon.Class;

namespace DF2DCreate.Command
{
    class CmdCreateCircle : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
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
  
        }

      #region 基类操作

      public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
      {
          {
              rubberBand = new RubberCircleClass();              
             IGeometry polygon = rubberBand.TrackNew(this.m_pMapControl.ActiveView.ScreenDisplay, null);
              if (polygon != null)
              {
                  AddCreateElement(polygon, this.m_pMapControl.ActiveView);
                  this.m_pMapControl.ActiveView.Refresh();
              }
          }
          base.OnMouseDown(button, shift, x, y, mapX, mapY);
      }

      #endregion
      public void AddCreateElement(IGeometry pCircularArc, IActiveView pAV)
          
      {
         IFillShapeElement pElemFillShp;
          IElement pElem;
          ISimpleFillSymbol pSFSym;
         ISegmentCollection pSegColl = new PolygonClass();
          object missing    = Type.Missing;
          ISegment segement = (ISegment)pCircularArc;          
          pSegColl.AddSegment(segement, missing, missing);
          pElem = new CircleElementClass();
          pElem.Geometry = (IGeometry)pSegColl;
          pElemFillShp = (IFillShapeElement)pElem;
          pSFSym = new SimpleFillSymbolClass();
          Color color = ColorTranslator.FromHtml(SystemInfo.Instance.FillColor);
          IColor pColor = new RgbColorClass();
          pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
          pSFSym.Color = pColor;
          pSFSym.Style = esriSimpleFillStyle.esriSFSSolid;
          pElemFillShp.Symbol = pSFSym;
          pGraphicsContainer = pAV as IGraphicsContainer;
          pGraphicsContainer.AddElement(pElem, 0);
         
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
