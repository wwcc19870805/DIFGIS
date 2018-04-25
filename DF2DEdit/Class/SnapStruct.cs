/*----------------------------------------------------------------
            // Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：SnapStruct.cs
            // 文件功能描述：捕捉模式结构
            //               
            // 
            // 创建标识：LuoXuan
            //
            // 修改描述：

----------------------------------------------------------------*/
using System;
using ESRI.ArcGIS.Carto;

namespace DF2DEdit.Class
{
	public class SnapStruct
	{
		public SnapStruct()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region//捕捉类型
		public struct EnumSnapType
		{
			string partBoundary ;//边线
			string partVertex;//节点
			string endpoint;//端点
			string intersection;//交点

			public string PartBoundary//边线
			{
				set
				{ 
					partBoundary = value;
				}
				get 
				{
					return "partBoundary";
				}
			}

			public string PartVertex //节点
			{
				set 
				{ 
					partVertex = value; 
				}
				get 
				{
					return "partVertex";
				}
			}
			public string Endpoint //端点
			{
				set 
				{ 
					endpoint = value; 
				}
				get 
				{
					return "endpoint";
				}
			}

			public string Intersection //交点
			{
				set 
				{ 
					intersection = value; 
				}
				get 
				{
					return "intersection";
				}
			}
		}
		#endregion

		#region//捕捉模式（标识捕捉类型是否打开）
		public struct BoolSnapMode
		{
			bool bPartBoundary ;//边线
			bool bPartVertex;//节点
			bool bEndpoint;//端点
			bool bIntersection;//交点

			public bool PartBoundary//边 线
			{
				set
				{ 
					bPartBoundary = value;
				}
				get 
				{
					return bPartBoundary;
				}
			}

			public bool PartVertex //节点
			{
				set 
				{ 
					bPartVertex = value; 
				}
				get 
				{
					return bPartVertex;
				}
			}
			public bool Endpoint //端点
			{
				set 
				{ 
					bEndpoint = value; 
				}
				get 
				{
					return bEndpoint;
				}
			}
			public bool Intersection //交点
			{
				set 
				{ 
					bIntersection = value; 
				}
				get 
				{
					return bIntersection;
				}
			}
		}
		#endregion	
	
		//定义一种数据结构，方便将信息存入数组中
		public struct FeatureLayerSnap
		{
			public IFeatureLayer pFeatureLayer;
			public bool bSnap;
			
			public IFeatureLayer FeatureLayer
			{
				get 
				{
					return pFeatureLayer;
				}
				set 
				{
					pFeatureLayer = value;
				}
			}
			public bool IsSnap
			{
				get 
				{
					return bSnap;
				}
				set 
				{
					bSnap = value;
				}
			}
		}
	}
}
