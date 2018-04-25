using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;
using System.Drawing;
using DFWinForms.Command;
using DF3DPipe.Stats.Frm;

namespace DF3DPipe.Stats.Command
{
    public class CmdStatsByAdditional : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmPropertyStats dialog = new FrmPropertyStats(this.CommandName, "Additional", "PipeNode");
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        }
    }
}
