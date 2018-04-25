using Gvitech.CityMaker.FdeCore;
using System;

namespace DF3DEdit.Class
{
    public class GridIndexBuffer : IndexBuffer
    {
        public IGridIndexInfo data;
        public GridIndexBuffer()
        {
        }
        public GridIndexBuffer(IGridIndexInfo info, IndexEditType type)
        {
            this.data = info;
            this.et = type;
        }
    }
}
