/*----------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：SetTargetLayer.cs
			// 文件功能描述：设置当前可编辑图层(目标图层)
			//
			// 
			// 创建标识：LuoXuan
            // 操作说明：
            //　　　　　    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;

using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DF2DData.Class;
using DF2DEdit.Class;
using DFWinForms.Service;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace DF2DEdit.BaseEdit
{
	/// <summary>
	/// SetEditLayer 的摘要说明。
	/// </summary>
    public class SetTargetLayer : AbstractMap2DCommand 			
	{
        private DF2DApplication m_App;
        private IMap m_pMap;

        public override void Init(object sender)
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;

            m_App = DF2DApplication.Application;
            if (m_App == null || m_App.Current2DMapControl == null) return;

            m_pMap = m_App.Current2DMapControl.ActiveView.FocusMap;
            if (m_pMap == null)
            {
                return;
            }
            if (m_pMap.LayerCount == 0)
            {
                return;
            }

            BarEditItem item = sender as BarEditItem;
            if (item.Edit is RepositoryItemComboBox)
            {
                RepositoryItemComboBox ricb = item.Edit as RepositoryItemComboBox;
                for (int i = 0; i < m_pMap.LayerCount; i++)
                {
                    ILayer curLayer = m_pMap.get_Layer(i);
                    AddLayersToTargetLayerComboBox(curLayer, ricb);
                }
            }

        }

        public override void Run(object sender, System.EventArgs e)
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            if (m_App == null || m_App.Current2DMapControl == null) return;

            ComboBoxEdit cbEdit = sender as ComboBoxEdit;
            Item item = cbEdit.SelectedItem as Item;
            Common.CurEditLayer = item.Value as ILayer;

            //选定当前编辑图层后，自动开启编辑模式
            Common.StartEditing(m_pMap);

            //自动开启混合捕捉
            CfgSnapEnvironmentSet cfgSnapEnvironmentSet = new CfgSnapEnvironmentSet();
            cfgSnapEnvironmentSet.Tolerence = 15;
            cfgSnapEnvironmentSet.IsOpen = true;

            SnapStruct.BoolSnapMode mode = cfgSnapEnvironmentSet.SnapMode;
            mode.Endpoint = true;
            mode.Intersection = true;
            mode.PartBoundary = true;
            mode.PartVertex = true;
            cfgSnapEnvironmentSet.SnapMode = mode;

            m_App.Workbench.UpdateMenu();
        }

        #region 填充目标图层下拉框
        /// <summary>
        /// 填充目标图层下拉框
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="ricb"></param>
        private void AddLayersToTargetLayerComboBox(ILayer pLayer, RepositoryItemComboBox ricb)
        {
            if (pLayer is IGroupLayer)//如果是组合图层
            {
                ICompositeLayer groupLayer = (ICompositeLayer)pLayer;

                for (int j = 0; j < groupLayer.Count; j++)
                {
                    //递归
                    AddLayersToTargetLayerComboBox(groupLayer.get_Layer(j), ricb);
                }
            }
            else if (pLayer is IFeatureLayer) //如果是地理要素图层
            {
                //排除CAD图层
                if ((pLayer as IFeatureLayer).DataSourceType == "CAD Annotation Feature Class" || (pLayer as IFeatureLayer).DataSourceType == "CAD Point Feature Class"
                    || (pLayer as IFeatureLayer).DataSourceType == "CAD Polyline Feature Class" || (pLayer as IFeatureLayer).DataSourceType == "CAD Polygon Feature Class")
                {
                    return;
                }
                //判断图层是否可编辑
                IDatasetEditInfo pEdit = (IDatasetEditInfo)((pLayer as IFeatureLayer).FeatureClass);
                if (!pEdit.CanEdit)
                {
                    return;
                }
                //将图层添加进ComBox
                Item item = new Item(pLayer.Name, pLayer);
                ricb.Items.Add(item);
            }
        }
        #endregion

	}		

}
