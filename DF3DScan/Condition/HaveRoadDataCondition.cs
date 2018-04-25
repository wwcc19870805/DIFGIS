using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF3DControl.Base;
using DFDataConfig.Class;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;

namespace DF3DScan.Condition
{
    public class HaveRoadDataCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName("Road");
            if (list == null || list.Count == 0) return false;
            foreach (DF3DFeatureClass dffc in list)
            {
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc != null) return true;
            }
            return false;
        }
    }
}
