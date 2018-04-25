using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF2DEdit.Class;
using DF2DControl.Base;

namespace DF2DEdit.Condition
{
    class SelectionEnabledCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return false;

            if (Common.CurEditLayer == null)
                return false;
            else
                return true;
        }
    }
}
