using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawHeevenWell : DrawWell, IDrawHeevenWell, IDrawWell, IDrawScanModel, IDrawGeometry
    {
        // Fields
        private double _depth;                   // 小室井深
        private const int _nSegments = 0x18;
        private const double _radius = 1.0;

        // Methods
        public DrawHeevenWell()
        {
            base._modeltype = ModelType.HeevenWell;
        }

        #region 绘制小室
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
                if (DrawGeometry.ConvertPolygon(base._vtx, -this._depth, 1, out vArray, out indexArray, out array3, out norms))
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
                int num5 = base._vtx.Length / 2;
                List<ushort> list = new List<ushort>();
                for (index = 0; index < num5; index++)
                {
                    primitive.VertexArray.Append((float)base._vtx[index * 2]);
                    primitive.VertexArray.Append((float)base._vtx[(index * 2) + 1]);
                    primitive.VertexArray.Append(0f);
                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                    primitive.VertexArray.Append((float)base._vtx[index * 2]);
                    primitive.VertexArray.Append((float)base._vtx[(index * 2) + 1]);
                    primitive.VertexArray.Append(-((float)this._depth));
                    list.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                    primitive.TexcoordArray.Append((index * 4f) / ((float)(num5 - 1)));
                    primitive.TexcoordArray.Append(0f);
                    primitive.TexcoordArray.Append((index * 4f) / ((float)(num5 - 1)));
                    primitive.TexcoordArray.Append((float)this._depth);
                    primitive.NormalArray.Append((float)Math.Sin((6.2831853071795862 * index) / 24.0));
                    primitive.NormalArray.Append((float)Math.Cos((6.2831853071795862 * index) / 24.0));
                    primitive.NormalArray.Append(0f);
                    primitive.NormalArray.Append((float)Math.Sin((6.2831853071795862 * index) / 24.0));
                    primitive.NormalArray.Append((float)Math.Cos((6.2831853071795862 * index) / 24.0));
                    primitive.NormalArray.Append(0f);
                }
                for (index = 0; index < (num5 - 1); index++)
                {
                    primitive.IndexArray.Append(list[index * 2]);
                    primitive.IndexArray.Append(list[((index + 1) * 2) + 1]);
                    primitive.IndexArray.Append(list[(index * 2) + 1]);
                    primitive.IndexArray.Append(list[index * 2]);
                    primitive.IndexArray.Append(list[(index + 1) * 2]);
                    primitive.IndexArray.Append(list[((index + 1) * 2) + 1]);
                }
                return true;
            }
            catch (Exception exception)
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

        #region 设置小室参数
        // Polyline类型  FX 2014.04.08
        public void SetParameter1(IGeometry geometry, double hTop, double hBottom)
        {
            base._hTop = hTop;
            base._hBottom = hBottom;
            base._minX = 999999.9;
            base._maxX = -999999.9;
            base._minY = 999999.9;
            base._maxY = -999999.9;
            try
            {
                IPolygon polygon;
                base._z = hTop;
                this._depth = base._hTop - base._hBottom;
                switch (geometry.GeometryType)
                {
                    case gviGeometryType.gviGeometryPoint:
                        {
                            IPoint point = geometry as IPoint;
                            base._x = point.X;
                            base._y = point.Y;
                            DrawGeometry.GetRoundVtxs(point, 1.0, 0x18, ref this._vtx, ref this._minX, ref this._maxX, ref this._minY, ref this._maxY);
                            break;
                        }
                    case gviGeometryType.gviGeometryPolyline:
                        goto Label_00CD;
                }
                return;
            Label_00CD:
                // 将polyline转化为polygon
                IPolyline polyline = geometry as IPolyline;
                if (polyline.PointCount < 4)
                {
                    return;
                }
                polygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                if (polygon != null)
                {
                    for (int i = 0; i < polyline.PointCount; i++)
                    {
                        IPoint point = polyline.GetPoint(i);
                        polygon.ExteriorRing.AppendPoint(point);
                    }
                }
                if (!polygon.IsClosed)
                {
                    polygon.Close();
                }
                if (polygon.QueryNormal().Z > 0.0)
                {
                    ReversePolygon(ref polygon);
                }
                DrawGeometry.GetPolygonVtxs(polygon, ref this._x, ref this._y, ref this._vtx, ref this._minX, ref this._maxX, ref this._minY, ref this._maxY);
            }
            catch (Exception exception)
            {
            }
        }
        // Polygon类型
        public void SetParameter(IGeometry geometry, double hTop, double hBottom)
        {
            base._hTop = hTop;
            base._hBottom = hBottom;
            base._minX = 999999.9;
            base._maxX = -999999.9;
            base._minY = 999999.9;
            base._maxY = -999999.9;
            try
            {
                IPolygon polygon;
                base._z = hTop;
                this._depth = base._hTop - base._hBottom;
                switch (geometry.GeometryType)
                {
                    case gviGeometryType.gviGeometryPoint:
                        {
                            IPoint point = geometry as IPoint;
                            base._x = point.X;
                            base._y = point.Y;
                            DrawGeometry.GetRoundVtxs(point, 1.0, 0x18, ref this._vtx, ref this._minX, ref this._maxX, ref this._minY, ref this._maxY);
                            break;
                        }
                    case gviGeometryType.gviGeometryPolygon:
                        goto Label_00CD;
                }
                return;
            Label_00CD:
                polygon = geometry as IPolygon;
                if (polygon != null)
                {
                    if (polygon.QueryNormal().Z > 0.0)
                    {
                        ReversePolygon(ref polygon);
                    }
                    DrawGeometry.GetPolygonVtxs(polygon, ref this._x, ref this._y, ref this._vtx, ref this._minX, ref this._maxX, ref this._minY, ref this._maxY);
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
        private void ReversePolygon(ref IPolygon polygon)
        {
            if ((polygon != null) && (polygon.ExteriorRing.PointCount != 0))
            {
                Stack<IPoint> stack = null;
                stack = new Stack<IPoint>();
                for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
                {
                    stack.Push(polygon.ExteriorRing.GetPoint(i));
                }
                polygon.ExteriorRing.RemovePoints(0, polygon.ExteriorRing.PointCount);
                while (stack.Count > 0)
                {
                    polygon.ExteriorRing.AppendPoint(stack.Pop());
                }
            }
        }

        private void ReversePolyline(ref IPolyline ply)
        {
            if ((ply != null) && (ply.PointCount != 0))
            {
                Stack<IPoint> stack = null;
                stack = new Stack<IPoint>();
                for (int i = 0; i < ply.PointCount; i++)
                {
                    stack.Push(ply.GetPoint(i));
                }
                ply.RemovePoints(0, ply.PointCount);
                while (stack.Count > 0)
                {
                    ply.AppendPoint(stack.Pop());
                }
            }
        }

    }
}
