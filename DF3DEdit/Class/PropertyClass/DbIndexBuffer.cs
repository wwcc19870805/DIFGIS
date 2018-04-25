using Gvitech.CityMaker.FdeCore;
using System;

namespace DF3DEdit.Class
{
    public class DbIndexBuffer : IndexBuffer
    {
        public IDbIndexInfo data;
        public DbIndexBuffer()
        {
        }
        public DbIndexBuffer(IDbIndexInfo info, IndexEditType type)
        {
            this.data = info;
            this.et = type;
        }
    }
}
