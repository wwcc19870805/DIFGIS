using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DPipeCreateTool.Class;

namespace DF3DPipeCreateTool.ParamModeling
{
    public interface ISection
    {
        // Methods
        double[] GetVtxs();
        double[] GetVtxs(double rate, double offX, double offY);

        // Properties
        double OffsetX { get; set; }
        double OffsetY { get; set; }
        SecShape SecShape { get; }
        int SegCount { get; }
    }
}
