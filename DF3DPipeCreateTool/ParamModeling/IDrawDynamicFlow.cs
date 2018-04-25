using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawDynamicFlow : IDrawScanModel, IDrawGeometry
    {
        // Methods
        void SetParameter(IPipeSection pipeSection, IPolyline path, int flowDir);

        // Properties
        int FlowDrection { get; set; }
        IPipeSection PipeSection { get; set; }
        List<Vector> Route { get; set; }
        TurnerStyle TurnerStyle { get; set; }
    }

 

}
