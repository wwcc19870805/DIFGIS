using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawStaticModel : DrawGeometry, IDrawStaticModel, IDrawGeometry
    {
        // Fields
        private double _angleZ;
        private double _bottomH;
        private Vector[] _conns;
        private Vector[] _directions;
        private IModel _fmodel;
        private bool _isBillboardZ;
        private bool _isFollowSurfH;
        private bool _isRotate;
        private bool _isScale;
        private bool _isStrechZ;
        private IPipeSection[] _sections;
        private IModel _smodel;
        private double _surfH;
        private double _topH;                 // 井顶高程               // FX 2014.04.04
        private bool _isValve;                // 是否为阀门             // FX 2014.04.04
        private bool _isWell;                 // 是否为井               // FX 2014.04.04
        private int _FLAG;                    // 用于架空阀门方向参数   // FX 2014.04.04
        private double _pitch;                // 用于架空阀门方向参数   // FX 2014.04.04
        private double _yaw;                  // 用于架空阀门方向参数   // FX 2014.04.04

        // Methods
        public DrawStaticModel()
        {
            base._modeltype = ModelType.StaticModel;
        }

        #region 绘静态制模型
        // 绘制架空静态模型 FX 2014.04.04
        public override bool DrawOver(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            mp = null;
            fmodel = null;
            smodel = null;
            try
            {
                double x;
                double num5;
                double y;
                double num7;
                double num8;
                double num9;
                Vector vector;
                Vector vector2;
                Vector vector3;
                double num = -999999.99;  // 相关管线中心Z坐标最小值
                double z = 0.0;
                if ((this._conns != null) && (this._conns.Length > 0))
                {
                    num = this._conns[Vector.MinZ(this._conns)].Z - (this._sections[0].Diameter / 2.0);
                    z = this._conns[0].Z;
                }
                if ((this._isScale && (this._sections != null)) && (this._sections.Length > 0))
                {
                    int index = PipeSection.MaxSection(this._sections);
                    base._scaleX = base._scaleY = base._scaleZ = this._sections[index].Diameter * 1.05;
                }
                if ((this._isRotate && (this._directions != null)) && (this._directions.Length > 0))
                {
                    switch (this._directions.Length)
                    {
                        case 1:
                            if (this._directions[0].Z > 0)
                            {
                                base._rotateY = -Maths.AngleTan(new Vector(-this._directions[0].X, -this._directions[0].Y, 0.0).Length, -this._directions[0].Z);
                            }
                            else
                            {
                                base._rotateY = -Maths.AngleTan(new Vector(this._directions[0].X, this._directions[0].Y, 0.0).Length, this._directions[0].Z);
                            }
                            if (this._FLAG == 1)
                            {
                                base._rotateZ = this._yaw;
                            }
                            else
                            {
                                base._rotateZ = Maths.AngleTan(this._directions[0].X, this._directions[0].Y);
                                if (this._FLAG == -1)
                                {
                                    base._rotateX = this._pitch;
                                }
                            }
                            //base._rotateZ = Maths.AngleTan(this._directions[0].X, this._directions[0].Y);
                            //base._rotateY = -Maths.AngleTan(new Vector(this._directions[0].X, this._directions[0].Y, 0.0).Length, this._directions[0].Z);
                            goto Label_02E2;

                        case 2:
                            x = this._directions[0].X;
                            y = this._directions[0].Y;
                            num8 = this._directions[0].Z;
                            num5 = this._directions[1].X;
                            num7 = this._directions[1].Y;
                            num9 = this._directions[1].Z;
                            vector2 = new Vector(x, y, 0.0).UnitVector();
                            vector3 = new Vector(num5, num7, 0.0).UnitVector();
                            //if ((vector2 + vector3).Length >= 0.08)
                            if ((vector2 + vector3).Length >= 0.04)
                            {
                                goto Label_0229;
                            }
                            vector = vector2;
                            goto Label_023E;

                        case 3:
                            goto Label_02E2;
                    }
                }
                goto Label_02E2;
            Label_0229:
                vector = (-vector2 + vector3).UnitVector();
            Label_023E:
                //base._rotateZ = Maths.AngleTan(vector.X, vector.Y);
                vector2 = new Vector(x, y, num8).UnitVector();
                vector3 = new Vector(num5, num7, num9).UnitVector();
                //if ((vector2 + vector3).Length < 0.08)
                if ((vector2 + vector3).Length < 0.04)
                {
                    vector = vector2;
                }
                else
                {
                    if ((vector2.Z - vector3.Z) < 0)
                    {
                        vector = (-vector2 + vector3).UnitVector();
                    }
                    else
                    {
                        vector = (vector2 - vector3).UnitVector();
                    }
                    //vector = (-vector2 + vector3).UnitVector();
                }
                //base._rotateY = -Maths.AngleTan(new Vector(vector.X, vector.Y, 0.0).Length, vector.Z);
                if (vector.Z > 0)
                {
                    base._rotateY = -Maths.AngleTan(new Vector(-vector.X, -vector.Y, 0.0).Length, -vector.Z);
                }
                else
                {
                    base._rotateY = -Maths.AngleTan(new Vector(vector.X, vector.Y, 0.0).Length, vector.Z);
                }
                if (this._FLAG == 1)
                {
                    base._rotateZ = this._yaw;
                }
                else
                {
                    base._rotateZ = Maths.AngleTan(vector.X, vector.Y);
                    if (this._FLAG == -1)
                    {
                        //if (((this._pitch * 180) / 3.1415926535897931) < 3 || Math.Abs((this._pitch * 180) / 3.1415926535897931 - 180) < 3)
                        //{
                        //}
                        //else
                        //{
                        //    base._rotateZ = this._yaw ;
                        //}
                        if (base._rotateZ > 0)
                        {
                            base._rotateX = this._pitch;
                        }
                        else
                        {
                            base._rotateX = this._pitch;
                        }
                    }
                }
            Label_02E2:
                if (this._isFollowSurfH)
                {
                    base._z = this._surfH;
                }
                else
                {
                    base._z = z;
                }
                if (this._isStrechZ)
                {
                    if (((this._conns != null) && (this._conns.Length > 0)) && (this._bottomH > num))
                    {
                        this._bottomH = num - 0.5;
                    }
                    base._scaleZ = (this._surfH - this._bottomH) / 2.0;
                    if (base._scaleZ <= 0.0)
                    {
                        base._scaleZ = 1.0;
                    }
                }
                if (this._isBillboardZ)
                {
                    if (this._angleZ == -1.0)
                    {
                        Vector vector4 = null;
                        if (this._directions.Length == 2)
                        {
                            vector4 = (this._directions[0] - this._directions[1]).UnitVector();
                        }
                        else
                        {
                            vector4 = this._directions[0];
                        }
                        this._angleZ = (vector4.CalcDirection() * 180.0) / 3.1415926535897931;
                        base._rotateZ = vector4.CalcDirection();
                    }
                    else
                    {
                        base._rotateZ = (3.1415926535897931 * (this._angleZ + 90.0)) / 180.0;
                    }
                }
                mp = this.GetModelPoint();
                fmodel = this._fmodel;
                smodel = this._smodel;
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        // 根据数据做改变 FX 2014.04.04
        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            mp = null;
            fmodel = null;
            smodel = null;
            try
            {
                double x;
                double num5;
                double y;
                double num7;
                double num8;
                double num9;
                Vector vector;
                Vector vector2;
                Vector vector3;
                double num = -999999.99; // 相关管线中心Z坐标最小值
                double z = base._z;
                //double z = 0.0;
                if ((this._conns != null) && (this._conns.Length > 0))
                {
                    num = this._conns[Vector.MinZ(this._conns)].Z - (this._sections[0].Diameter / 2.0);
                    z = this._conns[0].Z;
                }
                if ((this._isScale && (this._sections != null)) && (this._sections.Length > 0))
                {
                    int index = PipeSection.MaxSection(this._sections);
                    //base._scaleX = base._scaleY = base._scaleZ = this._sections[index].Diameter * 1.05;
                    if (this._isValve)
                    {
                        base._scaleX = base._scaleY = base._scaleZ = this._sections[index].Diameter * 1.05;
                    }
                    else
                    {
                        base._scaleX = base._scaleY = base._scaleZ = this._sections[index].Diameter * 1.2;
                    }
                    // 井\篦子模型随管径变化比例不小于1
                    if (base._scaleX <= 1.0)
                    {
                        if (this._isWell)
                        {
                            base._scaleX = base._scaleY = base._scaleZ = 1.0;
                        }
                    }
                }
                if ((this._isRotate && (this._directions != null)) && (this._directions.Length > 0))
                {
                    switch (this._directions.Length)
                    {
                        case 1:
                            base._rotateZ = Maths.AngleTan(this._directions[0].X, this._directions[0].Y);
                            base._rotateY = -Maths.AngleTan(new Vector(this._directions[0].X, this._directions[0].Y, 0.0).Length, this._directions[0].Z);
                            goto Label_02E2;

                        case 2:
                            x = this._directions[0].X;
                            y = this._directions[0].Y;
                            num8 = this._directions[0].Z;
                            num5 = this._directions[1].X;
                            num7 = this._directions[1].Y;
                            num9 = this._directions[1].Z;
                            vector2 = new Vector(x, y, 0.0).UnitVector();
                            vector3 = new Vector(num5, num7, 0.0).UnitVector();
                            if ((vector2 + vector3).Length >= 0.08)
                            {
                                goto Label_0229;
                            }
                            vector = vector2;
                            goto Label_023E;

                        case 3:
                            Vector v1;
                            Vector v2;
                            double a1 = Math.Abs(Vector.CalcAngle(this._directions[0], this._directions[1]));
                            double a2 = Math.Abs(Vector.CalcAngle(this._directions[1], this._directions[2]));
                            double a3 = Math.Abs(Vector.CalcAngle(this._directions[0], this._directions[2]));
                            double angleMax = Math.Max(Math.Max(a1, a2), a3);
                            if (angleMax == a1)
                            {
                                v1 = this._directions[0];
                                v2 = this._directions[1];
                            }
                            else
                            {
                                if (angleMax == a2)
                                {
                                    v1 = this._directions[1];
                                    v2 = this._directions[2];
                                }
                                else
                                {
                                    v1 = this._directions[0];
                                    v2 = this._directions[2];
                                }
                            }
                            x = v1.X;
                            y = v1.Y;
                            num8 = v1.Z;
                            num5 = v2.X;
                            num7 = v2.Y;
                            num9 = v2.Z;
                            vector2 = new Vector(x, y, 0.0).UnitVector();
                            vector3 = new Vector(num5, num7, 0.0).UnitVector();
                            if ((vector2 + vector3).Length >= 0.08)
                            {
                                goto Label_0229;
                            }
                            vector = vector2;
                            goto Label_023E;
                    }
                }
                goto Label_02E2;
            Label_0229:
                vector = (-vector2 + vector3).UnitVector();
            Label_023E:
                base._rotateZ = Maths.AngleTan(vector.X, vector.Y);
                vector2 = new Vector(x, y, num8).UnitVector();
                vector3 = new Vector(num5, num7, num9).UnitVector();
                if ((vector2 + vector3).Length < 0.08)
                {
                    vector = vector2;
                }
                else
                {
                    vector = (-vector2 + vector3).UnitVector();
                }
                base._rotateY = -Maths.AngleTan(new Vector(vector.X, vector.Y, 0.0).Length, vector.Z);
            Label_02E2:
                if (this._isFollowSurfH)
                {
                    base._z = this._surfH;
                }
                else
                {
                    if (!this._isWell)
                    {
                        base._z = z;
                    }
                    //base._z = z;
                }
                if (this._isStrechZ)
                {
                    if (((this._conns != null) && (this._conns.Length > 0)) && (this._bottomH > num))
                    {
                        this._bottomH = num - 0.5;
                        base._z = (this._bottomH + this._topH) / 2.0;
                    }
                    // 数据中用井顶井底高程计算
                    //base._scaleZ = (this._surfH - this._bottomH) / 2.0;
                    base._scaleZ = this._topH - this._bottomH;
                    if (base._scaleZ <= 0.0)
                    {
                        base._scaleZ = 1.0;
                    }
                }
                if (this._isBillboardZ)
                {
                    // 绕Z轴旋转
                    switch (this._directions.Length)
                    {
                        case 1:
                            base._rotateZ = Maths.AngleTan(this._directions[0].X, this._directions[0].Y);
                            break;

                        case 2:
                            x = this._directions[0].X;
                            y = this._directions[0].Y;
                            num8 = this._directions[0].Z;
                            num5 = this._directions[1].X;
                            num7 = this._directions[1].Y;
                            num9 = this._directions[1].Z;
                            vector2 = new Vector(x, y, 0.0).UnitVector();
                            vector3 = new Vector(num5, num7, 0.0).UnitVector();
                            if ((vector2 + vector3).Length >= 0.08)
                            {
                                vector = (-vector2 + vector3).UnitVector();
                            }
                            else
                            {
                                vector = vector2;
                            }
                            base._rotateZ = Maths.AngleTan(vector.X, vector.Y);
                            break;

                        case 3:
                            Vector v1;
                            Vector v2;
                            double a1 = Math.Abs(Vector.CalcAngle(this._directions[0], this._directions[1]));
                            double a2 = Math.Abs(Vector.CalcAngle(this._directions[1], this._directions[2]));
                            double a3 = Math.Abs(Vector.CalcAngle(this._directions[0], this._directions[2]));
                            double angleMax = Math.Max(Math.Max(a1, a2), a3);
                            if (angleMax == a1)
                            {
                                v1 = this._directions[0];
                                v2 = this._directions[1];
                            }
                            else
                            {
                                if (angleMax == a2)
                                {
                                    v1 = this._directions[1];
                                    v2 = this._directions[2];
                                }
                                else
                                {
                                    v1 = this._directions[0];
                                    v2 = this._directions[2];
                                }
                            }
                            x = v1.X;
                            y = v1.Y;
                            num8 = v1.Z;
                            num5 = v2.X;
                            num7 = v2.Y;
                            num9 = v2.Z;
                            vector2 = new Vector(x, y, 0.0).UnitVector();
                            vector3 = new Vector(num5, num7, 0.0).UnitVector();
                            if ((vector2 + vector3).Length >= 0.08)
                            {
                                vector = (-vector2 + vector3).UnitVector();
                            }
                            else
                            {
                                vector = vector2;
                            }
                            //base._rotateZ = Maths.AngleTan(vector.X, vector.Y);
                            // 天门地下管网系统里面下水篦子默认朝向是南北朝向,三通位置在计算按管线方向绕Z轴旋转后还要再转90°
                            double rotateangle = Maths.AngleTan(vector.X, vector.Y);
                            base._rotateZ = rotateangle + (90 * 3.1415926) / 180;
                            break;
                    }
                    //if (this._angleZ == -1.0)
                    //{
                    //    Vector vector4 = null;
                    //    if (this._directions.Length == 2)
                    //    {
                    //        vector4 = (this._directions[0] - this._directions[1]).UnitVector();
                    //    }
                    //    else
                    //    {
                    //        vector4 = this._directions[0];
                    //    }
                    //    this._angleZ = (vector4.CalcDirection() * 180.0) / 3.1415926535897931;
                    //    base._rotateZ = vector4.CalcDirection();
                    //}
                    //else
                    //{
                    //    base._rotateZ = (3.1415926535897931 * (this._angleZ + 90.0)) / 180.0;
                    //}
                }
                mp = this.GetModelPoint();
                fmodel = this._fmodel;
                smodel = this._smodel;
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        #endregion

        #region 设置静态模型参数
        public void SetParameter(Vector center, double surfH, double bottomH, double angleZ, bool isRotate, bool isScale, bool isFollowSurfH, bool isStrechZ, bool isBillboardZ)
        {
            try
            {
                base._x = center.X;
                base._y = center.Y;
                base._z = center.Z;
                this._surfH = surfH;
                this._bottomH = bottomH;
                this._angleZ = angleZ;
                this._isScale = isScale;
                this._isRotate = isRotate;
                this._isFollowSurfH = isFollowSurfH;
                this._isStrechZ = isStrechZ;
                this._isBillboardZ = isBillboardZ;
            }
            catch (Exception exception)
            {
            }
        }
        // FX 2014.04.04
        public void SetParameter(Vector center, bool isValve, bool isWell, double surfH, double topH, double bottomH, bool isRotate, bool isScale, bool isFollowSurfH, bool isStrechZ, bool isBillboardZ)
        {
            try
            {
                base._x = center.X;
                base._y = center.Y;
                base._z = center.Z;
                this._isValve = isValve;
                this._isWell = isWell;
                this._surfH = surfH;
                this._topH = topH;
                this._bottomH = bottomH;
                this._isScale = isScale;
                this._isFollowSurfH = isFollowSurfH;
                this._isStrechZ = isStrechZ;
                this._isRotate = isRotate;
                this._isBillboardZ = isBillboardZ;
            }
            catch (Exception exception)
            {
            }
        }
        #endregion

        // Properties
        public Vector[] ConnectPoint
        {
            get
            {
                return this._conns;
            }
            set
            {
                this._conns = value;
            }
        }

        public Vector[] Directions
        {
            get
            {
                return this._directions;
            }
            set
            {
                this._directions = value;
            }
        }

        public IModel FineModel
        {
            get
            {
                return this._fmodel;
            }
            set
            {
                this._fmodel = value;
            }
        }

        public bool IsBillboardZ
        {
            get
            {
                return this._isBillboardZ;
            }
            set
            {
                this._isBillboardZ = value;
            }
        }

        public bool IsFollowSurfH
        {
            get
            {
                return this._isFollowSurfH;
            }
            set
            {
                this._isFollowSurfH = value;
            }
        }

        public bool IsRotate
        {
            get
            {
                return this._isRotate;
            }
            set
            {
                this._isRotate = value;
            }
        }

        public bool IsScale
        {
            get
            {
                return this._isScale;
            }
            set
            {
                this._isScale = value;
            }
        }

        public bool IsZStrech
        {
            get
            {
                return this._isStrechZ;
            }
            set
            {
                this._isStrechZ = value;
            }
        }

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

        public IModel SimpleModel
        {
            get
            {
                return this._smodel;
            }
            set
            {
                this._smodel = value;
            }
        }
        // FX 2014.04.04
        public int FLAG
        {
            get
            {
                return this._FLAG;
            }
            set
            {
                this._FLAG = value;
            }
        }

        public double Pitch
        {
            get
            {
                return this._pitch;
            }
            set
            {
                this._pitch = value;
            }
        }

        public double Yaw
        {
            get
            {
                return this._yaw;
            }
            set
            {
                this._yaw = value;
            }
        }
    }
}
