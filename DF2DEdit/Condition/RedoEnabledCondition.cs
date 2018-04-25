using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DEdit.Condition
{
    class RedoEnabledCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            bool hasRedos = false;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return false;

            IWorkspaceEdit pWorkspaceEdit = Class.Common.CurWspEdit;
            if (pWorkspaceEdit == null) return false;

            if (pWorkspaceEdit.IsBeingEdited())
            {
                pWorkspaceEdit.HasRedos(ref hasRedos);
                if (hasRedos)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
