using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DF3DPipeCreateTool.Class
{
    public enum TurnerStyle
    {
        [Description("未知")]
        Unknown = 0,
        [Description("圆角")]
        Capround = 1,
        [Description("方角")]
        Capsquare = 2
    }
}
