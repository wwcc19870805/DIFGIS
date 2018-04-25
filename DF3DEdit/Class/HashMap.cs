using System;
using System.Collections;
using System.Linq;
using DF3DData.Class;

namespace DF3DEdit.Class
{
    public class HashMap : System.Collections.Hashtable
    {
        public override object this[object key]
        {
            get
            {
                DF3DFeatureClass featureClassInfo = key as DF3DFeatureClass;
                if (featureClassInfo == null)
                {
                    return null;
                }
                foreach (DF3DFeatureClass featureClassInfo2 in this.Keys)
                {
                    if (featureClassInfo2.GetFeatureLayer().Guid == featureClassInfo.GetFeatureLayer().Guid)
                    {
                        return base[featureClassInfo2];
                    }
                }
                return null;
            }
            set
            {
                base[key] = value;
            }
        }
        public object this[int i]
        {
            get
            {
                object result = null;
                int num = 0;
                foreach (DF3DFeatureClass featureClassInfo in this.Keys)
                {
                    if (num == i)
                    {
                        result = featureClassInfo;
                        break;
                    }
                    num++;
                }
                return result;
            }
        }
        public override bool Contains(object key)
        {
            DF3DFeatureClass cfg = key as DF3DFeatureClass;
            return cfg != null && (this.Keys.Cast<DF3DFeatureClass>().Any((DF3DFeatureClass info) => cfg.GetFeatureLayer().Guid == info.GetFeatureLayer().Guid) || base.Contains(key));
        }
        public object FindObject(string fLayerGuid)
        {
            object result = null;
            foreach (DF3DFeatureClass featureClassInfo in this.Keys)
            {
                if (featureClassInfo.GetFeatureLayer().Guid.ToString() == fLayerGuid)
                {
                    return base[featureClassInfo];
                }
            }
            return result;
        }
    }
}
