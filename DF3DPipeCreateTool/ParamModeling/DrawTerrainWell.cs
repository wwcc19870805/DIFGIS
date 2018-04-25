using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawTerrainWell : DrawWell, IDrawTerrainWell, IDrawWell, IDrawScanModel, IDrawGeometry
    {
        // Fields
        private double[] _segLenth;
        private double[] _terrainLine;
        protected double[] _vtx2;

        // Methods
        public DrawTerrainWell()
        {
            base._modeltype = ModelType.TerrainWell;
            base._cullModel = gviCullFaceMode.gviCullNone;
        }

        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            int index = -1;
            IDrawPrimitive primitive = null;
            IDoubleArray vArray = null;
            IUInt16Array indexArray = null;
            IFloatArray array3 = null;
            IDoubleArray norms = null;
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                int[] numArray = new int[2];
                numArray[1] = 1;
                if (!base.NewEmptyModel(numArray, base._renderType, renderInfo, out fmodel))
                {
                    return false;
                }
                //绘制底部
                primitive = fmodel.GetGroup(0).GetPrimitive(0);
                if (DrawGeometry.ConvertPolygon(this._vtx2, 0.0, 0, out vArray, out indexArray, out array3, out norms))
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
                        float num2 = (float)vArray.Array[index * 3];
                        primitive.TexcoordArray.Append(num2);
                        num2 = (float)vArray.Array[(index * 3) + 1];
                        primitive.TexcoordArray.Append(num2);
                    }
                    for (index = 0; index < norms.Length; index++)
                    {
                        primitive.NormalArray.Append((float)norms.Array[index]);
                    }
                }
                double num3 = 0.0;
                int num4 = base._vtx.Length / 2;
                List<ushort> list = new List<ushort>();
                primitive = fmodel.GetGroup(0).GetPrimitive(1);
                for (index = 0; index < num4; index++)
                {
                    primitive.VertexArray.Append((float)base._vtx[index * 2]);
                    primitive.VertexArray.Append((float)base._vtx[(index * 2) + 1]);
                    primitive.VertexArray.Append((float)(this._terrainLine[index] - base._hBottom));
                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                    primitive.VertexArray.Append((float)this._vtx2[index * 2]);
                    primitive.VertexArray.Append((float)this._vtx2[(index * 2) + 1]);
                    primitive.VertexArray.Append(0f);
                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                    num3 = (index == 0) ? num3 : (num3 += this._segLenth[index - 1]);
                    primitive.TexcoordArray.Append((float)num3);
                    primitive.TexcoordArray.Append((float)(this._terrainLine[index] - base._hBottom));
                    primitive.TexcoordArray.Append((float)num3);
                    primitive.TexcoordArray.Append(0f);
                    IVector3 vector = new Vector3Class
                    {
                        X = base._vtx[index * 2],
                        Y = base._vtx[(index * 2) + 1],
                        Z = 0.0
                    };
                    vector.Normalize();
                    primitive.NormalArray.Append((float)vector.X);
                    primitive.NormalArray.Append((float)vector.Y);
                    primitive.NormalArray.Append((float)vector.Z);
                    //工程开挖挖洞效果贴图模型出错,添加法向量数组,使其与顶点数组数量一致
                    primitive.NormalArray.Append((float)vector.X);
                    primitive.NormalArray.Append((float)vector.Y);
                    primitive.NormalArray.Append((float)vector.Z);
                }
                for (index = 0; index < (num4 - 1); index++)
                {
                    primitive.IndexArray.Append(list[index * 2]);
                    primitive.IndexArray.Append(list[(index * 2) + 1]);
                    primitive.IndexArray.Append(list[((index + 1) * 2) + 1]);
                    primitive.IndexArray.Append(list[index * 2]);
                    primitive.IndexArray.Append(list[((index + 1) * 2) + 1]);
                    primitive.IndexArray.Append(list[(index + 1) * 2]);
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public static bool GetPolygonVtxs(IPolygon polygon, double angle, double height, ref double cX, ref double cY, ref double[] vtxs, ref double[] _vtx2)
        {
            if (polygon == null)
            {
                return false;
            }
            double num = 0.0;
            IPoint other = null;
            IPoint point = null;
            IPolygon polygon2 = null;
            IPolygon polygon3 = null;
            polygon3 = polygon.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolygon;
            other = polygon3.Centroid;
            cX = other.X;
            cY = other.Y;
            num = (polygon.ExteriorRing.GetSegment(0) as IProximityOperator).Distance2D(other);
            double num2 = height / Math.Tan((angle * 3.1415926535897931) / 180.0);
            double num3 = num - num2;
            if (num3 <= 0.0)
            {
                num3 = 0.2;
            }
            double scaleX = num3 / num;
            polygon2 = polygon.Clone() as IPolygon;
            (polygon2 as ITransform).Scale2D(scaleX, scaleX, other.X, other.Y);
            vtxs = new double[polygon.ExteriorRing.PointCount * 2];
            _vtx2 = new double[polygon.ExteriorRing.PointCount * 2];
            for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
            {
                point = polygon.ExteriorRing.GetPoint(i);
                vtxs[i * 2] = point.X - cX;
                vtxs[(i * 2) + 1] = point.Y - cY;
                point = polygon2.ExteriorRing.GetPoint(i);
                _vtx2[i * 2] = point.X - cX;
                _vtx2[(i * 2) + 1] = point.Y - cY;
            }
            return true;
        }

        public void SetParameter(IPolygon region, double hTop, double hBottom, double angle)
        {
            try
            {
                int num;
                base._hTop = hTop;
                base._z = base._hBottom = hBottom;
                double height = base._hTop - base._hBottom;
                this._terrainLine = new double[region.ExteriorRing.PointCount];
                for (num = 0; num < region.ExteriorRing.PointCount; num++)
                {
                    this._terrainLine[num] = region.ExteriorRing.GetPoint(num).Z;
                }
                this._segLenth = new double[region.ExteriorRing.SegmentCount];
                for (num = 0; num < region.ExteriorRing.SegmentCount; num++)
                {
                    this._segLenth[num] = region.ExteriorRing.GetSegment(num).Length / 2.0;
                }
                GetPolygonVtxs(region, angle, height, ref this._x, ref this._y, ref this._vtx, ref this._vtx2);
            }
            catch (Exception exception)
            {
            }
        }
    }
}
