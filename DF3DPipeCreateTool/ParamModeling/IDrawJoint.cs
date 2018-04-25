using DF3DPipeCreateTool.Class;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IDrawJoint : IDrawScanModel, IDrawGeometry
    {
        // Methods
        void SetParameter(Vector center, Vector[][] vtx, IPipeSection[] sections);

        // Properties
        IPipeSection[] Sections { get; }
        Vector[][] Vtx { get; }
    }
}
