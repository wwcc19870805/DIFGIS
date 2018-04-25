using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using DFWinForms.Service;
using ESRI.ArcGIS.Geometry;
using System.Drawing;
using DFCommon.Class;
using DF2DTool.Class;
using DF2DTool.Frm;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace DF2DTool.Command
{
    class CmdArea : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_FocusMap;
        private IActiveView m_pActiveView;
        private IRubberBand rubberBand = null;
        private IGraphicsContainer pGraphicsContainer;

        public static string strResult1;
        public static string strResult2;

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
            m_FocusMap = m_pMapControl.ActiveView.FocusMap;
            m_pActiveView = (IActiveView)this.m_FocusMap;
            m_pMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;

        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            
            rubberBand = new RubberPolygonClass();
            IGeometry polygon = rubberBand.TrackNew(this.m_pMapControl.ActiveView.ScreenDisplay, null);
            if (polygon != null)
            {
                // 获取ILine符号接口
                ILineSymbol outline = new SimpleLineSymbol();
                // 设置线符号属性
                outline.Width = 1.5;

                Color color = ColorTranslator.FromHtml(SystemInfo.Instance.LineColor);
                IColor pColor = new RgbColorClass();
                pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
                outline.Color = pColor;
                // 获取IFillSymbol接口
                ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
                // 设置填充符号属性
                simpleFillSymbol.Outline = outline;
                simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;
                IFillSymbol pSymbol = new SimpleFillSymbolClass();
                pSymbol = (IFillSymbol)simpleFillSymbol;

                IElement pElement = new PolygonElementClass();
                pElement.Geometry = polygon;

                IFillShapeElement pFillElement;
                pFillElement = (IFillShapeElement)pElement;
                pFillElement.Symbol = (IFillSymbol)pSymbol;

                m_pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);//绘制地图元素
                this.m_pMapControl.ActiveView.Refresh();
                /* CommonFunction.AddElement(m_pMapControl, polygon);*///绘制地图元素

                Object obj = Math.Abs(double.Parse(((IArea)polygon).Area.ToString(".##")));
                strResult1 = obj.ToString() + "平方米";
                strResult2 = ((IPolygon)polygon).Length.ToString(".##") + "米";

                if ((strResult1 != null) && (strResult2 != null))
                {
                    FrmArea  frmArea = FrmArea.Instance();                     
                    frmArea.ShowDialog();    
                    /*XtraMessageBox.Show()*/
                }
            }
            base.OnMouseDown(button, shift, x, y, mapX, mapY);
            

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
