    /*----------------------------------------------------------------
                // Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
                // 版权所有。 
                //
                // 文件名：ModifyPolygon.cs
                // 文件功能描述：修改线类型图层要素的几何信息
                //
                // 
                // 创建标识：高攀20051231
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
	/// ModifyPolyline 的摘要说明。
	/// </summary>
	public class ModifyPolyline : IUpdatePoint, IModifyGeometry
	{
        public ModifyPolyline()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private IFeature m_Feature;
        private IGeometryCollection m_GeoColl;

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
                m_GeoColl = m_Feature.Shape as IGeometryCollection;
            }
        }
        
        public void UpdatePoint(int partIndex, int pointIndex, IPoint newPoint)
        {
            // TODO:  添加 ModifyPoint.WSGRI.DigitalFactory.DFQuery.IUpdatePoint.UpdatePoint 实现
            IPointCollection pColl = (IPointCollection)m_GeoColl.get_Geometry(partIndex);
            if ((pointIndex > -1) && (pointIndex < pColl.PointCount) )
            {
                pColl.UpdatePoint(pointIndex, newPoint);
                Feature.Shape = m_GeoColl as IGeometry;
                Feature.Store();
            }
        }

        #endregion

        #region IModifyGeometry 成员

        public void InsertPoint(int partIndex, int pointIndex, IPoint newPoint)
        {
            // TODO:  添加 ModifyMultiPoint.WSGRI.DigitalFactory.DFQuery.IModifyGeometry.InsertPoint 实现
            IPointCollection pColl = (IPointCollection)m_GeoColl.get_Geometry(partIndex);
            if ((pointIndex >= -1) && (pointIndex <= pColl.PointCount))
            {
                //object none = Type.Missing;
                pColl.InsertPoints(pointIndex,1,ref newPoint);
                Feature.Store();
            }
        }

        public void RemovePoint(int partIndex, int pointIndex)
        {
            // TODO:  添加 ModifyMultiPoint.WSGRI.DigitalFactory.DFQuery.IModifyGeometry.RemovePoint 实现
            IPointCollection pColl = (IPointCollection)m_GeoColl.get_Geometry(partIndex);
            if ((pointIndex > -1) && (pointIndex < pColl.PointCount) && pColl.PointCount > 2)
            {
                pColl.RemovePoints(pointIndex,1);
                Feature.Shape = (IGeometry)m_GeoColl;
                Feature.Store();
            }
        }

        public void RemovePart(int partIndex)
        {
            // TODO:  添加 ModifyMultiPoint.WSGRI.DigitalFactory.DFQuery.IModifyGeometry.RemovePart 实现
            if (m_GeoColl.GeometryCount<2) return;
            if(partIndex<m_GeoColl.GeometryCount)
            {
                m_GeoColl.RemoveGeometries(partIndex,1);
                Feature.Shape = (IGeometry)m_GeoColl;
                Feature.Store();
            }
        }

        #endregion

    }
}
