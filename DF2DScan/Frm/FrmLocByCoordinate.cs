using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace DF2DScan.Frm
{
    public partial class FrmLocByCoordinate : XtraForm
    {
        private double x;
        private double y;
        public double X
        {
            get { return x; }
        }
        public double Y
        {
            get { return y; }
        }
        public FrmLocByCoordinate()
        {
            InitializeComponent();
        }

       

        

        private static IEnvelope NewEnv(IPoint point,double offset)
        {
            IEnvelope env = new EnvelopeClass();
            env.XMin = point.X - offset;
            env.YMin = point.Y - offset;
            env.XMax = point.X + offset;
            env.YMax = point.Y + offset;
            return env;

        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    if (textEdit1.Text != null && textEdit2.Text != null)
        //    {
        //        x = Convert.ToDouble(textEdit1.Text);
        //        y = Convert.ToDouble(textEdit2.Text);
        //    }
        //    else
        //    {
        //        MessageBox.Show("请输入正确的坐标！");
        //    }

        //    IPoint pPoint = new PointClass();
        //    pPoint.X = x;
        //    pPoint.Y = y;
        //    IEnvelope env = NewEnv(pPoint, 100);
        //    DF2DApplication app = DF2DApplication.Application;
        //    app.Current2DMapControl.ActiveView.Extent = env;
        //    app.Current2DMapControl.CenterAt(pPoint);

           

            //IGeoFeatureLayer pGeoFtLayer = pFtLayer as IGeoFeatureLayer;

            //ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbolClass();
            //pMarkerSymbol.Size = 10;
            //pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;

            //IRgbColor pColor = new RgbColorClass();
            //pColor.Blue = 100;
            //pColor.Red = 100;
            //pColor.Green = 100;

            //pMarkerSymbol.Color = pColor;
            //ISymbol pSymbol = (ISymbol)pMarkerSymbol;

            //ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
            //pSimpleRenderer.Symbol = pSymbol;
            //pGeoFtLayer.Renderer = pSimpleRenderer as IFeatureRenderer;

            //app.Current2DMapControl.ActiveView.Refresh();


           

            
        }

        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
       
    }
}
