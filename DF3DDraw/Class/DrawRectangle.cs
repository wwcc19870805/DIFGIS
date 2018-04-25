using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using ICSharpCode.Core;
using DFCommon.Class;
using Gvitech.CityMaker.Math;

namespace DF3DDraw
{
    public class DrawRectangle:DrawTool
    {
        private IPolygon _polygon;
        private IRenderPoint _rPoint;
        private IRenderPolygon _rPolygon;

        public DrawRectangle()
        {
            this._geoType = DrawType.Rectangle;
        }

        public override void Close()
        {
            if (_rPoint != null)
            {
                this._objectManager.DeleteObject(_rPoint.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_rPoint);
                _rPoint = null;
            }
            if (_polygon != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_polygon);
                _polygon = null;
            }
            if (_rPolygon != null)
            {
                this._3DControl.ObjectManager.DeleteObject(_rPolygon.Guid);
                _rPolygon = null;
            }
            this._3DControl.HighlightHelper.VisibleMask = 0;
            this._3DControl.HighlightHelper.SetRegion(null);
        }

        public override void Start()
        {
            this._3DControl.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.InteractMode = gviInteractMode.gviInteractSelect;
            this._3DControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            this._3DControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this._3DControl.HighlightHelper.SetRegion(null);
            this._3DControl.HighlightHelper.Color = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
            this._3DControl.HighlightHelper.MinZ = -10000;
        }

        public override void End()
        {
            this._3DControl.RcMouseClickSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.InteractMode = gviInteractMode.gviInteractNormal;
            this._3DControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this._3DControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            //Close();
        }

        private void _3DControl_RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e)
        {
            try
            {
                if (e.intersectPoint != null)
                {
                    IPoint pt1 = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                    pt1.X = e.intersectPoint.X;
                    pt1.Y = e.intersectPoint.Y;
                    pt1.Z = e.intersectPoint.Z;
                    if (e.eventSender == gviMouseSelectMode.gviMouseSelectClick)
                    {
                        if (!this._isStarted)
                        {
                            this.Close();
                            if (this._onStartDraw != null) this._onStartDraw();
                            IPoint pt2 = pt1.Clone() as IPoint;
                            pt2.Y -= 0.0015;
                            IPoint pt3 = pt1.Clone() as IPoint;
                            pt3.X += 0.0015;
                            pt3.Y -= 0.0015;
                            IPoint pt4 = pt1.Clone() as IPoint;
                            pt4.X += 0.0015;
                            this._polygon = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                            this._polygon.ExteriorRing.AppendPoint(pt1);
                            this._polygon.ExteriorRing.AppendPoint(pt2);
                            this._polygon.ExteriorRing.AppendPoint(pt3);
                            this._polygon.ExteriorRing.AppendPoint(pt4);
                            _rPolygon = this._3DControl.ObjectManager.CreateRenderPolygon(this._polygon, this._surfaceSymbol, this._rootID);
                            _rPolygon.HeightStyle = this._heightStyle;
                            this._3DControl.HighlightHelper.SetRegion(this._polygon);
                            this._3DControl.HighlightHelper.VisibleMask = 1;
                            this._isStarted = true;
                            this._isFinished = false;
                        }
                        else
                        {
                            //this.End();
                            this._isStarted = false;
                            this._isFinished = true;
                            IPoint startPt = this._polygon.ExteriorRing.GetPoint(0);
                            IPoint pt2 = startPt.Clone() as IPoint;
                            //IEulerAngle ang = this._3DControl.Camera.GetAimingAngles2(startPt, pt2);
                            //this._3DControl.Camera.GetAimingPoint2()

                            pt2.Y = pt1.Y;
                            this._polygon.ExteriorRing.UpdatePoint(1, pt2);
                            this._polygon.ExteriorRing.UpdatePoint(2, pt1);
                            IPoint pt4 = startPt.Clone() as IPoint;
                            pt4.X = pt1.X;
                            this._polygon.ExteriorRing.UpdatePoint(3, pt4);
                            this._rPolygon.SetFdeGeometry(this._polygon);
                            this._3DControl.HighlightHelper.SetRegion(this._polygon);
                            this._polygon.Close();
                            this._geo = this._polygon.Clone2(gviVertexAttribute.gviVertexAttributeNone);
                            if (this._onFinishedDraw != null) this._onFinishedDraw();
                        }
                    }
                    else if (e.eventSender == gviMouseSelectMode.gviMouseSelectMove)
                    {
                        if (_rPoint != null)
                        {
                            this._objectManager.DeleteObject(_rPoint.Guid);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(_rPoint);
                            _rPoint = null;
                        }
                        _rPoint = this._objectManager.CreateRenderPoint(pt1, this._simplePointSymbol, this._rootID);
                        if (this._isStarted)
                        {
                            IPoint startPt = this._polygon.ExteriorRing.GetPoint(0);
                            IPoint pt2 = startPt.Clone() as IPoint;
                            pt2.Y = pt1.Y;
                            this._polygon.ExteriorRing.UpdatePoint(1, pt2);
                            this._polygon.ExteriorRing.UpdatePoint(2, pt1);
                            IPoint pt4 = startPt.Clone() as IPoint;
                            pt4.X = pt1.X;
                            this._polygon.ExteriorRing.UpdatePoint(3, pt4);
                            this._rPolygon.SetFdeGeometry(this._polygon);
                            this._3DControl.HighlightHelper.SetRegion(this._polygon);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }

        }

    }
}
