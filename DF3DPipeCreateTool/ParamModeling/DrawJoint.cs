using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawJoint : DrawScanModel, IDrawJoint, IDrawScanModel, IDrawGeometry
    {
        // Fields
        protected IPipeSection[] _sections;
        protected Vector[][] _vtx;

        // Methods
        #region 双壁绘制-原代码
        /// <summary>
        /// 绘制两通
        /// </summary>
        /// <param name="dVtxs"></param>
        /// <param name="dSections"></param>
        /// <param name="fmodel"></param>
        /// <returns></returns>
        //public bool DrawConBetween(Vector[][] dVtxs, IPipeSection[] dSections, ref IModel fmodel)
        //{
        //    Vector[] array = null;
        //    double[][] numArray = null;
        //    Vector w = null;
        //    Vector u = null;
        //    Vector v = null;
        //    Vector vector4 = null;
        //    Vector vector5 = null;
        //    Vector vector6 = null;
        //    List<ushort> list = null;
        //    List<ushort> list2 = null;
        //    List<ushort> list3 = null;
        //    try
        //    {
        //        int num;
        //        int num2;
        //        if ((dVtxs.Length != 2) || (dSections.Length != 2))
        //        {
        //            return false;
        //        }
        //        if (dSections[0].SecShape != dSections[1].SecShape)
        //        {
        //            return false;
        //        }
        //        if (!DrawGeometry.Compare(dVtxs[0][0], dVtxs[1][0], false))
        //        {
        //            return false;
        //        }
        //        int segCount = dSections[0].SegCount;
        //        SecShape secShape = dSections[0].SecShape;
        //        IDrawGroup group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);
        //        IDrawPrimitive primitive2 = group.GetPrimitive(1);
        //        IDrawPrimitive primitive3 = group.GetPrimitive(2);
        //        list = new List<ushort>();
        //        list2 = new List<ushort>();
        //        list3 = new List<ushort>();
        //        List<Vector> vtxs = new List<Vector>();
        //        for (num = dVtxs[0].Length - 1; num >= 0; num--)
        //        {
        //            vtxs.Add(dVtxs[0][num]);
        //        }
        //        for (num = 1; num < dVtxs[0].Length; num++)
        //        {
        //            vtxs.Add(dVtxs[1][num]);
        //        }
        //        vtxs = Maths.DisperseArc(vtxs, Math.Min(dSections[0].Diameter, dSections[1].Diameter));
        //        Vector vector7 = (dVtxs[0][dVtxs.Length - 1] - dVtxs[0][0]).UnitVector();
        //        Vector vector8 = (dVtxs[1][dVtxs.Length - 1] - dVtxs[1][0]).UnitVector();
        //        array = new Vector[vtxs.Count + 4];
        //        array[0] = vtxs[0] + ((Vector)((vector7 * dSections[0].Diameter) / 2.0));
        //        array[1] = vtxs[0] + ((Vector)(vector7 * dSections[0].Thick));
        //        vtxs.CopyTo(array, 2);
        //        array[array.Length - 2] = vtxs[vtxs.Count - 1] + ((Vector)(vector8 * dSections[0].Thick));
        //        array[array.Length - 1] = vtxs[vtxs.Count - 1] + ((Vector)((vector8 * dSections[1].Diameter) / 2.0));
        //        double[] numArray2 = new double[array.Length];
        //        for (num = 0; num < array.Length; num++)
        //        {
        //            if (num == 0)
        //            {
        //                numArray2[num] = 0.0;
        //            }
        //            else
        //            {
        //                numArray2[num] = (array[num] - array[num - 1]).Length + numArray2[num - 1];
        //            }
        //        }
        //        double naN = double.NaN;
        //        numArray = new double[array.Length][];
        //        if (dSections[0].Diameter != dSections[1].Diameter)
        //        {
        //            double diameter = dSections[0].Diameter;
        //            double num6 = dSections[1].Diameter - dSections[0].Diameter;
        //            for (num = 0; num < array.Length; num++)
        //            {
        //                switch (num)
        //                {
        //                    case 0:
        //                    case 1:
        //                        naN = (dSections[0].Thick + dSections[0].Diameter) / dSections[0].Diameter;
        //                        numArray[num] = dSections[0].GetVtxs(naN, 0.0, dSections[0].Thick);
        //                        break;

        //                    default:
        //                        if ((num == (array.Length - 2)) || (num == (array.Length - 1)))
        //                        {
        //                            naN = (dSections[1].Thick + dSections[1].Diameter) / dSections[1].Diameter;
        //                            numArray[num] = dSections[1].GetVtxs(naN, 0.0, dSections[1].Thick);
        //                        }
        //                        else
        //                        {
        //                            naN = (((numArray2[num] - numArray2[2]) / (numArray2[numArray2.Length - 3] - numArray2[2])) * num6) / diameter;
        //                            numArray[num] = dSections[0].GetVtxs(naN + 1.0, 0.0, 0.0);
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            naN = (dSections[0].Thick + dSections[0].Diameter) / dSections[0].Diameter;
        //            for (num = 0; num < array.Length; num++)
        //            {
        //                if (((num == 0) || (num == 1)) || ((num == (array.Length - 2)) || (num == (array.Length - 1))))
        //                {
        //                    numArray[num] = dSections[0].GetVtxs(naN, 0.0, dSections[0].Thick);
        //                }
        //                else
        //                {
        //                    numArray[num] = dSections[0].GetVtxs();
        //                }
        //            }
        //        }
        //        int num7 = 0;
        //        Vector vector9 = (array[1] - array[0]).UnitVector();
        //        Vector vector10 = (array[array.Length - 1] - array[array.Length - 2]).UnitVector();
        //        if (((Math.Abs((double)(vector9.X - vector10.X)) < 0.08) && (Math.Abs((double)(vector9.Y - vector10.Y)) < 0.08)) && (Math.Abs((double)(vector9.Z - vector10.Z)) < 0.08))
        //        {
        //            num7 = 0;
        //        }
        //        else
        //        {
        //            Maths.GenerateComplementBasis((array[array.Length - 2] - array[0]).UnitVector(), out u, out v);
        //            if (Math.Abs(((vector9 * vector10)).UnitVector().Z) < 0.8)
        //            {
        //                num7 = 1;
        //            }
        //            else
        //            {
        //                num7 = 2;
        //            }
        //        }
        //        num = 0;
        //        while (num < array.Length)
        //        {
        //            if (num == (array.Length - 1))
        //            {
        //                w = (array[num] - array[num - 1]).UnitVector();
        //            }
        //            else
        //            {
        //                w = (array[num + 1] - array[num]).UnitVector();
        //            }
        //            switch (num7)
        //            {
        //                case 0:
        //                    Maths.GenerateComplementBasis(w, out u, out v);
        //                    break;

        //                case 1:
        //                    v = (u * w).UnitVector();
        //                    break;

        //                default:
        //                    u = (w * v).UnitVector();
        //                    break;
        //            }
        //            num2 = 0;
        //            while (num2 <= segCount)
        //            {
        //                vector6 = (((numArray[num][num2 * 4] - dSections[0].OffsetX) * u) + ((numArray[num][(num2 * 4) + 1] - dSections[0].OffsetY) * v)).UnitVector();
        //                vector4 = array[num] + ((Vector)((numArray[num][num2 * 4] * u) + (numArray[num][(num2 * 4) + 1] * v)));
        //                vector5 = array[num] + ((Vector)((numArray[num][(num2 * 4) + 2] * u) + (numArray[num][(num2 * 4) + 3] * v)));
        //                if ((num == 0) || (num == (array.Length - 1)))
        //                {
        //                    primitive3.VertexArray.Append((float)vector4.X);
        //                    primitive3.VertexArray.Append((float)vector4.Y);
        //                    primitive3.VertexArray.Append((float)vector4.Z);
        //                    list3.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
        //                    primitive3.VertexArray.Append((float)vector5.X);
        //                    primitive3.VertexArray.Append((float)vector5.Y);
        //                    primitive3.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive3.TexcoordArray.Append(num * 0.4f);
        //                        primitive3.TexcoordArray.Append(num2 * 0.4f);
        //                        primitive3.TexcoordArray.Append(num * 0.6f);
        //                        primitive3.TexcoordArray.Append(num2 * 0.6f);
        //                    }
        //                    primitive3.NormalArray.Append(-((float)w.X));
        //                    primitive3.NormalArray.Append(-((float)w.Y));
        //                    primitive3.NormalArray.Append(-((float)w.Z));
        //                    primitive3.NormalArray.Append((float)w.X);
        //                    primitive3.NormalArray.Append((float)w.Y);
        //                    primitive3.NormalArray.Append((float)w.Z);
        //                }
        //                if (secShape == SecShape.CircleRing)
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list2.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                    }
        //                    primitive.NormalArray.Append((float)vector6.X);
        //                    primitive.NormalArray.Append((float)vector6.Y);
        //                    primitive.NormalArray.Append((float)vector6.Z);
        //                    primitive2.NormalArray.Append(-((float)vector6.X));
        //                    primitive2.NormalArray.Append(-((float)vector6.Y));
        //                    primitive2.NormalArray.Append(-((float)vector6.Z));
        //                }
        //                else
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list2.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list2.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                        primitive.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                    }
        //                    primitive.NormalArray.Append((float)v.X);
        //                    primitive.NormalArray.Append((float)v.Y);
        //                    primitive.NormalArray.Append((float)v.Z);
        //                    primitive.NormalArray.Append((float)-u.X);
        //                    primitive.NormalArray.Append((float)-u.Y);
        //                    primitive.NormalArray.Append((float)-u.Z);
        //                    primitive2.NormalArray.Append(-((float)-v.X));
        //                    primitive2.NormalArray.Append(-((float)-v.Y));
        //                    primitive2.NormalArray.Append(-((float)-v.Z));
        //                    primitive2.NormalArray.Append(-((float)u.X));
        //                    primitive2.NormalArray.Append(-((float)u.Y));
        //                    primitive2.NormalArray.Append(-((float)u.Z));
        //                }
        //                num2++;
        //            }
        //            num++;
        //        }
        //        int num8 = (segCount + 1) * 2;
        //        for (num = 0; num < (array.Length - 1); num++)
        //        {
        //            for (num2 = 0; num2 < segCount; num2++)
        //            {
        //                if (num == 0)
        //                {
        //                    primitive3.IndexArray.Append(list3[num2 * 2]);
        //                    primitive3.IndexArray.Append(list3[(num2 * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[((num2 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[num2 * 2]);
        //                    primitive3.IndexArray.Append(list3[((num2 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[(num2 + 1) * 2]);
        //                }
        //                if (secShape == SecShape.CircleRing)
        //                {
        //                    primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list[((num + 1) * (segCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list[((num * (segCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list2[(num * (segCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list2[((num + 1) * (segCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list2[(((num + 1) * (segCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list2[(num * (segCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list2[(((num + 1) * (segCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list2[((num * (segCount + 1)) + num2) + 1]);
        //                }
        //                else
        //                {
        //                    primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                }
        //                if (num == (array.Length - 2))
        //                {
        //                    primitive3.IndexArray.Append(list3[num8 + (num2 * 2)]);
        //                    primitive3.IndexArray.Append(list3[(num8 + ((num2 + 1) * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list3[(num8 + (num2 * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list3[num8 + (num2 * 2)]);
        //                    primitive3.IndexArray.Append(list3[num8 + ((num2 + 1) * 2)]);
        //                    primitive3.IndexArray.Append(list3[(num8 + ((num2 + 1) * 2)) + 1]);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        /// <summary>
        /// 绘制单通
        /// </summary>
        /// <param name="dVtx"></param>
        /// <param name="dSection"></param>
        /// <param name="fmodel"></param>
        /// <returns></returns>
        //public bool DrawConSingle(Vector[] dVtx, IPipeSection dSection, ref IModel fmodel)
        //{
        //    Vector[] vectorArray = null;
        //    double[][] numArray = null;
        //    List<ushort> list = null;
        //    List<ushort> list2 = null;
        //    List<ushort> list3 = null;
        //    try
        //    {
        //        int num;
        //        int num2;
        //        int segCount = dSection.SegCount;
        //        SecShape secShape = dSection.SecShape;
        //        IDrawGroup group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);
        //        IDrawPrimitive primitive2 = group.GetPrimitive(1);
        //        IDrawPrimitive primitive3 = group.GetPrimitive(2);
        //        list = new List<ushort>();
        //        list2 = new List<ushort>();
        //        list3 = new List<ushort>();
        //        Vector vector7 = (dVtx[1] - dVtx[0]).UnitVector();
        //        vectorArray = new Vector[] { dVtx[0], dVtx[1], dVtx[1] + (vector7 * dSection.Thick), dVtx[1] + ((vector7 * dSection.Diameter) / 2.0) };
        //        double[] numArray2 = new double[vectorArray.Length];
        //        for (num = 0; num < vectorArray.Length; num++)
        //        {
        //            if (num == 0)
        //            {
        //                numArray2[num] = 0.0;
        //            }
        //            else
        //            {
        //                numArray2[num] = (vectorArray[num] - vectorArray[num - 1]).Length + numArray2[num - 1];
        //            }
        //        }
        //        double naN = double.NaN;
        //        numArray = new double[vectorArray.Length][];
        //        for (num = 0; num < vectorArray.Length; num++)
        //        {
        //            if ((num == (vectorArray.Length - 2)) || (num == (vectorArray.Length - 1)))
        //            {
        //                naN = (dSection.Thick + dSection.Diameter) / dSection.Diameter;
        //                numArray[num] = dSection.GetVtxs(naN, 0.0, dSection.Thick);
        //            }
        //            else
        //            {
        //                numArray[num] = dSection.GetVtxs();
        //            }
        //        }
        //        for (num = 0; num < vectorArray.Length; num++)
        //        {
        //            Vector vector;
        //            Vector vector2;
        //            Vector vector3;
        //            if (num == (vectorArray.Length - 1))
        //            {
        //                vector = (vectorArray[num] - vectorArray[num - 1]).UnitVector();
        //            }
        //            else
        //            {
        //                vector = (vectorArray[num + 1] - vectorArray[num]).UnitVector();
        //            }
        //            Maths.GenerateComplementBasis(vector, out vector2, out vector3);
        //            num2 = 0;
        //            while (num2 <= segCount)
        //            {
        //                Vector vector6 = (((numArray[num][num2 * 4] - dSection.OffsetX) * vector2) + ((numArray[num][(num2 * 4) + 1] - dSection.OffsetY) * vector3)).UnitVector();
        //                Vector vector4 = vectorArray[num] + ((Vector)((numArray[num][num2 * 4] * vector2) + (numArray[num][(num2 * 4) + 1] * vector3)));
        //                Vector vector5 = vectorArray[num] + ((Vector)((numArray[num][(num2 * 4) + 2] * vector2) + (numArray[num][(num2 * 4) + 3] * vector3)));
        //                if ((num == 0) || (num == (vectorArray.Length - 1)))
        //                {
        //                    primitive3.VertexArray.Append((float)vector4.X);
        //                    primitive3.VertexArray.Append((float)vector4.Y);
        //                    primitive3.VertexArray.Append((float)vector4.Z);
        //                    list3.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
        //                    primitive3.VertexArray.Append((float)vector5.X);
        //                    primitive3.VertexArray.Append((float)vector5.Y);
        //                    primitive3.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive3.TexcoordArray.Append(num * 0.4f);
        //                        primitive3.TexcoordArray.Append(num2 * 0.4f);
        //                        primitive3.TexcoordArray.Append(num * 0.6f);
        //                        primitive3.TexcoordArray.Append(num2 * 0.6f);
        //                    }
        //                    primitive3.NormalArray.Append(-((float)vector.X));
        //                    primitive3.NormalArray.Append(-((float)vector.Y));
        //                    primitive3.NormalArray.Append(-((float)vector.Z));
        //                    primitive3.NormalArray.Append((float)vector.X);
        //                    primitive3.NormalArray.Append((float)vector.Y);
        //                    primitive3.NormalArray.Append((float)vector.Z);
        //                }
        //                if (secShape == SecShape.CircleRing)
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list2.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                    }
        //                    primitive.NormalArray.Append((float)vector6.X);
        //                    primitive.NormalArray.Append((float)vector6.Y);
        //                    primitive.NormalArray.Append((float)vector6.Z);
        //                    primitive2.NormalArray.Append(-((float)vector6.X));
        //                    primitive2.NormalArray.Append(-((float)vector6.Y));
        //                    primitive2.NormalArray.Append(-((float)vector6.Z));
        //                }
        //                else
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list2.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list2.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                        primitive.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
        //                    }
        //                    primitive.NormalArray.Append((float)vector3.X);
        //                    primitive.NormalArray.Append((float)vector3.Y);
        //                    primitive.NormalArray.Append((float)vector3.Z);
        //                    primitive.NormalArray.Append((float)-vector2.X);
        //                    primitive.NormalArray.Append((float)-vector2.Y);
        //                    primitive.NormalArray.Append((float)-vector2.Z);
        //                    primitive2.NormalArray.Append(-((float)-vector3.X));
        //                    primitive2.NormalArray.Append(-((float)-vector3.Y));
        //                    primitive2.NormalArray.Append(-((float)-vector3.Z));
        //                    primitive2.NormalArray.Append(-((float)vector2.X));
        //                    primitive2.NormalArray.Append(-((float)vector2.Y));
        //                    primitive2.NormalArray.Append(-((float)vector2.Z));
        //                }
        //                num2++;
        //            }
        //        }
        //        int num5 = (segCount + 1) * 2;
        //        for (num = 0; num < (vectorArray.Length - 1); num++)
        //        {
        //            for (num2 = 0; num2 < segCount; num2++)
        //            {
        //                if (num == 0)
        //                {
        //                    primitive3.IndexArray.Append(list3[num2 * 2]);
        //                    primitive3.IndexArray.Append(list3[(num2 * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[((num2 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[num2 * 2]);
        //                    primitive3.IndexArray.Append(list3[((num2 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[(num2 + 1) * 2]);
        //                }
        //                if (secShape == SecShape.CircleRing)
        //                {
        //                    primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list[((num + 1) * (segCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list[((num * (segCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list2[(num * (segCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list2[((num + 1) * (segCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list2[(((num + 1) * (segCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list2[(num * (segCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list2[(((num + 1) * (segCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list2[((num * (segCount + 1)) + num2) + 1]);
        //                }
        //                else
        //                {
        //                    primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                }
        //                if (num == (vectorArray.Length - 2))
        //                {
        //                    primitive3.IndexArray.Append(list3[num5 + (num2 * 2)]);
        //                    primitive3.IndexArray.Append(list3[(num5 + ((num2 + 1) * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list3[(num5 + (num2 * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list3[num5 + (num2 * 2)]);
        //                    primitive3.IndexArray.Append(list3[num5 + ((num2 + 1) * 2)]);
        //                    primitive3.IndexArray.Append(list3[(num5 + ((num2 + 1) * 2)) + 1]);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        #endregion

        #region 单壁绘制////-冯欣修改-20131101////////
        public bool DrawConBetween(Vector[][] dVtxs, IPipeSection[] dSections, ref IModel fmodel)
        {
            Vector[] array = null;
            double[][] numArray = null;
            Vector w = null;
            Vector u = null;
            Vector v = null;
            Vector vector4 = null;
            Vector vector5 = null;
            Vector vector6 = null;
            List<ushort> list = null;
            List<ushort> list2 = null;
            List<ushort> list3 = null;
            try
            {
                int num;
                int num2;
                if ((dVtxs.Length != 2) || (dSections.Length != 2))
                {
                    return false;
                }
                if (dSections[0].SecShape != dSections[1].SecShape)
                {
                    return false;
                }
                if (!DrawGeometry.Compare(dVtxs[0][0], dVtxs[1][0], false))
                {
                    return false;
                }
                int segCount = dSections[0].SegCount;
                SecShape secShape = dSections[0].SecShape;
                IDrawGroup group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);
                list = new List<ushort>();
                list2 = new List<ushort>();
                list3 = new List<ushort>();
                List<Vector> vtxs = new List<Vector>();
                for (num = dVtxs[0].Length - 1; num >= 0; num--)
                {
                    vtxs.Add(dVtxs[0][num]);
                }
                for (num = 1; num < dVtxs[0].Length; num++)
                {
                    vtxs.Add(dVtxs[1][num]);
                }
                vtxs = Maths.DisperseArc(vtxs, Math.Min(dSections[0].Diameter, dSections[1].Diameter));
                Vector vector7 = (dVtxs[0][dVtxs.Length - 1] - dVtxs[0][0]).UnitVector();
                Vector vector8 = (dVtxs[1][dVtxs.Length - 1] - dVtxs[1][0]).UnitVector();
                array = new Vector[vtxs.Count + 4];
                array[0] = vtxs[0] + ((Vector)((vector7 * dSections[0].Diameter) / 2.0));
                array[1] = vtxs[0] + ((Vector)(vector7 * dSections[0].Thick));
                vtxs.CopyTo(array, 2);
                array[array.Length - 2] = vtxs[vtxs.Count - 1] + ((Vector)(vector8 * dSections[0].Thick));
                array[array.Length - 1] = vtxs[vtxs.Count - 1] + ((Vector)((vector8 * dSections[1].Diameter) / 2.0));
                double[] numArray2 = new double[array.Length];
                for (num = 0; num < array.Length; num++)
                {
                    if (num == 0)
                    {
                        numArray2[num] = 0.0;
                    }
                    else
                    {
                        numArray2[num] = (array[num] - array[num - 1]).Length + numArray2[num - 1];
                    }
                }
                double naN = double.NaN;
                numArray = new double[array.Length][];
                if (dSections[0].Diameter != dSections[1].Diameter)
                {
                    double diameter = dSections[0].Diameter;
                    double num6 = dSections[1].Diameter - dSections[0].Diameter;
                    for (num = 0; num < array.Length; num++)
                    {
                        switch (num)
                        {
                            case 0:
                            case 1:
                                naN = (dSections[0].Thick + dSections[0].Diameter) / dSections[0].Diameter;
                                numArray[num] = dSections[0].GetVtxs(naN, 0.0, dSections[0].Thick);
                                break;

                            default:
                                if ((num == (array.Length - 2)) || (num == (array.Length - 1)))
                                {
                                    naN = (dSections[1].Thick + dSections[1].Diameter) / dSections[1].Diameter;
                                    numArray[num] = dSections[1].GetVtxs(naN, 0.0, dSections[1].Thick);
                                }
                                else
                                {
                                    naN = (((numArray2[num] - numArray2[2]) / (numArray2[numArray2.Length - 3] - numArray2[2])) * num6) / diameter;
                                    numArray[num] = dSections[0].GetVtxs(naN + 1.0, 0.0, 0.0);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    naN = (dSections[0].Thick + dSections[0].Diameter) / dSections[0].Diameter;
                    for (num = 0; num < array.Length; num++)
                    {
                        if (((num == 0) || (num == 1)) || ((num == (array.Length - 2)) || (num == (array.Length - 1))))
                        {
                            numArray[num] = dSections[0].GetVtxs(naN, 0.0, dSections[0].Thick);
                        }
                        else
                        {
                            numArray[num] = dSections[0].GetVtxs();
                        }
                    }
                }
                int num7 = 0;
                Vector vector9 = (array[1] - array[0]).UnitVector();
                Vector vector10 = (array[array.Length - 1] - array[array.Length - 2]).UnitVector();
                if (((Math.Abs((double)(vector9.X - vector10.X)) < 0.08) && (Math.Abs((double)(vector9.Y - vector10.Y)) < 0.08)) && (Math.Abs((double)(vector9.Z - vector10.Z)) < 0.08))
                {
                    num7 = 0;
                }
                else
                {
                    Maths.GenerateComplementBasis((array[array.Length - 2] - array[0]).UnitVector(), out u, out v);
                    if (Math.Abs(((vector9 * vector10)).UnitVector().Z) < 0.8)
                    {
                        num7 = 1;
                    }
                    else
                    {
                        num7 = 2;
                    }
                }
                num = 0;
                while (num < array.Length)
                {
                    if (num == (array.Length - 1))
                    {
                        w = (array[num] - array[num - 1]).UnitVector();
                    }
                    else
                    {
                        w = (array[num + 1] - array[num]).UnitVector();
                    }
                    switch (num7)
                    {
                        case 0:
                            Maths.GenerateComplementBasis(w, out u, out v);
                            break;

                        case 1:
                            v = (u * w).UnitVector();
                            break;

                        default:
                            u = (w * v).UnitVector();
                            break;
                    }
                    num2 = 0;
                    while (num2 <= segCount)
                    {
                        vector6 = (((numArray[num][num2 * 4] - dSections[0].OffsetX) * u) + ((numArray[num][(num2 * 4) + 1] - dSections[0].OffsetY) * v)).UnitVector();
                        vector4 = array[num] + ((Vector)((numArray[num][num2 * 4] * u) + (numArray[num][(num2 * 4) + 1] * v)));
                        vector5 = array[num] + ((Vector)((numArray[num][(num2 * 4) + 2] * u) + (numArray[num][(num2 * 4) + 3] * v)));
                        if (secShape == SecShape.CircleRing)
                        {
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                            }
                            primitive.NormalArray.Append((float)vector6.X);
                            primitive.NormalArray.Append((float)vector6.Y);
                            primitive.NormalArray.Append((float)vector6.Z);
                        }
                        else
                        {
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                            }
                            primitive.NormalArray.Append((float)v.X);
                            primitive.NormalArray.Append((float)v.Y);
                            primitive.NormalArray.Append((float)v.Z);
                            primitive.NormalArray.Append((float)-u.X);
                            primitive.NormalArray.Append((float)-u.Y);
                            primitive.NormalArray.Append((float)-u.Z);
                        }
                        num2++;
                    }
                    num++;
                }
                primitive.Material.CullMode = gviCullFaceMode.gviCullNone;
                int num8 = (segCount + 1) * 2;
                for (num = 0; num < (array.Length - 1); num++)
                {
                    for (num2 = 0; num2 < segCount; num2++)
                    {
                        if (secShape == SecShape.CircleRing)
                        {
                            primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
                            primitive.IndexArray.Append(list[((num + 1) * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[((num * (segCount + 1)) + num2) + 1]);
                            primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
                        }
                        else
                        {
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DrawConSingle(Vector[] dVtx, IPipeSection dSection, ref IModel fmodel)
        {
            Vector[] vectorArray = null;
            double[][] numArray = null;
            List<ushort> list = null;
            List<ushort> list2 = null;
            List<ushort> list3 = null;
            try
            {
                int num;
                int num2;
                int segCount = dSection.SegCount;
                SecShape secShape = dSection.SecShape;
                IDrawGroup group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);
                list = new List<ushort>();
                list2 = new List<ushort>();
                list3 = new List<ushort>();
                Vector vector7 = (dVtx[1] - dVtx[0]).UnitVector();
                vectorArray = new Vector[] { dVtx[0], dVtx[1], dVtx[1] + (vector7 * dSection.Thick), dVtx[1] + ((vector7 * dSection.Diameter) / 2.0) };
                double[] numArray2 = new double[vectorArray.Length];
                for (num = 0; num < vectorArray.Length; num++)
                {
                    if (num == 0)
                    {
                        numArray2[num] = 0.0;
                    }
                    else
                    {
                        numArray2[num] = (vectorArray[num] - vectorArray[num - 1]).Length + numArray2[num - 1];
                    }
                }
                double naN = double.NaN;
                numArray = new double[vectorArray.Length][];
                for (num = 0; num < vectorArray.Length; num++)
                {
                    if ((num == (vectorArray.Length - 2)) || (num == (vectorArray.Length - 1)))
                    {
                        naN = (dSection.Thick + dSection.Diameter) / dSection.Diameter;
                        numArray[num] = dSection.GetVtxs(naN, 0.0, dSection.Thick);
                    }
                    else
                    {
                        numArray[num] = dSection.GetVtxs();
                    }
                }
                for (num = 0; num < vectorArray.Length; num++)
                {
                    Vector vector;
                    Vector vector2;
                    Vector vector3;
                    if (num == (vectorArray.Length - 1))
                    {
                        vector = (vectorArray[num] - vectorArray[num - 1]).UnitVector();
                    }
                    else
                    {
                        vector = (vectorArray[num + 1] - vectorArray[num]).UnitVector();
                    }
                    Maths.GenerateComplementBasis(vector, out vector2, out vector3);
                    num2 = 0;
                    while (num2 <= segCount)
                    {
                        Vector vector6 = (((numArray[num][num2 * 4] - dSection.OffsetX) * vector2) + ((numArray[num][(num2 * 4) + 1] - dSection.OffsetY) * vector3)).UnitVector();
                        Vector vector4 = vectorArray[num] + ((Vector)((numArray[num][num2 * 4] * vector2) + (numArray[num][(num2 * 4) + 1] * vector3)));
                        if (secShape == SecShape.CircleRing)
                        {
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                            }
                            primitive.NormalArray.Append((float)vector6.X);
                            primitive.NormalArray.Append((float)vector6.Y);
                            primitive.NormalArray.Append((float)vector6.Z);
                        }
                        else
                        {
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                            }
                            primitive.NormalArray.Append((float)vector3.X);
                            primitive.NormalArray.Append((float)vector3.Y);
                            primitive.NormalArray.Append((float)vector3.Z);
                            primitive.NormalArray.Append((float)-vector2.X);
                            primitive.NormalArray.Append((float)-vector2.Y);
                            primitive.NormalArray.Append((float)-vector2.Z);
                        }
                        num2++;
                    }
                }
                primitive.Material.CullMode = gviCullFaceMode.gviCullNone;
                int num5 = (segCount + 1) * 2;
                for (num = 0; num < (vectorArray.Length - 1); num++)
                {
                    for (num2 = 0; num2 < segCount; num2++)
                    {
                        if (secShape == SecShape.CircleRing)
                        {
                            primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
                            primitive.IndexArray.Append(list[((num + 1) * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[((num * (segCount + 1)) + num2) + 1]);
                            primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
                        }
                        else
                        {
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 断头绘制(封口) FX 2014.04.04
        public bool DrawConSingle1(Vector[] dVtx, IPipeSection dSection, ref IModel fmodel)
        {
            Vector[] vectorArray = null;
            double[][] numArray = null;
            List<ushort> list = null;
            List<ushort> list2 = null;
            List<ushort> list3 = null;
            try
            {
                int num;
                int num2;
                int segCount = dSection.SegCount;
                SecShape secShape = dSection.SecShape;
                IDrawGroup group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);
                IDrawPrimitive primitive3 = group.GetPrimitive(1);
                list = new List<ushort>();
                list2 = new List<ushort>();
                list3 = new List<ushort>();
                Vector vector7 = (dVtx[1] - dVtx[0]).UnitVector();
                /////////////-冯欣修改-20131102///////////////
                Vector vector8 = (dVtx[1] - dVtx[0]);
                double length = vector8.Length;
                if (length < (dSection.Diameter / 2.0))
                {
                    vectorArray = new Vector[] { dVtx[0], dVtx[1], dVtx[1] + (vector7 * dSection.Thick), dVtx[1] + (vector7 * length) };
                }
                else
                {
                    vectorArray = new Vector[] { dVtx[0], dVtx[1], dVtx[1] + (vector7 * dSection.Thick), dVtx[1] + ((vector7 * dSection.Diameter) / 2.0) };
                }
                ///////////////////////////////////////////////
                //vectorArray = new Vector[] { dVtx[0], dVtx[1], dVtx[1] + (vector7 * dSection.Thick), dVtx[1] + ((vector7 * dSection.Diameter) / 2.0) };
                double[] numArray2 = new double[vectorArray.Length];
                for (num = 0; num < vectorArray.Length; num++)
                {
                    if (num == 0)
                    {
                        numArray2[num] = 0.0;
                    }
                    else
                    {
                        numArray2[num] = (vectorArray[num] - vectorArray[num - 1]).Length + numArray2[num - 1];
                    }
                }
                double naN = double.NaN;
                numArray = new double[vectorArray.Length][];
                for (num = 0; num < vectorArray.Length; num++)
                {
                    if ((num == (vectorArray.Length - 2)) || (num == (vectorArray.Length - 1)))
                    {
                        naN = (dSection.Thick + dSection.Diameter) / dSection.Diameter;
                        numArray[num] = dSection.GetVtxs(naN, 0.0, dSection.Thick);
                    }
                    else
                    {
                        numArray[num] = dSection.GetVtxs();
                    }
                }
                for (num = 0; num < vectorArray.Length; num++)
                {
                    Vector vector;
                    Vector vector2;
                    Vector vector3;
                    if (num == (vectorArray.Length - 1))
                    {
                        vector = (vectorArray[num] - vectorArray[num - 1]).UnitVector();
                    }
                    else
                    {
                        vector = (vectorArray[num + 1] - vectorArray[num]).UnitVector();
                    }
                    Maths.GenerateComplementBasis(vector, out vector2, out vector3);
                    num2 = 0;
                    int IsAdd = -1;
                    while (num2 <= segCount)
                    {
                        Vector vector6 = (((numArray[num][num2 * 4] - dSection.OffsetX) * vector2) + ((numArray[num][(num2 * 4) + 1] - dSection.OffsetY) * vector3)).UnitVector();
                        Vector vector4 = vectorArray[num] + ((Vector)((numArray[num][num2 * 4] * vector2) + (numArray[num][(num2 * 4) + 1] * vector3)));
                        //Vector vector5 = vectorArray[num] + ((Vector)((numArray[num][(num2 * 4) + 2] * vector2) + (numArray[num][(num2 * 4) + 3] * vector3)));
                        if (num == 0)
                        {
                            if (IsAdd < 0)
                            {
                                primitive3.VertexArray.Append((float)vectorArray[num].X);
                                primitive3.VertexArray.Append((float)vectorArray[num].Y);
                                primitive3.VertexArray.Append((float)vectorArray[num].Z);
                                list3.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
                                primitive3.NormalArray.Append(-((float)vector.X));
                                primitive3.NormalArray.Append(-((float)vector.Y));
                                primitive3.NormalArray.Append(-((float)vector.Z));
                                IsAdd = 1;
                            }
                            primitive3.VertexArray.Append((float)vector4.X);
                            primitive3.VertexArray.Append((float)vector4.Y);
                            primitive3.VertexArray.Append((float)vector4.Z);
                            list3.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive3.TexcoordArray.Append(num * 0.4f);
                                primitive3.TexcoordArray.Append(num2 * 0.4f);
                                primitive3.TexcoordArray.Append(num * 0.6f);
                                primitive3.TexcoordArray.Append(num2 * 0.6f);
                            }
                            primitive3.NormalArray.Append(-((float)vector.X));
                            primitive3.NormalArray.Append(-((float)vector.Y));
                            primitive3.NormalArray.Append(-((float)vector.Z));
                        }
                        if (secShape == SecShape.CircleRing)
                        {
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                            }
                            primitive.NormalArray.Append((float)vector6.X);
                            primitive.NormalArray.Append((float)vector6.Y);
                            primitive.NormalArray.Append((float)vector6.Z);
                        }
                        else
                        {
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                                primitive.TexcoordArray.Append((float)numArray2[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)segCount));
                            }
                            primitive.NormalArray.Append((float)vector3.X);
                            primitive.NormalArray.Append((float)vector3.Y);
                            primitive.NormalArray.Append((float)vector3.Z);
                            primitive.NormalArray.Append((float)-vector2.X);
                            primitive.NormalArray.Append((float)-vector2.Y);
                            primitive.NormalArray.Append((float)-vector2.Z);
                        }
                        num2++;
                    }
                }
                primitive.Material.CullMode = gviCullFaceMode.gviCullNone;
                primitive3.Material.CullMode = gviCullFaceMode.gviCullNone;
                int num5 = (segCount + 1) * 2;
                for (num = 0; num < (vectorArray.Length - 1); num++)
                {
                    for (num2 = 0; num2 < segCount; num2++)
                    {
                        if (num == 0)
                        {
                            primitive3.IndexArray.Append(list3[0]);
                            primitive3.IndexArray.Append(list3[(num2 + 1)]);
                            primitive3.IndexArray.Append(list3[(num2 + 1) + 1]);
                        }
                        if (secShape == SecShape.CircleRing)
                        {
                            primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
                            primitive.IndexArray.Append(list[((num + 1) * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[(num * (segCount + 1)) + num2]);
                            primitive.IndexArray.Append(list[((num * (segCount + 1)) + num2) + 1]);
                            primitive.IndexArray.Append(list[(((num + 1) * (segCount + 1)) + num2) + 1]);
                        }
                        else
                        {
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list[(((num * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                            primitive.IndexArray.Append(list[((((num + 1) * (segCount + 1)) * 2) + (num2 * 2)) + 2]);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 设置渲染颜色
        public override void SetColorRender(uint[] colors)
        {
            base.SetColorRender(colors);
        }
        #endregion

        #region 设置参数-中心点/相关管线坐标
        public virtual void SetParameter(Vector center, Vector[][] vtx, IPipeSection[] sections)
        {
            try
            {
                this._vtx = vtx;
                this._sections = sections;
                base._x = center.X;
                base._y = center.Y;
                base._z = center.Z;
                for (int i = 0; i < vtx.Length; i++)
                {
                    for (int j = 0; j < vtx[i].Length; j++)
                    {
                        this._vtx[i][j] = new Vector(this._vtx[i][j].X - base._x, this._vtx[i][j].Y - base._y, this._vtx[i][j].Z - base._z);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
        #endregion

        #region 设置渲染纹理
        public override void SetTextureRender(string[] tcNames)
        {
            base.SetTextureRender(tcNames);
        }
        #endregion

        // Properties
        public IPipeSection[] Sections
        {
            get
            {
                return this._sections;
            }
            set
            {
                this._sections = value;
            }
        }
        public Vector[][] Vtx
        {
            get
            {
                return this._vtx;
            }
            set
            {
                this._vtx = value;
            }
        }
    }
}
