using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using DFWinForms.Service;
using ESRI.ArcGIS.Display;
using System.Drawing;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using DFCommon.Class;
using DevExpress.Utils;
using Microsoft.VisualBasic;
using DevExpress.XtraEditors;
using DF2DCreate.Frm;
using System.Windows.Forms;


namespace DF2DCreate.Command
{
    class CmdCreateText : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IGraphicsContainer pGraphicsContainer;

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
           
        }


        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            //弹出对话框
            IGraphicElements iGraphicElements = new GraphicElementsClass();

            FrmCreateText frmCreateText = new FrmCreateText();
            ITextElement iTextElement = new TextElementClass();
            //XtraMessageBox.Show("请输入文本");
            //string str = Interaction.InputBox("请输入文本", "字符串", "", 100, 100);//即获取用户输入的数据 
            if (frmCreateText.ShowDialog() == DialogResult.OK)
            {
                //string str = textEdit1.Text;               
                iTextElement.Text = FrmCreateText.value;
                //string str = frmCreateText.textEdit1.Text;
                //iTextElement.Text = "HAO";//this.iMapDocument.DocumentFilename;//判断           
                ITextSymbol sce = new TextSymbolClass();
                //ISymbolCollectionElement sce = (ISymbolCollectionElement)iTextElement;
                sce.Size = 1250/*SystemInfo.Instance.TextSize*/;
                Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
                IColor pColor = new RgbColorClass();
                pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
                sce.Color = pColor;
                iTextElement.Symbol = sce;
                IElement iElement = (IElement)iTextElement;
                IActiveView iActiveView = this.m_pMapControl.ActiveView;
                IEnvelope env = m_pMapControl.ActiveView.Extent;
                tagRECT rect = new tagRECT();
                m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.TransformRect(env, ref rect,
                    (int)esriDisplayTransformationEnum.esriTransformToDevice);

                Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left,
                     rect.bottom - rect.top);

                int x1 = (rectangle.Left + rectangle.Right) / 2;
                int y1 = (rectangle.Top + rectangle.Bottom) / 2;

               IPoint pt = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
               int x2 =(int) pt.X;
               int y2 = (int)pt.Y;
                iElement.Geometry =
                    iActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x2, y2);
                iElement.Geometry =
                  this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this.m_pMapControl.ActiveView.GraphicsContainer.AddElement(iElement, 0);
                this.m_pMapControl.ActiveView.Refresh();
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
