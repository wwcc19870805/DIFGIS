using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DF2DTool.Class
{
    public class clsAutoMerge
    {
        public struct FeatureInfo
        {
            public string strGeoNum;
            public string strDescrip;
            public string strFeaClass;
            public string strMark;
            public int nType;
            //public int nSearchRegion;
        }

        private string FCNAME = "ASSIST500";
        private IFeatureWorkspace m_pFeaWsp = null;
        private IFeatureClass m_pFCAssist;
        public int MergeCount;
        public FeatureInfo[] MergeArray = null;

        #region  构造函数
        public clsAutoMerge(IFeatureWorkspace pFeaWsp)
        {
            m_pFeaWsp = pFeaWsp;
            string m_strConfigFile = "要素合并配置文件.xls";
            string strFilePath = System.Windows.Forms.Application.StartupPath + "\\..\\Support\\" + m_strConfigFile;
            m_pFCAssist = m_pFeaWsp.OpenFeatureClass(this.FCNAME);

            //ArrayList arrayList = this.ReadExcelFile(strFilePath);
            //MergeCount = arrayList.Count;
            MergeArray = new FeatureInfo[MergeCount];
            for (int i = 0; i < MergeCount; i++)
            {
                //string text = arrayList[i].ToString();
                //string[] array2 = text.Split(new char[] { '&' });
                MergeArray[i].strGeoNum = array2[1];
                MergeArray[i].strDescrip = array2[2];
                MergeArray[i].nType = int.Parse(array2[3]);
                MergeArray[i].strFeaClass = array2[4];
                MergeArray[i].strMark = array2[5];
            }

        }
        #endregion

        //#region 读取excel文件
        ///// <summary>
        ///// 读取excel文件
        ///// </summary>
        ///// <param name="strFilePath"></param>
        ///// <returns></returns>
        //private ArrayList ReadExcelFile(string strFilePath)
        //{
        //    string text = "";
        //    object missing = Type.Missing;
        //    ArrayList arrayList = new ArrayList();
        //    Excel.Application application = new ApplicationClass();
        //    _Workbook workbook = application.Workbooks._Open(strFilePath, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
        //    _Worksheet worksheet = (Worksheet)workbook.Sheets.get_Item(1);
        //    for (int i = 2; i <= worksheet.UsedRange.Rows.Count; i++)
        //    {
        //        for (int j = 1; j <= worksheet.UsedRange.Columns.Count; j++)
        //        {
        //            text = text + GetCellText(i, j, worksheet) + "&";
        //        }
        //        arrayList.Add(text);
        //        text = "";
        //    }
        //    try
        //    {
        //        if (application != null)
        //        {
        //            if (workbook != null)
        //            {
        //                if (worksheet != null)
        //                {
        //                    Marshal.ReleaseComObject(worksheet);
        //                }
        //                workbook.Close(false, false, missing);
        //                Marshal.ReleaseComObject(workbook);
        //            }
        //            application.Quit();
        //            Marshal.ReleaseComObject(application);
        //            GC.Collect(0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        Process[] processesByName = Process.GetProcessesByName("Excel");
        //        for (int k = 0; k < processesByName.Length; k++)
        //        {
        //            Process process = processesByName[k];
        //            process.Kill();
        //        }
        //    }
        //    return arrayList;
        //}

        //public static string GetCellText(int row, int col, Excel._Worksheet oSheet)
        //{
        //    string result = "";
        //    bool flag = false;
        //    int num = 1;
        //    Range range = (Range)oSheet.Cells.get_Item(row, col);
        //    if (range.Value2 != null)
        //    {
        //        result = range.Value2.ToString();
        //        flag = true;
        //    }
        //    else
        //    {
        //        if (!(bool)range.MergeCells)
        //        {
        //            result = null;
        //            flag = true;
        //        }
        //    }
        //    if (!flag)
        //    {
        //        for (int i = row - 1; i >= 1; i--)
        //        {
        //            range = (Range)oSheet.Cells.get_Item(i, col);
        //            if (!(bool)range.MergeCells)
        //            {
        //                num = i + 1;
        //                break;
        //            }
        //            try
        //            {
        //                if (range.Value2 != null)
        //                {
        //                    result = range.Value2.ToString();
        //                    flag = true;
        //                    break;
        //                }
        //            }
        //            catch (Exception)
        //            {
        //            }
        //        }
        //        if (!flag)
        //        {
        //            for (int j = col - 1; j >= 1; j--)
        //            {
        //                range = (Range)oSheet.Cells.get_Item(num, j);
        //                if (!(bool)range.MergeCells)
        //                {
        //                    int num2 = j + 1;
        //                    break;
        //                }
        //                try
        //                {
        //                    if (range.Value2 != null)
        //                    {
        //                        result = range.Value2.ToString();
        //                        flag = true;
        //                        break;
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                }
        //            }
        //        }
        //        if (!flag)
        //        {
        //            result = null;
        //        }
        //    }
        //    if (range != null)
        //    {
        //        Marshal.ReleaseComObject(range);
        //        range = null;
        //    }
        //    return result;
        //}
        //#endregion

        #region 合并同类要素
        public void autoMerge(FeatureInfo feaInfo)
        {
            int nType = feaInfo.nType;
            string strGeoNum = feaInfo.strGeoNum;
            string strFeaClass = feaInfo.strFeaClass;
            string strMark = feaInfo.strMark + ";" + strGeoNum;
            string[] array;
            if (strMark != "")
            {
                array = strMark.Split(new char[] { ';' });
            }
            else
            {
                array = null;
            }

            ArrayList arrayList = new ArrayList();
            IQueryFilter queryFilter = new QueryFilterClass();
            IFeatureClass featureClass = this.m_pFeaWsp.OpenFeatureClass(strFeaClass);
            IFeatureCursor featureCursor;
            IFeature feature, feature2;
            esriSpatialRelEnum spatialRel;
            IPolyline polyline;
            IPolygon polygon;
            IPointCollection pPtC;
            ESRI.ArcGIS.Geometry.IPoint pPt0, pPtn;

            queryFilter.WhereClause = "SMSSYMBOL ='" + strGeoNum + "'";
            if (nType == 1 || nType == 2 || nType == 5)
            {
                featureCursor = this.m_pFCAssist.Search(queryFilter, true);
            }
            else
            {
                featureCursor = featureClass.Update(queryFilter, true);
            }
            if (featureCursor == null)
            {
                return;
            }
            feature = featureCursor.NextFeature();

            switch (nType)
            {
                case 1:
                    {
                        spatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        while (feature != null)
                        {
                            polyline = (IPolyline)feature.Shape;
                            pPtC = polyline as IPointCollection;
                            pPt0 = pPtC.get_Point(0);
                            pPtn = pPtC.get_Point(pPtC.PointCount - 1);
                            if (polyline.IsClosed || (pPtC.PointCount > 2 && Math.Abs(pPt0.X - pPtn.X) < 0.05 && Math.Abs(pPt0.Y - pPtn.Y) < 0.05))
                            {
                                polygon = this.polylinetoPolygon(polyline);
                                polygon = (IPolygon)(polygon as ITopologicalOperator).Buffer(0.05);
                                for (int i = 0; i < array.Length; i++)
                                {
                                    feature2 = this.FeatureMerge(featureClass, polygon, spatialRel, array[i]);
                                }
                            }
                            feature = featureCursor.NextFeature();
                        }
                        break;
                    }
                case 2:
                    {
                        spatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        while (feature != null)
                        {
                            polyline = (IPolyline)feature.Shape;
                            for (int i = 0; i < array.Length; i++)
                            {
                                feature2 = this.FeatureMerge(featureClass, polyline, spatialRel, array[i]);
                            }
                            feature = featureCursor.NextFeature();
                        }
                        break;
                    }
                case 3:
                    {
                        spatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        while (feature != null)
                        {
                            polyline = (IPolyline)feature.Shape;
                            pPtC = polyline as IPointCollection;
                            pPt0 = pPtC.get_Point(0);
                            pPtn = pPtC.get_Point(pPtC.PointCount - 1);
                            if (polyline.IsClosed || (pPtC.PointCount > 2 && Math.Abs(pPt0.X - pPtn.X) < 0.05 && Math.Abs(pPt0.Y - pPtn.Y) < 0.05))
                            {
                                polygon = this.polylinetoPolygon(polyline);
                                for (int i = 0; i < array.Length; i++)
                                {
                                    feature2 = this.FeatureMerge(featureClass, polygon, spatialRel, array[i]);
                                    if (feature2 != null)
                                    {
                                        feature.Shape = CommonFunction.UnionGeometry(feature.Shape, feature2.Shape);
                                        feature.Store();
                                        feature2.Delete();
                                    }
                                }
                            }
                            feature = featureCursor.NextFeature();
                        }
                        break;
                    }
                case 4:
                    {
                        spatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        while (feature != null)
                        {
                            polyline = (IPolyline)feature.Shape;
                            pPtC = polyline as IPointCollection;
                            pPt0 = pPtC.get_Point(0);
                            pPtn = pPtC.get_Point(pPtC.PointCount - 1);
                            if (polyline.IsClosed || (pPtC.PointCount > 2 && Math.Abs(pPt0.X - pPtn.X) < 0.05 && Math.Abs(pPt0.Y - pPtn.Y) < 0.05))
                            {
                                polygon = this.polylinetoPolygon(polyline);
                                for (int i = 0; i < array.Length; i++)
                                {
                                    feature2 = this.FeatureMerge(featureClass, polygon, spatialRel, array[i]);
                                }
                            }
                            feature = featureCursor.NextFeature();
                        }
                        break;
                    }
                case 5:
                    {
                        spatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        while (feature != null)
                        {
                            polyline = (IPolyline)feature.Shape;
                            polygon = (IPolygon)(polyline as ITopologicalOperator).Buffer(1.01);
                            for (int i = 0; i < array.Length; i++)
                            {
                                feature2 = this.FeatureMerge(featureClass, polygon, spatialRel, array[i]);
                            }
                            feature = featureCursor.NextFeature();
                        }
                        break;
                    }
                case 6:
                    {
                        spatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        while (feature != null)
                        {
                            if ((feature.Shape as IGeometryCollection).GeometryCount == 1)
                            {
                                polyline = (IPolyline)feature.Shape;
                                polygon = (IPolygon)(polyline as ITopologicalOperator).Buffer(0.05);
                                for (int i = 0; i < array.Length; i++)
                                {
                                    feature2 = this.FeatureMerge(featureClass, polygon, spatialRel, array[i]);
                                }
                            }
                            feature = featureCursor.NextFeature();
                        }
                        break;
                    }
            }
        }
        #endregion

        #region 合并同类要素
        private IFeature FeatureMerge(IFeatureClass pSearchFC, IGeometry pEnv, esriSpatialRelEnum spatialRel, string strGeoObjNum)
        {
            IFeature feature = null;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = pEnv;
            spatialFilter.SpatialRel = spatialRel;
            spatialFilter.WhereClause = "SMSSYMBOL = '" + strGeoObjNum + "'";
            IFeatureCursor featureCursor = pSearchFC.Update(spatialFilter, false);
            if (featureCursor != null)
            {
                IFeature feature2 = featureCursor.NextFeature();
                feature = feature2;
                while (feature2 != null)
                {
                    if (feature2.OID != feature.OID)
                    {
                        feature.Shape = CommonFunction.UnionGeometry(feature.Shape, feature2.Shape);
                        feature2.Delete();
                        feature.Store();
                    }
                    feature2 = featureCursor.NextFeature();
                }
                Marshal.ReleaseComObject(featureCursor);
            }
            return feature;
        }

        private void FeatureMerge(ArrayList arrFeature)
        {
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < arrFeature.Count; i++)
            {
                IFeature feature = (IFeature)arrFeature[i];
                if (!arrayList.Contains(feature))
                {
                    IRelationalOperator relationalOperator = (IRelationalOperator)feature.Shape;
                    IGeometry geometry = feature.Shape;
                    for (int j = 0; j < arrFeature.Count; j++)
                    {
                        IFeature feature2 = (IFeature)arrFeature[j];
                        if (feature2.OID != feature.OID && !arrayList.Contains(feature2))
                        {
                            IGeometry shape = feature2.Shape;
                            if (!relationalOperator.Disjoint(shape) || relationalOperator.Touches(shape))
                            {
                                geometry = CommonFunction.UnionGeometry(geometry, shape);
                                arrayList.Add(feature2);
                            }
                        }
                    }
                    feature.Shape = geometry;
                    feature.Store();
                }
            }
            for (int k = 0; k < arrayList.Count; k++)
            {
                (arrayList[k] as IFeature).Delete();
            }
        }

        private IPolygon polylinetoPolygon(IPolyline pPolyline)
        {
            IPointCollection pPtC;
            ESRI.ArcGIS.Geometry.IPoint pPt0, pPtn;
            ESRI.ArcGIS.Geometry.ILine pSeg = new LineClass();

            IPolygon polygon = new PolygonClass();
            ISegmentCollection segmentCollection = (ISegmentCollection)pPolyline;
            int segmentCount = segmentCollection.SegmentCount;
            ISegmentCollection segmentCollection2 = (ISegmentCollection)polygon;
            object value = Missing.Value;
            object value2 = Missing.Value;
            for (int i = 0; i < segmentCount; i++)
            {
                segmentCollection2.AddSegment(segmentCollection.get_Segment(i), ref value, ref value2);
            }
            if (!pPolyline.IsClosed)
            {
                pPtC = pPolyline as IPointCollection;
                pPt0 = pPtC.get_Point(0);
                polygon.ToPoint = pPt0;
                //pPtn = pPtC.get_Point(pPtC.PointCount - 1);
                //pSeg.PutCoords(pPtn, pPt0);
                //segmentCollection2.AddSegment((ISegment)pSeg, ref value, ref value2);
            }

            polygon.SimplifyPreserveFromTo();
            return polygon;
        }
        #endregion
    }
}
