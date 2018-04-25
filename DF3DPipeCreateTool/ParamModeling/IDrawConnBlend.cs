using Gvitech.CityMaker.Resource;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawConnBlend : IDrawJoint, IDrawScanModel, IDrawGeometry
    {
        void SetBaseModel(IModel bfModel, IModel bsModel);
    }
}
