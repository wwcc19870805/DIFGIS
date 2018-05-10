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

namespace DF2DPipe.Stats.Command
{
    class CmdRegionPipeLineLengthStats2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        DataTable dtstats;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            app.Workbench.SetMenuEnable(false);
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            bool ready = true;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            app.Workbench.SetMenuEnable(true);
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
                    WaitForm.Start("正在查询...", "请稍后");

                    if (geo.IsEmpty)//如果多边形为空，则以点的缓冲区为图形要素
                    {
                        IPoint searchPoint = new PointClass();
                        searchPoint.PutCoords(mapX, mapY);
                        geo = PublicFunction.DoBuffer(searchPoint, PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                        //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                    }
                    if (ready)
                    {
                        WaitForm.Start("正在统计...", "请稍后");
                        DataTable dtResult = RegionAnalysis(geo);//根据所得图形要素分析，得到统计用数据表
                        if (dtResult == null || dtResult.Rows.Count == 0)
                        {
                            WaitForm.Stop();
                            XtraMessageBox.Show("统计结果为空！", "提示");
                            return;
                        }
                        WaitForm.Stop();
                        FrmPipeLineStatsOutput dialog = new FrmPipeLineStatsOutput();
                        dialog.SetData1(dtResult);
                        //dialog.SetData1(dtstats);
                        dialog.ShowDialog();
                        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        {
                            this.RestoreEnv();
                        }
                    }
                }
            }
            catch
            {
            }
            
        }

        private DataTable RegionAnalysis(IGeometry geo)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});//初始化统计用数据表的列

            dtstats = new DataTable();
            dtstats.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),new DataColumn("FIELDNAME"),
                                new DataColumn("LENGTH",typeof(double))});//初始化生成统计图表用数据表的列
            foreach (MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())//对所有二级大类进行遍历
            {
                string[] arrFc2DId = mc.Fc2D.Split(';');//将二级大类所对应的要素类ID转换为数组
                if (arrFc2DId == null) continue;
                double majorclasslength = 0.0;
                int indexStart = dtResult.Rows.Count;//获得数据表当前的行数
                double subclasslength = 0.0;
                foreach (SubClass sc in mc.SubClasses)//对当前二级大类的子类进行遍历
                {
                    if (!sc.Visible2D) continue;
                    double subfieldlength = 0.0;
                    bool bHave = false;
                    
                    foreach (string fc2DId in arrFc2DId)//遍历二级子类所对应的要素类ID
                    {
                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);//根据要素类ID得到DF2DFC
                        if (dffc == null) continue;
                        FacilityClass facc = dffc.GetFacilityClass();//得到设施类
                        IFeatureClass fc = dffc.GetFeatureClass();//得到要素类
                        if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                        DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength2D");//得到设施类的管线长度字段
                        if (fiPipeLength == null) continue;

                        IFields pFields = fc.Fields;//得到要素类字段集
                        int indexPipeLength = pFields.FindField(fiPipeLength.Name);//根据管线长度字段名得到要素类管线长度字段的索引
                        if (indexPipeLength < 0) continue;
                        IField pField = pFields.get_Field(indexPipeLength);//根据管线长度字段索引得到管线长度字段
                        ISpatialFilter filter = new SpatialFilter();//初始化空间过滤类
                        filter.Geometry = geo;
                        filter.SubFields = pField.Name;
                        filter.WhereClause = UpOrDown.DecorateWhereClasuse(fc) + mc.ClassifyField + " =  '" + sc.Name + "'";
                        filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;//选择过滤的空间条件
                        if (fc == null || geo == null) return null;

                        IFeatureCursor pFeatureCursor = null;
                        IFeature pFeature = null;
                        try
                        {
                            pFeatureCursor = fc.Search(filter, false);//获得过滤结果的游标
                             
                            while ((pFeature = pFeatureCursor.NextFeature()) != null)
                            {
                                object tempobj = pFeature.get_Value(indexPipeLength);//获得当前要素管线长度字段的值
                                double dtemp = 0.0;
                                if (tempobj != null && double.TryParse(tempobj.ToString(), out dtemp))//将管线长度转换为double
                                {
                                    bHave = true;
                                    subfieldlength += dtemp;//累加到当前二级子类总长度
                                }
                            }
                        }
                        catch { }
                        finally
                        {
                            if (pFeatureCursor != null)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                                pFeatureCursor = null;
                            }
                            if (pFeature != null)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeature);
                                pFeature = null;
                            }
                        }
                        if (bHave)
                        {
                            DataRow dr = dtResult.NewRow();//将对应数据填写到统计用数据表的新行
                            dr["PIPELINETYPE"] = mc;
                            dr["FIELDNAME"] = "";
                            dr["PVALUE"] = sc;
                            subclasslength += subfieldlength;
                            dr["LENGTH"] = subfieldlength.ToString("0.00");
                            dtResult.Rows.Add(dr);

                            DataRow dr1 = dtstats.NewRow();//将对应数据填写到统计图表用数据表的新行
                            dr1["PIPELINETYPE"] = mc;
                            dr1["FIELDNAME"] = sc;
                            dr1["LENGTH"] = subfieldlength.ToString("0.00");
                            dtstats.Rows.Add(dr1);


                        }
                       
                    }
                }
                int indexEnd = dtResult.Rows.Count;
                for (int i = indexStart; i < indexEnd; i++)
                {
                    DataRow dr = dtResult.Rows[i];
                    dr["TOTALLENGTH"] = subclasslength.ToString("0.00");
                }
            }
            return dtResult;
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }


    }
}
