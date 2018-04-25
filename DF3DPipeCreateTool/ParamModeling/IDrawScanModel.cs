using System;
using DF3DPipeCreateTool.Class;

namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawScanModel : IDrawGeometry
    {
        // Methods
        void SetColorRender(uint[] colors);
        void SetTextureRender(string[] tcNames);

        // Properties
        uint[] Colors { get; }
        RenderType RenderType { get; }
        string[] TextureNames { get; }
    }
}
