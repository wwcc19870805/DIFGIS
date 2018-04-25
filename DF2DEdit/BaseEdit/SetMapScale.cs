/*----------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：SetMapScale.cs
			// 文件功能描述：设置地图比例尺
			//
			// 
			// 创建标识：YuanHY 20060712
            // 操作说明：点击下拉框，或在下拉框中输入数字，回车
            //　　　　　    
----------------------------------------------------------------*/
using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

using WSGRI.DigitalFactory.Commands;
using WSGRI.DigitalFactory.Base;
using WSGRI.DigitalFactory.DFFunction;

using Infragistics.Win;
using Infragistics.Win.UltraWinToolbars;

namespace WSGRI.DigitalFactory.DFEditorTool
{
	/// <summary>
	/// SetMapScale 的摘要说明。
	/// </summary>
	public class SetMapScale:AbstractToolCommand
	{
		private IDFApplication	m_App;
		private IMapControl2	m_MapControl;
		private IMap			m_FocusMap;
		private IActiveView     m_ActiveView;

		public SetMapScale()
		{       

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

			try
			{
				m_MapControl  = m_App.CurrentMapControl;
				m_ActiveView  = m_App.CurrentMapControl.ActiveView;
				m_FocusMap    = m_ActiveView.FocusMap;

				UltraToolbarsManager tbManager;
				tbManager = m_App.Workbench.CommandBarManager;
				ComboBoxTool tbUltraCombo;
				tbUltraCombo =(ComboBoxTool)tbManager.Tools["2dmap.Scale"];	

				IDisplayTransformation pDisplayTransform = m_App.CurrentMapControl.ActiveView.ScreenDisplay.DisplayTransformation;
                pDisplayTransform.ScaleRatio = tbUltraCombo.Value==null?1000:double.Parse(tbUltraCombo.Value.ToString());
				m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,null,m_MapControl.Extent);
			}
			catch(Exception ex)
			{
                Console.WriteLine(ex.Message+ex.StackTrace);
			}
			
		}

		public  void AddMapScaleTOComBox(IDFApplication app)
		{
			//填充地图比例尺下拉框列表
			UltraToolbarsManager tbManager;
			tbManager= app.Workbench.CommandBarManager;
			tbManager.Tools["2dmap.Scale.Label"].SharedProps.Enabled = true;
			tbManager.Tools["2dmap.Scale"].SharedProps.Enabled = true;

			ComboBoxTool tbUltraComboConstruct;
			tbUltraComboConstruct=(ComboBoxTool)tbManager.Tools["2dmap.Scale"];	
			
            try
            {
                tbUltraComboConstruct.ToolKeyPress -= new ToolKeyPressEventHandler(tbUltraComboConstruct_ToolKeyPress);
            }
            catch
            {

            }
            tbUltraComboConstruct.ToolKeyPress += new ToolKeyPressEventHandler(tbUltraComboConstruct_ToolKeyPress);
		
			try
			{
				int a = 250;
				int b = 500;
				int c = 1000;
				int d = 1500;
				int e = 2000;
				int f = 2500;
				int g = 5000;
				int h = 10000;
 
                tbUltraComboConstruct.ValueList.ValueListItems.Clear();
				tbUltraComboConstruct.ValueList.ValueListItems.Add(a, "1:250");
				tbUltraComboConstruct.ValueList.ValueListItems.Add(b, "1:500");
				tbUltraComboConstruct.ValueList.ValueListItems.Add(c, "1:1,000");
				tbUltraComboConstruct.ValueList.ValueListItems.Add(d, "1:1,500");
				tbUltraComboConstruct.ValueList.ValueListItems.Add(e, "1:2,000");
				tbUltraComboConstruct.ValueList.ValueListItems.Add(f, "1:2,500");
				tbUltraComboConstruct.ValueList.ValueListItems.Add(g, "1:5,000");
				tbUltraComboConstruct.ValueList.ValueListItems.Add(h, "1:10,000");			
			 
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void tbUltraComboConstruct_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
		{
			if(!(Char.IsNumber(e.KeyChar)||e.KeyChar==45 ||e.KeyChar==46||e.KeyChar==8 ))     
			{
				e.Handled=true;
			}
		}

	}
}
