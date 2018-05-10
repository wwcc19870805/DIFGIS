using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;

namespace DF2DTool.Class
{
    class clsUpDatalineMerge
    {
        private static string GEOOBJNUM = "GEOOBJNUM";    //地物编码字段名
        private static string GEOGUID = "GeoGUID";      //唯一标示符字段名
        private static string GEOOBJNUMPO = "850002,850102,851112";      //需要特殊处理的坡的编码

        #region //合并要素类中GUID码相同的要素
        /// <summary>
        /// 合并要素类中GUID码相同的要素
        /// </summary>
        /// <param name="pFeaClass">要合并的要素</param>
        /// <param name="inArGEOOGJNUM">多部件的地物编码</param>
        /// <param name="pGeoC">只合并此范围内的要素</param>
        public static void FeatureClassMerge(IFeatureClass pFeaClass, IGeometryCollection pGeoC, ArrayList inArGEOOGJNUM, double douTolerence)
        {
            IQueryFilter pFilter = new QueryFilterClass();
            IFeatureCursor pFeaCursor = null;
            IFeature pfea;
            int index = pFeaClass.FindField(GEOOBJNUM);

            ArrayList arrUniqueFea = new ArrayList();     //所有唯一值集合
            ArrayList arrMergeFea = new ArrayList();      //待合并的要素
            ArrayList arrMergedFea = new ArrayList();     //已经合并的要素，等待下一步删除

            arrUniqueFea = GetUniqueValue(pFeaClass, GEOGUID, pGeoC, douTolerence);

            for (int i = 0; i < arrUniqueFea.Count; i++)
            {
                pFilter.WhereClause = String.Format("{0}='{1}'", GEOGUID, arrUniqueFea[i]);
                pFeaCursor = pFeaClass.Update(pFilter, false);
                arrMergeFea.Clear();

                pfea = pFeaCursor.NextFeature();
                while (pfea != null)
                {
                    arrMergeFea.Add(pfea);
                    pfea = pFeaCursor.NextFeature();
                }
                if (arrMergeFea.Count > 1)
                {
                    FeatureMerge(arrMergeFea, ref arrMergedFea, pGeoC, inArGEOOGJNUM, index);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);

            }

            for (int k = 0; k < arrMergedFea.Count; k++)    //删除已经合并的要素
            {
                (arrMergedFea[k] as IFeature).Delete();
            }
        }
        #endregion

        #region //合并同类要素
        /// <summary>
        /// 合并同类要素
        /// </summary>
        /// <param name="arrFeature"></param>
        private static void FeatureMerge(ArrayList arrFeature, ref ArrayList arrMergedFea, IGeometryCollection pGeoC, ArrayList inArGEOOGJNUM, int index)
        {
            IFeature pMergeFea;
            ITopologicalOperator pTopOperator;
            IRelationalOperator pRelOperator;
            IRelationalOperator pRelOp;
            IGeometry pGeo;

            pRelOperator = (pGeoC as ITopologicalOperator).Boundary as IRelationalOperator;

            IFeature pOrigFea = (IFeature)arrFeature[0];
            IGeometry pOrigGeometry = (IGeometry)pOrigFea.Shape;
            int pOrigGeometryCount = (pOrigGeometry as IGeometryCollection).GeometryCount;

            if (inArGEOOGJNUM.Contains(pOrigFea.get_Value(index).ToString()))     //如果该地物可以是多部件，用添加多部件方法合并
            {
                for (int i = 1; i < arrFeature.Count; i++)
                {
                    pMergeFea = (IFeature)arrFeature[i];
                    pOrigGeometry = UnionGeometry(pOrigGeometry, (IGeometry)pMergeFea.Shape);     //调用合并多部件方法
                    arrMergedFea.Add(pMergeFea);
                }
            }
            else                 //否则不能为多部件，就用union方法合并
            {
                for (int j = 1; j < arrFeature.Count; j++)
                {
                    pRelOp = pOrigGeometry as IRelationalOperator;
                    pMergeFea = (IFeature)arrFeature[j];
                    pGeo = (IGeometry)pMergeFea.Shape;
                    pTopOperator = pOrigGeometry as ITopologicalOperator;

                    if (pOrigGeometry.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        //if (pRelOp.Touches(pGeo))    //20140620增加判断是否相接条件
                        //{
                        pOrigGeometry = pTopOperator.Union(pGeo);
                        arrMergedFea.Add(pMergeFea);
                        //}
                    }
                    else if (pOrigGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        if (pRelOp.Contains(pGeo))
                        {
                            pOrigGeometry = pTopOperator.Difference(pGeo);
                            arrMergedFea.Add(pMergeFea);
                        }
                        else if (pRelOp.Within(pGeo))
                        {
                            pOrigGeometry = (pGeo as ITopologicalOperator).Difference(pOrigGeometry);
                            arrMergedFea.Add(pMergeFea);
                        }
                        else if (pRelOp.Touches(pGeo))
                        {
                            pOrigGeometry = pTopOperator.Union(pGeo);
                            arrMergedFea.Add(pMergeFea);
                        }
                    }
                }
            }
            if (pRelOperator.Crosses(pOrigGeometry))    //如果合并后的对象和接边线相交，删掉接边儿点
            {
                DeleteJieBianPoint(ref pOrigGeometry, (pGeoC as ITopologicalOperator).Boundary);    // 删除接边线处的点
            }

            //if (GEOOBJNUMPO.IndexOf (pOrigFea.get_Value(index).ToString())>0)     //如果该地物是坡
            //{
            //    pOrigGeometry=ProcessPoXian(pOrigGeometry);
            //}
            pOrigFea.Shape = pOrigGeometry;
            pOrigFea.Store();
        }
        #endregion

