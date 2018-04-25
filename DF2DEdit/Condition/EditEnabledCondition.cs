using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;

namespace DF2DEdit.Condition
{
    class EditEnabledCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return false;

            IMap pMap = app.Current2DMapControl.ActiveView.FocusMap;
            int count = pMap.SelectionCount;

            return count > 0;
        }
    }
}
