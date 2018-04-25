﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Base;
using DF3DControl.Command;
using DF3DEdit.Service;
using DF3DEdit.Class;

namespace DF3DEdit.Command
{
    public class CmdRedo : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            try
            {
                RenderControlEditServices.Instance().StopGeometryEdit(true);
                app.Current3DMapControl.PauseRendering(false);
                using (new UndoRedoOperator(1))
                {
                    CommandManagerServices.Instance().Redo();
                    app.Workbench.UpdateMenu();
                }
            }
            catch (System.Exception)
            {
            }
            finally
            {
                app.Current3DMapControl.ResumeRendering();
            }
        }

        public override void RestoreEnv()
        {

        }
    }
}
