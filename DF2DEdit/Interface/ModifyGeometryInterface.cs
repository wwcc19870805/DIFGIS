using System;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DEdit.Interface
{
	/// <summary>
	/// 更新一个点的信息
	/// </summary>
    public interface IUpdatePoint
    {
        /// <summary>
        /// 要修改几何信息的要素
        /// </summary>
        IFeature Feature{get;set;}
        /// <summary>
        /// 更新第partIndex部分的pointIndex个点
        /// </summary>
        /// <param name="partIndex"></param>
        /// <param name="pointIndex"></param>
        /// <param name="newPoint"></param>
        void UpdatePoint(int partIndex,int pointIndex,IPoint newPoint);
    }

    /// <summary>
    /// 修改几何信息
    /// </summary>
    public interface IModifyGeometry
    {
        /// <summary>
        /// 要修改几何信息的要素
        /// </summary>
        IFeature Feature{get;set;}
        /// <summary>
        /// 在第partIndex部分的pointIndex处插入新的点
        /// </summary>
        /// <param name="partIndex"></param>
        /// <param name="pointIndex"></param>
        /// <param name="newPoint"></param>
        void InsertPoint(int partIndex , int pointIndex , IPoint newPoint);
        /// <summary>
        /// 删除第partIndex部分的第pointIndex个点
        /// </summary>
        /// <param name="partIndex"></param>
        /// <param name="pointIndex"></param>
        void RemovePoint(int partIndex , int pointIndex);
        /// <summary>
        /// 删除第partIndex部分
        /// </summary>
        /// <param name="partIndex"></param>
        void RemovePart(int partIndex);
    }
}
