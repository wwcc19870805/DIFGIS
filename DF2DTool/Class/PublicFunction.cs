using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using System.Collections;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Drawing;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using DF2DTool.Class;
using ESRI.ArcGIS.Controls;
using System.Data;
using System.IO;

namespace DF2DTool
{
     class PublicFunction
    {
        public static double BufferArea; //-----------------------By 袁怀月---------------

        #region 将一个地图中的所有图层加入Hashtable 或 ArrayList
        /// <summary>
        /// 将所有的FeatureLayer放入Hashtable
        /// </summary>
        /// <returns></returns>
        public static object GetAllFeatureLayer(IMap pMap, Type type)
        {
            if (type == typeof(Hashtable))
            {
                Hashtable ht = new Hashtable();
                PublicFunction.GetAllFeatureLayer(pMap, ht);
                return ht;
            }
            else if (type == typeof(ArrayList))
            {
                ArrayList result = new ArrayList();
                PublicFunction.GetAllFeatureLayer(pMap, result);
                return result;
            }
            else
            {
                MessageBox.Show("指定的类型不是ArrayList或Hashtable。");
                return null;
            }
        }

        /// <summary>
        /// 将一个地图中的所有图层加入Hashtable
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        public static void GetAllFeatureLayer(object obj, Hashtable ht)
        {
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    GetAllFeatureLayer(pMap.get_Layer(i), ht);
                }
            }
            else if (obj is IGroupLayer)
            {
                IGroupLayer pGroupLayer = obj as IGroupLayer;
                if (pGroupLayer.Visible == true)
                {
                    ICompositeLayer comLayer = obj as ICompositeLayer;
                    for (int i = 0; i < comLayer.Count; i++)
                    {
                        GetAllFeatureLayer(comLayer.get_Layer(i), ht);
                    }
                }
            }
            else if ((obj is IFeatureLayer) && !(obj is IAnnotationLayer) && !(obj is ICadLayer))
            {
                if (ht != null)
                {
                    IFeatureLayer pFeaLay = obj as IFeatureLayer;
                    if (ht[pFeaLay.FeatureClass.FeatureClassID] == null && pFeaLay.Visible == true) //--------By 袁怀月--------------
                    {
                        ht.Add(pFeaLay.FeatureClass.FeatureClassID, pFeaLay);
                    }
                }
            }
        }

        /// <summary>
        /// 将一个地图中的所有图层加入Hashtable
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        public static void GetAllFeatureLayer(object obj, ArrayList list)
        {
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    GetAllFeatureLayer(pMap.get_Layer(i), list);
                }
            }
            else if (obj is IGroupLayer)
            {
                IGroupLayer pGroupLayer = obj as IGroupLayer;

                if (pGroupLayer.Visible == false) return;

                ICompositeLayer comLayer = obj as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    GetAllFeatureLayer(comLayer.get_Layer(i), list);
                }
            }
            else if ((obj is IFeatureLayer) && !(obj is IAnnotationLayer) && !(obj is ICadLayer))
            {
                if (list != null)
                {
                    IFeatureLayer pFeaLay = obj as IFeatureLayer;
                    if (!list.Contains(pFeaLay) && pFeaLay.Visible == true)//--------By 袁怀月--------------
                    {
                        list.Add(pFeaLay);
                    }
                }
            }
        }
        #endregion

        #region 将一个地图中的所有图层（含注记层）加入Hashtable 或 ArrayList， 包括所有不可见图层//---------Add By 罗璇 2008.11.10-----------------
        /// <summary>
        /// 将所有的FeatureLayer放入Hashtable
        /// </summary>
        /// <returns></returns>
        public static object GetAllLayersFromMap(IMap pMap, Type type)
        {
            if (type == typeof(Hashtable))
            {
                Hashtable ht = new Hashtable();
                PublicFunction.GetAllLayersFromMap(pMap, ht);
                return ht;
            }
            else if (type == typeof(ArrayList))
            {
                ArrayList result = new ArrayList();
                PublicFunction.GetAllLayersFromMap(pMap, result);
                return result;
            }
            else
            {
                MessageBox.Show("指定的类型不是ArrayList或Hashtable。");
                return null;
            }
        }

        /// <summary>
        /// 将一个地图中的所有图层加入Hashtable
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        public static void GetAllLayersFromMap(object obj, Hashtable ht)
        {
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    GetAllLayersFromMap(pMap.get_Layer(i), ht);
                }
            }
            else if (obj is IGroupLayer)
            {
                ICompositeLayer comLayer = obj as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    GetAllLayersFromMap(comLayer.get_Layer(i), ht);
                }
            }
            //else if ((obj is IFeatureLayer) && !(obj is IAnnotationLayer) && !(obj is ICadLayer))
            else if ((obj is IFeatureLayer) && !(obj is ICadLayer))//---------By 罗璇---------
            {
                if (ht != null)
                {
                    IFeatureLayer pFeaLay = obj as IFeatureLayer;
                    if (ht[pFeaLay.FeatureClass.FeatureClassID] == null) //--------By 袁怀月--------------
                    {
                        ht.Add(pFeaLay.FeatureClass.FeatureClassID, pFeaLay);
                    }
                }
            }
        }

        /// <summary>
        /// 将一个地图中的所有图层加入ArrayList
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        public static void GetAllLayersFromMap(object obj, ArrayList list)
        {
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    GetAllLayersFromMap(pMap.get_Layer(i), list);
                }
            }
            else if (obj is IGroupLayer)
            {
                ICompositeLayer comLayer = obj as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    GetAllLayersFromMap(comLayer.get_Layer(i), list);
                }
            }
            //else if ((obj is IFeatureLayer) && !(obj is IAnnotationLayer) && !(obj is ICadLayer))
            else if ((obj is IFeatureLayer) && !(obj is ICadLayer))//---------By 罗璇---------
            {
                if (list != null)
                {
                    IFeatureLayer pFeaLay = obj as IFeatureLayer;
                    if (!list.Contains(pFeaLay))//--------By 袁怀月--------------
                    {
                        list.Add(pFeaLay);
                    }
                }
            }
        }
        #endregion

        #region 将一个地图中所有当前比例尺下可见的图层（含注记层）加入Hashtable 或 ArrayList//---------Add By 罗璇 2008.11.10-----------------
        /// <summary>
        /// 将所有的FeatureLayer放入Hashtable
        /// </summary>
        /// <returns></returns>
        public static object GetCurVisibleLayers(IMap pMap, Type type)
        {
            double dblScale = pMap.MapScale;
            if (type == typeof(Hashtable))
            {
                Hashtable ht = new Hashtable();
                PublicFunction.GetCurVisibleLayers(pMap, ht, dblScale);
                return ht;
            }
            else if (type == typeof(ArrayList))
            {
                ArrayList result = new ArrayList();
                PublicFunction.GetCurVisibleLayers(pMap, result, dblScale);
                return result;
            }
            else
            {
                MessageBox.Show("指定的类型不是ArrayList或Hashtable。");
                return null;
            }
        }

        /// <summary>
        /// 将一个地图中的所有图层加入Hashtable
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        public static void GetCurVisibleLayers(object obj, Hashtable ht, double dblScale)
        {
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    GetCurVisibleLayers(pMap.get_Layer(i), ht, dblScale);
                }
            }
            else if (obj is IGroupLayer)
            {
                IGroupLayer pGroupLayer = obj as IGroupLayer;
                if (pGroupLayer.Visible && ((pGroupLayer.MaximumScale == 0 && pGroupLayer.MinimumScale == 0)
                    || (pGroupLayer.MaximumScale == 0 && dblScale < pGroupLayer.MinimumScale)
                    || (pGroupLayer.MinimumScale == 0 && dblScale > pGroupLayer.MaximumScale)
                    || (pGroupLayer.MaximumScale < dblScale && dblScale < pGroupLayer.MinimumScale)))
                {
                    ICompositeLayer comLayer = obj as ICompositeLayer;
                    for (int i = 0; i < comLayer.Count; i++)
                    {
                        GetCurVisibleLayers(comLayer.get_Layer(i), ht, dblScale);
                    }
                }
            }
            //else if ((obj is IFeatureLayer) && !(obj is IAnnotationLayer) && !(obj is ICadLayer))
            else if ((obj is IFeatureLayer) && !(obj is ICadLayer))//---------By 罗璇---------
            {
                IFeatureLayer pFeaLay = obj as IFeatureLayer;
                if (pFeaLay.Visible && ((pFeaLay.MaximumScale == 0 && pFeaLay.MinimumScale == 0)
                    || (pFeaLay.MaximumScale == 0 && dblScale < pFeaLay.MinimumScale)
                    || (pFeaLay.MinimumScale == 0 && dblScale > pFeaLay.MaximumScale)
                    || (pFeaLay.MaximumScale < dblScale && dblScale < pFeaLay.MinimumScale)))
                {
                    if (ht != null && ht[pFeaLay.FeatureClass.FeatureClassID] == null)
                    {
                        ht.Add(pFeaLay.FeatureClass.FeatureClassID, pFeaLay);
                    }
                }
            }
        }

        /// <summary>
        /// 将一个地图中的所有图层加入ArrayList
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        public static void GetCurVisibleLayers(object obj, ArrayList list, double dblScale)
        {
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    GetCurVisibleLayers(pMap.get_Layer(i), list, dblScale);
                }
            }
            else if (obj is IGroupLayer)
            {
                IGroupLayer pGroupLayer = obj as IGroupLayer;
                if (pGroupLayer.Visible && ((pGroupLayer.MaximumScale == 0 && pGroupLayer.MinimumScale == 0)
                    || (pGroupLayer.MaximumScale == 0 && dblScale < pGroupLayer.MinimumScale)
                    || (pGroupLayer.MinimumScale == 0 && dblScale > pGroupLayer.MaximumScale)
                    || (pGroupLayer.MaximumScale < dblScale && dblScale < pGroupLayer.MinimumScale)))
                {
                    ICompositeLayer comLayer = obj as ICompositeLayer;
                    for (int i = 0; i < comLayer.Count; i++)
                    {
                        GetCurVisibleLayers(comLayer.get_Layer(i), list, dblScale);
                    }
                }
            }
            //else if ((obj is IFeatureLayer) && !(obj is IAnnotationLayer) && !(obj is ICadLayer))
            else if ((obj is IFeatureLayer) && !(obj is ICadLayer))//---------By 罗璇---------
            {
                IFeatureLayer pFeaLay = obj as IFeatureLayer;
                if (pFeaLay.Visible && ((pFeaLay.MaximumScale == 0 && pFeaLay.MinimumScale == 0)
                    || (pFeaLay.MaximumScale == 0 && dblScale < pFeaLay.MinimumScale)
                    || (pFeaLay.MinimumScale == 0 && dblScale > pFeaLay.MaximumScale)
                    || (pFeaLay.MaximumScale < dblScale && dblScale < pFeaLay.MinimumScale)))
                {
                    if (list != null && !list.Contains(pFeaLay))
                    {
                        list.Add(pFeaLay);
                    }
                }
            }
        }
        #endregion

        #region 根据Geometry生成缓冲区
        /// <summary>
        /// 根据Geometry生成缓冲区
        /// </summary>
        /// <param name="pGeo"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static IGeometry DoBuffer(IGeometry pGeo, double distance)
        {
            try
            {
                ITopologicalOperator ptopo;
                ptopo = pGeo as ITopologicalOperator;
                if (ptopo != null)
                {
                    IGeometry geo = ptopo.Buffer(distance);
                    return geo;
                }
            }
            catch
            {
                //                //“线”“面”上如果几个点坐标相同，buffer就会失败
                //                Console.WriteLine(ex.Message+"\r\n"+ex.StackTrace);
                //                ShowGeometryAllPoints(pGeo);
            }
            return null;
        }
        #endregion

        #region 在控制台显示Geometry的所有节点信息
        /// <summary>
        /// 显示Geometry的所有节点
        /// </summary>
        /// <param name="pGeo"></param>
        public static void ShowGeometryAllPoints(IGeometry pGeo)
        {
            try
            {
                Console.WriteLine(GetGeometryInfo(pGeo));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        #endregion

        #region 得到一个IGeometry的坐标信息
        /// <summary>
        /// 得到一个IGeometry的坐标信息
        /// </summary>
        /// <param name="pGeo"></param>
        /// <returns></returns>
        public static string GetGeometryInfo(IGeometry pGeo)
        {
            string result = "";
            string sep = "\r\n";
            try
            {
                if (pGeo.IsEmpty)
                {
                    result = "没有坐标信息";
                }
                else
                {
                    IPoint pPoint = null;
                    IPointCollection pPointColl = null;
                    IGeometryCollection pGeoColl = null;
                    if (pGeo.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        pPoint = pGeo as IPoint;
                        result = GetPointInfo(pPoint);
                    }
                    else
                    {
                        pGeoColl = (IGeometryCollection)pGeo;

                        for (int i = 0; i < pGeoColl.GeometryCount; i++)
                        {
                            if (pGeoColl.GeometryCount > 1)
                            {
                                result = "第 " + (i + 1) + " 部分" + sep;
                            }
                            pGeo = pGeoColl.get_Geometry(i);
                            if (pGeo.GeometryType == esriGeometryType.esriGeometryPoint)
                            {
                                pPoint = (IPoint)pGeo;
                                result = "";
                                result += GetPointInfo(pPoint);
                            }
                            else
                            {
                                pPointColl = (IPointCollection)pGeo;
                                for (int j = 0; j < pPointColl.PointCount; j++)
                                {
                                    pPoint = pPointColl.get_Point(j);
                                    result += "第 " + (j + 1) + " 个点" + sep;
                                    result += GetPointInfo(pPoint) + sep;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            return result;
        }
        #endregion

        #region 得到一个点的坐标信息
        /// <summary>
        /// 得到一个点的坐标信息
        /// </summary>
        /// <param name="pPoint"></param>
        /// <returns></returns>
        private static string GetPointInfo(IPoint pPoint)
        {
            string pointInfo = "";
            string sep = "\r\n";
            if (pPoint == null)
            {
                pointInfo = "坐标信息为空";
            }
            else
            {
                pointInfo = "X坐标： " + pPoint.X.ToString("0.000") + sep;
                pointInfo += "Y坐标： " + pPoint.Y.ToString("0.000") + sep;
                pointInfo += "Z高程： " + pPoint.Z.ToString("0.000") + sep;
                pointInfo += "M埋深： " + pPoint.M.ToString("0.000");
            }
            return pointInfo;
        }
        #endregion

        #region 屏幕单位到地图单位的转换
        /// <summary>
        /// 屏幕单位到地图单位的转换
        /// </summary>
        /// <param name="pActiveView">IActiveView</param>
        /// <param name="pixelUnits">象素距离</param>
        /// <returns></returns>
        public static double ConvertPixelsToMapUnits(IActiveView pActiveView, double pixelUnits)
        {
            double realWorldDisplayExtent;
            int pixelExtent;
            double sizeOfOnePixel;

            pixelExtent = pActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right -
                pActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;

            realWorldDisplayExtent = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            //激活视图的地图单位距离
            //激活视图的屏幕象素距离
            sizeOfOnePixel = realWorldDisplayExtent / pixelExtent;
            //每个象素对应的地图单位距离
            return pixelUnits * sizeOfOnePixel;
            //pixelUnits个象素对应的地图单位距离
        }
        #endregion

        #region MS和ESRI颜色转换
        /// <summary>
        /// MS颜色转换到ESRI颜色
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static IColor FromMSColor(Color clr)
        {
            RgbColorClass rgb = new RgbColorClass();
            rgb.Red = clr.R;
            rgb.Green = clr.G;
            rgb.Blue = clr.B;
            rgb.Transparency = clr.A;

            return rgb as IColor;
        }

        /// <summary>
        /// MS颜色值转换到ESRI颜色
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static IColor FromMSColor(int rgb)
        {
            Color clr = Color.FromArgb(rgb);
            return FromMSColor(clr);
        }
        #endregion

        #region 选择集的颜色
        /// <summary>
        /// 选择集的颜色
        /// </summary>
        /// <returns></returns>
        public static IColor GetSelectionColor(int argb)
        {
            return PublicFunction.FromMSColor(argb);
        }
        #endregion

        #region 将Envelope转换为Polygon
        /// <summary>
        /// 将Envelope转换为Polygon
        /// </summary>
        /// <param name="pEnvelope"></param>
        /// <returns></returns>
        public static IPolygon GetPolygonFromEnvelope(IEnvelope pEnvelope)
        {
            object o = Type.Missing;
            PolygonClass pPolygon = new PolygonClass();
            IPoint pPoint1, pPoint2, pPoint3, pPoint4;
            pPoint1 = new PointClass();
            pPoint2 = new PointClass();
            pPoint3 = new PointClass();
            pPoint4 = new PointClass();
            pPoint1.PutCoords(pEnvelope.XMin, pEnvelope.YMin);
            pPoint2.PutCoords(pEnvelope.XMin, pEnvelope.YMax);
            pPoint3.PutCoords(pEnvelope.XMax, pEnvelope.YMax);
            pPoint4.PutCoords(pEnvelope.XMax, pEnvelope.YMin);
            pPolygon.AddPoint(pPoint1, ref o, ref o);
            pPolygon.AddPoint(pPoint2, ref o, ref o);
            pPolygon.AddPoint(pPoint3, ref o, ref o);
            pPolygon.AddPoint(pPoint4, ref o, ref o);
            return pPolygon;
        }
        #endregion

        #region 得到一个图层所处的工作空间
        /// <summary>
        /// 得到一个图层所处的工作空间
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <returns></returns>
        private static IWorkspace GetWorkspace(IFeatureLayer pFeaLay)
        {
            return (pFeaLay.FeatureClass as IDataset).Workspace;
        }
        #endregion

        #region 得到指定字段组合构成的唯一值
        /// <summary>
        /// 得到指定字段组合构成的唯一值
        /// </summary>
        /// <param name="pTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns>成员为string数组的arraylist</returns>
        public static ArrayList GetUniqueValue(ITable pTable, string[] fieldNames)
        {
            string sep = ", ";
            int[] fieldIndexs = new int[fieldNames.Length];
            ArrayList result = new ArrayList();
            IQueryFilter pFilter = new QueryFilterClass();

            string strSQL = " distinct ";
            for (int i = 0; i < fieldNames.Length; i++)
            {
                fieldIndexs[i] = pTable.FindField(fieldNames[i]);
                strSQL += fieldNames[i] + sep;
            }
            pFilter.SubFields = strSQL.Remove(strSQL.Length - 2, 2);
            ICursor pCur = pTable.Search(pFilter, false);
            IRow pRow = pCur.NextRow();
            while (pRow != null)
            {
                string val = "";
                for (int i = 0; i < fieldIndexs.Length; i++)
                {
                    val += ConvertNull(pRow.get_Value(fieldIndexs[i])) + sep;
                }
                val = val.Remove(val.Length - 2, 2);
                if (!result.Contains(val))
                {
                    result.Add(val);
                }
                pRow = pCur.NextRow();
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCur);
            return result;
        }
        #endregion

        #region 空值转换为字符串
        /// <summary>
        /// 空值转换为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertNull(object obj)
        {
            try
            {
                if (obj == null || obj == System.DBNull.Value)
                {
                    return ""; //return "<Null>"; 修改 by 袁怀月 2007-10-05
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return "";
            }
        }
        #endregion

//         #region 根据查询条件和结果保存方式刷新图层的选择集
//         /// <summary>
//         /// 根据查询条件和结果保存方式刷新图层的选择集
//         /// </summary>
//         /// <param name="pFeaLay"></param>
//         /// <param name="pFilter"></param>
//         /// <param name="selOption"></param>
//         public static void FeatureLayerSelect(IFeatureLayer pFeaLay, IQueryFilter pFilter, SelectOption selOption)
//         {
//             try
//             {
//                 if (pFeaLay == null) return;
//                 IFeatureSelection pFeaSel = null;
//                 ISelectionSet pSelSet = null;
//                 IEnumIDs IDs;
//                 ArrayList oldSelection = new ArrayList();
//                 pFeaSel = pFeaLay as IFeatureSelection;
//                 pSelSet = pFeaSel.SelectionSet;
//                 IDs = pSelSet.IDs;
// 
//                 if (selOption.ClearInVisible && (!pFeaLay.Visible))
//                 {
//                     IDs.Reset();
//                     int tmpFeaOID = IDs.Next();
//                     while (tmpFeaOID != -1)
//                     {
//                         oldSelection.Add(tmpFeaOID);
//                         tmpFeaOID = IDs.Next();
//                     }
//                     for (int i = 0; i < oldSelection.Count; i++)
//                     {
//                         tmpFeaOID = (int)oldSelection[i];
//                         pSelSet.RemoveList(1, ref tmpFeaOID);
//                         tmpFeaOID = IDs.Next();
//                     }
// 
//                 }
//                 if (pFeaLay.Visible && pFeaLay.Selectable)
//                 {
//                     IDs.Reset();
//                     int tmpFeaOID = IDs.Next();
//                     while (tmpFeaOID != -1)
//                     {
//                         oldSelection.Add(tmpFeaOID);
//                         tmpFeaOID = IDs.Next();
//                     }
// 
//                     IFeatureCursor pFeaCur = pFeaLay.Search(pFilter, false);
//                     IFeature pFeature;
//                     pFeature = pFeaCur.NextFeature();
//                     ArrayList result = new ArrayList();
//                     while (pFeature != null)
//                     {
//                         result.Add(pFeature.OID);
//                         pFeature = pFeaCur.NextFeature();
//                     }
//                     System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCur);
//                     switch ((esriSelectionResultEnum)selOption.ResultMethod)
//                     {
//                         case esriSelectionResultEnum.esriSelectionResultNew://新选择集            
//                             for (int i = 0; i < oldSelection.Count; i++)
//                             {
//                                 tmpFeaOID = (int)oldSelection[i];
//                                 pSelSet.RemoveList(1, ref tmpFeaOID);
//                                 tmpFeaOID = IDs.Next();
//                             }
//                             for (int i = 0; i < result.Count; i++)
//                             {
//                                 pSelSet.Add((int)result[i]);
//                             }
//                             break;
//                         case esriSelectionResultEnum.esriSelectionResultAdd://加入选择集
//                             for (int i = 0; i < result.Count; i++)
//                             {
//                                 pSelSet.Add((int)result[i]);
//                             }
//                             break;
//                         case esriSelectionResultEnum.esriSelectionResultSubtract://从选择集中删除
//                             for (int i = 0; i < result.Count; i++)
//                             {
//                                 tmpFeaOID = (int)result[i];
//                                 pSelSet.RemoveList(1, ref tmpFeaOID);
//                             }
//                             break;
//                         case esriSelectionResultEnum.esriSelectionResultAnd://从选择集中查询
//                             ICursor pCur;
//                             pSelSet.Search(pFilter, false, out pCur);
//                             for (int i = 0; i < oldSelection.Count; i++)
//                             {
//                                 tmpFeaOID = (int)oldSelection[i];
//                                 pSelSet.RemoveList(1, ref tmpFeaOID);
//                                 tmpFeaOID = IDs.Next();
//                             }
//                             IRow pRow = pCur.NextRow();
//                             while (pRow != null)
//                             {
//                                 pSelSet.Add(pRow.OID);
//                                 pRow = pCur.NextRow();
//                             }
//                             System.Runtime.InteropServices.Marshal.ReleaseComObject(pCur);
// 
//                             break;
//                     }
//                 }
//                 pFeaSel.SelectionColor = PublicFunction.GetSelectionColor(selOption.DefaultColorRGB);
//                 pFeaSel.SelectionSet = pSelSet;
//             }
//             catch (Exception ex)
//             {
//                 MessageBox.Show("FeatureLayerSelect出错！" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
//             }
// 
//         }
//         #endregion

        #region 根据查询条件和结果保存方式刷新图层的选择集
        /// <summary>
        /// 根据查询条件和结果保存方式刷新图层的选择集
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pFilter"></param>
        /// <param name="selOption"></param>
        public static void FeatureLayerSelect(IFeatureLayer pFeaLay, IQueryFilter pFilter, esriSelectionResultEnum pResultMethod)
        {
            try
            {
                if (pFeaLay == null) return;


                IFeatureSelection pFeaSel = null;
                ISelectionSet pSelSet = null;
                IEnumIDs IDs;
                ArrayList oldSelection = new ArrayList();
                pFeaSel = pFeaLay as IFeatureSelection;
                pSelSet = pFeaSel.SelectionSet;
                IDs = pSelSet.IDs;

                if (pFeaLay.Visible && pFeaLay.Selectable)
                {
                    IDs.Reset();
                    int tmpFeaOID = IDs.Next();
                    while (tmpFeaOID != -1)
                    {
                        oldSelection.Add(tmpFeaOID);
                        tmpFeaOID = IDs.Next();
                    }

                    IFeatureCursor pFeaCur = pFeaLay.Search(pFilter, false);
                    IFeature pFeature;
                    pFeature = pFeaCur.NextFeature();
                    ArrayList result = new ArrayList();
                    while (pFeature != null)
                    {
                        result.Add(pFeature.OID);
                        pFeature = pFeaCur.NextFeature();
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCur);
                    switch (pResultMethod)
                    {
                        case esriSelectionResultEnum.esriSelectionResultNew://新选择集            
                            for (int i = 0; i < oldSelection.Count; i++)
                            {
                                tmpFeaOID = (int)oldSelection[i];
                                pSelSet.RemoveList(1, ref tmpFeaOID);
                                tmpFeaOID = IDs.Next();
                            }
                            for (int i = 0; i < result.Count; i++)
                            {
                                pSelSet.Add((int)result[i]);
                            }
                            break;
                        case esriSelectionResultEnum.esriSelectionResultAdd://加入选择集
                            for (int i = 0; i < result.Count; i++)
                            {
                                pSelSet.Add((int)result[i]);
                            }
                            break;
                        case esriSelectionResultEnum.esriSelectionResultSubtract://从选择集中删除
                            for (int i = 0; i < result.Count; i++)
                            {
                                tmpFeaOID = (int)result[i];
                                pSelSet.RemoveList(1, ref tmpFeaOID);
                            }
                            break;
                        case esriSelectionResultEnum.esriSelectionResultAnd://从选择集中查询
                            ICursor pCur;
                            pSelSet.Search(pFilter, false, out pCur);
                            for (int i = 0; i < oldSelection.Count; i++)
                            {
                                tmpFeaOID = (int)oldSelection[i];
                                pSelSet.RemoveList(1, ref tmpFeaOID);
                                tmpFeaOID = IDs.Next();
                            }
                            IRow pRow = pCur.NextRow();
                            while (pRow != null)
                            {
                                pSelSet.Add(pRow.OID);
                                pRow = pCur.NextRow();
                            }
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCur);

                            break;
                    }
                }
                //pFeaSel.SelectionColor =  PublicFunction.GetSelectionColor(selOption.DefaultColorRGB);
                pFeaSel.SelectionSet = pSelSet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FeatureLayerSelect出错！" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        #endregion

        #region 得到指定图层上选中要素的个数
        /// <summary>
        /// 得到指定图层上选中要素的个数
        /// </summary>
        /// <param name="pSelection"></param>
        /// <param name="LayerName"></param>
        /// <returns></returns>
        public static int GetLayerSelectionInfo(IFeatureSelection pFeaSeltion)
        {
            return pFeaSeltion.SelectionSet.Count;
        }
        #endregion

        #region 根据Geometry类型生成一个默认符号
        /// <summary>
        /// 根据Geometry类型生成一个默认符号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ISymbol GetDefaultSymbol(esriGeometryType type)
        {
            ISymbol sym = null;

            IRgbColor mCor = new RgbColorClass();
            mCor.Green = 255;
            mCor.Blue = 255;
            IRgbColor lCor = new RgbColorClass();
            lCor.Red = 0;
            lCor.Green = 0;
            lCor.Blue = 255;
            IRgbColor fCor = new RgbColorClass();
            fCor.Green = 255;
            fCor.Blue = 255;

            IMarkerSymbol mark = new SimpleMarkerSymbolClass();
            mark.Color = mCor;
            mark.Size = 8;

            ILineSymbol line = new SimpleLineSymbolClass();
            line.Width = 2;
            line.Color = lCor;

            IFillSymbol fill = new SimpleFillSymbolClass();
            fill.Color = fCor;
            fill.Outline = line;

            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    sym = (ISymbol)mark;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    sym = (ISymbol)line;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    sym = (ISymbol)fill;
                    break;
            }

            return sym;
        }
        #endregion

        #region//检查撤销、重做情况
        /// <summary>
        /// 检查撤销、重做情况
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="undo"></param>
        /// <returns></returns>
        private static bool CheckUndoReDo(ILayer pLayer, bool undo)
        {

            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            IWorkspaceEdit pWorkspaceEdit;
            bool hasRedos = false;
            bool hasUndos = false;

            ICompositeLayer pComp;


            if (pLayer is IGroupLayer)
            {

                pComp = (ICompositeLayer)pLayer;
                for (int i = 0; i < pComp.Count; i++)
                {
                    if (CheckUndoReDo(pComp.get_Layer(i), undo))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass == null) return false;
                    pDataset = (IDataset)pFeatureLayer.FeatureClass;
                    if (pDataset != null)
                    {
                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
                            if (pWorkspaceEdit.IsBeingEdited())
                            {
                                if (undo)
                                {
                                    pWorkspaceEdit.HasUndos(ref hasUndos);
                                    if (hasUndos) pWorkspaceEdit.UndoEditOperation();
                                }
                                else
                                {
                                    pWorkspaceEdit.HasRedos(ref  hasRedos);
                                    if (hasRedos) pWorkspaceEdit.RedoEditOperation();
                                }
                                return true;

                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region//编辑的撤销/重做
        /// <summary>
        /// 编辑的撤销/重做;操作的地图对象 pMap;undo = true则做撤销 ，undo = false则做重做
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="undo"></param>
        public static void UndoRedoEdit(IMap pMap, bool undo)
        {
            if (pMap.LayerCount < 1) return;

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (CheckUndoReDo(pMap.get_Layer(i), undo)) break;
            }

            IActiveView pActiveView = (IActiveView)pMap;

            pActiveView.Refresh();

        }
        #endregion

        #region //刷新地图
        /// <summary>
        /// 刷新地图
        /// </summary>
        /// <param name="pActiveView"></param>
        /// <returns></returns>
        public static void MapRefresh(IActiveView pActiveView)
        {
            pActiveView.FocusMap.ClearSelection();
            pActiveView.GraphicsContainer.DeleteAllElements();//删除创建的地图元素
            pActiveView.Refresh();//PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pActiveView.Extent);//视图刷新
        }
        #endregion

        #region //开始编辑
        /// <summary>
        /// 开始编辑
        /// </summary>
        /// <param name="pMap"></param>
        public static void StartEditing(IMap pMap)
        {
            if (pMap == null) return;

            if (pMap.LayerCount < 1) return;

            //有多个Workspace的处理
            //IWorkspace pWorkspace 
            int count = 0;
            string compString = "";

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                count += PublicFunction.CheckStartEditing(pMap.get_Layer(i), ref compString);
            }
            if (count == 0)
            {
                System.Windows.Forms.MessageBox.Show("不能编辑任何图层，请检查数据是否已经进行了版本注册或是否有更新权限！",
                    "开始编辑", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
            }

        }
        #endregion

        #region//检查图层开始编辑状态.
        /// <summary>
        /// 检查图层开始编辑状态
        /// </summary>
        /// <param name="pLayer"></param>待检查的图层
        /// <param name="compString"></param>返回不能开始编辑的图层
        /// <returns></returns>	
        public static int CheckStartEditing(ILayer pLayer, ref string compString)
        {
            int count = 0;
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            IWorkspaceEdit pWorkspaceEdit;
            ICompositeLayer pComp;
            int i;
            if (pLayer is IGroupLayer)//如果是图层组
            {
                pComp = (ICompositeLayer)pLayer;
                for (i = 0; i < pComp.Count; i++)
                {
                    count += CheckStartEditing(pComp.get_Layer(i), ref compString);
                    //if(count)
                }
            }
            else
            {
                if (pLayer is IGeoFeatureLayer)//如果是地理要素图层
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;//跳转到IFeatureLayer接口
                    if (pFeatureLayer.FeatureClass == null) return count;
                    pDataset = (IDataset)pFeatureLayer.FeatureClass;//跳转到IDataset接口
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass ||
                        pDataset.Type == esriDatasetType.esriDTFeatureDataset)//如果数据集是要素类或要素数据集
                    {
                        pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;//跳转到IWorkspaceEdit接口
                        if (pDataset.Workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)//如果是远程数据库工作空间
                        {
                            if (pDataset.Workspace is IVersionedWorkspace)//如果是版本工作空间
                            {
                                IVersionedObject pVersionObject = pDataset as IVersionedObject;//跳转到IVersionedObject接口
                                if (pVersionObject.IsRegisteredAsVersioned)//若版本对象注册
                                {
                                    if (!pWorkspaceEdit.IsBeingEdited())//若没有开始 编辑
                                    {
                                        try
                                        {
                                            pWorkspaceEdit.StartEditing(true);//试图开始编辑
                                            pWorkspaceEdit.EnableUndoRedo();//开始重作社生效
                                            count++;//计数器累加
                                        }
                                        catch (Exception ex)
                                        {

                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                            }
                            else//如果是非版本工作空间
                            {
                                compString = compString + @"\r\n" + pFeatureLayer.Name;
                            }
                        }
                        else//如果不是远程数据库工作空间
                        {
                            if (!pWorkspaceEdit.IsBeingEdited())//若没有开始编辑
                            {
                                try
                                {
                                    pWorkspaceEdit.StartEditing(true);//试图开始编辑
                                    pWorkspaceEdit.EnableUndoRedo();//开始重作生效
                                    count++;//计数器累加
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }

                    }
                    else//如果数据集不是是要素类或要素数据集
                    {
                        compString = compString + @"\r\n" + pFeatureLayer.Name;
                    }
                }
            }

            return count;
        }

        #endregion

        #region//检查停止编辑状态
        /// <summary>
        /// 检查停止编辑状态
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="saveEdits"></param>
        /// <returns></returns>
        private static bool CheckStopEdits(ILayer pLayer, bool saveEdits)
        {
            bool result = false;
            try
            {
                IFeatureLayer pFeatureLayer;
                IDataset pDataset;
                IWorkspaceEdit pWorkspaceEdit;
                ICompositeLayer pComp;

                if (pLayer is IGroupLayer)
                {
                    pComp = (ICompositeLayer)pLayer;
                    for (int i = 0; i < pComp.Count; i++)
                    {
                        if (CheckStopEdits(pComp.get_Layer(i), saveEdits))
                            result = true;
                    }
                }
                else
                {
                    if (pLayer is IGeoFeatureLayer)
                    {
                        pFeatureLayer = (IFeatureLayer)pLayer;
                        if (pFeatureLayer.FeatureClass == null) return false;
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
                            if (pWorkspaceEdit.IsBeingEdited())
                            {
                                if (saveEdits)
                                {
                                    pWorkspaceEdit.HasEdits(ref saveEdits);
                                }
                                pWorkspaceEdit.StopEditing(saveEdits);

                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        #endregion

        #region//停止或保存编辑，传入的pMap不能为空
        /// <summary>
        /// 停止或保存编辑，传入的pMap不能为空
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="saveEdits"></param>
        /// <param name="warnUser"></param>
        /// <returns></returns>
        public static bool StopEditing(IMap pMap, bool saveEdits, bool warnUser)
        {
            if (pMap.LayerCount < 1) return false;
            System.Windows.Forms.DialogResult result;

            bool haveEdits = false;
            if (warnUser)
            {
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    if (CheckWorkspaceEdit(pMap.get_Layer(i), "hasEdits"))
                    {
                        haveEdits = true;
                        break;
                    }
                }
                if (!haveEdits)
                {
                    result = DialogResult.No;
                }
                else
                {
                    result = MessageBox.Show("数据已经被修改过，保存修改吗?", "更改提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                if (result == DialogResult.Cancel)
                    return false;
            }
            else
            {
                if (saveEdits)
                {
                    result = DialogResult.Yes;
                }
                else
                {
                    result = DialogResult.No;

                }
            }
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                CheckStopEdits(pMap.get_Layer(i), (result == DialogResult.Yes));
            }

            pMap.ClearSelection();

            ((IActiveView)pMap).Refresh();
            return true;
        }
        #endregion

        #region//检查图层编辑情况
        /// <summary>
        /// 检查图层编辑情况
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static bool CheckWorkspaceEdit(ILayer pLayer, string check)
        {
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            IWorkspaceEdit pWorkspaceEdit;
            bool boolCheck = false;
            ICompositeLayer pComp;
            if (pLayer is IGroupLayer)
            {
                pComp = (ICompositeLayer)pLayer;
                for (int i = 0; i < pComp.Count; i++)
                {
                    if (CheckWorkspaceEdit(pComp.get_Layer(i), check))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = pDataset.Workspace as IWorkspaceEdit;
                            if (pWorkspaceEdit == null) return false;

                            switch (check)
                            {
                                case "IsBeingEdited":
                                    if (pWorkspaceEdit.IsBeingEdited())
                                        return true;
                                    break;
                                case "hasEdits":
                                    pWorkspaceEdit.HasEdits(ref boolCheck);
                                    return boolCheck;
                                case "hasUndos":
                                    pWorkspaceEdit.HasUndos(ref boolCheck);
                                    return boolCheck;
                                case "hasRedos":
                                    pWorkspaceEdit.HasRedos(ref boolCheck);
                                    return boolCheck;
                            }

                        }
                    }
                }

            }
            return false;
        }
        #endregion

        #region 将本地文件转换成Byte数组
        /// <summary>
        /// 将本地文件转换成Byte数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] GetByteFromFileName(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    System.IO.FileStream stream = new System.IO.FileStream(fileName, FileMode.Open);
                    byte[] b = new byte[stream.Length];
                    stream.Read(b, 0, b.Length);
                    stream.Close();
                    return b;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 二进制流转换到IMemoryBlobStream对象(存入engine建的库blob类型字段中)
        /// <summary>
        /// 二进制流转换到IMemoryBlobStream对象
        /// </summary>
        /// <param name="bt">二进制流</param>
        /// <returns>如果成功，返回IMemoryBlobStream对象；否则返回null。</returns>
        public static IMemoryBlobStream Byte2Blob(Byte[] bt)
        {
            try
            {
                if (bt == null)
                    throw new Exception("二进制数组没有初始化！");

                IMemoryBlobStreamVariant blobStream;
                blobStream = new MemoryBlobStreamClass();
                blobStream.ImportFromVariant(bt);

                return (IMemoryBlobStream)blobStream;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Byte2Blob 函数错误！");
                return null;
            }
        }
        #endregion

        #region 将文件读入esri的FileStream
        /// <summary>
        /// 将文件读入esri的FileStream
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static IMemoryBlobStream GetStreamFromFile(string filename)
        {
            //            IMemoryBlobStreamVariant blobStream;
            //            blobStream = new MemoryBlobStreamClass();
            //            FileStreamClass fs = new FileStreamClass();
            //            fs.LoadFromFile(filename);
            ////            IStream ppstm;
            ////            fs.Clone(out ppstm);
            ////            fs.Close();//没有close。
            //////            Byte[] bt = new byte[fs.Size];
            //////            uint cb,pcbRead;
            //////            for(int i=0;i<fs.Size;i++)
            //////            {
            //////                (fs as IStream).RemoteRead(out bt[i],cb,out pcbRead);
            //////            }
            //            blobStream.ImportFromVariant(fs);
            //            return blobStream as IMemoryBlobStream;
            MemoryBlobStreamClass fileBlob = new MemoryBlobStreamClass();
            fileBlob.LoadFromFile(filename);
            return fileBlob;
        }
        #endregion

        #region 对整个地图，查询与 pGeometry 交叉的要素
        /// <summary>
        /// 对整个地图，查询与 pGeometry 交叉的要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pGeometry"></param>
        /// <param name="justOne"></param>
        /// <param name="allLayers"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IMap pMap, IGeometry pGeometry, esriSpatialRelEnum queryMethod, bool justOne, out ArrayList layers)
        {
            layers = (ArrayList)PublicFunction.GetAllFeatureLayer(pMap, typeof(ArrayList));
            ArrayList result = new ArrayList();
            if (pGeometry == null) return null;
            SpatialFilterClass spaFilter = new SpatialFilterClass();
            spaFilter.Geometry = pGeometry;
            spaFilter.SpatialRel = queryMethod;//esriSpatialRelEnum.esriSpatialRelIntersects;//交叉
            foreach (IFeatureLayer pFeaLay in layers)
            {
                spaFilter.GeometryField = pFeaLay.FeatureClass.ShapeFieldName;
                if (justOne)
                {
                    ArrayList layerFeatures = PublicFunction.SearchFeature(pFeaLay, spaFilter, true);
                    if (layerFeatures.Count > 0)
                    {
                        layers.Clear();
                        layers.Add(pFeaLay);
                        return layerFeatures;
                    }
                }
                else
                {
                    result.Add(PublicFunction.SearchFeature(pFeaLay, spaFilter, false));
                }
            }
            return result;
        }
        #endregion

        #region 查询一个图层中，与 pGeometry 交叉的要素
        /// <summary>
        /// 查询一个图层中，与 pGeometry 交叉的要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pGeometry"></param>
        /// <param name="justOne"></param>
        /// <param name="allLayers"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureLayer pFeaLay, IGeometry pGeometry, esriSpatialRelEnum queryMethod, bool justOne)
        {
            SpatialFilterClass spaFilter = new SpatialFilterClass();
            spaFilter.Geometry = pGeometry;
            spaFilter.SpatialRel = queryMethod;//esriSpatialRelEnum.esriSpatialRelIntersects;//交叉
            spaFilter.GeometryField = pFeaLay.FeatureClass.ShapeFieldName;
            return PublicFunction.SearchFeature(pFeaLay, spaFilter, justOne);
        }

        /// <summary>
        /// 查询一个图层中，与 pGeometry 交叉的要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pGeometry"></param>
        /// <param name="justOne"></param>
        /// <param name="allLayers"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureClass pFeatureClass, IGeometry pGeometry, esriSpatialRelEnum queryMethod, bool justOne)
        {
            SpatialFilterClass spaFilter = new SpatialFilterClass();
            spaFilter.Geometry = pGeometry;
            spaFilter.SpatialRel = queryMethod;//esriSpatialRelEnum.esriSpatialRelIntersects;//交叉
            spaFilter.GeometryField = pFeatureClass.ShapeFieldName;
            return PublicFunction.SearchFeature(pFeatureClass, spaFilter, justOne);
        }

        #endregion

        #region 查询一个图层中，满足条件 SQL 的要素
        /// <summary>
        /// 查询一个图层中，满足条件 SQL 的要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pGeometry"></param>
        /// <param name="justOne"></param>
        /// <param name="allLayers"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureLayer pFeaLay, string strSQL, bool justOne)
        {
            QueryFilterClass pFilter = new QueryFilterClass();
            pFilter.WhereClause = strSQL;
            return PublicFunction.SearchFeature(pFeaLay, pFilter, justOne);
        }
        #endregion

        #region 根据表，OIDs构造查询条件
        /// <summary>
        /// 根据表，OIDs构造查询条件
        /// </summary>
        /// <param name="pTable"></param>
        /// <param name="OIDs"></param>
        /// <param name="isSwitch"></param>
        /// <returns></returns>
        public static string GetQuerySQL(ITable pTable, ArrayList OIDs, bool isSwitch)
        {
            string tmpStr = pTable.OIDFieldName;
            if (isSwitch)
            {
                tmpStr += " not in (";
            }
            else
            {
                tmpStr += " in (";
            }

            if (OIDs != null && OIDs.Count > 0)
            {
                foreach (int oid in OIDs)
                {
                    tmpStr += oid.ToString() + " , ";
                }
                tmpStr = tmpStr.Remove(tmpStr.Length - 3, 3);
            }
            tmpStr += ")";
            return tmpStr;
        }
        #endregion

        #region 根据表，IEnumIDs构造查询条件
        /// <summary>
        /// 根据表，OIDs构造查询条件
        /// </summary>
        /// <param name="pTable"></param>
        /// <param name="OIDs"></param>
        /// <param name="isSwitch"></param>
        /// <returns></returns>
        public static string GetQuerySQL(ITable pTable, IEnumIDs OIDs, bool isSwitch)
        {
            string tmpStr = pTable.OIDFieldName;
            if (OIDs == null)
            {
                return tmpStr + " in ()";
            }

            OIDs.Reset();
            if (isSwitch)
            {
                tmpStr += " not in (";
            }
            else
            {
                tmpStr += " in (";
            }

            int oid = -1;
            oid = OIDs.Next();
            if (oid != -1)
            {
                while (oid != -1)
                {
                    tmpStr += oid.ToString() + " , ";
                    oid = OIDs.Next();
                }
                tmpStr = tmpStr.Remove(tmpStr.Length - 3, 3);
            }
            tmpStr += ")";
            return tmpStr;
        }
        #endregion

        #region 根据subFields和查询语句构造查询条件
        /// <summary>
        /// 根据subFields和查询语句构造查询条件
        /// </summary>
        /// <param name="subFields"></param>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static QueryFilterClass GetQueryFilter(string subFields, string strSQL)
        {
            QueryFilterClass pFilter = new QueryFilterClass();
            pFilter.WhereClause = strSQL;
            pFilter.SubFields = subFields != "" ? subFields : "*";
            return pFilter;
        }
        #endregion

        #region 找一个图层中符合条件的要素
        /// <summary>
        /// 找一个图层中符合条件的要素
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pFilter"></param>
        /// <param name="justOne"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureLayer pFeaLay, IQueryFilter pFilter, bool justOne)
        {
            ArrayList result = new ArrayList();
            IFeatureCursor pFeaCur = pFeaLay.Search(pFilter, false);
            IFeature pFeature = pFeaCur.NextFeature();
            if (pFeature != null)
            {
                result.Add(pFeature);
                if (!justOne)
                {
                    pFeature = pFeaCur.NextFeature();
                    while (pFeature != null)
                    {
                        result.Add(pFeature);
                        pFeature = pFeaCur.NextFeature();
                    }
                }
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCur);
            return result;
        }
        #endregion

        #region 找一个要素类中符合条件的要素
        /// <summary>
        /// 找一个要素类中符合条件的要素
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pFilter"></param>
        /// <param name="justOne"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureClass pFeatureClass, IQueryFilter pFilter, bool justOne)
        {
            ArrayList result = new ArrayList();
            IFeatureCursor pFeaCur = pFeatureClass.Search(pFilter, false);
            IFeature pFeature = pFeaCur.NextFeature();
            if (pFeature != null)
            {
                result.Add(pFeature);
                if (!justOne)
                {
                    pFeature = pFeaCur.NextFeature();
                    while (pFeature != null)
                    {
                        result.Add(pFeature);
                        pFeature = pFeaCur.NextFeature();
                    }
                }
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCur);
            return result;
        }
        #endregion

        #region 得到一个图层选择集OIDs构成的SQL语句
        /// <summary>
        /// 得到一个图层选择集OIDs构成的SQL语句
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="isSwitch">是否要反选</param>
        /// <returns></returns>
        public static QueryFilterClass GetFeatureLayerSelectionFilter(IFeatureLayer pFeaLay, bool isSwitch)
        {
            string tmpStr = pFeaLay.FeatureClass.OIDFieldName;
            if (isSwitch)
            {
                tmpStr += " not in (";
            }
            else
            {
                tmpStr += " in (";
            }
            IEnumIDs pIDs = (pFeaLay as IFeatureSelection).SelectionSet.IDs;
            pIDs.Reset();
            int id;
            id = pIDs.Next();
            bool haveIDs = id != -1 ? true : false;
            QueryFilterClass pQueryFilter = new QueryFilterClass();
            if (haveIDs)
            {
                while (id != -1)
                {
                    tmpStr += id.ToString() + " , ";
                    id = pIDs.Next();
                }
                tmpStr = tmpStr.Remove(tmpStr.Length - 3, 3);
                tmpStr += ")";
                pQueryFilter.WhereClause = tmpStr;
            }
            else
            {
                pQueryFilter.WhereClause = "";
            }
            return pQueryFilter;
        }
        #endregion

        #region 获得一个ICodeValueDomain所有可选的项目(值(显示)和代码(存储))
        /// <summary>
        /// 获得一个ICodeValueDomain所有可选的项目(值(显示)和代码(存储))
        /// </summary>
        /// <param name="pCD"></param>
        /// <returns></returns>
        public static Item[] GetCodedValueDomainItems(ICodedValueDomain pCD)
        {
            Item[] items = new Item[pCD.CodeCount];
            for (int i = 0; i < pCD.CodeCount; i++)
            {
                items[i] = new Item(pCD.get_Name(i).ToString(), pCD.get_Value(i));
                //ESRI，存在字段中的值是Name(编码名称)，编辑时显示给用户看的是Value(objcet类型)，没脑子？
            }
            return items;
        }
        #endregion

        #region 获得Domain的描述
        /// <summary>
        /// 获得Domain的描述
        /// </summary>
        /// <param name="pDomain"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public static string GetCodedDescriptionDomainValue(ICodedValueDomain pDomain, string domainValue)
        {
            string result = "<Null>";
            for (int i = 0; i < pDomain.CodeCount; i++)
            {
                if (pDomain.get_Value(i).ToString() == domainValue)
                {
                    result = pDomain.get_Name(i).ToString();
                    break;
                }
            }
            return result;

        }
        #endregion

        #region 获得Domain的值
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

        #region 从一个MapControl中得到所有的工作空间

        /// <summary>
        /// 从一个MapControl中得到所有的工作空间
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <returns></returns>
        public static ArrayList GetAllWorkspace(IMapControl2 pMapControl, bool onlySDEWorkspace)
        {
            ArrayList result = new ArrayList();
            if (pMapControl != null)
            {
                GetAllWorkspace(pMapControl.Map, ref result, onlySDEWorkspace);
            }
            return result;
        }

        /// <summary>
        /// 找所有的工作空间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        public static void GetAllWorkspace(object obj, ref ArrayList list, bool onlySDEWorkspace)
        {
            IWorkspace pWorkspace = null;
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    GetAllWorkspace(pMap.get_Layer(i), ref list, onlySDEWorkspace);
                }
            }
            else if (obj is IGroupLayer)
            {
                ICompositeLayer comLayer = obj as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    GetAllWorkspace(comLayer.get_Layer(i), ref list, onlySDEWorkspace);
                }
            }
            else if (obj is IFeatureLayer)
            {
                pWorkspace = ((obj as IFeatureLayer).FeatureClass as IDataset).Workspace;
                if (!list.Contains(pWorkspace))
                {
                    if (onlySDEWorkspace)
                    {
                        if (pWorkspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            list.Add(pWorkspace);
                        }
                    }
                    else
                    {
                        list.Add(pWorkspace);
                    }
                }
            }
        }
        #endregion

        #region 构造存放Geometry信息的Datatable
        /// <summary>
        /// 存放一个要素几何信息的DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateNewGeoTable()
        {
            DataTable dt = new DataTable();
            DataColumn column;

            DataColumn col;
            col = new DataColumn();
            col.ColumnName = "部分";
            col.DataType = typeof(System.Int32);
            dt.Columns.Add(col);

            column = new DataColumn();
            column.ColumnName = "序号";
            column.DataType = typeof(System.Int32);
            dt.Columns.Add(column);
            dt.PrimaryKey = new DataColumn[] { col, column };

            column = new DataColumn();
            column.ColumnName = "X坐标";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Y坐标";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Z高程";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "M埋深";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);
            return dt;
        }
        #endregion

        #region 将一个字段值转换为可以显示的字符串
        /// <summary>
        /// 将一个字段值转换为可以显示的字符串
        /// </summary>
        /// <param name="pField"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetFieldValue(IField pField, object val)
        {
            string result = "";
            try
            {
                if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    result = "<几何数据>";
                }
                else if (pField.Type == esriFieldType.esriFieldTypeRaster)
                {
                    result = "<栅格数据>";
                }
                else if (pField.Type == esriFieldType.esriFieldTypeBlob)
                {
                    result = "<二进制数据>";
                }
                else if (pField.Type == esriFieldType.esriFieldTypeSingle)
                {
                    if (val != null && val != System.DBNull.Value)
                    {
                        Single s = (Single)val;
                        result = s.ToString("0.###");
                    }
                    else
                    {
                        result = PublicFunction.ConvertNull(val);
                    }
                }
                else if (pField.Type == esriFieldType.esriFieldTypeDouble)
                {
                    if (val != null && val != System.DBNull.Value)
                    {
                        Double d = (Double)val;
                        result = d.ToString("0.###");

                    }
                    else
                    {
                        result = PublicFunction.ConvertNull(val);
                    }
                }
                else if (pField.Type == esriFieldType.esriFieldTypeDate)
                {
                    if (val is DateTime)
                    {
                        result = ((DateTime)val).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        result = "<Null>";
                    }
                }
                else
                {
                    result = PublicFunction.ConvertNull(val);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            return result;
        }
        #endregion

        #region 根据红、绿、蓝三种颜色的值生成一个IRgbColor对象
        /// <summary>
        /// 根据红、绿、蓝三种颜色的值生成一个IRgbColor对象
        /// </summary>
        /// <param name="m_Red">红色取值范围：0～255</param>
        /// <param name="m_Green">绿色取值范围：0～255</param>
        /// <param name="m_Blue">蓝色取值范围：0～255</param>
        /// <returns>一个IRgbColor对象</returns>
        public static RgbColorClass GetRGBColor(int m_Red, int m_Green, int m_Blue)
        {
            //生成一个RGBColor
            RgbColorClass pRGB = new RgbColorClass();

            pRGB.Red = m_Red;
            pRGB.Green = m_Green;
            pRGB.Blue = m_Blue;
            pRGB.UseWindowsDithering = false;
            return pRGB;
        }
        #endregion

        #region 选择一层上的所有要素
        /// <summary>
        /// 选择一层上的所有要素
        /// </summary>
        /// <param name="pFeaLay">图层</param>
        /// <returns>选择一层上的所有要素</returns>
        public static void SelectALayer(IFeatureLayer pFeaLay, bool warning)
        {
            if (pFeaLay == null) return;
            if (warning)
            {
                if (pFeaLay.FeatureClass.FeatureCount(null) > 2000)
                {
                    if (MessageBox.Show("图层上的要素数量大于2000，选择操作需要较长时间。继续该操作吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            IFeatureSelection pFeaSel = (IFeatureSelection)pFeaLay;
            pFeaSel.SelectFeatures(null, esriSelectionResultEnum.esriSelectionResultAdd, false);
            pFeaSel.SelectionSet.Refresh();
        }
        #endregion

        #region 对一个图层，反选要素
        /// <summary>
        /// 对一个图层，反选要素
        /// </summary>
        /// <param name="pFeaLay">图层</param>
        /// <returns>反选</returns>
        public static void SwitchSelection(IFeatureLayer pFeaLay)
        {
            if (pFeaLay == null) return;
            IFeatureClass pF;
            pF = pFeaLay.FeatureClass;

            string tmpStr = pF.OIDFieldName;
            tmpStr += " not in (";

            IEnumIDs pIDs = (pFeaLay as IFeatureSelection).SelectionSet.IDs;
            pIDs.Reset();
            int id;
            id = pIDs.Next();
            bool haveIDs = id != -1 ? true : false;

            IQueryFilter pQueryFilter = new QueryFilterClass();
            if (haveIDs)
            {
                while (id != -1)
                {
                    tmpStr += id.ToString() + " , ";
                    id = pIDs.Next();
                }
                tmpStr = tmpStr.Remove(tmpStr.Length - 3, 3);
                tmpStr += ")";
                pQueryFilter.WhereClause = tmpStr;
            }
            else
            {
                pQueryFilter.WhereClause = "";
            }

            try
            {
                ESRI.ArcGIS.Geodatabase.IWorkspace pWorkspace;
                IDataset pFDataset = (IDataset)pF;
                pWorkspace = pFDataset.Workspace;
                ISelectionSet pSelectionSet;
                pSelectionSet = pF.Select(pQueryFilter, esriSelectionType.esriSelectionTypeIDSet, esriSelectionOption.esriSelectionOptionNormal, pWorkspace);
                IFeatureSelection pFS = (IFeatureSelection)pFeaLay;
                pFS.SelectionSet = pSelectionSet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 清除一个图层上选择的要素
        /// <summary>
        /// 清除一个图层上选择的要素
        /// </summary>
        /// <param name="pFeaLay"></param>
        public static void ClearSelection(IFeatureLayer pFeaLay)
        {
            if (pFeaLay != null)
            {
                (pFeaLay as IFeatureSelection).Clear();
            }
        }
        #endregion

        #region 将一个图层上的选择集OIDs加入ArrayList
        /// <summary>
        /// 将一个图层上的选择集OIDs加入ArrayList
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <returns></returns>
        public static ArrayList GetFeatureLayerSelectionOIDs(IFeatureLayer pFeaLay)
        {
            ArrayList result = new ArrayList();
            IFeatureSelection pFeaSel = pFeaLay as IFeatureSelection;
            IEnumIDs pEnumIDs = pFeaSel.SelectionSet.IDs;
            pEnumIDs.Reset();
            int tmpID = pEnumIDs.Next();
            while (tmpID != -1)
            {
                result.Add(tmpID);
                tmpID = pEnumIDs.Next();
            }
            return result;
        }
        #endregion

//         #region 缓冲区分析 : 新建一个存放一个图层分析结果的DataTable
//         /// <summary>
//         /// 新建一个存放一个图层分析结果的DataTable
//         /// </summary>
//         /// <param name="type"></param>
//         /// <returns></returns>
//         public static DataTable CreateAnalysisResultTable(IFeatureLayer pFeaLay)
//         {
//             esriGeometryType type = pFeaLay.FeatureClass.ShapeType;
//             DataTable dt = new DataTable(pFeaLay.Name);
//             DataColumn column;
//             //分类字段名
//             column = new DataColumn();
//             column.ColumnName = "序号";
//             column.DataType = typeof(System.Int32);
//             column.AutoIncrement = true;
//             column.AutoIncrementSeed = 1;
//             column.AutoIncrementStep = 1;
//             dt.Columns.Add(column);
// 
//             //图层按照 GeoObjNum(代码)字段分类
//             column = new DataColumn();
//             column.ColumnName = GlobalValue.GEO_OBJ_NUM_ALIAS_NAME;   // "地物编码";
//             column.DataType = typeof(System.String);//typeof(System.Int32);
//             column.MaxLength = 20;
//             dt.Columns.Add(column);
// 
//             //地物名称通过地物编码在DFGIDCode树中查找
//             column = new DataColumn();
//             column.ColumnName = "地物名称";
//             column.DataType = typeof(System.String);
//             column.MaxLength = 50;
//             dt.Columns.Add(column);
// 
//             //要素数量
//             column = new DataColumn();
//             column.ColumnName = "要素数量";
//             column.DataType = typeof(System.Int32);
//             dt.Columns.Add(column);
//             //线有总长度；面有总长度，总面积
//             switch (type)
//             {
//                 case esriGeometryType.esriGeometryPolyline: //fType = "线";
//                     column = new DataColumn();
//                     column.ColumnName = "总长度";
//                     column.DataType = typeof(System.Double);
//                     dt.Columns.Add(column);
//                     break;
//                 case esriGeometryType.esriGeometryPolygon://fType = "多边形";
//                     if (pFeaLay.Name == GlobalValue.NAME_OF_VIR_PLYGON || pFeaLay.Name == GlobalValue.NAME_OF_VIR_PLYGON + "_2D")//若为植被
//                     {
//                         column = new DataColumn();
//                         column.ColumnName = "绿化面积";
//                         column.DataType = typeof(System.Double);
//                         dt.Columns.Add(column);
// 
//                         column = new DataColumn();
//                         column.ColumnName = "受影响的绿化地块总面积";
//                         column.DataType = typeof(System.Double);
//                         dt.Columns.Add(column);
// 
//                         column = new DataColumn();
//                         column.ColumnName = "绿化率(%)";
//                         column.DataType = typeof(System.Double);
//                         dt.Columns.Add(column);
//                     }
//                     else if (pFeaLay.Name == GlobalValue.NAME_OF_BUD_PLYGON || pFeaLay.Name == GlobalValue.NAME_OF_CON_PLYGON || pFeaLay.Name == GlobalValue.NAME_OF_IND_PLYGON || pFeaLay.Name == GlobalValue.NAME_OF_RES_PLYGON
//                         || pFeaLay.Name == GlobalValue.NAME_OF_BUD_PLYGON + "_2D" || pFeaLay.Name == GlobalValue.NAME_OF_CON_PLYGON + "_2D" || pFeaLay.Name == GlobalValue.NAME_OF_IND_PLYGON + "_2D" || pFeaLay.Name == GlobalValue.NAME_OF_RES_PLYGON + "_2D")//若为工业建设、或居民地
//                     {
//                         column = new DataColumn();
//                         column.ColumnName = "总占地面积";
//                         column.DataType = typeof(System.Double);
//                         dt.Columns.Add(column);
// 
//                         column = new DataColumn();
//                         column.ColumnName = "总建筑面积";
//                         column.DataType = typeof(System.Double);
//                         dt.Columns.Add(column);
// 
//                         if (pFeaLay.Name == GlobalValue.NAME_OF_BUD_PLYGON || pFeaLay.Name == GlobalValue.NAME_OF_BUD_PLYGON + "_2D")//若为建筑物
//                         {
//                             column = new DataColumn();
//                             column.ColumnName = "容积率(%)";
//                             column.DataType = typeof(System.Double);
//                             dt.Columns.Add(column);
//                         }
//                         else
//                         {
//                             column = new DataColumn();
//                             column.ColumnName = "占地面积/缓冲区面积(%)";
//                             column.DataType = typeof(System.Double);
//                             dt.Columns.Add(column);
//                         }
//                     }
//                     else
//                     {
//                         column = new DataColumn();
//                         column.ColumnName = "占地面积";
//                         column.DataType = typeof(System.Double);
//                         dt.Columns.Add(column);
// 
//                         column = new DataColumn();
//                         column.ColumnName = "占地面积/缓冲区面积(%)";
//                         column.DataType = typeof(System.Double);
//                         dt.Columns.Add(column);
//                     }
// 
//                     break;
//             }
//             return dt;
//         }
//         #endregion

//         #region 缓冲区分析 ： 新建一个存放一个图层分析结果的DataTable
//         /// 新建一个存放一个图层分析结果的DataTable
//         /// </summary>
//         /// <param name="type"></param>
//         /// <returns></returns>
//         public static DataTable CreateAnalysisInfoTable(IFeatureLayer pFeaLay)
//         {
//             esriGeometryType type = pFeaLay.FeatureClass.ShapeType;
//             DataTable dt = new DataTable(pFeaLay.Name);
//             DataColumn column = null; ;
// 
//             ESRI.ArcGIS.Geodatabase.ITable table = pFeaLay as ESRI.ArcGIS.Geodatabase.ITable;
//             ESRI.ArcGIS.Geodatabase.IField field = null;
//             for (int i = 0; i < table.Fields.FieldCount; i++)
//             {
//                 if ((pFeaLay as ITableFields).get_FieldInfo(i).Visible == true)//-----------------------By 袁怀月---------------
//                 {
//                     field = table.Fields.get_Field(i);
//                     column = new DataColumn();
// 
//                     column.Caption = field.AliasName;//-----------------------By 袁怀月---------------
//                     column.ColumnName = field.Name;
// 
//                     if (field.Domain is ICodedValueDomain)
//                     {
//                         column.DataType = typeof(System.String);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeInteger
//                         || field.Type == esriFieldType.esriFieldTypeSmallInteger
//                         || field.Type == esriFieldType.esriFieldTypeOID)
//                     {
//                         if (column.Caption == GlobalValue.GEO_OBJ_NUM_ALIAS_NAME)//"地物编码"
//                         {
//                             column.DataType = typeof(System.String);
//                         }
//                         else
//                         {
//                             column.DataType = typeof(System.Int32);
//                         }
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeSingle)
//                     {
//                         column.DataType = typeof(System.Single);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeDouble)
//                     {
//                         column.DataType = typeof(System.Double);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeDate)
//                     {
//                         column.DataType = typeof(System.DateTime);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeString)
//                     {
//                         column.DataType = typeof(System.String);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeGeometry)
//                     {
//                         column.DataType = typeof(Item);
//                     }
//                     else
//                     {
//                         //don't add to dt
//                         continue;
//                     }
// 
//                     dt.Columns.Add(column);
//                 }
//             }
// 
//             return dt;
//         }
// 
//         public static DataTable CreateAnalysisInfoTable(IFeatureClass pFeatureClass)
//         {
//             esriGeometryType type = pFeatureClass.ShapeType;
//             DataTable dt = new DataTable(pFeatureClass.AliasName);
//             DataColumn column = null; ;
// 
//             ESRI.ArcGIS.Geodatabase.ITable table = pFeatureClass as ESRI.ArcGIS.Geodatabase.ITable;
//             ESRI.ArcGIS.Geodatabase.IField field = null;
//             for (int i = 0; i < table.Fields.FieldCount; i++)
//             {
//                 if ((pFeatureClass as ITableFields).get_FieldInfo(i).Visible == true)//-----------------------By 袁怀月---------------
//                 {
//                     field = table.Fields.get_Field(i);
//                     column = new DataColumn();
// 
//                     column.Caption = field.AliasName;//-----------------------By 袁怀月---------------
//                     column.ColumnName = field.Name;
// 
//                     if (field.Domain is ICodedValueDomain)
//                     {
//                         column.DataType = typeof(System.String);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeInteger
//                         || field.Type == esriFieldType.esriFieldTypeSmallInteger
//                         || field.Type == esriFieldType.esriFieldTypeOID)
//                     {
//                         if (column.Caption == GlobalValue.GEO_OBJ_NUM_ALIAS_NAME)//"地物编码"
//                         {
//                             column.DataType = typeof(System.String);
//                         }
//                         else
//                         {
//                             column.DataType = typeof(System.Int32);
//                         }
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeSingle)
//                     {
//                         column.DataType = typeof(System.Single);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeDouble)
//                     {
//                         column.DataType = typeof(System.Double);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeDate)
//                     {
//                         column.DataType = typeof(System.DateTime);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeString)
//                     {
//                         column.DataType = typeof(System.String);
//                     }
//                     else if (field.Type == esriFieldType.esriFieldTypeGeometry)
//                     {
//                         column.DataType = typeof(Item);
//                     }
//                     else
//                     {
//                         //don't add to dt
//                         continue;
//                     }
// 
//                     dt.Columns.Add(column);
//                 }
//             }
// 
//             return dt;
//         }
// 
// 
//         #endregion

        #region 主要经济技术指标统计 : 新建一个存放要素详细信息的DataTable
        /// 新建一个存放要素类分析结果的DataTable
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable CreateStatInfoTable(IFeatureLayer pFeatureLayer, string DataTableName)
        {
            DataTable dt = new DataTable(DataTableName);
            DataColumn column = null;

            column = new DataColumn();
            column.Caption = "区域名称";
            column.ColumnName = "RegionName";
            column.DataType = typeof(System.String);
            dt.Columns.Add(column);

            if (DataTableName == "建筑物表")
            {
                column = new DataColumn();
                column.Caption = "建筑物";
                column.ColumnName = "建筑物";
                column.DataType = typeof(System.String);
                dt.Columns.Add(column);
            }

            if (DataTableName == "构筑物表")
            {
                column = new DataColumn();
                column.Caption = "构筑物";
                column.ColumnName = "构筑物";
                column.DataType = typeof(System.String);
                dt.Columns.Add(column);
            }

            if (DataTableName == "绿化表")
            {
                column = new DataColumn();
                column.Caption = "绿化地块";
                column.ColumnName = "绿化";
                column.DataType = typeof(System.String);
                dt.Columns.Add(column);
            }


            if (DataTableName == "道路表")
            {
                column = new DataColumn();
                column.Caption = "道路";
                column.ColumnName = "道路";
                column.DataType = typeof(System.String);
                dt.Columns.Add(column);
            }

            if (DataTableName == "铁路表")
            {
                column = new DataColumn();
                column.Caption = "铁路";
                column.ColumnName = "铁路";
                column.DataType = typeof(System.String);
                dt.Columns.Add(column);
            }

            ESRI.ArcGIS.Geodatabase.ITable table = pFeatureLayer as ESRI.ArcGIS.Geodatabase.ITable;
            ESRI.ArcGIS.Geodatabase.IField field = null;
            for (int i = 0; i < table.Fields.FieldCount; i++)
            {
                if ((pFeatureLayer as ITableFields).get_FieldInfo(i).Visible == true)
                {
                    field = table.Fields.get_Field(i);
                    column = new DataColumn();

                    column.Caption = field.AliasName;
                    column.ColumnName = field.Name;

                    if (field.Domain is ICodedValueDomain)
                    {
                        column.DataType = typeof(System.String);
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeInteger
                        || field.Type == esriFieldType.esriFieldTypeSmallInteger
                        || field.Type == esriFieldType.esriFieldTypeOID)
                    {
                        column.DataType = typeof(System.Int32);
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeSingle)
                    {
                        column.DataType = typeof(System.Single);
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeDouble)
                    {
                        column.DataType = typeof(System.Double);
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeDate)
                    {
                        column.DataType = typeof(System.DateTime);
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeString)
                    {
                        column.DataType = typeof(System.String);
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        column.DataType = typeof(Item);
                    }
                    else
                    {
                        //don't add to dt
                        continue;
                    }

                    dt.Columns.Add(column);
                }
            }

            return dt;
        }
        #endregion

        #region 查询并统计一个图层上与pGeo相交的所有要素，并按地物编码字段分类
        /// <summary>
        /// 查询并统计一个图层上与pGeo相交的所有要素，并按地物编码字段分类
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pGeo"></param>
        /// <param name="queryMethod"></param>
        /// <returns></returns>
//         public static DataTable GetAnalysisResult(IFeatureLayer pFeaLay, IGeometry pGeo, esriSpatialRelEnum queryMethod, bool cutCaculate, out DataTable dtInfo)
//         {
//             ITableFields pTableFields = null; //图层属性表的字段    －－－－－－－－By 袁怀月－－－－－－－－－
//             if (pFeaLay != null) pTableFields = pFeaLay as ITableFields;
// 
//             dtInfo = new DataTable("");
// 
//             try
//             {
//                 bool hasGeoObjNum = pFeaLay.FeatureClass.FindField(GlobalValue.GEO_OBJ_NUM) > -1 ? true : false;
//                 int geoFieldIndex = pFeaLay.FeatureClass.FindField(GlobalValue.GEO_OBJ_NUM);
//                 int lengthFieldIndex = pFeaLay.FeatureClass.FindField(GlobalValue.SHAPE_LENGTH);
//                 int areaFieldIndex = pFeaLay.FeatureClass.FindField(GlobalValue.SHAPE_AREA);     //建筑物的占地面积字段
//                 if (areaFieldIndex == -1) //SHAPE.AREA
//                 {
//                     areaFieldIndex = pFeaLay.FeatureClass.FindField("SHAPE.AREA");
//                 }
//                 int bulidAreaFieldIndex = pFeaLay.FeatureClass.FindField(GlobalValue.BUILDING_AREA);  //建筑物的建筑面积字段
// 
//                 DataTable dtResult = CreateAnalysisResultTable(pFeaLay);//分析结果
//                 dtInfo = CreateAnalysisInfoTable(pFeaLay);  //要素的详细信息
// 
//                 ArrayList features = PublicFunction.SearchFeature(pFeaLay, pGeo, queryMethod, false);
//                 IFeature pFeature;
//                 Hashtable hashTable = new Hashtable();
//                 IField pField;
// 
// 
//                 if (hasGeoObjNum)//有地物编码字段时
//                 {
//                     #region
//                     for (int i = 0; i < features.Count; i++)
//                     {
//                         pFeature = features[i] as IFeature;
//                         DataRow drInfo = dtInfo.NewRow();
//                         for (int a = 0; a < dtInfo.Columns.Count; a++)
//                         {
//                             string columnName = dtInfo.Columns[a].ColumnName;//---------------------by 袁怀月 ------------------------------
//                             if (columnName.ToUpper() == "SHAPE")
//                             {
//                                 drInfo[a] = new Item("几何数据", pFeature.get_Value(pFeature.Fields.FindField(columnName)) as ESRI.ArcGIS.Geometry.IGeometry);
//                             }
//                             else
//                             {
//                                 #region 属性数据
//                                 if (columnName.ToUpper() == GlobalValue.GEO_OBJ_NUM) //"GEOOBJNUM"
//                                 {
//                                     if (pFeature.get_Value(pFeature.Fields.FindField(columnName)).ToString() == "")
//                                     {
//                                         drInfo[a] = "<null>";
//                                     }
//                                     else
//                                     {
//                                         drInfo[a] = pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                     }
//                                 }
//                                 else
//                                 {
//                                     pField = pFeature.Fields.get_Field(pFeature.Fields.FindField(columnName));
//                                     if (pField.Domain is ICodedValueDomain)
//                                     {
//                                         drInfo[a] = PublicFunction.GetCodedDescriptionDomainValue(pField.Domain as ICodedValueDomain, pFeature.get_Value(pFeature.Fields.FindField(columnName)).ToString()) as object;
//                                     }
//                                     else
//                                     {
//                                         try
//                                         {
//                                             if (pField.Type == esriFieldType.esriFieldTypeDouble)
//                                             {
//                                                 Double d = (Double)pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                                 drInfo[a] = d.ToString("0.###");
//                                             }
//                                             else if (pField.Type == esriFieldType.esriFieldTypeSingle)
//                                             {
//                                                 Single s = (Single)pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                                 drInfo[a] = s.ToString("0.###");
//                                             }
//                                             else
//                                             {
//                                                 drInfo[a] = pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                             }
//                                         }
//                                         catch
//                                         {
//                                             drInfo[a] = pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                         }
//                                     }
//                                 }
//                                 #endregion
//                             }
//                         }
//                         dtInfo.Rows.Add(drInfo);
// 
//                         string code = pFeature.get_Value(geoFieldIndex).ToString();//地物编码
//                         if (hashTable[code] == null)//若哈希表中不存在该编码，则向哈希表中添加该编码
//                         {
//                             hashTable.Add(code, new double[5]);
//                         }
// 
//                         double[] tmpVal = (double[])hashTable[code];
// 
//                         switch (pFeaLay.FeatureClass.ShapeType)
//                         {
//                             case esriGeometryType.esriGeometryPoint:	 //fType = "点";
//                             case esriGeometryType.esriGeometryMultipoint://fType = "多点";
//                             case esriGeometryType.esriGeometryMultiPatch://fType = "多面";
//                                 tmpVal = (double[])hashTable[code];
//                                 tmpVal[0]++;
//                                 break;
// 
//                             case esriGeometryType.esriGeometryPolyline: //fType = "线";
//                                 tmpVal = (double[])hashTable[code];
// 
//                                 tmpVal[0]++;
//                                 if (queryMethod == esriSpatialRelEnum.esriSpatialRelContains)//完全包含
//                                 {
//                                     tmpVal[1] += Double.Parse(ConvertNull(pFeature.get_Value(lengthFieldIndex), true));
//                                 }
//                                 else if (queryMethod == esriSpatialRelEnum.esriSpatialRelIntersects)//部分包含
//                                 {
//                                     if (cutCaculate)//切割计算
//                                     {
//                                         tmpVal[1] += GetGeometryLength(GetIntersectResult(pFeature.Shape as IPolyline, pGeo as IPolygon));
//                                     }
//                                     else
//                                     {
//                                         tmpVal[1] += Double.Parse(ConvertNull(pFeature.get_Value(lengthFieldIndex), true));
//                                     }
//                                 }
//                                 break;
// 
//                             case esriGeometryType.esriGeometryPolygon://fType = "多边形";
//                                 double dblFloorArea = 0;
//                                 tmpVal = (double[])hashTable[code];
// 
//                                 tmpVal[0]++;
//                                 if (queryMethod == esriSpatialRelEnum.esriSpatialRelContains)//完全包含
//                                 {
//                                     dblFloorArea = Double.Parse(ConvertNull(pFeature.get_Value(areaFieldIndex), true));
//                                     tmpVal[1] = tmpVal[1] + dblFloorArea;
//                                 }
//                                 else if (queryMethod == esriSpatialRelEnum.esriSpatialRelIntersects)//部分包含
//                                 {
//                                     if (cutCaculate)//切割计算
//                                     {
//                                         dblFloorArea = GetGeometryArea(GetIntersectResult(pFeature.Shape as IPolygon, pGeo as IPolygon));
//                                         tmpVal[1] = tmpVal[1] + dblFloorArea;
//                                     }
//                                     else
//                                     {
//                                         dblFloorArea = Double.Parse(ConvertNull(pFeature.get_Value(areaFieldIndex), true));
//                                         tmpVal[1] = tmpVal[1] + dblFloorArea;
// 
//                                     }
//                                 }
// 
//                                 if (bulidAreaFieldIndex > -1)//建筑面积
//                                 {	//按几何比例分割建筑面积
// 
//                                     if (cutCaculate)//切割计算
//                                     {
//                                         double dblRate = dblFloorArea / Double.Parse(ConvertNull(pFeature.get_Value(areaFieldIndex), true));
//                                         tmpVal[2] += dblRate * Double.Parse(ConvertNull(pFeature.get_Value(bulidAreaFieldIndex), true));
//                                     }
//                                     else
//                                     {
//                                         tmpVal[2] += Double.Parse(ConvertNull(pFeature.get_Value(bulidAreaFieldIndex), true));
//                                     }
//                                 }
//                                 else
//                                 {
//                                     tmpVal[2] += Double.Parse(ConvertNull(pFeature.get_Value(areaFieldIndex), true));
//                                 }
// 
//                                 tmpVal[3] = 100 * tmpVal[1] / BufferArea;
// 
//                                 break;
//                         }
//                     }
//                     #endregion
//                 }
//                 else//无地物编码字段时
//                 {
//                     #region
//                     hashTable.Add("", new double[5]);
//                     double[] tmpVal = (double[])hashTable[""];
// 
//                     for (int i = 0; i < features.Count; i++)
//                     {
//                         pFeature = features[i] as IFeature;
//                         DataRow drInfo = dtInfo.NewRow();
//                         for (int a = 0; a < dtInfo.Columns.Count; a++)
//                         {
//                             string columnName = dtInfo.Columns[a].ColumnName;//---------------------by 袁怀月 ------------------------------
// 
//                             if (columnName.ToUpper() == "SHAPE")
//                             {
//                                 drInfo[a] = new Item("几何数据", pFeature.get_Value(pFeature.Fields.FindField(columnName)) as ESRI.ArcGIS.Geometry.IGeometry);
//                             }
//                             else
//                             {
//                                 #region 属性数据
// 
//                                 pField = pFeature.Fields.get_Field(pFeature.Fields.FindField(columnName));
// 
//                                 if (pField.Domain is ICodedValueDomain)
//                                 {
//                                     drInfo[a] = PublicFunction.GetCodedDescriptionDomainValue(pField.Domain as ICodedValueDomain, pFeature.get_Value(pFeature.Fields.FindField(columnName)).ToString()) as object;
//                                 }
//                                 else
//                                 {
//                                     try
//                                     {
//                                         if (pField.Type == esriFieldType.esriFieldTypeDouble)
//                                         {
//                                             Double d = (Double)pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                             drInfo[a] = d.ToString("0.###");
//                                         }
//                                         else if (pField.Type == esriFieldType.esriFieldTypeSingle)
//                                         {
//                                             Single s = (Single)pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                             drInfo[a] = s.ToString("0.###");
//                                         }
//                                         else
//                                         {
//                                             drInfo[a] = pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                         }
//                                     }
//                                     catch
//                                     {
//                                         drInfo[a] = pFeature.get_Value(pFeature.Fields.FindField(columnName));
//                                     }
//                                 }
//                                 #endregion
//                             }
//                         }
//                         dtInfo.Rows.Add(drInfo);
// 
//                         switch (pFeaLay.FeatureClass.ShapeType)
//                         {
//                             case esriGeometryType.esriGeometryPoint:     //fType = "点";
//                             case esriGeometryType.esriGeometryMultipoint://fType = "多点";
//                             case esriGeometryType.esriGeometryMultiPatch://fType = "多面";
//                                 tmpVal[0]++;
//                                 break;
// 
//                             case esriGeometryType.esriGeometryPolyline: //fType = "线";
//                                 tmpVal[0]++;
//                                 if (queryMethod == esriSpatialRelEnum.esriSpatialRelContains)//完全包含
//                                 {
//                                     tmpVal[1] += Double.Parse(ConvertNull(pFeature.get_Value(lengthFieldIndex), true));
//                                 }
//                                 else if (queryMethod == esriSpatialRelEnum.esriSpatialRelIntersects)//部分包含
//                                 {
//                                     if (cutCaculate)//切割计算
//                                     {
//                                         tmpVal[1] += GetGeometryLength(GetIntersectResult(pFeature.Shape as IPolyline, pGeo as IPolygon));
//                                     }
//                                     else
//                                     {
//                                         tmpVal[1] += Double.Parse(ConvertNull(pFeature.get_Value(lengthFieldIndex), true));
//                                     }
//                                 }
//                                 break;
// 
//                             case esriGeometryType.esriGeometryPolygon://fType = "多边形";
//                                 double dblFloorArea = 0;
//                                 tmpVal[0]++;
//                                 if (queryMethod == esriSpatialRelEnum.esriSpatialRelContains)//完全包含
//                                 {
//                                     dblFloorArea = Double.Parse(ConvertNull(pFeature.get_Value(areaFieldIndex), true));
//                                     tmpVal[1] = tmpVal[1] + dblFloorArea;
//                                 }
//                                 else if (queryMethod == esriSpatialRelEnum.esriSpatialRelIntersects)//部分包含
//                                 {
//                                     if (cutCaculate)//切割计算
//                                     {
//                                         dblFloorArea = GetGeometryArea(GetIntersectResult(pFeature.Shape as IPolygon, pGeo as IPolygon));
//                                         tmpVal[1] = tmpVal[1] + dblFloorArea;
//                                     }
//                                     else
//                                     {
//                                         dblFloorArea = Double.Parse(ConvertNull(pFeature.get_Value(areaFieldIndex), true));
//                                         tmpVal[1] = tmpVal[1] + dblFloorArea;
//                                     }
//                                 }
// 
//                                 if (bulidAreaFieldIndex > -1)//建筑面积
//                                 {	//按几何比例分割建筑面积
//                                     if (cutCaculate)//切割计算
//                                     {
//                                         double dblRate = dblFloorArea / Double.Parse(ConvertNull(pFeature.get_Value(areaFieldIndex), true));
//                                         tmpVal[2] += dblRate * Double.Parse(ConvertNull(pFeature.get_Value(bulidAreaFieldIndex), true));
//                                     }
//                                     else
//                                     {
//                                         tmpVal[2] += Double.Parse(ConvertNull(pFeature.get_Value(bulidAreaFieldIndex), true));
//                                     }
//                                 }
//                                 else
//                                 {
//                                     tmpVal[2] += Double.Parse(ConvertNull(pFeature.get_Value(areaFieldIndex), true));
//                                 }
// 
//                                 tmpVal[3] = 100 * tmpVal[1] / BufferArea;
// 
//                                 break;
//                         }
//                     }
//                     #endregion
//                 }
// 
//                 #region 遍历哈希表填充缓冲区分析统计结果
//                 IDictionaryEnumerator pEnumerator = hashTable.GetEnumerator();
//                 pEnumerator.Reset();
//                 while (pEnumerator.MoveNext())
//                 {
//                     double[] tmpVal = (double[])pEnumerator.Value;
// 
//                     DataRow dataRow_Of_dtResult = dtResult.NewRow();
// 
//                     if (pEnumerator.Key.ToString() == "")
//                     {
//                         dataRow_Of_dtResult[1] = "<null>";
//                         dataRow_Of_dtResult[2] = "编码为空";
//                     }
//                     else
//                     {
//                         dataRow_Of_dtResult[1] = pEnumerator.Key.ToString();
//                         dataRow_Of_dtResult[2] = PublicFunction.GetGISName(pEnumerator.Key.ToString());
//                     }
// 
//                     dataRow_Of_dtResult[3] = tmpVal[0];//数量
// 
//                     switch (pFeaLay.FeatureClass.ShapeType)
//                     {
//                         case esriGeometryType.esriGeometryPoint:     //fType = "点";
//                         case esriGeometryType.esriGeometryMultipoint://fType = "多点";
//                         case esriGeometryType.esriGeometryMultiPatch://fType = "多面";
//                             break;
// 
//                         case esriGeometryType.esriGeometryPolyline:  //fType = "线";
//                             dataRow_Of_dtResult[4] = tmpVal[1] == 0 ? "0" : tmpVal[1].ToString("#.##");//长度
//                             break;
// 
//                         case esriGeometryType.esriGeometryPolygon:   //fType = "多边形";
//                             dataRow_Of_dtResult[4] = tmpVal[1] == 0 ? "0" : tmpVal[1].ToString("#.##");
// 
//                             //若为建筑物、或居民地 、绿化，则需要计算“比例”，否则无需计算“比例”
//                             if (pFeaLay.Name == GlobalValue.NAME_OF_BUD_PLYGON || pFeaLay.Name == GlobalValue.NAME_OF_IND_PLYGON || pFeaLay.FeatureClass.AliasName == GlobalValue.NAME_OF_RES_PLYGON || pFeaLay.FeatureClass.AliasName == GlobalValue.NAME_OF_VIR_PLYGON ||
//                                 pFeaLay.Name == GlobalValue.NAME_OF_BUD_PLYGON + "_2D" || pFeaLay.Name == GlobalValue.NAME_OF_IND_PLYGON + "_2D" || pFeaLay.FeatureClass.AliasName == GlobalValue.NAME_OF_RES_PLYGON + "_2D" || pFeaLay.FeatureClass.AliasName == GlobalValue.NAME_OF_VIR_PLYGON + "_2D")
//                             {
//                                 dataRow_Of_dtResult[5] = tmpVal[2] == 0 ? "0" : tmpVal[2].ToString("#.##");
//                                 dataRow_Of_dtResult[6] = tmpVal[3] == 0 ? "0" : tmpVal[3].ToString("#.##");	//比例					
//                             }
//                             else
//                             {
//                                 try
//                                 {
//                                     dataRow_Of_dtResult[5] = tmpVal[3] == 0 ? "0" : tmpVal[3].ToString("#.##");
//                                 }
//                                 catch
//                                 {
//                                     dataRow_Of_dtResult[5] = tmpVal[3] == 0 ? "0" : tmpVal[3].ToString();
//                                 }
//                             }
//                             break;
//                     }
// 
//                     dtResult.Rows.Add(dataRow_Of_dtResult);
//                 }
//                 #endregion
// 
//                 return dtResult;
//                 //         0      序号
//                 //         1      地物编码
//                 //         2      地物名称
//                 //         3      要素数量
//                 //         4      总长度
//                 //         5      总占地面积
//                 //         6      总建筑面积
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine(ex.Message + ex.StackTrace);
//                 return null;
//             }
//         }
        # endregion



//         #region
//         /// <summary>
//         /// 根据地物编码得到地物名称
//         /// </summary>
//         /// <param name="code"></param>
//         /// <returns></returns>
//         public static string GetGISName(string code)
//         {
//             if (DFGISCodeTree.GISCodes[code] != null)
//             {
//                 return DFGISCodeTree.GISCodes[code].ToString();
//             }
//             else
//             {
//                 return "";
//             }
//         }
// 
//         #endregion

        #region 空值转换，null转换成""
        /// <summary>
        /// 空值转换，null转换成""
        /// </summary>
        /// <param name="val"></param>
        /// <param name="isNumber"></param>
        /// <returns></returns>
        public static string ConvertNull(object val, bool isNumber)
        {
            if (val == null || val == System.DBNull.Value)
            {
                if (!isNumber)
                {
                    return "";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return val.ToString();
            }
        }
        #endregion

        #region 得到一个polyline上投影点为pPoint的点
        /// <summary>
        /// 得到一个polyline上投影点为pPoint的点
        /// </summary>
        /// <param name="pLine"></param>
        /// <param name="pPoint"></param>
        /// <returns></returns>
        public static void GetPolylinePoint(IPolyline pLine, ref IPoint pPoint)
        {
            ISegmentCollection pSegColl = pLine as ISegmentCollection;
            ISegment pSeg = null;
            if (pSegColl != null)
            {
                bool onSegment = false;
                for (int i = 0; i < pSegColl.SegmentCount; i++)
                {
                    pSeg = pSegColl.get_Segment(i);
                    if (pSeg.FromPoint.X <= pSeg.ToPoint.X)
                    {
                        if (pSeg.FromPoint.Y <= pSeg.ToPoint.Y)
                        {
                            if (pSeg.FromPoint.X <= pPoint.X &&
                                pSeg.ToPoint.X >= pPoint.X &&
                                pSeg.FromPoint.Y <= pPoint.Y &&
                                pSeg.ToPoint.Y >= pPoint.Y)
                            {
                                onSegment = true;
                            }
                        }
                        else
                        {
                            if (pSeg.FromPoint.X <= pPoint.X &&
                                pSeg.ToPoint.X >= pPoint.X &&
                                pSeg.FromPoint.Y >= pPoint.Y &&
                                pSeg.ToPoint.Y <= pPoint.Y)
                            {
                                onSegment = true;
                            }
                        }
                    }
                    else
                    {
                        if (pSeg.FromPoint.Y <= pSeg.ToPoint.Y)
                        {
                            if (pSeg.FromPoint.X >= pPoint.X &&
                                pSeg.ToPoint.X <= pPoint.X &&
                                pSeg.FromPoint.Y <= pPoint.Y &&
                                pSeg.ToPoint.Y >= pPoint.Y)
                            {
                                onSegment = true;
                            }
                        }
                        else
                        {
                            if (pSeg.FromPoint.X >= pPoint.X &&
                                pSeg.ToPoint.X <= pPoint.X &&
                                pSeg.FromPoint.Y >= pPoint.Y &&
                                pSeg.ToPoint.Y <= pPoint.Y)
                            {
                                onSegment = true;
                            }
                        }
                    }
                }
                if (onSegment)
                {
                    GetPointOnSegment(pSeg, ref pPoint);
                }
            }
        }
        #endregion

        #region 闪烁要素
        /// <summary>
        /// 闪烁要素
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="mapControl"></param>
        public static void FlashShape(ESRI.ArcGIS.Geometry.IGeometry shape, ESRI.ArcGIS.Controls.IMapControl2 mapControl)
        {
            mapControl.FlashShape(shape, 1, 100, PublicFunction.GetDefaultSymbol(shape.GeometryType));
        }

        #endregion

        #region 得到文件的保存路径,如果文件已经存在，删除它
        /// <summary>
        /// 得到文件的保存路径,如果文件已经存在，删除它
        /// </summary>
        /// <param name="mainForm"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string GetSaveFileName(System.Windows.Forms.Form mainForm, string filter)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = filter;
            if (mainForm == null)
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("删除已存在的文件失败！文件已打开或windows用户权限不够。");
                        return "";
                    }
                    return sfd.FileName;
                }
                return "";
            }
            else
            {
                if (sfd.ShowDialog(mainForm) == DialogResult.OK)
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("删除已存在的文件失败！文件已打开或windows用户权限不够。");
                        return "";
                    }
                    return sfd.FileName;
                }
                return "";
            }
        }
        #endregion

        #region 得到两个Geometry相交的部分
        /// <summary>
        /// 得到一条线与参考面相交的部分
        /// </summary>
        /// <param name="sourcePolyline"></param>
        /// <param name="refGeo"></param>
        /// <returns></returns>
        public static IGeometry GetIntersectResult(IPolyline sourcePolyline, IPolygon refGeo)
        {
            IGeometry pGeometry = null;
            IPolycurve pPolyCurve;
            ITopologicalOperator pTop = sourcePolyline as ITopologicalOperator;
            if (pTop != null)
            {
                try
                {
                    pPolyCurve = sourcePolyline as IPolycurve;
                    pPolyCurve.Generalize(0.05);
                    pGeometry = pTop.Intersect(refGeo, esriGeometryDimension.esriGeometry1Dimension);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                }
            }
            return pGeometry;
        }

        /// <summary>
        /// 得到一个面与参考面相交的部分
        /// </summary>
        /// <param name="sourcePolygon"></param>
        /// <param name="refGeo"></param>
        /// <returns></returns>
        public static IGeometry GetIntersectResult(IPolygon sourcePolygon, IPolygon refGeo)
        {
            IGeometry pGeometry = null;
            IPolycurve pPolyCurve;
            ITopologicalOperator pTop = sourcePolygon as ITopologicalOperator;
            if (pTop != null)
            {
                try
                {
                    pPolyCurve = sourcePolygon as IPolycurve;
                    pPolyCurve.Generalize(0.05);
                    pGeometry = pTop.Intersect(refGeo, esriGeometryDimension.esriGeometry2Dimension);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                }
            }
            return pGeometry;
        }
        #endregion

        #region 得到Geometry的长度和面积
        /// <summary>
        /// 得到Geometry的长度
        /// </summary>
        /// <param name="pGeometry"></param>
        /// <returns></returns>
        public static double GetGeometryLength(IGeometry pGeometry)
        {
            if (pGeometry == null) return 0;
            if (pGeometry.IsEmpty == true) return 0;

            double result = 0;
            if (pGeometry is IPolyline)
            {
                result = (pGeometry as IPolyline).Length;
            }
            else if (pGeometry is IPolygon)
            {
                result = (pGeometry as IPolygon).Length;
            }
            return result;
        }

        /// <summary>
        /// 得到多边形Geometry的面积
        /// </summary>
        /// <param name="pPolygon"></param>
        /// <returns></returns>
        public static double GetGeometryArea(IGeometry pGeometry)
        {
            if (pGeometry == null) return 0;
            if (pGeometry.IsEmpty == true) return 0;

            IArea pArea = pGeometry as IArea;
            if (pArea != null)
            {
                return pArea.Area;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 得到线段上一点的Z、M值
        /// <summary>
        /// 得到线段上一点的Z、M值
        /// </summary>
        /// <param name="pSeg"></param>
        /// <param name="pPoint"></param>
        private static void GetPointOnSegment(ISegment pSeg, ref IPoint pPoint)
        {
            double length = Math.Sqrt(Math.Pow((pPoint.X - pSeg.FromPoint.X), 2) + Math.Pow((pPoint.Y - pSeg.FromPoint.Y), 2));
            double distanceRate = length / pSeg.Length;
            pPoint.Z = pSeg.FromPoint.Z + (-pSeg.FromPoint.Z + pSeg.ToPoint.Z) * distanceRate;
            pPoint.M = pSeg.FromPoint.M + (-pSeg.FromPoint.M + pSeg.ToPoint.M) * distanceRate;
            pPoint.Z = double.Parse(pPoint.Z.ToString("0.000"));
            pPoint.M = double.Parse(pPoint.M.ToString("0.000"));
        }
        #endregion

        #region 删除地图控件上的所有选中要素
        /// <summary>
        /// 删除地图控件上的所有选中要素
        /// </summary>
        /// <param name="mapControl"></param>
        public static void DeleteFeatureSelection(IMapControl2 mapControl)
        {
            IActiveView activeView = mapControl.ActiveView;
            IEnumFeature pEnumFeature = activeView.Selection as IEnumFeature;
            if (pEnumFeature != null)
            {
                pEnumFeature.Reset();
                IFeature pFeature = pEnumFeature.Next();
                if (pFeature == null) return;
                IWorkspaceEdit pEdit = (pFeature.Class as IDataset).Workspace as IWorkspaceEdit;
                if (pEdit.IsBeingEdited())
                {
                    pEdit.StartEditOperation();
                    while (pFeature != null)
                    {
                        pFeature.Delete();
                        pFeature = pEnumFeature.Next();
                    }
                    pEdit.StopEditOperation();
                    activeView.Refresh();
                }
            }
        }
        #endregion

        #region 裁切一个几何对象
        /// <summary>
        /// 裁切一个几何对象
        /// </summary>
        /// <param name="inGeometry">传入要被裁切的几何对象</param>
        /// <param name="geometryType">几何对象的类型</param>
        /// <param name="clipPolygon">用于裁切的多边形</param>
        /// <param name="outGeometry">付出裁切后的几何对象</param>
        /// <returns>是否有被裁切的对象</returns>
        static public IGeometry ClipGeometry(IGeometry inGeometry, IPolygon clipPolygon)
        {
            IGeometry outGeometry = null;
            IPolycurve pPolyCurve;
            IGeometry tempGeometry = null;

            if (inGeometry == null) return outGeometry;

            ITopologicalOperator topologyOper = clipPolygon as ITopologicalOperator;
            topologyOper.Simplify();
            IRelationalOperator relationOper = clipPolygon as IRelationalOperator;

            switch (inGeometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    //如果点包含在多边形中
                    if (relationOper.Contains(inGeometry))
                    {
                        tempGeometry = inGeometry;
                    }
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    //'如果多义线穿越多边形或包含在多边形中
                    if (relationOper.Contains(inGeometry))
                        tempGeometry = inGeometry;
                    else //if(relationOper.Crosses(inGeometry))
                    {
                        pPolyCurve = inGeometry as IPolycurve;
                        pPolyCurve.Generalize(0.05);

                        try
                        {
                            tempGeometry = topologyOper.Intersect(inGeometry as IPolyline, esriGeometryDimension.esriGeometry1Dimension);
                        }
                        catch
                        {
                            try
                            {
                                IPolyline tempPolyline = inGeometry as IPolyline;
                                tempPolyline.ReverseOrientation();
                                tempGeometry = topologyOper.Intersect(tempPolyline, esriGeometryDimension.esriGeometry1Dimension);
                            }
                            catch
                            {
                                return inGeometry;
                            }
                        }
                    }
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                case esriGeometryType.esriGeometryPolygon:
                    //如果多边形与裁剪多边形相交或在裁剪多边形中
                    if (relationOper.Contains(inGeometry))
                        tempGeometry = inGeometry;
                    else //if(relationOper.Touches(inGeometry))
                    {
                        pPolyCurve = inGeometry as IPolycurve;
                        pPolyCurve.Generalize(0.05);

                        try
                        {
                            tempGeometry = topologyOper.Intersect(inGeometry, esriGeometryDimension.esriGeometry2Dimension);
                        }
                        catch
                        {
                            try
                            {
                                IPolygon tempPolygon = inGeometry as IPolygon;
                                tempPolygon.ReverseOrientation();
                                tempGeometry = topologyOper.Intersect(tempPolygon, esriGeometryDimension.esriGeometry2Dimension);
                            }
                            catch
                            {
                                return inGeometry;
                            }
                        }

                    }
                    break;
            }
            outGeometry = tempGeometry;
            return outGeometry;
        }
        #endregion

        #region 删除该函数
        //public static void GetAllFeatureLayer(IMap pMap, ArrayList list)
        //{
        //    for (int i = 0; i < pMap.LayerCount; i++)
        //    {
        //        ILayer curLayer = pMap.get_Layer(i);
        //        if ((curLayer.MaximumScale == 0 && curLayer.MinimumScale == 0)
        //            || (curLayer.MaximumScale == 0 && pMap.MapScale < curLayer.MinimumScale)
        //            || (curLayer.MinimumScale == 0 && pMap.MapScale > curLayer.MaximumScale)
        //            || (curLayer.MaximumScale < pMap.MapScale && pMap.MapScale < curLayer.MinimumScale))
        //        {
        //            AddLayersToArrayList(pMap, curLayer, list);
        //        }
        //    }
        //}

        //public static void AddLayersToArrayList(IMap pMap, ILayer pLayer, ArrayList list)
        //{
        //    ICompositeLayer pComp;
        //    if (pLayer is IGroupLayer)
        //    {
        //        pComp = (ICompositeLayer)pLayer;
        //        for (int i = 0; i < pComp.Count; i++)
        //        {
        //            //递归
        //            ILayer tempLayer = pComp.get_Layer(i) as ILayer;
        //            if ((tempLayer.MaximumScale == 0 && tempLayer.MinimumScale == 0)
        //                || (tempLayer.MaximumScale == 0 && pMap.MapScale < tempLayer.MinimumScale)
        //                || (tempLayer.MinimumScale == 0 && pMap.MapScale > tempLayer.MaximumScale)
        //                || (tempLayer.MaximumScale < pMap.MapScale && pMap.MapScale < tempLayer.MinimumScale))
        //            {
        //                AddLayersToArrayList(pMap, pComp.get_Layer(i), list);
        //            }

        //        }
        //    }

        //    else if ((pLayer is IFeatureLayer) && !(pLayer is ICadLayer))
        //    {
        //        if (list != null)
        //        {
        //            IFeatureLayer pFeaLay = pLayer as IFeatureLayer;
        //            if (!list.Contains(pFeaLay) && pFeaLay.Visible == true)//--------By 袁怀月--------------
        //            {
        //                if ((pFeaLay.MaximumScale == 0 && pFeaLay.MinimumScale == 0)
        //                    || (pFeaLay.MaximumScale == 0 && pMap.MapScale < pFeaLay.MinimumScale)
        //                    || (pFeaLay.MinimumScale == 0 && pMap.MapScale > pFeaLay.MaximumScale)
        //                    || (pFeaLay.MaximumScale < pMap.MapScale && pMap.MapScale < pFeaLay.MinimumScale))
        //                {
        //                    list.Add(pFeaLay);
        //                }
        //            }
        //        }
        //    }

        //}
        #endregion

    }
}
