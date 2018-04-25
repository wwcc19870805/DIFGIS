using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawReduceWell : DrawWell, IDrawReduceWell, IDrawWell, IDrawScanModel, IDrawGeometry
    {
        // Fields
        private double _depth;
        private double _maxDia;
        private const int _nSegments = 0x18;
        private const double _radius = 1.0;
        private double _rBottom;
        private double _rDepth1 = 1.0;
        private double _rDepth2 = 1.5;
        private double[] _vtxsBottom;

        // Methods
        public DrawReduceWell()
        {
            base._modeltype = ModelType.ReduceWell;
        }

        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            int index = -1;
            int num2 = -1;
            IDrawPrimitive primitive = null;
            IDoubleArray vArray = null;
            IUInt16Array indexArray = null;
            IFloatArray array3 = null;
            IDoubleArray norms = null;
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                int[] numArray = new int[3];
                numArray[1] = 1;
                numArray[2] = 2;
                if (!base.NewEmptyModel(numArray, base._renderType, renderInfo, out fmodel))
                {
                    return false;
                }
                primitive = fmodel.GetGroup(0).GetPrimitive(0);
                if (DrawGeometry.ConvertPolygon(base._vtx, 0.0, 0, out vArray, out indexArray, out array3, out norms))
                {
                    for (index = 0; index < vArray.Length; index++)
                    {
                        primitive.VertexArray.Append((float)vArray.Array[index]);
                    }
                    for (index = 0; index < indexArray.Length; index++)
                    {
                        primitive.IndexArray.Append(indexArray.Array[index]);
                    }
                    double naN = double.NaN;
                    double ty = double.NaN;
                    for (index = 0; index < (vArray.Length / 3); index++)
                    {
                        base.GetTexcoord(vArray.Array[index * 3], vArray.Array[(index * 3) + 1], out naN, out ty);
                        primitive.TexcoordArray.Append((float)naN);
                        primitive.TexcoordArray.Append((float)ty);
                    }
                    for (index = 0; index < norms.Length; index++)
                    {
                        primitive.NormalArray.Append((float)norms.Array[index]);
                    }
                }
                primitive = fmodel.GetGroup(0).GetPrimitive(1);
                if (DrawGeometry.ConvertPolygon(this._vtxsBottom, -this._depth, 1, out vArray, out indexArray, out array3, out norms))
                {
                    for (index = 0; index < vArray.Length; index++)
                    {
                        primitive.VertexArray.Append((float)vArray.Array[index]);
                    }
                    for (index = 0; index < indexArray.Length; index++)
                    {
                        primitive.IndexArray.Append(indexArray.Array[index]);
                    }
                    for (index = 0; index < (vArray.Length / 3); index++)
                    {
                        float num3 = (float)vArray.Array[index * 3];
                        primitive.TexcoordArray.Append(num3);
                        num3 = (float)vArray.Array[(index * 3) + 1];
                        primitive.TexcoordArray.Append(num3);
                    }
                    for (index = 0; index < norms.Length; index++)
                    {
                        primitive.NormalArray.Append((float)norms.Array[index]);
                    }
                }
                List<ushort> list = new List<ushort>();
                List<double> list2 = new List<double> {
                0.0,
                this._rDepth1,
                this._rDepth2,
                this._depth
            };
                for (index = 0; index <= 0x18; index++)
                {
                    num2 = 0;
                    while (num2 < list2.Count)
                    {
                        if (num2 <= 1)
                        {
                            primitive.VertexArray.Append((float)base._vtx[index * 2]);
                            primitive.VertexArray.Append((float)base._vtx[(index * 2) + 1]);
                        }
                        else
                        {
                            primitive.VertexArray.Append((float)this._vtxsBottom[index * 2]);
                            primitive.VertexArray.Append((float)this._vtxsBottom[(index * 2) + 1]);
                        }
                        primitive.VertexArray.Append(-((float)list2[num2]));
                        list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                        primitive.TexcoordArray.Append((index * 4f) / 23f);
                        primitive.TexcoordArray.Append((float)list2[num2]);
                        primitive.NormalArray.Append((float)Math.Sin((6.2831853071795862 * index) / 24.0));
                        primitive.NormalArray.Append((float)Math.Cos((6.2831853071795862 * index) / 24.0));
                        primitive.NormalArray.Append(0f);
                        num2++;
                    }
                }
                for (index = 0; index < 0x18; index++)
                {
                    for (num2 = 0; num2 < (list2.Count - 1); num2++)
                    {
                        primitive.IndexArray.Append(list[(index * 4) + num2]);
                        primitive.IndexArray.Append(list[(((index + 1) * 4) + num2) + 1]);
                        primitive.IndexArray.Append(list[((index * 4) + 1) + num2]);
                        primitive.IndexArray.Append(list[(index * 4) + num2]);
                        primitive.IndexArray.Append(list[((index + 1) * 4) + num2]);
                        primitive.IndexArray.Append(list[(((index + 1) * 4) + num2) + 1]);
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private double GetWellDia(double maxDia)
        {
            if (maxDia <= 0.8)
            {
                return 1.2;
            }
            if ((maxDia > 0.8) && (maxDia <= 1.2))
            {
                return 1.5;
            }
            if ((maxDia > 1.2) && (maxDia <= 1.6))
            {
                return 2.0;
            }
            if ((maxDia > 1.6) && (maxDia <= 2.0))
            {
                return 2.4;
            }
            if ((maxDia > 2.0) && (maxDia <= 2.4))
            {
                return 2.8;
            }
            if ((maxDia > 2.4) && (maxDia <= 2.8))
            {
                return 3.2;
            }
            if ((maxDia > 2.8) && (maxDia <= 3.5))
            {
                return 4.0;
            }
            return 5.0;
        }

        public void SetParameter(IPoint position, double hTop, double hBottom, double maxDia)
        {
            base._hTop = hTop;
            base._hBottom = hBottom;
            base._minX = 999999.9;
            base._maxX = -999999.9;
            base._minY = 999999.9;
            base._maxY = -999999.9;
            try
            {
                base._x = position.X;
                base._y = position.Y;
                base._z = hTop;
                this._maxDia = maxDia;
                this._depth = base._hTop - base._hBottom;
                this._rBottom = this.GetWellDia(this._maxDia);
                DrawGeometry.GetRoundVtxs(position, this._rBottom, 0x18, ref this._vtxsBottom);
                DrawGeometry.GetRoundVtxs(position, 1.0, 0x18, ref this._vtx, ref this._minX, ref this._maxX, ref this._minY, ref this._maxY);
            }
            catch (Exception exception)
            {
            }
        }

        // Properties
        public double MaxDia
        {
            get
            {
                return this._maxDia;
            }
        }
    }
}
