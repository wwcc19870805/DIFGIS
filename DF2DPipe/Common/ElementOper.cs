using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;

namespace DF2DPipe.Common
{
    public class ElementOper
    {
        #region 添加一个Element
        /// <summary>
        /// 根据IGeometry 生成一个Element，显示在地图上
        /// </summary>
        /// <param name="pGC3D">element的容器</param>
        /// <param name="pGeom">element的几何信息</param>
        /// <param name="pSym">符号</param>
        /// <param name="sElementName">element的名称</param>
        public static void AddGraphics(IGraphicsContainer pGC, IGeometry pGeom, ISymbol pSym, string sElementName)
        {
            if (pGeom.IsEmpty) return;

            IElementProperties pElemProps;
            IElement pElement = null;
            IFillShapeElement pFillElement;
            //Point Type
            if (pGeom.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                pElement = new MarkerElementClass();
                IMarkerElement pPointElement;
                pPointElement = (IMarkerElement)pElement;
                pPointElement.Symbol = (IMarkerSymbol)pSym;
                pElement.Geometry = pGeom;
            }
            //Polyline Type
            else if (pGeom.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                pElement = new LineElementClass();
                ILineElement pLineElement;
                pLineElement = (ILineElement)pElement;
                pLineElement.Symbol = (ILineSymbol)pSym;
                pElement.Geometry = pGeom;
            }
            //Polygon Type
            else if (pGeom.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                pElement = new PolygonElementClass();
                pFillElement = (IFillShapeElement)pElement;
                pFillElement.Symbol = (IFillSymbol)pSym;
                pElement.Geometry = pGeom;
            }
            //MultiPatch Type
            else if (pGeom.GeometryType == esriGeometryType.esriGeometryMultiPatch)
            {
                pElement = new MultiPatchElementClass();
                pFillElement = (IFillShapeElement)pElement;
                pFillElement.Symbol = (IFillSymbol)pSym;
                pElement.Geometry = pGeom;
            }
            if (pElement == null)
            {
                //Console.WriteLine("添加element时Element为null！");
                return;
            }
            if (sElementName != "")
            {
                pElemProps = (IElementProperties)pElement;
                pElemProps.Name = sElementName;
            }

            pGC.AddElement(pElement, 1);
        }
        #endregion

        #region 删除名称包含在names中的所有element
        /// <summary>
        /// 删除名称包含在names中的所有element
        /// </summary>
        public static void DeleteElement(IGraphicsContainer pGC, string[] names)
        {
            string name;
            for (int i = 0; i < names.Length; i++)
            {
                name = names[i];
                IElement pElement = null;
                pElement = GetElementByName(pGC, name);
                while (pElement != null)
                {
                    pGC.DeleteElement(pElement);
                    pElement = GetElementByName(pGC, name);
                }
            }
        }
        #endregion

        #region 删除所有名称为name 的 element
        /// <summary>
        /// 删除所有名称为name 的 element
        /// </summary>
        public static void DeleteElement(IGraphicsContainer pGC, string name)
        {
            IElement pElement = null;
            pElement = GetElementByName(pGC, name);
            while (pElement != null)
            {
                pGC.DeleteElement(pElement);
                pElement = GetElementByName(pGC, name);
            }
        }
        #endregion

        #region 找指定名称的Element
        /// <summary>
        /// 找指定名称的Element
        /// </summary>
        /// <param name="pGC"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IElement GetElementByName(IGraphicsContainer pGC, string name)
        {
            IElement result = null;
            IElement pElement = null;
            IElementProperties pElemProps = null;
            pGC.Reset();
            pElement = pGC.Next();
            while (pElement != null)
            {
                pElemProps = (IElementProperties)pElement;
                if (pElemProps.Name == name)
                {
                    result = pElement;
                }
                pElement = pGC.Next();
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

        #region 3D 根据点绘制符号
        /// <summary>
        /// 根据传入的点集合绘制符号
        /// </summary>
        /// <param name="points">传入的点集合</param>
        /// <param name="symbolStyle">符号样式</param>
        /// <param name="symbolColor">符号颜色</param>
        /// <param name="symbolSize">符号大小</param>
        public static void AddGraphics(IScene pScene, IPoint point, string name)
        {
            try
            {
                esriSimple3DMarkerStyle symbolStyle = esriSimple3DMarkerStyle.esriS3DMSSphere;
                IRgbColor symbolColor = GetRGBColor(255, 0, 0);
                int symbolSize = 1;
                ISimpleMarker3DSymbol pSimpleMarker3DSymbol;
                pSimpleMarker3DSymbol = new SimpleMarker3DSymbolClass();
                pSimpleMarker3DSymbol.Style = symbolStyle;
                pSimpleMarker3DSymbol.ResolutionQuality = 0.1;

                IMarker3DPlacement pMarker3DPlacement;
                pMarker3DPlacement = (IMarker3DPlacement)pSimpleMarker3DSymbol;
                pMarker3DPlacement.Color = symbolColor;
                pMarker3DPlacement.Width = 1;
                pMarker3DPlacement.Size = symbolSize;
                pMarker3DPlacement.Depth = 1;
                pMarker3DPlacement.SetRotationAngles(0, 0, 0);

                IGraphicsContainer3D pGC3D;
                pGC3D = pScene.BasicGraphicsLayer as IGraphicsContainer3D;

                MarkerElementClass pElement = new MarkerElementClass();

                IMarkerElement pPointElement;
                pPointElement = (IMarkerElement)pElement;

                IMarkerSymbol pMarkerSymbol;
                pMarkerSymbol = (IMarkerSymbol)pSimpleMarker3DSymbol;
                pMarkerSymbol.Color = symbolColor;

                pPointElement.Symbol = pMarkerSymbol;
                (pPointElement as IElementProperties).Name = name;

                pElement.Geometry = point;

                pGC3D.AddElement(pElement);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }
        #endregion

        #region 删除名称包含在names中的所有element
        /// <summary>
        /// 删除名称包含在names中的所有element
        /// </summary>
        public static void DeleteElement(IGraphicsContainer3D pGC, string[] names)
        {
            string name;
            for (int i = 0; i < names.Length; i++)
            {
                name = names[i];
                IElement pElement = null;
                pElement = GetElementByName(pGC, name);
                while (pElement != null)
                {
                    pGC.DeleteElement(pElement);
                    pElement = GetElementByName(pGC, name);
                }
            }
        }
        #endregion

        #region 删除所有名称为name 的 element
        /// <summary>
        /// 删除所有名称为name 的 element
        /// </summary>
        public static void DeleteElement(IGraphicsContainer3D pGC, string name)
        {
            IElement pElement = null;
            pElement = GetElementByName(pGC, name);
            while (pElement != null)
            {
                pGC.DeleteElement(pElement);
                pElement = GetElementByName(pGC, name);
            }
        }
        #endregion

        #region 找指定名称的Element
        /// <summary>
        /// 找指定名称的Element
        /// </summary>
        /// <param name="pGC"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IElement GetElementByName(IGraphicsContainer3D pGC, string name)
        {
            IElement result = null;
            IElement pElement = null;
            IElementProperties pElemProps = null;
            pGC.Reset();
            pElement = pGC.Next();
            while (pElement != null)
            {
                pElemProps = (IElementProperties)pElement;
                if (pElemProps.Name == name)
                {
                    result = pElement;
                }
                pElement = pGC.Next();
            }
            return result;
        }

        #endregion
    }
}
