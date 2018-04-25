using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using DFWinForms.Class;
using DF3DControl.Base;
using DF3DControl.Command;
using DF3DData.Class;
using DF3DEdit.Service;
using DF3DEdit.Class;
using DF3DEdit.Delegate;
using DF3DEdit.Interface;
using DF3DEdit.Frm;
using DF3DEdit.UC;

namespace DF3DEdit.Command
{
    public class CmdMove : AbstractMap3DCommand
    {
        private HashMap beforeRowBufferMap;
        private gviInteractMode _InteractaMode;
        private System.DateTime time;
        private DockPanel _dockPanel;
        private UIDockPanel _uPanel;
        private UCGeometryEdit _uc;
        private int _height = 600;
        private int _width = 200;

        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            RenderControlEditServices.Instance().StopGeometryEdit(true);
            int count = SelectCollection.Instance().GetCount(false);
            if (count > 10000)
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
            this._uPanel = new UIDockPanel("对象编辑", "对象编辑", this.Location, this._width, this._height);
            this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
            this._dockPanel.Visibility = DockVisibility.Visible;
            this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
            this._dockPanel.Width = this._width;
            this._dockPanel.Height = this._height;
            this._uc = new UCGeometryEdit();
            this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uPanel.RegisterEvent(new PanelClose(this.Close));
            this._dockPanel.Controls.Add(this._uc);

            this._InteractaMode = app.Current3DMapControl.InteractMode;
            this.SetMoveProperty(SelectCollection.Instance().GetSelectGeometrys());
            SelectCollection.Instance().SelectionMovingEvent += new System.Action<IVector3>(this.SelectionMovingEvent);
            app.Current3DMapControl.RcFeaturesMoving += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcFeaturesMovingEventHandler(this.AxRenderControl_RcFeaturesMoving);
        }

        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.ObjectEditor.FinishEdit();
            RenderControlEditServices.Instance().RenderEditorType = RenderEditorType.UnKnownType;
            app.Current3DMapControl.InteractMode = this._InteractaMode;
            app.Current3DMapControl.RcFeaturesMoving -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcFeaturesMovingEventHandler(this.AxRenderControl_RcFeaturesMoving);
            SelectCollection.Instance().SelectionMovingEvent -= new System.Action<IVector3>(this.SelectionMovingEvent);
            SelectCollection.Instance().ClearRowBuffers();

            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
        }

        public CmdMove()
        {
            RenderControlEditServices.Instance().RenderEditorChangedEvent += new RenderEditorChangedHandle(this.RenderEditorChange);
        }

        private void SelectionMovingEvent(IVector3 vec)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.ObjectEditor.Move(vec);
        }

        private void AxRenderControl_RcFeaturesMoving(object sender, _IRenderControlEvents_RcFeaturesMovingEvent e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this.Equals(Map3DCommandManager.Peek()) && app.Current3DMapControl.InteractMode == gviInteractMode.gviInteractEdit)
            {
                IVector3 translate = e.translate;
                this.MovingModel(translate.X, translate.Y, translate.Z);
                this.UpdateDatabase();
                this.StartEditFeatures();
            }
            //if (app.Current3DMapControl.InteractMode == gviInteractMode.gviInteractEdit)
            //{
            //    IVector3 translate = e.translate;
            //    this.MovingModel(translate.X, translate.Y, translate.Z);
            //    this.UpdateDatabase();
            //    this.StartEditFeatures();
            //}
        }

        private void RenderEditorChange(HashMap rowbufferMap)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this.Equals(Map3DCommandManager.Peek()) && app.Current3DMapControl.InteractMode == gviInteractMode.gviInteractEdit)
            {
                this.SetMoveProperty(SelectCollection.Instance().GetSelectGeometrys());
            }
            //if (app.Current3DMapControl.InteractMode == gviInteractMode.gviInteractEdit)
            //{
            //    this.SetMoveProperty(SelectCollection.Instance().GetSelectGeometrys());
            //}
        }

        private void SetMoveProperty(HashMap rowbufferMap)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            RenderControlEditServices.Instance().RenderEditorType = RenderEditorType.MoveType;
            app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractEdit;
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
            foreach (DF3DFeatureClass  featureClassInfo in this.beforeRowBufferMap.Keys)
            {
                IRowBufferCollection rowBufferCollection = this.beforeRowBufferMap[featureClassInfo] as IRowBufferCollection;
                if (rowBufferCollection != null && rowBufferCollection.Count >= 1)
                {
                    IFeatureLayer featureLayer = featureClassInfo.GetFeatureLayer();
                    app.Current3DMapControl.ObjectEditor.AddMovingFeatures(featureLayer, rowBufferCollection);
                }
            }
            app.Current3DMapControl.ObjectEditor.StartMoveFeatures(CommonUtils.Instance().CurEditDatasetWkt);
        }

        private void MovingModel(double X, double Y, double Z)
        {
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
                            int num = rowBuffer.FieldIndex(featureClassInfo.GetFeatureClass().FidFieldName);
                            if (num != -1)
                            {
                                int arg_87_0 = (int)rowBuffer.GetValue(num);
                            }
                            int num2 = rowBuffer.FieldIndex(featureClassInfo.GetFeatureLayer().GeometryFieldName);
                            if (num2 != -1)
                            {
                                IGeometry geometry = rowBuffer.GetValue(num2) as IGeometry;
                                if (geometry != null)
                                {
                                    ITransform transform = geometry as ITransform;
                                    if (geometry != null && transform != null)
                                    {
                                        if (geometry.HasZ())
                                        {
                                            transform.Move3D(X, Y, Z);
                                        }
                                        else
                                        {
                                            transform.Move2D(X, Y);
                                        }
                                        rowBuffer.SetValue(num2, transform);
                                    }
                                }
                            }
                            #region 管线设施
                            if (facName == "PipeLine" || facName == "PipeNode" || facName == "PipeBuild" || facName == "PipeBuild1")
                            {
                                int num3 = rowBuffer.FieldIndex("Shape");
                                if (num3 != -1)
                                {
                                    IGeometry geometry = rowBuffer.GetValue(num3) as IGeometry;
                                    if (geometry != null)
                                    {
                                        ITransform transform = geometry as ITransform;
                                        if (geometry != null && transform != null)
                                        {
                                            if (geometry.HasZ())
                                            {
                                                transform.Move3D(X, Y, Z);
                                            }
                                            else
                                            {
                                                transform.Move2D(X, Y);
                                            }
                                            rowBuffer.SetValue(num3, transform);
                                        }
                                    }
                                }
                                int num4 = rowBuffer.FieldIndex("FootPrint");
                                if (num4 != -1)
                                {
                                    IGeometry geometry = rowBuffer.GetValue(num4) as IGeometry;
                                    if (geometry != null)
                                    {
                                        ITransform transform = geometry as ITransform;
                                        if (geometry != null && transform != null)
                                        {
                                            if (geometry.HasZ())
                                            {
                                                transform.Move3D(X, Y, Z);
                                            }
                                            else
                                            {
                                                transform.Move2D(X, Y);
                                            }
                                            rowBuffer.SetValue(num4, transform);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
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

        private System.Drawing.Point Location
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }
    }
}
