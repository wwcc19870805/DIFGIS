using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DFileConvert.Class
{
    public class CommonFunction
    {
        #region 判断输入的字符串是否是一个数字  2007.10.18 TianK 添加
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClipboardText"></param>
        /// <returns></returns>	
        public static bool MatchNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex objNumberPattern = new System.Text.RegularExpressions.Regex("[^0-9.-]");
            return !objNumberPattern.IsMatch(strNumber);
        }
        #endregion

        #region 为几何形体增加Z值和M值
        /// <summary>
        /// 为几何形体增加Z值和M值
        /// </summary>
        /// <param name="pGeo">存储几何形体</param>
        /// <param name="pPointArray">用于更新几何形体Z值、M值的点数组</param>
        public static void AddZMValueForGeometry(ref IGeometry pGeo, IArray pPointArray)
        {
            IPointCollection pPointCol = (IPointCollection)pGeo;

            for (int j = 0; j < pPointCol.PointCount; j++)
            {
                IPoint pPoint = pPointCol.get_Point(j);
                IZAware pZ = (IZAware)pPoint;
                pZ.ZAware = true;
                pPoint.Z = 0;
                IMAware pM = (IMAware)pPoint;
                pM.MAware = true;
                pPoint.M = 0;

                for (int k = 0; k < pPointArray.Count; k++)
                {
                    IPoint pTempPoint = (IPoint)pPointArray.get_Element(k);
                    if (Math.Abs(pPoint.X - pTempPoint.X) < 0.001 && Math.Abs(pPoint.Y - pTempPoint.Y) < 0.001 && pTempPoint.Z != -999)
                    {
                        pPoint.Z = pTempPoint.Z;
                        pPoint.M = pTempPoint.M;
                        break;
                    }
                }
                pPointCol.UpdatePoint(j, pPoint);

            }

            IZAware pG = (IZAware)pGeo;
            pG.ZAware = true;
            pGeo = (IGeometry)pPointCol;

        }
        #endregion

        #region 获得Domain的值   2009.3.28  TianK   添加
        /// <summary>
        /// 获得Domain的值
        /// </summary>
        /// <param name="pDomain"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public static string GetCodedValueDomainValue(ICodedValueDomain pDomain, string domainDescription)
        {
            string result = "<Null>";
            for (int i = 0; i < pDomain.CodeCount; i++)
            {
                if (pDomain.get_Name(i) == domainDescription)
                {
                    result = pDomain.get_Value(i).ToString();
                    break;
                }
            }
            return result;

        }
        #endregion
    }
}
