using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPipeCreateTool.ParamModeling
{
    public class PipeSection : Section, IPipeSection, ISection
    {
        // Fields
        private double _height;          // 管线管径高度
        private HorizontalPos _horPos;   // 水平偏移类型
        private double _thick;           // 管线厚度
        private VerticalPos _vtPos;      // 垂直偏移类型
        private double _width;           // 管线管径宽度

        // Methods
        #region 管线截面构造函数
        public PipeSection(IPolygon SecGeometry, double Thick)
            : base(SecShape.PolygonRing)
        {
            this._horPos = HorizontalPos.Center;
            this._vtPos = VerticalPos.Center;
            base._offX = 0.0;
            base._offY = 0.0;
        }
        public PipeSection(double width, double height, HorizontalPos horPos, VerticalPos vtPos, double thick)
            : base(SecShape.RectangleRing)
        {
            this._width = width;
            this._height = height;
            this._thick = thick;
            this._horPos = horPos;
            this._vtPos = vtPos;
            base._segCount = 4;
            base._offX = base.HorOffset(this._horPos, this._width);
            base._offY = base.VtOffset(this._vtPos, this._height);
            base._vtxs = new double[(base._segCount + 1) * 4];
            base._vtxs[0] = this._width / 2.0;
            base._vtxs[1] = this._height / 2.0;
            base._vtxs[2] = (this._width / 2.0) - this._thick;
            base._vtxs[3] = (this._height / 2.0) - this._thick;
            base._vtxs[4] = this._width / 2.0;
            base._vtxs[5] = -(this._height / 2.0);
            base._vtxs[6] = base._offX + ((this._width / 2.0) - this._thick);
            base._vtxs[7] = -((this._height / 2.0) - this._thick);
            base._vtxs[8] = -(this._width / 2.0);
            base._vtxs[9] = -(this._height / 2.0);
            base._vtxs[10] = -((this._width / 2.0) - this._thick);
            base._vtxs[11] = -((this._height / 2.0) - this._thick);
            base._vtxs[12] = -(this._width / 2.0);
            base._vtxs[13] = this._height / 2.0;
            base._vtxs[14] = -((this._width / 2.0) - this._thick);
            base._vtxs[15] = (this._height / 2.0) - this._thick;
            base._vtxs[0x10] = this._width / 2.0;
            base._vtxs[0x11] = this._height / 2.0;
            base._vtxs[0x12] = (this._width / 2.0) - this._thick;
            base._vtxs[0x13] = (this._height / 2.0) - this._thick;
        }
        public PipeSection(double dia, int segCount, HorizontalPos horPos, VerticalPos vtPos, double thick)
            : base(SecShape.CircleRing)
        {
            this._width = dia;
            this._height = 0.0;
            this._thick = thick;
            this._horPos = horPos;
            this._vtPos = vtPos;
            base._segCount = segCount;
            base._offX = base.HorOffset(this._horPos, this._width);
            base._offY = base.VtOffset(this._vtPos, this._width);
            double num = 6.2831853071795862;
            double num2 = 6.2831853071795862 / ((double)base.SegCount);
            base._vtxs = new double[(base._segCount + 1) * 4];
            for (int i = 0; i <= base._segCount; i++)
            {
                base._vtxs[i * 4] = Math.Cos(num - (i * num2)) * (this._width / 2.0);
                base._vtxs[(i * 4) + 1] = Math.Sin(num - (i * num2)) * (this._width / 2.0);
                base._vtxs[(i * 4) + 2] = Math.Cos(num - (i * num2)) * ((this._width / 2.0) - this._thick);
                base._vtxs[(i * 4) + 3] = Math.Sin(num - (i * num2)) * ((this._width / 2.0) - this._thick);
            }
        }
        public PipeSection(double width, double height, HorizontalPos horPos, VerticalPos vtPos, double thick, int flag)
        {
            this._width = width;
            this._height = height;
            this._thick = thick;
            this._horPos = horPos;
            this._vtPos = vtPos;
            if (height == 0.0)
            {
                // 根据管径分配片面数  FX 2014.09.18
                //base._segCount = DrawGeometry.PipeSegments;
                //base._segCount = Int32.Parse(Config.GetConfigValue("PipeSegCount"));

                //if (width <= 0.05)
                //{
                //    base._segCount = 3;
                //}
                //else if (0.05 < width && width <= 0.1)
                //{
                //    base._segCount = 5;
                //}
                //else if (0.1 < width && width <= 1)
                //{
                //    base._segCount = 6;
                //}
                //else if (width > 1)
                //{
                //    base._segCount = 8;
                //}
                if (width <= 0.1)
                {
                    base._segCount = 5;
                }
                else if (0.1 < width && width <= 1)
                {
                    base._segCount = 6;
                }
                else if (width > 1)
                {
                    base._segCount = 8;
                }

                base._secShape = SecShape.CircleRing;
                base._offX = base.HorOffset(this._horPos, this._width);
                base._offY = base.VtOffset(this._vtPos, this._width);
                double num = 6.2831853071795862;
                double num2 = 6.2831853071795862 / ((double)base.SegCount);
                base._vtxs = new double[(base._segCount + 1) * 4];
                for (int i = 0; i <= base._segCount; i++)
                {
                    base._vtxs[i * 4] = Math.Cos(num - (i * num2)) * (this._width / 2.0);
                    base._vtxs[(i * 4) + 1] = Math.Sin(num - (i * num2)) * (this._width / 2.0);
                    base._vtxs[(i * 4) + 2] = Math.Cos(num - (i * num2)) * ((this._width / 2.0) - this._thick);
                    base._vtxs[(i * 4) + 3] = Math.Sin(num - (i * num2)) * ((this._width / 2.0) - this._thick);
                }
            }
            else
            {
                base._segCount = 4;
                base._secShape = SecShape.RectangleRing;
                base._offX = base.HorOffset(this._horPos, this._width);
                base._offY = base.VtOffset(this._vtPos, this._height);
                base._vtxs = new double[(base._segCount + 1) * 4];
                base._vtxs[0] = this._width / 2.0;
                base._vtxs[1] = this._height / 2.0;
                base._vtxs[2] = (this._width / 2.0) - this._thick;
                base._vtxs[3] = (this._height / 2.0) - this._thick;
                base._vtxs[4] = this._width / 2.0;
                base._vtxs[5] = -(this._height / 2.0);
                base._vtxs[6] = base._offX + ((this._width / 2.0) - this._thick);
                base._vtxs[7] = -((this._height / 2.0) - this._thick);
                base._vtxs[8] = -(this._width / 2.0);
                base._vtxs[9] = -(this._height / 2.0);
                base._vtxs[10] = -((this._width / 2.0) - this._thick);
                base._vtxs[11] = -((this._height / 2.0) - this._thick);
                base._vtxs[12] = -(this._width / 2.0);
                base._vtxs[13] = this._height / 2.0;
                base._vtxs[14] = -((this._width / 2.0) - this._thick);
                base._vtxs[15] = (this._height / 2.0) - this._thick;
                base._vtxs[0x10] = this._width / 2.0;
                base._vtxs[0x11] = this._height / 2.0;
                base._vtxs[0x12] = (this._width / 2.0) - this._thick;
                base._vtxs[0x13] = (this._height / 2.0) - this._thick;
            }
        }
        #endregion

        public static CompareRlt Compare(IPipeSection section1, IPipeSection section2)
        {
            if ((section1.SecShape == section2.SecShape) && (section1.Diameter == section2.Diameter))
            {
                return (CompareRlt.SameSize | CompareRlt.SameShape);
            }
            if ((section1.SecShape == section2.SecShape) && (section1.Diameter != section2.Diameter))
            {
                return CompareRlt.SameShape;
            }
            return CompareRlt.SameNot;
        }
        private double ConvertOffX(double offX)
        {
            switch (this._horPos)
            {
                case HorizontalPos.Left:
                    return offX;

                case HorizontalPos.Center:
                    return 0.0;

                case HorizontalPos.Right:
                    return -offX;
            }
            return 0.0;
        }
        private double ConvertOffY(double offY)
        {
            switch (this._vtPos)
            {
                case VerticalPos.Top:
                    return offY;

                case VerticalPos.Center:
                    return 0.0;

                case VerticalPos.Bottom:
                    return -offY;
            }
            return 0.0;
        }
        /// <summary>
        /// 获取简模断面顶点
        /// </summary>
        /// <returns></returns>
        public double[] GetSimpleVtxs()
        {
            switch (base._secShape)
            {
                case SecShape.CircleRing:
                    {
                        double num = 6.2831853071795862 / ((double)DrawGeometry.SimpleSegments);
                        double[] numArray = new double[(DrawGeometry.SimpleSegments + 1) * 4];
                        for (int i = 0; i <= DrawGeometry.SimpleSegments; i++)
                        {
                            numArray[i * 4] = base._offX + (Math.Sin(i * num) * (this._width / 1.9));
                            numArray[(i * 4) + 1] = base._offY + (Math.Cos(i * num) * (this._width / 1.9));
                            numArray[(i * 4) + 2] = base._offX + (Math.Sin(i * num) * ((this._width - this._thick) / 1.9));
                            numArray[(i * 4) + 3] = base._offY + (Math.Cos(i * num) * ((this._width - this._thick) / 1.9));
                        }
                        return numArray;
                    }
                case SecShape.RectangleRing:
                case SecShape.PolygonRing:
                    return this.GetVtxs();
            }
            return null;
        }

        #region 获取断面顶点
        public override double[] GetVtxs()
        {
            if ((base._vtxs == null) || (base._vtxs.Length == 0))
            {
                return null;
            }
            double[] numArray = new double[base._vtxs.Length];
            for (int i = 0; i < base._vtxs.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    numArray[i] = base._vtxs[i] + base._offX;
                }
                else
                {
                    numArray[i] = base._vtxs[i] + base._offY;
                }
            }
            return numArray;
        }
        public override double[] GetVtxs(double rate, double offX, double offY)
        {
            if ((base._vtxs == null) || (base._vtxs.Length == 0))
            {
                return null;
            }
            double[] numArray = new double[base._vtxs.Length];
            for (int i = 0; i < base._vtxs.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    numArray[i] = ((base._vtxs[i] + base._offX) * rate) + this.ConvertOffX(offX / 2.0);
                }
                else
                {
                    numArray[i] = ((base._vtxs[i] + base._offY) * rate) + this.ConvertOffY(offY / 2.0);
                }
            }
            return numArray;
        }
        #endregion

        public static int MaxDouble(double[] values)
        {
            if ((values == null) || (values.Length == 0))
            {
                return -1;
            }
            int num = 0;
            double num2 = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (num2 < values[i])
                {
                    num2 = values[i];
                    num = i;
                }
            }
            return num;
        }

        #region 最大断面
        public static int MaxSection(IPipeSection[] sections)
        {
            if ((sections == null) || (sections.Length == 0))
            {
                return -1;
            }
            int num = 0;
            double diameter = sections[0].Diameter;
            for (int i = 1; i < sections.Length; i++)
            {
                if (diameter < sections[i].Diameter)
                {
                    diameter = sections[i].Diameter;
                    num = i;
                }
            }
            return num;
        }
        #endregion

        public static int MinSection(IPipeSection[] sections)
        {
            if ((sections == null) || (sections.Length == 0))
            {
                return -1;
            }
            int num = 0;
            double diameter = sections[0].Diameter;
            for (int i = 1; i < sections.Length; i++)
            {
                if (diameter > sections[i].Diameter)
                {
                    diameter = sections[i].Diameter;
                    num = i;
                }
            }
            return num;
        }
        public static bool SetConnectIndex(IPipeSection[] sections, Vector[][] vtx, out int[] index)
        {
            index = null;
            if (((sections == null) || (sections.Length != vtx.Length)) || (sections.Length < 3))
            {
                return false;
            }
            if (sections.Length != 3)
            {
                if (MaxSection(sections) == MinSection(sections))
                {
                    int num6 = 0;
                    int num7 = 1;
                    double num9 = 0.0;
                    Vector[] vectorArray2 = new Vector[vtx.Length];
                    for (int m = 0; m < vtx.Length; m++)
                    {
                        vectorArray2[m] = vtx[m][vtx[m].Length - 1] - vtx[m][0];
                    }
                    for (int n = 1; n < vectorArray2.Length; n++)
                    {
                        double num8;
                        if (num9 < (num8 = Vector.CalcAngle(vectorArray2[0], vectorArray2[n])))
                        {
                            num9 = num8;
                            num7 = n;
                        }
                    }
                    index = new int[vectorArray2.Length];
                    index[num6++] = 0;
                    index[num6++] = num7;
                    for (int num12 = 0; num12 < vectorArray2.Length; num12++)
                    {
                        if ((num12 != 0) && (num12 != num7))
                        {
                            index[num6++] = num12;
                        }
                    }
                    return true;
                }
            }
            else if ((sections[0].Diameter == sections[1].Diameter) && (sections[0].Diameter == sections[2].Diameter))
            {
                Vector[] vectorArray = new Vector[vtx.Length];
                for (int num5 = 0; num5 < vtx.Length; num5++)
                {
                    vectorArray[num5] = vtx[num5][vtx[num5].Length - 1] - vtx[num5][0];
                }
                double num2 = Vector.CalcAngle(vectorArray[0], vectorArray[1]);
                double num3 = Vector.CalcAngle(vectorArray[0], vectorArray[2]);
                double num4 = Vector.CalcAngle(vectorArray[1], vectorArray[2]);
                switch (MaxDouble(new double[] { num2, num3, num4 }))
                {
                    case 0:
                        {
                            int[] numArray2 = new int[3];
                            numArray2[1] = 1;
                            numArray2[2] = 2;
                            index = numArray2;
                            break;
                        }
                    case 1:
                        {
                            int[] numArray3 = new int[3];
                            numArray3[1] = 2;
                            numArray3[2] = 1;
                            index = numArray3;
                            break;
                        }
                    case 2:
                        {
                            int[] numArray4 = new int[3];
                            numArray4[0] = 1;
                            numArray4[1] = 2;
                            index = numArray4;
                            break;
                        }
                }
                return true;
            }
            int num14 = 0;
            double diameter = -999999.99;
            for (int i = 0; i < sections.Length; i++)
            {
                if (diameter < sections[i].Diameter)
                {
                    diameter = sections[i].Diameter;
                    num14 = i;
                }
            }
            int num15 = 0;
            diameter = -999999.99;
            for (int j = 0; j < sections.Length; j++)
            {
                if ((j != num14) && (diameter < sections[j].Diameter))
                {
                    diameter = sections[j].Diameter;
                    num15 = j;
                }
            }
            int num18 = 2;
            index = new int[sections.Length];
            index[0] = num14;
            index[1] = num15;
            for (int k = 0; k < sections.Length; k++)
            {
                if ((k != num14) && (k != num15))
                {
                    index[num18++] = k;
                }
            }
            return true;
        }

        // Properties
        public double Diameter
        {
            get
            {
                return Math.Max(this._width, this._height);
            }
        }
        public double Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }
        public HorizontalPos HorPos
        {
            get
            {
                return this._horPos;
            }
            set
            {
                this._horPos = value;
            }
        }
        public double Thick
        {
            get
            {
                return this._thick;
            }
        }
        public VerticalPos VtPos
        {
            get
            {
                return this._vtPos;
            }
            set
            {
                this._vtPos = value;
            }
        }
        public double Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }
    }
}
