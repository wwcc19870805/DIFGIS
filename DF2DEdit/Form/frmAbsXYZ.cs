using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace DF2DEdit.Form
{
    public partial class frmAbsXYZ : XtraForm
    {
        public static IPoint m_pPoint;

        public frmAbsXYZ()
        {
            InitializeComponent();
        }

        public IPoint Point
        {
            get
            {
                return null;
            }
            set
            {
                m_pPoint = value;
            }
        }

        private void frmAbsXYZ_Load(object sender, EventArgs e)
        {
            //txtX.Text = m_pPoint.X.ToString(".###");
            //txtY.Text = m_pPoint.Y.ToString(".###");

            //switch (CurrentTool.m_CurrentToolName)
            //{
            //    case CurrentTool.CurrentToolName.drawPoint:
            //        DrawPoint.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawLine:
            //        DrawLine.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawBezier:
            //        DrawBezierCurve.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawParallelLine:
            //        DrawParallelLine.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawPolyline:
            //        DrawPolyline.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawRectSide2P:
            //        DrawRectSide2P.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawRectRelative2P:
            //        DrawRectRelative2P.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawArc3P:
            //        DrawArc3P.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawArcCenterFromTo:
            //        DrawArcCenterFromTo.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawCircle2P:
            //        DrawCircle2P.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawCircle3P:
            //        DrawCircle3P.m_bInputWindowCancel = true;
            //        break;
            //    case CurrentTool.CurrentToolName.drawCircleCentRad:
            //        DrawCircleCentRad.m_bInputWindowCancel = true;
            //        break;
            //    default:
            //        break;
            //}  

        } 

    }
}
