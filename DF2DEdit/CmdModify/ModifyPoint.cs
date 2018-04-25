/*----------------------------------------------------------------
            // Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：ModifyPoint.cs
            // 文件功能描述：修改点类型图层要素的几何信息
            //
            // 
            // 创建标识：高攀20051230
            //
            // 修改标识：
            // 修改描述：
            //
            // 修改标识：
            // 修改描述：
----------------------------------------------------------------*/

using System;
using System.Data;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;

using DF2DEdit.Interface;

namespace DF2DEdit.CmdModify
{
	/// <summary>
	/// ModifyPoint 的摘要说明。
	/// </summary>
	public class ModifyPoint : IUpdatePoint 
	{
		public ModifyPoint()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        private IFeature m_Feature;

        #region IUpdatePoint 成员

        public IFeature Feature
        {
            get
            {
                // TODO:  添加 ModifyPoint.Feature getter 实现
                return m_Feature;
            }
            set
            {
                // TODO:  添加 ModifyPoint.Feature setter 实现
                m_Feature = value;
            }
        }

        public void UpdatePoint(int partIndex, int pointIndex, IPoint newPoint)
        {
            // TODO:  添加 ModifyPoint.WSGRI.DigitalFactory.DFQuery.IUpdatePoint.UpdatePoint 实现
            if (newPoint!=null)
            {
                Feature.Shape = newPoint;
                Feature.Store();
            }

        }

        #endregion
    }
}
