using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF3DTool.Frm;

namespace DF3DTool.Command
{
    public class CmdImageExport : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmImageExport dlg = new FrmImageExport();
            dlg.ShowDialog();
        }

    }
}
