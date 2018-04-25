using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using ICSharpCode.Core;
using DFUser.Frm;

namespace DFUser.Command
{
    public class CmdRoleManage:AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmRoleManage dlg = new FrmRoleManage();
            dlg.ShowDialog();
        }
    }
}
