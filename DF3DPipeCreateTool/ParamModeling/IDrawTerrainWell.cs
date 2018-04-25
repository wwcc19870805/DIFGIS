using Gvitech.CityMaker.FdeGeometry;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawTerrainWell : IDrawWell, IDrawScanModel, IDrawGeometry
    {
        // Methods
        void SetParameter(IPolygon region, double hTop, double hBottom, double angle);
    }

}
