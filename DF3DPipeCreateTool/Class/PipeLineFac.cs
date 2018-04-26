using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.FdeGeometry;
using DF3DPipeCreateTool.ParamModeling;
using DFDataConfig.Class;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.RenderControl;
using System.Windows.Forms;
using System.IO;

namespace DF3DPipeCreateTool.Class
{
    public class PipeLineFac:Fac
    {
        protected TopoClass _tc;
        private bool _isSBackhind = true;                      // 起点退让        
        private bool _isEBackhind = true;                      // 终点退让        
        private double _dia1;                                  // 管径1
        private double _dia2;                                  // 管径2
        private double _sdeep;                                 // 起点埋深
        private double _sheight;                               // 起点高程
        private double _ssurfh;                                // 起点地面高程
        private double _edeep;                                 // 终点埋深
        private double _eheight;                               // 终点高程
        private double _esurfh;                                // 终点地面高程
        private double _thick;                                 // 管壁厚度
        private SegType _segtype;                              // 管线形状
        private VerticalPos _vPos;
        private int _tag;                                      // 管点在管线的位置索引-起点\终点
        public int Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                this._tag = value;
            }
        }
        public double OffsetZ
        {
            get
            {
                switch (this._vPos)
                {
                    case VerticalPos.Top:
                        return (-((this._segtype == SegType.Round) ? this._dia1 : this._dia2) / 2.0);

                    case VerticalPos.Bottom:
                        return (((this._segtype == SegType.Round) ? this._dia1 : this._dia2) / 2.0);
                }
                return 0.0;
            }
        }
        public bool IsSBackhind
        {
            get
            {
                return this._isSBackhind;
            }
            set
            {
                this._isSBackhind = value;
            }
        }

        public bool IsEBackhind
        {
            get
            {
                return this._isEBackhind;
            }
            set
            {
                this._isEBackhind = value;
            }
        }
        public PipeLineFac(FacClassReg facClassReg, FacStyleClass style, IRowBuffer rowInfo, TopoClass tc, bool isSBackhind = true, bool isEBackhind = true)
            : base(facClassReg, style, rowInfo)
        {
            this._tc = tc;
            this._isSBackhind = isSBackhind;
            this._isEBackhind = isEBackhind;
            this.Init();
        }

        #region 管径拆分
        private void SplitDiameter(string diaInfo, string coverstsle, out double dia1, out double dia2)
        {
            dia1 = 0.0;
            dia2 = 0.0;
            if (string.IsNullOrEmpty(diaInfo))
            {
                return;
            }
            try
            {
                if (diaInfo.Contains("+"))
                {
                    List<string> stdValues = new List<string>();
                    string[] stdValues1 = diaInfo.Split(new char[] { '+' });
                    for (int j = 0; j < stdValues1.Length; j++)
                    {
                        string[] stdValues2 = stdValues1[j].Split(Convert.ToChar("*"));
                        for (int k = 0; k < stdValues2.Length; k++)
                            stdValues.Add(stdValues2[k]);
                    }
                    double stdMax = Convert.ToDouble(stdValues[0]);
                    for (int j = 1; j < stdValues.Count; j++)
                    {
                        double std1 = Convert.ToDouble(stdValues[j]);
                        if (std1 > stdMax)
                            stdMax = std1;
                    }
                    dia1 = stdMax;
                    dia2 = 0.0;
                }
                else
                {
                    if (diaInfo.Contains("*"))
                    {
                        string[] stdValues = diaInfo.Split(Convert.ToChar("*"));
                        if (stdValues.Length > 2)
                        {
                            double stdMax = Convert.ToDouble(stdValues[0]);
                            for (int j = 1; j < stdValues.Length; j++)
                            {
                                double std1 = Convert.ToDouble(stdValues[j]);
                                if (std1 > stdMax)
                                {
                                    stdMax = std1;
                                }
                            }
                            dia1 = stdMax;
                            dia2 = 0.0;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(coverstsle))
                            {
                                if (coverstsle.Equals("3"))
                                {
                                    double num1, num2;
                                    num1 = Convert.ToDouble(stdValues[1]);
                                    num2 = Convert.ToDouble(stdValues[0]);
                                    dia1 = num1 > num2 ? num1 : num2;
                                    dia2 = 0.0;
                                }
                                else
                                {
                                    double num;
                                    dia1 = (double.TryParse(stdValues[0], out num) ? num : 0.0);
                                    dia2 = (double.TryParse(stdValues[1], out num) ? num : 0.0);
                                }
                            }
                            else
                            {
                                double num;
                                dia1 = (double.TryParse(stdValues[1], out num) ? num : 0.0);
                                dia2 = (double.TryParse(stdValues[0], out num) ? num : 0.0);
                            }
                        }
                    }
                    else
                    {
                        double num;
                        dia1 = (double.TryParse(diaInfo, out num) ? num : 0.0);
                        dia2 = 0.0;
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }
        #endregion

        #region 获取管线形状
        private SegType GetSegType(double dia1, double dia2)
        {
            if ((dia1 > 0.0015) && (dia2 < 0.0015))
            {
                return SegType.Round;
            }
            if ((dia1 > 0.0015) && (dia2 > 0.0015))
            {
                return SegType.Rectangle;
            }
            return SegType.UnKnown;
        }
        #endregion

        #region 获取管线对象顶点坐标序列,以多段线形式返回
        // Shap数据中含各顶点高程数据
        private bool GetPipeLineVertexs(IGeometry geo, double sHeight, double eHeight, out IPolyline route)
        {
            route = null;
            try
            {
                IPolyline polyline = null;
                if ((geo == null) || ((polyline = geo as IPolyline) == null))
                {
                    return false;
                }
                IPoint pointValue = null;
                route = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                if (polyline.PointCount == 2)
                {
                    pointValue = polyline.StartPoint.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                    pointValue.Z = sHeight;
                    route.AppendPoint(pointValue);
                    pointValue = polyline.EndPoint.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                    pointValue.Z = eHeight;
                    route.AppendPoint(pointValue);
                }
                else
                {
                    IPoint point;
                    Stack<int> stack = new Stack<int>();
                    double num4 = eHeight - sHeight;
                    if (Math.Abs(num4) < 0.0015)
                    {
                        point = null;
                        for (int i = 0; i < polyline.PointCount; i++)
                        {
                            pointValue = polyline.GetPoint(i).Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                            if (i < 1)
                            {
                                pointValue.Z = sHeight;
                                route.AppendPoint(pointValue);
                                point = pointValue;
                            }
                            else if (Math.Sqrt(((pointValue.X - point.X) * (pointValue.X - point.X)) + ((pointValue.Y - point.Y) * (pointValue.Y - point.Y))) < (this._dia1 * 1.5))
                            {
                                stack.Push(i);
                            }
                            else
                            {
                                pointValue.Z = sHeight;
                                route.AppendPoint(pointValue);
                                point = pointValue;
                            }
                        }
                    }
                    else
                    {
                        double num3 = sHeight;
                        IPolyline o = polyline.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline;
                        double length = o.Length;
                        Marshal.ReleaseComObject(o);
                        point = null;
                        for (int j = 0; j < polyline.PointCount; j++)
                        {
                            pointValue = polyline.GetPoint(j).Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                            if (j < 1)
                            {
                                pointValue.Z = sHeight;
                                route.AppendPoint(pointValue);
                                point = pointValue;
                            }
                            else
                            {
                                double num2;
                                if ((num2 = Math.Sqrt(((pointValue.X - point.X) * (pointValue.X - point.X)) + ((pointValue.Y - point.Y) * (pointValue.Y - point.Y)))) < (this._dia1 * 1.5))
                                {
                                    stack.Push(j);
                                }
                                else
                                {
                                    num3 += (num2 / length) * num4;
                                    pointValue.Z = num3;
                                    route.AppendPoint(pointValue);
                                    point = pointValue;
                                }
                            }
                        }
                    }
                    while (stack.Count > 0)
                    {
                        polyline.RemovePoints(stack.Pop(), 1);
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        // 根据管线起始点高程计算管线中间点高程,返回多段线对象
        private bool GetPipeLineVertexs(IGeometry geo, out IPolyline route)
        {
            route = null;
            try
            {
                IPolyline polyline = null;
                if ((geo == null) || ((polyline = geo as IPolyline) == null))
                {
                    return false;
                }
                IPoint pointValue = null;
                route = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                for (int i = 0; i < polyline.PointCount; i++)
                {
                    pointValue = polyline.GetPoint(i).Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                    route.AppendPoint(pointValue);
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        // 抽稀坐标点
        private bool SimplifyPolyline(IGeometry geo, out IPolyline route)
        {
            route = null;
            try
            {
                IPolyline polyline = null;
                if ((geo == null) || ((polyline = geo as IPolyline) == null))
                {
                    return false;
                }
                List<Vector> list = new List<Vector>();
                IPoint p = null;
                IPoint point = null;
                route = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                int num2 = 0;
                IPoint pointValue = null;
                Vector vector = null;
                Vector vector2 = null;
                Vector vector3 = null;
                Vector vector4 = null;
                if (polyline.PointCount == 2)
                {
                    list.Add(new Vector(polyline.StartPoint));
                    route.AppendPoint(polyline.StartPoint);
                    list.Add(new Vector(polyline.EndPoint));
                    route.AppendPoint(polyline.EndPoint);
                }
                else
                {
                    new Stack<int>();
                    p = null;
                    for (int i = 0; i < polyline.PointCount; i++)
                    {
                        point = polyline.GetPoint(i);
                        if (i == 0)
                        {
                            //DrawGeometry.Ocx.ObjectManager.CreateRenderPoint(point, null, this._);
                            list.Add(new Vector(point));
                            route.AppendPoint(point);
                            num2 = i;
                            p = point;
                        }
                        else if (Math.Sqrt((((point.X - p.X) * (point.X - p.X)) + ((point.Y - p.Y) * (point.Y - p.Y))) + ((point.Z - p.Z) * (point.Z - p.Z))) < (this._dia1 / 2.0))
                        {
                            p = point;
                        }
                        else
                        {
                            vector2 = new Vector(p);
                            vector4 = new Vector(p.X - point.X, p.Y - point.Y, p.Z - point.Z);
                            if ((num2 > 0) && (num2 == (i - 1)))
                            {
                                list.Add(new Vector(p));
                                route.AppendPoint(p);
                            }
                            else if ((vector != null) && (vector3 != null))
                            {
                                pointValue = this.RadialIntersect(vector, vector3, vector2, vector4);
                                if (pointValue != null)
                                {
                                    route.AppendPoint(pointValue);
                                    list.Add(new Vector(pointValue.X, pointValue.Y, pointValue.Z));
                                }
                                num2 = i;
                            }
                            vector = new Vector(point);
                            vector3 = -vector4;
                            p = point;
                            if (i == (polyline.PointCount - 1))
                            {
                                list.Add(new Vector(point));
                                route.AppendPoint(point);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        public IPoint RadialIntersect(Vector p0, Vector d0, Vector p1, Vector d1)
        {
            try
            {
                IPoint other = DrawGeometry.geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                other.SetCoords(p0.X, p0.Y, p0.Z, 0.0, -1);
                IPolyline polyline = DrawGeometry.geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                IPoint pointValue = DrawGeometry.geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointValue.SetCoords(p1.X, p1.Y, p1.Z, 0.0, -1);
                polyline.AppendPoint(pointValue);
                pointValue = DrawGeometry.geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                Vector vector = p1 + ((Vector)(d1.UnitVector() * 10.0));
                pointValue.SetCoords(vector.X, vector.Y, vector.Z, 0.0, -1);
                polyline.AppendPoint(pointValue);
                return (polyline as IProximityOperator).NearestPoint3D(other);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        #endregion

        #region 获取管点参数
        // 地下—针对管线较短情况
        public bool GetPipeNodeParameterUnder(int flag, out Vector[] vtxs, out Vector dir, out IPipeSection section)
        {
            vtxs = null;
            dir = null;
            section = null;
            IPolyline polyline = null;
            if ((base._geoGroup[1] == null) || ((polyline = base._geoGroup[1] as IPolyline) == null))
            {
                return false;
            }
            if (polyline.PointCount < 2)
            {
                return false;
            }
            ISegment seg = null;
            Vector vector = null;
            Vector vector2 = null;
            vtxs = new Vector[2];
            section = new PipeSection(this._dia1, this._dia2, HorizontalPos.Center, VerticalPos.Center, 0.02, 0);
            if (flag == 0)
            {
                vector = new Vector(polyline.GetPoint(0));
                vtxs[0] = new Vector(vector.X, vector.Y, vector.Z + this.OffsetZ);
                seg = polyline.GetSegment(0);
                dir = new Vector(seg);
                dir = dir.UnitVector();
                double length = dir.Length;
                // 针对管线较短情况
                if (length > (section.Diameter / 2))
                {
                    vector2 = vector + ((Vector)((dir * section.Diameter) / 2.0));
                }
                else
                {
                    vector2 = vector + ((Vector)((dir * length) / 2.0));
                }
                vtxs[1] = new Vector(vector2.X, vector2.Y, vector2.Z + this.OffsetZ);
            }
            else
            {
                vector = new Vector(polyline.GetPoint(polyline.PointCount - 1));
                vtxs[0] = new Vector(vector.X, vector.Y, vector.Z + this.OffsetZ);
                seg = polyline.GetSegment(polyline.PointCount - 2);
                dir = new Vector(seg);
                double length = dir.Length;
                dir = -dir.UnitVector();
                // 针对管线较短情况
                if (length > (section.Diameter / 2))
                {
                    vector2 = vector + ((Vector)((dir * section.Diameter) / 2.0));
                }
                else
                {
                    vector2 = vector + ((Vector)((dir * length) / 2.0));
                }
                vtxs[1] = new Vector(vector2.X, vector2.Y, vector2.Z + this.OffsetZ);
            }
            return true;
        }
        // 架空
        public bool GetPipeNodeParameterOver(int flag, out Vector[] vtxs, out Vector dir, out IPipeSection section, out int FLAG)
        {
            FLAG = -1;
            vtxs = null;
            dir = null;
            section = null;
            IPolyline polyline = null;
            if ((base._geoGroup[1] == null) || ((polyline = base._geoGroup[1] as IPolyline) == null))
            {
                return false;
            }
            if (polyline.PointCount < 2)
            {
                return false;
            }
            ISegment seg = null;
            Vector vector = null;
            Vector vector2 = null;
            vtxs = new Vector[2];
            section = new PipeSection(this._dia1, this._dia2, HorizontalPos.Center, this._vPos, 0.02, 0);
            if (flag == 0)
            {
                vector = new Vector(polyline.GetPoint(0));
                vtxs[0] = vector;
                seg = polyline.GetSegment(0);
                if (Math.Abs(seg.EndPoint.Z - seg.StartPoint.Z) > 0.4)
                {
                    FLAG = 1;
                }
                dir = new Vector(seg);
                dir = dir.UnitVector();
                vector2 = vector + ((Vector)((dir * section.Diameter) / 2.0));
                vtxs[1] = vector2;
            }
            else
            {
                vector = new Vector(polyline.GetPoint(polyline.PointCount - 1));
                vtxs[0] = vector;
                seg = polyline.GetSegment(polyline.PointCount - 2);
                if (Math.Abs(seg.EndPoint.Z - seg.StartPoint.Z) > 0.4)
                {
                    FLAG = 1;
                }
                dir = new Vector(seg);
                dir = (-dir).UnitVector();
                vector2 = vector + ((Vector)((dir * section.Diameter) / 2.0));
                vtxs[1] = vector2;
            }
            return true;
        }
        #endregion

        #region 获取管线参数
        public bool GetPipeLineParameter(double thick, out List<Vector> vtxs, out IPipeSection section)
        {
            vtxs = null;
            section = null;
            IPolyline polyline = null;
            if ((base._geoGroup[1] == null) || ((polyline = base._geoGroup[1] as IPolyline) == null))
            {
                return false;
            }
            if (polyline.PointCount < 2)
            {
                return false;
            }
            IPoint p = null;
            Vector vector = null;
            Vector vector2 = null;
            Vector item = null;
            vtxs = new List<Vector>();
            section = new PipeSection(this._dia1, this._dia2, HorizontalPos.Center, VerticalPos.Center, thick, 0);
            for (int i = 0; i < polyline.PointCount; i++)
            {
                p = polyline.GetPoint(i);
                if (p != null)
                {
                    vector = new Vector(p);
                    if ((this._dia1 > 1.5) || (this._dia2 > 1.5))
                    {
                        item = vector;
                    }
                    else if (i == 0)
                    {
                        // 增加起点退让判断,并对较短管线做处理 FX 2014.04.08
                        vector2 = new Vector(polyline.GetSegment(i));
                        double length = vector2.Length;
                        vector2 = vector2.UnitVector();
                        if (this._isSBackhind)
                        {
                            if (length > (section.Diameter / 1.8))
                            {
                                item = vector + ((Vector)((vector2 * section.Diameter) / 1.8));
                            }
                            else
                            {
                                item = vector + ((Vector)((vector2 * length) / 1.8));
                            }
                        }
                        else
                        {
                            item = vector;
                        }
                    }
                    else if (i == (polyline.PointCount - 1))
                    {
                        // 增加终点退让判断,并对较短管线做处理 FX 2014.04.08
                        vector2 = new Vector(polyline.GetSegment(i - 1));
                        double length = vector2.Length;
                        vector2 = -vector2.UnitVector();
                        if (this._isEBackhind)
                        {
                            if (length > (section.Diameter / 1.8))
                            {
                                item = vector + ((Vector)((vector2 * section.Diameter) / 1.8));
                            }
                            else
                            {
                                item = vector + ((Vector)((vector2 * length) / 1.8));
                            }
                        }
                        else
                        {
                            item = vector;
                        }
                    }
                    else
                    {
                        item = vector;
                    }
                    item.Z += this.OffsetZ;
                    vtxs.Add(item);
                }
            }
            return true;
        }
        #endregion

        public override void Init()
        {
            try
            {
                base.Init();

                FacilityClass fac = this._facClassReg.FacilityType;
                if (fac == null) return;
                #region 设置管径
                bool bHaveDia1 = false;
                bool bHaveDia2 = false;
                bool bHaveDia = false;
                DFDataConfig.Class.FieldInfo fiDiameter1 = fac.GetFieldInfoBySystemName("Diameter1");
                if (fiDiameter1 != null)
                {
                    int indexDiameter1 = this._rowInfo.FieldIndex(fiDiameter1.Name);
                    if (indexDiameter1 != -1)
                    {
                        string diaInfo1 = "";
                        if (!this._rowInfo.IsNull(indexDiameter1) && !string.IsNullOrEmpty(diaInfo1 = this._rowInfo.GetValue(indexDiameter1).ToString()))
                        {
                            this._dia1 = double.Parse(diaInfo1) * 0.001;
                            bHaveDia1 = true;
                        }
                    }
                }
                DFDataConfig.Class.FieldInfo fiDiameter2 = fac.GetFieldInfoBySystemName("Diameter2");
                if (fiDiameter2 != null)
                {
                    int indexDiameter2 = this._rowInfo.FieldIndex(fiDiameter2.Name);
                    if (indexDiameter2 != -1)
                    {
                        string diaInfo2 = "";
                        if (!this._rowInfo.IsNull(indexDiameter2) && !string.IsNullOrEmpty(diaInfo2 = this._rowInfo.GetValue(indexDiameter2).ToString()))
                        {
                            this._dia2 = double.Parse(diaInfo2) * 0.001;
                            bHaveDia2 = true;
                        }
                    }
                }
                if (bHaveDia1 && bHaveDia2)
                {
                    bHaveDia = true;
                }
                else
                {
                    DFDataConfig.Class.FieldInfo fiDiameter = fac.GetFieldInfoBySystemName("Diameter");
                    if (fiDiameter != null)
                    {
                        int indexDiameter = this._rowInfo.FieldIndex(fiDiameter.Name);
                        if (indexDiameter != -1)
                        {
                            string diaInfo = "";
                            string coverstyle = "";
                            if (!this._rowInfo.IsNull(indexDiameter) && !string.IsNullOrEmpty(diaInfo = this._rowInfo.GetValue(indexDiameter).ToString()))
                            {
                                DFDataConfig.Class.FieldInfo fiCoverStyle = fac.GetFieldInfoBySystemName("CoverStyle");
                                if (fiCoverStyle != null)
                                {
                                    int indexCoverStyle = this._rowInfo.FieldIndex(fiCoverStyle.Name);
                                    if (indexCoverStyle != -1 && !this._rowInfo.IsNull(indexCoverStyle))
                                    {
                                        coverstyle = this._rowInfo.GetValue(indexCoverStyle).ToString();
                                    }
                                }
                                SplitDiameter(diaInfo, coverstyle, out this._dia1, out this._dia2);
                                if (this._rowInfo.FieldIndex(fiDiameter.Name + "1") != -1)
                                {
                                    this._rowInfo.SetValue(this._rowInfo.FieldIndex(fiDiameter.Name + "1"), this._dia1);
                                }
                                if (this._rowInfo.FieldIndex(fiDiameter.Name + "2") != -1)
                                {
                                    this._rowInfo.SetValue(this._rowInfo.FieldIndex(fiDiameter.Name + "2"), this._dia1);
                                }
                                bHaveDia = true;
                                this._dia1 *= 0.001;
                                this._dia2 *= 0.001;
                            }
                        }
                    }
                }
                if (!bHaveDia || (this._dia1 < 0.000001 && this._dia2 < 0.000001))
                {
                    this._dia1 = 0.05;
                    this._dia2 = 0.0;
                }
                this._segtype = GetSegType(this._dia1, this._dia2);
                #endregion

                // 设置管线对象二维Shape数据-顶点坐标\高程信息
                IPolyline route = null;
                if ((this._style != null) && (this._style is PipeLineStyleClass))
                {
                    PipeLineStyleClass style = this._style as PipeLineStyleClass;
                    switch (style.HeightParam)
                    {
                        case HeightParam.PipeHeight:
                            DFDataConfig.Class.FieldInfo fiStartHeight = fac.GetFieldInfoBySystemName("StartHeight");
                            if (fiStartHeight != null)
                            {
                                int indexStartHeight = this._rowInfo.FieldIndex(fiStartHeight.Name);
                                if (indexStartHeight != -1)
                                {
                                    this._sheight = Fac.GetDouble(base._rowInfo, indexStartHeight);
                                    if (double.IsNaN(this._sheight))
                                    {
                                        this._sheight = 0.0;
                                    }
                                }
                            }
                            DFDataConfig.Class.FieldInfo fiEndHeight = fac.GetFieldInfoBySystemName("EndHeight");
                            if (fiEndHeight != null)
                            {
                                int indexEndHeight = this._rowInfo.FieldIndex(fiEndHeight.Name);
                                if (indexEndHeight != -1)
                                {
                                    this._eheight = Fac.GetDouble(base._rowInfo, indexEndHeight);
                                }
                                if (double.IsNaN(this._eheight))
                                {
                                    this._eheight = 0.0;
                                }
                            }
                            // 根据管线起始点高程计算中间点高程,并替换设施对象中Shape二维数据
                            
                            if (this.GetPipeLineVertexs(base._geoGroup[2], this._sheight, this._eheight, out route))
                            {
                                base._geoGroup[1] = route;
                                base.SetValue("Shape", base._geoGroup[1]);
                            }
                            break;
                        case HeightParam.LineSurfH2Deep:
                            DFDataConfig.Class.FieldInfo fiStartSurfHeight = fac.GetFieldInfoBySystemName("StartSurfHeight");
                            if (fiStartSurfHeight != null)
                            {
                                int indexStartSurfHeight = this._rowInfo.FieldIndex(fiStartSurfHeight.Name);
                                if (indexStartSurfHeight != -1)
                                {
                                    this._ssurfh = Fac.GetDouble(base._rowInfo, indexStartSurfHeight);
                                    if (double.IsNaN(this._ssurfh))
                                    {
                                        this._ssurfh = 0.0;
                                    }
                                }
                            }
                            DFDataConfig.Class.FieldInfo fiStartDepth = fac.GetFieldInfoBySystemName("StartDepth");
                            if (fiStartDepth != null)
                            {
                                int indexStartDepth = this._rowInfo.FieldIndex(fiStartDepth.Name);
                                if (indexStartDepth != -1)
                                {
                                    this._sdeep = Fac.GetDouble(base._rowInfo, indexStartDepth);
                                    if (double.IsNaN(this._sdeep))
                                    {
                                        this._sdeep = 0.0;
                                    }
                                }
                            }
                            DFDataConfig.Class.FieldInfo fiEndSurfHeight = fac.GetFieldInfoBySystemName("EndSurfHeight");
                            if (fiEndSurfHeight != null)
                            {
                                int indexEndSurfHeight = this._rowInfo.FieldIndex(fiEndSurfHeight.Name);
                                if (indexEndSurfHeight != -1)
                                {
                                    this._esurfh = Fac.GetDouble(base._rowInfo, indexEndSurfHeight);
                                    if (double.IsNaN(this._esurfh))
                                    {
                                        this._esurfh = 0.0;
                                    }
                                }
                            }
                            DFDataConfig.Class.FieldInfo fiEndDepth = fac.GetFieldInfoBySystemName("EndDepth");
                            if (fiEndDepth != null)
                            {
                                int indexEndDepth = this._rowInfo.FieldIndex(fiEndDepth.Name);
                                if (indexEndDepth != -1)
                                {
                                    this._edeep = Fac.GetDouble(base._rowInfo, indexEndDepth);
                                    if (double.IsNaN(this._edeep))
                                    {
                                        this._edeep = 0.0;
                                    }
                                }
                            }
                            this._sheight = this._ssurfh - this._sdeep;
                            this._eheight = this._esurfh - this._edeep;
                            // 根据管线起始点高程计算中间点高程,并替换设施对象中Shape二维数据
                            if (this.GetPipeLineVertexs(base._geoGroup[2], this._sheight, this._eheight, out route))
                            {
                                base._geoGroup[1] = route;
                                base.SetValue("Shape", base._geoGroup[1]);
                            }
                            break;
                        case HeightParam.PipeVertexHeight:
                            this.GetPipeLineVertexs(base._geoGroup[1], out route);
                            this._sheight = double.IsNaN(route.StartPoint.Z) ? 0.0 : route.StartPoint.Z;
                            this._eheight = double.IsNaN(route.EndPoint.Z) ? 0.0 : route.EndPoint.Z;
                            // 替换设施对象中缓存行\Shape二维数据
                            base._geoGroup[1] = route;
                            base.SetValue("Shape", route);
                            break;
                        case HeightParam.Unkown:
                            this._sheight = double.IsNaN(route.StartPoint.Z) ? 0.0 : route.StartPoint.Z;
                            this._eheight = double.IsNaN(route.EndPoint.Z) ? 0.0 : route.EndPoint.Z;
                            this.SimplifyPolyline(base._geoGroup[1], out route);
                            // 替换设施对象中缓存行\Shape二维数据
                            base._geoGroup[1] = route;
                            base.SetValue("Shape", route);
                            break;
                    }
                    switch (style.HeightMode)
                    {
                        case HeightMode.Top:
                            this._vPos = VerticalPos.Top;
                            return;

                        case HeightMode.Bottom:
                            this._vPos = VerticalPos.Bottom;
                            return;
                    }
                    this._vPos = VerticalPos.Center;
                    // 重新设置高程选项,针对同类管线中高程设置不一致的情况
                    DFDataConfig.Class.FieldInfo fiHLB = fac.GetFieldInfoBySystemName("HLB");
                    if (fiHLB != null)
                    {
                        int indexHLB = this._rowInfo.FieldIndex(fiHLB.Name);
                        if (indexHLB != -1)
                        {
                            string hlb = base._rowInfo.GetValue(indexHLB).ToString();
                            if (hlb.Contains("内"))
                            {
                                this._vPos = VerticalPos.Bottom;
                            }
                            else if (hlb.Contains("外"))
                            {
                                this._vPos = VerticalPos.Top;
                            }
                            else
                            {
                                this._vPos = VerticalPos.Center;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }


        #region 显示流向信息
        public bool ShowFlowDirection(int flowDir, out IRenderModelPoint rpt)
        {
            rpt = null;
            if (base._rowInfo == null)
            {
                return false;
            }

            string str = Application.StartupPath + @"\..\Resource\Images\FlowDirection";
            if (!Directory.Exists(str))
            {
                return false;
            }
            IImage _imgFlowImg = DrawGeometry.resFactory.CreateImageFromFile(str);
            _imgFlowImg.FrameInterval = 50;

            IPolyline path = null;
            IPipeSection pipeSection = null;
            string name = "FlowDirection";
            IDrawDynamicFlow flow = null;
            IModelPoint mp = null;
            try
            {
                IModel model;
                IModel model2;
                IPoint pointValue = null;
                IPolyline polyline2 = null;
                polyline2 = base._geoGroup[1] as IPolyline;
                if ((polyline2 == null) || (polyline2.PointCount < 2))
                {
                    return false;
                }
                path = polyline2.Clone() as IPolyline;
                for (int i = 0; i < path.PointCount; i++)
                {
                    pointValue = path.GetPoint(i);
                    if (pointValue != null)
                    {
                        pointValue.Z += this.OffsetZ;
                        path.UpdatePoint(i, pointValue);
                    }
                }

                pipeSection = new PipeSection(this._dia1, this._dia2, HorizontalPos.Center, VerticalPos.Center, 0.02, 0);

                flow = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.DynamicFlow, Guid.NewGuid().ToString()) as IDrawDynamicFlow;
                flow.SetParameter(pipeSection, path, flowDir);
                flow.SetTextureRender(new string[] { name });
                if (!flow.Draw(out mp, out model, out model2))
                {
                    return false;
                }

                #region 需要runtime授权
                IFeatureDataSet iFeatureDataSet = DF3DPipeCreateApp.App.TempLib.OpenFeatureDataset("FeatureDataSet");
                IResourceManager manager = iFeatureDataSet as IResourceManager;
                if (!manager.ModelExist(mp.ModelName))
                {
                    manager.AddModel(mp.ModelName, model, null);
                }
                if (!manager.ImageExist(name))
                {
                    manager.AddImage(name, _imgFlowImg);
                }
                #endregion

                IModelPointSymbol symbol = new ModelPointSymbolClass();
                symbol.SetResourceDataSet(iFeatureDataSet);
                symbol.Color = uint.MaxValue;
                symbol.EnableColor = true;
                mp.ModelEnvelope = model.Envelope;
                rpt = DrawGeometry.Ocx.ObjectManager.CreateRenderModelPoint(mp, symbol, DrawGeometry.Ocx.ProjectTree.RootID);

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
