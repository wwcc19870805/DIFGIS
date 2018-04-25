using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeGeometry;
using ICSharpCode.Core;
using Gvitech.CityMaker.Controls;
using DFCommon.Class;

namespace DF3DDraw
{
    public class DrawPolygon : DrawTool
    {
        private IPolygon _polygon;
        private IRenderPoint _rPoint;
        private IRenderPolygon _rPolygon;

        public DrawPolygon()
        {
            this._geoType = DrawType.Polygon;
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
            this._3DControl.RcRButtonUp +=new Gvitech.CityMaker.Controls._IRenderControlEvents_RcRButtonUpEventHandler(_3DControl_RcRButtonUp); 
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
            this._3DControl.RcRButtonUp -=new Gvitech.CityMaker.Controls._IRenderControlEvents_RcRButtonUpEventHandler(_3DControl_RcRButtonUp); 
            this._3DControl.InteractMode = gviInteractMode.gviInteractNormal;
            this._3DControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectHover;
            this._3DControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            //Close();
        }

        private bool _3DControl_RcRButtonUp(object sender, _IRenderControlEvents_RcRButtonUpEvent e)
        {
            if ((this._polygon == null) || (this._polygon.ExteriorRing.PointCount < 3))
            {
                this.Close();
                return false;
            }
            if (!this._isFinished)
            {
                this._polygon.ExteriorRing.RemovePoints(this._polygon.ExteriorRing.PointCount - 1, 1);
                this._rPolygon.SetFdeGeometry(this._polygon);
                this._3DControl.HighlightHelper.SetRegion(this._polygon);
                this._polygon.Close();
                this._geo = this._polygon.Clone2(gviVertexAttribute.gviVertexAttributeNone);

                if (this._onFinishedDraw != null)
                {
                    this._onFinishedDraw();
                }
                this._isFinished = true;
                this._isStarted = false;
            }
            return false;
        }

        private void _3DControl_RcMouseClickSelect(object sender, _IRenderControlEvents_RcMouseClickSelectEvent e)
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
                            if (this._onStartDraw != null)
                                this._onStartDraw();
                            this._polygon = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                            this._polygon.ExteriorRing.AppendPoint(pt1);
                            IPoint pt2 = pt1.Clone() as IPoint;
                            pt2.X += 0.015;
                            this._polygon.ExteriorRing.AppendPoint(pt2);
                            _rPolygon = this._3DControl.ObjectManager.CreateRenderPolygon(this._polygon, this._surfaceSymbol, this._rootID);
                            _rPolygon.HeightStyle = this._heightStyle;

                            this._3DControl.HighlightHelper.SetRegion(this._polygon);
                            this._3DControl.HighlightHelper.VisibleMask = 1;
                            //this._polygon.ExteriorRing.Close();
                            this._isStarted = true;
                            this._isFinished = false;
                        }
                        else
                        {
                            this._polygon.ExteriorRing.AppendPoint(pt1);
                            this._polygon.ExteriorRing.Close();
                            this._rPolygon.SetFdeGeometry(this._polygon);
                            this._3DControl.HighlightHelper.SetRegion(this._polygon);
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
                            this._polygon.ExteriorRing.UpdatePoint(this._polygon.ExteriorRing.PointCount - 1, pt1);
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
