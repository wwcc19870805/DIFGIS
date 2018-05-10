using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using DFWinForms.Service;
using ESRI.ArcGIS.Geometry;
using System.Drawing;
using DFCommon.Class;
using DF2DTool.Class;
using DF2DTool.Frm;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace DF2DTool.Command
{
    class CmdArea : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private IGraphicsContainer pGraphicsContainer;
        public static string strResult1;
        public static string strResult2;

        public override void Run(object sender, EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null || app.Workbench == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            try
            {
                if (button == 1)
                {
                    IRubberBand rubberBand = new RubberPolygonClass();
                    IGeometry pGeo = rubberBand.TrackNew(m_Display, null);
                    if (pGeo != null)
                    {
                        AddPolygonElement(pGeo, this.m_ActiveView);
                        this.m_ActiveView.Refresh();         
                    }
                    if (pGeo.IsEmpty) return;
                    Object obj = Math.Abs(double.Parse(((IArea)pGeo).Area.ToString(".##")));
                    strResult1 = obj.ToString() + "平方米";
                    strResult2 = ((IPolygon)pGeo).Length.ToString(".##") + "米";
                    app.Current2DMapControl.ActiveView.Refresh();
                    FrmArea.Instance().ShowDialog();
                }
            }
            catch
            {
            }
                                



        }


        private void AddPolygonElement(IGeometry geo, IActiveView pAV)
        {
            IElement pElem = new PolygonElement();
            pElem.Geometry = geo;


            ISimpleFillSymbol pSFSym = new SimpleFillSymbol();
            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.LineColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            pSFSym.Color = pColor;
            pSFSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;

            ISimpleLineSymbol pSLSym = new SimpleLineSymbol();
            pSLSym.Style = esriSimpleLineStyle.esriSLSSolid;
            pSLSym.Width = 1;
            pSLSym.Color = pColor;

            pSFSym.Outline = pSLSym;

            IFillShapeElement pElemFillShp = pElem as IFillShapeElement;
            pElemFillShp.Symbol = pSFSym;

            pGraphicsContainer = pAV as IGraphicsContainer;
            pGraphicsContainer.AddElement(pElem, 0);
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
            if (mapView == null) return;
            mapView.UnBind(this);
            Map2DCommandManager.Pop();

        }



    }
}
