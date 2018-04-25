using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF3DPipe.Stats.Frm;
using DFDataConfig.Logic;

namespace DF3DPipe.Stats.Command
{
    public class CmdCompoundConditionStats : AbstractCommand
    {

        public override void Run(object sender, EventArgs e)
        {
            FrmCompoundConditionStats dialog = new FrmCompoundConditionStats();
            dialog.SetData(LogicDataStructureManage3D.Instance.RootLogicGroups, LogicDataStructureManage3D.Instance.RootMajorClasses, null);
            dialog.ShowDialog();
        }

    }
}
