using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DDocumentManage.Frm;

namespace DF2DDocumentManage.Command
{
    public class CmdImportAllFiles : AbstractMap2DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmImportAllFiles dialog = new FrmImportAllFiles();
            dialog.ShowDialog();
        }
    }
}
