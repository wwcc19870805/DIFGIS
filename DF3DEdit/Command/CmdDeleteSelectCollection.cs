using System;
using System.Collections;
using System.Windows.Forms;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Command;
using DF3DData.Class;
using DF3DEdit.Service;
using DF3DEdit.Interface;
using DF3DEdit.Class;
using DF3DEdit.Frm;

namespace DF3DEdit.Command
{
    public class CmdDeleteSelectCollection : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            RenderControlEditServices.Instance().StopGeometryEdit(true);
            System.DateTime temproalTime = System.DateTime.Now;
            if (CommonUtils.Instance().EnableTemproalEdit)
            {
                using (DateSettingDialog dateSettingDialog = new DateSettingDialog())
                {
                    if (dateSettingDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                    temproalTime = dateSettingDialog.Time;
                }
            }
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
            editParameters.TemproalTime = temproalTime;
            IBatcheEdit batcheEdit = BatchEditFactory.CreateBatchEdit(count);
            batcheEdit.BeginEdit();
            batcheEdit.DoWork(EditType.ET_DELETE_SELCTION, editParameters);
            batcheEdit.EndEdit();
        }

        public override void RestoreEnv()
        {
        }
    }
}
