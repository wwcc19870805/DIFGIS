using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using ICSharpCode.Core;

namespace DFSysManage.Command
{
    public class CmdHelp : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            try
            {
                string fileName = string.Empty;
                fileName = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\Help\\help." + ResourceService.Language + ".chm");
                new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo(fileName)
                }.Start();
            }
            catch (Exception)
            {
            }
        }
    }
}
