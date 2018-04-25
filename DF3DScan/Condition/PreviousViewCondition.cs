using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF3DControl.Base;

namespace DF3DScan.Condition
{
    public class PreviousViewCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Current3DMapControl.Camera == null) return false;
            return app.Current3DMapControl.Camera.CanUndo;
        }
    }
}
