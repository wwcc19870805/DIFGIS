using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DF3DPipeCreateTool.Class
{
    public enum LocationType
    {
        [Description("未知类型")]
        UnKnown = 0,
        [Description("地下管线")]
        UnderGround = 1,
        [Description("架空管线")]
        OverHead = 2
    }
}
