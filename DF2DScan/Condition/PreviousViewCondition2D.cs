using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;

namespace DF2DScan.Condition
{
    public class PreviousViewCondition2D : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null ) return false;
            IExtentStack pExtentStack = app.Current2DMapControl.ActiveView.ExtentStack;
            return pExtentStack.CanUndo();
        }
    }
}
