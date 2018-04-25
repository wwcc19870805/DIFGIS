using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;

namespace DF3DPipeCreateTool.Class
{
    public class FacClass
    {
        private string _code;             // 编码
        private string _comment;          // 属性值
        private int _id;                  // ID
        private string _name;             // 名称
        private string _pCode;            // 父节点编码
        private string _topoLayerId;    // 拓扑层ID
        private bool _bGroup;
        private FacilityClass _facilityType;
        private LocationType _locationType;
        private TurnerStyle _turnerStyle;

        public override string ToString()
        {
            return this._name;
        }
        public bool IsGroup
        {
            get
            {
                return this._bGroup;
            }
            set
            {
                this._bGroup = value;
            }
        }
        public FacilityClass FacilityType
        {
            get
            {
                return this._facilityType;
            }
            set
            {
                this._facilityType = value;
            }
        }
        public LocationType LocationType
        {
            get
            {
                return this._locationType;
            }
            set
            {
                this._locationType = value;
            }
        }
        public TurnerStyle TurnerStyle
        {
            get
            {
                return this._turnerStyle;
            }
            set
            {
                this._turnerStyle = value;
            }
        }
        public string Code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        public string Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
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

        public string PCode
        {
            get
            {
                return this._pCode;
            }
            set
            {
                this._pCode = value;
            }
        }

        public string TopoLayerId
        {
            get
            {
                return this._topoLayerId;
            }
            set
            {
                this._topoLayerId = value;
            }
        }

    }
}
