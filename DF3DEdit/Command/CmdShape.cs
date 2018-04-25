using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DEdit.Service;
using DF3DEdit.Class;
using DevExpress.XtraEditors;
using DF3DEdit.Frm;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using DF3DEdit.Delegate;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using DF3DEdit.Interface;

namespace DF3DEdit.Command
{
    class CmdShape : AbstractMap3DCommand
    {
        private System.DateTime time;
        private gviInteractMode _InteractaMode;
        private HashMap beforeRowBufferMap;
        public override void Run(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            RenderControlEditServices.Instance().StopGeometryEdit(true);
            int count = SelectCollection.Instance().GetCount(false);
            if (count > 1)
            {
                XtraMessageBox.Show("批量编辑超过上限,请重新选择");
                base.HighLight = false;
                return;
            }
            if (CommonUtils.Instance().EnableTemproalEdit)
            {
                using (DateSettingDialog dateSettingDialog = new DateSettingDialog())
                {
                    if (dateSettingDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        base.HighLight = false;
                        return;
                    }
                    this.time = dateSettingDialog.Time;
                }
            }
            Map3DCommandManager.Push(this);

            this._InteractaMode = app.Current3DMapControl.InteractMode;
            app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractEdit;
            app.Current3DMapControl.RcObjectEditing += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEventHandler(Current3DMapControl_RcObjectEditing);
            app.Current3DMapControl.RcObjectEditFinish += new EventHandler(Current3DMapControl_RcObjectEditFinish);
            this.SetVertexProperty(SelectCollection.Instance().GetSelectGeometrys());
        }

        void Current3DMapControl_RcObjectEditFinish(object sender, EventArgs e)
        {
            this.SetVertexProperty(SelectCollection.Instance().GetSelectGeometrys());
        }

