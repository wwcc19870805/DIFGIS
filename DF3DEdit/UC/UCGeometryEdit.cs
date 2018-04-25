using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Class;
using DF3DEdit.Service;
using DF3DEdit.Delegate;
using DF3DEdit.Frm;
using DF3DEdit.Interface;

namespace DF3DEdit.UC
{
    public class UCGeometryEdit : XtraUserControl
    {
        private DF3DFeatureClass _connInfo;
        private int _oid = -1;
        private RenderEditorType _editType;
        private System.DateTime _time = System.DateTime.Now;
        private System.ComponentModel.IContainer components;
        private PanelControl panelControl1;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private SimpleButton btnApply;
        private SpinEdit spinEditY;
        private SpinEdit spinEditX;
        private SpinEdit spinEditZ;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem4;
        private EmptySpaceItem emptySpaceItem1;
        private EmptySpaceItem emptySpaceItem2;
        //public DF3DFeatureClass ConnInfo
        //{
        //    set
        //    {
        //        this._connInfo = value;
        //    }
        //}
        public int Oid
        {
            set
            {
                this._oid = value;
            }
        }

        public UCGeometryEdit()
        {
            this.InitializeComponent();
            this._connInfo = CommonUtils.Instance().CurEditLayer;
            RenderControlEditServices.Instance().GeometryEditorCreateGeometryEvent += new GeometryEditorCreateGeometryHandle(this.GeometryEditorCreateGeometryEvent);
            RenderControlEditServices.Instance().GeometryEditorStopEvent += new GeometryEditorStopHandle(this.GeometryEditorStopEvent);
            RenderControlEditServices.Instance().RenderEditorTypeChangedEvent += new RenderEditorTypeChangedHandle(this.RenderEditorTypeChanged);
            RenderControlEditServices.Instance().RenderEditorType = RenderEditorType.UnKnownType;
            SelectCollection.Instance().SelectionChangedEvent += new SelectionChangedEventHandler(this.SelectionChanged);
        }

        private void GeometryEditorStopEvent(bool bSave)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            RenderControlEditServices.Instance().RenderEditorType = RenderEditorType.UnKnownType;
            if (app.Current3DMapControl.ObjectEditor.IsEditing)
            {
                GeometryEdit.Instance().StopEdit(bSave);
            }
        }

        private void GeometryEditorCreateGeometryEvent(DF3DFeatureClass fcInfo, IRenderGeometry renderGeo)
        {
            RenderControlEditServices.Instance().RenderEditorType = RenderEditorType.UnKnownType;
            GeometryEdit.Instance().GeometryEditStart(fcInfo, renderGeo, gviGeoEditType.gviGeoEditCreator);
        }

