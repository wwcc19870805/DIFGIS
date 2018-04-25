using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using DFWinForms.Service;
using DF2DData.Class;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using System.Collections;
using ESRI.ArcGIS.Display;
using System.Drawing;
using DFCommon.Class;
using DF2DCreate.Class;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DF2DCreate.Command
{
    class CmdLableLineProperty : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_pMap;
        private IActiveView m_pActiveView;
        private static IGeometry Geo;

        private IFeatureClass pFeatureClass;
        private IFeatureCursor pFeaCur;
        private IFeature pFea;
        private IPoint p;
        private IGeometry pGeo;
        private IFeature pFeature;




        private ArrayList m_pArrText = new ArrayList();
        private ArrayList m_pArrFeature = new ArrayList();
        private ArrayList m_pArrLineLayers;
        private ArrayList  m_pArrPipes = new ArrayList() ;
        private ArrayList m_pArrIntersectPnt  = new ArrayList();

        private double dblStartX;
        private double dblStartY;
        private int nWidth = 8; //标注表格的单位宽高
        private int nHeight = 2;

        private String str1, str2, str3, str4, str5, str6, str7;

        DFDataConfig.Class.FieldInfo fi, fi1, fi2, fi3, fi4, fi5, fi6;//管线字段信息

          public override void Run(object sender, EventArgs e)
          {
              Map2DCommandManager.Push(this);
              mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
              if (mapView == null) return;
              bool bBind = mapView.Bind(this);
              if (!bBind) return;
              app = (DF2DApplication)this.Hook;
              if (app == null || app.Current2DMapControl == null) return;
              m_pMapControl = app.Current2DMapControl;
              m_pActiveView = this.m_pMapControl.ActiveView;
              m_pMap = m_pMapControl.ActiveView.FocusMap;
              List<DF2DFeatureClass> st = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
              if (st == null) return;
              foreach (DF2DFeatureClass dfcc in st)
              {
                  IFeatureClass fc = dfcc.GetFeatureClass();
                  if (fc == null) continue;
                  FacilityClass fac = dfcc.GetFacilityClass();
                  if (fac == null) continue;
                  List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                  fi = fac.GetFieldInfoBySystemName("classify");
                  if (fi == null || fi.CanQuery == false) continue;
                  fi1 = fac.GetFieldInfoBySystemName("snodeid");
                  if (fi1 == null || fi1.CanQuery == false) continue;
                  fi2 = fac.GetFieldInfoBySystemName("enodeid");
                  if (fi2 == null || fi2.CanQuery == false) continue;
                  fi3 = fac.GetFieldInfoBySystemName("matter");
                  if (fi3 == null || fi3.CanQuery == false) continue;
                  fi4 = fac.GetFieldInfoBySystemName("coverstsle");
                  if (fi4 == null || fi4.CanQuery == false) continue;
                  fi5 = fac.GetFieldInfoBySystemName("standard");
                  if (fi5 == null || fi5.CanQuery == false) continue;
                  fi6 = fac.GetFieldInfoBySystemName("proad");
                  if (fi6 == null||fi6.CanQuery==false)continue;
              }
              m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
          }

          public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
          {
               IScreenDisplay pScr = m_pActiveView.ScreenDisplay;
               
               if (m_pArrPipes.Count != 0)
               {
                   m_pArrPipes.Clear();//清除上一步操作的要素
               }


                IRubberBand pRB = new RubberLineClass();
               Geo= pRB.TrackNew(pScr, null);
              

               Element.DeleteElement(m_pActiveView.GraphicsContainer, "gx");
               Element.AddGraphics(m_pActiveView.GraphicsContainer, Geo, GetDefaultSymbol(esriGeometryType.esriGeometryPolyline), "dmx");
               IElement pElement = Element.GetElementByName(m_pMapControl.ActiveView.GraphicsContainer, "gx");
               if (pElement != null)
               {
                   m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
               }
              base.OnMouseDown(button, shift, x, y, mapX, mapY);
              
             
              //得到相交的管线
              GetPipeInfo();


              if (m_pArrPipes.Count != 1)
              {
                  XtraMessageBox.Show("请重新获取单个管线");
                  m_pArrPipes.Clear();
                  return;
              }
              if (m_pArrText.Count > 0 && m_pArrFeature.Count > 0)
              {
                  m_pArrText.Clear();
                  m_pArrFeature.Clear();
              }


              for (int i = 0; i < m_pArrPipes.Count; i++)
              {
                  pFeature = m_pArrPipes[i] as IFeature;
                  if (pFeature == null) return;
                  if (pFeature.get_Value(pFeature.Fields.FindField(fi.Name)).ToString() != "")
                  {
                      str1 = pFeature.get_Value(pFeature.Fields.FindField(fi.Name)).ToString();
                      m_pArrText.Add(fi.SystemAlias);
                      m_pArrFeature.Add(str1);
                  }

                  if (pFeature.get_Value(pFeature.Fields.FindField(fi1.Name)).ToString() != "")
                  {
                      str2 = pFeature.get_Value(pFeature.Fields.FindField(fi1.Name)).ToString();
                      m_pArrText.Add(fi1.SystemAlias);
                      m_pArrFeature.Add(str2);
                  }

                  if (pFeature.get_Value(pFeature.Fields.FindField(fi2.Name)).ToString() != "")
                  {
                      str3 = pFeature.get_Value(pFeature.Fields.FindField(fi2.Name)).ToString();
                      m_pArrText.Add(fi2.SystemAlias);
                      m_pArrFeature.Add(str3);
                  }
                  if (pFeature.get_Value(pFeature.Fields.FindField(fi3.Name)).ToString() != "")
                  {
                      str4 = pFeature.get_Value(pFeature.Fields.FindField(fi3.Name)).ToString();
                      m_pArrText.Add(fi3.SystemAlias);
                      m_pArrFeature.Add(str4);
                  }
                  if (pFeature.get_Value(pFeature.Fields.FindField(fi4.Name)).ToString() != "")
                  {
                      str5 = pFeature.get_Value(pFeature.Fields.FindField(fi4.Name)).ToString();
                      m_pArrText.Add(fi4.SystemAlias);
                      m_pArrFeature.Add(str5);
                  }

                  if (pFeature.get_Value(pFeature.Fields.FindField(fi5.Name)).ToString() != "")
                  {
                      str6 = pFeature.get_Value(pFeature.Fields.FindField(fi5.Name)).ToString();
                      m_pArrText.Add(fi5.SystemAlias);
                      m_pArrFeature.Add(str6);
                  }

                  if (pFeature.get_Value(pFeature.Fields.FindField(fi6.Name)).ToString() != "")
                  {
                      str7 = pFeature.get_Value(pFeature.Fields.FindField(fi6.Name)).ToString();
                      m_pArrText.Add(fi6.SystemAlias);
                      m_pArrFeature.Add(str7);
                  }
              }
              //管线属性标注
              DrawPipeLabels();

          }



          #region //根据Geometry类型生成一个默认符号
          /// <summary>
          /// 根据Geometry类型生成一个默认符号
          /// </summary>
          /// <param name="type"></param>
          /// <returns></returns>
          public ISymbol GetDefaultSymbol(esriGeometryType type)
          {
              ISymbol sym = null;

              IRgbColor mCor = new RgbColorClass();
              mCor.Red = 0;
              mCor.Blue = 0;
              mCor.Green = 0;

              IRgbColor lCor = new RgbColorClass();
              mCor.Red = 0;
              lCor.Blue = 0;
              mCor.Green = 0;

              IRgbColor fCor = new RgbColorClass();
              mCor.Red = 0;
              fCor.Blue = 0;
              mCor.Green = 0;

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

          private void GetPipeInfo()
          {

               
              IGeometry pPipeLineGeo;
              ITopologicalOperator pTopo;
              Load_LyrLstToArray();
              
              ISpatialFilter pSpatialFilter = new SpatialFilterClass();
              pSpatialFilter.Geometry = Geo;
              pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
              for (int i = 0; i < m_pArrLineLayers.Count; i++)
              {

                  pFeatureClass = (m_pArrLineLayers[i] as IFeatureLayer).FeatureClass;
                  pFeaCur = pFeatureClass.Search(pSpatialFilter, false);//获得相交的管线
                  pFea = pFeaCur.NextFeature();
                  while (pFea != null)
                  {
                     
                      m_pArrPipes.Add(pFea);

                      pFea = pFeaCur.NextFeature();
                  }

              }

              
          }

          #region 判断可进行操作的图层数据
          /// <summary>
          /// 判断可进行操作的图层数据
          /// </summary>
          private void Load_LyrLstToArray()
          {
              m_pArrLineLayers = new ArrayList();
              ILayer pLayer;

              if (m_pMap.LayerCount == 0)
              {
                  return;
              }

              for (int i = 0; i < m_pMap.LayerCount; i++)
              {
                  pLayer = m_pMap.get_Layer(i);
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

                      if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                      {
                          m_pArrLineLayers.Add(pLyr);
                      }

                      //修改-添加20060913
                  }
              }
          }
          #endregion

          private void DrawPipeLabels()
          {

              for (int i = 0; i < m_pArrPipes.Count; i++)
              {
                  //确定交点坐标
                  IFeature pea = m_pArrPipes[i] as IFeature;
                  if (pea == null) return;
                  p = (Geo as IPolyline).ToPoint as IPoint;
                  dblStartX = p.X;
                  dblStartY = p.Y;
                 
                  

                  IPoint p1 = new PointClass();
                  IPoint p2 = new PointClass();
                  IPoint p3 = new PointClass();
                  IPoint p4 = new PointClass();
                  IPoint p5 = new PointClass();
                  IPoint p6 = new PointClass();
                  IPoint p7 = new PointClass();
                  IPoint p8 = new PointClass();

                  p3.X = dblStartX;
                  p3.Y = dblStartY + nHeight * m_pArrText.Count - 5.5;
                  p1.X = dblStartX + 1;
                  p1.Y = dblStartY + nHeight * m_pArrText.Count - 5.5;
                  p2.X = dblStartX + 1 ;
                  p2.Y = dblStartY + nHeight * m_pArrText.Count + 2;
                  p4.X = dblStartX + 1;
                  p4.Y = dblStartY + nHeight * m_pArrText.Count - 13;
                  p5.X = dblStartX + nWidth/2-1;
                  p5.Y = dblStartY + nHeight * m_pArrText.Count - 13;
                  p6.X = dblStartX + nWidth +15;
                  p6.Y = dblStartY + nHeight * m_pArrText.Count - 13;
                  p7.X = dblStartX + nWidth + 15;
                  p7.Y = dblStartY + nHeight * m_pArrText.Count + 2;
                  p8.X = dblStartX + nWidth / 2-1 ;
                  p8.Y = dblStartY + nHeight * m_pArrText.Count + 2;


                  //画线
                  IPolyline line1 = new PolylineClass();
                  //ILine line1 = new LineClass();
                  line1.FromPoint = p;
                  line1.ToPoint = p3;
                  DrawLine(line1);

                  IPolyline line2 = new PolylineClass();
                  //ILine line1 = new LineClass();
                  line2.FromPoint = p3;
                  line2.ToPoint = p1;
                  DrawLine(line2);

                  IPolyline line3 = new PolylineClass();
                  line3.FromPoint = p2;
                  line3.ToPoint = p1;
                  DrawLine(line3);

                  IPolyline line4 = new PolylineClass();
                  line4.FromPoint = p1;
                  line4.ToPoint = p4;
                  DrawLine(line4);

                  IPolyline line5 = new PolylineClass();
                  line5.FromPoint = p5;
                  line5.ToPoint = p6;
                  DrawLine(line5);

                  IPolyline line6 = new PolylineClass();
                  line6.FromPoint = p6;
                  line6.ToPoint = p7;
                  DrawLine(line6);

                  IPolyline line7 = new PolylineClass();
                  line7.FromPoint = p7;
                  line7.ToPoint = p8;
                  DrawLine(line7);

                  IPolyline line8 = new PolylineClass();
                  line8.FromPoint = p8;
                  line8.ToPoint = p5;
                  DrawLine(line8);
              }

              //画信息栏列名
              DrawColName();

              //画管点信息
              DrawPipiInfo();

          }


          private void DrawLine(IPolyline line)
          {
              ILineElement lineElement;
              lineElement = new LineElementClass();
              IElement element;
              ISimpleLineSymbol pSLnSym;
              IRgbColor pRGB;
              pRGB = new RgbColorClass();
              pRGB.Red = 0;
              pRGB.Green = 0;
              pRGB.Blue = 0;
              pSLnSym = new SimpleLineSymbolClass();
              pSLnSym.Color = pRGB;
              pSLnSym.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid;
              lineElement.Symbol = pSLnSym;
              element = lineElement as IElement;
              element.Geometry = line;
              IGraphicsContainer graphicsContainer = this.m_pMapControl.Map as IGraphicsContainer;
              graphicsContainer.AddElement(element, 0);
              this.m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
          }

          private void DrawColName()
          {
              IGraphicsContainer pGraContainer = m_pMapControl.Map as IGraphicsContainer;

              IPoint pAnnoPnt = new PointClass();
              IFormattedTextSymbol sym = new TextSymbolClass();
              ITextElement pTextElement;
              IElement pElement;


              stdole.IFontDisp myfont = (stdole.IFontDisp)new stdole.StdFontClass();
              myfont.Name = "华文细黑";
              sym.Font = myfont;

              myfont.Bold = true;
              sym.Size =SystemInfo.Instance.TextSize;


              Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
              IColor pColor = new RgbColorClass();
              pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
              sym.Color = pColor;

              for (int i = 0; i < m_pArrText.Count; i++)
              {
                 
                  sym.Text = m_pArrText[i].ToString();
                  pAnnoPnt.PutCoords(dblStartX + nWidth, dblStartY + nHeight * (m_pArrText.Count-i));
                  pTextElement = new TextElementClass();
                  pTextElement.ScaleText = true;
                  pTextElement.Text = sym.Text;
                  pTextElement.Symbol = sym;
                  pElement = (IElement)pTextElement;
                  pElement.Geometry = pAnnoPnt;
                  pGraContainer.AddElement(pElement, 0);
        
            }

              (m_pMapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_pMapControl.Extent);

          }

          int i = 0;
          private void DrawPipiInfo()
          {

              /*IFeature pFeature;*/
              IGraphicsContainer pGraContainer = m_pMapControl.Map as IGraphicsContainer;
              ISpatialFilter pSpatialFilter = new SpatialFilterClass();
              pSpatialFilter.Geometry = Geo;
              pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
              IPoint pAnnoPnt = new PointClass();
              ITextSymbol sym = new TextSymbolClass();
              stdole.IFontDisp myfont = (stdole.IFontDisp)new stdole.StdFontClass();
              myfont.Name = "华文细黑";
              sym.Font = myfont;

              myfont.Bold = true;
              sym.Size = SystemInfo.Instance.TextSize;

              Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
              IColor pColor = new RgbColorClass();
              pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
              sym.Color = pColor;
              for (int i = 0; i < m_pArrFeature.Count; i++)
              {
                 
                  sym.Text = m_pArrFeature[i].ToString();
                  pAnnoPnt.PutCoords(dblStartX + nWidth + 10, dblStartY + nHeight * (m_pArrFeature.Count-i));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                  ITextElement pTextElement = new TextElementClass();
                  pTextElement.ScaleText = true;
                  pTextElement.Text = sym.Text;
                  pTextElement.Symbol = sym;
                  IElement pElement = (IElement)pTextElement;
                  pElement.Geometry = pAnnoPnt;

                  pGraContainer.AddElement(pElement, 0);
                  
              }
          }


          public override void RestoreEnv()
          {
              Map2DCommandManager.Pop();
              mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
              if (mapView == null) return;
              mapView.UnBind(this);

          }


    }
}
