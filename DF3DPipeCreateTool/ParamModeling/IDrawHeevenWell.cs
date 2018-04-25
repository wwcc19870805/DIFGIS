using Gvitech.CityMaker.FdeGeometry;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawHeevenWell : IDrawWell, IDrawScanModel, IDrawGeometry
    {
        // Methods
        void SetParameter(IGeometry geometry, double hTop, double hBottom);
        // 适应辅助数据-井面\井底高程 polyline转化polygon FX 2014.04.08
        void SetParameter1(IGeometry geometry, double hTop, double hBottom);
    }
}
