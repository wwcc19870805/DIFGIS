/*----------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：EditSet.cs
			// 文件功能描述：编辑设置
			//
			// 
			// 创建标识：YuanHY 20060109
            // 操作说明：1、
            //　　　　　    
----------------------------------------------------------------*/
using System;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto; 

using WSGRI.DigitalFactory.Commands;
using WSGRI.DigitalFactory.Gui.Views;
using WSGRI.DigitalFactory.Base;
using WSGRI.DigitalFactory.DFSystem.DFConfig;
using WSGRI.DigitalFactory.DFFunction;

namespace WSGRI.DigitalFactory.DFEditorTool
{
	/// <summary>
	/// EditSet 的摘要说明。
	/// </summary>
	public class EditSet:AbstractMapCommand
	{
		private IDFApplication m_App;
		private IMapControl2  m_MapControl;
		private IMapView m_MapView = null;

		//private bool	isEnabled   = true;
		private string	strCaption  = "编辑设置" ;
		private string	strCategory = "编辑" ;

		public EditSet()
		{

		}

		#region 类的属性
//		public override bool IsEnabled 
//		{
//			get 
//			{
//				return isEnabled;
//			}
//			set 
//			{
//				isEnabled = value;
//			}
//		}

		public override string Caption 
		{
			get
			{
				return strCaption;
			}
			set
			{
				strCaption = value ;
			}
		}

		public override string Category 
		{
			get
			{
				return strCategory;
			}
			set
			{
				strCategory = value ;
			}
		}
		#endregion

		public override void UnExecute()
		{
			// TODO:  添加 EditSet.UnExecute 实现

		}
	
		public override void Execute()
		{
			if (!(this.Hook is IDFApplication))
			{
				return;
			}
			else
			{
				m_App = (IDFApplication)this.Hook;
			}
           
			m_MapView = m_App.Workbench.GetView(typeof(MapView)) as IMapView;
			if (m_MapView == null)
			{
				return;
			}
			else
			{
				//绑定事件
				//m_MapView.CurrentTool = this;
			}

			m_MapControl   = m_App.CurrentMapControl;
            WSGRI.DigitalFactory.DFEditorTool.frmEditSet fromEditSet = new frmEditSet();
			fromEditSet.m_CfgSet      = m_App.CurrentConfig;
			fromEditSet.m_pActiveView = m_App.CurrentMapControl.ActiveView; 

			fromEditSet.ShowDialog();            

		}
       
	}
}
