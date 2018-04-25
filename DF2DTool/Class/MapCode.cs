using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DTool.Class
{
    /// <summary>
    /// 图幅编号及接图表编号自动生成的类
    /// </summary>
    public class MapCode
    {
        private string seprate = "-";
        /// <summary>
        /// 图幅编号中的分隔符
        /// </summary>
        public string Seprate
        {
            get
            {
                return seprate;
            }
            set
            {
                seprate = value;
            }
        }
        private int x = 0;
        /// <summary>
        /// 西南角的X坐标
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public string XString
        {
            get
            {
                return this.CoordToString(X);
            }
        }

        public string YString
        {
            get
            {
                return this.CoordToString(Y);
            }
        }
        /// <summary>
        /// 根据比例尺将坐标转换为字符串
        /// </summary>
        /// <param name="coord">坐标值</param>
        /// <returns>字符串</returns>
        public string CoordToString(int coord)
        {
            switch (this.Scale)
            {
                case 500:
                    return (coord / 1000.0).ToString("F2");
                case 1000:
                    return (coord / 1000.0).ToString("F1");
                case 2000:
                    return (coord / 1000.0).ToString("F0");
                case 5000:
                    return (coord / 1000.0).ToString("F0");
                case 10000:
                    return (coord / 1000.0).ToString("F0");
                default:
                    return (coord / 1000.0).ToString("F2");
            }
        }
        private int y = 0;
        /// <summary>
        /// 西南角的Y坐标
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                y = value;
            }
        }

        private int scale = 0;
        /// <summary>
        /// 比例尺
        /// </summary>
        public int Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        private int xCellCount;
        /// <summary>
        /// 图幅宽度
        /// </summary>
        public int XCellCount
        {
            get
            {
                return xCellCount;
            }
            set
            {
                xCellCount = value;
            }
        }
        private int yCellCount;
        /// <summary>
        /// 图幅高度
        /// </summary>
        public int YCellCount
        {
            get
            {
                return yCellCount;
            }
            set
            {
                yCellCount = value;
            }
        }
        /// <summary>
        /// X最小值
        /// </summary>
        public int XMin
        {
            get
            {
                return this.X;
            }
        }
        /// <summary>
        /// Y最小值
        /// </summary>
        public int YMin
        {
            get
            {
                return this.Y;
            }
        }
        /// <summary>
        /// X最大值
        /// </summary>
        public int XMax
        {
            get
            {
                return this.GetJoin(1).X;
            }
        }
        /// <summary>
        /// Y最大值
        /// </summary>
        public int YMax
        {
            get
            {
                return this.GetJoin(3).Y;
            }
        }
        public override string ToString()
        {
            return this.XString + this.Seprate + this.YString;

        }
        /// <summary>
        /// 根据比例尺计算和图幅西南角（左下角）的X、Y坐标计算图幅编号
        /// </summary>
        /// <param name="scale">1：500的比例尺的scale值为500</param>
        /// <param name="X">X坐标的值，单位为米</param>
        /// <param name="Y">Y坐标的值，单位为米</param>
        public string GenerateMapCode(int pScale, int pX, int pY, int pXCellCount, int pYCellCount)
        {
            this.Scale = pScale;
            this.X = pX;
            this.Y = pY;
            this.XCellCount = pXCellCount;
            this.YCellCount = pYCellCount;
            return this.ToString();
        }
        /// <summary>
        /// index的值：当前图幅为0，其余各接边图按从上到下，从左到右的顺序编号
        /// </summary>
        public string this[int index]
        {
            get
            {
                return this.GetJoin(index).ToString();
            }
        }
        /// <summary>
        /// 获得接图表的编号
        /// </summary>
        /// <param name="index">接图表的索引</param>
        /// <returns>编号</returns>
        public MapCode GetJoin(int index)
        {
            int incrementX = this.Scale / 10 * this.XCellCount;
            int incrementY = this.Scale / 10 * this.YCellCount;

            MapCode temp = new MapCode();
            switch (index)
            {
                case 0:
                    return this;
                case 1:
                    temp.Scale = this.Scale;
                    temp.X = this.X + incrementX;
                    temp.Y = this.Y - incrementY;
                    return temp;
                case 2:
                    temp.Scale = this.Scale;
                    temp.X = this.X + incrementX;
                    temp.Y = this.Y;
                    return temp;
                case 3:
                    temp.Scale = this.Scale;
                    temp.X = this.X + incrementX;
                    temp.Y = this.Y + incrementY;
                    return temp;
                case 4:
                    temp.Scale = this.Scale;
                    temp.X = this.X;
                    temp.Y = this.Y - incrementY;
                    return temp;
                case 5:
                    temp.Scale = this.Scale;
                    temp.X = this.X;
                    temp.Y = this.Y + incrementY;
                    return temp;
                case 6:
                    temp.Scale = this.Scale;
                    temp.X = this.X - incrementX;
                    temp.Y = this.Y - incrementY;
                    return temp;
                case 7:
                    temp.Scale = this.Scale;
                    temp.X = this.X - incrementX;
                    temp.Y = this.Y;
                    return temp;
                case 8:
                    temp.Scale = this.Scale;
                    temp.X = this.X - incrementX;
                    temp.Y = this.Y + incrementY;
                    return temp;
                default:
                    return null;
            }
        }



    }
}
