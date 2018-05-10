using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using System.Collections;
using DFWinForms.Service;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Drawing;
using DFCommon.Class;
using ESRI.ArcGIS.esriSystem;
using DF2DTool.Class;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF2DTool.Frm;

namespace DF2DTool.Command
{
    class CmdCoordinate : AbstractMap2DCommand
    {

        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IActiveView m_pActiveView;
        private IMap m_FocusMap;

        DialogResult result;

        private IElement element;
        private IPoint point;
        private ISimpleMarkerSymbol simpleMarkerSymbol;
        private IMarkerElement markerElement;
        private IGraphicsContainer pGraphicsContainer;
        private IRubberBand rubberBand = null;
        private IRgbColor pRGB;

        public static IPoint m_pAnchorPoint = new PointClass();

        private IDisplayFeedback m_pFeedback;
        private INewLineFeedback m_pLineFeed;

        public static string Result;

        private bool m_bInUse;
        private IArray m_pRecordPointArray = new ArrayClass();

        const string CRLF = "\r\n";


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

        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            m_pAnchorPoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (m_pAnchorPoint == null) return;
            simpleMarkerSymbol = new SimpleMarkerSymbolClass();
            markerElement = new MarkerElementClass();
            element = (IElement)markerElement;
            element.Geometry = m_pAnchorPoint;
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
            Result = "X=" + m_pAnchorPoint.X.ToString(".##") + "; " + "Y=" + m_pAnchorPoint.Y.ToString(".##");
            FrmCoordinate.Instance().ShowDialog();

 
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

