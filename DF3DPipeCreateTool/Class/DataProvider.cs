using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPipeCreateTool.Class
{
    public class DataProvider
    {
        // Fields
        private Dictionary<Guid, Dictionary<string, IFeatureDataSet>> _dicDsCache = new Dictionary<Guid, Dictionary<string, IFeatureDataSet>>();
        private DataObject.Finder _finder = new DataObject.Finder();
        private static DataProvider _instance;
        private List<DataObject> _lst = new List<DataObject>();

        // Methods
        private DataProvider()
        {
        }

        public void Clear()
        {
            try
            {
                foreach (KeyValuePair<Guid, Dictionary<string, IFeatureDataSet>> pair in this._dicDsCache)
                {
                    Dictionary<string, IFeatureDataSet> dictionary = pair.Value;
                    if ((dictionary != null) && (dictionary.Count > 0))
                    {
                        foreach (KeyValuePair<string, IFeatureDataSet> pair2 in dictionary)
                        {
                            Marshal.ReleaseComObject(pair2.Value);
                        }
                        dictionary.Clear();
                    }
                    dictionary.Clear();
                }
                this._dicDsCache.Clear();
                foreach (DataObject obj2 in this._lst)
                {
                    obj2.Release();
                }
                this._lst.Clear();
            }
            catch (Exception exception)
            {
            }
        }

        public void ClearDataSource(IDataSource ds)
        {
            List<DataObject> list = null;
            if (ds != null)
            {
                try
                {
                    if (this._dicDsCache.ContainsKey(ds.Guid))
                    {
                        Dictionary<string, IFeatureDataSet> dictionary = this._dicDsCache[ds.Guid];
                        if ((dictionary != null) && (dictionary.Count > 0))
                        {
                            foreach (KeyValuePair<string, IFeatureDataSet> pair in dictionary)
                            {
                                Marshal.ReleaseComObject(pair.Value);
                            }
                            dictionary.Clear();
                        }
                        this._dicDsCache.Remove(ds.Guid);
                    }
                    this._finder.SetDataSourceId(ds.Guid);
                    list = this._lst.FindAll(new Predicate<DataObject>(this._finder.FindByDataSourceId));
                    if ((list != null) && (list.Count != 0))
                    {
                        foreach (DataObject obj2 in list)
                        {
                            obj2.Release();
                            this._lst.Remove(obj2);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        public void DeleteFeatureClass(Guid fcGuid)
        {
            List<DataObject> list = null;
            try
            {
                this._finder.SetGuid(fcGuid);
                list = this._lst.FindAll(new Predicate<DataObject>(this._finder.FindByGuid));
                if ((list != null) && (list.Count != 0))
                {
                    foreach (DataObject obj2 in list)
                    {
                        obj2.Release();
                        this._lst.Remove(obj2);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        public IFeatureClass GetFeatureClass(Guid guid)
        {
            IFeatureClass class2 = null;
            List<DataObject> list = null;
            if (Guid.Empty == guid)
            {
                return class2;
            }
            try
            {
                this._finder.SetGuid(guid);
                list = this._lst.FindAll(new Predicate<DataObject>(this._finder.FindByGuid));
                if ((list == null) || (list.Count == 0))
                {
                    return class2;
                }
                return (list[0].GetData() as IFeatureClass);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public IObjectClass GetObjectClass(Guid guid)
        {
            IObjectClass class2 = null;
            List<DataObject> list = null;
            if (Guid.Empty == guid)
            {
                return class2;
            }
            try
            {
                this._finder.SetGuid(guid);
                list = this._lst.FindAll(new Predicate<DataObject>(this._finder.FindByGuid));
                if ((list == null) || (list.Count == 0))
                {
                    return class2;
                }
                return list[0].GetData();
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        #region 创建要素类
        public int TryCeateFeatureClass(IFeatureDataSet dataset, string Name, IFieldInfoCollection fields, string[] indexField, out IFeatureClass fc)
        {
            fc = null;
            if (((dataset == null) || string.IsNullOrEmpty(Name)) || (fields == null))
            {
                return -1;
            }
            try
            {
                string[] namesByType = dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if ((namesByType != null) && (Array.IndexOf<string>(namesByType, Name) != -1))
                {
                    fc = dataset.OpenFeatureClass(Name);
                    return 0;
                }
                fc = dataset.CreateFeatureClass(Name, fields);
                if (dataset == null)
                {
                    return -1;
                }
                IDbIndexInfo index = null;
                if (indexField != null)
                {
                    foreach (string str in indexField)
                    {
                        try
                        {
                            index = new DbIndexInfoClass
                            {
                                Name = string.Format("Index_{0}_{1}", fc.Id, str)
                            };
                            index.AppendFieldDefine(str, true);
                            fc.AddDbIndex(index);
                        }
                        catch (Exception exception)
                        {
                        }
                    }
                }
                return 1;
            }
            catch (Exception exception2)
            {
                return -1;
            }
        }
        #endregion

        #region 创建数据集
        public int TryCeateFeatureDataSet(IDataSource ds, string Name, ISpatialCRS SpatialCRS, out IFeatureDataSet dataset)
        {
            dataset = null;
            if (((ds == null) || string.IsNullOrEmpty(Name)) || (SpatialCRS == null))
            {
                return -1;
            }
            try
            {
                string[] featureDatasetNames = ds.GetFeatureDatasetNames();
                if ((featureDatasetNames != null) && (Array.IndexOf<string>(featureDatasetNames, Name) != -1))
                {
                    dataset = ds.OpenFeatureDataset(Name);
                    return 0;
                }
                dataset = ds.CreateFeatureDataset(Name, SpatialCRS);
                if (dataset == null)
                {
                    return -1;
                }
                return 1;
            }
            catch (Exception exception)
            {
                return -1;
            }
        }
        #endregion

        #region 创建ObjectClass
        public int TryCeateObjectClass(IFeatureDataSet dataset, string Name, IFieldInfoCollection fields, string[] indexField, out IObjectClass oc)
        {
            oc = null;
            if (((dataset == null) || string.IsNullOrEmpty(Name)) || (fields == null))
            {
                return -1;
            }
            try
            {
                string[] namesByType = dataset.GetNamesByType(gviDataSetType.gviDataSetObjectClassTable);
                if ((namesByType != null) && (Array.IndexOf<string>(namesByType, Name) != -1))
                {
                    oc = dataset.OpenObjectClass(Name);
                    return 0;
                }
                oc = dataset.CreateObjectClass(Name, fields);
                if (dataset == null)
                {
                    return -1;
                }
                IDbIndexInfo index = null;
                if (indexField != null)
                {
                    foreach (string str in indexField)
                    {
                        try
                        {
                            index = new DbIndexInfoClass
                            {
                                Name = string.Format("Index_{0}_{1}", oc.Id, str)
                            };
                            index.AppendFieldDefine(str, true);
                            oc.AddDbIndex(index);
                        }
                        catch (Exception exception)
                        {
                        }
                    }
                }
                return 1;
            }
            catch (Exception exception2)
            {
                return -1;
            }
        }
        #endregion

        public bool TryOpenFeatureClass(IDataSource ds, string datasetName, string fcName, out IFeatureClass fc)
        {
            fc = null;
            List<DataObject> list = null;
            if (((ds == null) || string.IsNullOrEmpty(datasetName)) || string.IsNullOrEmpty(fcName))
            {
                return false;
            }
            try
            {
                this._finder.SetByName(ds.Guid, datasetName, fcName);
                list = this._lst.FindAll(new Predicate<DataObject>(this._finder.FinderByName));
                if ((list == null) || (list.Count == 0))
                {
                    IFeatureDataSet o = ds.OpenFeatureDataset(datasetName);
                    fc = o.OpenFeatureClass(fcName);
                    if (fc != null)
                    {
                        this._lst.Add(new DataObject(fc));
                    }
                    Marshal.ReleaseComObject(o);
                }
                else
                {
                    fc = list[0].GetData() as IFeatureClass;
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool TryOpenFeatureDataSet(IDataSource ds, string datasetName, out IFeatureDataSet dataset)
        {
            dataset = null;
            if (ds == null)
            {
                return false;
            }
            try
            {
                Dictionary<string, IFeatureDataSet> dictionary = null;
                if (this._dicDsCache.ContainsKey(ds.Guid))
                {
                    dictionary = this._dicDsCache[ds.Guid];
                    if ((dictionary != null) && dictionary.ContainsKey(datasetName))
                    {
                        dataset = dictionary[datasetName];
                        return true;
                    }
                }
                dataset = ds.OpenFeatureDataset(datasetName);
                if (dataset == null)
                {
                    return false;
                }
                if (dictionary == null)
                {
                    dictionary = new Dictionary<string, IFeatureDataSet>();
                    this._dicDsCache.Add(ds.Guid, dictionary);
                }
                dictionary.Add(datasetName, dataset);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool TryOpenObjectClass(IDataSource ds, string datasetName, string ocName, out IObjectClass oc)
        {
            oc = null;
            List<DataObject> list = null;
            if (((ds == null) || string.IsNullOrEmpty(datasetName)) || string.IsNullOrEmpty(ocName))
            {
                return false;
            }
            try
            {
                this._finder.SetByName(ds.Guid, datasetName, ocName);
                list = this._lst.FindAll(new Predicate<DataObject>(this._finder.FinderByName));
                if ((list == null) || (list.Count == 0))
                {
                    IFeatureDataSet o = ds.OpenFeatureDataset(datasetName);
                    oc = o.OpenObjectClass(ocName);
                    if (oc != null)
                    {
                        this._lst.Add(new DataObject(oc));
                    }
                    Marshal.ReleaseComObject(o);
                }
                else
                {
                    oc = list[0].GetData();
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        // Properties
        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProvider();
                }
                return _instance;
            }
        }
    }
}
