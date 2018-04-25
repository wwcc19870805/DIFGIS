using System;
using System.Collections.Generic;
using DF3DPipeCreateTool.Class;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawCylinderLine : IDrawGeometry
    {
        // Properties
        uint Color { get; set; }
        IPipeSection PipeSection { get; set; }
        List<List<Vector>> Route { get; set; }
        string TcName { get; set; }
    }

 

}
