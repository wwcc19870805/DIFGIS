using System;
using System.Collections.Generic;
using System.Linq;
using DFWinForms.Service;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ICSharpCode.Core;
using DF2DControl.Command;
using DF2DData.UserControl.Pad;
using DF2DData.Class;
using System.Collections;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using DFCommon.Class;
using DF2DControl.UserControl.Pad;
using DFWinForms.LogicTree;
using DF2DData.Tree;
using DF2DData.LogicTree;
using DFDataConfig.Class;
using DevExpress.XtraSplashScreen;



namespace DF2DData.Command
{
    public class Open2DDataCommand: AbstractMap2DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DView mapView = (Map2DView)UCService.GetContent(typeof(Map2DView));
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
         
            DF2DApplication app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            try
            {
                Layer2DTreePad wsPad = (Layer2DTreePad)UCService.GetContent(typeof(Layer2DTreePad));
                EagleEyePad epad = UCService.GetContent(typeof(EagleEyePad)) as EagleEyePad;
                if (wsPad == null||epad == null) return;
                app.Workbench.SetStatusInfo("二维数据加载中…");
                SplashScreenManager.Default.SendCommand(null, "开始加载二维数据......");

                if (wsPad != null&&epad != null)
                {
                    DataUtils2D.AddAndVisualizeTreelistPipe(wsPad.TreeList, app.Current2DMapControl);//加载管线图层
                    DataUtils2D.AddAndVisualizeTreelistBackground(wsPad.TreeList, app.Current2DMapControl);//加载背景图及辅助图层，为鹰眼加载影像图
                    DataUtils2D.AddAndVisualizeEgleEyeControlBackground(epad.MapControl);
                }                            
                app.Workbench.SetStatusInfo("就绪");
                LoggingService.Info("二维数据加载成功！");
            }
            catch (Exception ex)
            {
                app.Workbench.SetStatusInfo("二维数据加载错误");
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            
            DF2DApplication app = DF2DApplication.Application;
            if (app == null) return;
            string sMapUnit = GetMapUnit(app.Current2DMapControl.Map.MapUnits);
            string coord = String.Format("{0:#.###}  {1:#.###}{2}", mapX, mapY, sMapUnit);
            app.Workbench.SetOtherInfo(coord);
        }

        public override void OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null) return;
            double sMapScale = app.Current2DMapControl.MapScale;
            //List<TreeNodeComLayer> listTreeLayer = DF2DFeatureClassManager.Instance.GetAllTreeLayer();
            List<DF2DFeatureClass> listFC = DF2DFeatureClassManager.Instance.GetAllFeatureClass();
            List<DF2DRaster> listRaster = DF2DRasterManager.Instance.GetAllRaster();
          
#region  树控件栅格节点显示控制

            foreach(DF2DRaster dfrd in listRaster)
            {
                IRasterLayer rl = dfrd.GetRasterLayer();
                TreeNodeComLayer treeLayer = dfrd.GetTreeLayer();
                if (treeLayer == null) continue;
                if (rl == null) continue;
                if (!treeLayer.CheckOn) continue;
                if (rl.MinimumScale != 0)
                {
                    if (sMapScale >= rl.MinimumScale) treeLayer.Visible = false;
                    else treeLayer.Visible = true;
                }
                else if (rl.MaximumScale != 0)
                {
                    if (sMapScale <= rl.MaximumScale) treeLayer.Visible = false;
                    else treeLayer.Visible = true;
                }
                else
                {
                    treeLayer.Visible = true;
                }
            }
#endregion
            foreach (DF2DFeatureClass dffc in listFC)
            {
#region   控制树控件管线部分二级分类显示
                string faccName = dffc.GetFacilityClassName();
                if (faccName == "PipeLine")
                {
                    string id = dffc.GetFeatureClass().FeatureClassID.ToString();
                    DF2DMajorClass dfmc = DF2DMajorClassManager.Instance.GetDFMCByFeatureClassID(id);
                    if (dfmc != null)
                    {
                        ILayer lyr = dffc.GetLayer();
                        if (lyr != null)
                        {
                            TreeNodeMajorClass2D tnmc = dfmc.GetTNByFeatureClassID(id);
                            if (!tnmc.CheckOn) continue;
                            if (tnmc != null)
                            {
                                if (lyr.MinimumScale != 0)
                                {
                                    if (sMapScale >= lyr.MinimumScale) tnmc.Visible = false;
                                    else tnmc.Visible = true;
                                }
                                else if (lyr.MaximumScale != 0)
                                {
                                    if (sMapScale <= lyr.MaximumScale) tnmc.Visible = false;
                                    else tnmc.Visible = true;
                                }
                                else
                                {
                                    tnmc.Visible = true;
                                }
                            }
                        }
                    }
                }
               
#endregion

#region  控制图层树背景图部分显示


                IFeatureLayer fl = dffc.GetFeatureLayer();
                TreeNodeComLayer treeLayer = dffc.GetTreeLayer();
                if (treeLayer == null) continue;
                if (fl == null) continue;
                if (!treeLayer.CheckOn) continue;
                if (fl.MinimumScale != 0 )
                {
                    
                    if (sMapScale >= fl.MinimumScale) treeLayer.Visible = false;
                    else treeLayer.Visible = true;
                }
                else if (fl.MaximumScale != 0 )
                {
                    if (sMapScale <= fl.MaximumScale) treeLayer.Visible = false;
                    else treeLayer.Visible = true;
                }
                else
                {
                    treeLayer.Visible = true;
                }
#endregion

            }

        }

        private string GetMapUnit(esriUnits _esriMapUnit)
        {
            string sMapUnits = string.Empty;
            switch (_esriMapUnit)
            {
                case esriUnits.esriCentimeters:
                    sMapUnits = "cm";
                    break;
                case esriUnits.esriDecimalDegrees:
                    sMapUnits = "十进制";
                    break;
                case esriUnits.esriDecimeters:
                    sMapUnits = "分米";
                    break;
                case esriUnits.esriFeet:
                    sMapUnits = "尺";
                    break;
                case esriUnits.esriInches:
                    sMapUnits = "英寸";
                    break;
                case esriUnits.esriKilometers:
                    sMapUnits = "km";
                    break;
                case esriUnits.esriMeters:
                    sMapUnits = "m";
                    break;
                case esriUnits.esriMiles:
                    sMapUnits = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    sMapUnits = "mm";
                    break;
                case esriUnits.esriNauticalMiles:
                    sMapUnits = "海里";
                    break;
                case esriUnits.esriPoints:
                    sMapUnits = "点";
                    break;
                case esriUnits.esriUnitsLast:
                    sMapUnits = "UnitsLast";
                    break;
                case esriUnits.esriUnknownUnits:
                    sMapUnits = "未知单位";
                    break;
                case esriUnits.esriYards:
                    sMapUnits = "码";
                    break;
                default:
                    break;
            }
            return sMapUnits;
        }
    }
}
