using Gvitech.CityMaker.FdeCore;
using System;

namespace DF3DEdit.Class
{
    public class RenderIndexBuffer : IndexBuffer
    {
        public IRenderIndexInfo data;
        public RenderIndexBuffer()
        {
        }
        public RenderIndexBuffer(IRenderIndexInfo info, IndexEditType type)
        {
            this.data = info;
            this.et = type;
        }
    }
}