        private void SelectionChanged()
        {
            int count = SelectCollection.Instance().GetCount(false);
            if (count != 1)
            {
                RenderControlEditServices.Instance().StopGeometryEdit(false);
                //this._selectRow = null;
                //this._editCtrl.bar2.Visible = false;
                //this.memoEdit1.Visible = false;
                //SelectCollection.Instance().PickUpSingleRecord(null, -1);
                return;
            }
            //int oid = -1;
            //foreach (DF3DFeatureClass featureClassInfo in SelectCollection.Instance().FeatureClassInfoMap.Keys)
            //{
            //    this._selectFeatureClass = featureClassInfo;
            //    ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo] as ResultSetInfo;
            //    if (resultSetInfo != null)
            //    {
            //        this.fidFieldName = this._selectFeatureClass.FidFieldName;
            //        this.curGeoField = this._selectFeatureClass.GeometryFieldName;
            //        System.Data.DataTable resultSetTable = resultSetInfo.ResultSetTable;
            //        if (resultSetTable != null && resultSetTable.Rows.Count != 0)
            //        {
            //            this._selectRow = resultSetTable.Rows[0];
            //            oid = int.Parse(this._selectRow[this.fidFieldName].ToString());
            //            System.Data.DataRow dataRow = this._recordTable.NewRow();
            //            KeyValuePairEx<string, string> keyValuePairEx = new KeyValuePairEx<string, string>(this.curGeoField, this.curGeoField);
            //            dataRow[0] = keyValuePairEx;
            //            dataRow[1] = this._selectRow[this.curGeoField];
            //            this._recordTable.Rows.Add(dataRow);
            //            if (this._selectFeatureClass.HasAttachment)
            //            {
            //                this.layoutControlItem3.Visibility = (LayoutVisibility)0;
            //            }
            //            else
            //            {
            //                this.layoutControlItem3.Visibility = (LayoutVisibility)1;
            //            }
            //            System.Collections.IEnumerator enumerator2 = resultSetTable.Columns.GetEnumerator();
            //            try
            //            {
            //                while (enumerator2.MoveNext())
            //                {
            //                    System.Data.DataColumn dataColumn = (System.Data.DataColumn)enumerator2.Current;
            //                    if (!(dataColumn.ColumnName == this.curGeoField))
            //                    {
            //                        dataRow = this._recordTable.NewRow();
            //                        KeyValuePairEx<string, string> keyValuePairEx2 = new KeyValuePairEx<string, string>(dataColumn.ColumnName, dataColumn.Caption);
            //                        dataRow[0] = keyValuePairEx2;
            //                        dataRow[1] = this._selectRow[dataColumn.ColumnName];
            //                        this._recordTable.Rows.Add(dataRow);
            //                    }
            //                }
            //                break;
            //            }
            //            finally
            //            {
            //                System.IDisposable disposable = enumerator2 as System.IDisposable;
            //                if (disposable != null)
            //                {
            //                    disposable.Dispose();
            //                }
            //            }
            //        }
            //    }
            //}
            //SelectCollection.Instance().PickUpSingleRecord(this._selectFeatureClass, oid);
        }
        private void RenderEditorTypeChanged(RenderEditorType type)
        {
            this._editType = type;
            switch (type)
            {
                case RenderEditorType.MoveType:
                case RenderEditorType.GeometryMove:
                    {
                        this.spinEditX.Properties.MinValue = -79228162514264337593543950335m;
                        this.spinEditY.Properties.MinValue = -79228162514264337593543950335m;
                        this.spinEditZ.Properties.MinValue = -79228162514264337593543950335m;
                        this.spinEditX.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditY.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditZ.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditX.EditValue = 0;
                        this.spinEditY.EditValue = 0;
                        this.spinEditZ.EditValue = 0;
                        this.layoutControlGroup1.Text = "平移";
                        break;
                    }
                case RenderEditorType.RotateAllType:
                case RenderEditorType.RotateCenterType:
                case RenderEditorType.GeometryRoate:
                    {
                        this.spinEditX.Properties.MinValue = -79228162514264337593543950335m;
                        this.spinEditY.Properties.MinValue = -79228162514264337593543950335m;
                        this.spinEditZ.Properties.MinValue = -79228162514264337593543950335m;
                        this.spinEditX.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditY.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditZ.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditX.EditValue = 0;
                        this.spinEditY.EditValue = 0;
                        this.spinEditZ.EditValue = 0;
                        this.layoutControlGroup1.Text = "旋转";
                        break;
                    }
                case RenderEditorType.ScaleAllType:
                case RenderEditorType.ScaleCenterType:
                case RenderEditorType.GeometryScale:
                    {
                        this.spinEditX.Properties.MinValue = 0.01m;
                        this.spinEditY.Properties.MinValue = 0.01m;
                        this.spinEditZ.Properties.MinValue = 0.01m;
                        this.spinEditX.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditY.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditZ.Properties.MaxValue = 79228162514264337593543950335m;
                        this.spinEditX.EditValue = 1;
                        this.spinEditY.EditValue = 1;
                        this.spinEditZ.EditValue = 1;
                        this.layoutControlGroup1.Text = "缩放";
                        break;
                    }
                //default:
                //    {
                //        this.panelControl1.Visible = false;
                //        return;
                //    }
            }
            //this.panelControl1.Visible = WorkSpaceServices.Instance().PropertyCanEdit;
            this.panelControl1.Visible = true;
        }
        private void btnApply_Click(object sender, System.EventArgs e)
        {
            //if (this._connInfo != null && this._connInfo.GetFeatureClass().HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
            //{
            //    using (DateSettingDialog dateSettingDialog = new DateSettingDialog())
            //    {
            //        if (dateSettingDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //        {
            //            return;
            //        }
            //        this._time = dateSettingDialog.Time;
            //    }
            //}
            switch (this._editType)
            {
                case RenderEditorType.MoveType:
                    {
                        this.MoveSelection();
                        goto IL_AC;
                    }
                case RenderEditorType.RotateAllType:
                case RenderEditorType.RotateCenterType:
                    {
                        this.RoateSelection();
                        goto IL_AC;
                    }
                case RenderEditorType.ScaleAllType:
                case RenderEditorType.ScaleCenterType:
                    {
                        this.ScaleSelection();
                        goto IL_AC;
                    }
                case RenderEditorType.GeometryMove:
                    {
                        this.MoveGeometry();
                        goto IL_AC;
                    }
                case RenderEditorType.GeometryRoate:
                    {
                        this.RoateGeometry();
                        goto IL_AC;
                    }
                case RenderEditorType.GeometryScale:
                    {
                        this.ScaleGeometry();
                        goto IL_AC;
                    }
                default:
                    {
                        goto IL_AC;
                    }
            }
            return;
        IL_AC:
            RenderControlEditServices.Instance().RenderEditorType = this._editType;
        }

