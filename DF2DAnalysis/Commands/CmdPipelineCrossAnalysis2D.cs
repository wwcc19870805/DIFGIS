using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DFWinForms.Class;
using DF2DData.Class;
using DevExpress.XtraTreeList.Nodes;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DControl.Base;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using DF2DPipe.Class;
using ICSharpCode.Core;
using System.Data;
using DevExpress.XtraBars.Docking;
using DF2DPipe.Query.UC;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Display;
using DF2DPipe.Stats.Frm;
using DF2DAnalysis.Frm;
using System;


namespace DF2DAnalysis.Commands
{
    public class CmdPipelineCrossAnalysis2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private string diameter = "Diameter";
        private string sysName = "HLB";
        private Dictionary<string, string> cross;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(false);
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            bool ready = true;
            //cross = new Dictionary<string, string>();
            if (app == null || app.Current2DMapControl == null) return;
            m_ActiveView = app.Current2DMapControl.ActiveView;          
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            try
            {   
                if (button == 1)
                {
                    ISimpleLineSymbol pLineSym = new SimpleLineSymbol();//设置线样式
                    IRgbColor pColor = new RgbColorClass();
                    pColor.Red = 255;
                    pColor.Green = 255;
                    pColor.Blue = 0;
                    pLineSym.Color = pColor;
                    pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
                    pLineSym.Width = 2;

                    ISimpleFillSymbol pFillSym = new SimpleFillSymbol();//设置平面填充样式

                    pFillSym.Color = pColor;
                    pFillSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
                    pFillSym.Outline = pLineSym;

                    object symbol = pFillSym as object;
                    IRubberBand band = new RubberPolygonClass();
                    IGeometry geo = band.TrackNew(m_Display, null);//在地图上画多边形
                    app.Current2DMapControl.DrawShape(geo, ref symbol);
                    
                    

                    if (geo.IsEmpty)//如果多边形为空，则以点的缓冲区为图形要素
                    {
                        IPoint searchPoint = new PointClass();
                        searchPoint.PutCoords(mapX, mapY);
                        geo = PublicFunction.DoBuffer(searchPoint, PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                        //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                    }
                    if (ready)
                    {
                        Dictionary<string,List<IFeature>> lines = GetPipeLines(geo);
                        if (lines == null) return;
                        WaitForm.Start("正在查询...", "请稍后");
                        int n = 0;

                        foreach (List<IFeature> l in lines.Values)
                        {
                            n += l.Count;
                        }
                        if (n > 500)
                        {
                            XtraMessageBox.Show("区域内管线大于500条，请重新选择区域", "提示");
                            WaitForm.Stop();
                            return;
                        }
                        FrmPipelineCross dialog = new FrmPipelineCross(geo,diameter,sysName,lines);
                        WaitForm.Stop();
                        UnBind();
                        dialog.Show();
                        if (dialog.DialogResult == DialogResult.Cancel || dialog.DialogResult == DialogResult.OK)
                        {
                            RestoreEnv();
                        }
                            
                        //DataTable dt = CrossAnalysis(lines);
                        //FrmPipelineCross2D dialog = new FrmPipelineCross2D(dt);                        
                        //WaitForm.Stop();
                        //dialog.ShowDialog();
                        

                    }
                }
            }
            catch (System.Exception ex)
            {

            }

        }

        private Dictionary<string,List<IFeature>> GetPipeLines(IGeometry geo)
        {
            IFeatureCursor cursor = null;
            IFeature feature = null;
            Dictionary<string, List<IFeature>> dict = new Dictionary<string, List<IFeature>>();
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
                            //DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(sysName);
                            //if (fi == null) return null ;
                            //this.diameter = fi.Name;
                            ISpatialFilter filter = new SpatialFilter();
                            filter.Geometry = geo;
                            //filter.WhereClause = sc.Parent.ClassifyField + " =  '" + sc.Name + "'";
                            //filter.WhereClause = UpOrDown.DecorateWhereClasuse(fc).Substring(0, UpOrDown.DecorateWhereClasuse(fc).Length - 4);
                            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;//选择过滤的空间条件
                            if (fc == null || geo == null) return null;
                            cursor = fc.Search(filter, false);                          
                            while ((feature = cursor.NextFeature()) != null)
                            {
                                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                                {
                                    
                                    lines.Add(feature);
                                }
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
                if(cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if(feature != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(feature);
                    feature = null;
                }               
            }            
        }

        /*private DataTable CrossAnalysis(Dictionary<string, List<IFeature>> dict)
        {
            if (dict.Count <= 0) return null;  
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("图层"));
            foreach (string s in dict.Keys)
            {
                dt.Columns.Add(new DataColumn(s));
            }
            try
            {
                foreach (MajorClass mca in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                {
                    if (!dict.ContainsKey(mca.Alias)) continue;
                    List<IFeature> linesA = dict[mca.Alias];
                    DataRow dr = dt.NewRow();
                    dr["图层"] = mca.Alias;
                    foreach (MajorClass mcb in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                    {
                        if (!dict.ContainsKey(mcb.Alias)) continue;
                        List<IFeature> linesB = dict[mcb.Alias];
                        int count = 0;
                        
                        //if (linesB == linesA) continue;
                        
                        int n = 0;
                        foreach (IFeature la in linesA)
                        {
                            count++;
                            WaitForm.SetCaption("正在分析【" + mca.Alias + "】与【" + mcb.Alias + "】，" + count + "/" + linesA.Count);
                            int dA = GetDiameter(la,this.diameter);
                            IPolyline lineA = la.Shape as IPolyline;
                            foreach (IFeature lb in linesB)
                            {
                                if (la == lb) continue;
                                int dB = GetDiameter(lb,this.diameter);
                                IPolyline lineB = lb.Shape as IPolyline;
                                //ITopologicalOperator topo = lineA as ITopologicalOperator;
                                ITopologicalOperator topo = lineA as ITopologicalOperator;
                                topo.Simplify();
                                IGeometry geo = topo.Intersect(lineB, esriGeometryDimension.esriGeometryNoDimension);
                                if (geo.IsEmpty) continue;                               
                                IPointCollection pc = geo as IPointCollection;
                                IPoint point = pc.get_Point(0);                               
                                double disA = double.MaxValue;
                                double zA = double.MaxValue;
                                GetDistanceAndZ(lineA, point, out disA, out zA);
                                double disB = double.MaxValue;
                                double zB = double.MaxValue;
                                GetDistanceAndZ(lineB, point, out disB, out zB);
                                double l = Convert.ToDouble((dA + dB)/2000);
                                if (System.Math.Abs(zA - zB) > l) continue;
                                else
                                {
                                    string IdA = mca.Name + "_" + la.OID;
                                    string IdB = mcb.Name + "_" + lb.OID;
                                    if(!cross.ContainsKey(IdA)) cross[IdA] = IdB;
                                    n++;
                                }                               
                            }
                        }
                        dr[mcb.Alias] = n;
                        
                    }
                    dt.Rows.Add(dr);                    
                }
                return dt;
            }
            catch (System.Exception ex)
            {
                return null;
            }
           
        }
        private  void GetDistanceAndZ(IPolyline line, IPoint point,out double dis ,out double z )
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
                z = dis * (line.ToPoint.Z - line.FromPoint.Z) / line.Length + line.FromPoint.Z;
            }
            
        }

        private int GetDiameter(IFeature feature, string fieldName)
        {
            IFields fields = feature.Fields;
            int index = fields.FindField(this.diameter);
            string d =feature.get_Value(index).ToString();
            int h;
            if (d.Contains("*"))
            {
                int n = d.IndexOf("*");
                string strd = d.Substring(n + 1);
                System.Int32.TryParse(strd, out h);
            }
            else
            {
                System.Int32.TryParse(d, out h);
            }
            return h;
        }*/
        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(true);
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            gc.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
            //mapView.UnBind(this);
            //Map2DCommandManager.Pop();
        }
        private void UnBind()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
    }
}
