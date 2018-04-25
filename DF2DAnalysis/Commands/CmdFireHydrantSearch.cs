using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using DFWinForms.Service;
using ESRI.ArcGIS.Geometry;
using DF2DPipe.Class;
using System.Drawing;
using DFCommon.Class;
using ESRI.ArcGIS.esriSystem;
using System.Collections;
using ESRI.ArcGIS.Geodatabase;
using DF2DData.Class;
using DFDataConfig.Class;
using System.Windows.Forms;

namespace DF2DAnalysis.Commands
{
    class CmdFireHydrantSearch : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_FocusMap;
        private IActiveView m_pActiveView;
        private IRubberBand rubberBand = null;
        private IGraphicsContainer pGraphicsContainer;
        private double m_radius;

        private IPoint downPoint; // 鼠标按下的坐标
        private IPoint movePoint; // 鼠标移动的坐标
        private IPoint pFromPoint;//圆弧的起点
        public static IPoint m_pPoint;
        public static IPoint m_pAnchorPoint;

        private IDisplayFeedback m_pFeedback;
        private INewCircleFeedback m_pCircleFeed;
        private INewLineFeedback m_pLineFeed;
        private IGeometry pGeom = null;
        private IPolyline pPolyline;
        private IPolygon pPolygon;
        private ISegmentCollection pSegmentCollection ;
        private IArray m_pRecordPointArray = new ArrayClass();
        private ArrayList m_pArrPipes;
        private ArrayList m_pArrPointLayers;

        private IFeatureClass pFeatureClass;
        private IFeatureCursor pFeaCur;
        private IFeature pFeature;
        private IFeature pFea;

        private bool m_bInUse;
        DFDataConfig.Class.FieldInfo fi;
        public override void Run(object sender, EventArgs e)
        {
            List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
            if (list == null) return;
            foreach (DF2DFeatureClass dffc in list)
            {
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc.AliasName == "给水_点")
                {
                    FacilityClass fac = dffc.GetFacilityClass();
                    if (fac == null) continue;
                    fi = fac.GetFieldInfoBySystemName("Additional");
                    break;
                }
            }
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;
            m_FocusMap = m_pMapControl.ActiveView.FocusMap;
            m_pActiveView = (IActiveView)this.m_FocusMap;

        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            IGeometry Geom = null;

