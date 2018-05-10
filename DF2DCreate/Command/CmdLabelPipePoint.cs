using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using System.Collections;
using DFWinForms.Service;
using ESRI.ArcGIS.Geometry;
using DF2DCreate.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Drawing;
using DFCommon.Class;



/// <summary>
/// 管点标注
/// </summary>
namespace DF2DCreate.Command
{


    class CmdLabelPipePoint : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_pMap;
        private ArrayList m_arrPntLayer;
        private IActiveView m_pActiveView;

        public override void Run(object sender, EventArgs e)
        {
            Map2DCommandManager.Push(this);
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;
            m_pMap = m_pMapControl.Map;
            m_pActiveView = m_pMapControl.ActiveView;
            if (mapView == null)
            {
                return;
            }
            else
            {
                m_arrPntLayer = new ArrayList();
                GetPointLayer();
            }

        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            IPolygon pPoly;
            IEnvelope env = m_pMapControl.TrackRectangle() as ESRI.ArcGIS.Geometry.IEnvelope;
            pPoly = new PolygonClass();
            ESRI.ArcGIS.Geometry.IPointCollection pc = pPoly as ESRI.ArcGIS.Geometry.IPointCollection;
            object missing = Type.Missing;
            pc.AddPoint(env.UpperLeft, ref missing, ref missing);
            pc.AddPoint(env.UpperRight, ref missing, ref missing);
            pc.AddPoint(env.LowerRight, ref missing, ref missing);
            pc.AddPoint(env.LowerLeft, ref missing, ref missing);
            IFeatureLayer pFeaLayer;
            //             IRubberBand pRubberPoly = new RubberPolygonClass();
            //             pPoly = (IPolygon)pRubberPoly.TrackNew(m_Display, null);

            //遍历图层，查找选择框内的点要素，并标注
            for (int i = 0; i < m_arrPntLayer.Count; i++)
            {
                pFeaLayer = m_arrPntLayer[i] as IFeatureLayer;
                ArrayList features = PublicFunction.SearchFeature(pFeaLayer.FeatureClass, pPoly, esriSpatialRelEnum.esriSpatialRelContains, false);
                
                for (int j = 0; j < features.Count; j++)
                {
                    LabelPointFeature(features[j] as IFeature);
                }
            }
            this.m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        #region 判断可进行操作的图层
        /// <summary>
        /// 判断可进行操作的要素个数
        /// </summary>
        private void GetPointLayer()
        {
            ILayer pLayer;

            if (m_pMap.LayerCount == 0)
            {
                return;
            }
            for (int i = 0; i < m_pMap.LayerCount; i++)
            {
                pLayer = m_pMap.get_Layer(i);

                LoopLayerForGetSelectCount(pLayer);

            }
        }
        /// <summary>
        /// 递归调用
        /// </summary>
        /// <param name="pLyr"></param> 
        private void LoopLayerForGetSelectCount(ILayer pLyr)
        {
            ICompositeLayer pComp;
            IFeatureLayer pFeatureLayer;
            IFeatureClass pFeatureClass;

            if (pLyr is IGroupLayer)//如果是图层组
            {
                pComp = (ICompositeLayer)pLyr;
                for (int i = 0; i < pComp.Count; i++)
                {
                    pLyr = pComp.get_Layer(i);
                    LoopLayerForGetSelectCount(pLyr);
                }
            }
            else
            {
                if ((pLyr is IGeoFeatureLayer) && (pLyr.Visible))//如果是地理要素图层
                {
                    pFeatureLayer = pLyr as IFeatureLayer;
                    IFeatureSelection pFeatSel = pFeatureLayer as IFeatureSelection;
                    pFeatureClass = pFeatureLayer.FeatureClass;
                    if (pFeatureClass != null && pFeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                    {
                        m_arrPntLayer.Add(pFeatureLayer);
                    }
                }
            }
        }
        #endregion

        private void LabelPointFeature(IFeature pfea)
        {
            IPoint pPoint = (IPoint)pfea.ShapeCopy;
            string strText = "X:" + pPoint.Y.ToString("F2") + "\n" + "Y:" + pPoint.X.ToString("F2");
            AddCallout(pPoint, strText);
        }

        /// <summary>
        /// 添加标注信息
        /// </summary>
        /// <param name="pPoint"></param>
        public void AddCallout(IPoint pPoint, string strText)
        {
            IActiveView pActiveView;
            IGraphicsContainer pGraphicsContainer;
            IPoint pPntText = new PointClass();
            pPntText.PutCoords(pPoint.X + 3, pPoint.Y + 3);
            pActiveView = m_pMapControl.ActiveView;
            pGraphicsContainer = pActiveView.GraphicsContainer;

            IElement pElement;
            IFormattedTextSymbol pTextSymbol = new TextSymbolClass();
            ITextElement pTextElement = new TextElementClass();
            pElement = (IElement)pTextElement;
            pTextElement.Text = strText;
            pElement.Geometry = pPntText;

            IRgbColor pRgbClr = new RgbColorClass();
            pRgbClr.Red = 255;
            pRgbClr.Blue = 255;
            pRgbClr.Green = 255;

            ISimpleFillSymbol pSmplFill = new SimpleFillSymbolClass();
            pSmplFill.Color = pRgbClr;
            pSmplFill.Style = esriSimpleFillStyle.esriSFSHollow;

            IBalloonCallout pBllnCallout = new BalloonCalloutClass();
            pBllnCallout.Style = esriBalloonCalloutStyle.esriBCSRoundedRectangle;
            pBllnCallout.Symbol = pSmplFill;
            pBllnCallout.LeaderTolerance = 5;
            pBllnCallout.AnchorPoint = pPoint;

            pRgbClr.Red = 255;
            pRgbClr.Blue = 0;
            pRgbClr.Green = 0;

            pTextSymbol.Background = (ITextBackground)pBllnCallout;
            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            pTextSymbol.Color = pColor;
            pTextSymbol.Size = SystemInfo.Instance.TextSize;
            pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            pTextElement.Symbol = pTextSymbol;
            pGraphicsContainer.AddElement(pElement, 1);

            //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public override void RestoreEnv()
        {
            try
            {
                IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
                if (mapView == null) return;
                if (app == null || app.Current2DMapControl == null || app.Workbench == null) return;
                m_pActiveView.GraphicsContainer.DeleteAllElements();
                app.Current2DMapControl.ActiveView.Refresh();
                mapView.UnBind(this);
                Map2DCommandManager.Pop();
            }
            catch (System.Exception ex)
            {

            }

        }

    }
}
