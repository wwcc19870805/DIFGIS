using System;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Service;
using DF3DEdit.Class;
using DF3DEdit.Frm;

namespace DF3DEdit.Command
{
    public class CmdSplit : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            if (CommonUtils.Instance().CheckCurrentFeatureDatasetEncrypted())
            {
                XtraMessageBox.Show("数据集已加密!", "提示信息");
            }
            else
            {
                Map3DCommandManager.Push(this);
                RenderControlEditServices.Instance().StopGeometryEdit(true);
                //WorkSpaceServices.Instance().PropertyCanEdit = true;
                SplitDlg splitDlg = new SplitDlg();
                splitDlg.Show(DF3DApplication.Application.Workbench);
            }
        }

        public override void RestoreEnv()
        {
        }
    }
}
