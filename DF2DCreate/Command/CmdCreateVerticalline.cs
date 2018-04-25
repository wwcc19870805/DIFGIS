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
using ESRI.ArcGIS.esriSystem;
using DF2DCreate.Class;

namespace DF2DCreate.Command
{
    class CmdCreateVerticalline : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IPoint downPoint; // 鼠标按下的坐标
        private IPoint movePoint; // 鼠标移动的坐标
        
        private INewLineFeedback m_pNewLineFeedback = null;

        private IArray m_pRecordPointArray = new ArrayClass();

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
            m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;

        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {

            {
                downPoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
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
                    m_pNewLineFeedback.Constraint = esriLineConstraints.esriLineConstraintsVertical;
                    m_pNewLineFeedback.Start(downPoint);

                }
                else
                {
                    m_pNewLineFeedback.AddPoint(downPoint);
                }
                m_pRecordPointArray.Add(downPoint);
            }
            base.OnMouseDown(button, shift, x, y, mapX, mapY);
        }


        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            {
                if (m_pNewLineFeedback != null)
                {                  
                    movePoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                    movePoint.X = downPoint.X;  
                    m_pNewLineFeedback.MoveTo(movePoint);
                }

            }
            base.OnMouseMove(button, shift, x, y, mapX, mapY);
        }


        public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {
            {
                m_pRecordPointArray.Add(movePoint);

                IPolyline pPolyline;
                pPolyline = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pRecordPointArray);
                IGeometry pGeomLn = (IGeometry)pPolyline;
              
                if (pGeomLn != null)
                {
                    AddCreateElement(pGeomLn, this.m_pMapControl.ActiveView);
                     this.m_pMapControl.ActiveView.Refresh();
                }

                if (m_pRecordPointArray.Count != 0)
                {
                    m_pRecordPointArray.RemoveAll();
                }
               
            }
            m_pNewLineFeedback = null;
            base.OnMouseUp(button, shift, x, y, mapX, mapY);
        }

//         public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
//         {
//             {
//                
//                
//                     m_pNewLineFeedback = new NewLineFeedbackClass();
//                     IGeometry pGeomLn = m_pNewLineFeedback.Stop();
// 
//                     if (pGeomLn != null)
//                     {
//                         AddCreateElement(pGeomLn, this.m_pMapControl.ActiveView);
//                        /* this.m_pMapControl.ActiveView.Refresh();*/
//                     }
// 
//                     m_pNewLineFeedback = null;
//                
// 
// 
//             }
//             base.OnDoubleClick(button, shift, x, y, mapX, mapY);
//         }

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
