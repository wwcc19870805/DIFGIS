using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DF2DAnalysis.Class
{
    class PPLineCompare2D : Comparer<PPLine2D>
    {
        public override int Compare(PPLine2D a, PPLine2D b)
        {
            if (a == null || b == null) return 0;
            double disa = (a.interPoint.X - a.startPt.X) * (a.interPoint.X - a.startPt.X) + (a.interPoint.Y - a.startPt.Y) * (a.interPoint.Y - a.startPt.Y);
            double disb = (b.interPoint.X - b.startPt.X) * (b.interPoint.X - b.startPt.X) + (b.interPoint.Y - b.startPt.Y) * (b.interPoint.Y - b.startPt.Y);
            if (Math.Abs(disa - disb) < 0.0000001) return 0;
            if (disa > disb) return 1;
            else if (disa < disb) return -1;
            return 0;
        }
    }
}
