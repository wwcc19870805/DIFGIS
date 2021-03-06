﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DEdit.Service;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using DF3DData.Class;

namespace DF3DEdit.Condition
{
    public class AddPipeBuildEnabledCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            if (CommonUtils.Instance().CurEditLayer != null)
            {
                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc != null)
                {
                    string facType = dffc.GetFacilityClassName();
                    if (facType != null && (facType == "PipeBuild" || facType == "PipeBuild1")) return true;
                }
            }
            return false;

        }
    }
}
