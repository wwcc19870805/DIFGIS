using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DScan.Class
{
    public delegate double[] TerrainAnalyseProcess(gviTerrainAnalyseOperation op, double[] ptArray);
    [ComVisible(true)]
    public class TerrainAnalyseCallBack
    {
        public TerrainAnalyseProcess onProcessing;
        public double[] OnProcessing(gviTerrainAnalyseOperation op, double[] ptArray)
        {
            if (onProcessing != null)
                return onProcessing(op, ptArray);
            else
                return null;
        }
    }
}
