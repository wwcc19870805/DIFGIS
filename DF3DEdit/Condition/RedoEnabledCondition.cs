using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DEdit.Service;
using DF3DEdit.Class;

namespace DF3DEdit.Condition
{
    public class RedoEnabledCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            //int count = SelectCollection.Instance().GetCount(false);
            //return count > 0 && CommandManagerServices.Instance().CanRedo();
            return  CommandManagerServices.Instance().CanRedo();
        }
    }
}
