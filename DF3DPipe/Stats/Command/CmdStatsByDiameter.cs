using System;
using System.Collections.Generic;
using System.Linq;
using DFWinForms.Command;
using DF3DPipe.Stats.Frm;

namespace DF3DPipe.Stats.Command
{
    public class CmdStatsByDiameter : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmPropertyStatsByDiameter dialog = new FrmPropertyStatsByDiameter(this.CommandName, "PipeLine");
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        }
    }
}
