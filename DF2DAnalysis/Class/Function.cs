using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
namespace DF2DAnalysis.Class
{
    public class Function
    {
        #region 根据所划线与管线的交点推算地面模拟线的情况，形成地面模拟线的管点集合
        /// <summary>
        /// pPointCollection  所划线与管线的交点
        /// </summary>
        /// <returns></returns>
        public IPointCollection get_InsertPointColDepth(IPointCollection pPointCollection)
        {
            IPointCollection pResultPointCollection = new PolylineClass();//地表插入点集合
            IPointCollection pTempPointCollection = new PolylineClass();//地表插入点集合

            ArrayList pArrayList = new ArrayList();
            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                pArrayList.Add(pPointCollection.get_Point(i).X);
            }
            pArrayList.Sort();

            object o = Type.Missing;

            for (int i = 0; i < pArrayList.Count; i++)
            {
                for (int j = 0; j < pPointCollection.PointCount; j++)
                {
                    if (pPointCollection.get_Point(j).X == Convert.ToDouble(pArrayList[i]))
                    {
                        //Console.WriteLine(pPointCollection.get_Point(j).X.ToString());

                        IPoint pNewPoint = new PointClass();
                        pNewPoint.X = pPointCollection.get_Point(j).X;
                        pNewPoint.Y = pPointCollection.get_Point(j).M + pPointCollection.get_Point(j).Z;
                        pResultPointCollection.AddPoint(pNewPoint, ref o, ref o);

                        break;
                    }
                }
            }
            m_XMin = pResultPointCollection.get_Point(0).X;
            return pResultPointCollection;
        }

        #endregion

        #region 内插值法求点的高程
        /// <summary>
        /// 通过地表内插计算点的高程值
        /// </summary>
        /// <param name="pPnt"></param>
        /// <returns>点的高程值</returns>
        private double Calculate_Z(IPoint pPnt)
        {
            double pZ = 0;
            //================================================
            double dRad = double.Parse("15".Trim()) / 2.0; //搜索半径一半
            double dBufferRad = 0.0;
            ITopologicalOperator pTopo = pPnt as ITopologicalOperator;
            IGeometry pBuffer = pTopo.Buffer(dRad);
            IPointCollection pPC = pntColl(pBuffer);  //this.Get_PntColl();
            if (pPC == null)
            {
                return -1;
            }
            //================================================
            int pTag = 1;
            //如果点数小于2,则增大搜索半径，重新搜索
            while (pPC.PointCount < 2)
            {
                if (pTag > 4)//若重新搜索4次还没有点，则退出
                {
                    break;
                }
                pTag++;
                dBufferRad = dRad * pTag;
                pBuffer = pTopo.Buffer(dBufferRad);
                pPC = pntColl(pBuffer);
            }
            //================================================			
            if (pPC.PointCount > 1)
            {
                double pTolZ = 0;
                double pLength = 0;
                double iP = 0;
                double pToliP = 0;//权系数
                //                MessageBox.Show("搜索点数为"+pPC.PointCount.ToString());

                for (int i = 0; i < pPC.PointCount; i++)
                {
                    IPoint pPt = pPC.get_Point(i);

                    if (pPt.Z >= 0 || pPt.Z < 0)
                    {
                        pLength = Get_Len_2piont(pPnt, pPt);
                        if (pLength > 0)
                        {
                            iP = 1 / Math.Pow(pLength, 2);
                            pToliP = pToliP + iP;
                            pTolZ = pTolZ + iP * pPt.Z;

                        }
                    }//if (pPt.Z != double.NaN)

                }

                if (pToliP != 0)
                {
                    pZ = pTolZ / pToliP;
                }//pToliP = 0使用初始化值0作为pZ结果
                else//大于两个点均与查询点重合
                {
                    pZ = 0;
                }
            }
            else//点数小于两个
            {
                pZ = 0;
                m_tempPoitArray.Add(pPnt);
               
            }
            return pZ;
        }
        #endregion

