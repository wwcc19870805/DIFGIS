using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DFWinForms.Base;
using DFCommon.Class;

namespace DF3DInit.Command
{
    public class Init3DCommand : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            DFApplication.Application.Workbench.BarPerformClick("InitView3D");// 初始视角

            string isInitHideElevation = Config.GetConfigValue("IsInitHideElevation");
            if (isInitHideElevation.ToLower() == "true")
                DFApplication.Application.Workbench.BarPerformClick("HideElevation");

        }
    }
}
