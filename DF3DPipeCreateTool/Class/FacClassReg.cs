using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;
using Gvitech.CityMaker.FdeCore;

namespace DF3DPipeCreateTool.Class
{
    public class FacClassReg
    {
        private int _id;
        private string _facClassCode;
        private string _name;
        private string _dataSetName;
        private string _featureClassId;
        private string _fcName;
        private DataLifeCyle _dataType;
        private FacilityClass _facilityType;
        private LocationType _locationType;
        private TurnerStyle _turnerStyle;
        private string _comment;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string FacClassCode
        {
            get { return this._facClassCode; }
            set { this._facClassCode = value; }
        }
        public string DataSetName
        {
            get { return this._dataSetName; }
            set { this._dataSetName = value; }
        }

        public string FeatureClassId
        {
            get { return this._featureClassId; }
            set { this._featureClassId = value; }
        }

        public string FcName
        {
            get { return this._fcName; }
            set { this._fcName = value; }
        }

        public DataLifeCyle DataType
        {
            get { return this._dataType; }
            set { this._dataType = value; }
        }

        public string Comment
        {
            get { return this._comment; }
            set { this._comment = value; }
        }

        public FacilityClass FacilityType
        {
            get { return this._facilityType; }
            set { this._facilityType = value; }
        }


        public LocationType LocationType
        {
            get { return this._locationType; }
            set { this._locationType = value; }
        }

        public TurnerStyle TurnerStyle
        {
            get { return this._turnerStyle; }
            set { this._turnerStyle = value; }
        }

        public override string ToString()
        {
            return this._name;
        }

        public IFeatureClass GetFeatureClass()
        {
            IDataSource ds = DF3DPipeCreateApp.App.PipeLib;
            if (ds == null) return null;
            try
            {
                IFeatureDataSet fds = ds.OpenFeatureDataset(this._dataSetName);
                if (fds == null) return null;
                return fds.OpenFeatureClass(this._fcName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