        private void MoveGeometry()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            double num = double.Parse(this.spinEditX.EditValue.ToString());
            double num2 = double.Parse(this.spinEditY.EditValue.ToString());
            double num3 = double.Parse(this.spinEditZ.EditValue.ToString());
            if (num == 0.0 && num2 == 0.0 && num3 == 0.0)
            {
                return;
            }
            IVector3 vector = new Vector3Class();
            vector.Set(num, num2, num3);
            app.Current3DMapControl.ObjectEditor.Move(vector);
        }
        private void MoveSelection()
        {
            double num = double.Parse(this.spinEditX.EditValue.ToString());
            double num2 = double.Parse(this.spinEditY.EditValue.ToString());
            double num3 = double.Parse(this.spinEditZ.EditValue.ToString());
            if (num == 0.0 && num2 == 0.0 && num3 == 0.0)
            {
                return;
            }
            IVector3 vector = new Vector3Class();
            vector.Set(num, num2, num3);
            SelectCollection.Instance().SelectionMoving(vector);
        }
        private void ScaleGeometry()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            double num = double.Parse(this.spinEditX.EditValue.ToString());
            double num2 = double.Parse(this.spinEditY.EditValue.ToString());
            double num3 = double.Parse(this.spinEditZ.EditValue.ToString());
            if (num == 0.0 && num2 == 0.0 && num3 == 0.0)
            {
                return;
            }
            if (this._connInfo == null || this._oid == -1)
            {
                return;
            }
            Gvitech.CityMaker.FdeCore.IFeatureClass featureClass = null;
            try
            {
                featureClass = this._connInfo.GetFeatureClass();
                IFeatureLayer featureLayer = this._connInfo.GetFeatureLayer();
                Gvitech.CityMaker.FdeCore.IRowBuffer row = featureClass.GetRow(this._oid);
                int num4 = row.FieldIndex(featureLayer.GeometryFieldName);
                if (num4 != -1)
                {
                    IGeometry geometry = row.GetValue(num4) as IGeometry;
                    if (geometry != null)
                    {
                        IVector3 vector = new Vector3Class();
                        vector.Set(num, num2, num3);
                        app.Current3DMapControl.ObjectEditor.Scale(vector, geometry.Envelope.Center);
                    }
                }
            }
            catch (System.Exception)
            {
            }
            finally
            {
                //if (featureClass != null)
                //{
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                //    featureClass = null;
                //}
            }
        }
        private void ScaleSelection()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            double num = double.Parse(this.spinEditX.EditValue.ToString());
            double num2 = double.Parse(this.spinEditY.EditValue.ToString());
            double num3 = double.Parse(this.spinEditZ.EditValue.ToString());
            if (num == 0.0 && num2 == 0.0 && num3 == 0.0)
            {
                return;
            }
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> rowBuffers = this.GetRowBuffers();
            if (rowBuffers.Count < 1)
            {
                return;
            }
            IEnvelope selectEnvelope = SelectCollection.Instance().GetSelectEnvelope(SelectCollection.Instance().FcRowBuffersMap);
            double centerX = 0.0;
            double centerY = 0.0;
            double centerZ = 0.0;
            if (selectEnvelope != null && selectEnvelope.Valid())
            {
                centerX = (selectEnvelope.MinX + selectEnvelope.MaxX) / 2.0;
                centerY = (selectEnvelope.MinY + selectEnvelope.MaxY) / 2.0;
                centerZ = (selectEnvelope.MinZ + selectEnvelope.MaxZ) / 2.0;
            }
            foreach (DF3DFeatureClass current in rowBuffers.Keys)
            {
                Gvitech.CityMaker.FdeCore.IFeatureClass featureClass = current.GetFeatureClass();
                IFeatureLayer featureLayer = current.GetFeatureLayer();
                IRowBufferCollection rowBufferCollection = rowBuffers[current];
                for (int i = 0; i < rowBufferCollection.Count; i++)
                {
                    Gvitech.CityMaker.FdeCore.IRowBuffer rowBuffer = rowBufferCollection.Get(i);
                    int position = rowBuffer.FieldIndex(featureLayer.GeometryFieldName);
                    IGeometry geometry = rowBuffer.GetValue(position) as IGeometry;
                    ITransform transform = geometry as ITransform;
                    if (geometry != null && transform != null)
                    {
                        if (this._editType == RenderEditorType.ScaleCenterType)
                        {
                            IEnvelope envelope = geometry.Envelope;
                            centerX = (envelope.MinX + envelope.MaxX) / 2.0;
                            centerY = (envelope.MinY + envelope.MaxY) / 2.0;
                            centerZ = (envelope.MinZ + envelope.MaxZ) / 2.0;
                        }
                        if (geometry.HasZ())
                        {
                            transform.Scale3D(num, num2, num3, centerX, centerY, centerZ);
                        }
                        else
                        {
                            transform.Scale2D(num, num2, centerX, centerY);
                        }
                        rowBuffer.SetValue(position, transform);
                    }
                }
                app.Current3DMapControl.FeatureManager.EditFeatures(featureClass, rowBufferCollection);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
            }
            RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.Instance().FcRowBuffersMap);
            this.UpdateDatabase(rowBuffers);
        }
        private void RoateGeometry()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            double num = double.Parse(this.spinEditX.EditValue.ToString());
            double num2 = double.Parse(this.spinEditY.EditValue.ToString());
            double num3 = double.Parse(this.spinEditZ.EditValue.ToString());
            if (num == 0.0 && num2 == 0.0 && num3 == 0.0)
            {
                return;
            }
            num = 3.14 * num / 180.0;
            num2 = 3.14 * num2 / 180.0;
            num3 = 3.14 * num3 / 180.0;
            if (this._connInfo == null || this._oid == -1)
            {
                return;
            }
            Gvitech.CityMaker.FdeCore.IFeatureClass featureClass = null;
            try
            {
                featureClass = this._connInfo.GetFeatureClass();
                IFeatureLayer featureLayer = this._connInfo.GetFeatureLayer();
                Gvitech.CityMaker.FdeCore.IRowBuffer row = featureClass.GetRow(this._oid);
                int num4 = row.FieldIndex(featureLayer.GeometryFieldName);
                if (num4 != -1)
                {
                    IGeometry geometry = row.GetValue(num4) as IGeometry;
                    if (geometry != null)
                    {
                        IVector3 vector = new Vector3Class();
                        vector.Set(1.0, 0.0, 0.0);
                        app.Current3DMapControl.ObjectEditor.Rotate(vector, geometry.Envelope.Center, num);
                        vector.Set(0.0, 1.0, 0.0);
                        app.Current3DMapControl.ObjectEditor.Rotate(vector, geometry.Envelope.Center, num2);
                        vector.Set(0.0, 0.0, 1.0);
                        app.Current3DMapControl.ObjectEditor.Rotate(vector, geometry.Envelope.Center, num3);
                    }
                }
            }
            catch (System.Exception)
            {
            }
            finally
            {
                //if (featureClass != null)
                //{
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                //    featureClass = null;
                //}
            }
        }
        private void RoateSelection()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            double num = double.Parse(this.spinEditX.EditValue.ToString());
            double num2 = double.Parse(this.spinEditY.EditValue.ToString());
            double num3 = double.Parse(this.spinEditZ.EditValue.ToString());
            if (num == 0.0 && num2 == 0.0 && num3 == 0.0)
            {
                return;
            }
            num = 3.14 * num / 180.0;
            num2 = 3.14 * num2 / 180.0;
            num3 = 3.14 * num3 / 180.0;
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> rowBuffers = this.GetRowBuffers();
            if (rowBuffers.Count < 1)
            {
                return;
            }
            IEnvelope selectEnvelope = SelectCollection.Instance().GetSelectEnvelope(SelectCollection.Instance().FcRowBuffersMap);
            double centerX = 0.0;
            double centerY = 0.0;
            double centerZ = 0.0;
            if (selectEnvelope != null)
            {
                centerX = (selectEnvelope.MinX + selectEnvelope.MaxX) / 2.0;
                centerY = (selectEnvelope.MinY + selectEnvelope.MaxY) / 2.0;
                centerZ = (selectEnvelope.MinZ + selectEnvelope.MaxZ) / 2.0;
            }
            foreach (DF3DFeatureClass current in rowBuffers.Keys)
            {
                Gvitech.CityMaker.FdeCore.IFeatureClass featureClass = current.GetFeatureClass();
                IFeatureLayer featureLayer = current.GetFeatureLayer();
                IRowBufferCollection rowBufferCollection = rowBuffers[current];
                for (int i = 0; i < rowBufferCollection.Count; i++)
                {
                    Gvitech.CityMaker.FdeCore.IRowBuffer rowBuffer = rowBufferCollection.Get(i);
                    int position = rowBuffer.FieldIndex(featureLayer.GeometryFieldName);
                    IGeometry geometry = rowBuffer.GetValue(position) as IGeometry;
                    ITransform transform = geometry as ITransform;
                    if (geometry != null && transform != null)
                    {
                        if (this._editType == RenderEditorType.RotateCenterType)
                        {
                            IEnvelope envelope = geometry.Envelope;
                            centerX = (envelope.MinX + envelope.MaxX) / 2.0;
                            centerY = (envelope.MinY + envelope.MaxY) / 2.0;
                            centerZ = (envelope.MinZ + envelope.MaxZ) / 2.0;
                        }
                        if (geometry.HasZ())
                        {
                            transform.Rotate3D(1.0, 0.0, 0.0, centerX, centerY, centerZ, num);
                            transform.Rotate3D(0.0, 1.0, 0.0, centerX, centerY, centerZ, num2);
                            transform.Rotate3D(0.0, 0.0, 1.0, centerX, centerY, centerZ, num3);
                        }
                        else
                        {
                            transform.Rotate2D(centerX, centerY, num3);
                        }
                        rowBuffer.SetValue(position, transform);
                    }
                }
                app.Current3DMapControl.FeatureManager.EditFeatures(featureClass, rowBufferCollection);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
            }
            RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.Instance().FcRowBuffersMap);
            this.UpdateDatabase(rowBuffers);
        }
        private void UpdateDatabase(System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> rowsMap)
        {
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection>.Enumerator enumerator = rowsMap.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return;
            }
            System.Collections.Generic.KeyValuePair<DF3DFeatureClass, IRowBufferCollection> current = enumerator.Current;
            DF3DFeatureClass key = current.Key;
            int count = SelectCollection.Instance().GetCount(false);
            EditParameters editParameters = new EditParameters(key.GetFeatureClass().GuidString);
            editParameters.connectionInfo = key.GetFeatureClass().DataSource.ConnectionInfo.ToConnectionString();
            editParameters.datasetName = key.GetFeatureClass().FeatureDataSet.Name;
            editParameters.geometryMap = rowsMap;
            editParameters.nTotalCount = count;
            editParameters.TemproalTime = this._time;
            IBatcheEdit batcheEdit = BatchEditFactory.CreateBatchEdit(count);
            batcheEdit.BeginEdit();
            batcheEdit.DoWork(EditType.ET_UPDATE_GEOMETRY, editParameters);
            batcheEdit.EndEdit();
        }
        private System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> GetRowBuffers()
        {
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> dictionary = new System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection>();
            HashMap selectGeometrys = SelectCollection.Instance().GetSelectGeometrys();
            if (selectGeometrys == null)
            {
                return dictionary;
            }
            foreach (DF3DFeatureClass key in selectGeometrys.Keys)
            {
                IRowBufferCollection value = selectGeometrys[key] as IRowBufferCollection;
                dictionary.Add(key, value);
            }
            return dictionary;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.spinEditY = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditX = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditZ = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(342, 272);
            this.panelControl1.TabIndex = 4;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.spinEditY);
            this.layoutControl1.Controls.Add(this.spinEditX);
            this.layoutControl1.Controls.Add(this.spinEditZ);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(338, 268);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(194, 110);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(132, 22);
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // spinEditY
            // 
            this.spinEditY.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditY.Location = new System.Drawing.Point(27, 58);
            this.spinEditY.Name = "spinEditY";
            this.spinEditY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditY.Size = new System.Drawing.Size(299, 22);
            this.spinEditY.StyleController = this.layoutControl1;
            this.spinEditY.TabIndex = 6;
            // 
            // spinEditX
            // 
            this.spinEditX.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditX.Location = new System.Drawing.Point(27, 32);
            this.spinEditX.Name = "spinEditX";
            this.spinEditX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditX.Size = new System.Drawing.Size(299, 22);
            this.spinEditX.StyleController = this.layoutControl1;
            this.spinEditX.TabIndex = 5;
            // 
            // spinEditZ
            // 
            this.spinEditZ.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditZ.Location = new System.Drawing.Point(27, 84);
            this.spinEditZ.Name = "spinEditZ";
            this.spinEditZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditZ.Size = new System.Drawing.Size(299, 22);
            this.spinEditZ.StyleController = this.layoutControl1;
            this.spinEditZ.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(338, 268);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.spinEditZ;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(318, 26);
            this.layoutControlItem1.Text = "Z:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(12, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinEditX;
            this.layoutControlItem2.CustomizationFormText = "X:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(318, 26);
            this.layoutControlItem2.Text = "X:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(12, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.spinEditY;
            this.layoutControlItem3.CustomizationFormText = "Y:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(318, 26);
            this.layoutControlItem3.Text = "Y:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(12, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnApply;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(182, 78);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(136, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 78);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(182, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 104);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(318, 124);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // UCGeometryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UCGeometryEdit";
            this.Size = new System.Drawing.Size(342, 272);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
