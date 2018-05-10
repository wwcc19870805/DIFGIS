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
using ESRI.ArcGIS.esriSystem;
using DF2DTool.Class;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF2DTool.Frm;
using System.Drawing;
using DFCommon.Class;

namespace DF2DTool.Command
{
    class CmdAzimuthAngle2 : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IActiveView m_pActiveView;
        private IMap m_FocusMap;

        private IDisplayFeedback m_pFeedback;
        private INewLineFeedback m_pLineFeed;

        private bool m_bInUse;

        public static IPoint m_pPoint;
        public static IPoint m_pAnchorPoint;
        private IPoint m_pLastPoint;
        private IPoint m_pPoint1 = new PointClass();
        private IPoint m_pPoint2 = new PointClass();

        private IGeometry pGeom;

        private IPoint downPoint; // 鼠标按下的坐标
        private IPoint movePoint; // 鼠标移动的坐标
        
        private INewLineFeedback m_pNewLineFeedback = null;

        public static string strResult1;
        public static string strResult2;
        public static string strResult3;

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
            // TODO:  添加 CalculateZimuth.OnMouseDown 实现
            
                
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
//                 else
//                 {
//                     m_pNewLineFeedback.AddPoint(downPoint);
//                 }
            }
              /* CommonFunction.DrawPointSMSSquareSymbol(m_pMapControl, downPoint);*/
        }


        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 CalculateZimuth.OnMouseMove 实现
            {
 
                if (m_pNewLineFeedback != null)
                {                  
                    movePoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                    
                    m_pNewLineFeedback.MoveTo(movePoint);
                   
                }
           }
            base.OnMouseMove(button, shift, x, y, mapX, mapY);
        }

        public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {

            {
                /*CommonFunction.DrawPointSMSSquareSymbol(m_pMapControl, movePoint);       */
                m_pNewLineFeedback = new NewLineFeedbackClass();
                IGeometry pGeomLn = m_pNewLineFeedback.Stop();
                
                if (pGeomLn != null)
                {
                   
                    AddCreateElement(pGeomLn, this.m_pMapControl.ActiveView);
                    this.m_pMapControl.ActiveView.Refresh();
                }
                m_pNewLineFeedback = null;

            }
            double dblZimuth = CommonFunction.GetAzimuth_P12(downPoint.Y, downPoint.X, movePoint.Y, movePoint.X);
            dblZimuth = CommonFunction.RadToDeg(dblZimuth);
            strResult1 = dblZimuth.ToString(".#####") + " (°) ";
            strResult2 = "X=" + downPoint.X.ToString(".##") + " " + "Y=" + downPoint.Y.ToString(".##");
            strResult3 = "X=" + movePoint.X.ToString(".##") + " " + "Y=" + movePoint.Y.ToString(".##");
            if (strResult1 != null && strResult2 != null && strResult3 != null)
            {
                //FrmAzimuthAngle.instance.SetInfo();
                FrmAzimuthAngle.instance.ShowDialog();

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
            mapView.UnBind(this);

        }

    }
}
