using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DScan.Frm;

namespace DF3DScan.Command
{
    public class CmdLocByCoordinate : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            FormLocByCoordinate.Instance.Show();
        }
    }
}
