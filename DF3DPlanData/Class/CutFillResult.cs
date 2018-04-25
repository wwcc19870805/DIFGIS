using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPlanData.Class
{
    public class CutFillResult
    {
        // Methods
        public CutFillResult()
        {
            this.CutPolygons = new List<IMultiPolygon>();
            this.FillPolygons = new List<IMultiPolygon>();
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", new object[] { this.Flag.ToString(), this.Tolerance.ToString(), this.CutReuslt.ToString(), this.FillResult.ToString() });
        }

        // Properties
        public List<IMultiPolygon> CutPolygons { get; set; }

        public double CutReuslt { get; set; }

        public List<IMultiPolygon> FillPolygons { get; set; }

        public double FillResult { get; set; }

        public bool Flag { get; set; }

        public double Tolerance { get; set; }
    }
}
