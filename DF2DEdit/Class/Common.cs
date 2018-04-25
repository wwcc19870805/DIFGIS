/*----------------------------------------------------------------
            // Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：Common.cs
            // 文件功能描述：公共函数类
            //               
            // 
            // 创建标识：LuoXuan
            //
            // 修改描述：

----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using DevExpress.XtraEditors;
using DF2DEdit.Form;

namespace DF2DEdit.Class
{
    public class Common
    {
        public static IArray m_SelectArray = new ArrayClass();
        public static IArray m_OriginArray = new ArrayClass();
        public static IArray m_pFeatureLayerArray = new ArrayClass();//要素图层
        private static ILayer m_pCurEditLayer;//当前编辑图层
        private static IWorkspaceEdit m_pCurWspEdit;//当前工作空间编辑
        public static frmModifyFeaturePro System_ModifyFeature_Form;//编辑要素属性对话框

        /// <summary>
        /// 当前编辑图层
        /// </summary>
        public static ILayer CurEditLayer
        {
            get
            {
                return m_pCurEditLayer;
            }
            set
            {
                m_pCurEditLayer = value;
            }
        }

        /// <summary>
        /// 当前工作空间编辑
        /// </summary>
        public static IWorkspaceEdit CurWspEdit
        {
            get
            {
                if (m_pCurEditLayer == null)
                {
                    return null;
                }
                else
                {
                    m_pCurWspEdit = (m_pCurEditLayer as IFeatureLayer).FeatureClass.FeatureDataset.Workspace as IWorkspaceEdit;
                    return m_pCurWspEdit;
                }
            }
            set
            {
                m_pCurWspEdit = value;
            }
        }


        #region//拷贝要素
        /// <summary>
        /// 拷贝要素
        /// </summary>
        /// <param name="pMapControl"></param>
        public static void CopyObjects(IMapControl2 pMapControl)
        {
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IMap pMap;

            m_SelectArray.RemoveAll();//清空选择数组

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;

            pMap = pMapControl.Map;
            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象

            //将选中的要素存入选择数组中
            pSelected.Reset();
            pFeature = pSelected.Next();
            while (pFeature != null)
            {
                m_SelectArray.Add(pFeature);
                pFeature = pSelected.Next();
            }
        }
        #endregion

        #region//剪切要素
        /// <summary>
        /// 剪切要素
        /// </summary>
        /// <param name="pMapControl"></param>
        public static void CutObjects(IMapControl2 pMapControl)
        {
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IMap pMap;
            IArray cutObjectsArray = new ArrayClass();

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;

            pMap = pMapControl.Map;
            pGeoFeatureLayers = GeoFeatureLayers(pMap);
            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = CurWspEdit;
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;
            pWorkspaceEdit.StartEditOperation();

            m_SelectArray.RemoveAll();//清空选择数组

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象

            //将选中的要素存入选择数组和删除数组中
            pSelected.Reset();
            pFeature = pSelected.Next();
            while (pFeature != null)
            {
                cutObjectsArray.Add(pFeature);
                m_SelectArray.Add(pFeature);
                pFeature = pSelected.Next();
            }

            //删除要素
            for (int i = 0; i < cutObjectsArray.Count; i++)
            {
                pFeature = (IFeature)cutObjectsArray.get_Element(i);
                pFeature.Delete();
            }

            pWorkspaceEdit.StopEditOperation();

            pMapControl.ActiveView.Refresh();
        }
        #endregion

        #region//粘贴要素
        /// <summary>
        /// 粘贴要素
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pFeatureLayer"></param>
        /// <param name="bPolylinePolygonChange"></param>
        /// <param name="dblpolylineToPolygonTolerence"></param>
        /// <param name="bCopyPorperty"></param>
        public static void PlastObjects(IMapControl2 pMapControl, IFeatureLayer pFeatureLayer,
            bool bPolylinePolygonChange, double dblpolylineToPolygonTolerence, bool bCopyPorperty)
        {
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;
            IMap pMap;
            IFeature pFeature;
            IPoint pPoint;
            IPolyline pPolyline;
            IPolygon pPolygon;

            m_OriginArray.RemoveAll();//清空源数组

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;
            if (m_SelectArray.Count == 0) return;

            pMap = pMapControl.Map;
            pGeoFeatureLayers = GeoFeatureLayers(pMap);
            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = CurWspEdit;
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;
            pWorkspaceEdit.StartEditOperation();

            //根据目标图层的类型过滤选择的要素，将待转化或拷贝的要素存入源数组中
            for (int i = 0; i < m_SelectArray.Count; i++)
            {
                pFeature = (IFeature)m_SelectArray.get_Element(i);
                if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint && pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {//若为点图层
                    m_OriginArray.Add((IFeature)m_SelectArray.get_Element(i));
                }
                else if ((pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline || pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                    && (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline || pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                {//若为线、面图层，要进行线面转化
                    m_OriginArray.Add((IFeature)m_SelectArray.get_Element(i));
                }
            }

            //转化或拷贝要素到目标图层
            for (int i = 0; i < m_OriginArray.Count; i++)
            {
                pFeature = (IFeature)m_OriginArray.get_Element(i);

                if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)//粘贴点到点图层
                {
                    pPoint = (IPoint)pFeature.Shape;
                    //					pPoint.Z = Convert.ToDouble( pFeature.get_Value(pFeature.Fields.FindField("Elevation")));
                    //					CreateFeature(pPoint as IGeometry,pMap,pFeatureLayer);

                    IFeatureClass featureClassIn = pFeatureLayer.FeatureClass;
                    IFeatureCursor featureCursorInsert = featureClassIn.Insert(true);

                    IFeatureBuffer featureBufferInsert = featureClassIn.CreateFeatureBuffer();
                    featureBufferInsert.Shape = pPoint;
                    if (bCopyPorperty)
                    {
                        AddFields_FromSourceFeatureToObjectFeature(featureBufferInsert, pFeature);
                    }

                    featureCursorInsert.InsertFeature(featureBufferInsert);
                }
                else if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)//粘贴线和面到线图层
                {
                    if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)//若将要处理要素为线
                    {
                        pPolyline = (IPolyline)pFeature.Shape;
                        InsertFeature(pFeatureLayer, pFeature, pPolyline, bCopyPorperty);
                    }
                    else if ((pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon) && bPolylinePolygonChange)//若将要处理要素为面
                    {
                        pPolyline = GetPolygonBoundary((IPolygon)pFeature.Shape);
                        InsertFeature(pFeatureLayer, pFeature, pPolyline, bCopyPorperty);
                    }
                }
                else if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)//粘贴线和面到面图层
                {
                    if ((pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline) && bPolylinePolygonChange)//若将要处理要素为线
                    {
                        IPoint pFromPoint = ((IPolyline)pFeature.Shape).FromPoint;
                        IPoint pToPoint = ((IPolyline)pFeature.Shape).ToPoint;
                        double Dist = Class.Common.GetDistance_P12(pFromPoint, pToPoint);
                        if (Dist <= dblpolylineToPolygonTolerence)
                        {
                            Class.Common.AdjustPolyline((IPolyline)pFeature.Shape, false);
                            pPolygon = Class.Common.PolylineToPolygon((IPolyline)pFeature.Shape);
                            InsertFeature(pFeatureLayer, pFeature, pPolygon, bCopyPorperty);
                        }
                    }
                    else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)//若将要处理要素为面
                    {
                        pPolygon = (IPolygon)pFeature.Shape;
                        InsertFeature(pFeatureLayer, pFeature, pPolygon, bCopyPorperty);
                    }
                }
            }

            m_OriginArray.RemoveAll();

            pWorkspaceEdit.StopEditOperation();

            pMapControl.ActiveView.Refresh();


        }
        private static void InsertFeature(IFeatureLayer pFeatureLayer, IFeature pFeature, IPolyline pPolyline, bool bCopyPorperty)
        {
            IFeatureClass featureClassIn = pFeatureLayer.FeatureClass;
            IFeatureCursor featureCursorInsert = featureClassIn.Insert(true);

            IFeatureBuffer featureBufferInsert = featureClassIn.CreateFeatureBuffer();
            featureBufferInsert.Shape = pPolyline;
            if (bCopyPorperty)
            {
                AddFields_FromSourceFeatureToObjectFeature(featureBufferInsert, pFeature);
            }

            featureCursorInsert.InsertFeature(featureBufferInsert);
        }

        private static void InsertFeature(IFeatureLayer pFeatureLayer, IFeature pFeature, IPolygon pPolygon, bool bCopyPorperty)
        {
            IFeatureClass featureClassIn = pFeatureLayer.FeatureClass;
            IFeatureCursor featureCursorInsert = featureClassIn.Insert(true);

            IFeatureBuffer featureBufferInsert = featureClassIn.CreateFeatureBuffer();
            featureBufferInsert.Shape = pPolygon;
            if (bCopyPorperty)
            {
                AddFields_FromSourceFeatureToObjectFeature(featureBufferInsert, pFeature);
            }

            featureCursorInsert.InsertFeature(featureBufferInsert);
        }

        #endregion

        #region//拷贝属性
        /// <summary>
        /// 拷贝属性
        /// </summary>
        /// <param name="featureBuffer"></param>
        /// <param name="pSourceFeature"></param>
        public static void AddFields_FromSourceFeatureToObjectFeature(IFeatureBuffer featureBuffer, IFeature pSourceFeature)
        {//创建要素缓冲：将目标要素的属性字段逐一和源要素的字段比较，若字段名称和类型相同，则将源要素的属性拷贝到目标要素，否则保留目标要素的字段属性
            IRowBuffer rowBuffer = (IRowBuffer)featureBuffer;
            IFields fieldsNew = rowBuffer.Fields;

            IFields fields = featureBuffer.Fields;
            for (int i = 0; i <= fields.FieldCount - 1; i++)
            {
                IField field = fields.get_Field(i);
                if (field.Editable == true)//目标要素的字段可编辑
                {
                    if (field.Type != esriFieldType.esriFieldTypeOID)
                    {
                        string FieldName = field.Name.ToUpper();

                        if (FieldName != "SHAPE" && FieldName != "SHAPE_AREA" && FieldName != "SHAPE_LENGTH")
                        {
                            for (int j = 0; j < pSourceFeature.Fields.FieldCount; j++)
                            {
                                if ((pSourceFeature.Fields.get_Field(j).Name.ToUpper() == featureBuffer.Fields.get_Field(i).Name.ToUpper()) &&
                                    (pSourceFeature.Fields.get_Field(j).Type == featureBuffer.Fields.get_Field(i).Type)) //名称和类型一致
                                {
                                    try
                                    {
                                        if (pSourceFeature.get_Value(j) != null)
                                        {
                                            featureBuffer.set_Value(i, pSourceFeature.get_Value(j));
                                        }
                                    }
                                    catch { }
                                }
                            }

                        }

                    }
                }
            }
        }
        #endregion

        #region//根据配置文件参数处理线要素，是取起止点坐标的几何平均值，还是增加一条边来闭合线要素
        /// <summary>
        /// 根据配置文件参数处理线要素，是取起止点坐标的几何平均值，还是增加一条边来闭合线要素
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <param name="bAddLine"></param>
        /// <returns></returns>
        public static IPolyline AdjustPolyline(IPolyline pPolyline, bool bAddLine)
        {
            IPoint pPoint = new PointClass();
            IPoint pFromPoint = new PointClass();
            IPoint pToPoint = new PointClass();

            IPointCollection pPointCollection;
            pPointCollection = (IPointCollection)pPolyline;

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            if (bAddLine)//首尾相连
            {
                pPointCollection.AddPoint(pPointCollection.get_Point(0), ref a, ref b);
            }
            else//取几何平均值
            {
                pFromPoint = pPointCollection.get_Point(0);
                pToPoint = pPointCollection.get_Point(pPointCollection.PointCount - 1);
                pPoint.PutCoords((pFromPoint.X + pToPoint.X) / 2, (pFromPoint.Y + pToPoint.Y) / 2);

                pPointCollection.UpdatePoint(0, pPoint);
                pPointCollection.UpdatePoint(pPointCollection.PointCount - 1, pPoint);

            }

            return (IPolyline)pPointCollection;

        }
        #endregion

        #region//删除地理要素
        /// <summary>
        /// 删除地理要素
        /// </summary>
        /// <param name="pMap"></param>
        public static void DeleteFeature(IMap pMap)
        {
            IActiveView pActiveView;
            IFeatureCursor pFCursor;
            IFeature pFeatrue;
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;

            pGeoFeatureLayers = GeoFeatureLayers(pMap);

            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = CurWspEdit;
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;

            pWorkspaceEdit.StartEditOperation();

            for (int i = 0; i < pGeoFeatureLayers.Count; i++)
            {
                pFCursor = GetSelectedFeatures((ILayer)pGeoFeatureLayers.get_Element(i));

                if (pFCursor == null) continue;

                pFeatrue = pFCursor.NextFeature();
                while (pFeatrue != null)
                {
                    pFeatrue.Delete();
                    pFeatrue = pFCursor.NextFeature();
                }

            }
            pWorkspaceEdit.StopEditOperation();

            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();
        }
        #endregion

        #region//删除存入数组中的要素
        /// <summary>
        /// 剪切要素
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pFeatureArray"></param>

        public static void DelFeaturesFromArray(IMapControl2 pMapControl, ref IArray pFeatureArray)
        {
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;
            IFeature pFeature;//IFeature指单个图形对象
            IMap pMap;

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;
            if (pFeatureArray.Count == 0) return;

            pMap = pMapControl.Map;
            pGeoFeatureLayers = GeoFeatureLayers(pMap);
            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = CurWspEdit;
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;
            pWorkspaceEdit.StartEditOperation();

            //删除要素
            for (int i = 0; i < pFeatureArray.Count; i++)
            {
                pFeature = (IFeature)pFeatureArray.get_Element(i);
                pFeature.Delete();
            }

            pWorkspaceEdit.StopEditOperation();

            pFeatureArray.RemoveAll();//清空选择数组

            m_pFeatureLayerArray.RemoveAll();//清空选择数组

            //			CommonFunction.MapRefresh(pMapControl.ActiveView); 			

        }
        #endregion

        #region//将可编辑的地理要素层存入数组中
        /// <summary>
        /// 将可编辑的地理要素层存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IArray GeoFeatureLayers(IMap pMap)
        {
            ILayer pLayer;
            IEnumLayer pLayers;

            if (pMap.LayerCount > 0)
            {
                pLayers = pMap.get_Layers(null, false);
                pLayers.Reset();
                pLayer = pLayers.Next();
                while (pLayer != null)
                {
                    AddLayersTom_FeatureLayerArray(pLayer);
                    pLayer = pLayers.Next();
                }
            }
            return m_pFeatureLayerArray;

        }

        private static void AddLayersTom_FeatureLayerArray(ILayer pLayer)
        {
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;

            if (pLayer is ICompositeLayer)//如果是组合图层
            {
                ICompositeLayer groupLayer = (ICompositeLayer)pLayer;

                for (int j = 0; j < groupLayer.Count; j++)
                {
                    //递归
                    AddLayersTom_FeatureLayerArray(groupLayer.get_Layer(j));
                }
            }
            else if (pLayer is IFeatureLayer)//如果是地理要素图层
            {
                if (pLayer.Visible == true)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            m_pFeatureLayerArray.Add(pLayer);
                        }
                    }
                }
            }
        }

        #endregion

        #region//由图层名称查找工作空间
        /// <summary>
        /// 由图层名称查找工作空间
        /// </summary>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        public static IWorkspace GetLayerWorkspace(ILayer pLayer)
        {
            if (pLayer == null) return null;

            IFeatureLayer pFLayer;
            IFeatureClass pFClass;
            IDataset pDataset;

            pFLayer = (IFeatureLayer)pLayer;
            pFClass = pFLayer.FeatureClass;
            pDataset = (IDataset)pFClass;

            if (pDataset == null)
            {
                return null;
            }
            else
            {
                return pDataset.Workspace;
            }
        }

        #endregion

        #region//获取图层选择要素的游标
        /// <summary>
        /// 获取图层选择要素的游标
        /// </summary>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        private static IFeatureCursor GetSelectedFeatures(ILayer pLayer)
        {
            ICursor pCursor;
            IFeatureSelection pFSelection;
            ISelectionSet pFSet;

            pFSelection = (IFeatureSelection)pLayer;
            pFSet = pFSelection.SelectionSet;

            if (pFSet.Count > 0)
            {
                pFSet.Search(null, false, out pCursor);
                return (IFeatureCursor)pCursor;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region//得到指定字段组合构成的唯一值
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

        #region//空值转换为字符串
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

        #region//获得Domain的描述
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

        #region //获得Domain的值
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
                count += CheckStartEditing(pMap.get_Layer(i), ref compString);
            }
            if (count == 0)
            {
                System.Windows.Forms.MessageBox.Show("不能编辑任何图层，请检查数据是否已经进行了版本注册或是否有更新权限！",
                    "开始编辑", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
            }

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
            DialogResult result;

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
                    result = XtraMessageBox.Show("数据已经被修改过，保存修改吗?", "更改提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

        #region//编辑的撤销/重做
        /// <summary>
        /// 编辑的撤销/重做;
        /// </summary>
        /// <param name="pMap">操作的地图对象 pMap;</param>
        /// <param name="undo">undo = true则做撤销 ，undo = false则做重做</param>
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
                if (pLayer is IFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass == null) return false;
                    pDataset = (IDataset)pFeatureLayer.FeatureClass;
                    if (pDataset != null)
                    {
                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = CurWspEdit;
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
                if (pLayer is IFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = CurWspEdit;
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

        #region//检查停止编辑状态
        /// <summary>
        /// 检查停止编辑状态
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="saveEdits"></param>
        /// <returns></returns>
        public static bool CheckStopEdits(ILayer pLayer, bool saveEdits)
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
                    if (pLayer is IFeatureLayer)
                    {
                        pFeatureLayer = (IFeatureLayer)pLayer;
                        if (pFeatureLayer.FeatureClass == null) return false;
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = CurWspEdit;
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
                if (pLayer is FeatureLayer)//如果是地理要素图层
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;//跳转到IFeatureLayer接口
                    if (pFeatureLayer.FeatureClass == null) return count;
                    pDataset = (IDataset)pFeatureLayer.FeatureClass;//跳转到IDataset接口
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass ||
                        pDataset.Type == esriDatasetType.esriDTFeatureDataset)//如果数据集是要素类或要素数据集
                    {
                        pWorkspaceEdit = CurWspEdit;//跳转到IWorkspaceEdit接口
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
                                    else
                                    {
                                        count = 1;
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
                            else
                            {
                                count = 1;
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

        #region //根据Geometry生成缓冲区
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

        #region //屏幕单位到地图单位的转换
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

        #region//颜色函数
        /// <summary>
        /// 颜色函数
        /// </summary>
        /// <param name="Red"></param>
        /// <param name="Green"></param>
        /// <param name="Blue"></param>
        /// <returns></returns>
        public static IRgbColor GetRgbColor(int Red, int Green, int Blue)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = Red;
            rgbColor.Green = Green;
            rgbColor.Blue = Blue;
            return rgbColor;
        }
        #endregion

        #region //MS和ESRI颜色转换
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

        #region //选择集的颜色
        /// <summary>
        /// 选择集的颜色
        /// </summary>
        /// <returns></returns>
        public static IColor GetSelectionColor(int argb)
        {
            return FromMSColor(argb);
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

            //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pActiveView.Extent);//视图刷新

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pActiveView.Extent);//视图刷新
        }
        #endregion

        #region //检查坐标是否超出地图范围
        public static bool PointIsOutMap(ILayer pLayer, IPoint pPoint)
        {
            double xMin = 0;
            double yMin = 0;
            double xMax = 0;
            double yMax = 0;

            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;

            int index = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
            IGeometryDef pGD = pFeatureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;

            pGD.SpatialReference.GetDomain(out xMin, out xMax, out yMin, out yMax);

            bool isOut = false;
            if ((pPoint.X >= xMin && pPoint.X <= xMax) && (pPoint.Y >= yMin && pPoint.Y <= yMax))
            {
                isOut = true;
            }
            return isOut;
        }
        #endregion

        #region//创建地理要素
        /// <summary>
        /// 创建地理要素
        /// </summary>
        /// <param name="m_App"></param>
        /// <param name="pGeom"></param>
        /// <param name="pMap"></param>
        /// <param name="pCurrentLayer"></param>
        public static void CreateFeature(IGeometry pGeom, IMap pMap, ILayer pCurrentLayer)
        {
            if (pGeom != null)
            {
                IFeatureLayer pFeatureLayer;
                IDataset pDataset;
                IWorkspaceEdit pWorkspaceEdit;
                ILayer pLayer;

                pLayer = pCurrentLayer;

                if (pLayer == null) return;

                if (!(pLayer is IFeatureLayer)) return;

                pFeatureLayer = (IFeatureLayer)pLayer;
                pDataset = (IDataset)pFeatureLayer.FeatureClass;
                pWorkspaceEdit = CurWspEdit;

                int index = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
                IGeometryDef pGD = pFeatureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;
                if (pGD.HasZ)
                {
                    IZAware pZA = (IZAware)pGeom;
                    pZA.ZAware = true;

                    IZ pZ = pGeom as IZ;

                    double zmin = -1000, zmax = 1000;
                    if (pGD.SpatialReference.HasZPrecision())
                    {
                        pGD.SpatialReference.GetZDomain(out zmin, out zmax);
                    }

                    if (pZ != null)
                    {
                        //pZ.SetConstantZ(zmin);
                        pZ.SetConstantZ(0);
                    }
                    else
                    {
                        IPoint p = (pGeom as IPoint);
                        if (p != null)
                        {
                            if (p.Z.ToString() == "非数字")
                            {
                                //p.Z = zmin;
                                p.Z = 0;
                            }

                        }
                    }
                }
                if (pGD.HasM)
                {
                    IMAware pMA = (IMAware)pGeom;
                    pMA.MAware = true;
                }

                pWorkspaceEdit.StartEditOperation();

                IFeature pFeature;
                pFeature = pFeatureLayer.FeatureClass.CreateFeature();

                try
                {
                    pFeature.Shape = pGeom;
                    pFeature.Store();
                }
                catch
                {
                    MessageBox.Show("创建要素时存贮错误");
                }

                pWorkspaceEdit.StopEditOperation();

                pMap.ClearSelection();  //清除所有选择

            }

        }
        #endregion

        #region//在图上用方块状的地图元素显示选中的地理要素的节点
        /// <summary>
        /// 在图上用方块状的地图元素显示选中的地理要素的节点
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pPoint"></param>
        public static void DrawPointSMSSquareSymbol(IMapControl2 pMapControl, IPoint pPoint)
        {
            ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
            pSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;
            if (pMapControl.MapScale != 0)
            {
                pSymbol.Size = 4 * (pMapControl.MapScale / 500); //Edit By 罗璇 2008-12-05
            }
            else
            {
                pSymbol.Size = 4;
            }
            IColor pColor;
            pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pSymbol.Color = pColor;
            IElement pElement = new MarkerElementClass();
            pElement.Geometry = (IGeometry)pPoint;

            IMarkerElement pMarkerElement;
            pMarkerElement = (IMarkerElement)pElement;
            pMarkerElement.Symbol = pSymbol;

            pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            IEnvelope enve = new EnvelopeClass();
            enve = NewRect(pPoint, ConvertPixelsToMapUnits(pMapControl.ActiveView, 4));
            pMapControl.Refresh(esriViewDrawPhase.esriViewGraphics, pElement, enve);

        }
        #endregion

        #region//在地图上用方块闪烁捕捉到的点
        /// <summary>
        /// 在地图上用方块闪烁捕捉到的点
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pPoint"></param>
        public static void DrawSnapSMSSquareSymbol(IMapControl2 pMapControl, IPoint pPoint)
        {
            ISymbol pSym = new SimpleMarkerSymbolClass();
            ISimpleMarkerSymbol pMarkerSym = new SimpleMarkerSymbolClass();
            pMarkerSym.Style = esriSimpleMarkerStyle.esriSMSSquare;
            pMarkerSym.Size = 4;
            IColor pColor;
            pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pMarkerSym.Color = pColor;
            pSym = (ISymbol)pMarkerSym;
            pSym.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            object opSym = pSym;
            pMapControl.DrawShape(pPoint, ref opSym);
            System.Threading.Thread.Sleep(50);
            pMapControl.DrawShape(pPoint, ref opSym);

        }
        #endregion

        #region //高亮显示存入数组中的要素
        /// <summary>
        /// 高亮显示选择
        /// </summary>
        /// <returns>选择的要素高亮显示</returns>
        public static void ShowSelectionFeatureArray(IMapControl2 pMapControl, IArray pFeatureArray)
        {
            //分点、线、面，给新的symbol


            IFeature pFeature;

            m_SelectArray.RemoveAll();//清空选择数组

            for (int index = 0; index < pFeatureArray.Count; index++)
            {
                pFeature = (IFeature)pFeatureArray.get_Element(index);
                if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    IMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
                    pSymbol = (IMarkerSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                    IElement pElement = new MarkerElementClass();
                    pElement.Geometry = pFeature.Shape;

                    IMarkerElement pMarkerElement;
                    pMarkerElement = (IMarkerElement)pElement;
                    pMarkerElement.Symbol = pSymbol;

                    pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, index);

                }
                else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    ILineSymbol pSymbol = new SimpleLineSymbolClass();
                    pSymbol = (ILineSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                    IElement pElement = new LineElementClass();
                    pElement.Geometry = pFeature.Shape;

                    ILineElement pLineElement;
                    pLineElement = (ILineElement)pElement;
                    pLineElement.Symbol = pSymbol;

                    pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, index);

                }
                else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    IElement pElement = new LineElementClass();
                    IPolyline pLine;
                    pLine = GetPolygonBoundary((IPolygon)pFeature.Shape);
                    pElement.Geometry = pLine;

                    ILineSymbol pSymbol = new SimpleLineSymbolClass();
                    pSymbol = (ILineSymbol)GetDefaultSymbol(pLine.GeometryType);

                    ILineElement pLineElement;
                    pLineElement = (ILineElement)pElement;
                    pLineElement.Symbol = pSymbol;

                    pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, index);
                }

                m_SelectArray.Add(pFeature);//将要素添加到选择数组中	

            }

            pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pMapControl.ActiveView.Extent);//视图刷新
        }
        #endregion

        #region //根据Geometry类型生成一个默认符号
        /// <summary>
        /// 根据Geometry类型生成一个默认符号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ISymbol GetDefaultSymbol(esriGeometryType type)
        {
            ISymbol sym = null;

            IRgbColor mCor = new RgbColorClass();
            mCor.Red = 163;
            mCor.Blue = 255;
            mCor.Green = 117;

            IRgbColor lCor = new RgbColorClass();
            lCor.Red = 163;
            lCor.Blue = 255;
            lCor.Green = 117;

            IRgbColor fCor = new RgbColorClass();
            fCor.Red = 163;
            fCor.Blue = 255;
            fCor.Green = 117;

            IMarkerSymbol mark = new SimpleMarkerSymbolClass();
            mark.Color = mCor;
            mark.Size = 8;

            ILineSymbol line = new SimpleLineSymbolClass();
            line.Width = 1.5;
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
                    //sym = (ISymbol)line;
                    break;
            }

            return sym;
        }
        #endregion

        #region //得到一个Polygon对象的轮廓线
        /// <summary>
        /// 得到一个Polygon对象的轮廓线
        /// </summary>
        /// <param name="pPolygon">一个Polygon对象</param>
        /// <returns>一个Polyline对象</returns>
        public static IPolyline GetPolygonBoundary(IPolygon pPolygon)
        {
            //通过ITopologicalOperator 对象转换成线
            ITopologicalOperator pTopo;
            IPolyline pPolyline;

            pTopo = (ITopologicalOperator)pPolygon;
            pPolyline = (IPolyline)pTopo.Boundary;

            return pPolyline;


        }
        #endregion

        #region//将SegmentCollection显示到屏幕
        /// <summary>
        /// 将SegmentCollection显示到屏幕
        /// </summary>
        /// <param name="MapControl"></param>
        /// <param name="PointArray"></param>
        public static void DisplaypSegmentColToScreen(IMapControl2 MapControl, ref IArray PointArray)
        {
            IActiveView pActiveView = MapControl.ActiveView;
            ISegmentCollection pPolylineCol;
            pPolylineCol = new PolylineClass();
            ISegmentCollection pSegmentCollection = MadeSegmentCollection(ref PointArray);
            pPolylineCol.AddSegmentCollection(pSegmentCollection);

            IPointCollection pPointCollection;
            pPointCollection = (IPointCollection)pPolylineCol;

            pActiveView.ScreenDisplay.ActiveCache = (short)esriScreenCache.esriNoScreenCache;
            ISimpleLineSymbol pLineSym = new SimpleLineSymbolClass();
            pLineSym.Color = GetRgbColor(0, 0, 0);

            pActiveView.ScreenDisplay.StartDrawing(pActiveView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            pActiveView.ScreenDisplay.SetSymbol((ISymbol)pLineSym);
            pActiveView.ScreenDisplay.DrawPolyline((IPolyline)pPolylineCol);
            pActiveView.ScreenDisplay.FinishDrawing();

            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                DrawPointSMSSquareSymbol(MapControl, pPointCollection.get_Point(i));
            }
        }
        #endregion

        #region//将数组里的坐标构造SegmentCollection
        /// <summary>
        /// 将数组里的坐标构造SegmentCollection
        /// </summary>
        /// <param name="PointArray"></param>
        /// <returns></returns>
        public static ISegmentCollection MadeSegmentCollection(ref IArray PointArray)
        {
            ISegment pSegment;
            ISegmentCollection pSegmentCollection = new PolylineClass();
            IPoint fromPoint;
            IPoint toPoint;

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            for (int i = 0; i < PointArray.Count - 1; i++)
            {	//获取线起点和端点
                fromPoint = (IPoint)PointArray.get_Element(i);
                toPoint = (IPoint)PointArray.get_Element(i + 1);
                pSegment = new LineClass();
                pSegment = MadeLineSeg_2Point(fromPoint, toPoint);
                pSegmentCollection.AddSegment(pSegment, ref a, ref b);

            }// end for

            return pSegmentCollection;

        }
        #endregion

        #region//两点构造线段的Segment
        /// <summary>
        /// 两点构造线段的Segment
        /// </summary>
        /// <param name="pPoint1"></param>
        /// <param name="pPoint2"></param>
        /// <returns></returns>
        public static ISegment MadeLineSeg_2Point(IPoint pPoint1, IPoint pPoint2)
        {
            ILine pLine = new LineClass();
            pLine.PutCoords(pPoint1, pPoint2);
            return (ISegment)pLine;
        }
        #endregion

        #region //构造一个矩形
        /// <summary>
        /// 构造一个矩形
        /// </summary>
        /// <param name="pPoint"></param>
        /// <param name="dblConst"></param>
        /// <returns></returns>
        public static IEnvelope NewRect(IPoint pPoint, double dblConst)
        {
            IEnvelope pRect;
            pRect = new EnvelopeClass();
            //调整边界的宽度为16个象素大小
            pRect.XMin = pPoint.X - dblConst;
            pRect.YMin = pPoint.Y - dblConst;
            pRect.XMax = pPoint.X + dblConst;
            pRect.YMax = pPoint.Y + dblConst;

            return pRect;
        }
        #endregion

        #region//在图上用X状的地图元素显示镜像、旋转、偏移、移动的基点
        /// <summary>
        /// 在图上用X状的地图元素显示镜像、旋转、偏移、移动的基点
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pPoint"></param>
        public static IElement DrawPointSMSXSymbol(IMapControl2 pMapControl, IPoint pPoint)
        {
            ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
            pSymbol.Style = esriSimpleMarkerStyle.esriSMSX;
            pSymbol.Size = 16;
            pSymbol.OutlineSize = 10;
            IColor pColor;
            pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pSymbol.Color = pColor;
            IElement pElement = new MarkerElementClass();
            pElement.Geometry = (IGeometry)pPoint;

            IMarkerElement pMarkerElement;
            pMarkerElement = (IMarkerElement)pElement;
            pMarkerElement.Symbol = pSymbol;

            pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            IEnvelope enve = new EnvelopeClass();
            enve = NewRect(pPoint, ConvertPixelsToMapUnits(pMapControl.ActiveView, 4));

            pMapControl.Refresh(esriViewDrawPhase.esriViewGraphics, pElement, enve);

            return pElement;

        }
        #endregion

        #region//删除数组中重复的元素，使其元素具有惟一性
        /// <summary>
        /// 删除数组中重复的元素，使其元素具有惟一性
        /// </summary>
        /// <param name="FeatureArray"></param>
        public static void MadeFeatureArrayOnlyAloneOID(IArray FeatureArray)
        {
            IArray tempArray = new ArrayClass();
            object pObject;
            object pTObject;
            int tempInt = 0;

            if (FeatureArray.Count < 2) return;

            tempArray.Add(FeatureArray.get_Element(0));
            for (int i = 1; i < FeatureArray.Count; i++)
            {
                bool equal = false;
                pObject = FeatureArray.get_Element(i);

                for (int j = 0; j < tempArray.Count; j++)
                {
                    pTObject = tempArray.get_Element(j);
                    if (pObject.Equals(pTObject)) equal = true;
                    tempInt = j;
                }

                if (!equal)
                {
                    tempArray.Add(pObject);
                }
                else
                {
                    tempArray.Remove(tempInt);

                }

            }

            FeatureArray.RemoveAll();

            for (int i = 0; i < tempArray.Count; i++)
            {
                FeatureArray.Add(tempArray.get_Element(i));
            }

        }
        #endregion

        #region//捕捉
        /// <summary>
        /// 捕捉
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="cfgsnapEnvironmentSet"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <returns></returns>
        public static bool Snap(IMapControl2 pMapControl, CfgSnapEnvironmentSet cfgsnapEnvironmentSet, IGeometry pGeometry, IPoint pPoint)
        {
            bool bSnap = false;

            if (cfgsnapEnvironmentSet.IsOpen == false) return false;

            if (cfgsnapEnvironmentSet.mapSnap == null) return false;

            if (cfgsnapEnvironmentSet.mapSnap.LayerCount == 0) return false;

            pMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;

            IPoint pInitPoint = new PointClass();
            pInitPoint.X = pPoint.X;
            pInitPoint.Y = pPoint.Y;

            double tolerance;//捕捉容限
            tolerance = cfgsnapEnvironmentSet.Tolerence;
            tolerance = ConvertPixelsToMapUnits(pMapControl.ActiveView, tolerance);//屏幕单位转化到地图单位
            double dblOffsetDist = 1000;

            IFeatureCache2 pFeatureCache = new FeatureCacheClass();
            pFeatureCache.Initialize(pPoint, tolerance);//	

            pFeatureCache.AddLayers(cfgsnapEnvironmentSet.mapSnap.get_Layers(null, true), pMapControl.Extent);//

            IArray ArrayPoint = new ArrayClass();
            IPoint tempPoint0 = new PointClass();
            tempPoint0 = pPoint;
            IPoint tempPoint1 = new PointClass();
            tempPoint1 = pPoint;
            IPoint tempPoint2 = new PointClass();
            tempPoint2 = pPoint;
            IPoint tempPoint3 = new PointClass();
            tempPoint3 = pPoint;
            IPoint tempPoint4 = new PointClass();
            tempPoint4 = pPoint;
            IPoint tempPoint5 = new PointClass();
            tempPoint5 = pPoint;
            IPoint tempPoint6 = new PointClass();
            tempPoint6 = pPoint;
            IPoint tempPoint7 = new PointClass();
            tempPoint7 = pPoint;
            IPoint tempPoint8 = new PointClass();
            tempPoint8 = pPoint;
            IPoint tempPoint9 = new PointClass();
            tempPoint9 = pPoint;

            IPoint pCurPoint = new PointClass();
            pCurPoint = pPoint;

            if (ArrayPoint.Count > 0) ArrayPoint.RemoveAll();

            SnapStruct.BoolSnapMode bSnapMode = cfgsnapEnvironmentSet.SnapMode;

            if (bSnapMode.PartBoundary)//边	
            {
                PartBoundarySnapAgent(pFeatureCache, pGeometry, tempPoint0, tolerance, bSnap);
                if (bSnap) ArrayPoint.Add(tempPoint0);
            }
            if (bSnapMode.PartVertex)//节点
            {
                PartVertexSnapAgent(pFeatureCache, pGeometry, tempPoint2, tolerance, bSnap);
                if (bSnap) ArrayPoint.Add(tempPoint2);
            }
            if (bSnapMode.Endpoint)//端点
            {
                EndpointSnapAgent(pFeatureCache, pGeometry, tempPoint3, tolerance, bSnap);
                if (bSnap) ArrayPoint.Add(tempPoint3);
            }
            if (bSnapMode.Intersection)//交点
            {
                IntersectionSnapAgent(pFeatureCache, pGeometry, tempPoint5, tolerance, bSnap);
                if (bSnap) ArrayPoint.Add(tempPoint5);
            }

            pFeatureCache = null;

            ILine pLine = new LineClass();
            MadeFeatureArrayOnlyAloneOID(ArrayPoint);//保证数组的元素的唯一性
            for (int i = 0; i < ArrayPoint.Count; i++)//求取距离当前点最近的点作为捕捉的点
            {
                pLine.PutCoords(pCurPoint, (IPoint)ArrayPoint.get_Element(i));
                if (pLine.Length < tolerance)//若当前点到捕捉到的点的距离在容限内
                {
                    if (dblOffsetDist > pLine.Length)
                    {
                        dblOffsetDist = pLine.Length;
                        pPoint.PutCoords(((IPoint)ArrayPoint.get_Element(i)).X, ((IPoint)ArrayPoint.get_Element(i)).Y);

                        bSnap = true;
                    }
                }
            }

            if (pInitPoint.X == pPoint.X && pInitPoint.Y == pPoint.Y)
            {
                bSnap = false;
            }
            else
            {
                bSnap = true;
                DrawSnapSMSSquareSymbol(pMapControl, pPoint);
            }

            tempPoint0 = null;
            tempPoint1 = null;
            tempPoint2 = null;
            tempPoint3 = null;
            tempPoint4 = null;
            tempPoint5 = null;
            tempPoint6 = null;
            tempPoint7 = null;
            tempPoint8 = null;
            tempPoint9 = null;

            return bSnap;
        }
        #endregion

        #region//边线捕捉
        /// <summary>
        /// 边线捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void PartBoundarySnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                    bSnap = true;
                }
            }
        }
        #endregion

        #region//节点捕捉
        /// <summary>
        /// 节点捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void PartVertexSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartVertex,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                    bSnap = true;
                }
            }
        }
        #endregion

        #region//端点捕捉
        /// <summary>
        /// 端点捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void EndpointSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartEndpoint,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                    bSnap = true;
                }
            }
        }
        #endregion

        #region//交点捕捉
        /// <summary>
        /// 交点捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void IntersectionSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();

            object a = System.Reflection.Missing.Value;
            IArray pArray = new ArrayClass();
            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest is IPolyline || pHitTest is IPolygon)
                {
                    if (pHitTest.HitTest(pPoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary, hitPoint, ref dblHitDist,
                        ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                    {
                        pArray.Add(pHitTest);
                    }
                }
            }

            IPointCollection pMultipoint = new MultipointClass();
            IArray pArray1 = new ArrayClass();
            ITopologicalOperator2 pTopoOperator;
            for (int i = 0; i < pArray.Count; i++)
            {
                //交点加入到数组中
                pTopoOperator = (ITopologicalOperator2)pArray.get_Element(i);
                for (int j = 0; j < pArray.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (((IGeometry)pArray.get_Element(i)).GeometryType == ((IGeometry)pArray.get_Element(j)).GeometryType)
                    {
                        IGeometry pGeom = pTopoOperator.Intersect((IGeometry)pArray.get_Element(j), esriGeometryDimension.esriGeometry0Dimension);
                        if (pGeom != null)
                        {
                            if (pGeom is IPointCollection)
                            {
                                pMultipoint.AddPointCollection((IPointCollection)pGeom);
                            }
                            else if (pGeom is IPoint)
                            {
                                pMultipoint.AddPoint((IPoint)pGeom, ref a, ref a);
                            }
                        }
                    }
                    else
                    {
                        IGeometry pGeom = pTopoOperator.IntersectMultidimension((IGeometry)pArray.get_Element(j));
                        if (pGeom != null)
                        {
                            IGeometryCollection pGB = pGeom as IGeometryCollection;

                            if (pGB != null)
                            {
                                for (int k = 0; k < pGB.GeometryCount; k++)
                                {
                                    pGeom = pGB.get_Geometry(k);
                                    if (pGeom is IPointCollection)
                                    {
                                        pMultipoint.AddPointCollection((IPointCollection)pGeom);
                                    }
                                    else if (pGeom is IPoint)
                                    {
                                        pMultipoint.AddPoint((IPoint)pGeom, ref a, ref a);
                                    }

                                }

                            }
                        }
                    }
                }
            }
            //从交点数组来判断是否有捕捉到的情况
            pHitTest = (IHitTest)pMultipoint;
            if (pHitTest.HitTest(pPoint, tolerance, esriGeometryHitPartType.esriGeometryPartVertex, hitPoint, ref dblHitDist,
                ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
            {
                pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                bSnap = true;
            }

        }
        #endregion

        #region//将封闭的线要素转化成面要素
        /// <summary>
        /// 将封闭的线要素转化成面要素
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static IPolygon PolylineToPolygon(IPolyline pPolyline)
        {
            IPoint pPoint = new PointClass();
            ISegmentCollection pSegsPolygon;
            ISegmentCollection pSegsPolyline;

            IPolygon pPolygon = new PolygonClass();
            int count;

            pSegsPolyline = (ISegmentCollection)pPolyline;
            count = pSegsPolyline.SegmentCount;

            pSegsPolygon = (ISegmentCollection)pPolygon;

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            for (int i = 0; i < count; i++)
            {
                pSegsPolygon.AddSegment(pSegsPolyline.get_Segment(i), ref a, ref b);
            }

            pPolygon.SimplifyPreserveFromTo();

            return pPolygon;

        }
        #endregion

        #region //获取包含数组点的集合的最小矩形
        /// <summary>
        /// 获取包含数组点的集合的最小矩形
        /// </summary>
        /// <param name="pFeatureArray">包含要素的数组</param>
        public static IEnvelope GetMinEnvelopeOfTheArray(IArray pPointArray)
        {
            IEnvelope pEnvelope = new EnvelopeClass();
            IPolyline pPolyline = new PolylineClass();
            pPolyline = MadeBezierCurve(ref pPointArray);
            pEnvelope = pPolyline.Envelope;

            return pEnvelope;
        }

        #endregion

        #region //获取包含数组中要素的最小矩形
        /// <summary>
        /// 获取包含数组中要素的最小矩形
        /// </summary>
        /// <param name="pFeatureArray">包含要素的数组</param>
        public static IEnvelope GetMinEnvelopeOfTheFeatures(IArray pFeatureArray)
        {
            IEnvelope pEnvelope = new EnvelopeClass();
            if (pFeatureArray.Count != 0)
            {
                pEnvelope = ((IFeature)pFeatureArray.get_Element(0)).Extent;

                for (int i = 1; i < pFeatureArray.Count; i++)
                {
                    pEnvelope.Union(((IFeature)pFeatureArray.get_Element(i)).Extent);

                }
            }

            return pEnvelope;
        }

        #endregion

        #region//将数组里的坐标构造贝塞尔曲线
        /// <summary>
        /// 将数组里的坐标构造贝塞尔曲线
        /// </summary>
        /// <param name="PointArray"></param>
        /// <returns></returns>
        public static IPolyline MadeBezierCurve(ref IArray PointArray)
        {
            IPolyline pPolyline;
            INewBezierCurveFeedback pBezierCurveFeedback = new NewBezierCurveFeedbackClass();

            pBezierCurveFeedback.Start((IPoint)PointArray.get_Element(0));
            for (int i = 1; i < PointArray.Count; i++)
            {	//获取线起点和端点
                pBezierCurveFeedback.AddPoint((IPoint)PointArray.get_Element(i));
            }
            pPolyline = pBezierCurveFeedback.Stop();

            IGeometry tempGeo = (IGeometry)pPolyline;
            AddZMValueForGeometry(ref tempGeo, PointArray);

            return pPolyline;
        }
        #endregion

        #region //为几何形体增加Z值和M值
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

        #region//鼠标在固定方向上的投影点坐标
        /// <summary>
        /// 鼠标在固定方向上的投影点坐标
        /// </summary>
        /// <param name="FormPoint"></param>
        /// <param name="MousePoint"></param>
        /// <param name="FixDirection"></param>
        /// <returns></returns>
        public static IPoint GetTwoPoint_FormPointMousePointFixDirection(IPoint FormPoint, IPoint MousePoint, double FixDirection)
        {
            double tempA;
            double tempDis;
            double dx;
            double dy;
            double dx1;
            double dy1;

            dx = MousePoint.X - FormPoint.X;
            dy = MousePoint.Y - FormPoint.Y;
            tempA = GetAzimuth_P12(FormPoint, MousePoint);
            tempDis = GetDistance_P12(FormPoint, MousePoint);
            dx1 = tempDis * Math.Cos((90 - FixDirection) * Math.PI / 180);
            dy1 = tempDis * Math.Sin((90 - FixDirection) * Math.PI / 180);

            dx = FormPoint.X + dx1;
            dy = FormPoint.Y + dy1;

            IPoint pPoint = new PointClass();
            pPoint.PutCoords(dx, dy);
            return pPoint;

        }
        #endregion

        #region		//计算P1到P2的方位角函数Point1,Point2(以弧度为单位)
        /// <summary>
        /// 计算P1到P2的方位角(以弧度为单位)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetAzimuth_P12(IPoint p1, IPoint p2)
        {
            double Ap12 = 0;
            if (p2.X - p1.X != 0)
            {
                Ap12 = Math.Atan((p2.Y - p1.Y) / (p2.X - p1.X));

                //第一象限
                if (p2.Y - p1.Y > 0 && p2.X - p1.X > 0)
                {
                    Ap12 = Ap12;
                }

                //第二象限
                if (p2.Y - p1.Y > 0 && p2.X - p1.X < 0)
                {
                    Ap12 = Ap12 + Math.PI;
                }

                //第三象限
                if (p2.Y - p1.Y < 0 && p2.X - p1.X < 0)
                {
                    Ap12 = Ap12 + Math.PI;
                }

                //第四象限
                if (p2.Y - p1.Y < 0 && p2.X - p1.X > 0)
                {
                    Ap12 = Ap12 + 2 * Math.PI;
                }

                //与X轴平行
                if (p2.Y - p1.Y == 0)
                {
                    if (p2.X - p1.X > 0)
                    {
                        Ap12 = 0;
                    }
                    else
                    {
                        Ap12 = Math.PI;
                    }
                }
            }

            else //与Y轴平行
            {
                if (p2.Y - p1.Y > 0)
                {
                    Ap12 = Math.PI / 2;
                }
                else
                {
                    Ap12 = 3 * Math.PI / 2;
                }
            }
            return Ap12;
        }
        #endregion

        #region		//计算P1到P2的距离函数Point1,Point2
        /// <summary>
        /// 计算P1到P2的距离
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetDistance_P12(IPoint p1, IPoint p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double s = dx * dx + dy * dy;
            return Math.Sqrt(s);
        }
        #endregion

        #region//已知矩形两相邻点p1、p2以及第三点p0，计算矩形边长
        /// <summary>
        /// 已知矩形两相邻点p1、p2以及第三点p0，计算矩形边长
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p0"></param>
        /// <returns></returns>
        public static double GetRectangleOfSide_Length(IPoint p1, IPoint p2, IPoint p0)
        {
            double dblSideLength;
            double Ap20 = 0;
            double Ap21 = 0;

            //计算p2到p0的方位角
            Ap20 = GetAzimuth_P12(p2, p0);

            //计算p2到p1的方位角
            Ap21 = GetAzimuth_P12(p2, p1);

            //计算边长d
            dblSideLength = Math.Sqrt((p2.X - p0.X) * (p2.X - p0.X) + (p2.Y - p0.Y) * (p2.Y - p0.Y)) * Math.Cos(Math.PI / 2 - (Ap21 - Ap20));
            return dblSideLength;
        }
        #endregion

        #region //已知矩形两相邻点p1,p2的坐标和一条边长d时，求取另两个角点的坐标pm,pn
        /// <summary>
        /// 已知矩形两相邻角点p1,p2的坐标和一条边长d时，求取另两个角点的坐标pm,pn
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static IArray GetPointRectangle2(IPoint p1, IPoint p2, double d)
        {
            IArray pointArray = new ESRI.ArcGIS.esriSystem.ArrayClass();
            IPoint pm = new PointClass();
            IPoint pn = new PointClass();
            double A_P2m = 0;
            double A_P1n = 0;

            //计算P1，P2的方位角Ap12
            double Ap12 = 0;
            Ap12 = GetAzimuth_P12(p1, p2);

            //计算方位角A_P2m
            A_P2m = Ap12 + Math.PI / 2;

            if (A_P2m > 2 * Math.PI)
            {
                A_P2m = A_P2m - 2 * Math.PI;
            }
            if (A_P2m < 0)
            {
                A_P2m = A_P2m + 2 * Math.PI;
            }

            //计算Pm坐标
            pm.X = p2.X + d * Math.Cos(A_P2m);
            pm.Y = p2.Y + d * Math.Sin(A_P2m);

            A_P1n = A_P2m;
            //计算Pn坐标
            pn.X = p1.X + d * Math.Cos(A_P1n);
            pn.Y = p1.Y + d * Math.Sin(A_P1n);

            //将点添加进点数组
            pointArray.Add(pm);
            pointArray.Add(pn);
            return pointArray;
        }
        #endregion

        #region//创建弧Arc转换成Polyline; pFromPoint:弧起点，pCenterPoint：弧圆心，pToPoint：弧终点
        //
        public static IPolyline ArcToPolyline(IPoint pFromPoint, IPoint pCenterPoint, IPoint pToPoint, esriArcOrientation ArcOrientation)
        {
            ICircularArc pCArc = new CircularArcClass();
            pCArc.PutCoords(pCenterPoint, pFromPoint, pToPoint, ArcOrientation);

            ISegmentCollection pSegmentCollection = new PolylineClass();

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            pSegmentCollection.AddSegment((ISegment)pCArc, ref a, ref b);

            return pSegmentCollection as IPolyline;

        }
        #endregion

        #region//获得匹配的目标地理要素，将其存入数组中
        /// <summary>
        /// 获得匹配的目标地理要素，将其存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IArray GetSelectFeatureSaveToArray_2(IMap pMap)
        {
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IArray FeatureArray = new ArrayClass();

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象
            pSelected.Reset();
            pFeature = pSelected.Next();//取得一个对象来判空
            FeatureArray.RemoveAll();
            while (pFeature != null)
            {
                FeatureArray.Add(pFeature);
                pFeature = pSelected.Next();
            }

            return FeatureArray;
        }
        #endregion

        #region //将一个字段值转换为可以显示的字符串
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
                        result = ConvertNull(val);
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
                        result = ConvertNull(val);
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
                    result = ConvertNull(val);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            return result;
        }
        #endregion

        #region //将一个地图中的所有图层（含注记层）加入Hashtable 或 ArrayList， 包括所有不可见图层
        /// <summary>
        /// 将所有的FeatureLayer放入Hashtable
        /// </summary>
        /// <returns></returns>
        public static object GetAllLayersFromMap(IMap pMap, Type type)
        {
            if (type == typeof(Hashtable))
            {
                Hashtable ht = new Hashtable();
                GetAllLayersFromMap(pMap, ht);
                return ht;
            }
            else if (type == typeof(ArrayList))
            {
                ArrayList result = new ArrayList();
                GetAllLayersFromMap(pMap, result);
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
            else if ((obj is IFeatureLayer) && !(obj is ICadLayer))
            {
                if (ht != null)
                {
                    IFeatureLayer pFeaLay = obj as IFeatureLayer;
                    if (ht[pFeaLay.FeatureClass.FeatureClassID] == null)
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

        #region     //弧度转换成角度
        /// <summary>
        /// 弧度转换成角度
        /// </summary>
        /// <param name="Rad"></param>
        /// <returns></returns>
        public static double RadToDeg(double Rad)
        {
            double c;
            double d;
            double e;
            double g;
            double Deg;

            if (Rad < 0)
            {
                Rad = -Rad;
                c = 180 * Rad / Math.PI;
                if (c * 10e11 - (System.Int64)(c * 10e10) * 10 >= 5)
                {
                    c = ((System.Int64)(c * 10e10) + 1) / (10e10);
                }
                else
                {
                    c = ((System.Int64)(c * 10e10)) / (10e10);
                }
                d = (int)c;
                e = (int)((c - d) * 60);
                g = ((c - d) * 60 - e) * 600000000;
                if (g * 10 - (int)g >= 5)
                {
                    g = (int)g + 1;
                }
                else
                {
                    g = (int)g;
                }
                Deg = -(d + e / 100 + g / 100000000000);
            }
            else
            {
                c = 180 * Rad / Math.PI;
                if (c * 10e11 - (System.Int64)(c * 10e10) * 10 >= 5)
                {
                    c = ((System.Int64)(c * 10e10) + 1) / (10e10);
                }
                else
                {
                    c = ((System.Int64)(c * 10e10)) / (10e10);
                }
                d = (int)c;
                e = (int)((c - d) * 60);
                g = ((c - d) * 60 - e) * 600000000;
                if (g * 10 - (int)g >= 5)
                {
                    g = (int)g + 1;
                }
                else
                {
                    g = (int)g;
                }
                Deg = d + e / 100 + g / 100000000000;
            }
            //MessageBox.Show("c=" + c + ",d=" + d + ",e=" + e + ",g=" + g );
            return Deg;
        }
        #endregion

    }
}
