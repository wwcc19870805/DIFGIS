using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DFWinForms.Class;
using DF2DData.Class;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using DFWinForms.Service;
using DF2DControl.Base;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using DF2DPipe.Class;
using ICSharpCode.Core;
using System.Data;
using DF2DPipe.Query.UC;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Display;
using DFCommon.Class;


namespace DF2DAnalysis.Class
{
    public class CrossAnalysis
    {
        int _crosstype = 1;
        string _diameter;
        string _sysname;
        string _standard;
        string _hlb;
        Dictionary<string, string> _dicCross;
        HashSet<string> _hsCross;
        HashSet<string> _hsTxt;
        Dictionary<string, List<IFeature>> geoPipelines;
        Dictionary<string, double> dictHorizon;
        Dictionary<string, double> dictVertical;
        Dictionary<string, double> dictDepth;
        public int CrossType
        {
            get { return this._crosstype; }
            set { this._crosstype = value; }
        }
        public CrossAnalysis(string diameter,string sysname,ref HashSet<string> hsCross)
        {
            this._diameter = diameter;
            this._sysname = sysname;
            this._hsCross = hsCross;
            geoPipelines = new Dictionary<string, List<IFeature>>();
        }
        public CrossAnalysis(string diameter,string sysname, ref HashSet<string> hsCross,ref HashSet<string> hsTxt)
        {
            this._diameter = diameter;
            this._sysname = sysname;
            this._hsCross = hsCross;
            this._hsTxt = hsTxt;
            geoPipelines = new Dictionary<string, List<IFeature>>();
        }

