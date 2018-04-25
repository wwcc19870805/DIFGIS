using Gvitech.CityMaker.Resource;
using System;
using DF3DPipeCreateTool.Class;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawStaticModel : IDrawGeometry
    {
        // Methods
        // 以区分井\阀门处理方法 FX 2014.04.04
        void SetParameter(Vector center, bool isValve, bool isWell, double surfH, double topH, double bottomH, bool isScale, bool isRotate, bool isFollowSurfH, bool isStrechZ, bool isBillboardZ);
        void SetParameter(Vector center, double surfH, double bottomH, double angleZ, bool isScale, bool isRotate, bool isFollowSurfH, bool isStrechZ, bool isBillboardZ);

        // Properties
        Vector[] ConnectPoint { get; set; }
        Vector[] Directions { get; set; }
        IModel FineModel { get; set; }
        bool IsBillboardZ { get; set; }
        bool IsFollowSurfH { get; set; }
        bool IsRotate { get; set; }
        bool IsScale { get; set; }
        bool IsZStrech { get; set; }
        IPipeSection[] Sections { get; set; }
        IModel SimpleModel { get; set; }
        // 添加架空阀门转向属性 FX 2014.04.04
        int FLAG { get; set; }
        double Pitch { get; set; }
        double Yaw { get; set; }
    }
}
