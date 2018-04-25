using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DEdit.Class
{
    public struct RegexDataStruct
    {
        private string key;
        private CharactorType type;
        public string Key
        {
            get
            {
                return this.key;
            }
        }
        public CharactorType CharType
        {
            get
            {
                return this.type;
            }
        }
        public RegexDataStruct(string key, CharactorType type)
        {
            this.key = key;
            this.type = type;
        }
    }
}
