using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DFWinForms.Base;

namespace DFSysManage.Command
{
    class CmdExit : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            DFApplication app = DFApplication.Application;
            if (app == null || app.Workbench == null) return;
            app.Workbench.CloseWorkBench();
        }
    }
}
