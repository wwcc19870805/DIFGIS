using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Base;
using DF3DControl.Command;
using DF3DEdit.Service;
using DF3DEdit.Class;

namespace DF3DEdit.Command
{
    public class CmdCancelSelect : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            Map3DCommandManager.Push(this);
            RenderControlEditServices.Instance().StopGeometryEdit(true);
            //FDECommand fDECommand = new FDECommand(true, false);
            SelectCollection.Instance().Clear();
            SelectCollection.Instance().ClearRowBuffers();
            RenderControlEditServices.Instance().SetEditorPosition(null);
            //fDECommand.SetSelectionMap();
            //CommandManagerServices.Instance().CallCommand(fDECommand);
            app.Workbench.UpdateMenu();
        }

        public override void RestoreEnv()
        {

        }
    }
}
