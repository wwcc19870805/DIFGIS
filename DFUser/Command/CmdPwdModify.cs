using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using ICSharpCode.Core;
using DFUser.Frm;

namespace DFUser.Command
{
    public class CmdPwdModify : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmChangePwd dlg = new FrmChangePwd();
            dlg.ShowDialog();
        }
    }
}
