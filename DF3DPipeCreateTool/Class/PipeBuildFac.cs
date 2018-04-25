using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.FdeGeometry;
using DFDataConfig.Class;

namespace DF3DPipeCreateTool.Class
{
    public class PipeBuildFac:Fac
    {
        private double _surfH;          // 地面高程
        private double _topDeep;        // 井面埋深
        private double _bottomDeep;     // 井底埋深
        private double _topHeight;      // 井面高程  FX 2014.04.08
        private double _bottomHeight;   // 井底高程  FX 2014.04.08

        public double BottomDeep
        {
            get
            {
                return this._bottomDeep;
            }
            set
            {
                this._bottomDeep = value;
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
        public double TopDeep
        {
            get
            {
                return this._topDeep;
            }
            set
            {
                this._topDeep = value;
            }
        }
        public double TopHeight
        {
            get
            {
                return this._topHeight;
            }
            set
            {
                this._topHeight = value;
            }
        }
        public double BottomHeight
        {
            get
            {
                return this._bottomHeight;
            }
            set
            {
                this._bottomHeight = value;
            }
        }

        public PipeBuildFac(FacClassReg facClass, FacStyleClass style, IRowBuffer rowInfo)
            : base(facClass, style, rowInfo)
        {
            this.Init();
        }

        public override void Init()
        {
            try
            {
                base.Init();
                FacilityClass fac = this._facClassReg.FacilityType;
                if (fac == null) return;
                DFDataConfig.Class.FieldInfo fi = null;
                bool bHaveTopHeight = false;
                bool bHaveBottomHeight = false;
                fi = fac.GetFieldInfoBySystemName("TopHeight");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1)
                    {
                        this._topHeight = Fac.GetDouble(this._rowInfo, index);
                        bHaveTopHeight = true;
                    }
                }
                fi = fac.GetFieldInfoBySystemName("BottomHeight");
                if (fi != null)
                {
                    int index = this._rowInfo.FieldIndex(fi.Name);
                    if (index != -1)
                    {
                        this._bottomHeight = Fac.GetDouble(this._rowInfo, index);
                        bHaveBottomHeight = true;
                    }
                }
                if (!(bHaveBottomHeight && bHaveTopHeight))
                {
                    bool bHaveSurfHeight = false;
                    bool bHaveTopDepth = false;
                    bool bHaveBottomDepth = false;
                    fi = fac.GetFieldInfoBySystemName("SurfHeight");
                    if (fi != null)
                    {
                        int index = this._rowInfo.FieldIndex(fi.Name);
                        if (index != -1)
                        {
                            this._surfH = Fac.GetDouble(this._rowInfo, index);
                            bHaveSurfHeight = true;
                        }
                    }
                    if (bHaveSurfHeight)
                    {
                        fi = fac.GetFieldInfoBySystemName("TopDepth");
                        if (fi != null)
                        {
                            int index = this._rowInfo.FieldIndex(fi.Name);
                            if (index != -1)
                            {
                                this._topDeep = Fac.GetDouble(this._rowInfo, index);
                                bHaveTopDepth = true;
                                this._topHeight = this._surfH - this._topDeep;
                            }
                            else
                            {
                                this._topHeight = 0.0;
                            }
                        }
                        else
                        {
                            this._topHeight = 0.0;
                        }
                        fi = fac.GetFieldInfoBySystemName("BottomDepth");
                        if (fi != null)
                        {
                            int index = this._rowInfo.FieldIndex(fi.Name);
                            if (index != -1)
                            {
                                this._bottomDeep = Fac.GetDouble(this._rowInfo, index);
                                bHaveBottomDepth = true;
                                this._bottomHeight = this._surfH - this._bottomDeep;
                            }
                            else
                            {
                                this._bottomHeight = 0.0;
                            }
                        }
                        else
                        {
                            this._bottomHeight = 0.0;
                        }
                    }
                    else
                    {
                        this._topHeight = 0.0;
                        this._bottomHeight = 0.0;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
