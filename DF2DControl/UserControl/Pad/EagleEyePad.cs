using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using DFWinForms.Service;
using ESRI.ArcGIS.Display;
using DF2DControl.UserControl.View;
using DFWinForms.UserControl;
using ICSharpCode.Core;

namespace DF2DControl.UserControl.Pad
{
    public class EagleEyePad : BasePadContent
    {
        private IMapControlEvents2_Event mapControlEvents = null;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
        public ESRI.ArcGIS.Controls.AxMapControl MapControl
        {
            get
            {
                return axMapControl;
            }
        }

        public EagleEyePad()
        {
            try
            {
                InitializeComponent();

                mapControlEvents = axMapControl.GetOcx() as IMapControlEvents2_Event;
                mapControlEvents.OnMapReplaced += new IMapControlEvents2_OnMapReplacedEventHandler(mapEvent_OnMapReplaced);
                mapControlEvents.OnMouseMove += new IMapControlEvents2_OnMouseMoveEventHandler(mapControlEvents_OnMouseMove);
                mapControlEvents.OnMouseDown += new IMapControlEvents2_OnMouseDownEventHandler(mapControlEvents_OnMouseDown);
                mapControlEvents.OnMouseUp += new IMapControlEvents2_OnMouseUpEventHandler(mapControlEvents_OnMouseUp);
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private IEnvelope interEnvelope;
        private IPoint ptInEnvelope;
        private bool bPtInEnvelope;
        private void mapControlEvents_OnMouseDown(int button, int shift, int X, int Y, double mapX, double mapY)
        {
            if (this.axMapControl.Map.LayerCount > 0)
            {
                if (button == 1)
                {
                    if (interEnvelope == null)
                    {
                        Map2DView view = UCService.GetContent(typeof(Map2DView)) as Map2DView;
                        if (view == null) return;
                        interEnvelope = view.MapControl.Extent;
                    }
                    if (interEnvelope == null) return;
                    IPoint pt = new PointClass();
                    pt.PutCoords(mapX, mapY);
                    IRelationalOperator relOper = interEnvelope as IRelationalOperator;
                    bPtInEnvelope = relOper.Contains(pt);
                    if (bPtInEnvelope)
                    {
                        ptInEnvelope = pt;
                    }
                    else interEnvelope.PutCoords(mapX - interEnvelope.Width / 2, mapY - interEnvelope.Height / 2, mapX + interEnvelope.Width / 2, mapY + interEnvelope.Height / 2);
                    DrawEnvelope(interEnvelope);
                }
                else if (button == 2)
                {
                    //interEnvelope = this.axMapControl.TrackRectangle();
                    //view.MapControl.Extent = interEnvelope;
                }
            }
        }
        public void DrawEnvelope(IEnvelope envelope)
        {
            if (envelope == null) return;
            try
            {
                //将鹰眼地图设置为地理容器,再设置为活动视图
                IGraphicsContainer pGraphicsContainer = this.axMapControl.Map as IGraphicsContainer;
                IActiveView pActiveView = pGraphicsContainer as IActiveView;
                //清除鹰眼地图中的任何图形元素
                pGraphicsContainer.DeleteAllElements();
                //设置矩形范围
                IRectangleElement pRectangeEle = new RectangleElementClass();
                IElement pElement = pRectangeEle as IElement;
                pElement.Geometry = envelope;

                //创建鹰眼图中的红线框
                IRgbColor pColor = new RgbColor();
                pColor.Red = 255;
                pColor.Blue = 0;
                pColor.Green = 0;
                pColor.Transparency = 255;
                //创建线符号对象
                ILineSymbol pOutline = new SimpleLineSymbolClass();
                pOutline.Width = 2;
                pOutline.Color = pColor;

                //设置颜色属性
                pColor = new RgbColorClass();
                pColor.Red = 255;
                pColor.Blue = 0;
                pColor.Green = 0;
                pColor.Transparency = 0;

                //设置填充符号
                IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
                pFillSymbol.Color = pColor;
                pFillSymbol.Outline = pOutline;

                IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
                pFillShapeEle.Symbol = pFillSymbol;

                pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

                interEnvelope = envelope;
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        private void mapControlEvents_OnMouseMove(int button, int shift, int X, int Y, double mapX, double mapY)
        {
            if (this.axMapControl.Map.LayerCount > 0)
            {
                if (button == 1 && interEnvelope != null)
                {
                    if (bPtInEnvelope)
                    {
                        interEnvelope.PutCoords(interEnvelope.XMin + (mapX - ptInEnvelope.X), interEnvelope.YMin + (mapY - ptInEnvelope.Y),
                                interEnvelope.XMax + (mapX - ptInEnvelope.X), interEnvelope.YMax + (mapY - ptInEnvelope.Y));
                        ptInEnvelope.PutCoords(mapX, mapY);
                    }
                    else interEnvelope.PutCoords(mapX - interEnvelope.Width / 2, mapY - interEnvelope.Height / 2, mapX + interEnvelope.Width / 2, mapY + interEnvelope.Height / 2);
                    DrawEnvelope(interEnvelope);
                }
            }
        }

        void mapControlEvents_OnMouseUp(int button, int shift, int X, int Y, double mapX, double mapY)
        {
            if (this.axMapControl.Map.LayerCount > 0)
            {
                Map2DView view = UCService.GetContent(typeof(Map2DView)) as Map2DView;
                if (view == null) return;

                IPoint ptNew = new PointClass();
                if (bPtInEnvelope)
                {
                    interEnvelope.PutCoords(interEnvelope.XMin + (mapX - ptInEnvelope.X), interEnvelope.YMin + (mapY - ptInEnvelope.Y),
                            interEnvelope.XMax + (mapX - ptInEnvelope.X), interEnvelope.YMax + (mapY - ptInEnvelope.Y));
                    ptNew.PutCoords((interEnvelope.XMax + interEnvelope.XMin) / 2, (interEnvelope.YMax + interEnvelope.YMin) / 2);
                    ptInEnvelope.PutCoords(mapX, mapY);
                }
                else ptNew.PutCoords(mapX, mapY);
                view.MapControl.CenterAt(ptNew);
                view.MapControl.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        private void mapEvent_OnMapReplaced(object newMap)
        {
            this.axMapControl.ShowScrollbars = false;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EagleEyePad));
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.SuspendLayout();
            // 
            // axMapControl
            // 
            this.axMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl.Location = new System.Drawing.Point(0, 0);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
            this.axMapControl.Size = new System.Drawing.Size(165, 179);
            this.axMapControl.TabIndex = 0;
            // 
            // EagleEyePad
            // 
            this.Controls.Add(this.axMapControl);
            this.Name = "EagleEyePad";
            this.Size = new System.Drawing.Size(165, 179);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
