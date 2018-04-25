using System;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using DF3DData.Class;

namespace DF3DEdit.Class
{
    public class EditParameters
    {
        public string featureLayerGuid;
        public string featureClassGuid;
        public string connectionInfo;
        public string datasetName;
        public string fcName;
        public int nTotalCount;
        public System.DateTime TemproalTime;
        public string strWhere;
        public int[] fidList;
        public string colName;
        public System.Collections.Generic.IList<RegexDataStruct> regexDataList;
        public System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> geometryMap;
        public EditParameters(string fClassGuid)
        {
            this.featureClassGuid = fClassGuid;
        }
        //public EditParameters(string fLayerGuid)
        //{
        //    this.featureLayerGuid = fLayerGuid;
        //}
    }
}
