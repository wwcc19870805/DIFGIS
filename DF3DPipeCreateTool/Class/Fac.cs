using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPipeCreateTool.Class
{
    public class Fac
    {
        protected FacClassReg _facClassReg;
        protected string _modelname;
        protected string _objectId;
        protected int _oid;         
        protected IRowBuffer _rowInfo;
        protected FacStyleClass _style;
        protected string _styleId;
        protected IGeometry[] _geoGroup;                 // 几何对象1Geometry\2Shape\3FootPrint
        protected EditState _state;
        public virtual Vector CenterPosition
        {
            get
            {
                return new Vector(0.0, 0.0, 0.0);
            }
        }
        public FacClassReg Reg
        {
            get
            {
                return this._facClassReg;
            }
            set
            {
                this._facClassReg = value;
            }
        }
        public IGeometry FootPrint
        {
            get
            {
                return this._geoGroup[2];
            }
            set
            {
                this._geoGroup[2] = value;
            }
        }

        public IGeometry Geo2D
        {
            get
            {
                return this._geoGroup[1];
            }
            set
            {
                this._geoGroup[1] = value;
            }
        }
        public IGeometry Geo3D
        {
            get
            {
                return this._geoGroup[0];
            }
            set
            {
                this._geoGroup[0] = value;
            }
        }
        public IGeometry[] GeoGroup
        {
            get
            {
                return this._geoGroup;
            }
            set
            {
                this._geoGroup = value;
            }
        }
        public string ModelName
        {
            get
            {
                return string.Format("{0}_{1}", this._facClassReg.FcName, this._oid);
            }
        }
        public FacStyleClass Style
        {
            get
            {
                return this._style;
            }
            set
            {
                this._style = value;
            }
        }
        public IRowBuffer RowInfo
        {
            get
            {
                return this._rowInfo;
            }
            set
            {
                this._rowInfo = value;
            }
        }
        public int FeatureId
        {
            get
            {
                return this._oid;
            }
            set
            {
                this._oid = value;
            }
        }

        public Fac(FacClassReg facClassReg, FacStyleClass style, IRowBuffer rowInfo)
        {
            this._facClassReg = facClassReg;
            this._rowInfo = rowInfo;
            this._style = style;
            this._geoGroup = new IGeometry[3];
        }

        #region 取值
        public static int GetInt(IRowBuffer r, int nPos)
        {
            int result = 0;
            if (((r != null) && (nPos != -1)) && !r.IsNull(nPos))
            {
                int.TryParse(r.GetValue(nPos).ToString(), out result);
            }
            return result;
        }
        public static double GetDouble(IRowBuffer r, int nPos)
        {
            double result = 0.0;
            if (((r != null) && (nPos != -1)) && !r.IsNull(nPos))
            {
                double.TryParse(r.GetValue(nPos).ToString(), out result);
            }
            return result;
        }
        public static IGeometry GetGeometry(IRowBuffer r, int nPos)
        {
            if (((r == null) || (nPos == -1)) || r.IsNull(nPos))
            {
                return null;
            }
            return (r.GetValue(nPos) as IGeometry);
        }
        public static string GetString(IRowBuffer r, int nPos)
        {
            if (((r != null) && (nPos != -1)) && !r.IsNull(nPos))
            {
                return r.GetValue(nPos).ToString();
            }
            return "";
        }
        #endregion

        #region 设置属性字段值
        public bool SetValue(string fieldName, object value)
        {
            if (this._rowInfo == null)
            {
                return false;
            }
            try
            {
                int position  = this._rowInfo.FieldIndex(fieldName);
                if (position == -1)
                {
                    return false;
                }
                this._rowInfo.SetValue(position, value);
                if (this._state != EditState.NotSave)
                {
                    this._state = EditState.NotSave;
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        #endregion

        public virtual void Init()
        {
            try
            {
                if (this._rowInfo != null)
                {
                    this._oid = GetInt(this._rowInfo, 0);
                    if (this._rowInfo.FieldIndex("FacilityId") != -1) this._objectId = GetString(this._rowInfo, this._rowInfo.FieldIndex("FacilityId"));
                    if (this._rowInfo.FieldIndex("StyleId") != -1) this._styleId = GetString(this._rowInfo, this._rowInfo.FieldIndex("StyleId"));
                    if (this._rowInfo.FieldIndex("ModelName") != -1) this._modelname = GetString(this._rowInfo, this._rowInfo.FieldIndex("ModelName"));
                    if (this._rowInfo.FieldIndex("Geometry") != -1) this._geoGroup[0] = GetGeometry(this._rowInfo, this._rowInfo.FieldIndex("Geometry"));
                    if (this._rowInfo.FieldIndex("Shape") != -1) this._geoGroup[1] = GetGeometry(this._rowInfo, this._rowInfo.FieldIndex("Shape"));
                    if (this._rowInfo.FieldIndex("FootPrint") != -1) this._geoGroup[2] = GetGeometry(this._rowInfo, this._rowInfo.FieldIndex("FootPrint"));
                }
            }
            catch (Exception exception)
            {
            }
        }


    }
}
