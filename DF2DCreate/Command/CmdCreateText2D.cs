using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using DF2DCreate.Frm;
using System.Windows.Forms;
using System.Drawing;
using DFCommon.Class;
using ESRI.ArcGIS.Geometry;

namespace DF2DCreate.Command
{
    class CmdCreateText2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private IScreenDisplay m_Display;
        private DF2DApplication app;
        private IGraphicsContainer pGraphicsContainer;
        public override void Run(object sender, EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null || app.Workbench == null) return;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            pGraphicsContainer = app.Current2DMapControl.ActiveView.GraphicsContainer;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            FrmCreateText frmCreateText = new FrmCreateText();
            if (frmCreateText.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ITextElement textElement = new TextElementClass();
                    textElement.ScaleText = true;
                    textElement.Text = frmCreateText.Words;

                    ITextSymbol sym = new TextSymbol();

                    Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
                    IColor pColor = new RgbColorClass();
                    pColor.RGB = color.B * 65536 + color.G * 256 + color.R;

                    sym.Color = pColor;
                    textElement.Symbol = sym;
                    sym.Size = (double)frmCreateText.WordsSize;

                    IPoint point = new PointClass();
                    point.PutCoords(mapX, mapY);
                     

                    IElement element = textElement as IElement;
                    element.Geometry = point;
                    pGraphicsContainer.AddElement(element, 0);
                    m_ActiveView.Refresh();
                }
                catch (System.Exception ex)
                {
                	
                }
                
            
            }
        }

        public override void RestoreEnv()
        {
            try
            {
                IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
                if (mapView == null) return;
                if (app == null || app.Current2DMapControl == null || app.Workbench == null) return;
                pGraphicsContainer.DeleteAllElements();
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
