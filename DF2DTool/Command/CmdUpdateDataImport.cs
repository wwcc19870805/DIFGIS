using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DTool.Frm;

namespace DF2DTool.Command
{
    public class CmdUpdateDataImport : AbstractMap2DCommand
    {

        public override void Run(object sender, System.EventArgs e)
        {
            FrmImportUpdate dialog = new FrmImportUpdate();
            dialog.ShowDialog();
         
        }

      
    }
}
