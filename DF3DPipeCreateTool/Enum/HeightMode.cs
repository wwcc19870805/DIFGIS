using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DF3DPipeCreateTool.Class
{
    public enum HeightMode
    {
        [Description("管顶高程")]
        Top,
        [Description("中心高程")]
        Center,
        [Description("管底高程")]
        Bottom
    }
}