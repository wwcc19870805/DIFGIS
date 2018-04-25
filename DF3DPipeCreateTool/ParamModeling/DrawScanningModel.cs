using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
using DF3DPipeCreateTool.Class;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawScanningModel : DrawGeometry, IDrawScanningModel, IDrawGeometry
    {
        // Fields
        protected uint[] _colors;
        private double _minX;
        private double _minY;
        protected IPillarSection _pillarSection;
        protected RenderType _renderType;
        protected List<Vector> _route;
        protected string[] _tcNames;

        // Methods
        public DrawScanningModel(IMultiPolygon sSection, IMultiPolygon eSection, IPolyline polyline)
        {
            this.SetParameter(sSection, eSection, polyline);
        }

        public static bool ConvertBuildingParam(IPolygon footprint, double startZ, double height, out IMultiPolygon segPolygon, out IPolyline route)
        {
            segPolygon = null;
            route = null;
            IPoint point = null;
            IPoint pointValue = null;
            IEnvelope envelope = null;
            IPolygon polygon = null;
            if (footprint == null)
            {
                return false;
            }
            try
            {
                envelope = footprint.Envelope;
                segPolygon = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
                route = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                point = DrawGeometry.geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                point.SetCoords(envelope.Center.X, envelope.Center.Y, 0.0, 0.0, 0);
                pointValue = point.Clone() as IPoint;
                pointValue.Z = startZ;
                route.AppendPoint(pointValue);
                pointValue = point.Clone() as IPoint;
                pointValue.Z = startZ + height;
                route.AppendPoint(pointValue);
                int pointCount = footprint.ExteriorRing.PointCount;
                polygon = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                for (int i = 0; i < pointCount; i++)
                {
                    pointValue = footprint.ExteriorRing.GetPoint(i).Clone() as IPoint;
                    pointValue.X -= point.X;
                    pointValue.Y -= point.Y;
                    polygon.ExteriorRing.AppendPoint(pointValue);
                }
                segPolygon.AddPolygon(polygon);
                return true;
            }
            catch (Exception exception)
            {
                DrawGeometry.WriteLog(exception.StackTrace);
                return false;
            }
        }

        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            List<ushort> list2 = new List<ushort>();
            IPolygon polygon = null;
            IPolygon polygon2 = null;
            IPolygon polygon3 = null;
            IPoint pointValue = null;
            ITriMesh mesh = null;
            IDoubleArray vArray = null;
            IUInt16Array indexArray = null;
            IFloatArray array3 = null;
            IFloatArray array4 = null;
            IDoubleArray norms = null;
            IDrawGroup group = null;
            IDrawPrimitive primitive = null;
            IDrawPrimitive primitive2 = null;
            IDrawPrimitive primitive3 = null;
            double naN = double.NaN;
            double ty = double.NaN;
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                int num;
                int num2;
                List<Vector> list;
                polygon = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon2 = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon3 = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                if (this._route.Count > 2)
                {
                    list = Maths.DisperseLine(this._route, this._pillarSection.Diameter);
                }
                else
                {
                    list = this._route;
                }
                double[] vtxs = this._pillarSection.GetVtxs();
                SystemLog.Instance.Log("顶点个数:" + vtxs.Length);
                double[] numArray = new double[list.Count];
                for (num = 0; num < list.Count; num++)
                {
                    if (num == 0)
                    {
                        numArray[num] = 0.0;
                    }
                    else
                    {
                        numArray[num] = (list[num] - list[num - 1]).Length + numArray[num - 1];
                    }
                }
                object renderInfo = (this._renderType == RenderType.Texture) ? ((object)this._tcNames) : ((object)this._colors);
                int[] index = new int[3];
                index[1] = 1;
                index[2] = 2;
                if (!base.NewEmptyModel(index, this._renderType, renderInfo, out fmodel))
                {
                    return false;
                }
                group = fmodel.GetGroup(0);
                SystemLog.Instance.Log("开始计算正交向量:" + DateTime.Now.ToLongTimeString());
                primitive = group.GetPrimitive(0);
                for (num = 0; num < list.Count; num++)
                {
                    Vector vector;
                    Vector vector2;
                    Vector vector3;
                    if (num == 0)
                    {
                        vector = list[num + 1] - list[num];
                    }
                    else if (num == (list.Count - 1))
                    {
                        vector = list[num] - list[num - 1];
                    }
                    else
                    {
                        vector = ((list[num] - list[num - 1])).UnitVector() + ((list[num + 1] - list[num])).UnitVector();
                    }
                    Maths.GenerateComplementBasis(vector, out vector2, out vector3);
                    vector2 = -vector2;
                    num2 = 0;
                    while (num2 <= this._pillarSection.SegCount)
                    {
                        Vector vector6;
                        Vector vector7;
                        (((vtxs[num2 * 2] - this._pillarSection.OffsetX) * vector2) + ((vtxs[(num2 * 2) + 1] - this._pillarSection.OffsetY) * vector3)).UnitVector();
                        Vector vector4 = list[num] + ((Vector)((vtxs[num2 * 2] * vector2) + (vtxs[(num2 * 2) + 1] * vector3)));
                        Vector vector5 = (Vector)(vector4 * 1.01);
                        if (num == 0)
                        {
                            pointValue = DrawGeometry.geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pointValue.SetCoords(vector4.X, vector4.Y, vector4.Z, 0.0, 0);
                            polygon.ExteriorRing.AppendPoint(pointValue);
                            pointValue = DrawGeometry.geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pointValue.SetCoords(vector5.X, vector5.Y, vector5.Z, 0.0, 0);
                            polygon3.ExteriorRing.AppendPoint(pointValue);
                        }
                        else if (num == (list.Count - 1))
                        {
                            pointValue = DrawGeometry.geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pointValue.SetCoords(vector4.X, vector4.Y, vector4.Z, 0.0, 0);
                            polygon2.ExteriorRing.AppendPoint(pointValue);
                        }
                        primitive.VertexArray.Append((float)vector4.X);
                        primitive.VertexArray.Append((float)vector4.Y);
                        primitive.VertexArray.Append((float)vector4.Z);
                        list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                        primitive.VertexArray.Append((float)vector4.X);
                        primitive.VertexArray.Append((float)vector4.Y);
                        primitive.VertexArray.Append((float)vector4.Z);
                        list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                        if (this._renderType == RenderType.Texture)
                        {
                            primitive.TexcoordArray.Append((float)(num2 * 1.0));
                            primitive.TexcoordArray.Append((float)(num * 1.0));
                            primitive.TexcoordArray.Append((float)(num2 * 1.0));
                            primitive.TexcoordArray.Append((float)(num * 1.0));
                        }
                        if (num2 == 0)
                        {
                            vector6 = new Vector(vtxs[num2 * 2] - vtxs[(this._pillarSection.SegCount - 1) * 2], vtxs[(num2 * 2) + 1] - vtxs[((this._pillarSection.SegCount - 1) * 2) + 1], 0.0).UnitVector();
                            vector7 = new Vector(vtxs[(num2 + 1) * 2] - vtxs[num2 * 2], vtxs[((num2 + 1) * 2) + 1] - vtxs[(num2 * 2) + 1], 0.0).UnitVector();
                        }
                        else if (num2 == this._pillarSection.SegCount)
                        {
                            vector6 = new Vector(vtxs[num2 * 2] - vtxs[(num2 - 1) * 2], vtxs[(num2 * 2) + 1] - vtxs[((num2 - 1) * 2) + 1], 0.0).UnitVector();
                            vector7 = new Vector(vtxs[2] - vtxs[num2 * 2], vtxs[3] - vtxs[(num2 * 2) + 1], 0.0).UnitVector();
                        }
                        else
                        {
                            vector6 = new Vector(vtxs[num2 * 2] - vtxs[(num2 - 1) * 2], vtxs[(num2 * 2) + 1] - vtxs[((num2 - 1) * 2) + 1], 0.0).UnitVector();
                            vector7 = new Vector(vtxs[(num2 + 1) * 2] - vtxs[num2 * 2], vtxs[((num2 + 1) * 2) + 1] - vtxs[(num2 * 2) + 1], 0.0).UnitVector();
                        }
                        Vector vector8 = new Vector(vector6.Y, -vector6.X, vector6.Z);
                        primitive.NormalArray.Append((float)vector8.X);
                        primitive.NormalArray.Append((float)vector8.Y);
                        primitive.NormalArray.Append((float)vector8.Z);
                        vector8 = new Vector(vector7.Y, -vector7.X, vector7.Z);
                        primitive.NormalArray.Append((float)vector8.X);
                        primitive.NormalArray.Append((float)vector8.Y);
                        primitive.NormalArray.Append((float)vector8.Z);
                        num2++;
                    }
                }
                SystemLog.Instance.Log("结束计算正交向量:" + DateTime.Now.ToLongTimeString());
                SystemLog.Instance.Log("开始添加索引数组:" + DateTime.Now.ToLongTimeString());
                for (num = 0; num < (list.Count - 1); num++)
                {
                    for (num2 = 0; num2 < this._pillarSection.SegCount; num2++)
                    {
                        primitive.IndexArray.Append(list2[((num * ((this._pillarSection.SegCount + 1) * 2)) + (num2 * 2)) + 1]);
                        primitive.IndexArray.Append(list2[((num + 1) * ((this._pillarSection.SegCount + 1) * 2)) + ((num2 + 1) * 2)]);
                        primitive.IndexArray.Append(list2[(((num + 1) * ((this._pillarSection.SegCount + 1) * 2)) + (num2 * 2)) + 1]);
                        primitive.IndexArray.Append(list2[((num * ((this._pillarSection.SegCount + 1) * 2)) + (num2 * 2)) + 1]);
                        primitive.IndexArray.Append(list2[(num * ((this._pillarSection.SegCount + 1) * 2)) + ((num2 + 1) * 2)]);
                        primitive.IndexArray.Append(list2[((num + 1) * ((this._pillarSection.SegCount + 1) * 2)) + ((num2 + 1) * 2)]);
                    }
                }
                SystemLog.Instance.Log("结束添加索引数组:" + DateTime.Now.ToLongTimeString());
                SystemLog.Instance.Log("开始画底面顶面和线框:" + DateTime.Now.ToLongTimeString());
                vArray = new DoubleArrayClass();
                indexArray = new UInt16ArrayClass();
                array3 = new FloatArrayClass();
                norms = new DoubleArrayClass();
                primitive2 = group.GetPrimitive(1);
                polygon.ExteriorRing.ReverseOrientation();
                mesh = DrawGeometry.geoConvertor.PolygonToTriMesh(polygon);
                if ((mesh != null) && mesh.BatchExport(ref vArray, ref indexArray, ref array3, ref array4, ref norms))
                {
                    for (num = 0; num < vArray.Length; num++)
                    {
                        primitive2.VertexArray.Append((float)vArray.Array[num]);
                    }
                    for (num = 0; num < indexArray.Length; num++)
                    {
                        primitive2.IndexArray.Append(indexArray.Array[num]);
                    }
                    for (num = 0; num < norms.Length; num++)
                    {
                        primitive2.NormalArray.Append((float)norms.Array[num]);
                    }
                    if (this._renderType == RenderType.Texture)
                    {
                        for (num = 0; num < (vArray.Length / 3); num++)
                        {
                            this.GetTexcoord(vArray.Array[num * 3], vArray.Array[(num * 3) + 1], out naN, out ty);
                            primitive2.TexcoordArray.Append((float)naN);
                            primitive2.TexcoordArray.Append((float)ty);
                        }
                    }
                }
                vArray = new DoubleArrayClass();
                indexArray = new UInt16ArrayClass();
                array3 = new FloatArrayClass();
                norms = new DoubleArrayClass();
                primitive3 = group.GetPrimitive(2);
                mesh = DrawGeometry.geoConvertor.PolygonToTriMesh(polygon2);
                if ((mesh != null) && mesh.BatchExport(ref vArray, ref indexArray, ref array3, ref array4, ref norms))
                {
                    for (num = 0; num < vArray.Length; num++)
                    {
                        primitive3.VertexArray.Append((float)vArray.Array[num]);
                    }
                    for (num = 0; num < indexArray.Length; num++)
                    {
                        primitive3.IndexArray.Append(indexArray.Array[num]);
                    }
                    for (num = 0; num < norms.Length; num++)
                    {
                        primitive3.NormalArray.Append((float)norms.Array[num]);
                    }
                    if (this._renderType == RenderType.Texture)
                    {
                        for (num = 0; num < (vArray.Length / 3); num++)
                        {
                            this.GetTexcoord(vArray.Array[num * 3], vArray.Array[(num * 3) + 1], out naN, out ty);
                            primitive3.TexcoordArray.Append((float)naN);
                            primitive3.TexcoordArray.Append((float)ty);
                        }
                    }
                }
                SystemLog.Instance.Log("结束画底面顶面和线框:" + DateTime.Now.ToLongTimeString());
                SystemLog.Instance.Log("结束调用Draw:" + DateTime.Now.ToLongTimeString());
                return true;
            }
            catch (Exception exception)
            {
                SystemLog.Instance.Log(exception);
                return false;
            }
        }

        public bool DrawModel(out GvitechModelPoint mp, GvitechModel fmodel, GvitechModel smodel)
        {
            mp = null;
            fmodel = null;
            smodel = null;
            IModelPoint point = null;
            IModel model = null;
            IModel model2 = null;
            if (!this.Draw(out point, out model, out model2))
            {
                return false;
            }
            mp = new GvitechModelPoint(point);
            fmodel = (model == null) ? null : new GvitechModel(model);
            smodel = (model2 == null) ? null : new GvitechModel(model2);
            return true;
        }

        protected void GetTexcoord(double x, double y, out double tx, out double ty)
        {
            tx = (x - this._minX) / 10.0;
            ty = (y - this._minY) / 10.0;
        }

        public virtual void SetColorRender(uint[] colors)
        {
            this._renderType = RenderType.Color;
            this._colors = colors;
        }

        public void SetParameter(IMultiPolygon sSection, IMultiPolygon eSection, IPolyline polyline)
        {
            try
            {
                base._cullModel = gviCullFaceMode.gviCullNone;
                this.SetColorRender(new uint[] { uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue });
                if ((sSection != null) && (eSection != null))
                {
                    IPolygon geometry = sSection.GetGeometry(0) as IPolygon;
                    if (geometry != null)
                    {
                        if (geometry.QueryNormal().Z < 0.0)
                        {
                            GeometryTool.ReversePolygon(ref geometry);
                        }
                        this._pillarSection = new PillarSection(geometry);
                        DrawGeometry.GetPolylineVtxs(polyline, ref this._x, ref this._y, ref this._z, ref this._route);
                    }
                }
            }
            catch (Exception exception)
            {
                SystemLog.Instance.Log(exception.StackTrace);
            }
        }

        public virtual void SetTextureRender(string[] tcNames)
        {
            this._renderType = RenderType.Texture;
            this._tcNames = tcNames;
        }

        // Properties
        public uint[] Colors
        {
            get
            {
                return this._colors;
            }
            set
            {
                this._colors = value;
            }
        }

        public RenderType RenderType
        {
            get
            {
                return this._renderType;
            }
            set
            {
                this._renderType = value;
            }
        }

        public string[] TextureNames
        {
            get
            {
                return this._tcNames;
            }
            set
            {
                this._tcNames = value;
            }
        }
    }

 

}
