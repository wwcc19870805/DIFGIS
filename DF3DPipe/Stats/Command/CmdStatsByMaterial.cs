using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF3DPipe.Stats.Frm;

namespace DF3DPipe.Stats.Command
{
    public class CmdStatsByMaterial : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmPropertyStats dialog = new FrmPropertyStats(this.CommandName, "Material", "PipeLine");
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        }
    }
}
