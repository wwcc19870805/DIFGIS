using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DEdit.Class;
using DF3DControl.Base;
using DF3DEdit.Service;
using DevExpress.XtraEditors;
using DFWinForms.Class;
using DF3DEdit.UC;
using DevExpress.XtraBars.Docking;
using DF3DEdit.Frm;
using Gvitech.CityMaker.FdeCore;
using DF3DData.Class;
using System.Data;
using DF3DEdit.Interface;

namespace DF3DEdit.Command
{
    class CmdProperty : AbstractMap3DCommand
    {
        private System.DateTime time;
        private DockPanel _dockPanel;
        private UIDockPanel _uPanel;
        private UCPropertyEdit _uc;
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
            //Map3DCommandManager.Push(this);
            this._uPanel = new UIDockPanel("属性编辑", "属性编辑", this.Location, this._width, this._height);
            this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
            this._dockPanel.Visibility = DockVisibility.Visible;
            this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
            this._dockPanel.Width = this._width;
            this._dockPanel.Height = this._height;
            this._uc = new UCPropertyEdit();
            this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uPanel.RegisterEvent(new PanelClose(this.Close));
            this._dockPanel.Controls.Add(this._uc);

            SelectCollection.Instance().PropertyTableSelectionChangedEvent += new Delegate.PropertyTableSelectionChangedEventHandler(CmdProperty_PropertyTableSelectionChangedEvent);
        }

        private System.Collections.Generic.IList<RegexDataStruct> regexDataList;
        private string colName;
        private int[] fids;
        private void CmdProperty_PropertyTableSelectionChangedEvent(DataRow dr)
        {
            try
            {
                if (regexDataList != null) regexDataList.Clear();
                colName = "";
                fids = null;                
                if (dr == null) return;

                colName = (dr["F"] as IFieldInfo).Name;
                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if(dffc == null) return;
                HashMap hm =  SelectCollection.Instance().GetSelectGeometrys();
                if (hm != null && hm.Count == 1)
                {
                    IRowBufferCollection rowBufferCollection = hm[dffc] as IRowBufferCollection;
                    if (rowBufferCollection != null)
                    {
                        List<int> listFids = SelectCollection.Instance().GetOIDList(rowBufferCollection);
                        if (listFids != null)
                        {
                            fids = listFids.ToArray<int>();
                            if (dr["FV"] == null) regexDataList = null;
                            {
                                if (regexDataList == null) regexDataList = new List<RegexDataStruct>();
                                RegexDataStruct rds = new RegexDataStruct(dr["FV"].ToString(), CharactorType.ConstKey);
                                regexDataList.Add(rds);
                            }
                            UpdateDatabase();
                        }
                    }
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
            editParameters.regexDataList = regexDataList;
            editParameters.colName = colName;
            editParameters.fidList = fids;
            editParameters.nTotalCount = count;
            editParameters.TemproalTime = this.time;
            IBatcheEdit batcheEdit = BatchEditFactory.CreateBatchEdit(count);
            batcheEdit.BeginEdit();
            batcheEdit.DoWork(EditType.ET_UPDATE_ATTRIBUTE, editParameters);
            batcheEdit.EndEdit();
        }

        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;

            SelectCollection.Instance().PropertyTableSelectionChangedEvent -= new Delegate.PropertyTableSelectionChangedEventHandler(CmdProperty_PropertyTableSelectionChangedEvent);

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
