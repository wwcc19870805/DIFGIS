using System;
using System.Collections.Generic;
using System.Linq;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Class;
using DF3DEdit.Service;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DFWinForms.LogicTree;
using Gvitech.CityMaker.RenderControl;

namespace DF3DEdit.Command
{
    public class CmdSetCurEditLayer : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                DF3DApplication app = (DF3DApplication)this.Hook;
                if (app == null || app.Workbench == null) return;
                app.Workbench.BarPerformClick("CancelSelection");
                ComboBoxEdit cbEdit = sender as ComboBoxEdit;
                CboItem item = cbEdit.SelectedItem as CboItem;
                CommonUtils.Instance().SetCurEditLayer(item == null ? null : item.Value);
                app.Workbench.UpdateMenu();
                if (item != null && item.Value != null)
                {
                    IBaseLayer layer = item.Value.GetTreeLayer();
                    if (layer != null)
                    {
                        layer.Visible = true;
                    }
                    else
                    {
                        IFeatureLayer fl = item.Value.GetFeatureLayer();
                        if (fl != null)
                        {
                            fl.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void Init(object sender)
        {
            List<DF3DFeatureClass> dffcList = DF3DFeatureClassManager.Instance.GetAllFeatureClass();
            if (dffcList != null && dffcList.Count > 0)
            {
                BarEditItem item = sender as BarEditItem;
                if (item.Edit is RepositoryItemComboBox)
                {
                    RepositoryItemComboBox ricb = item.Edit as RepositoryItemComboBox;
                    ricb.NullText = "(请选择)";
                    ricb.NullValuePrompt = "(请选择)";
                    ricb.Items.Clear();
                    ricb.Items.Insert(0, "(清空选择)");
                    for (int i = 0; i < dffcList.Count; i++)
                    {
                        DF3DFeatureClass dffc = dffcList[i];
                        CboItem cboItem = new CboItem(dffc.ToString(), dffc);
                        ricb.Items.Add(cboItem);
                    }
                }

            }
        }
    }
}