        public void  GetAllPipelineCrossByGeo(IGeometry geo)
        {
            try
            {
                geoPipelines.Clear();
                GetPipeLines(geo);
                if (geoPipelines == null || geoPipelines.Count == 0) return;
                if (this.dictDepth == null || this.dictHorizon == null || this.dictVertical == null)
                {
                    GetCrossDisRules();
                }
                DoCrossAnalysis(geoPipelines, _crosstype);
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }

        private Dictionary<string, List<IFeature>> GetPipeLines(IGeometry geo)
        {
            IFeatureCursor cursor = null;
            IFeature feature = null;
            Dictionary<string, List<IFeature>> dict = geoPipelines;
            try
            {
                foreach (MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())//对所有二级大类进行遍历
                {
                    string[] arrFc2DId = mc.Fc2D.Split(';');//将二级大类所对应的要素类ID转换为数组
                    if (arrFc2DId == null) continue;
                    List<IFeature> lines = new List<IFeature>();
                    bool bHave = false;
                    foreach (SubClass sc in mc.SubClasses)//对当前二级大类的子类进行遍历
                    {
                        if (!sc.Visible2D) continue;
                        foreach (string fc2DId in arrFc2DId)//遍历二级子类所对应的要素类ID
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);//根据要素类ID得到DF2DFC
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();//得到设施类
                            IFeatureClass fc = dffc.GetFeatureClass();//得到要素类
                            if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(_diameter);
                            if (fi == null) return null;
                            this._standard = fi.Name;
                            DFDataConfig.Class.FieldInfo fi1 = facc.GetFieldInfoBySystemName(_sysname);
                            if (fi1 == null) return null;
                            this._hlb = fi1.Name;
                            ISpatialFilter filter = new SpatialFilter();
                            filter.Geometry = geo;
                            //filter.WhereClause = sc.Parent.ClassifyField + " =  '" + sc.Name + "'";
                            //filter.WhereClause = UpOrDown.DecorateWhereClasuse(fc).Substring(0, UpOrDown.DecorateWhereClasuse(fc).Length - 4);
                            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;//选择过滤的空间条件
                            if (fc == null || geo == null) return null;
                            cursor = fc.Search(filter, false);
                            int count = fc.FeatureCount(filter);
                            while ((feature = cursor.NextFeature()) != null)
                            {
                                //if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                                //{

                                    lines.Add(feature);
                                //}
                            }

                            bHave = true;
                            dict[mc.Alias] = lines;
                            break;
                        }
                        if (bHave) break;
                    }

                }
                return dict;
            }
            catch (System.Exception ex)
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
                if (feature != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(feature);
                    feature = null;
                }
            }
        }
        public Dictionary<string, string> GetAllPipeLines()
        {
            Dictionary<string,string> dict = new Dictionary<string, string>();
            try
            {
                foreach (MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())//对所有二级大类进行遍历
                {
                    string[] arrFc2DId = mc.Fc2D.Split(';');//将二级大类所对应的要素类ID转换为数组
                    if (arrFc2DId == null) continue;
                    List<IFeature> lines = new List<IFeature>();
                    bool bHave = false;
                    foreach (SubClass sc in mc.SubClasses)//对当前二级大类的子类进行遍历
                    {
                        if (!sc.Visible2D) continue;
                        foreach (string fc2DId in arrFc2DId)//遍历二级子类所对应的要素类ID
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);//根据要素类ID得到DF2DFC
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();//得到设施类
                            IFeatureClass fc = dffc.GetFeatureClass();//得到要素类
                            if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            IQueryFilter filter = new QueryFilter();
                            //filter.WhereClause = UpOrDown.DecorateWhereClasuse(fc).Substring(0, UpOrDown.DecorateWhereClasuse(fc).Length - 4);
                            int count = fc.FeatureCount(filter);
                            bHave = true;
                            dict[mc.Alias] = count.ToString();
                            break;
                        }
                        if (bHave) break;
                    }

                }
                return dict;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private bool IsMcChecked(List<string> mcNames,string mcName)
        {
            if(mcNames == null) return false;
            foreach(string name in mcNames)
            {
                if (name == mcName) return true;
            }
            return false;
        }
        private void DoCrossAnalysis(Dictionary<string,List<IFeature>> dict,int crosstype)
        {
            if (dict.Count == 0) return ;
            if (_hsCross == null) return;
            List<string> mcChecked = new List<string>();
            Dictionary<string,string> sameCross = new Dictionary<string,string>();
            try
            {
                foreach (MajorClass mca in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                {
                    
                    if (!dict.ContainsKey(mca.Alias)) continue;
                    List<IFeature> linesA = dict[mca.Alias];
                    foreach (MajorClass mcb in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                    {
                        if (IsMcChecked(mcChecked, mcb.Name)) continue;
                        if (!dict.ContainsKey(mcb.Alias)) continue;
                        List<IFeature> linesB = dict[mcb.Alias];
                        int count = 0;
                        int n = 0;
                        string hint;
                        foreach (IFeature la in linesA)
                        {
                            count++;
                            hint = "正在分析【" + mca.Alias + "】与【" + mcb.Alias + "】，" + count + "/" + linesA.Count;
                            WaitForm.SetCaption(hint,"请稍后");
                            double dA = GetDiameter(la, this._diameter);
                            IPolyline lineA = la.Shape as IPolyline;
                            foreach (IFeature lb in linesB)
                            {
                                if (la == lb) continue;
                                double dB = GetDiameter(lb, this._diameter);
                                IPolyline lineB = lb.Shape as IPolyline;
                                //ITopologicalOperator topo = lineA as ITopologicalOperator;
                                ITopologicalOperator topo = lineA as ITopologicalOperator;
                                topo.Simplify();
                                IGeometry geo = topo.Intersect(lineB, esriGeometryDimension.esriGeometry0Dimension);
                                if (geo.IsEmpty) continue;
                                IPointCollection pc = geo as IPointCollection;
                                IPoint point = pc.get_Point(0);
                                //if (point == lineA.FromPoint || point == lineA.ToPoint || point == lineB.FromPoint || point == lineB.ToPoint)
                                if (IsPointSame(point, lineA.FromPoint) || IsPointSame(point, lineA.ToPoint) || IsPointSame(point, lineB.FromPoint) || IsPointSame(point, lineB.ToPoint))
                                    continue;
                                string hlbA = GetHLB(la);
                                string hlbB = GetHLB(lb);
                                double disA = double.MaxValue;
                                double zA = double.MaxValue;//A管中心线高
                                GetDistanceAndZ(lineA, point,dA,hlbA, out disA, out zA);
                                double disB = double.MaxValue;
                                double zB = double.MaxValue;//B管中心线高
                                GetDistanceAndZ(lineB, point ,dB,hlbB,out disB, out zB);
                               
                                double l = Double.MaxValue;
                                if (_crosstype == 0)
                                {
                                    l = (dA + dB) / 2000;
                                    //if (zA > zB)
                                    //{
                                    //    if (IsTopOrBottom(hlbA) && IsTopOrBottom(hlbB))
                                    //    {
                                    //        l = dA / 1000 ;
                                    //    }
                                    //    else if (IsTopOrBottom(hlbA) && !IsTopOrBottom(hlbB))
                                    //    {
                                    //        l = (dA + dB) / 1000 ;
                                    //    }
                                    //    else
                                    //    {
                                    //        l = dB / 1000 ;
                                    //    }
                                    //}
                                    //else if (zA < zB)
                                    //{
                                    //    if (IsTopOrBottom(hlbB) && IsTopOrBottom(hlbA))
                                    //    {
                                    //        l = dB / 1000 ;
                                    //    }
                                    //    else if (IsTopOrBottom(hlbB) && !IsTopOrBottom(hlbA))
                                    //    {
                                    //        l = (dB + dA) / 1000 ;
                                    //    }
                                    //    else
                                    //    {
                                    //        l = dA / 1000 ;
                                    //    }
                                    //}
                                }
                                else
                                {
                                    if (dictVertical.ContainsKey(mca.Name + "_" + mcb.Name))
                                    {
                                        l = (dA + dB) / 2000 + dictVertical[mca.Name + "_" + mcb.Name];
                                        //if (zA > zB)
                                        //{
                                        //    if (IsTopOrBottom(hlbA) && IsTopOrBottom(hlbB))
                                        //    {
                                        //        l = dA / 1000 + dictVertical[mca.Name + "_" + mcb.Name];
                                        //    }
                                        //    else if (IsTopOrBottom(hlbA) && !IsTopOrBottom(hlbB))
                                        //    {
                                        //        l = (dA + dB) / 1000 + dictVertical[mca.Name + "_" + mcb.Name];
                                        //    }
                                        //    else
                                        //    {
                                        //        l =dB / 1000 + dictVertical[mca.Name + "_" + mcb.Name];
                                        //    }
                                        //}
                                        //else if (zA < zB)
                                        //{
                                        //    if (IsTopOrBottom(hlbB) && IsTopOrBottom(hlbA))
                                        //    {
                                        //        l = dB / 1000 + dictVertical[mca.Name + "_" + mcb.Name];
                                        //    }
                                        //    else if (IsTopOrBottom(hlbB) && !IsTopOrBottom(hlbA))
                                        //    {
                                        //        l = (dB + dA) / 1000 + dictVertical[mca.Name + "_" + mcb.Name];
                                        //    }
                                        //    else
                                        //    {
                                        //        l = dA / 1000 + dictVertical[mca.Name + "_" + mcb.Name];
                                        //    }
                                        //}
                                    }


                                        //l = (((double)dA + (double)dB) / 2000) + dictVertical[mca.Name + "_" + mcb.Name];
                                }
                                if (l == Double.MaxValue) continue;
                                double deltaH = System.Math.Abs(zA - zB);
                                if (deltaH > l) continue;
                                else
                                {
                                    string idA = mca.Name + "_" + la.OID;
                                    string idB = mcb.Name + "_" + lb.OID;
                                    if (mca.Alias == mcb.Alias)//如果是同类型管线分析碰撞
                                    {
                                        sameCross[idA + "," + idB] = idA + "," + idB;//将管线碰撞ID对添加到字典
                                        if (!sameCross.ContainsKey(idB + "," + idA))//如果字典中查不到该id对（相同的id对，顺序相反）
                                        {                                       
                                            _hsCross.Add(idA + "," + idB);      //将该碰撞管线对添加到碰撞列表                                                                            
                                        }
                                   
                                    }
                                    else
                                    {
                                        _hsCross.Add(idA + "," + idB);
                                    }                                   
                                    if (_hsTxt != null)
                                    {
                                        if (mca.Alias == mcb.Alias)
                                        {
                                            sameCross[idA + "," + idB] = idA + "," + idB;
                                            if (!sameCross.ContainsKey(idB + "," + idA))
                                            {
                                                
                                                _hsTxt.Add(idA + "," + idB + "," + point.X.ToString() + "," + point.Y.ToString() + "," + zA.ToString() + "," + zB.ToString() + "," + System.Math.Abs(deltaH - l).ToString());
                                                
                                            }                                        
                                        }
                                        else
                                        {
                                            _hsTxt.Add(idA + "," + idB + "," + point.X.ToString() + "," + point.Y.ToString() + "," + zA.ToString() + "," + zB.ToString() + "," + System.Math.Abs(deltaH - l).ToString());
                                        }
                                       
                                       
                                    }
                                    //if (_dicCross.ContainsValue(idA) && _dicCross.ContainsKey(idB))
                                    //{
                                    //    if (_dicCross[idB] == idA) continue;

                                    //}
                                    //else
                                    //{
                                    //    if (_dicCross.ContainsKey(idA)) continue;
                                    //    _dicCross[idA] = idB;
                                    //    n++;

                                    //}

                                }
                            }
                        }
                    }
                    mcChecked.Add(mca.Name);
                }
            }
            catch (System.Exception ex)
            {
                return ;
            }
        }

        private bool IsPointSame(IPoint p1, IPoint p2)
        {
            if (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z) return true;
            else return false;
        }
        private bool IsTopOrBottom(string hlb)
        {
            if (hlb.Contains("顶"))
            return true;
            else return false;
        }
        private void GetDistanceAndZ(IPolyline line, IPoint point,double d,string hlb,out double dis, out double z)
        {
            if (line == null || point == null)
            {
                dis = double.MaxValue;
                z = double.MaxValue;
                return;
            }
            else
            {
                dis = System.Math.Sqrt((point.X - line.FromPoint.X) * (point.X - line.FromPoint.X) + (point.Y - line.FromPoint.Y) * (point.Y - line.FromPoint.Y));
                if (IsTopOrBottom(hlb))
                {
                    z = dis * (line.ToPoint.Z - line.FromPoint.Z )/ line.Length + (line.FromPoint.Z - d/2000);//归化到中心线高
                }
                else
                {
                    z = dis * (line.ToPoint.Z - line.FromPoint.Z ) / line.Length + (line.FromPoint.Z + d / 2000);//归化到中心线高
                }
               
            }

        }

        private double GetDiameter(IFeature feature, string fieldName)
        {
            IFields fields = feature.Fields;
            int index = fields.FindField(this._standard);
            if (index == -1) return int.MaxValue;
            string d = feature.get_Value(index).ToString();
            double h;
            int n;
            if (d.Contains("*")||d.Contains("X"))
            {
                if(d.Contains("*"))n = d.IndexOf("*");
                else n = d.IndexOf("X");               
                string strd = d.Substring(n + 1);
                Double.TryParse(strd, out h);
            }
            else
            {
                Double.TryParse(d, out h);
            }
            return h;
        }
        private string GetHLB(IFeature feature)
        {
            IFields fields = feature.Fields;
            int index = fields.FindField(this._hlb);
            if (index == -1) return null;
            string hlb = feature.get_Value(index).ToString();
            return hlb;

        }
        private void GetCrossDisRules()
        {
            string verticalDisRule = Config.GetConfigValue("VerticalDisRule");
            string horizonDisRule = Config.GetConfigValue("HorizonDisRule");
            string depthRule = Config.GetConfigValue("DepthRule");
            dictHorizon = new Dictionary<string, double>();
            dictVertical = new Dictionary<string, double>();
            dictDepth = new Dictionary<string, double>();
            string[] horizonArray = horizonDisRule.Split('|');
            string[] verticalArray = verticalDisRule.Split('|');
            string[] depthArray = depthRule.Split('|');
            foreach (string d in depthArray)
            {
                int index = d.IndexOf(':');
                double temp;
                Double.TryParse(d.Substring(index + 1), out temp);
                string mcName = d.Substring(0, index);
                switch (mcName)
                {
                    case "电力":
                        dictDepth["DL"] = temp;
                        break;
                    case "通讯":
                        dictDepth["TX"] = temp;
                        break;
                    case "上水":
                        dictDepth["GS"] = temp;
                        break;
                    case "下水":
                        dictDepth["PS"] = temp;
                        break;
                    case "燃气":
                        dictDepth["RQ"] = temp;
                        break;
                    case "热力":
                        dictDepth["RL"] = temp;
                        break;
                    case "工业气体":
                        dictDepth["GYQT"] = temp;
                        break;
                    case "工业水管":
                        dictDepth["GYSG"] = temp;
                        break;
                    case "工业化工":
                        dictDepth["GYHG"] = temp;
                        break;
                    case "工业其他":
                        dictDepth["GYQT"] = temp;
                        break;
                }
            }
            foreach (string v in verticalArray)
            {
                string[] mc = v.Split(',');
                switch (mc[0])
                {
                    case "电力":
                        BuildDict(mc, "DL", ref dictVertical);
                        break;
                    case "通讯":
                        BuildDict(mc, "TX", ref dictVertical);
                        break;
                    case "上水":
                        BuildDict(mc, "GS", ref dictVertical);
                        break;
                    case "下水":
                        BuildDict(mc, "PS", ref dictVertical);
                        break;
                    case "燃气":
                        BuildDict(mc, "RQ", ref dictVertical);
                        break;
                    case "热力":
                        BuildDict(mc, "RL", ref dictVertical);
                        break;
                    case "工业气体":
                        BuildDict(mc, "GYQT", ref dictVertical);
                        break;
                    case "工业水管":
                        BuildDict(mc, "GYSG", ref dictVertical);
                        break;
                    case "工业化工":
                        BuildDict(mc, "GYHG", ref dictVertical);
                        break;
                    case "工业其他":
                        BuildDict(mc, "GYQT", ref dictVertical);
                        break;
                }
            }
            foreach (string h in horizonArray)
            {
                string[] mc = h.Split(',');
                switch (mc[0])
                {
                    case "电力":
                        BuildDict(mc, "DL", ref dictHorizon);
                        break;
                    case "通讯":
                        BuildDict(mc, "TX", ref dictHorizon);
                        break;
                    case "上水":
                        BuildDict(mc, "GS", ref dictHorizon);
                        break;
                    case "下水":
                        BuildDict(mc, "PS", ref dictHorizon);
                        break;
                    case "燃气":
                        BuildDict(mc, "RQ", ref dictHorizon);
                        break;
                    case "热力":
                        BuildDict(mc, "RL", ref dictHorizon);
                        break;
                    case "工业气体":
                        BuildDict(mc, "GYQT", ref dictHorizon);
                        break;
                    case "工业水管":
                        BuildDict(mc, "GYSG", ref dictHorizon);
                        break;
                    case "工业化工":
                        BuildDict(mc, "GYHG", ref dictHorizon);
                        break;
                    case "工业其他":
                        BuildDict(mc, "GYQT", ref dictHorizon);
                        break;
                }
            }
        }

        private void BuildDict(string[] mc, string row, ref Dictionary<string, double> dict)
        {
            string[] Col = new string[] { "DL", "TX", "GS", "PS", "RQ", "RL", "GYQT", "GYSG", "GYHG", "GYQT" };
            for (int i = 1; i < mc.Length; i++)
            {
                double temp = Double.MaxValue;
                Double.TryParse(mc[i], out temp);
                if (dict.ContainsKey(row + "_" + Col[i - 1])) continue;
                dict[row + "_" + Col[i - 1]] = temp;
            }

        }
        
    }
}
