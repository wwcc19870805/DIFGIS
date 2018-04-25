using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DPipeCreateTool.Class
{
    public class Maths
    {
        // Fields
        private static double BackRate = 0.5;

        // Methods
        public static double AngleTan(double x, double y)
        {
            if ((x > 0.0) && (y >= 0.0))
            {
                return Math.Atan(y / x);
            }
            if ((x < 0.0) && (y > 0.0))
            {
                return (3.1415926535897931 - Math.Atan(y / -x));
            }
            if ((x < 0.0) && (y <= 0.0))
            {
                return (3.1415926535897931 + Math.Atan(y / x));
            }
            return (6.2831853071795862 - Math.Atan(-y / x));
        }

        #region 线段\弧离散化
        public static List<Vector> DisperseArc(List<Vector> vtxs, double dia1)
        {
            List<Vector> list = new List<Vector>();
            try
            {
                if (vtxs.Count != 3)
                {
                    return null;
                }
                double d = 0.0;
                double num2 = (1.0 * dia1) / 2.0;
                Vector vector3 = (vtxs[0] - vtxs[1]).UnitVector();
                Vector vector4 = (vtxs[2] - vtxs[1]).UnitVector();
                double num4 = 0.0;
                num4 = Vector.CalcAngle(vector3, vector4);
                Vector vector5 = vtxs[1] + ((Vector)(((vector3 + vector4)).UnitVector() * (num2 / Math.Cos(num4 / 2.0))));
                Vector vector = vtxs[1] + ((Vector)(vector3 * num2));
                Vector vector2 = vtxs[1] + ((Vector)(vector4 * num2));
                double num = Math.Tan(num4 / 2.0) * num2;      // 圆半径
                num4 = 3.1415926535897931 - num4;
                int num5 = (int)((num4 * 18.0) / 3.1415926535897931);
                Vector vector6 = vector - vector5;
                Vector vector7 = vector2 - vector5;
                Vector vector9 = vector7 * vector6;
                Vector vector10 = vector6.UnitVector();
                Vector vector11 = (vector6 * vector9).UnitVector();
                list.Add(vtxs[0]);
                for (int i = 1; i <= num5; i++)
                {
                    d = (i * num4) / ((double)(num5 + 1));
                    Vector item = vector5 + ((Vector)(num * ((Math.Cos(d) * vector10) + (Math.Sin(d) * vector11))));
                    list.Add(item);
                }
                list.Add(vtxs[2]);
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public static List<Vector> DisperseLine(List<Vector> vtxs, double dia1)
        {
            try
            {
                double num = (1.0 * dia1) / 2.0;
                List<Vector> list = new List<Vector>();
                if ((vtxs == null) || (vtxs.Count <= 2))
                {
                    return vtxs;
                }
                for (int i = 1; i < (vtxs.Count - 1); i++)
                {
                    if (i == 1)
                    {
                        list.Add(vtxs[i - 1]);
                    }
                    Vector vector = vtxs[i - 1];
                    Vector item = vtxs[i];
                    Vector vector3 = vtxs[i + 1];
                    Vector vector6 = (vector - item).UnitVector();
                    Vector vector4 = item + ((Vector)(vector6 * num));
                    Vector vector7 = (vector3 - item).UnitVector();
                    Vector vector5 = item + ((Vector)(vector7 * num));
                    Vector vector1 = vector5 - vector4;
                    if (Vector.CalcAngle(vector6, vector7) > 2.967)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        List<Vector> list2 = new List<Vector> {
                        vector4,
                        item,
                        vector5
                    };
                        list.AddRange(DisperseArc(list2, dia1));
                    }
                    if (i == (vtxs.Count - 2))
                    {
                        list.Add(vtxs[i + 1]);
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public static List<Vector> DisperseLine(List<Vector> vtxs, double dia1, ref List<int> lstBpIndex)
        {
            try
            {
                double num = (1.0 * dia1) / 2.0;
                List<Vector> list = new List<Vector>();
                if ((vtxs == null) || (vtxs.Count <= 2))
                {
                    return vtxs;
                }
                for (int i = 1; i < (vtxs.Count - 1); i++)
                {
                    if (i == 1)
                    {
                        list.Add(vtxs[i - 1]);
                    }
                    Vector vector = vtxs[i - 1];
                    Vector item = vtxs[i];
                    Vector vector3 = vtxs[i + 1];
                    Vector vector6 = (vector - item).UnitVector();
                    Vector vector4 = item + ((Vector)(vector6 * num));
                    Vector vector7 = (vector3 - item).UnitVector();
                    Vector vector5 = item + ((Vector)(vector7 * num));
                    Vector vector1 = vector5 - vector4;
                    if (Vector.CalcAngle(vector6, vector7) > 2.967)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        List<Vector> list2 = new List<Vector> {
                    vector4,
                    item,
                    vector5
                };
                        list.AddRange(DisperseArc(list2, dia1));
                        lstBpIndex.Add(list.Count);
                        list.Add(list[list.Count - 1]);
                    }
                    if (i == (vtxs.Count - 2))
                    {
                        list.Add(vtxs[i + 1]);
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        // FX 2014.04.01
        public static List<Vector> DisperseLineFX(List<Vector> vtxs, double dia1, ref List<int> lstBpIndex)
        {
            try
            {
                double num = (1.0 * dia1) / 2.0;
                List<Vector> list = new List<Vector>();
                if ((vtxs == null) || (vtxs.Count <= 2))
                {
                    return vtxs;
                }
                for (int i = 1; i < (vtxs.Count - 1); i++)
                {
                    if (i == 1)
                    {
                        list.Add(vtxs[i - 1]);
                    }
                    Vector vector = vtxs[i - 1];
                    Vector item = vtxs[i];
                    Vector vector3 = vtxs[i + 1];
                    ////////////////////////////////////////////////
                    double d1 = (vector - item).Length;
                    double d2 = (item - vector3).Length;
                    double d = 0;
                    if (d1 < dia1 || d2 < dia1)
                    {
                        d = d1 < d2 ? d1 : d2;
                        num = (0.8 * d) / 2.0;
                        d = 0.8 * d;
                    }
                    else
                    {
                        d = dia1;
                    }
                    ///////////////////////////////////////////////
                    Vector vector6 = (vector - item).UnitVector();
                    Vector vector4 = item + ((Vector)(vector6 * num));
                    Vector vector7 = (vector3 - item).UnitVector();
                    Vector vector5 = item + ((Vector)(vector7 * num));
                    Vector vector1 = vector5 - vector4;
                    if (Vector.CalcAngle(vector6, vector7) > 2.967)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        List<Vector> list2 = new List<Vector> {
                            vector4,
                            item,
                            vector5
                        };
                        //list.AddRange(DisperseArc(list2, dia1));
                        list.AddRange(DisperseArc(list2, d));

                        lstBpIndex.Add(list.Count);
                        list.Add(list[list.Count - 1]);
                    }
                    if (i == (vtxs.Count - 2))
                    {
                        list.Add(vtxs[i + 1]);
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public static List<Vector> DisperseLineFX1(List<Vector> vtxs, double dia1, ref List<int> lstBpIndex)
        {
            try
            {
                double num = (1.0 * dia1) / 2.0;
                List<Vector> list = new List<Vector>();
                if ((vtxs == null) || (vtxs.Count <= 2))
                {
                    return vtxs;
                }
                for (int i = 1; i < (vtxs.Count - 1); i++)
                {
                    if (i == 1)
                    {
                        list.Add(vtxs[i - 1]);
                    }
                    Vector vector = vtxs[i - 1];
                    Vector item = vtxs[i];
                    Vector vector3 = vtxs[i + 1];
                    ////////////////////////////////////////////////
                    double d = 0;
                    double d1 = (vector - item).Length;
                    double d2 = (item - vector3).Length;
                    if (d1 < dia1 || d2 < dia1)
                    {
                        d = d1 < d2 ? d1 : d2;
                        num = (0.8 * d) / 2.0;
                        d = 0.8 * d;
                    }
                    else
                    {
                        d = dia1;
                        num = (1.0 * d) / 2.0;
                    }
                    ///////////////////////////////////////////////
                    Vector vector6 = (vector - item).UnitVector();
                    Vector vector4 = item + ((Vector)(vector6 * num));
                    Vector vector7 = (vector3 - item).UnitVector();
                    Vector vector5 = item + ((Vector)(vector7 * num));
                    Vector vector1 = vector5 - vector4;
                    //if (Vector.CalcAngle(vector6, vector7) > 2.967)
                    if (Vector.CalcAngle(vector6, vector7) > 3.054)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        List<Vector> list2 = new List<Vector> {
                            vector4,
                            item,
                            vector5
                        };
                        //list.AddRange(DisperseArc(list2, dia1));
                        lstBpIndex.Add(list.Count);
                        list.AddRange(DisperseArc(list2, d));
                        lstBpIndex.Add(list.Count - 1);
                        //list.Add(list[list.Count - 1]);
                    }
                    if (i == (vtxs.Count - 2))
                    {
                        list.Add(vtxs[i + 1]);
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public static List<Vector> DisperseLineFX2(List<Vector> vtxs, double dia1, ref List<Vector> lstNV)
        {
            try
            {
                List<int> Flag = new List<int>();
                List<Vector> NV = new List<Vector>();
                List<Vector> Center = new List<Vector>();
                int Index = 0;
                double num = (1.0 * dia1) / 2.0;
                List<Vector> list = new List<Vector>();
                if ((vtxs == null) || (vtxs.Count <= 2))
                {
                    return vtxs;
                }
                for (int i = 1; i < (vtxs.Count - 1); i++)
                {
                    if (i == 1)
                    {
                        list.Add(vtxs[i - 1]);
                        Flag.Add(0);
                    }
                    Vector vector = vtxs[i - 1];
                    Vector item = vtxs[i];
                    Vector vector3 = vtxs[i + 1];
                    ////////////////////////////////////////////////
                    double d1 = (vector - item).Length;
                    double d2 = (item - vector3).Length;
                    double d = 0;
                    if (d1 < dia1 || d2 < dia1)
                    {
                        d = d1 < d2 ? d1 : d2;
                        num = (0.8 * d) / 2.0;
                        d = 0.8 * d;
                    }
                    else
                    {
                        d = dia1;
                    }
                    ///////////////////////////////////////////////
                    Vector vector6 = (vector - item).UnitVector();
                    Vector vector4 = item + ((Vector)(vector6 * num));
                    Vector vector7 = (vector3 - item).UnitVector();
                    Vector vector5 = item + ((Vector)(vector7 * num));
                    Vector vector1 = vector5 - vector4;
                    if (Vector.CalcAngle(vector6, vector7) > 2.967)
                    {
                        list.Add(item);
                        Flag.Add(0);
                    }
                    else
                    {
                        List<Vector> list2 = new List<Vector> {
                            vector4,
                            item,
                            vector5
                        };
                        Vector center = new Vector();
                        Index = Index + 1;
                        list.AddRange(DisperseArcFX2(list2, d, Index, ref center, ref Flag));
                        Vector normalvector = (item - vector) * (vector3 - item);
                        Center.Add(center);
                        NV.Add(normalvector);
                    }
                    if (i == (vtxs.Count - 2))
                    {
                        list.Add(vtxs[i + 1]);
                        Flag.Add(0);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    Vector v = new Vector();
                    lstNV.Add(v);
                }
                for (int i = 0; i < Flag.Count; i++)
                {
                    if (i == Flag.Count - 1)
                    {
                        lstNV[i] = (list[i] - list[i - 1]).UnitVector();
                    }
                    else
                    {
                        if (Flag[i] == 0)
                        {
                            lstNV[i] = (list[i + 1] - list[i]).UnitVector();
                        }
                        else
                        {
                            int index = Flag[i] - 1;
                            lstNV[i] = ((list[i] - Center[index]) * NV[index]).UnitVector();
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public static List<Vector> DisperseArcFX2(List<Vector> vtxs, double dia1, int Index, ref Vector center, ref List<int> Flag)
        {
            List<Vector> list = new List<Vector>();
            try
            {
                if (vtxs.Count != 3)
                {
                    return null;
                }
                double d = 0.0;
                double num2 = (1.0 * dia1) / 2.0;
                Vector vector3 = (vtxs[0] - vtxs[1]).UnitVector();
                Vector vector4 = (vtxs[2] - vtxs[1]).UnitVector();
                double num4 = 0.0;
                num4 = Vector.CalcAngle(vector3, vector4);
                Vector vector5 = vtxs[1] + ((Vector)(((vector3 + vector4)).UnitVector() * (num2 / Math.Cos(num4 / 2.0)))); // 圆心
                Vector vector = vtxs[1] + ((Vector)(vector3 * num2));
                Vector vector2 = vtxs[1] + ((Vector)(vector4 * num2));
                double num = Math.Tan(num4 / 2.0) * num2;                                                                  // 圆半径
                num4 = 3.1415926535897931 - num4;
                int num5 = (int)((num4 * 18.0) / 3.1415926535897931);
                Vector vector6 = vector - vector5;
                Vector vector7 = vector2 - vector5;
                Vector vector9 = vector7 * vector6;
                Vector vector10 = vector6.UnitVector();
                Vector vector11 = (vector6 * vector9).UnitVector();
                list.Add(vtxs[0]);
                Flag.Add(Index);
                for (int i = 1; i <= num5; i++)
                {
                    d = (i * num4) / ((double)(num5 + 1));
                    Vector item = vector5 + ((Vector)(num * ((Math.Cos(d) * vector10) + (Math.Sin(d) * vector11))));
                    list.Add(item);
                    Flag.Add(Index);
                }
                list.Add(vtxs[2]);
                Flag.Add(Index);
                center = vector5;
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        // FX 2014.04.01
        public static List<Vector> DisperseLineFX11(List<Vector> vtxs, double dia1)
        {
            try
            {
                double num = (1.0 * dia1) / 2.0;
                List<Vector> list = new List<Vector>();
                if ((vtxs == null) || (vtxs.Count <= 2))
                {
                    return vtxs;
                }
                //////////////////////////////////////////////////
                //List<List<Vector>> list1 = new List<List<Vector>>();
                //for (int i = 0; i < vtxs.Count; i++)
                //{
                //    List<Vector> l = new List<Vector>();
                //    l.Add(vtxs[i]);
                //    list1.Add(l);
                //}
                //////////////////////////////////////////////
                List<List<Vector>> list1 = GroupPoints(vtxs);
                for (int k = 0; k < list1.Count; k++)
                {
                    List<Vector> list2 = list1[k];
                    if (list2.Count > 2)
                    {
                        foreach (Vector v in list2)
                        {
                            list.Add(v);
                        }
                    }
                    else
                    {
                        if (k == 0 || k == list1.Count - 1)
                        {
                            foreach (Vector v in list2)
                            {
                                list.Add(v);
                            }
                        }
                        else
                        {
                            if (list2.Count == 1)
                            {
                                List<Vector> list3 = new List<Vector>();
                                list3.Add(list[list.Count - 1]);
                                foreach (Vector v in list2)
                                {
                                    list3.Add(v);
                                }
                                list3.Add(list1[k + 1][0]);
                                for (int i = 1; i < (list3.Count - 1); i++)
                                {
                                    Vector vector = list3[i - 1];
                                    Vector item = list3[i];
                                    Vector vector3 = list3[i + 1];
                                    ////////////////////////////////////////////////
                                    double d = 0;
                                    double d1 = (vector - item).Length;
                                    double d2 = (item - vector3).Length;
                                    if (d1 < dia1 || d2 < dia1)
                                    {
                                        d = d1 < d2 ? d1 : d2;
                                        num = (0.8 * d) / 2.0;
                                        d = 0.8 * d;
                                    }
                                    else
                                    {
                                        d = dia1;
                                        num = (1.0 * d) / 2.0;
                                    }
                                    ///////////////////////////////////////////////
                                    Vector vector6 = (vector - item).UnitVector();
                                    Vector vector4 = item + ((Vector)(vector6 * num));
                                    Vector vector7 = (vector3 - item).UnitVector();
                                    Vector vector5 = item + ((Vector)(vector7 * num));
                                    Vector vector1 = vector5 - vector4;
                                    //if (Vector.CalcAngle(vector6, vector7) > 2.967)
                                    if (Vector.CalcAngle(vector6, vector7) > 3.054)
                                    {
                                        list.Add(item);
                                    }
                                    else
                                    {
                                        List<Vector> list4 = new List<Vector> {
                                    vector4,
                                    item,
                                    vector5
                                     };
                                        list.AddRange(DisperseArc(list4, d));
                                    }
                                }
                            }
                            else
                            {
                                foreach (Vector v in list2)
                                {
                                    list.Add(v);
                                }
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public static List<List<Vector>> GroupPoints(List<Vector> vtxs)
        {
            List<Vector> list3 = new List<Vector>();
            List<List<Vector>> list4 = new List<List<Vector>>();
            bool IsEnd = false;
            for (int i = 0; i < vtxs.Count - 1; i++)
            {
                Vector p1 = vtxs[i];
                Vector p2 = vtxs[i + 1];
                double d = GetPntsDist(p1, p2);
                if (d < 0.22)
                {
                    if (list3.Count == 0)
                    {
                        list3.Add(p1);
                    }
                    list3.Add(p2);
                    if (i == vtxs.Count - 2)
                    {
                        list4.Add(list3);
                        list3 = new List<Vector>();
                        IsEnd = true;
                    }
                    continue;
                }
                else
                {
                    if (list3.Count > 0)
                    {
                        list4.Add(list3);
                        list3 = new List<Vector>();
                        continue;
                    }
                    else
                    {
                        list3.Add(p1);
                        list4.Add(list3);
                        list3 = new List<Vector>();
                        continue;
                    }
                }
            }
            if (!IsEnd)
            {
                list3.Add(vtxs[vtxs.Count - 1]);
                list4.Add(list3);
                list3 = new List<Vector>();
            }
            return list4;
        }
        public static double GetPntsDist(Vector p1, Vector p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y) + (p1.Z - p2.Z) * (p1.Z - p2.Z));
        }
        #endregion

        #region 生成局部坐标系
        public static void GenerateComplementBasis(Vector w, out Vector u, out Vector v)
        {
            u = new Vector();
            v = new Vector();
            if (u == null)
            {
                v = u * w;
            }
            else if ((Math.Abs(w.X) < 0.005) && (Math.Abs(w.Y) < 0.005))
            {
                if (w.Z > 0.0)
                {
                    u = new Vector(-1.0, 0.0, 0.0);
                    v = new Vector(0.0, 1.0, 0.0);
                }
                else
                {
                    //u = new Vector(1.0, 0.0, 0.0);
                    //v = new Vector(0.0, 1.0, 0.0);
                    u = new Vector(1.0, 0.0, 0.0);
                    v = new Vector(0.0, 1.0, 0.0);
                }
            }
            else
            {
                u.X = w.Y;
                u.Y = -w.X;
                u.Z = 0.0;
                v = u * w;
            }
            v = v.UnitVector();
            u = u.UnitVector();
        }
        public static void GenerateComplementBasis1(Vector w, out Vector v, out Vector u)
        {
            double num;
            u = new Vector();
            v = new Vector();
            if (Math.Abs(w.X) >= Math.Abs(w.Y))
            {
                num = 1.0 / Math.Sqrt((w.X * w.X) + (w.Z * w.Z));
                u.Set(-w.Z * num, 0.0, w.X * num);
                v.Set(w.Y * u.Z, (w.Z * u.X) - (w.X * u.Z), -w.Y * u.X);
            }
            else
            {
                num = 1.0 / Math.Sqrt((w.Y * w.Y) + (w.Z * w.Z));
                u.Set(0.0, w.Z * num, -w.Y * num);
                v.Set((w.Y * u.Z) - (w.Z * u.Y), -w.X * u.Z, w.X * u.Y);
            }
            v = v.UnitVector();
            u = u.UnitVector();
        }
        #endregion

        #region 计算法向量
        public static Vector NormalVector(Vector v1, Vector v2)
        {
            return (v1 * v2);
        }
        public static Vector NormalVector(Vector v1, Vector v2, Vector v3)
        {
            Vector vector = v2 - v1;
            Vector vector2 = v3 - v2;
            return NormalVector(vector, vector2);
        }
        #endregion

        public static bool SubsCoordinate(List<Vector> vtxs, out Vector bPoint, out List<Vector> newvtxs)
        {
            bPoint = null;
            newvtxs = null;
            if ((vtxs == null) || (vtxs.Count == 0))
            {
                return false;
            }
            try
            {
                newvtxs = new List<Vector>();
                bPoint = new Vector(vtxs[0]);
                for (int i = 0; i < vtxs.Count; i++)
                {
                    newvtxs.Add(new Vector(vtxs[i].X - bPoint.X, vtxs[i].Y - bPoint.Y, vtxs[i].Z - bPoint.Z));
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        public static Vector SubsVector(Vector v1, Vector v2)
        {
            return new Vector(v2.X - v1.X, v2.Y - v1.Y, v2.Z - v1.Z);
        }
    }
}
