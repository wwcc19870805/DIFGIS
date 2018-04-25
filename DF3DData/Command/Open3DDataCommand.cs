using System;
using System.Collections.Generic;
using System.Linq;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.RenderControl;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;
using System.Collections;
using Gvitech.CityMaker.Math;
using DF3DControl.Command;
using ICSharpCode.Core;
using DF3DControl.Base;
using DFDataConfig.Class;
using DF3DData.Class;
using Gvitech.CityMaker.Controls;
using DF3DData.Tree;
using DFWinForms.Service;
using DF3DData.UserControl.Pad;
using DFCommon.Class;
using DevExpress.XtraEditors;

namespace DF3DData.Command
{
    public class Open3DDataCommand : AbstractMap3DCommand
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

                //加载地形数据
                string str3DTerrainConn = Config.GetConfigValue("3DTerrainConnStr");
                string str3DTerrainPwd = Config.GetConfigValue("3DTerrainPwd");
                if (!string.IsNullOrEmpty(str3DTerrainConn))
                {
                    DataUtils.AddAndVisualizeTerrainData(str3DTerrainConn, str3DTerrainPwd, pad.TreeList);
                }

                //加载基础数据 
                string str3DBaseDataConn = Config.GetConfigValue("3DBaseDataConnStr");
                if (!string.IsNullOrEmpty(str3DBaseDataConn))
                {
                    IConnectionInfo ci = new ConnectionInfo();
                    ci.FromConnectionString(str3DBaseDataConn);
                    DataUtils.AddAndVisualizeBaseData(ci, "Geometry", pad.TreeList, false, false);
                }

                //加载管线数据 
                string str3DTemplateDataConn = Config.GetConfigValue("3DTemplateDataConnStr");
                if (!string.IsNullOrEmpty(str3DTemplateDataConn))
                {
                    IConnectionInfo ciTemplate = new ConnectionInfo();
                    ciTemplate.FromConnectionString(str3DTemplateDataConn);

                    Dictionary<string, IRowBuffer> pipeInfos = DataUtils.GetPipeInfos(ciTemplate, "DataSet_BIZ", "OC_Catalog");
                    Dictionary<string, string> pipeCatelog = DataUtils.GetPipeCatelog(ciTemplate, "DataSet_BIZ", "OC_Catalog");

                    string str3DPipeDataConn = Config.GetConfigValue("3DPipeDataConnStr");
                    if (!string.IsNullOrEmpty(str3DPipeDataConn))
                    {
                        IConnectionInfo ciPipe = new ConnectionInfo();
                        ciPipe.FromConnectionString(str3DPipeDataConn);
                        DataUtils.AddAndVisualizePipeData(ciPipe, "Geometry", pad.TreeList, pipeCatelog, pipeInfos, false, false);
                    }                    
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
