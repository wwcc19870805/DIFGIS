using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawPipeLine : DrawScanModel, IDrawPipeLine, IDrawScanModel, IDrawGeometry
    {
        // Fields
        protected IPipeSection _pipeSection;
        protected List<Vector> _route;
        protected TurnerStyle _turnerStlye;
        protected LocationType _pipeLocateType;

        // Methods
        public DrawPipeLine()
        {
            base._modeltype = ModelType.PipeLine;
        }

        #region 绘制三维管线
        /// <summary>
        /// 原始代码
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="fmodel"></param>
        /// <param name="smodel"></param>
        /// <returns></returns>
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    Vector w = null;
        //    Vector u = null;
        //    Vector v = null;
        //    List<Vector> list = null;
        //    List<ushort> list5 = null;
        //    double[] numArray = null;                 // 管线顶点向量间长度
        //    double[] vtxs = null;                     // 精模断面顶点信息
        //    double[] simpleVtxs = null;               // 简模断面顶点信息
        //    List<int> lstBpIndex = new List<int>();
        //    base.Draw(out mp, out fmodel, out smodel);
        //    DrawGeometry.Clear();
        //    try
        //    {
        //        int num;
        //        int num2;
        //        List<ushort> list2;
        //        List<ushort> list3;
        //        List<ushort> list4;
        //        int num3;
        //        if ((this._route != null) && (this._route.Count != 0))
        //        {
        //            if (this._turnerStlye == TurnerStyle.Capround)
        //            {
        //                //list = Maths.DisperseLineFX(this._route, this._pipeSection.Diameter, ref lstBpIndex);
        //                list = this._route;
        //            }
        //            else
        //            {
        //                list = this._route;
        //            }
        //            vtxs = this._pipeSection.GetVtxs();
        //            if (this._pipeSection.SecShape == SecShape.CircleRing)
        //            {
        //                list5 = new List<ushort>();
        //                simpleVtxs = this._pipeSection.GetSimpleVtxs();
        //            }
        //            list2 = new List<ushort>();
        //            list3 = new List<ushort>();
        //            list4 = new List<ushort>();
        //            numArray = new double[list.Count];
        //            for (num = 0; num < list.Count; num++)
        //            {
        //                if (num == 0)
        //                {
        //                    numArray[num] = 0.0;
        //                }
        //                else
        //                {
        //                    numArray[num] += (list[num] - list[num - 1]).Length;
        //                }
        //            }
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            int[] index = new int[3];
        //            index[1] = 1;
        //            index[2] = 2;
        //            // 创建精模--1模型/1绘制组/3绘制单元
        //            if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
        //            {
        //                // 创建简模--1模型/1绘制组/1绘制单元
        //                int[] numArray5 = new int[1];
        //                if (base.NewEmptyModel(numArray5, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_017C;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_017C:
        //        num3 = 0;
        //        int num4 = 0;
        //        IDrawGroup group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);                // 精模绘制单元1--外壁
        //        IDrawPrimitive primitive2 = group.GetPrimitive(1);               // 精模绘制单元2--内壁
        //        IDrawPrimitive primitive3 = group.GetPrimitive(2);               // 精模绘制单元3--横截面
        //        IDrawPrimitive primitive4 = smodel.GetGroup(0).GetPrimitive(0);  // 简模绘制单元--单壁
        //        for (num = 0; num < list.Count; num++)
        //        {
        //            Vector vector4;
        //            Vector vector6;
        //            if (lstBpIndex.Count == 0)
        //            {
        //                if (num == 0)
        //                {
        //                    w = list[num + 1] - list[num];
        //                }
        //                else if (num == (list.Count - 1))
        //                {
        //                    w = list[num] - list[num - 1];
        //                }
        //                else
        //                {
        //                    w = list[num + 1] - list[num];
        //                }
        //            }
        //            else
        //            {
        //                if ((num == 0) || lstBpIndex.Contains(num))
        //                {
        //                    int num6;
        //                    int num5 = num;
        //                    if (num == 0)
        //                    {
        //                        num6 = lstBpIndex[num4];
        //                    }
        //                    else if (num4 < (lstBpIndex.Count - 1))
        //                    {
        //                        num6 = lstBpIndex[++num4];
        //                    }
        //                    else
        //                    {
        //                        num6 = list.Count - 1;
        //                    }
        //                    if ((num6 - num5) < 2)
        //                    {
        //                        num3 = 0;
        //                    }
        //                    else
        //                    {
        //                        Vector vector7 = (list[num5 + 1] - list[num5]).UnitVector();
        //                        Vector vector8 = (list[num6 - 1] - list[num6 - 2]).UnitVector();
        //                        if (((Math.Abs((double)(vector7.X - vector8.X)) < 0.08) && (Math.Abs((double)(vector7.Y - vector8.Y)) < 0.08)) && (Math.Abs((double)(vector7.Z - vector8.Z)) < 0.08))
        //                        {
        //                            num3 = 0;
        //                        }
        //                        else
        //                        {
        //                            Maths.GenerateComplementBasis((list[num6 - 1] - list[num5]).UnitVector(), out u, out v);
        //                            if (Math.Abs(((vector7 * vector8)).UnitVector().Z) < 0.8)
        //                            {
        //                                num3 = 1;
        //                            }
        //                            else
        //                            {
        //                                num3 = 2;
        //                            }
        //                        }
        //                    }
        //                }
        //                if (num == 0)
        //                {
        //                    w = list[num + 1] - list[num];
        //                }
        //                else if (lstBpIndex.Contains(num + 1))
        //                {
        //                    w = list[num + 2] - list[num + 1];
        //                }
        //                else if (lstBpIndex.Contains(num))
        //                {
        //                    w = list[num + 1] - list[num];
        //                }
        //                else if (num == (list.Count - 1))
        //                {
        //                    w = list[num] - list[num - 1];
        //                }
        //                else
        //                {
        //                    w = list[num + 1] - list[num];
        //                }
        //            }
        //            w = w.UnitVector();
        //            switch (num3)
        //            {
        //                case 0:
        //                    Maths.GenerateComplementBasis(w, out u, out v);
        //                    break;

        //                case 1:
        //                    v = (u * w).UnitVector();
        //                    break;

        //                case 2:
        //                    u = (w * v).UnitVector();
        //                    break;
        //            }
        //            num2 = 0;
        //            while (num2 <= this._pipeSection.SegCount)
        //            {
        //                vector6 = (((vtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((vtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                vector4 = list[num] + ((Vector)((vtxs[num2 * 4] * u) + (vtxs[(num2 * 4) + 1] * v)));
        //                Vector vector5 = list[num] + ((Vector)((vtxs[(num2 * 4) + 2] * u) + (vtxs[(num2 * 4) + 3] * v)));
        //                if ((num == 0) || (num == (list.Count - 1)))
        //                {
        //                    primitive3.VertexArray.Append((float)vector4.X);
        //                    primitive3.VertexArray.Append((float)vector4.Y);
        //                    primitive3.VertexArray.Append((float)vector4.Z);
        //                    list4.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
        //                    primitive3.VertexArray.Append((float)vector5.X);
        //                    primitive3.VertexArray.Append((float)vector5.Y);
        //                    primitive3.VertexArray.Append((float)vector5.Z);
        //                    list4.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
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
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
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
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
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
        //            if ((this._pipeSection.SecShape == SecShape.CircleRing) && (simpleVtxs != null))
        //            {
        //                num2 = 0;
        //                while (num2 <= DrawGeometry.SimpleSegments)
        //                {
        //                    vector6 = (((simpleVtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((simpleVtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                    vector4 = list[num] + ((Vector)((simpleVtxs[num2 * 4] * u) + (simpleVtxs[(num2 * 4) + 1] * v)));
        //                    primitive4.VertexArray.Append((float)vector4.X);
        //                    primitive4.VertexArray.Append((float)vector4.Y);
        //                    primitive4.VertexArray.Append((float)vector4.Z);
        //                    list5.Add((ushort)((primitive4.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive4.TexcoordArray.Append((float)numArray[num]);
        //                        primitive4.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive4.NormalArray.Append((float)vector6.X);
        //                    primitive4.NormalArray.Append((float)vector6.Y);
        //                    primitive4.NormalArray.Append((float)vector6.Z);
        //                    num2++;
        //                }
        //            }
        //        }
        //        int num7 = (this._pipeSection.SegCount + 1) * 2;
        //        for (num = 0; num < (list.Count - 1); num++)
        //        {
        //            if (!lstBpIndex.Contains(num + 1))
        //            {
        //                num2 = 0;
        //                while (num2 < this._pipeSection.SegCount)
        //                {
        //                    if (num == 0)
        //                    {
        //                        primitive3.IndexArray.Append(list4[num2 * 2]);
        //                        primitive3.IndexArray.Append(list4[(num2 * 2) + 1]);
        //                        primitive3.IndexArray.Append(list4[((num2 + 1) * 2) + 1]);
        //                        primitive3.IndexArray.Append(list4[num2 * 2]);
        //                        primitive3.IndexArray.Append(list4[((num2 + 1) * 2) + 1]);
        //                        primitive3.IndexArray.Append(list4[(num2 + 1) * 2]);
        //                    }
        //                    if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                    {
        //                        primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                        primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                        primitive.IndexArray.Append(list2[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
        //                        primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                        primitive.IndexArray.Append(list2[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                        primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                        primitive2.IndexArray.Append(list3[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                        primitive2.IndexArray.Append(list3[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
        //                        primitive2.IndexArray.Append(list3[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                        primitive2.IndexArray.Append(list3[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                        primitive2.IndexArray.Append(list3[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                        primitive2.IndexArray.Append(list3[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    }
        //                    else
        //                    {
        //                        primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                        primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                        primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                        primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                        primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                        primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                        primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                        primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                        primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                        primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                        primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                        primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    }
        //                    if (num == (list.Count - 2))
        //                    {
        //                        primitive3.IndexArray.Append(list4[num7 + (num2 * 2)]);
        //                        primitive3.IndexArray.Append(list4[(num7 + ((num2 + 1) * 2)) + 1]);
        //                        primitive3.IndexArray.Append(list4[(num7 + (num2 * 2)) + 1]);
        //                        primitive3.IndexArray.Append(list4[num7 + (num2 * 2)]);
        //                        primitive3.IndexArray.Append(list4[num7 + ((num2 + 1) * 2)]);
        //                        primitive3.IndexArray.Append(list4[(num7 + ((num2 + 1) * 2)) + 1]);
        //                    }
        //                    num2++;
        //                }
        //            }
        //        }
        //        primitive4.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        if (this._pipeSection.SecShape == SecShape.CircleRing)
        //        {
        //            for (num = 0; num < (list.Count - 1); num++)
        //            {
        //                for (num2 = 0; num2 < DrawGeometry.SimpleSegments; num2++)
        //                {
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[((num * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            primitive4.VertexArray = primitive.VertexArray;
        //            primitive4.NormalArray = primitive.NormalArray;
        //            primitive4.TexcoordArray = primitive.TexcoordArray;
        //            primitive4.IndexArray = primitive.IndexArray;
        //        }
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        SystemLog.Instance.Log(exception.StackTrace);
        //        return false;
        //    }
        //}
        /// <summary>
        /// 旋转矢量法--有问题 FX 2014.04.01
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="fmodel"></param>
        /// <param name="smodel"></param>
        /// <returns></returns>
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    Vector w = null;
        //    Vector u = null;
        //    Vector v = null;
        //    List<Vector> list = null;
        //    List<ushort> list5 = null;
        //    double[] numArray = null;                 // 管线顶点向量间长度
        //    double[] vtxs = null;                     // 精模断面顶点信息
        //    double[] simpleVtxs = null;               // 简模断面顶点信息
        //    List<Vector> lstNV = new List<Vector>();
        //    base.Draw(out mp, out fmodel, out smodel);
        //    DrawGeometry.Clear();
        //    try
        //    {
        //        int num;
        //        int num2;
        //        List<ushort> list2;
        //        List<ushort> list3;
        //        List<ushort> list4;
        //        int num3;
        //        if ((this._route != null) && (this._route.Count != 0))
        //        {
        //            if (this._turnerStlye == TurnerStyle.Capround)
        //            {
        //                //list = Maths.DisperseLineFX2(this._route, this._pipeSection.Diameter, ref lstNV);
        //                list = this._route;
        //            }
        //            else
        //            {
        //                list = this._route;
        //            }
        //            vtxs = this._pipeSection.GetVtxs();
        //            if (this._pipeSection.SecShape == SecShape.CircleRing)
        //            {
        //                list5 = new List<ushort>();
        //                simpleVtxs = this._pipeSection.GetSimpleVtxs();
        //            }
        //            list2 = new List<ushort>();
        //            list3 = new List<ushort>();
        //            list4 = new List<ushort>();
        //            numArray = new double[list.Count];
        //            for (num = 0; num < list.Count; num++)
        //            {
        //                if (num == 0)
        //                {
        //                    numArray[num] = 0.0;
        //                }
        //                else
        //                {
        //                    numArray[num] += (list[num] - list[num - 1]).Length;
        //                }
        //            }
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            int[] index = new int[3];
        //            index[1] = 1;
        //            index[2] = 2;
        //            // 创建精模--1模型/1绘制组/3绘制单元
        //            if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
        //            {
        //                // 创建简模--1模型/1绘制组/1绘制单元
        //                int[] numArray5 = new int[1];
        //                if (base.NewEmptyModel(numArray5, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_017C;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_017C:
        //        num3 = 0;
        //        int num4 = 0;
        //        IDrawGroup group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);                // 精模绘制单元1--外壁
        //        IDrawPrimitive primitive2 = group.GetPrimitive(1);               // 精模绘制单元2--内壁
        //        IDrawPrimitive primitive3 = group.GetPrimitive(2);               // 精模绘制单元3--横截面
        //        IDrawPrimitive primitive4 = smodel.GetGroup(0).GetPrimitive(0);  // 简模绘制单元--单壁
        //        Vector vector4;
        //        Vector vector6;
        //        Vector vector5;
        //        double r1 = 0.0;
        //        for (num = 0; num < list.Count; num++)
        //        {
        //            // 计算局部坐标系
        //            if (num == 0)
        //            {
        //                w = lstNV[num];
        //                Maths.GenerateComplementBasis(w, out u, out v);
        //                vector4 = list[num] + ((Vector)((vtxs[0 * 4] * u) + (vtxs[(0 * 4) + 1] * v)));
        //                u = (vector4 - list[num]).UnitVector();
        //                v = (u * w).UnitVector();
        //                r1 = (vector4 - list[num]).Mould();
        //            }
        //            else
        //            {
        //                w = (list[num] - list[num - 1]).UnitVector();
        //                double d = 0.0;
        //                double angle = ((Vector.CalcAngle(w, u)) * 180) / 3.1415926535897931;
        //                vector4 = list[num] + r1 * u;
        //                if (angle >= 90)
        //                {
        //                    d = r1 * Math.Cos(((180 - angle) / 180) * 3.1415926535897931);
        //                    vector5 = vector4 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d * 2);
        //                }
        //                else
        //                {
        //                    d = r1 * Math.Cos((angle / 180) * 3.1415926535897931);
        //                    vector5 = vector4 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d * 2);
        //                }
        //                u = (vector5 - list[num]).UnitVector();
        //                v = (u * lstNV[num]).UnitVector();
        //            }
        //            num2 = 0;
        //            while (num2 <= this._pipeSection.SegCount)
        //            {
        //                vector6 = (((vtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((vtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                vector4 = list[num] + ((Vector)((vtxs[num2 * 4] * u) + (vtxs[(num2 * 4) + 1] * v)));
        //                vector5 = list[num] + ((Vector)((vtxs[(num2 * 4) + 2] * u) + (vtxs[(num2 * 4) + 3] * v)));
        //                if ((num == 0) || (num == (list.Count - 1)))
        //                {
        //                    primitive3.VertexArray.Append((float)vector4.X);
        //                    primitive3.VertexArray.Append((float)vector4.Y);
        //                    primitive3.VertexArray.Append((float)vector4.Z);
        //                    list4.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
        //                    primitive3.VertexArray.Append((float)vector5.X);
        //                    primitive3.VertexArray.Append((float)vector5.Y);
        //                    primitive3.VertexArray.Append((float)vector5.Z);
        //                    list4.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
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
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
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
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
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
        //            if ((this._pipeSection.SecShape == SecShape.CircleRing) && (simpleVtxs != null))
        //            {
        //                num2 = 0;
        //                while (num2 <= DrawGeometry.SimpleSegments)
        //                {
        //                    vector6 = (((simpleVtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((simpleVtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                    vector4 = list[num] + ((Vector)((simpleVtxs[num2 * 4] * u) + (simpleVtxs[(num2 * 4) + 1] * v)));
        //                    primitive4.VertexArray.Append((float)vector4.X);
        //                    primitive4.VertexArray.Append((float)vector4.Y);
        //                    primitive4.VertexArray.Append((float)vector4.Z);
        //                    list5.Add((ushort)((primitive4.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive4.TexcoordArray.Append((float)numArray[num]);
        //                        primitive4.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive4.NormalArray.Append((float)vector6.X);
        //                    primitive4.NormalArray.Append((float)vector6.Y);
        //                    primitive4.NormalArray.Append((float)vector6.Z);
        //                    num2++;
        //                }
        //            }
        //        }
        //        int num7 = (this._pipeSection.SegCount + 1) * 2;
        //        for (num = 0; num < (list.Count - 1); num++)
        //        {
        //            //if (!lstBpIndex.Contains(num + 1))
        //            //{
        //            num2 = 0;
        //            while (num2 < this._pipeSection.SegCount)
        //            {
        //                if (num == 0)
        //                {
        //                    primitive3.IndexArray.Append(list4[num2 * 2]);
        //                    primitive3.IndexArray.Append(list4[(num2 * 2) + 1]);
        //                    primitive3.IndexArray.Append(list4[((num2 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list4[num2 * 2]);
        //                    primitive3.IndexArray.Append(list4[((num2 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list4[(num2 + 1) * 2]);
        //                }
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list2[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list3[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list3[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list3[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list3[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list3[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list3[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                }
        //                else
        //                {
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                }
        //                if (num == (list.Count - 2))
        //                {
        //                    primitive3.IndexArray.Append(list4[num7 + (num2 * 2)]);
        //                    primitive3.IndexArray.Append(list4[(num7 + ((num2 + 1) * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list4[(num7 + (num2 * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list4[num7 + (num2 * 2)]);
        //                    primitive3.IndexArray.Append(list4[num7 + ((num2 + 1) * 2)]);
        //                    primitive3.IndexArray.Append(list4[(num7 + ((num2 + 1) * 2)) + 1]);
        //                }
        //                num2++;
        //            }
        //            //}
        //        }
        //        primitive4.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        if (this._pipeSection.SecShape == SecShape.CircleRing)
        //        {
        //            for (num = 0; num < (list.Count - 1); num++)
        //            {
        //                for (num2 = 0; num2 < DrawGeometry.SimpleSegments; num2++)
        //                {
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[((num * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            primitive4.VertexArray = primitive.VertexArray;
        //            primitive4.NormalArray = primitive.NormalArray;
        //            primitive4.TexcoordArray = primitive.TexcoordArray;
        //            primitive4.IndexArray = primitive.IndexArray;
        //        }
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        SystemLog.Instance.Log(exception.StackTrace);
        //        return false;
        //    }
        //}
        /// <summary>
        /// 绘制内外壁 FX 2014.04.01
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="fmodel"></param>
        /// <param name="smodel"></param>
        /// <returns></returns>
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    Vector w = null;
        //    Vector u = null;
        //    Vector v = null;
        //    List<Vector> list = null;
        //    List<ushort> list5 = null;
        //    double[] numArray = null;                 // 管线顶点向量间长度
        //    double[] vtxs = null;                     // 精模断面顶点信息
        //    double[] simpleVtxs = null;               // 简模断面顶点信息
        //    List<int> lstBpIndex = new List<int>();
        //    base.Draw(out mp, out fmodel, out smodel);
        //    DrawGeometry.Clear();
        //    try
        //    {
        //        int num;
        //        int num2;
        //        List<ushort> list2;
        //        List<ushort> list3;
        //        List<ushort> list4;
        //        int num3;
        //        if ((this._route != null) && (this._route.Count != 0))
        //        {
        //            if (this._turnerStlye == TurnerStyle.Capround)
        //            {
        //                list = Maths.DisperseLineFX1(this._route, this._pipeSection.Diameter, ref lstBpIndex);
        //            }
        //            else
        //            {
        //                list = this._route;
        //            }
        //            vtxs = this._pipeSection.GetVtxs();
        //            if (this._pipeSection.SecShape == SecShape.CircleRing)
        //            {
        //                list5 = new List<ushort>();
        //                simpleVtxs = this._pipeSection.GetSimpleVtxs();
        //            }
        //            list2 = new List<ushort>();
        //            list3 = new List<ushort>();
        //            list4 = new List<ushort>();
        //            numArray = new double[list.Count];
        //            for (num = 0; num < list.Count; num++)
        //            {
        //                if (num == 0)
        //                {
        //                    numArray[num] = 0.0;
        //                }
        //                else
        //                {
        //                    numArray[num] += (list[num] - list[num - 1]).Length;
        //                }
        //            }
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            int[] index = new int[3];
        //            index[1] = 1;
        //            index[2] = 2;
        //            // 创建精模--1模型/1绘制组/3绘制单元
        //            if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
        //            {
        //                // 创建简模--1模型/1绘制组/1绘制单元
        //                int[] numArray5 = new int[1];
        //                if (base.NewEmptyModel(numArray5, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_017C;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_017C:
        //        num3 = 0;
        //        int num4 = 0;
        //        IDrawGroup group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);                // 精模绘制单元1--外壁
        //        IDrawPrimitive primitive2 = group.GetPrimitive(1);               // 精模绘制单元2--内壁
        //        IDrawPrimitive primitive3 = group.GetPrimitive(2);               // 精模绘制单元3--横截面
        //        IDrawPrimitive primitive4 = smodel.GetGroup(0).GetPrimitive(0);  // 简模绘制单元--单壁
        //        Vector vector4;
        //        Vector vector6;
        //        Vector vector5;
        //        Vector vector7;
        //        Vector vector8;
        //        Vector vector9;
        //        List<Vector> RefList1 = new List<Vector>();
        //        List<Vector> RefList2 = new List<Vector>();
        //        List<Vector> RefList3 = new List<Vector>();
        //        List<Vector> RefList4 = new List<Vector>();
        //        List<Vector> RefList5 = new List<Vector>();
        //        for (num = 0; num < list.Count; num++)
        //        {
        //            if (num == 0)
        //            {
        //                w = list[num + 1] - list[num];
        //                w = w.UnitVector();
        //                Maths.GenerateComplementBasis(w, out u, out v);
        //            }
        //            num2 = 0;
        //            while (num2 <= this._pipeSection.SegCount)
        //            {
        //                if (num == 0)
        //                {
        //                    vector6 = (((vtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((vtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                    vector4 = list[num] + ((Vector)((vtxs[num2 * 4] * u) + (vtxs[(num2 * 4) + 1] * v)));
        //                    vector5 = list[num] + ((Vector)((vtxs[(num2 * 4) + 2] * u) + (vtxs[(num2 * 4) + 3] * v)));
        //                    RefList1.Add(vector6);
        //                    RefList2.Add(vector4);
        //                    RefList3.Add(vector5);
        //                }
        //                else
        //                {
        //                    vector7 = RefList1[num2];
        //                    vector8 = RefList2[num2];
        //                    vector9 = RefList3[num2];
        //                    w = list[num] - list[num - 1];
        //                    double jiaodu = ((Vector.CalcAngle(vector7, w)) * 180) / 3.1415926535897931;
        //                    if (jiaodu >= 90)
        //                    {
        //                        //double d1 = (this._pipeSection.Width / 2.0) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                        //double d2 = ((this._pipeSection.Width / 2.0) - this._pipeSection.Thick) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                        double d1 = ((vector8 - list[num - 1]).Length) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                        double d2 = ((vector9 - list[num - 1]).Length) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                        vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d1 * 2);
        //                        vector5 = vector9 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d2 * 2);
        //                        vector6 = (vector4 - list[num]).UnitVector();
        //                        RefList1[num2] = vector6;
        //                        RefList2[num2] = vector4;
        //                        RefList3[num2] = vector5;
        //                    }
        //                    else
        //                    {
        //                        //double d1 = (this._pipeSection.Width / 2.0) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                        //double d2 = ((this._pipeSection.Width / 2.0) - this._pipeSection.Thick) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                        double d1 = ((vector8 - list[num - 1]).Length) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                        double d2 = ((vector9 - list[num - 1]).Length) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                        vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d1 * 2);
        //                        vector5 = vector9 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d2 * 2);
        //                        vector6 = (vector4 - list[num]).UnitVector();
        //                        RefList1[num2] = vector6;
        //                        RefList2[num2] = vector4;
        //                        RefList3[num2] = vector5;
        //                    }
        //                }
        //                if ((num == 0) || (num == (list.Count - 1)))
        //                {
        //                    primitive3.VertexArray.Append((float)vector4.X);
        //                    primitive3.VertexArray.Append((float)vector4.Y);
        //                    primitive3.VertexArray.Append((float)vector4.Z);
        //                    list4.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
        //                    primitive3.VertexArray.Append((float)vector5.X);
        //                    primitive3.VertexArray.Append((float)vector5.Y);
        //                    primitive3.VertexArray.Append((float)vector5.Z);
        //                    list4.Add((ushort)((primitive3.VertexArray.Length / 3) - 1));
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
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
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
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    primitive2.VertexArray.Append((float)vector5.X);
        //                    primitive2.VertexArray.Append((float)vector5.Y);
        //                    primitive2.VertexArray.Append((float)vector5.Z);
        //                    list3.Add((ushort)((primitive2.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive2.TexcoordArray.Append((float)numArray[num]);
        //                        primitive2.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
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
        //            if ((this._pipeSection.SecShape == SecShape.CircleRing) && (simpleVtxs != null))
        //            {
        //                num2 = 0;
        //                while (num2 <= DrawGeometry.SimpleSegments)
        //                {
        //                    if (num == 0)
        //                    {
        //                        vector6 = (((simpleVtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((simpleVtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                        vector4 = list[num] + ((Vector)((simpleVtxs[num2 * 4] * u) + (simpleVtxs[(num2 * 4) + 1] * v)));
        //                        RefList4.Add(vector6);
        //                        RefList5.Add(vector4);
        //                    }
        //                    else
        //                    {
        //                        vector7 = RefList4[num2];
        //                        vector8 = RefList5[num2];
        //                        w = list[num] - list[num - 1];
        //                        double jiaodu = ((Vector.CalcAngle(vector7, w)) * 180) / 3.1415926535897931;
        //                        if (jiaodu >= 90)
        //                        {
        //                            double d1 = ((vector8 - list[num - 1]).Length) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                            vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d1 * 2);
        //                            vector6 = (vector4 - list[num]).UnitVector();
        //                            RefList4[num2] = vector6;
        //                            RefList5[num2] = vector4;
        //                        }
        //                        else
        //                        {
        //                            double d1 = ((vector8 - list[num - 1]).Length) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                            vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d1 * 2);
        //                            vector6 = (vector4 - list[num]).UnitVector();
        //                            RefList4[num2] = vector6;
        //                            RefList5[num2] = vector4;
        //                        }
        //                    }
        //                    primitive4.VertexArray.Append((float)vector4.X);
        //                    primitive4.VertexArray.Append((float)vector4.Y);
        //                    primitive4.VertexArray.Append((float)vector4.Z);
        //                    list5.Add((ushort)((primitive4.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive4.TexcoordArray.Append((float)numArray[num]);
        //                        primitive4.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive4.NormalArray.Append((float)vector6.X);
        //                    primitive4.NormalArray.Append((float)vector6.Y);
        //                    primitive4.NormalArray.Append((float)vector6.Z);
        //                    num2++;
        //                }
        //            }
        //        }
        //        int num7 = (this._pipeSection.SegCount + 1) * 2;
        //        for (num = 0; num < (list.Count - 1); num++)
        //        {
        //            num2 = 0;
        //            while (num2 < this._pipeSection.SegCount)
        //            {
        //                if (num == 0)
        //                {
        //                    primitive3.IndexArray.Append(list4[num2 * 2]);
        //                    primitive3.IndexArray.Append(list4[(num2 * 2) + 1]);
        //                    primitive3.IndexArray.Append(list4[((num2 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list4[num2 * 2]);
        //                    primitive3.IndexArray.Append(list4[((num2 + 1) * 2) + 1]);
        //                    primitive3.IndexArray.Append(list4[(num2 + 1) * 2]);
        //                }
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list2[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list3[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list3[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list3[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list3[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive2.IndexArray.Append(list3[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive2.IndexArray.Append(list3[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                }
        //                else
        //                {
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive2.IndexArray.Append(list3[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive2.IndexArray.Append(list3[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                }
        //                if (num == (list.Count - 2))
        //                {
        //                    primitive3.IndexArray.Append(list4[num7 + (num2 * 2)]);
        //                    primitive3.IndexArray.Append(list4[(num7 + ((num2 + 1) * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list4[(num7 + (num2 * 2)) + 1]);
        //                    primitive3.IndexArray.Append(list4[num7 + (num2 * 2)]);
        //                    primitive3.IndexArray.Append(list4[num7 + ((num2 + 1) * 2)]);
        //                    primitive3.IndexArray.Append(list4[(num7 + ((num2 + 1) * 2)) + 1]);
        //                }
        //                num2++;
        //            }
        //        }
        //        primitive4.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        if (this._pipeSection.SecShape == SecShape.CircleRing)
        //        {
        //            for (num = 0; num < (list.Count - 1); num++)
        //            {
        //                for (num2 = 0; num2 < DrawGeometry.SimpleSegments; num2++)
        //                {
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[((num * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            primitive4.VertexArray = primitive.VertexArray;
        //            primitive4.NormalArray = primitive.NormalArray;
        //            primitive4.TexcoordArray = primitive.TexcoordArray;
        //            primitive4.IndexArray = primitive.IndexArray;
        //        }
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        SystemLog.Instance.Log(exception.StackTrace);
        //        return false;
        //    }
        //}
        /// <summary>
        /// 仅绘制外壁 FX 2014.04.01
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="fmodel"></param>
        /// <param name="smodel"></param>
        /// <returns></returns>
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    Vector w = null;
        //    Vector u = null;
        //    Vector v = null;
        //    List<Vector> list = null;
        //    List<ushort> list5 = null;
        //    double[] numArray = null;                 // 管线顶点向量间长度
        //    double[] vtxs = null;                     // 精模断面顶点信息
        //    double[] simpleVtxs = null;               // 简模断面顶点信息
        //    List<int> lstBpIndex = new List<int>();
        //    base.Draw(out mp, out fmodel, out smodel);
        //    DrawGeometry.Clear();
        //    try
        //    {
        //        int num;
        //        int num2;
        //        List<ushort> list2;
        //        List<ushort> list3;
        //        List<ushort> list4;
        //        int num3;
        //        if ((this._route != null) && (this._route.Count != 0))
        //        {
        //            if (this._turnerStlye == TurnerStyle.Capround)
        //            {
        //                list = Maths.DisperseLineFX1(this._route, this._pipeSection.Diameter, ref lstBpIndex);
        //            }
        //            else
        //            {
        //                list = this._route;
        //            }
        //            vtxs = this._pipeSection.GetVtxs();
        //            if (this._pipeSection.SecShape == SecShape.CircleRing)
        //            {
        //                list5 = new List<ushort>();
        //                simpleVtxs = this._pipeSection.GetSimpleVtxs();
        //            }
        //            list2 = new List<ushort>();
        //            list3 = new List<ushort>();
        //            list4 = new List<ushort>();
        //            numArray = new double[list.Count];
        //            for (num = 0; num < list.Count; num++)
        //            {
        //                if (num == 0)
        //                {
        //                    numArray[num] = 0.0;
        //                }
        //                else
        //                {
        //                    numArray[num] += (list[num] - list[num - 1]).Length;
        //                }
        //            }
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            //int[] index = new int[3];
        //            //index[1] = 1;
        //            //index[2] = 2;
        //            int[] numArray5 = new int[1];
        //            // 创建精模--1模型/1绘制组/3绘制单元
        //            if (base.NewEmptyModel(numArray5, base._renderType, renderInfo, out fmodel))
        //            {
        //                // 创建简模--1模型/1绘制组/1绘制单元
        //                //int[] numArray5 = new int[1];
        //                if (base.NewEmptyModel(numArray5, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_017C;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_017C:
        //        num3 = 0;
        //        int num4 = 0;
        //        IDrawGroup group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);                // 精模绘制单元1--外壁
        //        IDrawPrimitive primitive4 = smodel.GetGroup(0).GetPrimitive(0);  // 简模绘制单元--单壁
        //        Vector vector4;
        //        Vector vector6;
        //        Vector vector5;
        //        Vector vector7;
        //        Vector vector8;
        //        Vector vector9;
        //        List<Vector> RefList1 = new List<Vector>();
        //        List<Vector> RefList2 = new List<Vector>();
        //        List<Vector> RefList3 = new List<Vector>();
        //        List<Vector> RefList4 = new List<Vector>();
        //        List<Vector> RefList5 = new List<Vector>();
        //        for (num = 0; num < list.Count; num++)
        //        {
        //            if (num == 0)
        //            {
        //                w = list[num + 1] - list[num];
        //                w = w.UnitVector();
        //                Maths.GenerateComplementBasis(w, out u, out v);
        //            }
        //            num2 = 0;
        //            while (num2 <= this._pipeSection.SegCount)
        //            {
        //                if (num == 0)
        //                {
        //                    vector6 = (((vtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((vtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                    vector4 = list[num] + ((Vector)((vtxs[num2 * 4] * u) + (vtxs[(num2 * 4) + 1] * v)));
        //                    vector5 = list[num] + ((Vector)((vtxs[(num2 * 4) + 2] * u) + (vtxs[(num2 * 4) + 3] * v)));
        //                    RefList1.Add(vector6);
        //                    RefList2.Add(vector4);
        //                    RefList3.Add(vector5);
        //                }
        //                else
        //                {
        //                    vector7 = RefList1[num2];
        //                    vector8 = RefList2[num2];
        //                    vector9 = RefList3[num2];
        //                    w = list[num] - list[num - 1];
        //                    double jiaodu = ((Vector.CalcAngle(vector7, w)) * 180) / 3.1415926535897931;
        //                    if (jiaodu >= 90)
        //                    {
        //                        double d1 = (this._pipeSection.Width / 2.0) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                        double d2 = ((this._pipeSection.Width / 2.0) - this._pipeSection.Thick) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                        vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d1 * 2);
        //                        vector5 = vector9 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d2 * 2);
        //                        vector6 = (vector4 - list[num]).UnitVector();
        //                        RefList1[num2] = vector6;
        //                        RefList2[num2] = vector4;
        //                        RefList3[num2] = vector5;
        //                    }
        //                    else
        //                    {
        //                        double d1 = (this._pipeSection.Width / 2.0) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                        double d2 = ((this._pipeSection.Width / 2.0) - this._pipeSection.Thick) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                        vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d1 * 2);
        //                        vector5 = vector9 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d2 * 2);
        //                        vector6 = (vector4 - list[num]).UnitVector();
        //                        RefList1[num2] = vector6;
        //                        RefList2[num2] = vector4;
        //                        RefList3[num2] = vector5;
        //                    }
        //                }
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive.NormalArray.Append((float)vector6.X);
        //                    primitive.NormalArray.Append((float)vector6.Y);
        //                    primitive.NormalArray.Append((float)vector6.Z);
        //                }
        //                else
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive.NormalArray.Append((float)v.X);
        //                    primitive.NormalArray.Append((float)v.Y);
        //                    primitive.NormalArray.Append((float)v.Z);
        //                    primitive.NormalArray.Append((float)-u.X);
        //                    primitive.NormalArray.Append((float)-u.Y);
        //                    primitive.NormalArray.Append((float)-u.Z);
        //                }
        //                num2++;
        //            }
        //            if ((this._pipeSection.SecShape == SecShape.CircleRing) && (simpleVtxs != null))
        //            {
        //                num2 = 0;
        //                while (num2 <= DrawGeometry.SimpleSegments)
        //                {
        //                    if (num == 0)
        //                    {
        //                        vector6 = (((simpleVtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((simpleVtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                        vector4 = list[num] + ((Vector)((simpleVtxs[num2 * 4] * u) + (simpleVtxs[(num2 * 4) + 1] * v)));
        //                        RefList4.Add(vector6);
        //                        RefList5.Add(vector4);
        //                    }
        //                    else
        //                    {
        //                        vector7 = RefList4[num2];
        //                        vector8 = RefList5[num2];
        //                        w = list[num] - list[num - 1];
        //                        double jiaodu = ((Vector.CalcAngle(vector7, w)) * 180) / 3.1415926535897931;
        //                        if (jiaodu >= 90)
        //                        {
        //                            double d1 = ((vector8 - list[num - 1]).Length) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                            vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d1 * 2);
        //                            vector6 = (vector4 - list[num]).UnitVector();
        //                            RefList4[num2] = vector6;
        //                            RefList5[num2] = vector4;
        //                        }
        //                        else
        //                        {
        //                            double d1 = ((vector8 - list[num - 1]).Length) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                            vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d1 * 2);
        //                            vector6 = (vector4 - list[num]).UnitVector();
        //                            RefList4[num2] = vector6;
        //                            RefList5[num2] = vector4;
        //                        }
        //                    }
        //                    primitive4.VertexArray.Append((float)vector4.X);
        //                    primitive4.VertexArray.Append((float)vector4.Y);
        //                    primitive4.VertexArray.Append((float)vector4.Z);
        //                    list5.Add((ushort)((primitive4.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive4.TexcoordArray.Append((float)numArray[num]);
        //                        primitive4.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive4.NormalArray.Append((float)vector6.X);
        //                    primitive4.NormalArray.Append((float)vector6.Y);
        //                    primitive4.NormalArray.Append((float)vector6.Z);
        //                    num2++;
        //                }
        //            }
        //        }
        //        primitive.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        int num7 = (this._pipeSection.SegCount + 1) * 2;
        //        for (num = 0; num < (list.Count - 1); num++)
        //        {
        //            num2 = 0;
        //            while (num2 < this._pipeSection.SegCount)
        //            {
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list2[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                }
        //                else
        //                {
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                }
        //                num2++;
        //            }
        //        }
        //        primitive4.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        if (this._pipeSection.SecShape == SecShape.CircleRing)
        //        {
        //            for (num = 0; num < (list.Count - 1); num++)
        //            {
        //                for (num2 = 0; num2 < DrawGeometry.SimpleSegments; num2++)
        //                {
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[((num * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            primitive4.VertexArray = primitive.VertexArray;
        //            primitive4.NormalArray = primitive.NormalArray;
        //            primitive4.TexcoordArray = primitive.TexcoordArray;
        //            primitive4.IndexArray = primitive.IndexArray;
        //        }
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        SystemLog.Instance.Log(exception.StackTrace);
        //        return false;
        //    }
        //}
        /// <summary>
        /// 绘制外壁和起始封口 FX 2014.04.01(直-圆-直)
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="fmodel"></param>
        /// <param name="smodel"></param>
        /// <returns></returns>
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    Vector w = null;
        //    Vector u = null;
        //    Vector v = null;
        //    List<Vector> list = null;
        //    List<ushort> list5 = null;
        //    double[] numArray = null;                 // 管线顶点向量间长度
        //    double[] vtxs = null;                     // 精模断面顶点信息
        //    double[] simpleVtxs = null;               // 简模断面顶点信息
        //    List<int> lstBpIndex = new List<int>();
        //    base.Draw(out mp, out fmodel, out smodel);
        //    DrawGeometry.Clear();
        //    try
        //    {
        //        int num;
        //        int num2;
        //        List<ushort> list2;
        //        List<ushort> list3;
        //        List<ushort> list4;
        //        int num3;
        //        if ((this._route != null) && (this._route.Count != 0))
        //        {
        //            if (this._turnerStlye == TurnerStyle.Capround)
        //            {
        //                list = Maths.DisperseLineFX1(this._route, this._pipeSection.Diameter, ref lstBpIndex);
        //            }
        //            else
        //            {
        //                //list = Maths.DisperseLineFX11(this._route, this._pipeSection.Diameter);
        //                list = this._route;
        //            }
        //            vtxs = this._pipeSection.GetVtxs();
        //            if (this._pipeSection.SecShape == SecShape.CircleRing)
        //            {
        //                list5 = new List<ushort>();
        //                simpleVtxs = this._pipeSection.GetSimpleVtxs();
        //            }
        //            list2 = new List<ushort>();
        //            list3 = new List<ushort>();
        //            list4 = new List<ushort>();
        //            numArray = new double[list.Count];
        //            for (num = 0; num < list.Count; num++)
        //            {
        //                if (num == 0)
        //                {
        //                    numArray[num] = 0.0;
        //                }
        //                else
        //                {
        //                    numArray[num] += (list[num] - list[num - 1]).Length;
        //                }
        //            }
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            int[] index = new int[2];
        //            index[1] = 1;
        //            // 调整封口处颜色 与管壁颜色一致
        //            uint[] numArray1 = renderInfo as uint[];
        //            if (numArray1.Length > 1)
        //            {
        //                for (int i = 1; i < numArray1.Length; i++)
        //                {
        //                    numArray1[i] = numArray1[0];
        //                }
        //            }
        //            // 创建精模--1模型/1绘制组/3绘制单元
        //            if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
        //            {
        //                // 创建简模--1模型/1绘制组/1绘制单元
        //                int[] numArray5 = new int[1];
        //                if (base.NewEmptyModel(numArray5, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_017C;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_017C:
        //        num3 = 0;
        //        int num4 = 0;
        //        IDrawGroup group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);                // 精模绘制单元1--外壁
        //        IDrawPrimitive primitive1 = group.GetPrimitive(1);               // 精模绘制单元2--起始处封口
        //        IDrawPrimitive primitive4 = smodel.GetGroup(0).GetPrimitive(0);  // 简模绘制单元--单壁
        //        Vector vector4;
        //        Vector vector6;
        //        Vector vector5;
        //        Vector vector7;
        //        Vector vector8;
        //        Vector vector9;
        //        List<Vector> RefList1 = new List<Vector>();
        //        List<Vector> RefList2 = new List<Vector>();
        //        List<Vector> RefList3 = new List<Vector>();
        //        List<Vector> RefList4 = new List<Vector>();
        //        List<Vector> RefList5 = new List<Vector>();
        //        for (num = 0; num < list.Count; num++)
        //        {
        //            if (num == 0)
        //            {
        //                w = list[num + 1] - list[num];
        //                w = w.UnitVector();
        //                Maths.GenerateComplementBasis(w, out u, out v);
        //            }
        //            num2 = 0;
        //            int IsAdd = -1;
        //            while (num2 <= this._pipeSection.SegCount)
        //            {
        //                if (num == 0)
        //                {
        //                    vector6 = (((vtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((vtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector(); // 管壁法向量
        //                    vector4 = list[num] + ((Vector)((vtxs[num2 * 4] * u) + (vtxs[(num2 * 4) + 1] * v)));                                                    // 外壁点坐标
        //                    vector5 = list[num] + ((Vector)((vtxs[(num2 * 4) + 2] * u) + (vtxs[(num2 * 4) + 3] * v)));                                              // 内壁点坐标
        //                    RefList1.Add(vector6);
        //                    RefList2.Add(vector4);
        //                    RefList3.Add(vector5);
        //                }
        //                else
        //                {
        //                    vector7 = RefList1[num2];
        //                    vector8 = RefList2[num2];
        //                    vector9 = RefList3[num2];
        //                    w = list[num] - list[num - 1];
        //                    double jiaodu = ((Vector.CalcAngle(vector7, w)) * 180) / 3.1415926535897931;
        //                    if (jiaodu >= 90)
        //                    {
        //                        double d1 = (this._pipeSection.Width / 2.0) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                        double d2 = ((this._pipeSection.Width / 2.0) - this._pipeSection.Thick) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                        vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d1 * 2);
        //                        vector5 = vector9 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d2 * 2);
        //                        vector6 = (vector4 - list[num]).UnitVector();
        //                        RefList1[num2] = vector6;
        //                        RefList2[num2] = vector4;
        //                        RefList3[num2] = vector5;
        //                    }
        //                    else
        //                    {
        //                        double d1 = (this._pipeSection.Width / 2.0) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                        double d2 = ((this._pipeSection.Width / 2.0) - this._pipeSection.Thick) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                        vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d1 * 2);
        //                        vector5 = vector9 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d2 * 2);
        //                        vector6 = (vector4 - list[num]).UnitVector();
        //                        RefList1[num2] = vector6;
        //                        RefList2[num2] = vector4;
        //                        RefList3[num2] = vector5;
        //                    }
        //                }
        //                if ((num == 0) || (num == (list.Count - 1)))
        //                {
        //                    if (IsAdd < 0)
        //                    {
        //                        // 管线封口
        //                        primitive1.VertexArray.Append((float)list[num].X);
        //                        primitive1.VertexArray.Append((float)list[num].Y);
        //                        primitive1.VertexArray.Append((float)list[num].Z);
        //                        list4.Add((ushort)((primitive1.VertexArray.Length / 3) - 1));
        //                        primitive1.NormalArray.Append(-((float)w.X));
        //                        primitive1.NormalArray.Append(-((float)w.Y));
        //                        primitive1.NormalArray.Append(-((float)w.Z));
        //                        IsAdd = 1;
        //                    }
        //                    primitive1.VertexArray.Append((float)vector4.X);
        //                    primitive1.VertexArray.Append((float)vector4.Y);
        //                    primitive1.VertexArray.Append((float)vector4.Z);
        //                    list4.Add((ushort)((primitive1.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive1.TexcoordArray.Append(num * 0.4f);
        //                        primitive1.TexcoordArray.Append(num2 * 0.4f);
        //                        primitive1.TexcoordArray.Append(num * 0.6f);
        //                        primitive1.TexcoordArray.Append(num2 * 0.6f);
        //                    }
        //                    primitive1.NormalArray.Append(-((float)w.X));
        //                    primitive1.NormalArray.Append(-((float)w.Y));
        //                    primitive1.NormalArray.Append(-((float)w.Z));
        //                    //primitive1.NormalArray.Append((float)w.X);
        //                    //primitive1.NormalArray.Append((float)w.Y);
        //                    //primitive1.NormalArray.Append((float)w.Z);
        //                }
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive.NormalArray.Append((float)vector6.X);
        //                    primitive.NormalArray.Append((float)vector6.Y);
        //                    primitive.NormalArray.Append((float)vector6.Z);
        //                }
        //                else
        //                {
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    primitive.VertexArray.Append((float)vector4.X);
        //                    primitive.VertexArray.Append((float)vector4.Y);
        //                    primitive.VertexArray.Append((float)vector4.Z);
        //                    list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                        primitive.TexcoordArray.Append((float)numArray[num]);
        //                        primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive.NormalArray.Append((float)v.X);
        //                    primitive.NormalArray.Append((float)v.Y);
        //                    primitive.NormalArray.Append((float)v.Z);
        //                    primitive.NormalArray.Append((float)-u.X);
        //                    primitive.NormalArray.Append((float)-u.Y);
        //                    primitive.NormalArray.Append((float)-u.Z);
        //                }
        //                num2++;
        //            }
        //            if ((this._pipeSection.SecShape == SecShape.CircleRing) && (simpleVtxs != null))
        //            {
        //                num2 = 0;
        //                while (num2 <= DrawGeometry.SimpleSegments)
        //                {
        //                    if (num == 0)
        //                    {
        //                        vector6 = (((simpleVtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((simpleVtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
        //                        vector4 = list[num] + ((Vector)((simpleVtxs[num2 * 4] * u) + (simpleVtxs[(num2 * 4) + 1] * v)));
        //                        RefList4.Add(vector6);
        //                        RefList5.Add(vector4);
        //                    }
        //                    else
        //                    {
        //                        vector7 = RefList4[num2];
        //                        vector8 = RefList5[num2];
        //                        w = list[num] - list[num - 1];
        //                        double jiaodu = ((Vector.CalcAngle(vector7, w)) * 180) / 3.1415926535897931;
        //                        if (jiaodu >= 90)
        //                        {
        //                            double d1 = ((vector8 - list[num - 1]).Length) * Math.Cos(((180 - jiaodu) / 180) * 3.1415926535897931);
        //                            vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() + d1 * 2);
        //                            vector6 = (vector4 - list[num]).UnitVector();
        //                            RefList4[num2] = vector6;
        //                            RefList5[num2] = vector4;
        //                        }
        //                        else
        //                        {
        //                            double d1 = ((vector8 - list[num - 1]).Length) * Math.Cos((jiaodu / 180) * 3.1415926535897931);
        //                            vector4 = vector8 + w.UnitVector() * ((list[num] - list[num - 1]).Mould() - d1 * 2);
        //                            vector6 = (vector4 - list[num]).UnitVector();
        //                            RefList4[num2] = vector6;
        //                            RefList5[num2] = vector4;
        //                        }
        //                    }
        //                    primitive4.VertexArray.Append((float)vector4.X);
        //                    primitive4.VertexArray.Append((float)vector4.Y);
        //                    primitive4.VertexArray.Append((float)vector4.Z);
        //                    list5.Add((ushort)((primitive4.VertexArray.Length / 3) - 1));
        //                    if (base._renderType == RenderType.Texture)
        //                    {
        //                        primitive4.TexcoordArray.Append((float)numArray[num]);
        //                        primitive4.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
        //                    }
        //                    primitive4.NormalArray.Append((float)vector6.X);
        //                    primitive4.NormalArray.Append((float)vector6.Y);
        //                    primitive4.NormalArray.Append((float)vector6.Z);
        //                    num2++;
        //                }
        //            }
        //        }
        //        primitive.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        primitive1.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        int num7 = this._pipeSection.SegCount + 2;
        //        for (num = 0; num < (list.Count - 1); num++)
        //        {
        //            num2 = 0;
        //            while (num2 < this._pipeSection.SegCount)
        //            {
        //                //if (num == 0)
        //                //{
        //                //    primitive1.IndexArray.Append(list4[0]);
        //                //    primitive1.IndexArray.Append(list4[(num2 + 1)]);
        //                //    primitive1.IndexArray.Append(list4[(num2 + 1) + 1]);
        //                //}
        //                if (this._pipeSection.SecShape == SecShape.CircleRing)
        //                {
        //                    primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list2[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
        //                    primitive.IndexArray.Append(list2[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
        //                }
        //                else
        //                {
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
        //                    primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                    primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
        //                }
        //                //if (num == (list.Count - 2))
        //                //{
        //                //    primitive1.IndexArray.Append(list4[num7]);
        //                //    primitive1.IndexArray.Append(list4[(num7 + (num2 + 1))]);
        //                //    primitive1.IndexArray.Append(list4[(num7 + (num2 + 1) + 1)]);
        //                //}
        //                num2++;
        //            }
        //        }
        //        for (num = 0; num < list.Count; num++)
        //        {
        //            num2 = 0;
        //            while (num2 < this._pipeSection.SegCount)
        //            {
        //                if (num == 0)
        //                {
        //                    primitive1.IndexArray.Append(list4[0]);
        //                    primitive1.IndexArray.Append(list4[(num2 + 1)]);
        //                    primitive1.IndexArray.Append(list4[(num2 + 1) + 1]);
        //                }
        //                if (num == (list.Count - 1))
        //                {
        //                    primitive1.IndexArray.Append(list4[num7]);
        //                    primitive1.IndexArray.Append(list4[(num7 + (num2 + 1))]);
        //                    primitive1.IndexArray.Append(list4[(num7 + (num2 + 1) + 1)]);
        //                }
        //                num2++;
        //            }
        //        }
        //        primitive4.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        if (this._pipeSection.SecShape == SecShape.CircleRing)
        //        {
        //            for (num = 0; num < (list.Count - 1); num++)
        //            {
        //                for (num2 = 0; num2 < DrawGeometry.SimpleSegments; num2++)
        //                {
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
        //                    primitive4.IndexArray.Append(list5[((num * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                    primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            primitive4.VertexArray = primitive.VertexArray;
        //            primitive4.NormalArray = primitive.NormalArray;
        //            primitive4.TexcoordArray = primitive.TexcoordArray;
        //            primitive4.IndexArray = primitive.IndexArray;
        //        }
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        SystemLog.Instance.Log(exception.StackTrace);
        //        return false;
        //    }
        //}
        /// <summary>
        /// 绘制外壁和起始封口(剪切面法) FX 2014.09.17
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="fmodel"></param>
        /// <param name="smodel"></param>
        /// <returns></returns>
        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            Vector w = null;
            Vector u = null;
            Vector v = null;
            List<Vector> list = null;
            List<ushort> list5 = null;
            double[] numArray = null;                 // 管线顶点向量间长度
            double[] vtxs = null;                     // 精模断面顶点信息
            double[] simpleVtxs = null;               // 简模断面顶点信息
            List<int> lstBpIndex = new List<int>();
            base.Draw(out mp, out fmodel, out smodel);
            DrawGeometry.Clear();
            try
            {
                int num;
                int num2;
                List<ushort> list2;
                List<ushort> list3;
                List<ushort> list4;
                int num3;
                if ((this._route != null) && (this._route.Count != 0))
                {
                    if (this._turnerStlye == TurnerStyle.Capround)
                    {
                        list = Maths.DisperseLineFX1(this._route, this._pipeSection.Diameter, ref lstBpIndex);
                    }
                    else
                    {
                        list = this._route;
                    }
                    vtxs = this._pipeSection.GetVtxs();
                    if (this._pipeSection.SecShape == SecShape.CircleRing)
                    {
                        list5 = new List<ushort>();
                        simpleVtxs = this._pipeSection.GetSimpleVtxs();
                    }
                    list2 = new List<ushort>();
                    list3 = new List<ushort>();
                    list4 = new List<ushort>();
                    numArray = new double[list.Count];
                    for (num = 0; num < list.Count; num++)
                    {
                        if (num == 0)
                        {
                            numArray[num] = 0.0;
                        }
                        else
                        {
                            numArray[num] += (list[num] - list[num - 1]).Length;
                        }
                    }
                    object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                    int[] index = new int[2];
                    index[1] = 1;
                    // 调整封口处颜色 与管壁颜色一致
                    uint[] numArray1 = renderInfo as uint[];
                    if (numArray1.Length > 1)
                    {
                        for (int i = 1; i < numArray1.Length; i++)
                        {
                            numArray1[i] = numArray1[0];
                        }
                    }
                    // 创建精模--1模型/1绘制组/3绘制单元
                    if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
                    {
                        // 创建简模--1模型/1绘制组/1绘制单元
                        int[] numArray5 = new int[1];
                        if (base.NewEmptyModel(numArray5, base._renderType, renderInfo, out smodel))
                        {
                            goto Label_017C;
                        }
                    }
                }
                return false;
            Label_017C:
                num3 = 0;
                int num4 = 0;
                IDrawGroup group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);                // 精模绘制单元1--外壁
                IDrawPrimitive primitive1 = group.GetPrimitive(1);               // 精模绘制单元2--起始处封口
                IDrawPrimitive primitive4 = smodel.GetGroup(0).GetPrimitive(0);  // 简模绘制单元--单壁
                Vector vector4;
                Vector vector6;
                Vector vector5;
                Vector vector7;
                Vector vector8;
                Vector vector9;
                List<Vector> RefList1 = new List<Vector>();
                List<Vector> RefList2 = new List<Vector>();
                List<Vector> RefList3 = new List<Vector>();
                List<Vector> RefList4 = new List<Vector>();
                List<Vector> RefList5 = new List<Vector>();
                for (num = 0; num < list.Count; num++)
                {
                    if (num == 0)
                    {
                        w = list[num + 1] - list[num];
                        w = w.UnitVector();
                        Maths.GenerateComplementBasis(w, out u, out v);
                    }
                    num2 = 0;
                    int IsAdd = -1;
                    while (num2 <= this._pipeSection.SegCount)
                    {
                        if (num == 0)
                        {
                            vector6 = (((vtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((vtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector(); // 管壁法向量
                            vector4 = list[num] + ((Vector)((vtxs[num2 * 4] * u) + (vtxs[(num2 * 4) + 1] * v)));                                                    // 外壁点坐标
                            vector5 = list[num] + ((Vector)((vtxs[(num2 * 4) + 2] * u) + (vtxs[(num2 * 4) + 3] * v)));                                              // 内壁点坐标
                            RefList1.Add(vector6);
                            RefList2.Add(vector4);
                            RefList3.Add(vector5);
                        }
                        else
                        {
                            vector7 = RefList1[num2];
                            vector8 = RefList2[num2];                            // 空间直线上一点-外壁
                            vector9 = RefList3[num2];                            // 空间直线上一点-内壁
                            w = list[num] - list[num - 1];                       // 空间直线的方向向量
                            Vector planeVector = list[num];                      // 剪切面上一点   
                            Vector normalVector = new Vector();                  // 剪切面的法向量
                            if (num == list.Count - 1)
                            {
                                normalVector = list[num - 1] - list[num];
                            }
                            else
                            {
                                Vector n1 = (list[num - 1] - list[num]).UnitVector();
                                Vector n2 = (list[num + 1] - list[num]).UnitVector();
                                if (Vector.CalcAngle(n1, n2) > 2.967)
                                {
                                    normalVector = list[num - 1] - list[num];
                                }
                                else
                                {
                                    normalVector = (n1 * n2) * (n1 + n2);
                                }
                            }
                            // 求解空间直线与平面的交点坐标
                            double d1 = normalVector.X * w.X + normalVector.Y * w.Y + normalVector.Z * w.Z;
                            double d2 = (planeVector.X - vector8.X) * normalVector.X + (planeVector.Y - vector8.Y) * normalVector.Y + (planeVector.Z - vector8.Z) * normalVector.Z;
                            double d3 = (planeVector.X - vector9.X) * normalVector.X + (planeVector.Y - vector9.Y) * normalVector.Y + (planeVector.Z - vector9.Z) * normalVector.Z;
                            double t1 = 0;
                            double t2 = 0;
                            if (d1 != 0)
                            {
                                t1 = d2 / d1;
                                t2 = d3 / d1;
                            }
                            vector4 = vector8 + w * t1;
                            vector5 = vector9 + w * t2;
                            vector6 = (vector4 - planeVector).UnitVector();
                            RefList1[num2] = vector6;
                            RefList2[num2] = vector4;
                            RefList3[num2] = vector5;
                        }
                        if ((num == 0) || (num == (list.Count - 1)))
                        {
                            if (IsAdd < 0)
                            {
                                // 管线封口
                                primitive1.VertexArray.Append((float)list[num].X);
                                primitive1.VertexArray.Append((float)list[num].Y);
                                primitive1.VertexArray.Append((float)list[num].Z);
                                list4.Add((ushort)((primitive1.VertexArray.Length / 3) - 1));
                                primitive1.NormalArray.Append(-((float)w.X));
                                primitive1.NormalArray.Append(-((float)w.Y));
                                primitive1.NormalArray.Append(-((float)w.Z));
                                IsAdd = 1;
                            }
                            primitive1.VertexArray.Append((float)vector4.X);
                            primitive1.VertexArray.Append((float)vector4.Y);
                            primitive1.VertexArray.Append((float)vector4.Z);
                            list4.Add((ushort)((primitive1.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive1.TexcoordArray.Append(num * 0.4f);
                                primitive1.TexcoordArray.Append(num2 * 0.4f);
                                primitive1.TexcoordArray.Append(num * 0.6f);
                                primitive1.TexcoordArray.Append(num2 * 0.6f);
                            }
                            primitive1.NormalArray.Append(-((float)w.X));
                            primitive1.NormalArray.Append(-((float)w.Y));
                            primitive1.NormalArray.Append(-((float)w.Z));
                            //primitive1.NormalArray.Append((float)w.X);
                            //primitive1.NormalArray.Append((float)w.Y);
                            //primitive1.NormalArray.Append((float)w.Z);
                        }
                        if (this._pipeSection.SecShape == SecShape.CircleRing)
                        {
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
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
                            list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            primitive.VertexArray.Append((float)vector4.X);
                            primitive.VertexArray.Append((float)vector4.Y);
                            primitive.VertexArray.Append((float)vector4.Z);
                            list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive.TexcoordArray.Append((float)numArray[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
                                primitive.TexcoordArray.Append((float)numArray[num]);
                                primitive.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
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
                    // 简模
                    if ((this._pipeSection.SecShape == SecShape.CircleRing) && (simpleVtxs != null))
                    {
                        num2 = 0;
                        while (num2 <= DrawGeometry.SimpleSegments)
                        {
                            if (num == 0)
                            {
                                vector6 = (((simpleVtxs[num2 * 4] - this._pipeSection.OffsetX) * u) + ((simpleVtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * v)).UnitVector();
                                vector4 = list[num] + ((Vector)((simpleVtxs[num2 * 4] * u) + (simpleVtxs[(num2 * 4) + 1] * v)));
                                RefList4.Add(vector6);
                                RefList5.Add(vector4);
                            }
                            else
                            {
                                vector7 = RefList4[num2];
                                vector8 = RefList5[num2];                            // 空间直线上一点-外壁
                                w = list[num] - list[num - 1];                       // 空间直线的方向向量
                                Vector planeVector = list[num];                      // 剪切面上一点   
                                Vector normalVector = new Vector();                  // 剪切面的法向量
                                if (num == list.Count - 1)
                                {
                                    normalVector = list[num - 1] - list[num];
                                }
                                else
                                {
                                    Vector n1 = (list[num - 1] - list[num]).UnitVector();
                                    Vector n2 = (list[num + 1] - list[num]).UnitVector();
                                    if (Vector.CalcAngle(n1, n2) > 2.967)
                                    {
                                        normalVector = list[num - 1] - list[num];
                                    }
                                    else
                                    {
                                        normalVector = (n1 * n2) * (n1 + n2);
                                    }
                                }
                                // 求解空间直线与平面的交点坐标
                                double d1 = normalVector.X * w.X + normalVector.Y * w.Y + normalVector.Z * w.Z;
                                double d2 = (planeVector.X - vector8.X) * normalVector.X + (planeVector.Y - vector8.Y) * normalVector.Y + (planeVector.Z - vector8.Z) * normalVector.Z;
                                double t1 = 0;
                                if (d1 != 0)
                                {
                                    t1 = d2 / d1;
                                }
                                vector4 = vector8 + w * t1;
                                vector6 = (vector4 - planeVector).UnitVector();
                                RefList4[num2] = vector6;
                                RefList5[num2] = vector4;
                            }
                            primitive4.VertexArray.Append((float)vector4.X);
                            primitive4.VertexArray.Append((float)vector4.Y);
                            primitive4.VertexArray.Append((float)vector4.Z);
                            list5.Add((ushort)((primitive4.VertexArray.Length / 3) - 1));
                            if (base._renderType == RenderType.Texture)
                            {
                                primitive4.TexcoordArray.Append((float)numArray[num]);
                                primitive4.TexcoordArray.Append((num2 * 8f) / ((float)this._pipeSection.SegCount));
                            }
                            primitive4.NormalArray.Append((float)vector6.X);
                            primitive4.NormalArray.Append((float)vector6.Y);
                            primitive4.NormalArray.Append((float)vector6.Z);
                            num2++;
                        }
                    }
                }
                primitive.Material.CullMode = gviCullFaceMode.gviCullNone;
                primitive1.Material.CullMode = gviCullFaceMode.gviCullNone;
                int num7 = this._pipeSection.SegCount + 2;
                for (num = 0; num < (list.Count - 1); num++)
                {
                    num2 = 0;
                    while (num2 < this._pipeSection.SegCount)
                    {
                        //if (num == 0)
                        //{
                        //    primitive1.IndexArray.Append(list4[0]);
                        //    primitive1.IndexArray.Append(list4[(num2 + 1)]);
                        //    primitive1.IndexArray.Append(list4[(num2 + 1) + 1]);
                        //}
                        if (this._pipeSection.SecShape == SecShape.CircleRing)
                        {
                            primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
                            primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
                            primitive.IndexArray.Append(list2[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
                            primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
                            primitive.IndexArray.Append(list2[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
                            primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
                        }
                        else
                        {
                            primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
                            primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 1]);
                            primitive.IndexArray.Append(list2[(((num * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
                            primitive.IndexArray.Append(list2[((((num + 1) * (this._pipeSection.SegCount + 1)) * 2) + (num2 * 2)) + 2]);
                        }
                        //if (num == (list.Count - 2))
                        //{
                        //    primitive1.IndexArray.Append(list4[num7]);
                        //    primitive1.IndexArray.Append(list4[(num7 + (num2 + 1))]);
                        //    primitive1.IndexArray.Append(list4[(num7 + (num2 + 1) + 1)]);
                        //}
                        num2++;
                    }
                }
                for (num = 0; num < list.Count; num++)
                {
                    num2 = 0;
                    while (num2 < this._pipeSection.SegCount)
                    {
                        if (num == 0)
                        {
                            primitive1.IndexArray.Append(list4[0]);
                            primitive1.IndexArray.Append(list4[(num2 + 1)]);
                            primitive1.IndexArray.Append(list4[(num2 + 1) + 1]);
                        }
                        if (num == (list.Count - 1))
                        {
                            primitive1.IndexArray.Append(list4[num7]);
                            primitive1.IndexArray.Append(list4[(num7 + (num2 + 1))]);
                            primitive1.IndexArray.Append(list4[(num7 + (num2 + 1) + 1)]);
                        }
                        num2++;
                    }
                }
                primitive4.Material.CullMode = gviCullFaceMode.gviCullNone;
                if (this._pipeSection.SecShape == SecShape.CircleRing)
                {
                    for (num = 0; num < (list.Count - 1); num++)
                    {
                        for (num2 = 0; num2 < DrawGeometry.SimpleSegments; num2++)
                        {
                            primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
                            primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
                            primitive4.IndexArray.Append(list5[((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2]);
                            primitive4.IndexArray.Append(list5[(num * (DrawGeometry.SimpleSegments + 1)) + num2]);
                            primitive4.IndexArray.Append(list5[((num * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
                            primitive4.IndexArray.Append(list5[(((num + 1) * (DrawGeometry.SimpleSegments + 1)) + num2) + 1]);
                        }
                    }
                }
                else
                {
                    primitive4.VertexArray = primitive.VertexArray;
                    primitive4.NormalArray = primitive.NormalArray;
                    primitive4.TexcoordArray = primitive.TexcoordArray;
                    primitive4.IndexArray = primitive.IndexArray;
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        #endregion

        #region 设置绘制参数
        // 添加PipeLocateType类型参数以辨认管线为地下\架空 FX 2014.04.01
        public void SetParameter(IPipeSection pipeSection, List<Vector> vtxs, TurnerStyle turnerStyle, LocationType pipeLocateType)
        {
            try
            {
                this._pipeSection = pipeSection;
                base._x = vtxs[0].X;
                base._y = vtxs[0].Y;
                base._z = vtxs[0].Z;
                this._route = new List<Vector>();
                for (int i = 0; i < vtxs.Count; i++)
                {
                    Vector item = new Vector
                    {
                        X = vtxs[i].X - base._x,
                        Y = vtxs[i].Y - base._y,
                        Z = vtxs[i].Z - base._z
                    };
                    this._route.Add(item);
                }
                if (turnerStyle == TurnerStyle.Capround)
                {
                    this._turnerStlye = TurnerStyle.Capround;
                }
                else
                {
                    this._turnerStlye = TurnerStyle.Capsquare;
                }
                if (pipeLocateType == LocationType.OverHead)
                {
                    this._pipeLocateType = LocationType.OverHead;
                }
                else
                {
                    this._pipeLocateType = LocationType.UnderGround;
                }
            }
            catch (Exception exception)
            {
            }
        }
        public void SetParameter(IPipeSection pipeSection, IPolyline polyline)
        {
            this._pipeSection = pipeSection;
            DrawGeometry.GetPolylineVtxs(polyline, ref this._x, ref this._y, ref this._z, ref this._route);
        }
        public void SetParameter(IPipeSection pipeSection, List<Vector> vtxs)
        {
            try
            {
                this._pipeSection = pipeSection;
                base._x = vtxs[0].X;
                base._y = vtxs[0].Y;
                base._z = vtxs[0].Z;
                this._route = new List<Vector>();
                for (int i = 0; i < vtxs.Count; i++)
                {
                    Vector item = new Vector
                    {
                        X = vtxs[i].X - base._x,
                        Y = vtxs[i].Y - base._y,
                        Z = vtxs[i].Z - base._z
                    };
                    this._route.Add(item);
                }
            }
            catch (Exception exception)
            {
            }
        }
        #endregion

        // Properties
        public IPipeSection PipeSection
        {
            get
            {
                return this._pipeSection;
            }
            set
            {
                this._pipeSection = value;
            }
        }
        public List<Vector> Route
        {
            get
            {
                return this._route;
            }
            set
            {
                this._route = value;
            }
        }
        public TurnerStyle TurnerStyle
        {
            get
            {
                return this._turnerStlye;
            }
            set
            {
                this._turnerStlye = value;
            }
        }
        public LocationType PipeLocateType
        {
            get
            {
                return this._pipeLocateType;
            }
            set
            {
                this._pipeLocateType = value;
            }
        }
    }
}
