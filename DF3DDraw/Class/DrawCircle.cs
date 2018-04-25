using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using ICSharpCode.Core;
using DFCommon.Class;

namespace DF3DDraw
{
    public class DrawCircle : DrawTool
    {
        private IPoint startPoint;
        private IRenderPolyline _rPolyline;
        private ILabel _label;
        private IPolyline _polyline;
        private IRenderPoint _rPoint;
        private IRenderPolygon _rPolygon;

        public DrawCircle()
        {
            this._geoType = DrawType.Circle;
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
                this._3DControl.ObjectManager.DeleteObject(_rPolyline.Guid);
                _rPolyline = null;
            }
            if (_label != null)
            {
                this._3DControl.ObjectManager.DeleteObject(_label.Guid);
                _label = null;
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
                            if (_polyline != null)
                            {
                                _polyline = null;
                            }
                            startPoint = pt1;
                            double radius = 0.0000001;
                            this._geo = (startPoint as ITopologicalOperator2D).Buffer2D(radius, gviBufferStyle.gviBufferCapround);
                            _rPolygon = this._3DControl.ObjectManager.CreateRenderPolygon(this._geo as IPolygon, this._surfaceSymbol, this._rootID);
                            _rPolygon.HeightStyle = this._heightStyle;

                            this._3DControl.HighlightHelper.SetRegion(this._geo);
                            this._3DControl.HighlightHelper.VisibleMask = 1;

                            _polyline = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                            _polyline.AppendPoint(pt1);
                            IPoint pt2 = pt1.Clone() as IPoint;
                            pt2.X += 0.000001;
                            _polyline.AppendPoint(pt2);
                            _rPolyline = this._3DControl.ObjectManager.CreateRenderPolyline(_polyline, this._curveSymbol, this._3DControl.ProjectTree.RootID);
                            _label = this._3DControl.ObjectManager.CreateLabel(this._3DControl.ProjectTree.RootID);
                            _label.Position = _polyline.Midpoint;
                            _label.Text = _polyline.Length.ToString("0.00") + " 米";
                            ITextSymbol ts =new TextSymbolClass();
                            TextAttribute ta = new TextAttribute();
                            ta.TextColor = Convert.ToUInt32(SystemInfo.Instance.TextColor, 16);
                            ta.TextSize = SystemInfo.Instance.TextSize;
                            ts.TextAttribute = ta;
                            _label.TextSymbol = ts;
                            this._isStarted = true;
                            this._isFinished = false;
                        }
                        else
                        {
                            //this.End();
                            this._isStarted = false;
                            this._isFinished = true;

                            _polyline.UpdatePoint(1, pt1);
                            _rPolyline.SetFdeGeometry(_polyline);

                            _label.Position = _polyline.Midpoint;
                            _label.Text = _polyline.Length.ToString("0.00") + " 米";


                            double radius = Math.Sqrt((startPoint.X - pt1.X) * (startPoint.X - pt1.X)
                                + (startPoint.Y - pt1.Y) * (startPoint.Y - pt1.Y) + (startPoint.Z - pt1.Z) * (startPoint.Z - pt1.Z));
                            this._geo = (startPoint as ITopologicalOperator2D).Buffer2D(radius, gviBufferStyle.gviBufferCapround);
                            _rPolygon.SetFdeGeometry(this._geo);
                            this._3DControl.HighlightHelper.SetRegion(this._geo);
                            this._geo = this._geo.Clone2(gviVertexAttribute.gviVertexAttributeNone);
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

                            _polyline.UpdatePoint(1, pt1);
                            _rPolyline.SetFdeGeometry(_polyline);

                            _label.Position = _polyline.Midpoint;
                            _label.Text = _polyline.Length.ToString("0.00") + " 米";

                            double radius = Math.Sqrt((startPoint.X - pt1.X) * (startPoint.X - pt1.X)
                                + (startPoint.Y - pt1.Y) * (startPoint.Y - pt1.Y) + (startPoint.Z - pt1.Z) * (startPoint.Z - pt1.Z));
                            this._geo = (startPoint as ITopologicalOperator2D).Buffer2D(radius, gviBufferStyle.gviBufferCapround);
                            _rPolygon.SetFdeGeometry(this._geo);
                            this._3DControl.HighlightHelper.SetRegion(this._geo);
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
