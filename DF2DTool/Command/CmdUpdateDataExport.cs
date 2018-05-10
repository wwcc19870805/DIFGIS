using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using DFWinForms.Class;
using ESRI.ArcGIS.Geometry;
using DFWinForms.Service;
using DF2DPipe.Class;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using DevExpress.XtraEditors;
using DF2DTool.Frm;
using DF2DTool.Class;
using System.Windows.Forms;

namespace DF2DTool.Command
{
    public class CmdUpdateDataExport : AbstractMap2DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;

        }
        private IActiveView m_ActiveView;
        private IWorkspace m_Workspace;
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            bool ready = true;
            if (app == null || app.Current2DMapControl == null) return;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            try
            {
                if (button == 1)
                {
                    ISimpleLineSymbol pLineSym = new SimpleLineSymbol();
                    IRgbColor pColor = new RgbColorClass();
                    pColor.Red = 255;
                    pColor.Green = 255;
                    pColor.Blue = 0;
                    pLineSym.Color = pColor;
                    pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
                    pLineSym.Width = 2;

                    ISimpleFillSymbol pFillSym = new SimpleFillSymbol();

                    pFillSym.Color = pColor;
                    pFillSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
                    pFillSym.Outline = pLineSym;

                    object symbol = pFillSym as object;
                    IRubberBand band = new RubberPolygonClass();
                    IGeometry pGeo = band.TrackNew(m_Display, null);
                    app.Current2DMapControl.DrawShape(pGeo, ref symbol);
                    WaitForm.Start("正在查询...", "请稍后");
                    if (pGeo.IsEmpty)
                    {
                        IPoint searchPoint = new PointClass();
                        searchPoint.PutCoords(mapX, mapY);
                        pGeo = PublicFunction.DoBuffer(searchPoint, PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                        //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                    }
                    if (ready)
                    {
                        WaitForm.Start("正在准备导出，请稍后...");
                        IFeatureClass fc = GetUpdateRegionFC();
                        if (fc == null)
                        {
                            XtraMessageBox.Show("当前地图中没有找到\"修测范围\"图层,无法绘制修测范围！","修测范围确定");
                            return;
                        }


                        IMap pMap = app.Current2DMapControl.Map;
                        if (pMap == null) { WaitForm.Stop(); return; }
                        //ISelectionEnvironment selEnv = new SelectionEnvironmentClass();
                        //pMap.SelectByShape(pGeo, selEnv, false);

                        //ISelection pSelection = pMap.FeatureSelection;

                        FolderBrowserDialog sfd = new FolderBrowserDialog();
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            string strRegionName = string.Format("{0}{1}点{2}分", DateTime.Now.ToLongDateString(), DateTime.Now.Hour, DateTime.Now.Minute);
                            //存储范围线
                            WaitForm.SetCaption("正在存储范围线，请稍后...");
                            SaveFanWei(pGeo, strRegionName, fc);
                            string strPath = String.Format(@"{0}\{1}.dxf", sfd.SelectedPath, strRegionName);
                            if (this.m_Workspace == null)
                            {
                                XtraMessageBox.Show("获得数据工作空间失败");
                                WaitForm.Stop();
                                return;
                            }
                            WaitForm.SetCaption("开始导出数据，请稍后...");
                            ExportData(strPath, pGeo,pMap,this.m_Workspace);
                            WaitForm.SetCaption("数据导出成功！");
                            XtraMessageBox.Show("数据导出成功！");
                            RestoreEnv();
                            
                            //CreateFeature(pGeo, pMap, fc);
                        }
                       
                        WaitForm.Stop();


                    }
                }
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                return;
            }
        }

        private IFeatureClass GetUpdateRegionFC()
        {
            try
            {
                string path = Config.GetConfigValue("2DMdbPipe");
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
                this.m_Workspace = pFWs as IWorkspace;
                if (pFWs == null) return null;
                IFeatureDataset pFDs = pFWs.OpenFeatureDataset("Assi_10");
                if (pFDs == null) return null;
                IEnumDataset pEnumDs = pFDs.Subsets;
                IDataset fDs;
                IFeatureClass fc = null;
                while ((fDs = pEnumDs.Next()) != null)
                {
                    if (fDs.Name == "UpdataRegionPLY500")
                    {
                        fc = fDs as IFeatureClass;
                    }

                }
                return fc;
            }
            catch (System.Exception ex)
            {
                return null;
            }
           
        }

        /// <summary>
        /// 存储范围线
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="strRegionName"></param>
        /// <param name="fc"></param>
        private void SaveFanWei(IGeometry geo, string strRegionName, IFeatureClass fc)
        {
            try
            {
                int index1, index2, index3, index4;
                IFeature pFea;

                index1 = fc.FindField("REGIONNAME");
                index2 = fc.FindField("SMSCODE");
                index3 = fc.FindField("GEOOBJNUM");
                index4 = fc.FindField("Mark");

                //存储范围线
                pFea = fc.CreateFeature();
                pFea.set_Value(index1, strRegionName);
                pFea.set_Value(index2, "更新范围");
                pFea.set_Value(index3, "194013");
                pFea.set_Value(index4, "已导出，未导入更新");

                int index = fc.FindField(fc.ShapeFieldName);
                IGeometryDef pGD = fc.Fields.get_Field(index).GeometryDef;
                if (pGD.HasZ)
                {
                    IZAware pZA = geo as IZAware;
                    pZA.ZAware = true;
                    IZ pZ = geo as IZ;
                    double zmin = -1000, zmax = 1000;
                    if (pGD.SpatialReference.HasZPrecision())
                    {
                        pGD.SpatialReference.GetZDomain(out zmin, out zmax);
                    }
                    if (pZ != null)
                    {
                        pZ.SetConstantZ(0);
                    }
                    else
                    {
                        IPoint p = geo as IPoint;
                        if (p != null)
                        {
                            if (p.Z.ToString() == "非数字") p.Z = 0;
                        }
                    }
                    
                }

                if (pGD.HasM)
                {
                    IMAware pMA = geo as IMAware;
                    pMA.MAware = true;
                }
                (geo as ITopologicalOperator).Simplify();
                pFea.Shape = geo as IGeometry;
                pFea.Store();

            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void ExportData(string strPath, IGeometry geo,IMap pMap,IWorkspace pWorkspace)
        {
           try
           {
               clsMapExport pClsMapExport = new clsMapExport(pWorkspace, pMap, "500");
               //DateTime beginDt = new DateTime();
               //DateTime endDt = new DateTime();
               DateTime beginDt = DateTime.Now;
               pClsMapExport.ExportInit();
               pClsMapExport.gisReader.pGeo = geo;
               pClsMapExport.cadWriter.OutputFileName = strPath;
               pClsMapExport.ExportRun();
               DateTime endDt = DateTime.Now;
               
           }
           catch (System.Exception ex)
           {
           	
           }
        }
        private void CreateFeature(IGeometry geo, IMap pMap, IFeatureClass fc)
        {
            try
            {
                if (geo == null || fc == null || pMap == null) return;
                IDataset pDataset = fc as IDataset;
                IWorkspaceEdit pWorkspaceEdit = pDataset.Workspace as IWorkspaceEdit;
                int index = fc.FindField(fc.ShapeFieldName);
                IGeometryDef pGD = fc.Fields.get_Field(index).GeometryDef;
                if (pGD.HasZ)
                {
                    IZAware pZA = geo as IZAware;
                    pZA.ZAware = true;
                    IZ pZ = geo as IZ;
                    double zmin = -1000, zmax = 1000;
                    if (pGD.SpatialReference.HasZPrecision())
                    {
                        pGD.SpatialReference.GetZDomain(out zmin, out zmax);
                    }
                    if (pZ != null)
                    {
                        pZ.SetConstantZ(0);
                    }
                    else
                    {
                        IPoint p = geo as IPoint;
                        if (p.Z.ToString() == "非数字") p.Z = 0;
                    }

                }
                if (pGD.HasM)
                {
                    IMAware pMA = geo as IMAware;
                    pMA.MAware = true;
                }
                if (!pWorkspaceEdit.IsBeingEdited())
                {
                    pWorkspaceEdit.StartEditing(true);
                    pWorkspaceEdit.StartEditOperation();
                }
                IFeature pFeature = fc.CreateFeature();
                pFeature.Shape = geo;
                pFeature.Store();
                if (pWorkspaceEdit.IsBeingEdited())
                {
                    pWorkspaceEdit.StartEditOperation();
                    pWorkspaceEdit.StopEditing(true);
                }
                pMap.ClearSelection();
            }
            catch (System.Exception ex)
            {
                return;
            }
           
 
            
        }
        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            gc.DeleteAllElements();
            app.Current2DMapControl.Map.ClearSelection();
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
    }
}
