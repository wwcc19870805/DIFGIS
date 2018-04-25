using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DPipe.Analysis.Class
{
    public class _elipse
    {
        public static double timeSpan;
        public static double rate;
        public static double windSpeed;
        public static int segCount;
        public static double dAngle;
        public static double cx;
        public static double cy;
        public static double windDirection;
        public static double x;
        public static double y;

        public static double[] drawElipse()
        {
            double[] numArray = new double[_elipse.segCount * 2 + 2];
            double height = _elipse.timeSpan * _elipse.rate;
            double width = _elipse.timeSpan * _elipse.windSpeed * _elipse.rate * 0.6;
            _elipse.calcCenter();
            for (int i = 0; i <= _elipse.segCount; i++) {
                double d = i * _elipse.dAngle;
                if (i == _elipse.segCount) {
                    d = 0.0;
                }
                double x = _elipse.cx + Math.Cos(d) * width;
                double y = _elipse.cy + Math.Sin(d) * height;
                double resx,resy;
                _elipse.rotatePoint(x, y,out resx,out resy);
                numArray[i * 2] = resx;
                numArray[i * 2 + 1] = resy;
            }
            return numArray;
        }

        public static void calcCenter()
        {
            if (Math.Abs(_elipse.timeSpan) < 0.0000001)
            {
                _elipse.cx = _elipse.x + Math.Cos(_elipse.windDirection) * _elipse.windSpeed * 0.4;
                _elipse.cy = _elipse.y + Math.Sin(_elipse.windDirection) * _elipse.windSpeed * 0.4;
            }
            else
            {
                _elipse.cx = _elipse.x + _elipse.timeSpan * Math.Cos(_elipse.windDirection) * _elipse.windSpeed * 0.4;
                _elipse.cy = _elipse.y + _elipse.timeSpan * Math.Sin(_elipse.windDirection) * _elipse.windSpeed * 0.4;
            }
        }

        public static void rotatePoint(double x, double y, out double resx, out double resy)
        {
            double d = _elipse.offAngle(_elipse.cx, _elipse.cy, x, y);
            double num2 = Math.Sqrt(((x - _elipse.cx) * (x - _elipse.cx)) + ((y - _elipse.cy) * (y - _elipse.cy)));
            resx= _elipse.cx + (num2 * Math.Cos(d));
            resy = _elipse.cy + (num2 * Math.Sin(d));
        }

        public static double offAngle(double x1, double y1, double x2, double y2)
        {
            double num3;
            double num = x2 - x1;
            double num2 = y2 - y1;
            if (num >= 0.0 && num2 > 0.0)
            {
                num3 = Math.Atan(num2 / num);
            }
            else if ((num < 0.0) && (num2 >= 0.0))
            {
                num3 = Math.PI - Math.Atan(num2 / -num);
            }
            else if ((num < 0.0) && (num2 < 0.0))
            {
                num3 = Math.PI + Math.Atan(num2 / num);
            }
            else
            {
                num3 = Math.PI * 2 - Math.Atan(-num2 / num);
            }
            return (num3 + _elipse.windDirection);
        }
    }
}
