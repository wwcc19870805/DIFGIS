using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFCommon.Class;

namespace DF2D3DControl.Class
{
    public class Link2DAnd3D
    {
        private static double absoluteElevation = double.Parse(Config.GetConfigValue("TerrainElevation"));
        public static void _2DLink3D(double minX, double maxX, double minY, double maxY, double pitch, double fov, out double ox, out double oy, out double oz)
        {
            ox = (minX + maxX) / 2;
            double height = maxY - minY;
            double width = maxX - minX;
            double tp = Math.Tan(pitch);
            double tpf = Math.Tan(pitch + fov / 2);
            double temp = height * tp / (tpf - tp) / 2;
            oy = minY - temp;
            oz = temp * tpf;
            oz = oz + Link2DAnd3D.absoluteElevation;
        }

        public static void _3DLink2D(double ox, double oy, double oz, double pitch, double fov, double rate, out double left, out double top, out double right, out double bottom)
        {
            oz = oz - Link2DAnd3D.absoluteElevation;
            double tp = Math.Tan(pitch);
            double tpf = Math.Tan(pitch + fov / 2);
            double temp1 = oz / tpf;
            double temp2 = oz / tp - temp1;
             
            left = ox - rate * temp2;
            top = oy + temp1;
            right = ox + rate * temp2;
            bottom = oy + temp1 + 2 * temp2; 
        }
    }
}
