using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DEdit.Service;

namespace DF3DEdit.Condition
{
    public class SelectionEnabledCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            if (CommonUtils.Instance().CurEditLayer != null) return true;
            return false;
        }
    }
}
