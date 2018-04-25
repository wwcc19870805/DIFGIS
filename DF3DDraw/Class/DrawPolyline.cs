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
    public class DrawPolyline : DrawTool
    {
        private IPolyline _polyline;
        private IRenderPolyline _rPolyline;
        private IRenderPoint _rPoint;
        public DrawPolyline()
        {
            this._geoType = DrawType.Polyline;
        }

        public override void Close()
        {
            if (_rPoint != null)
            {
                this._objectManager.DeleteObject(_rPoint.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_rPoint);
                _rPoint = null;
            }
            if (_rPolyline != null)
            {
                this._objectManager.DeleteObject(_rPolyline.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_rPolyline);
                _rPolyline = null;
            }
            if (_polyline != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_polyline);
                _polyline = null;
            }
        }

        public override void Start()
        {
            this._3DControl.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.RcRButtonUp +=new Gvitech.CityMaker.Controls._IRenderControlEvents_RcRButtonUpEventHandler(_3DControl_RcRButtonUp); 
            this._3DControl.InteractMode = gviInteractMode.gviInteractSelect;
            this._3DControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            this._3DControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
        }

        private bool _3DControl_RcRButtonUp(object sender, _IRenderControlEvents_RcRButtonUpEvent e)
        {
            if ((this._polyline == null) || (this._polyline.PointCount < 3))
            {
                this.Close();
                return false;
            }
            if (!this._isFinished)
            {
                this._polyline.RemovePoints(this._polyline.PointCount - 1, 1);
                this._rPolyline.SetFdeGeometry(this._polyline);
                this._geo = this._polyline;
                if (this._onFinishedDraw != null)
                {
                    this._onFinishedDraw();
                } 
                this._isFinished = true;
                this._isStarted = false;
            }
            return false;
        }

        
        public override void End()
        {
            this._3DControl.RcMouseClickSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.RcRButtonUp -=new Gvitech.CityMaker.Controls._IRenderControlEvents_RcRButtonUpEventHandler(_3DControl_RcRButtonUp);
            this._3DControl.InteractMode = gviInteractMode.gviInteractNormal;
            this._3DControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this._3DControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll; 
            //Close();
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
                            this._polyline = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                            this._rPolyline = this._objectManager.CreateRenderPolyline(this._polyline, this._curveSymbol, this._rootID);
                            this._polyline.AppendPoint(pt1);
                            IPoint pt2 = pt1.Clone() as IPoint;
                            pt2.X += 0.000001;
                            this._polyline.AppendPoint(pt2);
                            this._isStarted = true;
                            this._isFinished = false;
                        }
                        else
                        {
                            this._polyline.AppendPoint(pt1);
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
                            this._polyline.UpdatePoint(this._polyline.PointCount - 1, pt1);
                            this._rPolyline.SetFdeGeometry(this._polyline);
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
