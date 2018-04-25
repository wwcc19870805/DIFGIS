using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;

namespace DF3DPipeCreateTool.Class
{
    public class CMFieldConfig : DFDataConfig.Class.FieldInfo
    {
        private gviFieldType _fieldType;
        private int _id;
        private int _length;
        private bool _nullable;
        private string _facClassCode;

        public CMFieldConfig(string name = "", string alias = "", string systemName = "", string systemAlias = "", bool bCanQuery = false)
            : base(name, alias, systemName, systemAlias, bCanQuery)
        {

        }

        public gviFieldType FieldType
        {
            get
            {
                return this._fieldType;
            }
            set
            {
                this._fieldType = value;
            }
        }

        public string FacClassCode
        {
            get
            {
                return this._facClassCode;
            }
            set
            {
                this._facClassCode = value;
            }
        }

        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }

        public bool Nullable
        {
            get
            {
                return this._nullable;
            }
            set
            {
                this._nullable = value;
            }
        }

    }
}
