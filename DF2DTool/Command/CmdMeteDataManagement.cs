using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DTool.Frm;

namespace DF2DTool.Command
{
    public class CmdMeteDataManagement : AbstractMap2DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmMetaDataManagement dialog = new FrmMetaDataManagement();
            dialog.Show();
        }
    }
}
