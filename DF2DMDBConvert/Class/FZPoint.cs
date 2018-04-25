using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace DF2DMDBConvert.Class
{
    public class FZPoint
    {
        private string name;
        private IPoint point;
        public string Name
        {
            get { return this.name; }
        }
        public IPoint Point
        {
            get { return this.point; }
        }
        public double X
        {
            get { return this.point.Y; }
        }
        public double Y
        {
            get { return this.point.X; }
        }
        public double Z
        {
            get { return this.point.Z; }
        }
        public FZPoint(string name, IPoint point)
        {
            this.name = name;
            this.point = point;
        }
    }
}
