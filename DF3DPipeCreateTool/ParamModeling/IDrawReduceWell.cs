using Gvitech.CityMaker.FdeGeometry;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawReduceWell : IDrawWell, IDrawScanModel, IDrawGeometry
    {
        // Methods
        void SetParameter(IPoint position, double hTop, double hBottom, double maxDia);

        // Properties
        double MaxDia { get; }
    }
}
