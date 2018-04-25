using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.FdeGeometry;
using DF3DPipeCreateTool.ParamModeling;
using DFDataConfig.Class;
using Gvitech.CityMaker.Common;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using DFCommon.Class;

namespace DF3DPipeCreateTool.Class
{
    public class PipeNodeFac : Fac
    {
        protected TopoClass _tc;
        private double _angleZ;            // 旋转角度Direction
        private double _bottomH;           // 井底高程
        private double _height;            // 设备高程
        private double _surfH;             // 地面高程
        private double _topH;              // 井顶高程            
        private bool _IsValve;             // 判断是否为阀门       
        private bool _IsWell;              // 判断是否为井        
        private string _fusu;              // 附属物类别
        private double _pitch;
        private double _yaw;

        public Vector CenterPosition
        {
            get
            {
                if ((base._geoGroup[1] != null) && (base._geoGroup[1].GeometryType == gviGeometryType.gviGeometryPoint))
                {
                    return new Vector(base._geoGroup[1] as IPoint);
                }
                return base.CenterPosition;
            }
        }
        public double AngleZ
        {
            get
            {
                return this._angleZ;
            }
            set
            {
                this._angleZ = value;
            }
        }
        public double BottomH
        {
            get
            {
                return this._bottomH;
            }
            set
            {
                this._bottomH = value;
            }
        }
        public double Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }
        public double SurfH
        {
            get
            {
                return this._surfH;
            }
            set
            {
                this._surfH = value;
            }
        }
        public double TopH
        {
            get
            {
                return this._topH;
            }
            set
            {
                this._topH = value;
            }
        }
        public bool IsValve
        {
            get
            {
                return this._IsValve;
            }
            set
            {
                this._IsValve = value;
            }
        }
        public bool IsWell
        {
            get
            {
                return this._IsWell;
            }
            set
            {
                this._IsWell = value;
            }
        }
        public TopoClass TopoLayer
        {
            get
            {
                return this._tc;
            }
            set
            {
                this._tc = value;
            }
        }
        public string Fusu
        {
            get
            {
                return this._fusu;
            }
            set
            {
                this._fusu = value;
            }
        }
        public double Yaw
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
        public double Pitch
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

        public PipeNodeFac(FacClassReg facClassReg, FacStyleClass style, IRowBuffer rowInfo, TopoClass tc)
            : base(facClassReg, style, rowInfo)
        {
            this._angleZ = -1.0;
            this._yaw = 0;
            this._pitch = 0;
            this._tc = tc;
            this.Init();
        }

