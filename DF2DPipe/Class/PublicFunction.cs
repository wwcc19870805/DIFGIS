using System;
using System.Globalization;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;

namespace DF2DPipe.Class
{
    public class PublicFunction
    {
        public static double BufferArea; //-----------------------By 袁怀月---------------
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
    }
}
