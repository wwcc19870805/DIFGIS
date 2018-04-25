using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace DF2DTool.Class
{
    public class DataSetNames
    {


        public int Count { get; set; }

        private List<IDatasetName> alist = new List<IDatasetName>();
        public void AddItem(IDatasetName dataSetName)
        {
            alist.Add(dataSetName);
        }


        public IDatasetName get_Item(int index)
        {
            if (index < 0 || index >= alist.Count) return null;
            return alist[index];
        }

        public void RemoveItem(int index)
        {
            if (index < 0 || index >= alist.Count) return ;
            alist.RemoveAt(index);
        }
    }
}
