using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using DFWinForms.LogicTree;

namespace DF3DData.Class
{
    public class DF3DFeatureClass : DFFeatureClass
    {
        //private IConnectionInfo connInfo;
        //private string dataSetName;
        //private string fcName;
        //private string fcGuid;
        private IFeatureClass fc;
        private IFeatureLayer fl;
        private IBaseLayer treeLayer;
        //public DF3DFeatureClass(IConnectionInfo connInfo, string dataSetName, string fcName, string fcGuid)
        //{
        //    this.connInfo = connInfo;
        //    this.dataSetName = dataSetName;
        //    this.fcName = fcName;
        //    this.fcGuid = fcGuid;
        //    string temp = Dictionary3DTable.Instance.GetFacilityClassNameByDFFeatureClassID(fcGuid);
        //    if (temp == null || temp == "") return;
        //    this.AttachFacilityClassByName(temp);
        //}

        public DF3DFeatureClass(IFeatureClass fc)
        {
            this.fc = fc;
            string temp = Dictionary3DTable.Instance.GetFacilityClassNameByDFFeatureClassID(fc.Guid.ToString());
            if (temp == null || temp == "") return;
            this.AttachFacilityClassByName(temp);
        }

        public IFeatureClass GetFeatureClass()
        {
            return this.fc;
        }
        public void SetFeatureLayer(IFeatureLayer fl)
        {
            this.fl = fl;
        }
        public IFeatureLayer GetFeatureLayer()
        {
            return this.fl;
        }
        public void SetTreeLayer(IBaseLayer bl)
        {
            this.treeLayer = bl;
        }
        public IBaseLayer GetTreeLayer()
        {
            return this.treeLayer;
        }
        public override string ToString()
        {
            if (this.fc == null) return "";
            return string.IsNullOrEmpty(this.fc.AliasName) ? this.fc.Name : this.fc.AliasName;
        }


    }
}
