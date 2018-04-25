using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DF3DPipeCreateTool.Class
{
    public class ColorClass
    {
        private string _code;          // 颜色编码
        private string _comment;       // 属性
        private string _groupId;       // 逻辑组ID
        private int _id;               // ID
        private string _name;          // 名称
        private Image _thumbnail;      // 缩略图
        private string _type;          // 类型
        private string _objectId;    // objectId编码


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

        public Color Color
        {
            get
            {
                if (string.IsNullOrEmpty(this._code))
                {
                    return Color.Empty;
                }
                return Color.FromArgb(int.Parse(this._code));
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

        public string Group
        {
            get
            {
                return this._groupId;
            }
            set
            {
                this._groupId = value;
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

        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public override string ToString()
        {
            return this._name;
        }

    }
}
