using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;

namespace DF3DScan.Condition
{
    public class ElevationCondition: IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Current3DMapControl.Terrain == null) return true;
            return !(app.Current3DMapControl.Terrain.IsRegistered && (app.Current3DMapControl.Terrain.VisibleMask != gviViewportMask.gviViewNone) && app.Current3DMapControl.Terrain.DemAvailable);
        }
    }
}
