using System;
using System.Collections.Generic;
using System.Data;

namespace DF3DEdit.Class
{
    public class ResultSetInfo
    {
        public DataTable ResultSetTable;
        public int TotalCount;
        public System.Collections.Generic.List<int> OidList;

        public ResultSetInfo(DataTable dt, System.Collections.Generic.List<int> oidList)
        {
            this.ResultSetTable = dt;
            this.TotalCount = oidList.Count;
            this.OidList = oidList;
        }
    }
}
