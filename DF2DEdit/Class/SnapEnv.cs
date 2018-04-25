/*----------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：CfgSnapEnvironmentSet.cs
			// 文件功能描述：捕捉环境参数设置
			//
			// 
			// 创建标识：YuanHY 20060214
            //　　　　　    
----------------------------------------------------------------*/
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace DF2DEdit.Class
{
	/// <summary>
	/// CfgSnapEnvironmentSet 的摘要说明。
	/// </summary>
	public class CfgSnapEnvironmentSet
	{
		private double dblTolerence;
		private bool   isOpen;
		private SnapStruct.BoolSnapMode snapMode;
		private SnapStruct.EnumSnapType snapType;

		private IActiveView  activeView;
        public IArray featurLayerSnapArray = new ESRI.ArcGIS.esriSystem.ArrayClass();

        public IMap mapSnap = new MapClass();

		public CfgSnapEnvironmentSet()
		{
            
		}

		public double Tolerence
		{
			get
			{
				return dblTolerence;
			}
			set
			{
				dblTolerence = value;
			}
		}

        public bool IsOpen
		{
			get
			{
				return isOpen;
			}
			set
			{
				isOpen = value;
			}
		}

        public SnapStruct.BoolSnapMode SnapMode
		{
			get
			{
				return snapMode;
			}
			set
			{
				snapMode = value;
			}
		}

        public SnapStruct.EnumSnapType SnapType
		{
			get
			{
				return snapType;
			}
			set
			{
				snapType = value;
			}
		}

		public  IActiveView  ActiveView
		{
			get
			{
				return activeView;
			}
			set
			{
				activeView = value;
			}
		}
	}
}
