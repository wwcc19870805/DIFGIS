using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPipe.Analysis.Class
{
    public class PPLine
    {
        public string facType = "";
        public bool isrect = false;
        public double clh = double.NaN;
        public double cgh = double.NaN;
        public List<double> gj = new List<double>();
        public string dia;
        public double space = 0.0;
        public PPPoint interPoint = null;
        public PPPoint startPt = null;
        public int hlb = 0;
    }
    public class PPPoint
    {
        public double X;
        public double Y;
        public double Z;
        public PPPoint(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
