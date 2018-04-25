/*----------------------------------------------------------------
            // Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：FeatureLayerProperties.cs
            // 文件功能描述：图层属性
            //               
            // 
            // 创建标识：LuoXuan
            //
            // 修改描述：

----------------------------------------------------------------*/
using System;
using System.Collections;
using System.ComponentModel;

using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;

using ESRI.ArcGIS.Carto;

namespace DF2DEdit.BaseEdit
{
	/// <summary>
	/// cmdLayerProperties 的摘要说明。
	/// </summary>
    public class FeatureLayerProperties : AbstractMap2DCommand
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

            if (Class.Common.CurEditLayer != null)
            {
                System.Windows.Forms.Form mainForm = (System.Windows.Forms.Form)app.Workbench;
                Form.frmLayerProperty frm = new Form.frmLayerProperty();
                frm.FeatureLayer = Class.Common.CurEditLayer as IFeatureLayer;
                frm.MapControl = app.Current2DMapControl; 

                if (frm.ShowDialog(mainForm) == System.Windows.Forms.DialogResult.OK)
                {
                    app.Current2DMapControl.ActiveView.Refresh();
                }
            }
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            Map2DCommandManager.Pop();
        }

	}
}
