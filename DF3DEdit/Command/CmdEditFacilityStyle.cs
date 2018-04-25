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
using DF3DEdit.Frm;
using DF3DPipeCreateTool.Class;
using DF3DPipeCreateTool.Service;
using DFDataConfig.Class;
using Gvitech.CityMaker.Resource;
using DF3DPipeCreateTool.UC;
using DF3DEdit.Interface;
using DF3DAuthority;
using DF3DEdit.UC;

namespace DF3DEdit.Command
{
    public class CmdEditFacilityStyle : AbstractMap3DCommand
    {
        private DockPanel _dockPanel;
        private UIDockPanel _uPanel;
        private UCEditFacilityStyle _uc;
        private int _height = 600;
        private int _width = 200;
        private DateTime time;

        public override void Run(object sender, System.EventArgs e)
        {
            bool _isAuth = Authority3DService.Instance.IsAuthorized;
            if (!_isAuth)
            {
                XtraMessageBox.Show("此功能需要USB Key。", "提示");
                return;
            }
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

            //Map3DCommandManager.Push(this);
            this._uPanel = new UIDockPanel("风格编辑", "风格编辑", this.Location, this._width, this._height);
            this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
            this._dockPanel.Visibility = DockVisibility.Visible;
            this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
            this._dockPanel.Width = this._width;
            this._dockPanel.Height = this._height;
            this._uc = new UCEditFacilityStyle();
            this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uPanel.RegisterEvent(new PanelClose(this.Close));
            this._dockPanel.Controls.Add(this._uc);

            SelectCollection.Instance().FacStyleClassChangedEvent += new Delegate.FacStyleClassChangedHandle(CmdEditFacilityStyle_FacStyleClassChangedEvent);
        }

        private Dictionary<DF3DFeatureClass, IRowBufferCollection> beforeRowBufferMap = new Dictionary<DF3DFeatureClass, IRowBufferCollection>();

        private void CmdEditFacilityStyle_FacStyleClassChangedEvent(DF3DPipeCreateTool.Class.FacStyleClass style)
        {
            try
            {
                this.beforeRowBufferMap.Clear();
                if (style == null) return;

                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc == null) return;
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) return;
                FacClassReg reg = FacilityInfoService.GetFacClassRegByFeatureClassID(fc.GuidString);
                if (reg == null) return;
                TopoClass tc = FacilityInfoService.GetTopoClassByFacClassCode(reg.FacClassCode);
                if (tc == null) return;
                FacilityClass facc = reg.FacilityType;
                if (facc == null) return;
                IResourceManager manager = fc.FeatureDataSet as IResourceManager;
                if (manager == null) return;

                IFieldInfoCollection fields = fc.GetFields();
                //int indexGroupid = -1;
                //int indexClassify = -1;
                //SubClass sc = null;
                //MajorClass mc = LogicDataStructureManage3D.Instance.GetMajorClassByDFFeatureClassID(fc.GuidString);
                //if (mc != null)
                //{
                //    indexClassify = fields.IndexOf(mc.ClassifyField);
                //    indexGroupid = fields.IndexOf("GroupId");   
                    
                //}

                int index = fields.IndexOf("StyleId");
                if (index == -1) return;
                int mnindex = fields.IndexOf("ModelName");
                int mpindex = fields.IndexOf("Geometry");
                if (mpindex == -1) return;
                HashMap hm = SelectCollection.Instance().GetSelectGeometrys();
                if (hm != null && hm.Count == 1)
                {
                    IRowBufferCollection res = new RowBufferCollection();
                    IRowBufferCollection rowBufferCollection = hm[dffc] as IRowBufferCollection;
                    if (rowBufferCollection != null)
                    {
                        for (int i = 0; i < rowBufferCollection.Count; i++)
                        {
                            IRowBuffer rowBuffer = rowBufferCollection.Get(i);
                            Fac fac = null;
                            IModelPoint mp = null;
                            IModel finemodel = null;
                            IModel simplemodel = null;
                            string name = "";
                            switch (facc.Name)
                            {
                                case "PipeNode":
                                    fac = new PipeNodeFac(reg, style, rowBuffer, tc);
                                    break;
                                case "PipeLine":
                                    fac = new PipeLineFac(reg, style, rowBuffer, tc, false, false);
                                    break;
                                case "PipeBuild":
                                case "PipeBuild1":
                                    fac = new PipeBuildFac(reg, style, rowBuffer);
                                    break;
                            }
                            if (UCAuto3DCreate.RebuildModel(fac, style, out mp, out finemodel, out simplemodel, out name))
                            {
                                if (finemodel == null || mp == null) continue;
                                mp.ModelEnvelope = finemodel.Envelope;
                                rowBuffer.SetValue(mpindex, mp);
                                rowBuffer.SetValue(index, style.ObjectId);
                                //if (mc != null)
                                //{
                                //    if (indexClassify != -1 && indexGroupid != -1)
                                //    {

                                //    }
                                //}
                                bool bRes = false;
                                if (!string.IsNullOrEmpty(mp.ModelName))
                                {
                                    if (!manager.ModelExist(mp.ModelName))
                                    {
                                        if (manager.AddModel(mp.ModelName, finemodel, simplemodel))
                                        {
                                            bRes = true;
                                        }
                                    }
                                    else
                                    {
                                        if (manager.UpdateModel(mp.ModelName, finemodel) && manager.UpdateSimplifiedModel(mp.ModelName, simplemodel))
                                        {
                                            bRes = true;
                                        }
                                    }
                                }
                                if (bRes)
                                {
                                    res.Add(rowBuffer);
                                }
                            }
                        }
                    }
                    beforeRowBufferMap[dffc] = res;
                    //SelectCollection.Instance().Clear();
                    UpdateDatabase();
                    DF3DApplication app = DF3DApplication.Application;
                    if (app == null || app.Current3DMapControl == null) return; 
                    app.Current3DMapControl.FeatureManager.RefreshFeatureClass(fc);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void UpdateDatabase()
        {
            DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
            if (featureClassInfo == null) return;
            IFeatureClass fc = featureClassInfo.GetFeatureClass();
            if (fc == null) return;
            int count = SelectCollection.Instance().GetCount(false);
            EditParameters editParameters = new EditParameters(fc.Guid.ToString());
            editParameters.connectionInfo = CommonUtils.Instance().GetCurrentFeatureDataset().DataSource.ConnectionInfo.ToConnectionString();
            editParameters.datasetName = CommonUtils.Instance().GetCurrentFeatureDataset().Name;
            editParameters.geometryMap = beforeRowBufferMap;
            editParameters.nTotalCount = count;
            editParameters.TemproalTime = this.time;
            IBatcheEdit batcheEdit = BatchEditFactory.CreateBatchEdit(count);
            batcheEdit.BeginEdit();
            batcheEdit.DoWork(EditType.ET_UPDATE_GEOMETRY, editParameters);
            batcheEdit.EndEdit();
        }

        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            bool _isAuth = Authority3DService.Instance.IsAuthorized;
            if (!_isAuth) return;
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            SelectCollection.Instance().ClearRowBuffers();

            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
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
