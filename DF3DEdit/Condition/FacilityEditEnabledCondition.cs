using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DEdit.Class;
using DF3DEdit.Service;
using DF3DData.Class;

namespace DF3DEdit.Condition
{
    public class FacilityEditEnabledCondition : IConditionEvaluator
    {
         public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            int count = SelectCollection.Instance().GetCount(false);
            bool bCount = count > 0;
            bool bFacility = false; 
            if (CommonUtils.Instance().CurEditLayer != null)
            {
                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc != null)
                {
                    string str = dffc.GetFacilityClassName();
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (str == "PipeLine" || str == "PipeNode" || str == "PipeBuild" || str == "PipeBuild1")
                        {
                            bFacility = true;
                        }
                    }
                }
            }
            return bCount && bFacility;
        }
    }
}
