using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DAnalysis.Class
{
    public class PPLine2D
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
        public PPPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