        public override void Init()
        {
            try
            {
                base.Init();
                FacilityClass fac = this._facClassReg.FacilityType;
                if (fac == null) return;
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("Height");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1) this._height = Fac.GetDouble(this._rowInfo, index);
                }
                fi = fac.GetFieldInfoBySystemName("SurfHeight");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1) this._surfH = Fac.GetDouble(this._rowInfo, index);
                }
                bool bHaveTopHeight = false;
                fi = fac.GetFieldInfoBySystemName("TopHeight");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1)
                    {
                        this._topH = Fac.GetDouble(this._rowInfo, index);
                        bHaveTopHeight = true;
                    }
                }
                bool bHaveBottomHeight = false;
                fi = fac.GetFieldInfoBySystemName("BottomHeight");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1)
                    {
                        this._bottomH = Fac.GetDouble(this._rowInfo, index);
                        bHaveBottomHeight = true;
                    }
                }
                if (!bHaveTopHeight && bHaveBottomHeight) this._topH = this.SurfH;

                fi = fac.GetFieldInfoBySystemName("Direction");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1) this._angleZ = Fac.GetDouble(this._rowInfo, index);
                }
                fi = fac.GetFieldInfoBySystemName("Additional");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1) this._fusu = Fac.GetString(this._rowInfo, index);
                }
                fi = fac.GetFieldInfoBySystemName("Pitch");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1)
                    {
                        this._pitch = Fac.GetDouble(this._rowInfo, index);
                    }
                }
                fi = fac.GetFieldInfoBySystemName("Yaw");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1)
                    {
                        this._yaw = Fac.GetDouble(this._rowInfo, index);
                    }
                }
                if (this._style != null)
                {

                    if (this._style.Name.Contains("井") || this._style.Name.Contains("篦"))
                    {
                        this._IsWell = true;
                        this._IsValve = false;
                        // 重新设置模型点Z坐标
                        if (base._geoGroup[1] != null && (base._geoGroup[1] as IPoint) != null)
                        {
                            IPoint pointValue = base._geoGroup[1] as IPoint;
                            if (this._topH != null && this._bottomH != null)
                            {
                                pointValue.Z = (this._topH + this._bottomH) / 2.0;
                            }
                            base._geoGroup[1] = pointValue;
                            base.SetValue("Shape", base._geoGroup[1]);
                        }

                    }
                    if (!this._IsWell && this._style.Name.Contains("阀"))
                    {
                        this._IsValve = true;
                        this._IsWell = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private FacClassReg GetFacClassReg(string fcGuid)
        {
            if (DF3DPipeCreateApp.App.PipeLib == null) return null;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = DF3DPipeCreateApp.App.PipeLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "FeatureClassId='" + fcGuid + "'";

                cursor = oc.Search(filter, false);
                List<FacClassReg> list = new List<FacClassReg>();
                if ((row = cursor.NextRow()) != null)
                {
                    FacClassReg fc = new FacClassReg();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fc.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("DataSetName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("DataSetName"));
                        if (obj != null) fc.DataSetName = obj.ToString();
                    }
                    if (row.FieldIndex("FeatureClassId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FeatureClassId"));
                        if (obj != null) fc.FeatureClassId = obj.ToString();
                    }
                    if (row.FieldIndex("FcName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FcName"));
                        if (obj != null) fc.FcName = obj.ToString();
                    }
                    if (row.FieldIndex("DataType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("DataType"));
                        if (obj != null)
                        {
                            DataLifeCyle ts = 0;
                            if (Enum.TryParse<DataLifeCyle>(obj.ToString(), out ts))
                                fc.DataType = ts;
                            else fc.DataType = 0;
                        }
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) fc.Comment = obj.ToString();
                    }
                    if (row.FieldIndex("TurnerStyle") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TurnerStyle"));
                        if (obj != null)
                        {
                            TurnerStyle ts = 0;
                            if (Enum.TryParse<TurnerStyle>(obj.ToString(), out ts))
                                fc.TurnerStyle = ts;
                            else fc.TurnerStyle = 0;
                        }
                    }
                    if (row.FieldIndex("FacilityType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacilityType"));
                        if (obj != null) fc.FacilityType = FacilityClassManager.Instance.GetFacilityClassByName(obj.ToString());
                    }
                    if (row.FieldIndex("LocationType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("LocationType"));
                        if (obj != null)
                        {
                            LocationType lt = 0;
                            if (Enum.TryParse<LocationType>(obj.ToString(), out lt))
                                fc.LocationType = lt;
                            else fc.LocationType = 0;
                        }
                    }
                    return fc;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }

        }
        private FacStyleClass GetFacStyleByID(string styleId)
        {
            if (DF3DPipeCreateApp.App.TemplateLib == null) return null;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = DF3DPipeCreateApp.App.TemplateLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityStyle");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = string.Format("ObjectId = '{0}'", styleId)
                };
                cursor = oc.Search(filter, true);
                List<FacStyleClass> list = new List<FacStyleClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    StyleType type;
                    FacStyleClass fs = null;
                    if (row.FieldIndex("StyleType") >= 0 && Enum.TryParse<StyleType>(row.GetValue(row.FieldIndex("StyleType")).ToString(), out type))
                    {
                        Dictionary<string, string> dictionary = null;
                        if (row.FieldIndex("StyleInfo") >= 0)
                        {
                            object obj = row.GetValue(row.FieldIndex("StyleInfo"));
                            if (obj != null)
                            {
                                IBinaryBuffer buffer2 = row.GetValue(row.FieldIndex("StyleInfo")) as IBinaryBuffer;
                                if (buffer2 != null)
                                {
                                    dictionary = JsonTool.JsonToObject<Dictionary<string, string>>(Encoding.UTF8.GetString(buffer2.AsByteArray()));
                                }
                            }
                        }
                        switch (type)
                        {
                            case StyleType.PipeNodeStyle:
                                fs = new PipeNodeStyleClass(dictionary);
                                break;
                            case StyleType.PipeLineStyle:
                                fs = new PipeLineStyleClass(dictionary);
                                break;
                            case StyleType.PipeBuildStyle:
                                fs = new PipeBuildStyleClass(dictionary);
                                break;
                        }
                    }
                    if (fs == null) continue;
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fs.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ObjectId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ObjectId"));
                        if (obj != null) fs.ObjectId = obj.ToString();
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fs.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fs.Name = obj.ToString();
                    }
                    int index = row.FieldIndex("Thumbnail");
                    if (index != -1 && !row.IsNull(index))
                    {
                        IBinaryBuffer b = row.GetValue(index) as IBinaryBuffer;
                        if (row != null)
                        {
                            MemoryStream stream = new MemoryStream(b.AsByteArray());
                            fs.Thumbnail = Image.FromStream(stream);
                        }
                    }
                    return fs;
                }
                return null;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        public List<PipeLineFac> GetTopoPipeLine()
        {
            if (this._tc == null) return null;
            if (DF3DPipeCreateApp.App.PipeLib == null) return null;
            IQueryFilter filter = null;
            IFdeCursor o = null;
            IRowBuffer r = null;
            Dictionary<string, List<int>> dictionary = null;
            Dictionary<string, FacClassReg> dictReg = null;
            Dictionary<int, int> dictionary2 = null;
            List<PipeLineFac> list = null;
            try
            {
                IFeatureDataSet fds = DF3DPipeCreateApp.App.PipeLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IFeatureClass class2 = fds.OpenFeatureClass(this._tc.TopoTable);
                if (class2 == null) return null;
                filter = new QueryFilterClass
                {
                    WhereClause = string.Format("(P_FacClass = '{0}' and PNode = {1}) or (E_FacClass = '{0}' and ENode = {1})", this._facClassReg.FeatureClassId, base._oid),
                    SubFields = "oid,A_FacClass,Edge,PNode,ENode"
                };
                int count = class2.GetCount(filter);
                if (count == 0) return null;
                dictionary = new Dictionary<string, List<int>>();
                dictionary2 = new Dictionary<int, int>();
                dictReg = new Dictionary<string, FacClassReg>();
                o = class2.Search(filter, true);
                while ((r = o.NextRow()) != null)
                {
                    if (!r.IsNull(1) && !r.IsNull(2))
                    {
                        FacClassReg reg = GetFacClassReg(r.GetValue(1).ToString());
                        if (!dictionary.ContainsKey(reg.FacClassCode))
                        {
                            dictionary.Add(reg.FacClassCode, new List<int>());
                            dictReg.Add(reg.FacClassCode, reg);
                        }
                        dictionary[reg.FacClassCode].Add(Fac.GetInt(r, 2));
                        if (Fac.GetInt(r, 3) == this._oid)
                        {
                            dictionary2.Add(Fac.GetInt(r, 2), 0);
                        }
                        else
                        {
                            dictionary2.Add(Fac.GetInt(r, 2), 1);
                        }
                    }
                }
                if (dictionary.Count == 0)
                {
                    return null;
                }
                list = new List<PipeLineFac>();
                foreach (KeyValuePair<string, List<int>> pair in dictionary)
                {
                    FacClassReg reg = dictReg[pair.Key];
                    if (reg.FacilityType.Name == "PipeLine")
                    {
                        IFeatureClass class3 = reg.GetFeatureClass();
                        o = class3.GetRows(pair.Value.ToArray(), false);
                        while ((r = o.NextRow()) != null)
                        {
                            int index = r.FieldIndex("StyleId");
                            if(index == -1) continue;
                            FacStyleClass style = GetFacStyleByID(r.GetValue(index).ToString());
                            if(style != null){
                                PipeLineFac line = new PipeLineFac(reg, style, r, this._tc);
                                line.Tag = dictionary2[line.FeatureId];
                                list.Add(line);
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (r != null)
                {
                    Marshal.ReleaseComObject(r);
                    r = null;
                }
            }
        }

    }
}
