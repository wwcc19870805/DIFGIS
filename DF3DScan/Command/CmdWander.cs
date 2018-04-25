using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DData.Class;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using ICSharpCode.Core;
namespace DF3DScan.Command
{
    public class CmdWander : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractNormal;
            app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
            app.Current3DMapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            app.Current3DMapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove; 
        }
    }
}