        #region 获得缓冲区域内的点
        /// <summary>
        /// 获得缓冲区域内的点集合
        /// </summary>
        /// <param name="pBuff">缓冲区域</param>
        /// <returns>缓冲区域内的点集合(IPointCollection)</returns>
        private IPointCollection pntColl(IGeometry pBuff)
        {

            //=============================================================
            string pFld = m_Parameters[1].ToString().Trim();
            object o = Type.Missing;

            IPointCollection pPointColl = new MultipointClass();
            ISpatialFilter pFilter = new SpatialFilterClass();
            //=============================================================
            ILayer pLyr = m_Parameters[0] as ILayer;
            IFeatureLayer pFLyr = pLyr as IFeatureLayer;
            if (pFLyr == null)
            {
                return null;
            }

            IFeatureClass pFCls = pFLyr.FeatureClass;
            //=============================================================           
            pFilter.Geometry = pBuff;
            pFilter.GeometryField = pFCls.ShapeFieldName;
            pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

            IFeatureCursor pOutFeatureCursor = pFCls.Search(pFilter, false);

            IFeature pFeature = pOutFeatureCursor.NextFeature();

            while (pFeature != null)
            {
                IGeometry pGeometry = pFeature.Shape;
                if (pGeometry != null)
                {
                    if (pFld == "Z")
                    {
                        #region 选中'Z'字段
                        if (pGeometry is IPoint)
                        {
                            IPoint pPnt = pGeometry as IPoint;
                            if (pPnt.Z > -999)
                            {
                                pPointColl.AddPoint(pPnt, ref o, ref o);
                            }
                        }
                        else
                        {
                            IPointCollection pPnts = pGeometry as IPointCollection;
                            for (int i = 0; i < pPnts.PointCount; i++)
                            {
                                IPoint pPnt = pPnts.get_Point(i);
                                if (pPnt.Z <= -999)
                                {
                                    //MessageBox.Show("无法读取该点数据！","提示");
                                    continue;
                                }
                                else
                                {
                                    pPointColl.AddPoint(pPnt, ref o, ref o);
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 选中其他字段

                        if (pGeometry is IPoint)
                        {
                            IPoint pPnt = pGeometry as IPoint;
                            pPnt.Z = Convert.ToDouble(pFeature.get_Value(pFCls.Fields.FindField(pFld)).ToString().Trim());
                            if (pPnt.Z != -999)
                            {
                                pPointColl.AddPoint(pPnt, ref o, ref o);
                            }
                        }
                        else
                        {
                            IPointCollection pPnts = pGeometry as IPointCollection;
                            for (int i = 0; i < pPnts.PointCount; i++)
                            {
                                IPoint pPnt = pPnts.get_Point(i);
                                pPnt.Z = Convert.ToDouble(pFeature.get_Value(pFCls.Fields.FindField(pFld)).ToString().Trim());
                                if (pPnt.Z != -999)
                                {
                                    pPointColl.AddPoint(pPnt, ref o, ref o);
                                }
                            }
                        }
                        #endregion
                    }

                }
                pFeature = pOutFeatureCursor.NextFeature();
            }
            //0919
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pOutFeatureCursor);
            }
            catch (Exception ex)
            {
               

            }
            //0919

            return pPointColl;
        }
        #endregion      

        #region 内插值法求点的“地面高”
        /// <summary>
        /// 内插值法求点的“地面高”
        /// </summary>
        /// <param name="pPnt"></param>
        /// <returns>点的高程值</returns>
        private double Calculate_Z2(IPoint pPnt)
        {
            double pZ = 0;
            //================================================
            double dRad = double.Parse("15".Trim()) / 2.0; //搜索半径一半
            double dBufferRad = 0.0;
            ITopologicalOperator pTopo = pPnt as ITopologicalOperator;
            IGeometry pBuffer = pTopo.Buffer(dRad);

            IPointCollection pPC = new MultipointClass();
            pPC.AddPointCollection(PointCollectionFromEdit(pPnt, dRad));
            pPC.AddPointCollection(pntColl(pBuffer));
            //================================================
            int pTag = 1;
            //如果点数小于2,则增大搜索半径，重新搜索
            while (pPC.PointCount < 2)
            {
                if (pTag > 4)//若重新搜索4次还没有点，则退出
                {
                    break;
                }
                pTag++;
                dBufferRad = dRad * pTag;
                pBuffer = pTopo.Buffer(dBufferRad);

                pPC.RemovePoints(0, pPC.PointCount);
                pPC.AddPointCollection(PointCollectionFromEdit(pPnt, dBufferRad));
                pPC.AddPointCollection(pntColl(pBuffer));
            }
            //================================================			
            if (pPC.PointCount > 1)
            {
                double pTolZ = 0;
                double pLength = 0;
                double iP = 0;
                double pToliP = 0;//权系数

                for (int i = 0; i < pPC.PointCount; i++)
                {
                    IPoint pPt = pPC.get_Point(i);

                    if (pPt.Z >= 0 || pPt.Z < 0)
                    {
                        pLength = Get_Len_2piont(pPnt, pPt);
                        if (pLength > 0)
                        {
                            iP = 1 / Math.Pow(pLength, 2);
                            pToliP = pToliP + iP;
                            pTolZ = pTolZ + iP * pPt.Z;

                        }
                    }//if (pPt.Z != double.NaN)

                }

                if (pToliP != 0)
                {
                    pZ = pTolZ / pToliP;
                }//pToliP = 0使用初始化值0作为pZ结果
                else//大于两个点均与查询点重合
                {
                    pZ = 0;
                }
            }
            else//点数小于两个
            {
                //if (frmSetValue.UseConst)
                //{
                //    pZ = frmSetValue.ConstHight;
                //}
                //else
                //{
                //    frmSetValue pFrmSetValue = new frmSetValue(pPnt.X, pPnt.Y);
                //    if (pFrmSetValue.ShowDialog() == DialogResult.OK)
                //    {
                //        pZ = pFrmSetValue.ReturnValue;
                //        bolEdit = true;
                //    }
                //}
            }
            //================================================

            return pZ;
        }
        #endregion
 

        #region 计算平面两点间距离(X Y)
        /// <summary>
        /// 计算平面两点间距离
        /// </summary>
        /// <param name="Pnt1">点1</param>
        /// <param name="Pnt2">点2</param>
        /// <returns>平面两点间距离</returns>
        private double Get_Len_2piont(IPoint Pnt1, IPoint Pnt2)
        {
            double Result = 0;

            Result = Math.Sqrt(Math.Pow(Pnt1.X - Pnt2.X, 2) + Math.Pow(Pnt1.Y - Pnt2.Y, 2));

            return Result;
        }
        #endregion

        #region 计算空间两点间距离(X Y Z)
        /// <summary>
        /// 计算空间两点间距离
        /// </summary>
        /// <param name="Pnt1">点1</param>
        /// <param name="Pnt2">点2</param>
        /// <returns>空间两点间距离</returns>
        private double Get_SpactialLen_2piont(IPoint Pnt1, IPoint Pnt2)
        {
            double Result = 0;

            Result = Math.Sqrt(Math.Pow(Pnt1.X - Pnt2.X, 2)
                + Math.Pow(Pnt1.Y - Pnt2.Y, 2) + Math.Pow(Pnt1.Z - Pnt2.Z, 2));

            return Result;
        }
        #endregion

    }
}
