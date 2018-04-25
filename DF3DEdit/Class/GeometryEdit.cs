using System;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Delegate;
using DF3DEdit.Service;

namespace DF3DEdit.Class
{
    public class GeometryEdit
    {
        private DF3DApplication _app;
        private static GeometryEdit _RGE;
        private DF3DFeatureClass _FeatureClass;
        private Gvitech.CityMaker.FdeCore.IRowBuffer _row;
        private gviInteractMode _InteractaMode;
        private gviGeoEditType _geoEditType;
        private IRenderGeometry _renderGeometry;
        private string curGeoField = "";
        private IObjectEditor _geoEditor;
        private bool _isSpatialQuery;
        private event SpatialQueryEditStopHandle spatialQueryEditStopEvent;
        public event SpatialQueryEditStopHandle SpatialQueryEditStopEvent
        {
            add
            {
                this.spatialQueryEditStopEvent += value;
            }
            remove
            {
                this.spatialQueryEditStopEvent -= value;
            }
        }
        public void GeometryEditStart(IRenderGeometry renderGeo)
        {
            if (this._app == null || this._app.Current3DMapControl == null) return;
            this._isSpatialQuery = true;
            this._InteractaMode = this._app.Current3DMapControl.InteractMode;
            this._app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractEdit;
            this._geoEditor.StartEditRenderGeometry(renderGeo, gviGeoEditType.gviGeoEditCreator);
        }
        private GeometryEdit()
        {
            this._app = DF3DApplication.Application;
            if (this._app != null && this._app.Current3DMapControl != null)
            {
                this._geoEditor = this._app.Current3DMapControl.ObjectEditor;
                this._app.Current3DMapControl.RcObjectEditFinish += new System.EventHandler(this.AxRenderControl_RcObjectEditFinish);
                this._app.Current3DMapControl.RcObjectEditing += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEventHandler(this.AxRenderControl_RcObjectEditing);
            }
        }
        public static GeometryEdit Instance()
        {
            if (GeometryEdit._RGE == null)
            {
                GeometryEdit._RGE = new GeometryEdit();
            }
            return GeometryEdit._RGE;
        }
        public void GeometryEditStart(DF3DFeatureClass fcInfo, Gvitech.CityMaker.FdeCore.IRowBuffer row, gviGeoEditType editType)
        {
            if (this._app == null || this._app.Current3DMapControl == null) return;
            if (this._geoEditor.IsEditing)
            {
                this.StopEdit(true);
            }
            this.curGeoField = fcInfo.GetFeatureLayer().GeometryFieldName;
            this._FeatureClass = fcInfo;
            this._row = row;
            this._geoEditType = editType;
            this._InteractaMode = this._app.Current3DMapControl.InteractMode;
            this._app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractEdit;
            if (!this._geoEditor.StartEditFeatureGeometry(row, fcInfo.GetFeatureLayer(), editType))
            {
                this._app.Current3DMapControl.InteractMode = this._InteractaMode;
                XtraMessageBox.Show("暂不支持该类型的编辑");
            }
        }
        public void GeometryEditStart(DF3DFeatureClass fcInfo, IRenderGeometry renderGeo, gviGeoEditType editType)
        {
            if (this._app == null || this._app.Current3DMapControl == null) return;
            if (this._geoEditor.IsEditing)
            {
                this.StopEdit(true);
            }
            this.curGeoField = fcInfo.GetFeatureLayer().GeometryFieldName;
            this._renderGeometry = renderGeo;
            this._FeatureClass = fcInfo;
            Gvitech.CityMaker.FdeCore.IFeatureClass featureClass = fcInfo.GetFeatureClass();
            this._row = featureClass.CreateRowBuffer();
            int num = this._row.FieldIndex(this.curGeoField);
            if (num != -1 && this._renderGeometry != null)
            {
                this._row.SetValue(num, this._renderGeometry.GetFdeGeometry());
            }
            this._geoEditType = editType;
            this._InteractaMode = this._app.Current3DMapControl.InteractMode;
            this._app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractEdit;
            if (!this._geoEditor.StartEditRenderGeometry(renderGeo, editType))
            {
                this._app.Current3DMapControl.InteractMode = this._InteractaMode;
                XtraMessageBox.Show("暂不支持该类型的编辑");
            }
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);

        }
        private void AxRenderControl_RcObjectEditing(object sender, _IRenderControlEvents_RcObjectEditingEvent e)
        {
            if (this._app == null || this._app.Current3DMapControl == null) return;
            if (this._isSpatialQuery)
            {
                this._app.Current3DMapControl.HighlightHelper.VisibleMask = 1;
                this._app.Current3DMapControl.HighlightHelper.SetRegion(e.geometry);
                return;
            }
            if (this._geoEditor.IsEditing && this._row != null)
            {
                int num = this._row.FieldIndex(this.curGeoField);
                if (num != -1)
                {
                    this._row.SetValue(num, e.geometry);
                }
            }
          
        }
        private void AxRenderControl_RcObjectEditFinish(object sender, System.EventArgs e)
        {
            RenderControlEditServices.Instance().RenderEditorType = RenderEditorType.UnKnownType;
            this.StopEdit(true);
        }
        public void StopEdit(bool bSave)
        {
            if (this._app == null || this._app.Current3DMapControl == null) return;
            try
            {
                if (this._isSpatialQuery)
                {
                    if (!bSave)
                    {
                        this._geoEditor.CancelEdit();
                    }
                    else
                    {
                        this._geoEditor.FinishEdit();
                    }
                    if (this.spatialQueryEditStopEvent != null)
                    {
                        this.spatialQueryEditStopEvent(bSave);
                    }
                    this._isSpatialQuery = false;
                    this._app.Current3DMapControl.InteractMode = this._InteractaMode;
                }
                else
                {
                    if (this._FeatureClass != null && this._row != null)
                    {
                        if (!bSave)
                        {
                            this._geoEditor.CancelEdit();
                            if (this._renderGeometry != null)
                            {
                                this._app.Current3DMapControl.ObjectManager.DeleteObject(this._renderGeometry.Guid);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(this._renderGeometry);
                                this._renderGeometry = null;
                            }
                            this._app.Current3DMapControl.InteractMode = this._InteractaMode;
                        }
                        else
                        {
                            if (this._geoEditor.IsEditing && bSave)
                            {
                                this._geoEditor.FinishEdit();
                            }
                            Gvitech.CityMaker.FdeCore.IFeatureClass featureClass = this._FeatureClass.GetFeatureClass();
                            int position = this._row.FieldIndex(this._FeatureClass.GetFeatureLayer().GeometryFieldName);
                            IGeometry geometry = this._row.GetValue(position) as IGeometry;
                            if (this._geoEditType == gviGeoEditType.gviGeoEditCreator)
                            {
                                ITopologicalOperator2D topologicalOperator2D = geometry as ITopologicalOperator2D;
                                if (topologicalOperator2D == null || !topologicalOperator2D.IsSimple2D())
                                {
                                    XtraMessageBox.Show("创建的对象无效!");
                                    this._app.Current3DMapControl.ObjectManager.DeleteObject(this._renderGeometry.Guid);
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(this._renderGeometry);
                                    this._renderGeometry = null;
                                }
                                else
                                {
                                    CommandManagerServices.Instance().StartCommand();
                                    FDECommand fDECommand = new FDECommand(true, true);
                                    SelectCollection.Instance().Clear();
                                    IRowBufferCollection rowBufferCollection = new RowBufferCollectionClass();
                                    rowBufferCollection.Add(this._row);
                                    CommonUtils.Instance().FdeUndoRedoManager.InsertFeatures(featureClass, rowBufferCollection);
                                    CommonUtils.Instance().Insert(this._FeatureClass, rowBufferCollection, true, true);
                                    this._app.Current3DMapControl.FeatureManager.CreateFeature(featureClass, this._row);
                                    this._app.Current3DMapControl.ObjectManager.DeleteObject(this._renderGeometry.Guid);
                                    fDECommand.SetSelectionMap();
                                    CommandManagerServices.Instance().CallCommand(fDECommand);
                                    this._app.Workbench.UpdateMenu();
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(this._renderGeometry);
                                    this._renderGeometry = null;
                                }
                            }
                            else
                            {
                                CommandManagerServices.Instance().StartCommand();
                                FDECommand cmd = new FDECommand(false, true);
                                CommonUtils.Instance().FdeUndoRedoManager.UpdateFeature(featureClass, this._row);
                                CommandManagerServices.Instance().CallCommand(cmd);
                                this._app.Workbench.UpdateMenu();
                            }
                            if (this._row != null)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(this._row);
                                this._row = null;
                            }
                            this._app.Current3DMapControl.InteractMode = this._InteractaMode;
                        }
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            catch (System.UnauthorizedAccessException)
            {
                XtraMessageBox.Show("拒绝访问");
            }
            catch (System.Exception ex2)
            {
                XtraMessageBox.Show(ex2.Message);
            }
        }
        private IModelPoint Polygon2ModelPoint(IPolygon polygon, out Gvitech.CityMaker.Resource.IModel gvModel)
        {
            if (polygon == null || !polygon.IsValid)
            {
                gvModel = null;
                return null;
            }
            IVector3 vector = polygon.QueryNormal();
            if (vector.Z < 0.0)
            {
                polygon.ExteriorRing.ReverseOrientation();
            }
            IModelPoint result = null;
            IGeometryConvertor geometryConvertor = new GeometryConvertorClass();
            ITriMesh triMesh = geometryConvertor.PolygonToTriMesh(polygon);
            if (triMesh != null)
            {
                IMultiTriMesh multiTriMesh = ((IGeometryFactory)new GeometryFactoryClass()).CreateGeometry(gviGeometryType.gviGeometryMultiTrimesh, polygon.VertexAttribute) as IMultiTriMesh;
                multiTriMesh.AddTriMesh(triMesh);
                bool flag = geometryConvertor.TriMeshToModelPoint(multiTriMesh, out gvModel, out result);
                if (flag && gvModel != null)
                {
                    this.Model2Water(ref gvModel);
                }
            }
            else
            {
                result = null;
                gvModel = null;
            }
            return result;
        }
        private void Model2Water(ref Gvitech.CityMaker.Resource.IModel gvModel)
        {
            if (gvModel != null)
            {
                for (int i = 0; i < gvModel.GroupCount; i++)
                {
                    Gvitech.CityMaker.Resource.IDrawGroup group = gvModel.GetGroup(i);
                    if (group != null)
                    {
                        for (int j = 0; j < group.PrimitiveCount; j++)
                        {
                            Gvitech.CityMaker.Resource.IDrawPrimitive primitive = group.GetPrimitive(j);
                            if (primitive != null)
                            {
                                primitive.PrimitiveType = Gvitech.CityMaker.Resource.gviPrimitiveType.gviPrimitiveWater;
                                primitive.Material.DiffuseColor = 4278393126u;
                            }
                        }
                    }
                }
            }
        }
    }
}
