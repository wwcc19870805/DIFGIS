using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DEdit.Frm;
using DF3DEdit.Class;
using DF3DEdit.Service;

namespace DF3DEdit.Command
{
    public class CmdCurEditLayerProperty : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            if (CommonUtils.Instance().CurEditLayer != null)
            {
                bool inProjectTree = false;
                bool isCheckOut = false;
                IFeatureLayer fl = CommonUtils.Instance().CurEditLayer.GetFeatureLayer();
                IDataSourceFactory dsf = new DataSourceFactory();
                if (dsf.HasDataSourceByString(fl.FeatureClassInfo.DataSourceConnectionString))
                {
                    IDataSource ds = dsf.OpenDataSourceByString(fl.FeatureClassInfo.DataSourceConnectionString);
                    IFeatureDataSet fds = ds.OpenFeatureDataset(fl.FeatureClassInfo.DataSetName);
                    if (fds != null)
                    {
                        using (FCPropertyFrm fCPropertyForm = new FCPropertyFrm(ds, fl.FeatureClassInfo.DataSetName, CommonUtils.Instance().CurEditLayer.GetFeatureClass(), inProjectTree, isCheckOut))
                        {
                            fCPropertyForm.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("请设置当前编辑要素类!");
            }
        }
    }
}
