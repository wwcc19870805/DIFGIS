using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using System.Runtime.InteropServices;

namespace DF3DPipeCreateTool.Class
{
    public class DataObject
    {
        // Fields
        private string _constr;
        private string _datasetName;
        private gviDataSetType _datasetType;
        private Guid _dsId;
        private Guid _guid;
        private string _name;
        private IObjectClass _oc;

        // Methods
        public DataObject()
        {
        }

        public DataObject(IObjectClass oc)
        {
            if (oc != null)
            {
                this._dsId = oc.DataSource.Guid;
                this._constr = oc.DataSource.ConnectionInfo.ToConnectionString();
                this._guid = oc.Guid;
                this._datasetName = oc.FeatureDataSet.Name;
                this._name = oc.Name;
                this._oc = oc;
                if (oc is IFeatureClass)
                {
                    this._datasetType = gviDataSetType.gviDataSetFeatureClassTable;
                }
                else
                {
                    this._datasetType = gviDataSetType.gviDataSetObjectClassTable;
                }
            }
        }

        public IObjectClass GetData()
        {
            IObjectClass class1 = this._oc;
            return this._oc;
        }

        public void Release()
        {
            if (this._oc != null)
            {
                Marshal.ReleaseComObject(this._oc);
                this._oc = null;
            }
        }

        // Properties
        public string ConnectString
        {
            get
            {
                return this._constr;
            }
            set
            {
                this._constr = value;
            }
        }

        public string DataSetName
        {
            get
            {
                return this._datasetName;
            }
            set
            {
                this._datasetName = value;
            }
        }

        public gviDataSetType DataSetType
        {
            get
            {
                return this._datasetType;
            }
            set
            {
                this._datasetType = value;
            }
        }

        public Guid DataSourceId
        {
            get
            {
                return this._dsId;
            }
            set
            {
                this._dsId = value;
            }
        }

        public Guid Guid
        {
            get
            {
                return this._guid;
            }
            set
            {
                this._guid = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        // Nested Types
        public class Finder
        {
            // Fields
            private string _datasetName;
            private Guid _dsId;
            private Guid _guid;
            private string _name;

            // Methods
            public bool FindByDataSourceId(DataObject data)
            {
                return (this._dsId == data.DataSourceId);
            }

            public bool FindByGuid(DataObject data)
            {
                return (this._guid == data.Guid);
            }

            public bool FinderByName(DataObject data)
            {
                return (((this._dsId == data.DataSourceId) && (this._datasetName == data.DataSetName)) && (this._name == data.Name));
            }

            public void SetByName(Guid dsid, string datasetName, string name)
            {
                this._dsId = dsid;
                this._datasetName = datasetName;
                this._name = name;
            }

            public void SetDataSourceId(Guid dsid)
            {
                this._dsId = dsid;
            }

            public void SetGuid(Guid guid)
            {
                this._guid = guid;
            }

            // Properties
            public string DataSetName
            {
                get
                {
                    return this._datasetName;
                }
                set
                {
                    this._datasetName = value;
                }
            }

            public Guid DataSourceId
            {
                get
                {
                    return this._dsId;
                }
                set
                {
                    this._dsId = value;
                }
            }

            public Guid Guid
            {
                get
                {
                    return this._guid;
                }
                set
                {
                    this._guid = value;
                }
            }

            public string Name
            {
                get
                {
                    return this._name;
                }
                set
                {
                    this._name = value;
                }
            }
        }
    }
}
