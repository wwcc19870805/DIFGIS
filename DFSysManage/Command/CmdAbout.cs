using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DevExpress.XtraEditors;

namespace DFSysManage.Command
{
    public class CmdAbout : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            XtraMessageBox.Show("Copyright © 2017 All Rights Reserved. 版权所有：中冶集团武汉勘察研究院有限公司", "提示");
        }
    }
}
