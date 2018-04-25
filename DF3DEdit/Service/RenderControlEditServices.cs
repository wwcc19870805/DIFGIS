using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Base;
using DF3DEdit.Class;
using DF3DEdit.Delegate;
using DF3DData.Class;
using System;

namespace DF3DEdit.Service
{
    public class RenderControlEditServices
    {
        private static RenderControlEditServices _RS;
        private RenderEditorType renderEditorType;
        private event RenderEditorChangedHandle renderEditorChangedEvent;
        private event DoubleClickGeometryHandle doubleClickGeometryEvent;
        private event GeometryEditorCreateGeometryHandle geometryEditorCreateGeometryEvent;
        private event GeometryEditorStopHandle geometryEditorStopEvent;
        private event RenderEditorTypeChangedHandle renderEditorTypeChangedEvent;
        public event RenderEditorTypeChangedHandle RenderEditorTypeChangedEvent
        {
            add
            {
                this.renderEditorTypeChangedEvent += value;
            }
            remove
            {
                this.renderEditorTypeChangedEvent -= value;
            }
        }
        public event GeometryEditorStopHandle GeometryEditorStopEvent
        {
            add
            {
                this.geometryEditorStopEvent += value;
            }
            remove
            {
                this.geometryEditorStopEvent -= value;
            }
        }
        public event RenderEditorChangedHandle RenderEditorChangedEvent
        {
            add
            {
                this.renderEditorChangedEvent += value;
            }
            remove
            {
                this.renderEditorChangedEvent -= value;
            }
        }
        public event DoubleClickGeometryHandle DoubleClickGeometryEvent
        {
            add
            {
                this.doubleClickGeometryEvent += value;
            }
            remove
            {
                this.doubleClickGeometryEvent -= value;
            }
        }
        public event GeometryEditorCreateGeometryHandle GeometryEditorCreateGeometryEvent
        {
            add
            {
                this.geometryEditorCreateGeometryEvent += value;
            }
            remove
            {
                this.geometryEditorCreateGeometryEvent -= value;
            }
        }
        public RenderEditorType RenderEditorType
        {
            get
            {
                return this.renderEditorType;
            }
            set
            {
                this.renderEditorType = value;
                if (this.renderEditorTypeChangedEvent != null)
                {
                    this.renderEditorTypeChangedEvent(this.renderEditorType);
                }
            }
        }
        public static RenderControlEditServices Instance()
        {
            if (RenderControlEditServices._RS == null)
            {
                RenderControlEditServices._RS = new RenderControlEditServices();
            }
            return RenderControlEditServices._RS;
        }
        private RenderControlEditServices()
        {
            this.renderEditorType = RenderEditorType.UnKnownType;
        }
        public void StopGeometryEdit(bool bSave)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this.geometryEditorStopEvent != null && app.Current3DMapControl.ObjectEditor.IsEditing)
            {
                this.geometryEditorStopEvent(bSave);
            }
        }
        public void DoubleClickGeometry(DF3DFeatureClass fcInfo, IRowBuffer row, IFeatureLayer fl)
        {
            if (this.doubleClickGeometryEvent != null)
            {
                this.doubleClickGeometryEvent(fcInfo, row, fl);
            }
        }
        public void SetEditorPosition(HashMap rowbufferMap)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this.renderEditorChangedEvent != null)
            {
                this.renderEditorChangedEvent(rowbufferMap);
            }
            //if (!WorkSpaceServices.Instance().PropertyCanEdit)
            //{
            //    RenderControlServices.Instance().AxRenderControl.TransformHelper.Type = gviEditorType.gviEditorNone;
            //    return;
            //}
            IEnvelope selectEnvelope = SelectCollection.Instance().GetSelectEnvelope(rowbufferMap);
            if (selectEnvelope == null || !selectEnvelope.Valid())
            {
                app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
                return;
            }
            app.Current3DMapControl.TransformHelper.CrsWKT = CommonUtils.Instance().CurEditDatasetWkt;
            double x = (selectEnvelope.MinX + selectEnvelope.MaxX) / 2.0;
            double y = (selectEnvelope.MinY + selectEnvelope.MaxY) / 2.0;
            double z = (selectEnvelope.MinZ + selectEnvelope.MaxZ) / 2.0;
            IVector3 vector = new Vector3Class();
            vector.Set(x, y, z);
            switch (this.renderEditorType)
            {
                case RenderEditorType.MoveType:
                    {
                        app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
                        return;
                    }
                case RenderEditorType.RotateAllType:
                case RenderEditorType.RotateCenterType:
                    {
                        app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorRotate;
                        app.Current3DMapControl.TransformHelper.SetPosition(vector);
                        return;
                    }
                case RenderEditorType.ScaleAllType:
                case RenderEditorType.ScaleCenterType:
                    {
                        app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorScale;
                        app.Current3DMapControl.TransformHelper.SetPosition(vector);
                        return;
                    }
                case RenderEditorType.GeometryMove:
                case RenderEditorType.GeometryRoate:
                case RenderEditorType.GeometryScale:
                    {
                        app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
                        return;
                    }
                default:
                    {
                        app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
                        return;
                    }
            }
        }
    }
}
