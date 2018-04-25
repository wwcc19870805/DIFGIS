using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DMDBConvert.Class
{
    public class TableField
    {
        protected string gisName;
        protected string cadName;
        protected string dataType;

        public string GisName
        {
            get { return this.gisName; }
            set { this.gisName = value; }
        }
        public string CadName
        {
            get { return this.cadName; }
            set { this.cadName = value; }
        }
        public string DataType
        {
            get { return this.dataType; }
            set { this.dataType = value; }
        }

        public TableField(string gisName = "", string cadName = "", string dataType = "")
        {
            this.gisName = gisName;
            this.cadName = cadName;
            this.dataType = dataType;
        }

    }
}
