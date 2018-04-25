using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DDraw
{
    public class DrawToolService
    {
        private static DrawToolService _instance;
        private static readonly object syncRoot = new object();
        private DrawToolService()
        {
        }

        public static DrawToolService Instance
        {
            get
            {
                if (DrawToolService._instance == null)
                {
                    lock (syncRoot)
                    {
                        if (DrawToolService._instance == null)
                        {
                            DrawToolService._instance = new DrawToolService();
                        }
                    }
                }
                return DrawToolService._instance;
            }
        }

        public DrawTool CreateDrawTool(DrawType dt)
        {

            switch (dt)
            {
                case DrawType.Point:
                    return new DrawPoint();
                case DrawType.Line:
                    return new DrawLine();
                case DrawType.Polyline:
                    return new DrawPolyline();
                case DrawType.Polygon:
                    return new DrawPolygon();
                case DrawType.Rectangle:
                    return new DrawRectangle();
                case DrawType.SelectOne:
                    return new DrawSelectOne();
                case DrawType.Circle:
                    return new DrawCircle();
                case DrawType._3DModel:
                    return new Draw3DModel();
                //case DrawType.TerrainHole:
                //    return new DrawTerrainHole();
                default:
                    return null;
            }
        }

    }
}
