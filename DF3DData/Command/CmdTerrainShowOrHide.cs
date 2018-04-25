using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using Gvitech.CityMaker.RenderControl;
using DF3DData.UserControl.Pad;
using DFWinForms.Service;
using DFWinForms.LogicTree;
using DF3DData.Tree;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;

namespace DF3DData.Command
{
    public class CmdTerrainShowOrHide : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                //Terrain地形
                Layer3DTreePad pad = UCService.GetContent(typeof(Layer3DTreePad)) as Layer3DTreePad;
                if (pad != null)
                {
                    List<IBaseLayer> list1 = pad.GetRootLayers();
                    if (list1 != null)
                    {
                        foreach (IBaseLayer layer in list1)
                        {
                            if (layer is TreeNodeTerrain)
                            {
                                layer.Visible = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (app.Current3DMapControl.Terrain != null && app.Current3DMapControl.Terrain.IsRegistered)
                        app.Current3DMapControl.Terrain.VisibleMask = gviViewportMask.gviViewAllNormalView;

                }
                //地形模型
                List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName("DX3DMODEL");
                if (list != null)
                {
                    foreach (DF3DFeatureClass dffc in list)
                    {
                        IBaseLayer bl = dffc.GetTreeLayer();
                        if (bl != null)
                        {
                            bl.Visible = true;
                        }
                    }
                }

                app.Workbench.UpdateMenu();
            }
            catch (Exception ex)
            {

            }
        }

        public override void RestoreEnv()
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                //Terrain地形
                Layer3DTreePad pad = UCService.GetContent(typeof(Layer3DTreePad)) as Layer3DTreePad;
                if (pad != null)
                {
                    List<IBaseLayer> list1 = pad.GetRootLayers();
                    if (list1 != null)
                    {
                        foreach (IBaseLayer layer in list1)
                        {
                            if (layer is TreeNodeTerrain)
                            {
                                layer.Visible = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (app.Current3DMapControl.Terrain != null && app.Current3DMapControl.Terrain.IsRegistered)
                        app.Current3DMapControl.Terrain.VisibleMask = gviViewportMask.gviViewNone;
                }
                //地形模型
                List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName("DX3DMODEL");
                if (list != null)
                {
                    foreach (DF3DFeatureClass dffc in list)
                    {
                        IBaseLayer bl = dffc.GetTreeLayer();
                        if (bl != null)
                        {
                            bl.Visible = false;
                        }
                    }
                }
                app.Workbench.UpdateMenu();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
