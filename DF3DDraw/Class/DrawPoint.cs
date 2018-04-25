using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;
using ICSharpCode.Core;

namespace DF3DDraw
{
    public class DrawPoint : DrawTool
    {
        private IRenderPoint _rPoint;
        private IRenderPoint _rPointRes;
        public DrawPoint()
        {
            this._geoType = DrawType.Point;
        }

        public override void Close()
        {
            if (_rPoint != null)
            {
                this._objectManager.DeleteObject(_rPoint.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_rPoint);
                _rPoint = null;
            }
            if (_rPointRes != null)
            {
                this._objectManager.DeleteObject(_rPointRes.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_rPointRes);
                _rPointRes = null;
            }
        }
        public override void Start()
        {
            this._3DControl.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractSelect;
            this._3DControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            this._3DControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
        }

        public override void End()
        {
            this._3DControl.RcMouseClickSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractNormal;
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
                        this.Close();
                        if (this._onStartDraw != null) this._onStartDraw();
                        _rPointRes = this._objectManager.CreateRenderPoint(pt1, this._simplePointSymbol, this._rootID);
                        this._geo = pt1;
                        this._isStarted = false;
                        this._isFinished = true;
                        if (this._onFinishedDraw != null) this._onFinishedDraw();
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
