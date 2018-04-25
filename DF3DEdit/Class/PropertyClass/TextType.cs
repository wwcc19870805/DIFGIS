using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DEdit.Class
{
    public enum TextType
    {
        FileName,
        PassWord,
        IP,
        Number = 4,
        Port = 8,
        RS = 16,
        Node = 32,
        Url = 64,
        Path = 128,
        FdeField
    }
}
