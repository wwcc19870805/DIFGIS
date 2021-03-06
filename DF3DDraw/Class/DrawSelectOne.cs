using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.RenderControl;
using DFCommon.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.FdeCore;

namespace DF3DDraw
{
    public class DrawSelectOne : DrawTool
    {
        private int _hoverID;
        private int _curSelectedID;
        private IFeatureLayer _hoverFL;
        private IFeatureLayer _curSelectedFL;
        private uint _hightlightColor;
        private IRenderPoint _rPoint;

        public DrawSelectOne()
        {
            this._geoType = DrawType.SelectOne;
            this._hightlightColor = Convert.ToUInt32(SystemInfo.Instance.HighlightColor, 16);
        }

        public override void Close()
        {
            try
            {
                if (_rPoint != null)
                {
                    this._objectManager.DeleteObject(_rPoint.Guid);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_rPoint);
                    _rPoint = null;
                }
                if (this._curSelectedFL != null)
                {
                    this._curSelectedFL.UnhighlightFeature(this._curSelectedID);
                }
                if (this._hoverFL != null)
                {
                    this._hoverFL.UnhighlightFeature(this._hoverID);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public override void Start()
        {
            this._3DControl.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.RcMouseMove += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseMoveEventHandler(_3DControl_RcMouseMove);
            this._3DControl.InteractMode = gviInteractMode.gviInteractSelect;
            this._3DControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick ;
            this._3DControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
        }

        private bool _3DControl_RcMouseMove(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseMoveEvent e)
        {
            IPoint pt;
            IPickResult prTemp = this._3DControl.Camera.ScreenToWorld(e.x, e.y, out pt);
            if (pt != null)
            {
                if (_rPoint != null)
                {
                    this._objectManager.DeleteObject(_rPoint.Guid);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_rPoint);
                    _rPoint = null;
                }
                _rPoint = this._objectManager.CreateRenderPoint(pt, this._simplePointSymbol, this._rootID);
            }
            if (prTemp != null && prTemp.Type == gviObjectType.gviObjectFeatureLayer)
            {
                if (this._hoverFL != null && this._hoverFL != this._curSelectedFL && this._curSelectedID != this._hoverID)
                {
                    this._hoverFL.UnhighlightFeature(this._hoverID);
                }

                IFeatureLayerPickResult pr = prTemp as IFeatureLayerPickResult;
                pr.FeatureLayer.HighlightFeature(pr.FeatureId, this._hightlightColor);
                this._hoverFL = pr.FeatureLayer;
                this._hoverID = pr.FeatureId;
            }
            return false;
        }

        private void _3DControl_RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e)
        {
            if (e.intersectPoint != null && e.pickResult != null && e.pickResult.Type == gviObjectType.gviObjectFeatureLayer)
            {
                this._onStartDraw();
                if (this._curSelectedFL != null)
                {
                    this._curSelectedFL.UnhighlightFeature(this._curSelectedID);
                }
                if (this._hoverFL != null)
                {
                    this._hoverFL.UnhighlightFeature(this._hoverID);
                }
                IFeatureLayerPickResult pr = e.pickResult as IFeatureLayerPickResult;
                int featureID = pr.FeatureId;
                pr.FeatureLayer.HighlightFeature(featureID, this._hightlightColor);
                this._curSelectedFL = pr.FeatureLayer;
                this._curSelectedID = pr.FeatureId;
                this._selectFeatureLayerPickResult = pr;
                this._selectPoint = e.intersectPoint;
                this._onFinishedDraw();
            }
        }


        public override void End()
        {
            this._3DControl.RcMouseClickSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.RcMouseMove -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseMoveEventHandler(_3DControl_RcMouseMove);
            this._3DControl.InteractMode = gviInteractMode.gviInteractNormal;
            this._3DControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this._3DControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            //Close();
        }
    }
}
