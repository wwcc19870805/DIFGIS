﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DEdit.Service;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;

namespace DF3DEdit.Condition
{
    public class Add3DModelEnabledCondition : IConditionEvaluator
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
                    if (facType != "PipeLine" && facType != "PipeNode" && facType != "PipeBuild" && facType != "PipeBuild1")
                    {
                        IFeatureLayer fl = dffc.GetFeatureLayer();
                        if (fl != null)
                        {
                            if (fl.GeometryType == gviGeometryColumnType.gviGeometryColumnModelPoint) return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
