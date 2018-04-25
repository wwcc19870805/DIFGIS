using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DMDBConvert.Class
{
    public class FZLine
    {
        string startPoint;
        string endPoint;
        public string StartPoint
        {
            get { return this.startPoint; }
        }
        public string EndPoint
        {
            get { return this.endPoint; }
        }
        public FZLine(string sPoint, string ePoint)
        {
            this.startPoint = sPoint;
            this.endPoint = ePoint;
        }
    }
}
