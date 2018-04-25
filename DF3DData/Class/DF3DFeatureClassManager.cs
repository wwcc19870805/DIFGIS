using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;
using Gvitech.CityMaker.FdeCore;

namespace DF3DData.Class
{
    public class DF3DFeatureClassManager
    {
        private static DF3DFeatureClassManager instance = null;
        private static readonly object syncRoot = new object();
        private List<DF3DFeatureClass> listFC;
        private DF3DFeatureClassManager()
        {
            this.listFC = new List<DF3DFeatureClass>();
        }

        public static DF3DFeatureClassManager Instance
        {
            get
            {
                if (DF3DFeatureClassManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (DF3DFeatureClassManager.instance == null)
                        {
                            DF3DFeatureClassManager.instance = new DF3DFeatureClassManager();
                        }
                    }
                }
                return DF3DFeatureClassManager.instance;
            }
        }

        public bool Exists(string id)
        {
            foreach (DF3DFeatureClass fc in this.listFC)
            {
                if (fc.GetFeatureClass().Guid.ToString() == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Exists(DF3DFeatureClass fc)
        {
            return Exists(fc.GetFeatureClass().Guid.ToString());
        }

        public DF3DFeatureClass GetFeatureClassByID(string id)
        {
            foreach (DF3DFeatureClass fc in this.listFC)
            {
                if (fc.GetFeatureClass().Guid.ToString() == id)
                {
                    return fc;
                }
            }
            return null;
        }

        public void Add(DF3DFeatureClass fc)
        {
            if (this.Exists(fc)) return;
            this.listFC.Add(fc);
        }

        public List<DF3DFeatureClass> GetAllFeatureClass()
        {
            return this.listFC;
        }

        public List<DF3DFeatureClass> GetFeatureClassByFacilityClassName(string facName)
        {
            List<DF3DFeatureClass> list = new List<DF3DFeatureClass>();
            foreach (DF3DFeatureClass dffc in this.listFC)
            {
                if (dffc.GetFacilityClassName() == facName) list.Add(dffc);
            }
            return list;
        }

    }
}
