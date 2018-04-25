//===============================================================================================
//编辑图层选项
//===============================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DData.Class;

namespace DF3DEdit.Class
{
    public class CboItem
    {
        private string _text;
        private DF3DFeatureClass _value;

        public CboItem(string text,DF3DFeatureClass value)
        {
            this._text = text;
            this._value = value;
        }

        public override string ToString()
        {
            return this.Text.ToString();
        }

        /// <summary>
        /// 显示值
        /// </summary>
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        /// <summary>
        /// 对象值
        /// </summary>
        public DF3DFeatureClass Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }
}