              if(!m_bInUse)
              {
                downPoint = m_pAnchorPoint;
                m_pRecordPointArray.Add(downPoint);
                m_pCircleFeed = new NewCircleFeedbackClass();
                m_bInUse = true;
                m_pCircleFeed.Display = m_pActiveView.ScreenDisplay;
                m_pLineFeed = new NewLineFeedbackClass();
                m_pLineFeed.Start(downPoint);
                m_pCircleFeed.Start(downPoint);

              }
              else //如果命令正在使用
              {
                  if (pGraphicsContainer != null)
                  {
                      pGraphicsContainer.DeleteAllElements();
                  }                      
                  movePoint=m_pAnchorPoint;

                  //画半径线
                  m_pRecordPointArray.Add(movePoint);
                  IPolyline pPolyline;
                  pPolyline = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pRecordPointArray);
                  Geom = (IGeometry)pPolyline;
                  CommonFunction.AddElement(m_pMapControl,Geom);
                  m_radius = CommonFunction.GetDistance_P12(downPoint, movePoint);//半径
                  //m_radius = Math.Round( m_radius,1,MidpointRounding.AwayFromZero);//保留一位小数
                  //缓冲半径文本
                  ITextElement iTextElement = new TextElementClass();
                  iTextElement.Text = m_radius.ToString(".##") + "米";
                  ITextSymbol sce = new TextSymbolClass();
                  sce.Size = 15/*SystemInfo.Instance.TextSize*/;
                  Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
                  IColor pColor = new RgbColorClass();
                  pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
                  sce.Color = pColor;
                  iTextElement.Symbol = sce;
                  IElement iElement = (IElement)iTextElement;
                  iElement.Geometry = Geom;
                  this.m_pMapControl.ActiveView.GraphicsContainer.AddElement(iElement, 0);
                  this.m_pMapControl.ActiveView.Refresh();
                  //画圆
                  pSegmentCollection = new PolygonClass();
                  pSegmentCollection.SetCircle(downPoint, m_radius);
                  pGeom = pSegmentCollection as IGeometry; 
            if (pGeom != null)
            {
                AddCreateCircleElement(pGeom, this.m_pMapControl.ActiveView);
                this.m_pMapControl.ActiveView.Refresh();


                GetPipeInfo();
            }
            if (m_pRecordPointArray.Count != 0)
            {
                m_pRecordPointArray.RemoveAll();
            }
            m_bInUse = false;
         }
                      
        }


          private void GetPipeInfo()
          {
              Load_LyrLstToArray();
              m_pArrPipes = new ArrayList();
              ISpatialFilter pSpatialFilter = new SpatialFilterClass();
              pSpatialFilter.Geometry = pGeom;             
              pSpatialFilter.WhereClause = fi.Name + "='消防栓'";
              pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

              for (int i = 0; i < m_pArrPointLayers.Count; i++)
              {
                  pFeatureClass = (m_pArrPointLayers[i] as IFeatureLayer).FeatureClass;
                  Console.WriteLine(pFeatureClass.AliasName);
                  if (pFeatureClass.AliasName == "给水_点")
                  {
                      pFeaCur = pFeatureClass.Search(pSpatialFilter, false);//获得框选的消防栓的管点
                      pFea = pFeaCur.NextFeature();
                      while (pFea != null)
                      {
                          m_pArrPipes.Add(pFea);
                          string Filename = Application.StartupPath + @"\..\Resource\Images\fireHydrant.png"; 
                          double mapX, mapY;
                          mapX = (pFea.Extent.XMax + pFea.Extent.XMin) / 2;
                          mapY = (pFea.Extent.YMax + pFea.Extent.YMin) / 2;
                          IPoint pPoint = new PointClass();
                          pPoint.PutCoords(mapX, mapY);
                          IPictureMarkerSymbol pPictureMarkerSymbol = new PictureMarkerSymbolClass();
                          pPictureMarkerSymbol.Size =50;
                          pPictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPicturePNG, Filename);
                          IMarkerElement pMarkerElement = new MarkerElementClass();
                          pMarkerElement.Symbol = pPictureMarkerSymbol as IMarkerSymbol;
                          IElement pElement = (IElement)pMarkerElement;
                          pElement.Geometry = pPoint;

                          this.m_pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);
                          this.m_pMapControl.FlashShape(pFea.Shape, 3, 500, null);
                          Console.WriteLine(pFea.OID);
                          pFea = pFeaCur.NextFeature();
                      }
                      
                      break;
                 
                  }

                  
                  
              }
              
          }

          #region 判断可进行操作的图层数据
          /// <summary>
          /// 判断可进行操作的图层数据
          /// </summary>
          private void Load_LyrLstToArray()
          {
              m_pArrPointLayers = new ArrayList();
              ILayer pLayer;

              if (m_FocusMap.LayerCount == 0)
              {
                  return;
              }

              for (int i = 0; i < m_FocusMap.LayerCount; i++)
              {
                  pLayer = m_FocusMap.get_Layer(i);
                  if (pLayer.Visible)
                  {
                      Load_LyrLstToArray(pLayer);
                  }
              }
          }
          /// <summary>
          /// 递归调用获得所有可操作的数据图层－散点
          /// </summary>
          /// <param name="pLyr"></param> 
          private void Load_LyrLstToArray(ILayer pLyr)
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
                      if (pLyr.Visible)
                      {
                          Load_LyrLstToArray(pLyr);
                      }
                  }
              }
              else
              {
                  if (pLyr is IGeoFeatureLayer)//如果是地理要素图层
                  {
                      pFeatureLayer = pLyr as IFeatureLayer;
                      pFeatureClass = pFeatureLayer.FeatureClass;
                      //  Console.WriteLine(pFeatureLayer.Name);
                      //修改-添加20060913
                      //if (pFeatureClass.FindField(fi5.Name) == -1) return;

                      if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                      {
                          m_pArrPointLayers.Add(pLyr);
                      }

                      //修改-添加20060913
                  }
              }
          }
          #endregion

        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
          {
              

              m_pPoint = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                   m_pAnchorPoint = m_pPoint;                  
                  if (!m_bInUse) return;
                  m_pCircleFeed.MoveTo(m_pAnchorPoint);
   
          }


        public void AddCreateCircleElement(IGeometry pCircularArc, IActiveView pAV)
        {
            IFillShapeElement pElemFillShp;
            IElement pElem;
            ISimpleFillSymbol pSFSym;
            ISegmentCollection pSegColl = (ISegmentCollection)pCircularArc;
            object missing = Type.Missing;
            pElem = new CircleElementClass();
            pElem.Geometry = (IGeometry)pCircularArc;
            pElemFillShp = (IFillShapeElement)pElem;
            pSFSym = new SimpleFillSymbolClass();
            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.FillColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            pSFSym.Color = pColor;
            pSFSym.Style = esriSimpleFillStyle.esriSFSHollow;
            pElemFillShp.Symbol = pSFSym;
            pGraphicsContainer = pAV as IGraphicsContainer;
            pGraphicsContainer.AddElement(pElem, 0);

        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            gc.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
    
    }
}
