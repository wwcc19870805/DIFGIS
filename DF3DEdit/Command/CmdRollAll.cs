﻿using System;
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
    public class CmdRollAll : AbstractMap3DCommand
    {
        private bool bFinished = true;
        private HashMap modifyRowbufferMap;
        private HashMap beginRowbufferMap;
        private double centerX;
        private double centerY;
        private double centerZ;
        private System.DateTime time;
        private DockPanel _dockPanel;
        private UIDockPanel _uPanel;
        private UCGeometryEdit _uc;
        private int _height = 600;
        private int _width = 200;

        public CmdRollAll()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.RcTransformHelperBegin += new System.EventHandler(this.AxRenderControl_RcTransformHelperBegin);
            app.Current3DMapControl.RcTransformHelperEnd += new System.EventHandler(this.AxRenderControl_RcTransformHelperEnd);
            app.Current3DMapControl.RcTransformHelperRotating += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcTransformHelperRotatingEventHandler(this.AxRenderControl_RcTransformHelperRotating);
            RenderControlEditServices.Instance().RenderEditorChangedEvent += new RenderEditorChangedHandle(this.RenderEditorChangedEvent);
        }

        public override void Run(object sender, System.EventArgs e)
        {
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

            RenderControlEditServices.Instance().RenderEditorType = RenderEditorType.RotateAllType;
            this.BeginRote(SelectCollection.Instance().GetSelectGeometrys());
            RenderControlEditServices.Instance().SetEditorPosition(this.beginRowbufferMap);
        }

        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            if (!this.bFinished)
            {
                this.EndRote();
            }
            RenderControlEditServices.Instance().RenderEditorType = RenderEditorType.UnKnownType;
            SelectCollection.Instance().ClearRowBuffers();

            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
        }

        private void RenderEditorChangedEvent(HashMap rowbufferMap)
        {
            if (this.Equals(Map3DCommandManager.Peek()))
            {
                this.BeginRote(rowbufferMap);
            }
        }

        private void AxRenderControl_RcTransformHelperEnd(object sender, System.EventArgs e)
        {
            if (this.Equals(Map3DCommandManager.Peek()))
            {
                this.EndRote();
            }
        }

        private void AxRenderControl_RcTransformHelperBegin(object sender, System.EventArgs e)
        {
            if (this.Equals(Map3DCommandManager.Peek()))
            {
                this.BeginRote(this.beginRowbufferMap);
            }
        }

        private void AxRenderControl_RcTransformHelperRotating(object sender, _IRenderControlEvents_RcTransformHelperRotatingEvent e)
        {
            if (this.Equals(Map3DCommandManager.Peek()))
            {
                this.RotatingModel(e.axis.X, e.axis.Y, e.axis.Z, e.angle);
            }
        }

        private void RotatingModel(double AxisX, double AxisY, double AxisZ, double Angle)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            this.bFinished = false;
            this.modifyRowbufferMap = SelectCollection.Instance().Clone(this.beginRowbufferMap);
            if (this.modifyRowbufferMap != null)
            {
                foreach (DF3DFeatureClass featureClassInfo in this.modifyRowbufferMap.Keys)
                {
                    IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                    string facName = featureClassInfo.GetFacilityClassName();
                    IRowBufferCollection rowBufferCollection = this.modifyRowbufferMap[featureClassInfo] as IRowBufferCollection;
                    for (int i = 0; i < rowBufferCollection.Count; i++)
                    {
                        IRowBuffer rowBuffer = rowBufferCollection.Get(i);
                        if (rowBuffer != null)
                        {
                            int num = rowBuffer.FieldIndex(featureClassInfo.GetFeatureLayer().GeometryFieldName);
                            if (num != -1)
                            {
                                IGeometry geometry = rowBuffer.GetValue(num) as IGeometry;
                                if (geometry != null)
                                {
                                    ITransform transform = geometry as ITransform;
                                    if (geometry.HasZ())
                                    {
                                        transform.Rotate3D(AxisX, AxisY, AxisZ, this.centerX, this.centerY, this.centerZ, Angle);
                                    }
                                    else
                                    {
                                        if (!geometry.HasZ() && AxisZ > 0.0)
                                        {
                                            transform.Rotate2D(this.centerX, this.centerY, Angle);
                                        }
                                    }
                                    rowBuffer.SetValue(num, transform);
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
                                                transform.Rotate3D(AxisX, AxisY, AxisZ, this.centerX, this.centerY, this.centerZ, Angle);
                                            }
                                            else
                                            {
                                                transform.Rotate2D(this.centerX, this.centerY, Angle);
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
                                                transform.Rotate3D(AxisX, AxisY, AxisZ, this.centerX, this.centerY, this.centerZ, Angle);
                                            }
                                            else
                                            {
                                                transform.Rotate2D(this.centerX, this.centerY, Angle);
                                            }
                                            rowBuffer.SetValue(num4, transform);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    app.Current3DMapControl.FeatureManager.EditFeatures(featureClass, rowBufferCollection);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                }
            }
        }

        private void EndRote()
        {
            this.bFinished = true;
            if (this.modifyRowbufferMap != null)
            {
                DF3DFeatureClass featureClassInfo = null;
                System.Collections.IEnumerator enumerator = this.modifyRowbufferMap.Keys.GetEnumerator();
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
            this.beginRowbufferMap = this.modifyRowbufferMap;
        }

        private void BeginRote(HashMap rowbufferMap)
        {
            this.beginRowbufferMap = rowbufferMap;
            IEnvelope selectEnvelope = SelectCollection.Instance().GetSelectEnvelope(this.beginRowbufferMap);
            if (selectEnvelope != null)
            {
                this.centerX = (selectEnvelope.MinX + selectEnvelope.MaxX) / 2.0;
                this.centerY = (selectEnvelope.MinY + selectEnvelope.MaxY) / 2.0;
                this.centerZ = (selectEnvelope.MinZ + selectEnvelope.MaxZ) / 2.0;
            }
        }

        private System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> ToDictionary()
        {
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> dictionary = new System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection>();
            foreach (DF3DFeatureClass key in this.modifyRowbufferMap.Keys)
            {
                IRowBufferCollection value = this.modifyRowbufferMap[key] as IRowBufferCollection;
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
