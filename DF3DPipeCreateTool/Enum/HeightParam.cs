using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DF3DPipeCreateTool.Class
{
    public enum HeightParam
    {
        [Description("管线起止点高程")]
        PipeHeight = 0,
        [Description("地面高程+埋深")]
        LineSurfH2Deep = 1,
        [Description("管线顶点高程")]
        PipeVertexHeight = 2,
        //[EnumType("管线管顶高程")]
        [Description("其他")]
        Unkown = 3
    }
}