        void Current3DMapControl_RcObjectEditing(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEvent e)
        {
            try
            {
                IGeometry geo = e.geometry;
                if (geo != null)
                {
                    SetGeometry(geo);
                    UpdateDatabase();
                }
                else
                {
                    DeleteGeometry();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteGeometry()
        {
            DF3DFeatureClass featureClassInfo = null;
            System.Collections.IEnumerator enumerator = SelectCollection.Instance().FeatureClassInfoMap.Keys.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    DF3DFeatureClass featureClassInfo2 = (DF3DFeatureClass)enumerator.Current;
                    featureClassInfo = featureClassInfo2;
                }
            }
            finally
            {
                System.IDisposable disposable = enumerator as System.IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            if (featureClassInfo == null)
            {
                return;
            }
            int count = SelectCollection.Instance().GetCount(false);
            EditParameters editParameters = new EditParameters(featureClassInfo.GetFeatureClass().Guid.ToString());
            editParameters.connectionInfo = CommonUtils.Instance().GetCurrentFeatureDataset().DataSource.ConnectionInfo.ToConnectionString();
            editParameters.datasetName = CommonUtils.Instance().GetCurrentFeatureDataset().Name;
            editParameters.TemproalTime = this.time;
            IBatcheEdit batcheEdit = BatchEditFactory.CreateBatchEdit(count);
            batcheEdit.BeginEdit();
            batcheEdit.DoWork(EditType.ET_DELETE_SELCTION, editParameters);
            batcheEdit.EndEdit();
        }
        private void UpdateDatabase()
        {
            if (this.beforeRowBufferMap != null)
            {
                DF3DFeatureClass featureClassInfo = null;
                System.Collections.IEnumerator enumerator = this.beforeRowBufferMap.Keys.GetEnumerator();
                try
                {
                    if (enumerator.MoveNext())
                    {
                        DF3DFeatureClass featureClassInfo2 = (DF3DFeatureClass)enumerator.Current;
                        featureClassInfo = featureClassInfo2;
                    }
                }
                finally
                {
                    System.IDisposable disposable = enumerator as System.IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                if (featureClassInfo == null)
                {
                    return;
                }
                int count = SelectCollection.Instance().GetCount(false);
                EditParameters editParameters = new EditParameters(featureClassInfo.GetFeatureClass().Guid.ToString());
                editParameters.connectionInfo = CommonUtils.Instance().GetCurrentFeatureDataset().DataSource.ConnectionInfo.ToConnectionString();
                editParameters.datasetName = CommonUtils.Instance().GetCurrentFeatureDataset().Name;
                editParameters.geometryMap = this.ToDictionary();
                editParameters.nTotalCount = count;
                editParameters.TemproalTime = this.time;
                IBatcheEdit batcheEdit = BatchEditFactory.CreateBatchEdit(count);
                batcheEdit.BeginEdit();
                batcheEdit.DoWork(EditType.ET_UPDATE_GEOMETRY, editParameters);
                batcheEdit.EndEdit();
            }
        }
        private System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> ToDictionary()
        {
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> dictionary = new System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection>();
            foreach (DF3DFeatureClass key in this.beforeRowBufferMap.Keys)
            {
                IRowBufferCollection value = this.beforeRowBufferMap[key] as IRowBufferCollection;
                dictionary[key] = value;
            }
            return dictionary;
        }


        private void SetGeometry(IGeometry geo)
        {
            if (geo == null) return;
            if (this.beforeRowBufferMap != null)
            {
                foreach (DF3DFeatureClass featureClassInfo in this.beforeRowBufferMap.Keys)
                {
                    string facName = featureClassInfo.GetFacilityClassName();
                    IRowBufferCollection rowBufferCollection = this.beforeRowBufferMap[featureClassInfo] as IRowBufferCollection;
                    object arg_51_0 = SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo];
                    for (int i = 0; i < rowBufferCollection.Count; i++)
                    {
                        IRowBuffer rowBuffer = rowBufferCollection.Get(i);
                        if (rowBuffer != null)
                        {
                            #region 管线设施
                            if (facName == "PipeLine" || facName == "PipeBuild" || facName == "PipeBuild1")
                            {
                                int num3 = rowBuffer.FieldIndex("Shape");
                                if (num3 != -1)
                                {
                                    rowBuffer.SetValue(num3, geo);
                                }
                                int num4 = rowBuffer.FieldIndex("FootPrint");
                                if (num4 != -1)
                                {
                                    rowBuffer.SetValue(num4, geo.Clone2(gviVertexAttribute.gviVertexAttributeNone));
                                }
                            }
                            #endregion
                            else
                            {
                                int num2 = rowBuffer.FieldIndex(featureClassInfo.GetFeatureLayer().GeometryFieldName);
                                if (num2 != -1)
                                {
                                    rowBuffer.SetValue(num2, geo);

                                }
                            }
                        }
                    }
                }
            }
        }

        private void SetVertexProperty(HashMap rowbufferMap)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (app.Current3DMapControl.ObjectEditor.IsEditing)
            {
                app.Current3DMapControl.ObjectEditor.FinishEdit();
            }
            if (rowbufferMap == null || rowbufferMap.Count == 0)
            {
                return;
            }
            this.beforeRowBufferMap = rowbufferMap;
            this.StartEditFeatures();
        }
        private void StartEditFeatures()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this.beforeRowBufferMap == null)
            {
                return;
            }
            if (app.Current3DMapControl.ObjectEditor.IsEditing)
            {
                app.Current3DMapControl.ObjectEditor.FinishEdit();
            }
            foreach (DF3DFeatureClass featureClassInfo in this.beforeRowBufferMap.Keys)
            {
                IRowBufferCollection rowBufferCollection = this.beforeRowBufferMap[featureClassInfo] as IRowBufferCollection;
                if (rowBufferCollection != null && rowBufferCollection.Count == 1)
                {
                    IFeatureLayer featureLayer = featureClassInfo.GetFeatureLayer();
                    app.Current3DMapControl.ObjectEditor.StartEditFeatureGeometry(rowBufferCollection.Get(0), featureLayer, gviGeoEditType.gviGeoEditVertex);
                }
            }
        }


        public override void RestoreEnv()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.ObjectEditor.FinishEdit();
            app.Current3DMapControl.InteractMode = this._InteractaMode;
            app.Current3DMapControl.RcObjectEditing -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEventHandler(Current3DMapControl_RcObjectEditing);
            app.Current3DMapControl.RcObjectEditFinish -= new EventHandler(Current3DMapControl_RcObjectEditFinish);
            SelectCollection.Instance().ClearRowBuffers();
        }
    }
}
