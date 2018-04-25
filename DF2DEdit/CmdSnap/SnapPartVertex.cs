using System;

using  WSGRI.DigitalFactory.Commands;
using WSGRI.DigitalFactory.Base;
using WSGRI.DigitalFactory.DFSystem.DFConfig; 
/*----------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：SnapPartVertex.cs
			// 文件功能描述：结点捕捉
			//
			// 
			// 创建标识：YuanHY XXXXXXXX
            // 操作步骤：1、
			//           
			// 操作说明：　
			// 修改标识：　　    
----------------------------------------------------------------*/
namespace WSGRI.DigitalFactory.DFEditorTool
{
	/// <summary>
	/// SnapPartVertex 的摘要说明。
	/// </summary>
	public class SnapPartVertex:AbstractMapCommand
	{
		private IDFApplication m_App;
		private SnapStruct.EnumSnapType  m_strSnapTpye;

		public SnapPartVertex()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public override string Caption
		{
			get
			{
				return "SnapPartVertex";
			}
			set
			{
				
			}
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

			Console.WriteLine(m_App.CurrentConfig.cfgSnapEnvironmentSet.CurrentSnapType);

			if(m_App.CurrentConfig.cfgSnapEnvironmentSet.CurrentSnapType != m_strSnapTpye.PartVertex)
			{
				m_App.CurrentConfig.cfgSnapEnvironmentSet.CurrentSnapType = m_strSnapTpye.PartVertex;
				m_App.CurrentConfig.cfgSnapEnvironmentSet.IsUseMixSnap =  false;
			}
			else
			{
				m_App.CurrentConfig.cfgSnapEnvironmentSet.CurrentSnapType = null;
			}
	
			
		}
          
		public override void UnExecute()
		{
			// TODO:  添加 SnapPartVertex.UnExecute 实现

		}

	}
}