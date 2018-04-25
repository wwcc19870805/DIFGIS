using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPipeCreateTool.Class
{
    public class Vector
    {
        // Fields
        public double X;
        public double Y;
        public double Z;

        // Methods
        public Vector()
        {
        }

        public Vector(Vector v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
        }

        public Vector(IPolyline line)
        {
            this.X = line.EndPoint.X - line.StartPoint.X;
            this.Y = line.EndPoint.Y - line.StartPoint.Y;
            this.Z = line.EndPoint.Z - line.StartPoint.Z;
        }


        public Vector(IPoint p)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.Z = p.Z;
        }

        public Vector(ISegment seg)
        {
            this.X = seg.EndPoint.X - seg.StartPoint.X;
            this.Y = seg.EndPoint.Y - seg.StartPoint.Y;
            this.Z = seg.EndPoint.Z - seg.StartPoint.Z;
        }

        public Vector(string vInfo)
        {
            if (!string.IsNullOrEmpty(vInfo))
            {
                string[] strArray = vInfo.Split(new char[] { ',' });
                if (strArray.Length == 3)
                {
                    this.X = double.TryParse(strArray[0], out this.X) ? this.X : 0.0;
                    this.Y = double.TryParse(strArray[1], out this.Y) ? this.Y : 0.0;
                    this.Z = double.TryParse(strArray[2], out this.Z) ? this.Z : 0.0;
                }
            }
        }

        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static double CalcAngle(Vector v1, Vector v2)
        {
            double d = (((v1.X * v2.X) + (v1.Y * v2.Y)) + (v1.Z * v2.Z)) / (v1.Mould() * v2.Mould());
            if (d > 1.0)
            {
                d = 1.0;
            }
            else if (d < -1.0)
            {
                d = -1.0;
            }
            return Math.Acos(d);

        }

        public double CalcDirection()
        {
            return Math.Atan2(this.Y, this.X);
        }

        public static Vector CenterPosition(Vector v1, Vector v2)
        {
            return new Vector((v1.X + v2.X) / 2.0, (v1.Y + v2.Y) / 2.0, (v1.Z + v2.Z) / 2.0);
        }

        public static int MaxLength(Vector[] arr)
        {
            if ((arr == null) || (arr.Length == 0))
            {
                return -1;
            }
            int num = 0;
            double length = arr[0].Length;
            for (int i = 1; i < arr.Length; i++)
            {
                if (length < arr[i].Length)
                {
                    num = i;
                    length = arr[i].Length;
                }
            }
            return num;
        }

        public static int MaxZ(Vector[] arr)
        {
            if ((arr == null) || (arr.Length == 0))
            {
                return -1;
            }
            int num = 0;
            double z = arr[0].Z;
            for (int i = 1; i < arr.Length; i++)
            {
                if (z < arr[i].Z)
                {
                    num = i;
                    z = arr[i].Z;
                }
            }
            return num;
        }

        public static int MinLength(Vector[] arr)
        {
            if ((arr == null) || (arr.Length == 0))
            {
                return -1;
            }
            int num = 0;
            double length = arr[0].Length;
            for (int i = 1; i < arr.Length; i++)
            {
                if (length > arr[i].Length)
                {
                    num = i;
                    length = arr[i].Length;
                }
            }
            return num;
        }

        public static int MinZ(Vector[] arr)
        {
            if ((arr == null) || (arr.Length == 0))
            {
                return -1;
            }
            int num = 0;
            double z = arr[0].Z;
            for (int i = 1; i < arr.Length; i++)
            {
                if (z > arr[i].Z)
                {
                    num = i;
                    z = arr[i].Z;
                }
            }
            return num;
        }

        public double Mould()
        {
            return Math.Sqrt(((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator /(Vector v1, double d)
        {
            return new Vector(v1.X / d, v1.Y / d, v1.Z / d);
        }

        public static double operator ^(Vector v1, Vector v2)
        {
            return (((v1.X * v2.X) + (v1.Y * v2.Y)) + (v1.Z * v2.Z));
        }

        public static Vector operator *(Vector v1, Vector v2)
        {
            return new Vector((v1.Y * v2.Z) - (v2.Y * v1.Z), (v1.Z * v2.X) - (v1.X * v2.Z), (v1.X * v2.Y) - (v2.X * v1.Y));
        }

        public static Vector operator *(Vector v1, double f)
        {
            return new Vector(v1.X * f, v1.Y * f, v1.Z * f);
        }

        public static Vector operator *(double d, Vector v1)
        {
            return new Vector(v1.X * d, v1.Y * d, v1.Z * d);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        public static Vector operator +(Vector v)
        {
            return new Vector(v);
        }

        public void Set(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.X, this.Y, this.Z);
        }

        public double UnitRate()
        {
            return (1.0 / Math.Sqrt(((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)));
        }

        public Vector UnitVector()
        {
            double num = 1.0 / Math.Sqrt(((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
            return (Vector)(this * num);
        }

        // Properties
        public double Length
        {
            get
            {
                return Math.Sqrt(((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
            }
        }
    }
}
