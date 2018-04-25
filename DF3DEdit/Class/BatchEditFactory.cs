using System;
using DF3DEdit.Interface;

namespace DF3DEdit.Class
{
    public class BatchEditFactory
    {
        public static IBatcheEdit CreateBatchEdit(int nCount)
        {
            if (nCount <= 300)
            {
                return new SmallDataEdit();
            }
            if (nCount <= 1000)
            {
                return new MiddleDataEdit();
            }
            return new LarageDataEdit();
        }
    }
}
