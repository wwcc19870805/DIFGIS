using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DScan.Frm;
using DF3DControl.Base;

namespace DF3DScan.Command
{
    public class CmdNormalRegionQuery : AbstractMap3DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            Map3DCommandManager.Push(this);
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null) return;

            app.Workbench.SetMenuEnable(false);

            FrmNormalRegionQuery.Instance.Show();
            FrmNormalRegionQuery.Instance.Init();
            FrmNormalRegionQuery.Instance.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Instance_FormClosed);
        }

        void Instance_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null) return;

            app.Workbench.SetMenuEnable(true);

            FrmNormalRegionQuery.Instance.FormClosed -= new System.Windows.Forms.FormClosedEventHandler(Instance_FormClosed);
        }

    }
}
