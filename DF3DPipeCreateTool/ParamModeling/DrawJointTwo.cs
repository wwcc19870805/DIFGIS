using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawJointTwo : DrawJoint, IDrawJointTwo, IDrawJoint, IDrawScanModel, IDrawGeometry
    {
        // Methods
        public DrawJointTwo()
        {
            base._modeltype = ModelType.JointTwo;
        }
        #region 双壁绘制－－原代码
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    Vector[] array = null;
        //    double[][] numArray = null;
        //    List<ushort> list = null;
        //    List<ushort> list2 = null;
        //    List<ushort> list3 = null;
        //    base.Draw(out mp, out fmodel, out smodel);
        //    try
        //    {
        //        int segCount;
        //        SecShape secShape;
        //        int num2;
        //        int num3;
        //        double[] numArray2;
        //        IDrawGroup group;
        //        if ((base._vtx.Length == 2) && (base._sections.Length == 2))
        //        {
        //            if (base._sections[0].SecShape != base._sections[1].SecShape)
        //            {
        //                return false;
        //            }
        //            if (!DrawGeometry.Compare(base._vtx[0][0], base._vtx[1][0], true))
        //            {
        //                return false;
        //            }
        //            segCount = base._sections[0].SegCount;
        //            secShape = base._sections[0].SecShape;
        //            list = new List<ushort>();
        //            list2 = new List<ushort>();
        //            list3 = new List<ushort>();
        //            List<Vector> vtxs = new List<Vector>();
        //            if ((base._vtx[0][0].Z - base._vtx[1][0].Z) > base._sections[0].Diameter)
        //            {
        //                for (num2 = base._vtx[0].Length - 1; num2 >= 0; num2--)
        //                {
        //                    vtxs.Add(base._vtx[0][num2]);
        //                }
        //                for (num2 = 0; num2 < base._vtx[0].Length; num2++)
        //                {
        //                    vtxs.Add(base._vtx[1][num2]);
        //                }
        //                vtxs = Maths.DisperseArc(vtxs, Math.Min(base._sections[0].Diameter, base._sections[1].Diameter));
        //            }
        //            else
        //            {
        //                num2 = base._vtx[0].Length - 1;
        //                while (num2 >= 0)
        //                {
        //                    vtxs.Add(base._vtx[0][num2]);
        //                    num2--;
        //                }
        //                for (num2 = 1; num2 < base._vtx[0].Length; num2++)
        //                {
        //                    vtxs.Add(base._vtx[1][num2]);
        //                }
        //                vtxs = Maths.DisperseArc(vtxs, Math.Min(base._sections[0].Diameter, base._sections[1].Diameter));
        //            }
        //            Vector vector7 = (base._vtx[0][base._vtx.Length - 1] - base._vtx[0][0]).UnitVector();
        //            Vector vector8 = (base._vtx[1][base._vtx.Length - 1] - base._vtx[1][0]).UnitVector();
        //            array = new Vector[vtxs.Count + 4];
        //            array[0] = vtxs[0] + ((Vector)((vector7 * base._sections[0].Diameter) / 2.0));
        //            array[1] = vtxs[0] + ((Vector)(vector7 * base._sections[0].Thick));
        //            vtxs.CopyTo(array, 2);
        //            array[array.Length - 2] = vtxs[vtxs.Count - 1] + ((Vector)(vector8 * base._sections[0].Thick));
        //            array[array.Length - 1] = vtxs[vtxs.Count - 1] + ((Vector)((vector8 * base._sections[1].Diameter) / 2.0));
        //            numArray2 = new double[array.Length];
        //            for (num2 = 0; num2 < array.Length; num2++)
        //            {
        //                if (num2 == 0)
        //                {
        //                    numArray2[num2] = 0.0;
        //                }
        //                else
        //                {
        //                    numArray2[num2] = (array[num2] - array[num2 - 1]).Length + numArray2[num2 - 1];
        //                }
        //            }
        //            double naN = double.NaN;
        //            numArray = new double[array.Length][];
        //            if (base._sections[0].Diameter != base._sections[1].Diameter)
        //            {
        //                double diameter = base._sections[0].Diameter;
        //                double num6 = base._sections[1].Diameter - base._sections[0].Diameter;
        //                for (num2 = 0; num2 < array.Length; num2++)
        //                {
        //                    switch (num2)
        //                    {
        //                        case 0:
        //                        case 1:
        //                            naN = (base._sections[0].Thick + base._sections[0].Diameter) / base._sections[0].Diameter;
        //                            numArray[num2] = base._sections[0].GetVtxs(naN, 0.0, base._sections[0].Thick);
        //                            break;

        //                        default:
        //                            if ((num2 == (array.Length - 2)) || (num2 == (array.Length - 1)))
        //                            {
        //                                naN = (base._sections[1].Thick + base._sections[1].Diameter) / base._sections[1].Diameter;
        //                                numArray[num2] = base._sections[1].GetVtxs(naN, 0.0, base._sections[1].Thick);
        //                            }
        //                            else
        //                            {
        //                                naN = (((numArray2[num2] - numArray2[2]) / (numArray2[numArray2.Length - 3] - numArray2[2])) * num6) / diameter;
        //                                numArray[num2] = base._sections[0].GetVtxs(naN + 1.0, 0.0, 0.0);
        //                            }
        //                            break;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                naN = (base._sections[0].Thick + base._sections[0].Diameter) / base._sections[0].Diameter;
        //                for (num2 = 0; num2 < array.Length; num2++)
        //                {
        //                    if (((num2 == 0) || (num2 == 1)) || ((num2 == (array.Length - 2)) || (num2 == (array.Length - 1))))
        //                    {
        //                        numArray[num2] = base._sections[0].GetVtxs(naN, 0.0, base._sections[0].Thick);
        //                    }
        //                    else
        //                    {
        //                        numArray[num2] = base._sections[0].GetVtxs();
        //                    }
        //                }
        //            }
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            int[] index = new int[3];
        //            index[1] = 1;
        //            index[2] = 2;
        //            if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
        //            {
        //                int[] numArray4 = new int[1];
        //                if (base.NewEmptyModel(numArray4, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_05D6;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_05D6:
        //        group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);
        //        IDrawPrimitive primitive2 = group.GetPrimitive(1);
        //        IDrawPrimitive primitive3 = group.GetPrimitive(2);
        //        IDrawPrimitive primitive4 = smodel.GetGroup(0).GetPrimitive(0);
        //        for (num2 = 0; num2 < array.Length; num2++)
        //        {
        //            Vector vector;
        //            Vector vector2;
        //            Vector vector3;
        //            if (num2 == (array.Length - 1))
        //            {
        //                vector = (array[num2] - array[num2 - 1]).UnitVector();
        //            }
        //            else
        //            {
        //                vector = (array[num2 + 1] - array[num2]).UnitVector();
        //            }
        //            Maths.GenerateComplementBasis(vector, out vector2, out vector3);
        //            num3 = 0;
        //            while (num3 <= segCount)
        //            {
        //                Vector vector6 = (((numArray[num2][num3 * 4] - base._sections[0].OffsetX) * vector2) + ((numArray[num2][(num3 * 4) + 1] - base._sections[0].OffsetY) * vector3)).UnitVector();
        //                Vector vector4 = array[num2] + ((Vector)((numArray[num2][num3 * 4] * vector2) + (numArray[num2][(num3 * 4) + 1] * vector3)));
        //                Vector vector5 = array[num2] + ((Vector)((numArray[num2][(num3 * 4) + 2] * vector2) + (numArray[num2][(num3 * 4) + 3] * vector3)));
        //                if ((num2 == 0) || (num2 == (array.Length - 1)))
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
        //                        primitive3.TexcoordArray.Append(num2 * 0.4f);
        //                        primitive3.TexcoordArray.Append(num3 * 0.4f);
        //                        primitive3.TexcoordArray.Append(num2 * 0.6f);
        //                        primitive3.TexcoordArray.Append(num3 * 0.6f);
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
        //                        primitive.TexcoordArray.Append((float)numArray2[num2]);
        //                        primitive.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num2]);
        //                        primitive2.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
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
        //                        primitive.TexcoordArray.Append((float)numArray2[num2]);
        //                        primitive.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
        //                        primitive.TexcoordArray.Append((float)numArray2[num2]);
        //                        primitive.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num2]);
        //                        primitive2.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
        //                        primitive2.TexcoordArray.Append((float)numArray2[num2]);
        //                        primitive2.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
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
        //                num3++;
        //            }
        //        }
        //        int num7 = (segCount + 1) * 2;
        //        for (num2 = 0; num2 < (array.Length - 1); num2++)
        //        {
        //            for (num3 = 0; num3 < segCount; num3++)
        //            {
        //                if (num2 == 0)
        //                {
        //                    primitive3.IndexArray.Append(list3[num3 * 2]);
        //                    primitive3.IndexArray.Append(list3[(num3 * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[((num3 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[num3 * 2]);
        //                    primitive3.IndexArray.Append(list3[((num3 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list3[(num3 + 1) * 2]);
        //                }
        //                if (secShape == SecShape.CircleRing)
        //                {
        //                    primitive.IndexArray.Append(list[(num2 * (segCount + 1)) + num3]);
        //                    primitive.IndexArray.Append(list[(((num2 + 1) * (segCount + 1)) + num3) + 1]);
        //                    primitive.IndexArray.Append(list[((num2 + 1) * (segCount + 1)) + num3]);
        //                    primitive.IndexArray.Append(list[(num2 * (segCount + 1)) + num3]);
        //                    primitive.IndexArray.Append(list[((num2 * (segCount + 1)) + num3) + 1]);
        //                    primitive.IndexArray.Append(list[(((num2 + 1) * (segCount + 1)) + num3) + 1]);
        //                    primitive2.IndexArray.Append(list2[(num2 * (segCount + 1)) + num3]);
        //                    primitive2.IndexArray.Append(list2[((num2 + 1) * (segCount + 1)) + num3]);
        //                    primitive2.IndexArray.Append(list2[(((num2 + 1) * (segCount + 1)) + num3) + 1]);
        //                    primitive2.IndexArray.Append(list2[(num2 * (segCount + 1)) + num3]);
        //                    primitive2.IndexArray.Append(list2[(((num2 + 1) * (segCount + 1)) + num3) + 1]);
        //                    primitive2.IndexArray.Append(list2[((num2 * (segCount + 1)) + num3) + 1]);
        //                }
        //                else
        //                {
        //                    primitive.IndexArray.Append(list[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list2[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list2[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
        //                }
        //                if (num2 == (array.Length - 2))
        //                {
        //                    primitive3.IndexArray.Append(list3[num7 + (num3 * 2)]);
        //                    primitive3.IndexArray.Append(list3[(num7 + ((num3 + 1) * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list3[(num7 + (num3 * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list3[num7 + (num3 * 2)]);
        //                    primitive3.IndexArray.Append(list3[num7 + ((num3 + 1) * 2)]);
        //                    primitive3.IndexArray.Append(list3[(num7 + ((num3 + 1) * 2)) + 1]);
        //                }
        //            }
        //        }
        //        primitive4.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        primitive4.VertexArray = primitive.VertexArray;
        //        primitive4.NormalArray = primitive.NormalArray;
        //        primitive4.TexcoordArray = primitive.TexcoordArray;
        //        primitive4.IndexArray = primitive.IndexArray;
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        SystemLog.Instance.Log(exception);
        //        return false;
        //    }
        //}
        #endregion

        #region 单壁绘制－－冯欣修改20131101
        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            Vector[] array = null;
            double[][] numArray = null;
            List<ushort> list = null;
            List<ushort> list2 = null;
            List<ushort> list3 = null;
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                int segCount;
                SecShape secShape;
                int num2;
                int num3;
                double[] numArray2;
                IDrawGroup group;
                if ((base._vtx.Length == 2) && (base._sections.Length == 2))
                {
                    if (base._sections[0].SecShape != base._sections[1].SecShape)
                    {
                        return false;
                    }
                    if (!DrawGeometry.Compare(base._vtx[0][0], base._vtx[1][0], true))
                    {
                        return false;
                    }
                    segCount = base._sections[0].SegCount;
                    secShape = base._sections[0].SecShape;
                    list = new List<ushort>();
                    list2 = new List<ushort>();
                    list3 = new List<ushort>();
                    List<Vector> vtxs = new List<Vector>();
                    if ((base._vtx[0][0].Z - base._vtx[1][0].Z) > base._sections[0].Diameter)
                    {
                        for (num2 = base._vtx[0].Length - 1; num2 >= 0; num2--)
                        {
                            vtxs.Add(base._vtx[0][num2]);
                        }
                        for (num2 = 0; num2 < base._vtx[0].Length; num2++)
                        {
                            vtxs.Add(base._vtx[1][num2]);
                        }
                        vtxs = Maths.DisperseArc(vtxs, Math.Min(base._sections[0].Diameter, base._sections[1].Diameter));
                    }
                    else
                    {
                        num2 = base._vtx[0].Length - 1;
                        while (num2 >= 0)
                        {
                            vtxs.Add(base._vtx[0][num2]);
                            num2--;
                        }
                        for (num2 = 1; num2 < base._vtx[0].Length; num2++)
                        {
                            vtxs.Add(base._vtx[1][num2]);
                        }
                        vtxs = Maths.DisperseArc(vtxs, Math.Min(base._sections[0].Diameter, base._sections[1].Diameter));
                    }
                    Vector vector7 = (base._vtx[0][base._vtx.Length - 1] - base._vtx[0][0]).UnitVector();
                    Vector vector8 = (base._vtx[1][base._vtx.Length - 1] - base._vtx[1][0]).UnitVector();
                    array = new Vector[vtxs.Count + 4];
                    array[0] = vtxs[0] + ((Vector)((vector7 * base._sections[0].Diameter) / 2.0));
                    array[1] = vtxs[0] + ((Vector)(vector7 * base._sections[0].Thick));
                    vtxs.CopyTo(array, 2);
                    array[array.Length - 2] = vtxs[vtxs.Count - 1] + ((Vector)(vector8 * base._sections[0].Thick));
                    array[array.Length - 1] = vtxs[vtxs.Count - 1] + ((Vector)((vector8 * base._sections[1].Diameter) / 2.0));
                    numArray2 = new double[array.Length];
                    for (num2 = 0; num2 < array.Length; num2++)
                    {
                        if (num2 == 0)
                        {
                            numArray2[num2] = 0.0;
                        }
                        else
                        {
                            numArray2[num2] = (array[num2] - array[num2 - 1]).Length + numArray2[num2 - 1];
                        }
                    }
                    double naN = double.NaN;
                    numArray = new double[array.Length][];
                    if (base._sections[0].Diameter != base._sections[1].Diameter)
                    {
                        double diameter = base._sections[0].Diameter;
                        double num6 = base._sections[1].Diameter - base._sections[0].Diameter;
                        for (num2 = 0; num2 < array.Length; num2++)
                        {
                            switch (num2)
                            {
                                case 0:
                                case 1:
                                    naN = (base._sections[0].Thick + base._sections[0].Diameter) / base._sections[0].Diameter;
                                    numArray[num2] = base._sections[0].GetVtxs(naN, 0.0, base._sections[0].Thick);
                                    break;

                                default:
                                    if ((num2 == (array.Length - 2)) || (num2 == (array.Length - 1)))
                                    {
                                        naN = (base._sections[1].Thick + base._sections[1].Diameter) / base._sections[1].Diameter;
                                        numArray[num2] = base._sections[1].GetVtxs(naN, 0.0, base._sections[1].Thick);
                                    }
                                    else
                                    {
                                        naN = (((numArray2[num2] - numArray2[2]) / (numArray2[numArray2.Length - 3] - numArray2[2])) * num6) / diameter;
                                        numArray[num2] = base._sections[0].GetVtxs(naN + 1.0, 0.0, 0.0);
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        naN = (base._sections[0].Thick + base._sections[0].Diameter) / base._sections[0].Diameter;
                        for (num2 = 0; num2 < array.Length; num2++)
                        {
                            if (((num2 == 0) || (num2 == 1)) || ((num2 == (array.Length - 2)) || (num2 == (array.Length - 1))))
                            {
                                numArray[num2] = base._sections[0].GetVtxs(naN, 0.0, base._sections[0].Thick);
                            }
                            else
                            {
                                numArray[num2] = base._sections[0].GetVtxs();
                            }
                        }
                    }
                    object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                    int[] index = new int[1];
                    if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
                    {
                        int[] numArray4 = new int[1];
                        if (base.NewEmptyModel(numArray4, base._renderType, renderInfo, out smodel))
                        {
                            goto Label_05D6;
                        }
                    }
                }
                return false;
            Label_05D6:
                group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);
                IDrawPrimitive primitive4 = smodel.GetGroup(0).GetPrimitive(0);
                for (num2 = 0; num2 < array.Length; num2++)
                {
                    Vector vector;
                    Vector vector2;
                    Vector vector3;
                    if (num2 == (array.Length - 1))
                    {
                        vector = (array[num2] - array[num2 - 1]).UnitVector();
                    }
                    else
                    {
                        vector = (array[num2 + 1] - array[num2]).UnitVector();
                    }
                    Maths.GenerateComplementBasis(vector, out vector2, out vector3);
                    num3 = 0;
                    while (num3 <= segCount)
                    {
                        Vector vector6 = (((numArray[num2][num3 * 4] - base._sections[0].OffsetX) * vector2) + ((numArray[num2][(num3 * 4) + 1] - base._sections[0].OffsetY) * vector3)).UnitVector();
                        Vector vector4 = array[num2] + ((Vector)((numArray[num2][num3 * 4] * vector2) + (numArray[num2][(num3 * 4) + 1] * vector3)));
                        Vector vector5 = array[num2] + ((Vector)((numArray[num2][(num3 * 4) + 2] * vector2) + (numArray[num2][(num3 * 4) + 3] * vector3)));
                        if (secShape == SecShape.CircleRing)
                        {
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray2[num2]);
                                primitive.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
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
                                primitive.TexcoordArray.Append((float)numArray2[num2]);
                                primitive.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
                                primitive.TexcoordArray.Append((float)numArray2[num2]);
                                primitive.TexcoordArray.Append((num3 * 8f) / ((float)segCount));
                            }
                            primitive.NormalArray.Append((float)vector3.X);
                            primitive.NormalArray.Append((float)vector3.Y);
                            primitive.NormalArray.Append((float)vector3.Z);
                            primitive.NormalArray.Append((float)-vector2.X);
                            primitive.NormalArray.Append((float)-vector2.Y);
                            primitive.NormalArray.Append((float)-vector2.Z);
                        }
                        num3++;
                    }
                }
                primitive.Material.CullMode = gviCullFaceMode.gviCullNone;
                int num7 = (segCount + 1) * 2;
                for (num2 = 0; num2 < (array.Length - 1); num2++)
                {
                    for (num3 = 0; num3 < segCount; num3++)
                    {
                        if (secShape == SecShape.CircleRing)
                        {
                            primitive.IndexArray.Append(list[(num2 * (segCount + 1)) + num3]);
                            primitive.IndexArray.Append(list[(((num2 + 1) * (segCount + 1)) + num3) + 1]);
                            primitive.IndexArray.Append(list[((num2 + 1) * (segCount + 1)) + num3]);
                            primitive.IndexArray.Append(list[(num2 * (segCount + 1)) + num3]);
                            primitive.IndexArray.Append(list[((num2 * (segCount + 1)) + num3) + 1]);
                            primitive.IndexArray.Append(list[(((num2 + 1) * (segCount + 1)) + num3) + 1]);
                        }
                        else
                        {
                            primitive.IndexArray.Append(list[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
                            primitive.IndexArray.Append(list[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
                            primitive.IndexArray.Append(list[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
                            primitive.IndexArray.Append(list[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 1]);
                            primitive.IndexArray.Append(list[(((num2 * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
                            primitive.IndexArray.Append(list[((((num2 + 1) * (segCount + 1)) * 2) + (num3 * 2)) + 2]);
                        }
                    }
                }
                primitive4.Material.CullMode = gviCullFaceMode.gviCullNone;
                primitive4.VertexArray = primitive.VertexArray;
                primitive4.NormalArray = primitive.NormalArray;
                primitive4.TexcoordArray = primitive.TexcoordArray;
                primitive4.IndexArray = primitive.IndexArray;
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        #endregion
    }
}
