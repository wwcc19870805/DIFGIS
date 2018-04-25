using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawPipeLine : IDrawScanModel, IDrawGeometry
    {
        // Methods
        void SetParameter(IPipeSection pipeSection, IPolyline path);
        void SetParameter(IPipeSection pipeSection, List<Vector> vtxs);
        // 添加PipeLocateType类型参数以辨认管线为地下\架空 FX 2014.09.23
        void SetParameter(IPipeSection pipeSection, List<Vector> vtxs, TurnerStyle turnerStyle, LocationType locType);

        // Properties
        IPipeSection PipeSection { get; set; }
        List<Vector> Route { get; set; }
        TurnerStyle TurnerStyle { get; set; }
        LocationType PipeLocateType { get; set; }
    }
}
