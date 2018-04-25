using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DCreate.Class
{
    class CurrentTool
    {
        public enum CurrentToolName
        {
            none, selectFeature, drawPoint, drawLine, drawBezier, drawCircleCentRad, drawCircle3P, drawCircle2P, drawRectSide2P,
            drawRectRelative2P, drawParallelLine, drawArc3P, drawArcCenterFromTo, drawArcCenRadFATA, drawPolyline, drawTraceLine,
            modifyLinePolygonConvert, modifyPropertyMatch, modifyReverseOrientation, modifyPolylineToBezierCurve, modifyBezierCurveToPolyline,
            modifySplit, modifyCutPolyline, modifyExtendPolyline, modifyBreakPolyline,
            modifyDelVertex, modifyAddVertex, modifyMoveVertex, modifyMirror, modifyMove, modifyRotate, modifyOffset,
            modifyExplodeMulti_part, modifyUnion, modifyJoin, modifyIntersection, modifyDifference,
            modifyBuffer, modifySmooth, modifyGeneralize, modifyExtractSpecifyFeatures,
            CalculateCurveParameter, CalculateDistance, CalculateArea, CalculateZimuth, CalculateCorner, ModifyPartPolylineToBezierCurve, ModifyPolygonOrPolylineToPoint
        };

        //成员变量定义	
        public static CurrentToolName m_CurrentToolName = CurrentToolName.none;

        public CurrentTool()
        {

        }

    }
}
