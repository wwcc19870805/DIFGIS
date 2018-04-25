using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using DFWinForms.Service;
using System.Drawing;
using DFCommon.Class;
using DevExpress.XtraEditors;
using DF2DCreate.Class;
using ESRI.ArcGIS.esriSystem;

namespace DF2DCreate.Command
{
    class CmdCreateVerticalline2D : AbstractMap2DCommand
    {
        private IMapControlEvents2_Event mapControlEvents = null;
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IPoint downPoint; // 鼠标按下的坐标
        private IPoint movePoint; // 鼠标移动的坐标
        private int ptCount = 0;
        private INewLineFeedback m_pNewLineFeedback;
        private IMap2DCommand map2DCommand;
        public override void Run(object sender, EventArgs e)
        {
            Map2DCommandManager.Push(this);
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;

            //bool bBind = mapView.Bind(this);
            //if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;
            m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
            map2DCommand = this as IMap2DCommand;
            mapControlEvents = m_pMapControl as IMapControlEvents2_Event;
            mapControlEvents.OnMouseDown += new IMapControlEvents2_OnMouseDownEventHandler(map2DCommand.OnMouseDown);
            mapControlEvents.OnMouseMove += new IMapControlEvents2_OnMouseMoveEventHandler(map2DCommand.OnMouseMove);
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            downPoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (button == 1 && ptCount == 0 && m_pNewLineFeedback == null)
            {
                ISimpleLineSymbol pSLnSym;
                m_pNewLineFeedback = new NewLineFeedbackClass();
                pSLnSym = (ISimpleLineSymbol)m_pNewLineFeedback.Symbol;
                Color color = ColorTranslator.FromHtml(SystemInfo.Instance.LineColor);
                IColor pColor = new RgbColorClass();
                pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
                pSLnSym.Color = pColor;
                pSLnSym.Style = esriSimpleLineStyle.esriSLSSolid;

                m_pNewLineFeedback.Display = this.m_pMapControl.ActiveView.ScreenDisplay;
                m_pNewLineFeedback.Constraint = esriLineConstraints.esriLineConstraintsVertical;
                m_pNewLineFeedback.Start(downPoint);
                ptCount++;
                mapControlEvents.OnMouseDown -= new IMapControlEvents2_OnMouseDownEventHandler(map2DCommand.OnMouseDown);
                mapControlEvents.OnMouseUp += new IMapControlEvents2_OnMouseUpEventHandler(map2DCommand.OnMouseUp);
            }
            else if (button == 1 && m_pNewLineFeedback != null && ptCount > 0)
            {
                m_pNewLineFeedback.AddPoint(downPoint);
                ptCount++;



            }


        }
        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            if (m_pNewLineFeedback != null)
            {
                movePoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                movePoint.X = downPoint.X;

                m_pNewLineFeedback.MoveTo(movePoint);

            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {
            if (m_pNewLineFeedback == null) return;
            if (ptCount >= 1)
            {
                m_pNewLineFeedback.AddPoint(movePoint);
                IPolyline line = m_pNewLineFeedback.Stop();
                ptCount = 0;
                m_pNewLineFeedback = null;
                mapControlEvents.OnMouseDown += new IMapControlEvents2_OnMouseDownEventHandler(map2DCommand.OnMouseDown);
                mapControlEvents.OnMouseUp -= new IMapControlEvents2_OnMouseUpEventHandler(map2DCommand.OnMouseUp);
                AddCreateElement(line, this.m_pMapControl.ActiveView);
                this.m_pMapControl.ActiveView.Refresh();

            }

        }
        public void AddCreateElement(IGeometry pGeomLine, IActiveView pAV)
        {
            ILineElement pElemLine;
            IElement pElem;
            IGraphicsContainer pGraCont;
            ISimpleLineSymbol pSLnSym;
            pElem = new LineElementClass();
            pElem.Geometry = pGeomLine;
            pElemLine = (ILineElement)pElem;
            pSLnSym = new SimpleLineSymbolClass();
            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.LineColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            pSLnSym.Color = pColor;
            pSLnSym.Style = esriSimpleLineStyle.esriSLSSolid;
            pElemLine.Symbol = pSLnSym;
            pGraCont = (IGraphicsContainer)pAV;
            pGraCont.AddElement(pElem, 0);

        }



        public override void RestoreEnv()
        {
            Map2DCommandManager.Pop();
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapControlEvents.OnMouseDown -= new IMapControlEvents2_OnMouseDownEventHandler(map2DCommand.OnMouseDown);
            mapControlEvents.OnMouseUp -= new IMapControlEvents2_OnMouseUpEventHandler(map2DCommand.OnMouseUp);
            mapControlEvents.OnMouseMove -= new IMapControlEvents2_OnMouseMoveEventHandler(map2DCommand.OnMouseMove);
            //mapView.UnBind(this);
            m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }
    }
}
