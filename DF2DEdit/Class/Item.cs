/*----------------------------------------------------------------
            // Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：Item.cs
            // 文件功能描述：控件项
            //               
            // 
            // 创建标识：LuoXuan
            //
            // 修改描述：

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DEdit.Class
{
    public class Item : object
    {
        public Item()
        {
        }

        public Item(string text, object val)
        {
            m_text = text;
            m_value = val;
        }

        private string m_text = "";
        private object m_value = null;

        public string Text
        {
            get { return m_text; }
            set { m_text = value; }
        }

        public object Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        public override string ToString()
        {
            return m_text;
        }

        public override bool Equals(object obj)
        {
            Item tmpItem = obj as Item;
            if ((tmpItem != null) && m_text.Equals(tmpItem.Text) && m_value.Equals(tmpItem.Value))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
