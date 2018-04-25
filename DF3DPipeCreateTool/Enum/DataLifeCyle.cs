using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DF3DPipeCreateTool.Class
{
    public enum DataLifeCyle
    {
        [Description("现状")]
        Actuality = 0,
        [Description("竣工")]
        Completed = 1,
        [Description("设计")]
        Design = 3,
        [Description("规划")]
        Plan = 2,
        UnKnown = 4
    }
}
