using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DPipeCreateTool.Class;

namespace DF3DPipeCreateTool.ParamModeling
{
    public interface IPipeSection : ISection
    {
        // Methods
        double[] GetSimpleVtxs();

        // Properties
        double Diameter { get; }
        double Height { get; }
        HorizontalPos HorPos { get; }
        double Thick { get; }
        VerticalPos VtPos { get; }
        double Width { get; }
    }
}
