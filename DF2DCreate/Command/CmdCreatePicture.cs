using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using System.Drawing;
using DevExpress.XtraEditors;

namespace DF2DCreate.Command
{
    class CmdCreatePicture : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IGraphicsContainer pGraphicsContainer;


        private IPoint point ;
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

            point = this.m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Formats|*.jpg;*.gif;*.tif;*.emf;*.bmp;*.png|JPEG Image|*.jpg|GIF Image|*.gif|TIF Image|*.tif|Windows Enhanced Metafile|*.emf|Windows Bitmap|*.bmp|PNG Image|*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.AddPictureElement(openFileDialog.FileName);
                this.m_pMapControl.ActiveView.Refresh();
            }	
        }

        private void AddPictureElement(string strFileName)
        {
            IPictureElement2 pictureElement = null;
            string suffix = strFileName.Substring(strFileName.Length-3,3).ToLower();
            switch (suffix)
            {
                case "jpg":
                    pictureElement = new JpgPictureElementClass();
                    break;
                case "gif":
                    pictureElement = new GifPictureElementClass();
                    break;
                case "tif":
                    pictureElement = new TifPictureElementClass();
                    break;
                case "emf":
                    pictureElement = new EmfPictureElementClass();
                    break;
                case "bmp":
                    pictureElement = new BmpPictureElementClass();
                    break;
                case "png":
                    pictureElement = new PngPictureElementClass();
                    break;
                default:
                    return; 
            }
            try
            {
                pictureElement.ImportPictureFromFile(strFileName);
            }
            catch (Exception ex)
            {
                
                XtraMessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
            IElement iElement = (IElement)pictureElement;
           IActiveView iActiveView = this.m_pMapControl.ActiveView;
            double pictureWidth = 0;
            double pictureHeight = 0;
            pictureElement.QueryIntrinsicSize(ref pictureWidth, ref pictureHeight);


            IEnvelope env = this.m_pMapControl.ActiveView.Extent;
            tagRECT rect = new tagRECT();
            m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation.TransformRect(env, ref rect,               
                (int)esriDisplayTransformationEnum.esriTransformToDevice);

            Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left,
                rect.bottom - rect.top);

            int x = (rectangle.Left + rectangle.Right) / 2;
            int y = (rectangle.Top + rectangle.Height) / 2;
           /* IPoint point = iActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);*/
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(point.X, point.Y,
                point.X + (int)(pictureWidth / 72 * iActiveView.ScreenDisplay.DisplayTransformation.ScaleRatio),
                point.Y + (int)(pictureHeight / 72 * iActiveView.ScreenDisplay.DisplayTransformation.ScaleRatio));

            iElement.Geometry = envelope;
            IBoundsProperties bp = iElement as IBoundsProperties;
            bp.FixedAspectRatio = true;
            pictureElement.MaintainAspectRatio = true;
            pGraphicsContainer = this.m_pMapControl.ActiveView as IGraphicsContainer;
            pGraphicsContainer.AddElement(iElement,0);
            this.m_pMapControl.ActiveView.Refresh();            

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
