using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Base;
using DevExpress.XtraEditors;
using DF3DControl.Command;
using DFWinForms.Service;
using DF3DPlanData.UC;
using DevExpress.XtraSplashScreen;
using Gvitech.CityMaker.FdeCore;
using DF3DPlanData.Class;
using DFCommon.Class;

namespace DF3DPlanData.Cmd
{
    public class Open3DPlanDataCommand : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = (DF3DApplication)this.Hook;
            if (app == null || app.Current3DMapControl == null) return;
            if (!app.IsInit3DControl)
            {
                XtraMessageBox.Show("三维空间初始化失败！", "提示");
                return;
            }
            try
            {
                Layer3DPlanTreePad pad = UCService.GetContent(typeof(Layer3DPlanTreePad)) as Layer3DPlanTreePad;
                if (pad == null) return;
                app.Workbench.SetStatusInfo("规划数据加载中…");

                SplashScreenManager.Default.SendCommand(null, "开始加载三维规划数据......");
                string str3DPlanDataConn = Config.GetConfigValue("3DPlanDataConnStr");
                if (!string.IsNullOrEmpty(str3DPlanDataConn))
                {
                    IConnectionInfo ci = new ConnectionInfo();
                    ci.FromConnectionString(str3DPlanDataConn);
                    DataUtils.Add3DPlanData(ci, pad, pad.TreeList);
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}
