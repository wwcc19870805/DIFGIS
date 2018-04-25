using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFDataConfig.Class
{
    public class DFFeatureClass
    {
        private FacilityClass facilityClass;

        public void AttachFacilityClassByName(string name)
        {
            FacilityClass fc = FacilityClassManager.Instance.GetFacilityClassByName(name);
            this.AttachFacilityClass(fc);
        }

        public void AttachFacilityClass(FacilityClass facilityClass)
        {
            this.facilityClass = facilityClass;
        }

        public void DettachFacilityClass()
        {
            this.facilityClass = null;
        }

        public FacilityClass GetFacilityClass()
        {
            return this.facilityClass;
        }

        public string GetFacilityClassName()
        {
            if (this.facilityClass == null) return "";
            return this.facilityClass.Name;
        }
    }
}
