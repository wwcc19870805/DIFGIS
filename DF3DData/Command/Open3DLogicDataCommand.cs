using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DData.UserControl.Pad;
using DFWinForms.Service;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DF3DControl.Base;
using DFCommon.Class;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace DF3DData.Command
{
    public class Open3DLogicDataCommand : AbstractMap3DCommand
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
                Layer3DTreePad pad = UCService.GetContent(typeof(Layer3DTreePad)) as Layer3DTreePad;
                if (pad == null) return;
                app.Workbench.SetStatusInfo("数据加载中…");

                SplashScreenManager.Default.SendCommand(null, "开始加载三维数据......");
                //加载地形数据
                string str3DTerrainConn = Config.GetConfigValue("3DTerrainConnStr");
                string str3DTerrainPwd = Config.GetConfigValue("3DTerrainPwd");
                if (!string.IsNullOrEmpty(str3DTerrainConn))
                {
                    SplashScreenManager.Default.SendCommand(null, "正在加载三维地形数据......");
                    DataUtils.AddAndVisualizeTerrainData(str3DTerrainConn, str3DTerrainPwd, pad.TreeList);
                }

                //加载专题数据 
                string str3DThematicDataConn = Config.GetConfigValue("3DThematicDataConnStr");
                if (!string.IsNullOrEmpty(str3DThematicDataConn))
                {
                    SplashScreenManager.Default.SendCommand(null, "正在加载三维专题数据......");
                    IConnectionInfo ci = new ConnectionInfo();
                    ci.FromConnectionString(str3DThematicDataConn);
                    DataUtils.AddAndVisualizeBaseData(ci, "Geometry", pad.TreeList, false, false);
                }

                //加载瓦片数据 
                string str3DTileConn = Config.GetConfigValue("3DTileConnStr");
                string str3DTilePwd = Config.GetConfigValue("3DTilePwd");
                if (!string.IsNullOrEmpty(str3DTileConn))
                {
                    SplashScreenManager.Default.SendCommand(null, "正在加载三维瓦片数据......");
                    DataUtils.AddAndVisualize3DTileData(str3DTileConn, str3DTilePwd, pad.TreeList);
                }

                //加载基础数据 
                string str3DBaseDataConn = Config.GetConfigValue("3DBaseDataConnStr");
                if (!string.IsNullOrEmpty(str3DBaseDataConn))
                {
                    SplashScreenManager.Default.SendCommand(null, "正在加载三维基础数据......");
                    IConnectionInfo ci = new ConnectionInfo();
                    ci.FromConnectionString(str3DBaseDataConn);
                    DataUtils.AddAndVisualizeBaseData(ci, "Geometry", pad.TreeList, false, false);
                }
                //加载管线数据 
                string str3DPipeDataConn = Config.GetConfigValue("3DPipeDataConnStr");
                if (!string.IsNullOrEmpty(str3DPipeDataConn))
                {
                    SplashScreenManager.Default.SendCommand(null, "正在加载三维管线数据......");
                    IConnectionInfo ciPipe = new ConnectionInfo();
                    ciPipe.FromConnectionString(str3DPipeDataConn);
                    DataUtils.AddAndVisualizePipeData(ciPipe, "Geometry", pad.TreeList, false, false);
                }
                app.Workbench.SetStatusInfo("就绪");
            }
            catch (Exception ex)
            {
                app.Workbench.SetStatusInfo("数据加载失败！");
            }
        }
    }
}
