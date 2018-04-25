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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using DFWinForms.Service;
using DF2DTool.Class;
using DF2DTool.Frm;

namespace DF2DTool.Command
{
    class CmdAzimuthAngle : AbstractMap2DCommand
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
            base.OnMouseDown(button, shift, x, y, mapX, mapY);

            CalculateZimuthMouseDown(m_pAnchorPoint);

        }


        private void CalculateZimuthMouseDown(IPoint pPoint)
        {
            IGeometry pGeom = null;

            if (!m_bInUse) //如果命令没有使用
            {
                m_pPoint1 = pPoint;
                m_pLastPoint = pPoint;
                m_pRecordPointArray.Add(m_pPoint1);
                m_bInUse = true;

                m_pFeedback = new NewLineFeedbackClass();
                m_pLineFeed = (INewLineFeedback)m_pFeedback;
                m_pLineFeed.Start(pPoint);
                if (m_pFeedback != null) m_pFeedback.Display = m_pActiveView.ScreenDisplay;

                CommonFunction.DrawPointSMSSquareSymbol(m_pMapControl, m_pPoint1);
            }
            else //如果命令正在使用
            {
                m_pPoint2 = pPoint;
                m_pRecordPointArray.Add(m_pPoint2);
                

                CommonFunction.DrawPointSMSSquareSymbol(m_pMapControl, m_pPoint2);

                IPolyline pPolyline;
                pPolyline = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pRecordPointArray);
                pGeom = (IGeometry)pPolyline;
                CommonFunction.AddElement(m_pMapControl, pGeom);

                double dblZimuth = CommonFunction.GetAzimuth_P12(m_pPoint1.Y, m_pPoint1.X, m_pPoint2.Y, m_pPoint2.X);
                dblZimuth = CommonFunction.RadToDeg(dblZimuth);

                strResult1 = dblZimuth.ToString(".#####") + " (°) ";
                strResult2 = "X=" + m_pPoint1.X.ToString(".##") + "; " + "Y=" + m_pPoint1.Y.ToString(".##");
                strResult3 = "X=" + m_pPoint2.X.ToString(".##") + "; " + "Y=" + m_pPoint2.Y.ToString(".##");
                if (strResult1 != null && strResult2 != null && strResult3 != null)
                {
                    FrmAzimuthAngle frmAzimuthAngle=FrmAzimuthAngle.Instance();
                    frmAzimuthAngle.ShowDialog();

                }
                if (m_pRecordPointArray.Count != 0)
                {
                    m_pRecordPointArray.RemoveAll();
                }
                m_bInUse = false;
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 CalculateZimuth.OnMouseMove 实现
            base.OnMouseMove(button, shift, x, y, mapX, mapY);
            m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            m_pAnchorPoint = m_pPoint;
            if (!m_bInUse) return;

            m_pFeedback.MoveTo(m_pAnchorPoint);

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
