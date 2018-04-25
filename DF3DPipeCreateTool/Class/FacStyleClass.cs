using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Gvitech.CityMaker.Common;

namespace DF3DPipeCreateTool.Class
{
    public class FacStyleClass
    {
        protected int _id;               // ID
        protected string _facClassCode;
        protected string _objectId;
        protected string _name;
        protected StyleType _type;
        protected Image _thumbnail;
        protected IPropertySet _images;
        protected bool _isInit;            // 是否初始化

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

        public StyleType Type
        {
            get
            {
                return this._type;
            }
        }

        public Image Thumbnail
        {
            get
            {
                return this._thumbnail;
            }
            set
            {
                this._thumbnail = value;
            }
        }

        public string ObjectId
        {
            get
            {
                return this._objectId;
            }
            set
            {
                this._objectId = value;
            }
        }

        public IPropertySet Images
        {
            get { return this._images; }
        }


        public override string ToString()
        {
            return this._name;
        }

        public virtual IBinaryBuffer ObjectToJson()
        {
            return null;
        }

        public virtual bool InitResource()
        {
            return false;
        }
    }
}
