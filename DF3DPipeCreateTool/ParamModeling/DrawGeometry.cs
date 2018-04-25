using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawGeometry : IDrawGeometry
    {
        // Fields
        protected gviCullFaceMode _cullModel = gviCullFaceMode.gviCullBack;              // 面剔除模式
        protected string _modelname = Guid.NewGuid().ToString();                         // 模型名称
        protected ModelType _modeltype;                                                  // 模型类型
        private static AxRenderControl _ocx;                                             // 三维窗口
        protected double _rotateX = 0.0;                                                 // 选择轴在右手坐标系中在X方向上的偏角
        protected double _rotateY = 0.0;                                                 // 选择轴在右手坐标系中在Y方向上的偏角
        protected double _rotateZ = 0.0;                                                 // 选择轴在右手坐标系中在Z方向上的偏角
        protected double _scaleX = 1.0;                                                  // X轴上的缩放比例
        protected double _scaleY = 1.0;                                                  // Y轴上的缩放比例
        protected double _scaleZ = 1.0;                                                  // Z轴上的缩放比例
        protected uint _specularColor = uint.MaxValue;
        protected double _x = 0.0;                                                       // 几何对象起点X坐标
        protected double _y = 0.0;                                                       // 几何对象起点Y坐标
        protected double _z = 0.0;                                                       // 几何对象起点Z坐标
        public static IGeometryConvertor geoConvertor = new GeometryConvertorClass();
        public static IGeometryFactory geoFactory = new GeometryFactoryClass();
        public const double MaxValue = 999999.99;
        public static double maxVisibleDis = 5000000.0;
        public const double MinValue = -999999.99;
        public static ushort PipeSegments = 12;
        public static IResourceFactory resFactory = new ResourceFactoryClass();
        public static ushort SimpleSegments = 6;
        public static int SwitchSize = 0x18;
        public static List<IRObject> tmpList = new List<IRObject>();
        public const double Zero = 0.08;

        // Methods
        public static void Clear()
        {
            if (tmpList.Count > 0)
            {
                foreach (IRObject obj2 in tmpList)
                {
                    Ocx.ObjectManager.DeleteObject(obj2.Guid);
                }
                tmpList.Clear();
            }
        }

        #region 两对象比较
        public static bool Compare(double val1, double val2)
        {
            return (Math.Abs((double)(val1 - val2)) < 0.08);
        }
        public static bool Compare(Vector v1, Vector v2, bool ignoreZ)
        {
            if ((v1 == null) || (v2 == null))
            {
                return false;
            }
            if (ignoreZ)
            {
                return ((Math.Abs((double)(v1.X - v2.X)) < 0.08) && (Math.Abs((double)(v1.Y - v2.Y)) < 0.08));
            }
            // FX 2014.04.01
            return (((Math.Abs((double)(v1.X - v2.X)) < 0.08) && (Math.Abs((double)(v1.Y - v2.Y)) < 0.08)) && (Math.Abs((double)(v1.Z - v2.Z)) < 0.4));
            //return (((Math.Abs((double)(v1.X - v2.X)) < 0.08) && (Math.Abs((double)(v1.Y - v2.Y)) < 0.08)) && (Math.Abs((double)(v1.Z - v2.Z)) < 0.08));
        }
        public static bool Compare(IPoint p1, IPoint p2, bool ignoreZ)
        {
            if ((p1 == null) || (p2 == null))
            {
                return false;
            }
            if (ignoreZ)
            {
                return ((Math.Abs((double)(p1.X - p2.X)) < 0.08) && (Math.Abs((double)(p1.Y - p2.Y)) < 0.08));
            }
            return (((Math.Abs((double)(p1.X - p2.X)) < 0.08) && (Math.Abs((double)(p1.Y - p2.Y)) < 0.08)) && (Math.Abs((double)(p1.Z - p2.Z)) < 0.08));
        }
        public static bool Compare(double x1, double y1, double x2, double y2)
        {
            return ((Math.Abs((double)(x1 - x2)) < 0.08) && (Math.Abs((double)(y1 - y2)) < 0.08));
        }
        public static bool Compare(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return (((Math.Abs((double)(x1 - x2)) < 0.08) && (Math.Abs((double)(y1 - y2)) < 0.08)) && (Math.Abs((double)(z1 - z2)) < 0.08));
        }
        #endregion

        public static bool ConvertPolygon(double[] vtx, double height, int flag, out IDoubleArray VArray, out IUInt16Array IndexArray, out IFloatArray TextureArrayU1V1, out IDoubleArray Norms)
        {
            bool flag2;
            VArray = new DoubleArrayClass();
            IndexArray = new UInt16ArrayClass();
            TextureArrayU1V1 = new FloatArrayClass();
            IFloatArray array = null;
            Norms = new DoubleArrayClass();
            IPolygon polygon = null;
            ITriMesh o = null;
            try
            {
                if (((vtx == null) || ((vtx.Length % 2) != 0)) || (vtx.Length < 8))
                {
                    return false;
                }
                polygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                if (polygon == null)
                {
                    return false;
                }
                int num = vtx.Length / 2;
                for (int i = 0; i < num; i++)
                {
                    IPoint pointValue = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    if (flag == 1)
                    {
                        pointValue.X = vtx[i * 2];
                        pointValue.Y = vtx[(i * 2) + 1];
                        pointValue.Z = height;
                    }
                    else
                    {
                        pointValue.X = vtx[((num - i) - 1) * 2];
                        pointValue.Y = vtx[(((num - i) - 1) * 2) + 1];
                        pointValue.Z = height;
                    }
                    polygon.ExteriorRing.AppendPoint(pointValue);
                }
                if (!polygon.IsClosed)
                {
                    polygon.Close();
                }
                o = geoConvertor.PolygonToTriMesh(polygon);
                if (o == null)
                {
                    return false;
                }
                if (!o.BatchExport(ref VArray, ref IndexArray, ref TextureArrayU1V1, ref array, ref Norms))
                {
                    return false;
                }
                flag2 = true;
            }
            catch (Exception)
            {
                flag2 = false;
            }
            finally
            {
                if (polygon != null)
                {
                    Marshal.ReleaseComObject(polygon);
                }
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                }
            }
            return flag2;
        }

        #region 默认截面
        public static IPipeSection DefaultSection(SecShape secShape)
        {
            switch (secShape)
            {
                case SecShape.CircleRing:
                    return new PipeSection(0.1, 0.0, HorizontalPos.Center, VerticalPos.Center, 0.01, 0);

                case SecShape.RectangleRing:
                    return new PipeSection(0.15, 0.12, HorizontalPos.Center, VerticalPos.Center, 0.01, 0);
            }
            return null;
        }
        #endregion

        #region 删除文件
        public static void deleteFiles(string strDir)
        {
            if (!Directory.Exists(strDir))
            {
                Directory.Delete(strDir, true);
                Directory.CreateDirectory(strDir);
            }
        }
        public static void deleteFiles3(string strDir)
        {
            if (Directory.Exists(strDir))
            {
                string[] directories = Directory.GetDirectories(strDir);
                foreach (string str in Directory.GetFiles(strDir))
                {
                    File.Delete(str);
                }
                foreach (string str2 in directories)
                {
                    Directory.Delete(str2, true);
                }
            }
        }
        #endregion

        #region 绘制三维模型
        public virtual bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            mp = this.GetModelPoint();
            fmodel = null;
            smodel = null;
            return true;
        }
        // 绘制架空模型 FX 2014.04.01
        public virtual bool DrawOver(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            mp = this.GetModelPoint();
            fmodel = null;
            smodel = null;
            return true;
        }
        #endregion

        #region 获取模型定位点\几何对象坐标序列(以某一点坐标为基点)
        public virtual IModelPoint GetModelPoint()
        {
            IModelPoint point = geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
            if (point == null)
            {
                return null;
            }
            point.ModelName = this._modelname;
            point.SetCoords(this._x, this._y, this._z, double.NaN, -1);
            point.SelfScale(this._scaleX, this._scaleY, this._scaleZ);
            point.SelfRotate(1.0, 0.0, 0.0, this._rotateX);
            point.SelfRotate(0.0, 1.0, 0.0, this._rotateY);
            point.SelfRotate(0.0, 0.0, 1.0, this._rotateZ);
            return point;
        }
        public static bool GetPolygonVtxs(IPolygon polygon, ref double cX, ref double cY, ref double[] vtxs)
        {
            if (polygon == null)
            {
                return false;
            }
            cX = (polygon.Envelope.MaxX + polygon.Envelope.MinX) / 2.0;
            cY = (polygon.Envelope.MaxY + polygon.Envelope.MinY) / 2.0;
            vtxs = new double[polygon.ExteriorRing.PointCount * 2];
            for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
            {
                IPoint point = polygon.ExteriorRing.GetPoint(i);
                vtxs[i * 2] = point.X - cX;
                vtxs[(i * 2) + 1] = point.Y - cY;
            }
            return true;
        }
        public static bool GetPolygonVtxs(IPolygon polygon, ref double cX, ref double cY, ref double[] vtxs, ref double minX, ref double maxX, ref double minY, ref double maxY)
        {
            if (polygon == null)
            {
                return false;
            }
            double naN = double.NaN;
            cX = (polygon.Envelope.MaxX + polygon.Envelope.MinX) / 2.0;
            cY = (polygon.Envelope.MaxY + polygon.Envelope.MinY) / 2.0;
            vtxs = new double[polygon.ExteriorRing.PointCount * 2];
            for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
            {
                IPoint point = polygon.ExteriorRing.GetPoint(i);
                naN = point.X - cX;
                minX = Math.Min(naN, minX);
                maxX = Math.Max(naN, maxX);
                vtxs[i * 2] = naN;
                naN = point.Y - cY;
                minY = Math.Min(naN, minX);
                maxY = Math.Max(naN, maxX);
                vtxs[(i * 2) + 1] = naN;
            }
            return true;
        }
        public static bool GetPolylineVtxs(IPolyline polyline, ref double cX, ref double cY, ref double cZ, ref List<Vector> route)
        {
            if (polyline == null)
            {
                return false;
            }
            cX = polyline.StartPoint.X;
            cY = polyline.StartPoint.Y;
            cZ = polyline.StartPoint.Z;
            route = new List<Vector>();
            for (int i = 0; i < polyline.PointCount; i++)
            {
                IPoint point = polyline.GetPoint(i);
                Vector item = new Vector(point.X - cX, point.Y - cY, point.Z - cZ);
                route.Add(item);
            }
            return true;
        }
        public static bool GetRoundVtxs(IPoint point, double radius, int nSegments, ref double[] vtxs)
        {
            if (point == null)
            {
                return false;
            }
            double naN = double.NaN;
            vtxs = new double[(nSegments + 1) * 2];
            double num2 = 6.2831853071795862 / ((double)nSegments);
            for (int i = 0; i <= nSegments; i++)
            {
                naN = radius * Math.Sin(num2 * i);
                vtxs[i * 2] = naN;
                naN = radius * Math.Cos(num2 * i);
                vtxs[(i * 2) + 1] = naN;
            }
            return true;
        }
        public static bool GetRoundVtxs(IPoint point, double radius, int nSegments, ref double[] vtxs, ref double minX, ref double maxX, ref double minY, ref double maxY)
        {
            if (point == null)
            {
                return false;
            }
            double naN = double.NaN;
            vtxs = new double[(nSegments + 1) * 2];
            double num2 = 6.2831853071795862 / ((double)nSegments);
            for (int i = 0; i <= nSegments; i++)
            {
                naN = radius * Math.Sin(num2 * i);
                minX = Math.Min(naN, minX);
                maxX = Math.Max(naN, maxX);
                vtxs[i * 2] = naN;
                naN = radius * Math.Cos(num2 * i);
                minY = Math.Min(naN, minX);
                maxY = Math.Max(naN, maxX);
                vtxs[(i * 2) + 1] = naN;
            }
            return true;
        }
        #endregion

        #region 合并模型
        //public static GvitechModel MergeModels(GvitechModel[] arrModel, GvitechVector3[] arrOff)
        //{
        //    if (((arrModel == null) || (arrOff == null)) || (arrModel.Length != arrOff.Length))
        //    {
        //        return null;
        //    }
        //    int length = arrModel.Length;
        //    IModel[] modelArray = new IModel[length];
        //    IVector3[] vectorArray = new IVector3[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        modelArray[i] = ((arrModel[i] == null) || arrModel[i].IsNull) ? null : arrModel[i].Model;
        //        vectorArray[i] = ((arrOff[i] == null) || arrOff[i].IsNull) ? null : arrOff[i].Vector3;
        //    }
        //    return new GvitechModel(MergeModels(modelArray, vectorArray));
        //}
        public static IModel MergeModels(IModel[] arrModel, IVector3[] arrOff)
        {
            IModel model = null;
            try
            {
                IModel model2 = null;
                IDrawGroup drawGroup = null;
                IDrawPrimitive primitive = null;
                IVector3 vector = null;
                model = resFactory.CreateModel();
                for (int i = 0; i < arrModel.Length; i++)
                {
                    if (((model2 = arrModel[i]) != null) && (model2.GroupCount != 0))
                    {
                        for (int j = 0; j < model2.GroupCount; j++)
                        {
                            drawGroup = model2.GetGroup(j);
                            if ((drawGroup != null) && (drawGroup.PrimitiveCount != 0))
                            {
                                for (int k = 0; k < drawGroup.PrimitiveCount; k++)
                                {
                                    if (((vector = arrOff[i]) != null) && (((arrOff[i].X != 0.0) || (arrOff[i].Y != 0.0)) || (arrOff[i].Z != 0.0)))
                                    {
                                        uint num;
                                        primitive = drawGroup.GetPrimitive(k);
                                        if ((primitive != null) && ((num = primitive.VertexArray.Length) != 0))
                                        {
                                            int num2 = (int)(num / 3);
                                            for (int m = 0; m < num2; m++)
                                            {
                                                primitive.VertexArray.Set(m * 3, primitive.VertexArray.Array[m * 3] + ((float)vector.X));
                                                primitive.VertexArray.Set((m * 3) + 1, primitive.VertexArray.Array[(m * 3) + 1] + ((float)vector.Y));
                                                primitive.VertexArray.Set((m * 3) + 2, primitive.VertexArray.Array[(m * 3) + 2] + ((float)vector.Z));
                                            }
                                        }
                                    }
                                }
                                model.AddGroup(drawGroup);
                            }
                        }
                    }
                }
                return model;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        #endregion

        #region 新建模型
        public static ICurveSymbol NewCvSymbol(uint color)
        {
            ICurveSymbol symbol = (CurveSymbol)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("E02C69C4-828D-40D5-869D-DAEB189B7F6F")));
            symbol.Color = color;
            return symbol;
        }
        // 新建空模型
        public bool NewEmptyModel(int[] index, RenderType renderType, object renderInfo, out IModel model)
        {
            model = null;
            if (index.Length == 0)
            {
                return false;
            }
            string[] strArray = null;
            uint[] numArray = null;
            if (renderType == RenderType.Texture)
            {
                strArray = renderInfo as string[];
                if ((strArray == null) || (strArray.Length < index.Length))
                {
                    return false;
                }
            }
            else
            {
                numArray = renderInfo as uint[];
                if ((numArray == null) || (numArray.Length < index.Length))
                {
                    return false;
                }
            }
            IDrawGroup drawGroup = null;
            IDrawPrimitive primitive = null;
            IDrawMaterial material = null;
            IResourceFactory factory = new ResourceFactoryClass();
            model = factory.CreateModel();
            model.SwitchSize = SwitchSize;
            drawGroup = new DrawGroupClass();
            for (int i = 0; i < index.Length; i++)
            {
                material = new DrawMaterialClass
                {
                    CullMode = this._cullModel,
                    EnableBlend = false,
                    EnableLight = true,
                    SpecularColor = this._specularColor,
                    WrapModeS = gviTextureWrapMode.gviTextureWrapRepeat,
                    WrapModeT = gviTextureWrapMode.gviTextureWrapRepeat
                };
                primitive = new DrawPrimitiveClass
                {
                    PrimitiveMode = gviPrimitiveMode.gviPrimitiveModeTriangleList,
                    PrimitiveType = gviPrimitiveType.gviPrimitiveNormal,
                    VertexArray = new FloatArrayClass(),
                    IndexArray = new UInt16ArrayClass(),
                    NormalArray = new FloatArrayClass()
                };
                if (renderType == RenderType.Texture)
                {
                    material.TextureName = strArray[index[i]];
                    primitive.TexcoordArray = new FloatArrayClass();
                    material.DiffuseColor = uint.MaxValue;
                }
                else
                {
                    material.TextureName = "";
                    primitive.TexcoordArray = null;
                    material.DiffuseColor = numArray[index[i]];
                }
                primitive.Material = material;
                drawGroup.AddPrimitive(primitive);
            }
            model.AddGroup(drawGroup);
            return true;
        }
        public static ISimplePointSymbol NewPtSymbol(uint color)
        {
            ISimplePointSymbol symbol = (SimplePointSymbol)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("6669A2D9-BA75-4B24-BF75-D1120B790AE9")));
            symbol.FillColor = color;
            symbol.Alignment = gviPivotAlignment.gviPivotAlignCenterCenter;
            symbol.Style = gviSimplePointStyle.gviSimplePointX;
            return symbol;
        }
        #endregion

        #region 测试绘制模型\几何对象
        public static void TestDrawModel(IModelPoint mp, IModel model, List<string> filesPath)
        {
            if ((mp != null) && (model != null))
            {
                string str = Application.StartupPath + @"\..\temp";
                string str2 = Path.Combine(str, "osg");
                string filePath = string.Format(@"{0}\{1}.osg", str2, mp.ModelName);
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }
                if (!Directory.Exists(str2))
                {
                    Directory.CreateDirectory(str2);
                }
                IPropertySet images = null;
                IImage image = null;
                string key = string.Empty;
                images = new PropertySetClass();
                if (filesPath != null)
                {
                    foreach (string str5 in filesPath)
                    {
                        if (File.Exists(str5) || Directory.Exists(str5))
                        {
                            key = Path.GetFileNameWithoutExtension(str5);
                            image = resFactory.CreateImageFromFile(str5);
                            images.SetProperty(key, image);
                        }
                    }
                }
                model.WriteFile(filePath, images);
                IModelPointSymbol symbol = (ModelPointSymbol)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("6BCF8C9B-E506-43AE-AD6E-44A41D748431")));
                symbol.Color = 0xaaaaaaaa;
                mp.ModelName = filePath;
                mp.ModelEnvelope = model.Envelope;
                IRenderModelPoint item = Ocx.ObjectManager.CreateRenderModelPoint(mp, symbol, Ocx.ProjectTree.RootID);
                item.MaxVisibleDistance = 500000.0;
                tmpList.Add(item);
                Ocx.Camera.LookAtEnvelope(item.Envelope);
            }
        }
        public static void TestDrawPoint(double offX, double offY, double offZ, double x, double y, double z, IPointSymbol pSymbol)
        {
            IPoint point = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            point.X = offX + x;
            point.Y = offY + y;
            point.Z = offZ + z;
            IRenderPoint item = Ocx.ObjectManager.CreateRenderPoint(point, pSymbol, Ocx.ProjectTree.RootID);
            item.MaxVisibleDistance = maxVisibleDis;
            tmpList.Add(item);
        }
        public static void TestDrawPolygon(double offX, double offY, double offZ, double[] vtxs, ISurfaceSymbol sfSymbol)
        {
            if ((vtxs.Length >= 9) && ((vtxs.Length % 3) == 0))
            {
                IPolygon polygon = null;
                IPoint pointValue = null;
                polygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                for (int i = 0; i < (vtxs.Length / 3); i++)
                {
                    pointValue = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    pointValue.X = offX + vtxs[i * 3];
                    pointValue.Y = offY + vtxs[(i * 3) + 1];
                    pointValue.Z = offZ + vtxs[(i * 3) + 2];
                    polygon.ExteriorRing.AppendPoint(pointValue);
                }
                if (!polygon.IsClosed)
                {
                    polygon.Close();
                }
                IRenderPolygon item = Ocx.ObjectManager.CreateRenderPolygon(polygon, sfSymbol, Ocx.ProjectTree.RootID);
                item.MaxVisibleDistance = maxVisibleDis;
                tmpList.Add(item);
            }
        }
        public static void TestDrawPolyline(double offX, double offY, double offZ, List<Vector> vLines, ICurveSymbol cvSymbol)
        {
            IPolyline polyline = null;
            IPoint pointValue = null;
            polyline = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            foreach (Vector vector in vLines)
            {
                pointValue = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointValue.X = offX + vector.X;
                pointValue.Y = offY + vector.Y;
                pointValue.Z = offZ + vector.Z;
                polyline.AppendPoint(pointValue);
            }
            IRenderPolyline item = Ocx.ObjectManager.CreateRenderPolyline(polyline, cvSymbol, Ocx.ProjectTree.RootID);
            item.MaxVisibleDistance = maxVisibleDis;
            tmpList.Add(item);
        }
        public static void TestDrawTriangle(double offX, double offY, double offZ, double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3, ISurfaceSymbol sfSymbol)
        {
            IPolygon polygon = null;
            IPoint pointValue = null;
            polygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            pointValue = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            pointValue.X = offX + x1;
            pointValue.Y = offY + y1;
            pointValue.Z = offZ + z1;
            polygon.ExteriorRing.AppendPoint(pointValue);
            pointValue.X = offX + x2;
            pointValue.Y = offY + y2;
            pointValue.Z = offZ + z2;
            polygon.ExteriorRing.AppendPoint(pointValue);
            pointValue.X = offX + x3;
            pointValue.Y = offY + y3;
            pointValue.Z = offZ + z3;
            polygon.ExteriorRing.AppendPoint(pointValue);
            polygon.Close();
            IRenderPolygon item = Ocx.ObjectManager.CreateRenderPolygon(polygon, sfSymbol, Ocx.ProjectTree.RootID);
            item.MaxVisibleDistance = maxVisibleDis;
            tmpList.Add(item);
        }
        public static void TestDrawTriMesh(IModelPoint mp, IModel model)
        {
            ISurfaceSymbol symbol = (SurfaceSymbol)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("4D5F0624-50A1-43E2-A0EC-A9713CB25608")));
            symbol.Color = 0xff0000ff;
            ICurveSymbol symbol2 = (CurveSymbol)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("E02C69C4-828D-40D5-869D-DAEB189B7F6F")));
            symbol2.Color = 0xffff0000;
            symbol.BoundarySymbol = symbol2;
            IMultiPolygon multiPolygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
            if (multiPolygon != null)
            {
                IPolygon geometry = null;
                IPoint pointValue = null;
                for (int i = 0; i < model.GroupCount; i++)
                {
                    IDrawGroup group = model.GetGroup(i);
                    for (int j = 0; j < group.PrimitiveCount; j++)
                    {
                        IDrawPrimitive primitive = group.GetPrimitive(j);
                        for (int k = 0; k < (primitive.IndexArray.Length / 3); k++)
                        {
                            geometry = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                            pointValue = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pointValue.X = mp.X + primitive.VertexArray.Array[primitive.IndexArray.Array[k * 3] * 3];
                            pointValue.Y = mp.Y + primitive.VertexArray.Array[(primitive.IndexArray.Array[k * 3] * 3) + 1];
                            pointValue.Z = mp.Z + primitive.VertexArray.Array[(primitive.IndexArray.Array[k * 3] * 3) + 2];
                            geometry.ExteriorRing.AppendPoint(pointValue);
                            pointValue = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pointValue.X = mp.X + primitive.VertexArray.Array[primitive.IndexArray.Array[(k * 3) + 1] * 3];
                            pointValue.Y = mp.Y + primitive.VertexArray.Array[(primitive.IndexArray.Array[(k * 3) + 1] * 3) + 1];
                            pointValue.Z = mp.Z + primitive.VertexArray.Array[(primitive.IndexArray.Array[(k * 3) + 1] * 3) + 2];
                            geometry.ExteriorRing.AppendPoint(pointValue);
                            pointValue = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pointValue.X = mp.X + primitive.VertexArray.Array[primitive.IndexArray.Array[(k * 3) + 2] * 3];
                            pointValue.Y = mp.Y + primitive.VertexArray.Array[(primitive.IndexArray.Array[(k * 3) + 2] * 3) + 1];
                            pointValue.Z = mp.Z + primitive.VertexArray.Array[(primitive.IndexArray.Array[(k * 3) + 2] * 3) + 2];
                            geometry.ExteriorRing.AppendPoint(pointValue);
                            geometry.Close();
                            multiPolygon.AddGeometry(geometry);
                        }
                    }
                }
                IRenderMultiPolygon item = Ocx.ObjectManager.CreateRenderMultiPolygon(multiPolygon, symbol, Ocx.ProjectTree.RootID);
                item.MaxVisibleDistance = 5000000.0;
                tmpList.Add(item);
            }
        }
        #endregion

        #region 写日志
        public static void WriteLog(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        public static void WriteLog(string mes)
        {
            Console.WriteLine(mes);
        }
        #endregion

        // Properties
        public gviCullFaceMode CullModel
        {
            get
            {
                return this._cullModel;
            }
            set
            {
                this._cullModel = value;
            }
        }

        public string ModelName
        {
            get
            {
                return this._modelname;
            }
            set
            {
                this._modelname = value;
            }
        }

        public ModelType ModelType
        {
            get
            {
                return this._modeltype;
            }
        }

        public static AxRenderControl Ocx
        {
            get
            {
                return _ocx;
            }
            set
            {
                _ocx = value;
            }
        }

        public double RotateX
        {
            get
            {
                return this._rotateX;
            }
        }

        public double RotateY
        {
            get
            {
                return this._rotateY;
            }
        }

        public double RotateZ
        {
            get
            {
                return this._rotateZ;
            }
        }

        public double ScaleX
        {
            get
            {
                return this._scaleX;
            }
        }

        public double ScaleY
        {
            get
            {
                return this._scaleY;
            }
        }

        public double ScaleZ
        {
            get
            {
                return this._scaleZ;
            }
        }

        public uint SpecularColor
        {
            get
            {
                return this._specularColor;
            }
            set
            {
                this._specularColor = value;
            }
        }

        public double X
        {
            get
            {
                return this.X;
            }
        }

        public double Y
        {
            get
            {
                return this.Y;
            }
        }

        public double Z
        {
            get
            {
                return this.Z;
            }
        }
    }
}
