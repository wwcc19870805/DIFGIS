using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using DFWinForms.Service;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System.Drawing;
using DFCommon.Class;
using DF2DTool.Frm;
using DF2DTool.Class;
using DevExpress.XtraEditors;

namespace DF2DTool.Command
{
    class CmdDistance : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IActiveView m_pActiveView;
        private IMap m_FocusMap;

        private INewLineFeedback m_pNewLineFeedback = null;

        private  IPoint m_pAnchorPoint;        
        private IPoint m_pLastPoint;

        private double m_dblDistance;

        public static string strResult;

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

            {
                m_pAnchorPoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                if (m_pNewLineFeedback == null)
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
                    m_pNewLineFeedback.Start(m_pAnchorPoint);

                }
                else
                {
                    m_pNewLineFeedback.AddPoint(m_pAnchorPoint);
                }
            }
            base.OnMouseDown(button, shift, x, y, mapX, mapY);




        }


         public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
         {
             {
                 if (m_pNewLineFeedback != null)
                 {
                     m_pLastPoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

                     m_pNewLineFeedback.MoveTo(m_pLastPoint);
                     m_dblDistance = CommonFunction.GetDistance_P12(m_pAnchorPoint, m_pLastPoint);
                 }
                 
             }
             base.OnMouseMove(button, shift, x, y, mapX, mapY);
         }


         public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
         {
             {

                /* m_pNewLineFeedback = new NewLineFeedbackClass();*/
                 IGeometry pGeomLn = m_pNewLineFeedback.Stop();
                 
                 if (pGeomLn != null)
                 {
                     AddCreateElement(pGeomLn, this.m_pMapControl.ActiveView);
                     this.m_pMapControl.ActiveView.Refresh();
                     strResult = m_dblDistance.ToString(".##") + "米";

                     if (strResult != null)
                     {
                         FrmDistance frmDistance = FrmDistance.Instance();
                         frmDistance.ShowDialog();

                         
                     }
                     
                 }

                 m_pNewLineFeedback = null;
             }

             
             base.OnDoubleClick(button, shift, x, y, mapX, mapY);

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
             mapView.UnBind(this);

         }


    }
}
