using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DEdit.Class;

namespace DF3DEdit.Condition
{
    public class MergeEnabledCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            HashMap featureClassInfoMap = SelectCollection.Instance().FeatureClassInfoMap;
            if (featureClassInfoMap != null && featureClassInfoMap.Count == 1)
            {
                int count = SelectCollection.Instance().GetCount(true);
                if (count > 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
