using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DControl.Base;
using ESRI.ArcGIS.Controls;
using DF2DTool.Frm;
using DFCommon.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Windows.Forms;
using DF2DTool.Class;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace DF2DTool.Command
{
    class CmdSheetClip : AbstractMap2DCommand
    {
        
        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                FrmLayoutPage dialog = new FrmLayoutPage();
                IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
                if (mapView == null) return;
                DF2DApplication app = DF2DApplication.Application;
                if (app == null || app.Current2DMapControl == null) return;
                string path = Config.GetConfigValue("2DMdbPipe");
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(path, 0);

                if (pWs == null) return;

                FrmStandardOutput sOutput = new FrmStandardOutput(pWs);

                sOutput.ShowDialog();

                if (sOutput.DialogResult == DialogResult.OK)
                {
                    this.DoSomething(sOutput, dialog);
                    dialog.Show();
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
           

        }
        /// <summary>
        /// 使用标准图幅输出的参数改变布局视图的内容
        /// </summary>
        /// <param name="opt">标准图廓对话框</param>
        private void DoSomething(FrmStandardOutput output,FrmLayoutPage dialog)
        {
            try
            {
                this.DeleteElements(dialog);
                this.AddDataFrame(dialog);
                this.CopyAndOverwriteMap(dialog);

                ClassOutline oline = new ClassOutline();
                ClassOutline.SetDefault();

                oline.SetCoord(output.Coord);
                oline.SetHeightBase(output.Height);
                oline.SetIsoDistance(output.IsoDistance);
                oline.SetMapChecker(output.MapChecker);
                oline.SetMapMaker(output.MapDrawer);
                oline.SetMapName(output.MapName);
                oline.SetMapNumber(output.MapNumber);
                oline.SetMapSurveyor(output.Surveyor);
                oline.SetProduceMethod(output.MakemapMethod);
                oline.SetProductUnit(output.ProductUnit);

                if (output.RadioGroup.SelectedIndex == 0)
                {
                    oline.SetMapSize(500, 400); // 单位: 毫米
                }
                else if (output.RadioGroup.SelectedIndex == 1)
                {
                    oline.SetMapSize(500, 400); // 单位: 毫米
                }
                else
                {
                    oline.SetMapSize(400, 400); // 单位: 毫米
                }

                oline.SetMapScale(output.MapScale);

                // 坐标要在比例尺后面初始化
                if (output.OrigX.Length > 0 && output.OrigY.Length > 0)
                {
                    oline.SetOrig(
                        System.Convert.ToDouble(output.OrigY),
                        System.Convert.ToDouble(output.OrigY));
                }
                else
                {
                }

                oline.CurMapCode = output.mapCode;	//接图表

                oline.XPrefix = output.XPrefix;
                oline.YPrefix = output.YPrefix;

                ClassOutline.MarginLeft = output.MarginLeft;
                ClassOutline.MarginBottom = output.MarginDown;

                dialog.PageControl.Page.FormID = esriPageFormID.esriPageFormCUSTOM;
                dialog.PageControl.Page.PutCustomSize(60, 60);
                oline.SetPageLayout(dialog.PageControl);
                //IPageLayoutControl2 pageControl = dialog.PageControl as IPageLayoutControl2;
                //pageControl.Page.FormID = esriPageFormID.esriPageFormCUSTOM;
                //pageControl.Page.PutCustomSize(60, 60);
                //oline.SetPageLayout(pageControl);
                oline.DrawFrame();
                IGraphicsContainer gc = dialog.PageControl.PageLayout as IGraphicsContainer;
                IMapFrame pMapFrame = gc.FindFrame(dialog.PageControl.ActiveView.FocusMap) as IMapFrame;
                pMapFrame.ExtentType = esriExtentTypeEnum.esriAutoExtentScale;
           
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }

        /// <summary>
        /// 删除布局视图中所有的元素
        /// </summary>
        private void DeleteElements(FrmLayoutPage dialog)
        {
            IMapFrame pMapFrame = dialog.PageControl.GraphicsContainer.FindFrame(dialog.PageControl.ActiveView.FocusMap) as IMapFrame;
            IGraphicsContainer pGraphicsCont;
            IElement pElement;
            pGraphicsCont = dialog.PageControl.GraphicsContainer;
            pGraphicsCont.Reset();
            pElement = pGraphicsCont.Next();
            while (pElement != null)
            {
                try
                {
                    pGraphicsCont.DeleteElement(pElement);
                    pGraphicsCont.Reset();
                }
                catch
                {
                }

                pElement = pGraphicsCont.Next();
            }
        }

        /// <summary>
        /// 在布局中添加一个地图数据框
        /// </summary>
        private void AddDataFrame(FrmLayoutPage dialog)
        {
            

            IMap pMap = new MapClass();
            pMap.Name = "New Map";

            IMapFrame pMapFrame = new MapFrameClass();
            pMapFrame.Map = pMap;

            IElement pElement = pMapFrame as IElement;
            IEnvelope pEnv = new EnvelopeClass();

            //设置一个默认的地图框大小
            pEnv.PutCoords(0, 0, 5, 5);
            pElement.Geometry = pEnv;

            IGraphicsContainer pGraphicsContainer = dialog.PageControl.GraphicsContainer;
            pGraphicsContainer.AddElement(pElement, 0);

            IActiveView pActiveView = dialog.PageControl.ActiveView;

            pActiveView.FocusMap = pMap;
        }

        /// <summary>
        /// 拷贝地图视图中的内容到布局视图中
        /// </summary>
        private void CopyAndOverwriteMap(FrmLayoutPage dialog)
        {
            IObjectCopy objectCopy = new ObjectCopyClass();

            DF2DApplication app = DF2DApplication.Application;
            object toCopyMap = app.Current2DMapControl.Map;
            object copiedMap = objectCopy.Copy(toCopyMap);

            object toOverwriteMap = dialog.PageControl.ActiveView.FocusMap;

            objectCopy.Overwrite(copiedMap, ref toOverwriteMap);
        }
    }
}
