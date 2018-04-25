using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DPipeCreateTool.Class;

namespace DF3DPipeCreateTool.ParamModeling
{
    public class Section : ISection
    {
        // Fields
        protected double _offX;        // 水平偏移
        protected double _offY;        // 垂直偏移
        protected SecShape _secShape;  // 断面形状
        protected int _segCount;       // 截面数
        protected double[] _vtxs;      // 断面顶点

        // Methods
        public Section()
        {
        }
        public Section(SecShape secShape)
        {
            this._secShape = secShape;
        }
        public virtual double[] GetVtxs()
        {
            return this._vtxs;
        }
        public virtual double[] GetVtxs(double rate, double offX, double offY)
        {
            if ((this._vtxs == null) || (this._vtxs.Length == 0))
            {
                return null;
            }
            double[] numArray = new double[this._vtxs.Length];
            for (int i = 0; i < this._vtxs.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    numArray[i] = (this._vtxs[i] * rate) + offX;
                }
                else
                {
                    numArray[i] = (this._vtxs[i] * rate) + offY;
                }
            }
            return numArray;
        }
        /// <summary>
        /// 计算水平偏移
        /// </summary>
        /// <param name="horPos">水平偏移参数</param>
        /// <param name="width">宽度</param>
        /// <returns></returns>
        protected double HorOffset(HorizontalPos horPos, double width)
        {
            switch (horPos)
            {
                case HorizontalPos.Left:
                    return (0.5 * width);

                case HorizontalPos.Right:
                    return (-0.5 * width);
            }
            return 0.0;
        }
        /// <summary>
        /// 计算垂直偏移
        /// </summary>
        /// <param name="vtPos"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        protected double VtOffset(VerticalPos vtPos, double height)
        {
            switch (vtPos)
            {
                case VerticalPos.Top:
                    return (-0.5 * height);

                case VerticalPos.Bottom:
                    return (0.5 * height);
            }
            return 0.0;
        }

        // Properties
        public double OffsetX
        {
            get
            {
                return this._offX;
            }
            set
            {
                this._offX = value;
            }
        }
        public double OffsetY
        {
            get
            {
                return this._offY;
            }
            set
            {
                this._offY = value;
            }
        }
        public SecShape SecShape
        {
            get
            {
                return this._secShape;
            }
        }
        public int SegCount
        {
            get
            {
                return this._segCount;
            }
        }
    }
}
