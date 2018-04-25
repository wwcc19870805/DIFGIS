using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
using DF3DPipeCreateTool.Class;

namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawGeometry
    {
        // Methods
        bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel);
        // 针对梅钢数据的架空管点绘制方法 FX 2014.04.04
        bool DrawOver(out IModelPoint mp, out IModel fmodel, out IModel smodel);

        IModelPoint GetModelPoint();

        // Properties
        gviCullFaceMode CullModel { get; set; }
        string ModelName { get; set; }
        ModelType ModelType { get; }
        double RotateX { get; }
        double RotateY { get; }
        double RotateZ { get; }
        double ScaleX { get; }
        double ScaleY { get; }
        double ScaleZ { get; }
        uint SpecularColor { get; set; }
        double X { get; }
        double Y { get; }
        double Z { get; }
    }
}
