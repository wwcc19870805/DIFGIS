using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace DF2DMDBConvert.Class
{
    public class Assist
    {
        private IPointCollection pCol;//点集合
        private List<FZLine> fzLines;//辅助线列表
        private FZLine fzLine;//辅助线
        private List<FZPoint> fzPoints;//辅助点列表
        private string fzNum;//辅助大类英文代码
        private string lineType;//线型
        private string mapName;//图幅名
        private double floorHeight;//地面高
        private int oid;//辅助元素oid

        public string LineType
        {
            get { return this.lineType; }
        }
        public string MapName
        {
            get { return this.mapName; }
        }
        public double FloorHeight
        {
            get { return this.floorHeight; }
        }
       
        //public  IPointCollection PointCollection
        //{
        //    get { return this.pCol; }
        //}
        public List<FZLine> FZLines
        {
            get { return this.fzLines; }
        }
        public List<FZPoint> FZPoints
        {
            get { return this.fzPoints; }
        }
        public Assist(int oid,IPointCollection pCol,string fzNum,string lineType,string mapName,double floorHeight)
        {
            this.pCol = pCol;
            this.fzNum = fzNum;
            this.floorHeight = floorHeight;
            this.lineType = lineType;
            this.mapName = mapName;
            this.oid = oid;
            Init();
        }
        private void Init()
        {
            if (this.pCol == null) return;
            fzPoints = new List<FZPoint>();//初始化辅助点列表
            try
            {
                int n = 1;
                for (int i = 0; i < pCol.PointCount - 1; i++)//遍历点集（因辅助图形闭合，点集点数=图形顶点+1，故只取前四个点）
                {
                    FZPoint fzPoint = new FZPoint(fzNum + oid + "_" + n, pCol.get_Point(i));
                    fzPoints.Add(fzPoint);
                    n++;
                }
                fzLines = new List<FZLine>();//初始化辅助线列表
                for (int j = 0; j < fzPoints.Count; j++)//遍历辅助点集
                {
                    if (j == fzPoints.Count - 1)//如果是最后一个点
                    {
                        fzLine = new FZLine(fzPoints[j].Name, fzPoints[0].Name);//则最后一点与第一点连线
                        fzLines.Add(fzLine);
                    }
                    else
                    {
                        fzLine = new FZLine(fzPoints[j].Name, fzPoints[j+1].Name);//当前点与下一点连线
                        fzLines.Add(fzLine);
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
            //lines = new List<ILine>();
            //for (int j = 1; j < fzPoints.Count; j++)
            //{
            //    if (j == fzPoints.Count - 1)
            //    {
            //        line = new Line();
            //        line.FromPoint = fzPoints[j].Point;
            //        line.ToPoint = fzPoints[0].Point;
            //        lines.Add(line);
            //    }
            //    else
            //    {
            //        line = new Line();
            //        line.FromPoint = fzPoints[j - 1].Point;
            //        line.ToPoint = fzPoints[j].Point;
            //        lines.Add(line);
            //    }
                               
            //}

               
        }
    }
}
