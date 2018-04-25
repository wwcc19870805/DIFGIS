using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using ICSharpCode.Core;
using DF3DPlan.UC;
using DevExpress.XtraBars.Docking;
using DFWinForms.Class;
using DF3DPlan.Frm;

namespace DF3DPlan.Command
{
    public  class CmdSunSimulation : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            FrmSunSimulation.Instance.Show();
        }


    }
}
