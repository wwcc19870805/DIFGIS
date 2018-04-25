using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DEdit.Service;

namespace DF3DEdit.Command
{
    class CmdTemporal: AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            CommonUtils.Instance().EnableTemproalEdit = true;
        }

        public override void RestoreEnv()
        {
            CommonUtils.Instance().EnableTemproalEdit = false;
        }
    }
}