        #region//合并两个几何形体
        /// <summary>
        /// 合并两个几何形体
        /// </summary>
        /// <param name="pGeo"></param>
        /// <param name="pOtherGeo"></param>
        /// <returns></returns>
        private static IGeometry UnionGeometry(IGeometry Merge, IGeometry pOtherGeo)
        {
            IGeometryCollection geometryCollection1 = Merge as IGeometryCollection;
            IGeometryCollection geometryCollection2 = pOtherGeo as IGeometryCollection;

            try
            {
                geometryCollection1.AddGeometryCollection(geometryCollection2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return geometryCollection1 as IGeometry;

        }
        #endregion

        //#region//合并多部件线要素
        ///// <summary>
        ///// 合并两个几何形体
        ///// </summary>
        ///// <param name="pGeo"></param>
        ///// <param name="pOtherGeo"></param>
        ///// <returns></returns>
        //private static IGeometry UnionGeometryCollection(IGeometry Merge, IGeometry pOtherGeo)
        //{
        //    IPolyline pPolyLine1 = new PolylineClass(), pPolyLine2 = new PolylineClass();
        //    IGeometryCollection geometryCollection1 = Merge as IGeometryCollection;
        //    IGeometryCollection geometryCollection2 = pOtherGeo as IGeometryCollection;
        //    IGeometryCollection geometryCollection;
        //    IGeometryCollection pPolyLine = new PolylineClass();
        //    IRelationalOperator pRelOperator;
        //    ITopologicalOperator pTopOperator;
        //    object missing = Type.Missing;

        //    if (geometryCollection1.GeometryCount > 1 && geometryCollection2.GeometryCount == 1)
        //    {
        //        geometryCollection = geometryCollection1;
        //        (pPolyLine2 as IGeometryCollection).AddGeometryCollection (geometryCollection2);
        //    }
        //    else if (geometryCollection1.GeometryCount == 1 && geometryCollection2.GeometryCount > 1)
        //    {
        //        geometryCollection = geometryCollection2;
        //        (pPolyLine2 as IGeometryCollection).AddGeometryCollection(geometryCollection1);
        //    }
        //    else
        //    {
        //        return UnionGeometry(Merge, pOtherGeo);
        //    }

        //    pRelOperator = pPolyLine2 as IRelationalOperator;
        //    pTopOperator = pPolyLine2 as ITopologicalOperator;

        //    for (int i = 0; i < geometryCollection.GeometryCount; i++)
        //    {
        //        pPolyLine1 = new PolylineClass();
        //        IPath pPH = geometryCollection.get_Geometry(i) as IPath;
        //        (pPolyLine1 as ISegmentCollection).AddSegmentCollection (pPH as ISegmentCollection);
        //        if (pPolyLine1 != null)
        //        {
        //            pTopOperator.Simplify();
        //            (pPolyLine1 as ITopologicalOperator).Simplify();
        //            if (pRelOperator.Touches(pPolyLine1 as IGeometry))
        //            {
        //                pTopOperator.Union(pPolyLine1 as IGeometry);
        //            }
        //            else
        //            {
        //                pPolyLine.AddGeometryCollection(pPolyLine1 as IGeometryCollection);
        //            }
        //        }
        //    }
        //    pPolyLine.AddGeometryCollection(pPolyLine2 as IGeometryCollection);

        //    return pPolyLine as IGeometry;

        //}
        //#endregion

        #region //得到指定字段组合构成的唯一值
        /// <summary>
        /// 得到指定字段组合构成的唯一值
        /// </summary>
        /// <param name="pTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns>成员为string数组的arraylist</returns>
        private static ArrayList GetUniqueValue(IFeatureClass pFeatureClass, string fieldName, IGeometryCollection pGeoC, double douTolerence)
        {
            ArrayList result = new ArrayList();
            ITopologicalOperator pTopoOp;
            ISpatialFilter pSFilter = new SpatialFilterClass(); ;
            IGeometry pPolyBuff = new PolygonClass();

            if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                ITopologicalOperator ptop = pGeoC as ITopologicalOperator;
                pGeoC = ptop.Boundary as IGeometryCollection;
            }

            // 得到IFeatureCursor游标 
            pTopoOp = (ITopologicalOperator)pGeoC;
            pPolyBuff = pTopoOp.Buffer(douTolerence);
            pSFilter.Geometry = pPolyBuff;
            pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            IFeatureCursor pFCursor = pFeatureClass.Search(pSFilter, false);

            // coClass对象实例生成 
            IDataStatistics pData = new DataStatisticsClass();
            pData.Field = fieldName;
            pData.Cursor = pFCursor as ICursor;

            // 枚举唯一值 
            IEnumerator pEnumVar = pData.UniqueValues;
            pEnumVar.Reset();

            while (pEnumVar.MoveNext())
            {
                result.Add(pEnumVar.Current.ToString());
            }

            return result;
        }
        #endregion

        #region 删除线或面在接边儿线处的多余点  2013.8.23 TianKuo 添加
        /// <summary
        /// 删除线或面在接边儿线处的多余点  2013.8.23 TianKuo 添加
        /// </summary>
        /// <param name="pFCursor">接边儿要素集合对象</param>
        /// <param name="pGeo">接边面边界</param>
        private static void DeleteJieBianPoint(ref IGeometry pGeoDelPoint, IGeometry pGeo)
        {
            IPointCollection pPointCBuFen;
            IGeometryCollection pGeoCollection;
            IPoint pPt, pPoint;
            ITopologicalOperator pTopoOp = pGeo as ITopologicalOperator;
            ITopologicalOperator pTopoOp1;
            bool isFind = false;

            IPointCollection pPointIntersect = null;
            pTopoOp1 = pGeoDelPoint as ITopologicalOperator;
            //pTopoOp1.Simplify();
            pGeoDelPoint.SnapToSpatialReference();
            try
            {
                pPointIntersect = pTopoOp.Intersect((IGeometry)pTopoOp1, esriGeometryDimension.esriGeometry0Dimension) as IPointCollection;
            }
            catch
            {
                Console.WriteLine("空间查询出错！");
            }
            if (pPointIntersect.PointCount > 0)
            {
                for (int i = 0; i < pPointIntersect.PointCount; i++)   //循环每一个交点
                {
                    pPt = pPointIntersect.get_Point(i);
                    isFind = false;
                    pGeoCollection = pGeoDelPoint as IGeometryCollection;
                    for (int k = 0; k < pGeoCollection.GeometryCount; k++)    //循环每一个部件
                    {
                        pPointCBuFen = pGeoCollection.get_Geometry(k) as IPointCollection;
                        if (pGeoDelPoint.GeometryType == esriGeometryType.esriGeometryPolygon)
                        {
                            for (int j = 0; j < pPointCBuFen.PointCount; j++)   //面的起点或终点可以删除
                            {
                                pPoint = pPointCBuFen.get_Point(j);
                                if (Math.Abs(pPt.X - pPoint.X) < 0.005 && Math.Abs(pPt.Y - pPoint.Y) < 0.005)
                                {
                                    pPointCBuFen.RemovePoints(j, 1);    //删除接边儿点
                                    isFind = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 1; j < pPointCBuFen.PointCount - 1; j++)   //线的起点和终点即使是接边儿点儿，也不能删除
                            {
                                pPoint = pPointCBuFen.get_Point(j);
                                if (Math.Abs(pPt.X - pPoint.X) < 0.005 && Math.Abs(pPt.Y - pPoint.Y) < 0.005)
                                {
                                    pPointCBuFen.RemovePoints(j, 1);    //删除接边儿点
                                    isFind = true;
                                    break;
                                }
                            }
                        }
                        if (isFind)
                        {
                            break;
                        }
                    }

                }
            }

        }
        #endregion

        #region//处理坡线

        private static IGeometry ProcessPoXian(IGeometry Merge)
        {
            IPolyline pPolyLine1 = new PolylineClass();
            ISegmentCollection pPolyLine2 = new PolylineClass();
            ISegment pSegment;
            ISegmentCollection segmentCollection = Merge as ISegmentCollection;
            IGeometryCollection pPolyLine = new PolylineClass();
            IRelationalOperator pRelOperator;
            object missing = Type.Missing;
            string mergedID = ",";

            for (int i = 0; i < segmentCollection.SegmentCount; i++)
            {
                if (mergedID.IndexOf("," + i.ToString() + ",") >= 0)
                {
                    continue;
                }
                pPolyLine1 = new PolylineClass();
                (pPolyLine1 as ISegmentCollection).AddSegment(segmentCollection.get_Segment(i), ref missing, ref missing);
                pRelOperator = pPolyLine1 as IRelationalOperator;

                for (int j = i + 1; j < segmentCollection.SegmentCount; j++)
                {
                    if (mergedID.IndexOf("," + j.ToString() + ",") < 0)
                    {
                        pSegment = segmentCollection.get_Segment(j);
                        pPolyLine2.AddSegment(pSegment, ref missing, ref missing);
                        if (pRelOperator.Touches(pPolyLine2 as IGeometry) && IsAngle180(pPolyLine1, pSegment))
                        {
                            (pPolyLine1 as ISegmentCollection).AddSegment(pSegment, ref missing, ref missing);
                            mergedID = mergedID + j.ToString() + ",";
                        }
                    }
                }
                (pPolyLine1 as ITopologicalOperator).Simplify();
                (pPolyLine1 as IPolycurve).Generalize(0.02);

                pPolyLine.AddGeometryCollection(pPolyLine1 as IGeometryCollection);
            }

            return pPolyLine as IGeometry;
        }
        #endregion

        #region//计算相接线段之间的夹角是否接近180度
        private static bool IsAngle180(IPolyline pPolyLine, ISegment pSegment)
        {
            IRelationalOperator pRelOperator;
            ISegmentCollection pSegC = pPolyLine as ISegmentCollection;
            ISegment pSeg, pSegStart, pSegEnd;
            pSegStart = pSegC.get_Segment(0);
            pSegEnd = pSegC.get_Segment(pSegC.SegmentCount - 1);

            ISegmentCollection pPolyStart = new PolylineClass();
            ISegmentCollection pPolyEnd = new PolylineClass();
            ISegmentCollection pPolyL = new PolylineClass();
            object missing = Type.Missing;

            pPolyL.AddSegment(pSegment, ref missing, ref missing);
            pPolyStart.AddSegment(pSegStart, ref missing, ref missing);
            pPolyEnd.AddSegment(pSegEnd, ref missing, ref missing);

            pRelOperator = pPolyL as IRelationalOperator;
            if (pRelOperator.Touches(pPolyEnd as IGeometry))
            {
                pSeg = pSegEnd;
            }
            else if (pRelOperator.Touches(pPolyStart as IGeometry))
            {
                pSeg = pSegStart;
            }
            else
            {
                return false;
            }
            double k1, k2, k;
            k1 = Math.Abs((pSeg.FromPoint.Y - pSeg.ToPoint.Y) / (pSeg.FromPoint.X - pSeg.ToPoint.X));
            k2 = Math.Abs((pSegment.FromPoint.Y - pSegment.ToPoint.Y) / (pSegment.FromPoint.X - pSegment.ToPoint.X));
            k = k1 / k2;
            if (k > 0.8 && k < 1.2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
