using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;

namespace DF2DEdit.Condition
{
    class PntAddEnabledCondition : IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return false;
            IFeatureLayer pCurLayer = Class.Common.CurEditLayer as IFeatureLayer;
            if (pCurLayer == null) return false;

            if (pCurLayer.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                return true;
            else
                return false;
        }
    }
}
