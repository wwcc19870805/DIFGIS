using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Base;
using ICSharpCode.Core;
using DF3DData.Class;
using DF3DEdit.Service;
using DF3DEdit.Class;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;

namespace DF3DEdit.Condition
{
    class ShapeEditEnabledCondition: IConditionEvaluator
    {
        public bool IsValid(object caller, ICSharpCode.Core.Condition condition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
            if (dffc == null) return false;
           IFeatureClass fc = dffc.GetFeatureClass();
            if (fc == null) return false;
            IFeatureLayer fl = dffc.GetFeatureLayer();
            bool b1 = false;
            if (fl != null && (fl.GeometryType == gviGeometryColumnType.gviGeometryColumnPolygon || fl.GeometryType == gviGeometryColumnType.gviGeometryColumnPolyline))
                b1 = true;
            int count = SelectCollection.Instance().GetCount(false);
            if (count != 1) b1 &= false;
            return b1;
        }
    }
}
