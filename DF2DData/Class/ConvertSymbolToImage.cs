using System;
using System.Drawing;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;

namespace DF2DData.Class
{
    /// <summary>
    /// 符号转化为图片.
    /// </summary>
    public class ConvertSymbolToImage
    {
        public ConvertSymbolToImage()
        {

        }
        //符号转化为图片.bmp
        public static Image Convert(ISymbol sym, int width, int height)
        {
            Image img = new Bitmap(width, height);
            Graphics gc = Graphics.FromImage(img);//从指定大小的img对象创建绘图设备
            IntPtr hdc = gc.GetHdc();//获得绘图设备句柄
            IEnvelope env = new EnvelopeClass();
            env.XMin = 0;
            env.XMax = width;
            env.YMin = 0;
            env.YMax = height;
            IGeometry geo = CreateGeometryFromSymbol(sym, env);
            if (geo != null)
            {
                ITransformation trans = CreateTransformationFromHDC(hdc, width, height);//仿射变换
                sym.SetupDC((int)hdc, trans);//与绘图设备句柄绑定
                sym.Draw(geo);
                sym.ResetDC();//重新设置
            }
            gc.ReleaseHdc(hdc);//可以释放了

            return img;
        }
        public static Image Convert(ISymbol sym)
        {
            return Convert(sym, 16, 16);
        }

        //从符号创建几何形体
        private static IGeometry CreateGeometryFromSymbol(ISymbol sym, IEnvelope env)
        {
            if (sym is IMarkerSymbol)
            {
                IArea area = (IArea)env;

                return (IGeometry)area.Centroid;
            }
            else if (sym is ILineSymbol || sym is ITextSymbol)
            {
                IPolyline line = new PolylineClass();
                IPoint pt = new PointClass();
                pt.PutCoords(env.LowerLeft.X, (env.LowerLeft.Y + env.UpperRight.Y) / 2);
                line.FromPoint = pt;
                pt = new PointClass();
                pt.PutCoords(env.UpperRight.X, (env.LowerLeft.Y + env.UpperRight.Y) / 2);
                line.ToPoint = pt;

                return (IGeometry)line;
            }
            else if (sym is IFillSymbol)
            {
                IPolygon polygon = new PolygonClass();
                IPointCollection ptCol = (IPointCollection)polygon;
                IPoint pt = new PointClass();

                pt.PutCoords(env.LowerLeft.X, env.LowerLeft.Y);
                ptCol.AddPoints(1, ref pt);
                pt.PutCoords(env.UpperLeft.X, env.UpperLeft.Y);
                ptCol.AddPoints(1, ref pt);
                pt.PutCoords(env.UpperRight.X, env.UpperRight.Y);
                ptCol.AddPoints(1, ref pt);
                pt.PutCoords(env.LowerRight.X, env.LowerRight.Y);
                ptCol.AddPoints(1, ref pt);
                pt.PutCoords(env.LowerLeft.X, env.LowerLeft.Y);
                ptCol.AddPoints(1, ref pt);

                return (IGeometry)polygon;
            }
            else
            {
                return null;
            }
        }

        //设备句柄仿射变换
        private static ITransformation CreateTransformationFromHDC(IntPtr HDC, int width, int height)
        {
            IEnvelope env = new EnvelopeClass();
            env.PutCoords(0, 0, width, height);

            //目标矩形的大小
            tagRECT frame = new tagRECT();
            frame.left = 0;
            frame.top = 0;
            frame.right = width;
            frame.bottom = height;

            double dpi = Graphics.FromHdc(HDC).DpiY;//设备句柄的垂直分辨率
            long lDpi = (long)dpi;
            if (lDpi == 0)
            {
                //XtraMessageBox.Show("获取设备比例尺失败!");

                return null;
            }

            IDisplayTransformation dispTrans = new DisplayTransformationClass();
            dispTrans.Bounds = env;
            dispTrans.VisibleBounds = env;
            dispTrans.set_DeviceFrame(ref frame);
            dispTrans.Resolution = dpi;

            return dispTrans;

        }
    }
}
