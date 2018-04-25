/*----------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：UndoEdit.cs
			// 文件功能描述：撤消编辑
			//
			// 
			// 创建标识：LuoXuan 20170918
            // 操作说明：
            //　　　　　    
----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;

namespace DF2DEdit.BaseEdit
{
    /// <summary>
    /// UndoEdit 的摘要说明。
    /// </summary>
    public class UndoEdit : AbstractMap2DCommand
    {
        private DF2DApplication m_App;
		private IMapControl2  m_MapControl;
		private IActiveView m_pActiveView; 
		private IMap m_FocusMap;

        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;

            m_App = DF2DApplication.Application;
            if (m_App == null || m_App.Current2DMapControl == null) return;
            m_pActiveView = m_App.Current2DMapControl.ActiveView;
            m_FocusMap = m_App.Current2DMapControl.ActiveView.FocusMap;

            Class.Common.MapRefresh(m_pActiveView);

            UndoEditing(m_FocusMap);
            m_App.Workbench.UpdateMenu();

        }

		private void UndoEditing(IMap pMap)
		{
			Class.Common.UndoRedoEdit(pMap,true);
		}

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            Map2DCommandManager.Pop();
        }

    }
}

