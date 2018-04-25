using DevExpress.XtraEditors;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeDataInterop;
using Gvitech.CityMaker.Resource;
using ICSharpCode.Core;
using DF3DEdit.Frm;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

namespace DF3DEdit.Class
{
    public class FDEProcess
    {
        public IFeatureDataSet _dataset;
        public IFeatureClass _fc;
        public IObjectClass _oc;
        public static string http = StringParser.Parse("${res:View_Type_Http}");
        public static string https = StringParser.Parse("${res:View_Type_Https}");
        private DataSourceFactory dsf;
        private IConnectionInfo info;
        public IConnectionInfo INFO
        {
            get
            {
                return this.info;
            }
        }
        public FDEProcess()
        {
            this.dsf = new DataSourceFactoryClass();
            this.info = new ConnectionInfoClass();
        }
        public bool CanOpen(string dsrc, IConnectionInfo info)
        {
            bool result;
            try
            {
                IDataSource dataSource = this.dsf.OpenDataSource(info);
                if (dataSource != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (System.Exception)
            {
                result = false;
            }
            return result;
        }
        public IConnectionInfo GetConnectionInfo(string cnnType, string oracleInstance, string database, string server, uint port, string user, string pwd)
        {
            IConnectionInfo connectionInfo = new ConnectionInfoClass();
            IConnectionInfo result;
            try
            {
                if (cnnType == "MySql")
                {
                    connectionInfo.ConnectionType = gviConnectionType.gviConnectionMySql5x;
                }
                else
                {
                    if (cnnType == "Microsoft SQLServer")
                    {
                        connectionInfo.ConnectionType = gviConnectionType.gviConnectionMSClient;
                        connectionInfo.Instance = oracleInstance;
                    }
                    else
                    {
                        if (cnnType == "File")
                        {
                            connectionInfo.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                        }
                        else
                        {
                            if (cnnType == "SQLite")
                            {
                                connectionInfo.ConnectionType = gviConnectionType.gviConnectionSQLite3;
                            }
                            else
                            {
                                if (cnnType == "Oracle")
                                {
                                    connectionInfo.ConnectionType = gviConnectionType.gviConnectionOCI11;
                                    connectionInfo.Instance = oracleInstance;
                                }
                                else
                                {
                                    if (cnnType == FDEProcess.http)
                                    {
                                        connectionInfo.ConnectionType = gviConnectionType.gviConnectionCms7Http;
                                    }
                                    else
                                    {
                                        if (cnnType == FDEProcess.http)
                                        {
                                            connectionInfo.ConnectionType = gviConnectionType.gviConnectionCms7Https;
                                        }
                                        else
                                        {
                                            connectionInfo.ConnectionType = gviConnectionType.gviConnectionUnknown;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (database != null)
                {
                    connectionInfo.Database = database;
                }
                if (server != null)
                {
                    connectionInfo.Server = server;
                }
                connectionInfo.Port = port;
                if (user != null)
                {
                    connectionInfo.UserName = user;
                }
                if (pwd != null)
                {
                    connectionInfo.Password = pwd;
                }
                result = connectionInfo;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                result = connectionInfo;
            }
            return result;
        }
        public string[] GetConnectionState(string cnnType, string oracleInstance, string server, uint port, string user, string pwd, bool hidenSysTable)
        {
            return this.dsf.GetDataBaseNames(this.GetConnectionInfo(cnnType, oracleInstance, null, server, port, user, pwd), hidenSysTable);
        }
        public IDataSource OpenDataSourceWithoutException(string cnnStr)
        {
            IDataSource result;
            try
            {
                IDataSource dataSource = this.dsf.OpenDataSourceByString(cnnStr);
                if (dataSource != null)
                {
                    result = dataSource;
                }
                else
                {
                    result = null;
                }
            }
            catch (System.Exception)
            {
                result = null;
            }
            return result;
        }
        public IDataSource OpenDataSourceByString(IConnectionInfo cnninfo)
        {
            IDataSource result;
            try
            {
                if (!this.dsf.HasDataSource(cnninfo))
                {
                    System.Exception ex = new System.Exception(StringParser.Parse("${res:View_DataSourceNotExist}"));
                    throw ex;
                }
                result = this.dsf.OpenDataSource(cnninfo);
            }
            catch (System.Runtime.InteropServices.COMException ex2)
            {
                throw ex2;
            }
            catch (System.Exception)
            {
                throw;
            }
            return result;
        }
        public IDataSource OpenDataSourceByString(string cnnStr)
        {
            IDataSource result;
            try
            {
                if (!this.dsf.HasDataSourceByString(cnnStr))
                {
                    System.Exception ex = new System.Exception(StringParser.Parse("${res:View_DataSourceNotExist}"));
                    throw ex;
                }
                IDataSource dataSource = this.dsf.OpenDataSourceByString(cnnStr);
                if (dataSource.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionCms7Http || dataSource.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionCms7Https)
                {
                    LoggingService.Info(string.Format("Time:{0};Message:{1}", System.DateTime.Now, "Server response in 20 seconds"));
                }
                result = dataSource;
            }
            catch (System.Runtime.InteropServices.COMException ex2)
            {
                throw ex2;
            }
            catch (System.Exception)
            {
                throw;
            }
            return result;
        }
        public IDataSource GetDataSourceFromCnnstring(string cnnStr)
        {
            IDataSource result;
            try
            {
                string[] array = cnnStr.Split(new char[]
				{
					';'
				});
                for (int i = 0; i < array.Length; i++)
                {
                    string[] array2 = array[i].Split(new char[]
					{
						'='
					});
                    array[i] = array2[1];
                }
                IConnectionInfo connectionInfo = new ConnectionInfoClass();
                string a;
                if ((a = array[0]) != null)
                {
                    if (a == "MySQL5")
                    {
                        connectionInfo.ConnectionType = gviConnectionType.gviConnectionMySql5x;
                        goto IL_A0;
                    }
                    if (a == "Microsoft SQLServer")
                    {
                        connectionInfo.ConnectionType = gviConnectionType.gviConnectionMSClient;
                        goto IL_A0;
                    }
                    if (a == "FireBird2")
                    {
                        connectionInfo.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                        goto IL_A0;
                    }
                }
                connectionInfo.ConnectionType = gviConnectionType.gviConnectionUnknown;
            IL_A0:
                if (array[1] == null)
                {
                    connectionInfo.Server = "";
                }
                else
                {
                    connectionInfo.Server = array[1];
                }
                connectionInfo.Port = uint.Parse(array[2]);
                connectionInfo.Database = array[3];
                connectionInfo.UserName = array[4];
                connectionInfo.Password = array[5];
                result = this.dsf.OpenDataSource(connectionInfo);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                result = null;
            }
            catch (System.Exception e)
            {
                LoggingService.Error(e.Message + "\r\n" + e.StackTrace);
                result = null;
            }
            return result;
        }
        public bool DeleteDataSource()
        {
            return false;
        }
        public IDataSource CreateDataSource(string type, string instance, string server, string database, uint port, string user, string pwd)
        {
            IDataSource result;
            try
            {
                if (type == "MySql")
                {
                    this.info.ConnectionType = gviConnectionType.gviConnectionMySql5x;
                }
                else
                {
                    if (type == "Microsoft SQLServer")
                    {
                        this.info.ConnectionType = gviConnectionType.gviConnectionMSClient;
                        this.info.Instance = instance;
                    }
                    else
                    {
                        if (type == "File")
                        {
                            this.info.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                            if (server == null)
                            {
                                server = "";
                            }
                        }
                        else
                        {
                            if (type == "Oracle")
                            {
                                this.info.ConnectionType = gviConnectionType.gviConnectionOCI11;
                                this.info.Instance = instance;
                            }
                            else
                            {
                                if (type == "SQLite")
                                {
                                    this.info.ConnectionType = gviConnectionType.gviConnectionSQLite3;
                                    if (server == null)
                                    {
                                        server = "";
                                    }
                                }
                                else
                                {
                                    if (type == "ArcSDE")
                                    {
                                        this.info.Instance = instance;
                                        this.info.ConnectionType = gviConnectionType.gviConnectionArcSDE10x;
                                    }
                                }
                            }
                        }
                    }
                }
                this.info.Server = server;
                this.info.Database = database;
                if (type != "SQLite" && type != "File")
                {
                    this.info.Port = port;
                }
                this.info.UserName = ((user == null) ? "" : user);
                this.info.Password = ((pwd == null) ? "" : pwd);
                if (this.dsf.HasDataSourceByString(this.info.ToConnectionString()))
                {
                    System.Exception ex = new System.Exception(StringParser.Parse("${res:View_DataSourceExist}"));
                    throw ex;
                }
                IDataSource dataSource = this.dsf.CreateDataSource(this.info, null);
                result = dataSource;
            }
            catch (System.Runtime.InteropServices.COMException ex2)
            {
                throw ex2;
            }
            catch (System.Exception)
            {
                throw;
            }
            return result;
        }
        public System.Collections.ArrayList GetCollectionofField(ITable table, string field)
        {
            IFdeCursor fdeCursor = null;
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            System.Collections.ArrayList result;
            try
            {
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.AddSubField(field);
                queryFilter.WhereClause = "1=1";
                fdeCursor = table.Search(queryFilter, false);
                IRowBuffer rowBuffer;
                while ((rowBuffer = fdeCursor.NextRow()) != null)
                {
                    int position = rowBuffer.FieldIndex("name");
                    string value = rowBuffer.GetValue(position).ToString();
                    arrayList.Add(value);
                }
                result = arrayList;
            }
            catch (System.Exception)
            {
                result = arrayList;
            }
            finally
            {
                if (fdeCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                    fdeCursor = null;
                }
            }
            return result;
        }
        public int GetFeatureCount(IFeatureDataSet fds, QueryFilter filter)
        {
            int num = 0;
            try
            {
                string[] namesByType = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (namesByType != null)
                {
                    string[] array = namesByType;
                    for (int i = 0; i < array.Length; i++)
                    {
                        string name = array[i];
                        IFeatureClass featureClass = fds.OpenFeatureClass(name);
                        if (featureClass != null)
                        {
                            num += featureClass.GetCount(filter);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                        }
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
            }
            catch (System.Exception e)
            {
                LoggingService.Error(e.Message + "\r\n" + e.StackTrace);
            }
            return num;
        }
        public bool InsertRow(IDataSource ds, string tname, object[] values)
        {
            ITable table = ds.OpenTable(tname);
            IFdeCursor fdeCursor = table.Insert();
            RowBufferFactory rowBufferFactory = new RowBufferFactoryClass();
            IRowBuffer rowBuffer = rowBufferFactory.CreateRowBuffer(table.GetFields());
            for (int i = 1; i <= values.Length; i++)
            {
                rowBuffer.SetValue(i, values[i]);
            }
            fdeCursor.InsertRow(rowBuffer);
            return true;
        }
        public System.Collections.Hashtable GetDomain(IFeatureDataSet fds, gviFieldType fieldType)
        {
            System.Collections.Hashtable result;
            try
            {
                IDataSource dataSource = fds.DataSource;
                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
                string[] domainNames = dataSource.GetDomainNames();
                for (int i = 0; i < domainNames.Length; i++)
                {
                    string text = domainNames[i];
                    IDomain domainByName = dataSource.GetDomainByName(text);
                    if (domainByName.FieldType == fieldType)
                    {
                        hashtable.Add(text, domainByName);
                    }
                }
                result = hashtable;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                result = null;
            }
            catch (System.Exception e)
            {
                LoggingService.Error(e.Message + "\r\n" + e.StackTrace);
                result = null;
            }
            return result;
        }
        public bool GetQueryResults(string whereclause, string tablename, IDataSource ds)
        {
            ITable table = null;
            QueryFilter queryFilter = null;
            IFdeCursor fdeCursor = null;
            bool result;
            try
            {
                table = ds.OpenTable(tablename);
                queryFilter = new QueryFilterClass();
                if (whereclause == null)
                {
                    queryFilter.WhereClause = "1=1";
                }
                else
                {
                    queryFilter.WhereClause = whereclause;
                }
                fdeCursor = table.Search(queryFilter, true);
                if (fdeCursor.NextRow() != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                throw;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                if (fdeCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                    fdeCursor = null;
                }
                if (queryFilter != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);
                    queryFilter = null;
                }
                if (table != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(table);
                    table = null;
                }
            }
            return result;
        }
        public bool CompareFeatureClass(IFieldInfoCollection fields1, IFieldInfoCollection fields2)
        {
            if (fields1.Count != fields2.Count)
            {
                return false;
            }
            for (int i = 0; i < fields1.Count; i++)
            {
                IFieldInfo fieldInfo = fields1.Get(i);
                int num = fields2.IndexOf(fieldInfo.Name);
                if (num == -1)
                {
                    return false;
                }
                if (!fieldInfo.Equal(fields2.Get(num)))
                {
                    return false;
                }
            }
            return true;
        }
        public void CompareFeatureDataSet(IFeatureDataSet target, IFeatureDataSet origin, out System.Collections.Hashtable fcmap)
        {
            fcmap = new System.Collections.Hashtable();
            IFeatureClass featureClass = null;
            IFeatureClass featureClass2 = null;
            try
            {
                string[] namesByType = target.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                string[] namesByType2 = origin.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                string[] array = namesByType2;
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    featureClass = origin.OpenFeatureClass(text);
                    if (namesByType != null)
                    {
                        string[] array2 = namesByType;
                        for (int j = 0; j < array2.Length; j++)
                        {
                            string text2 = array2[j];
                            if (text.Equals(text2))
                            {
                                featureClass2 = target.OpenFeatureClass(text2);
                                if (this.CompareFeatureClass(featureClass.GetFields(), featureClass2.GetFields()))
                                {
                                    fcmap.Add(text, text2);
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException)
            {
            }
            catch (System.Exception)
            {
            }
            finally
            {
                if (featureClass != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    featureClass = null;
                }
                if (featureClass2 != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass2);
                    featureClass2 = null;
                }
            }
        }
        private string GetNewFeatureClassName(string fn, string[] list)
        {
            while (this.CheckImportFCName(fn, list))
            {
                int postfix = this.GetPostfix(fn);
                string text = string.Format("({0})", postfix - 1);
                if (fn.LastIndexOf(text) == -1)
                {
                    fn = string.Format("{0}({1})", fn, postfix);
                }
                else
                {
                    fn = fn.Replace(text, string.Format("({0})", postfix));
                }
            }
            return fn;
        }
        private int GetPostfix(string fcname)
        {
            int num = fcname.LastIndexOf('(');
            int num2 = fcname.LastIndexOf(')');
            if (num == -1 || num2 == -1)
            {
                return 1;
            }
            string s = fcname.Substring(num + 1, num2 - num - 1);
            return int.Parse(s) + 1;
        }
        private bool CheckImportFCName(string fcname, string[] collection)
        {
            for (int i = 0; i < collection.Length; i++)
            {
                string value = collection[i];
                if (fcname.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
        public string GetConnectionString()
        {
            return "";
        }
        private System.Collections.ArrayList CheckModel(Gvitech.CityMaker.Resource.IModel model, string[] fine)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            for (int i = 0; i < fine.Length; i++)
            {
                string value = fine[i];
                arrayList.Add(value);
            }
            if (model != null)
            {
                string[] imageNames = model.GetImageNames();
                string[] array = imageNames;
                for (int j = 0; j < array.Length; j++)
                {
                    string value2 = array[j];
                    if (!fine.Contains(value2))
                    {
                        arrayList.Add(value2);
                    }
                }
            }
            return arrayList;
        }
        public bool CopyModelAndImage(MyProgressBar bar, IFeatureDataSet srcFds, IFeatureDataSet tarFds)
        {
            bool flag = true;
            int num = 1;
            try
            {
                tarFds.DataSource.StartEditing();
                IResourceManager resourceManager = srcFds as IResourceManager;
                IResourceManager resourceManager2 = tarFds as IResourceManager;
                EnumResName modelNames = resourceManager.GetModelNames();
                int modelCount = resourceManager.GetModelCount();
                while (modelNames.MoveNext())
                {
                    if (num % 100 == 0 || num == modelCount)
                    {
                        lock (bar)
                        {
                            if (bar.CallbackCancel)
                            {
                                bar.labelTooltip.Text = StringParser.Parse("${res:View_Cancling}");
                                bool result = false;
                                return result;
                            }
                        }
                    }
                    string current = modelNames.Current;
                    Gvitech.CityMaker.Resource.IModel model = resourceManager.GetModel(current);
                    Gvitech.CityMaker.Resource.IModel simplifiedModel = resourceManager.GetSimplifiedModel(current);
                    if (!resourceManager2.ModelExist(current))
                    {
                        resourceManager2.AddModel(current, model, simplifiedModel);
                        LoggingService.Debug(string.Format("Copy model:{0}", current));
                    }
                    num++;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(modelNames);
                EnumResName imageNames = resourceManager.GetImageNames();
                num = 1;
                resourceManager.GetImageCount();
                while (imageNames.MoveNext())
                {
                    if (num % 100 == 0 || num == modelCount)
                    {
                        lock (bar)
                        {
                            if (bar.CallbackCancel)
                            {
                                bar.labelTooltip.Text = StringParser.Parse("${res:View_Cancling}");
                                bool result = false;
                                return result;
                            }
                        }
                    }
                    string current2 = imageNames.Current;
                    IImage image = resourceManager.GetImage(current2);
                    if (image != null && !resourceManager2.ImageExist(current2))
                    {
                        resourceManager2.AddImage(current2, image);
                        LoggingService.Debug(string.Format("Copy model:{0}", current2));
                    }
                    num++;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(imageNames);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                flag = false;
            }
            catch (System.Exception ex2)
            {
                XtraMessageBox.Show(ex2.Message);
                flag = false;
            }
            finally
            {
                tarFds.DataSource.StopEditing(flag);
            }
            return flag;
        }
        public void GetObjectIds(IFeatureDataSet fds, IObjectClass oc)
        {
        }
        public int AppendByTable(ITable tar, ITable src, IPropertySet ps, IFeatureProgress fp)
        {
            int num = -1;
            IFdeCursor fdeCursor = null;
            IQueryFilter queryFilter = new QueryFilterClass();
            int result;
            try
            {
                IDataCopy dataCopy = new DataCopyClass();
                DataCopyParam dataCopyParam = new DataCopyParamClass();
                queryFilter.WhereClause = "1=1";
                fdeCursor = src.Search(queryFilter, false);
                dataCopyParam.SetFieldMapping(ps);
                dataCopyParam.Filter = queryFilter;
                dataCopyParam.KeepFid = true;
                dataCopyParam.ResourceConflictPolicy = gviResourceConflictPolicy.gviResourceRenameToNew;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                num = dataCopy.CopyTable(tar.DataSource, tar.TableName, src.DataSource, src.TableName, dataCopyParam);
                result = num;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                XtraMessageBox.Show(ex.Message);
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                result = num;
            }
            catch (System.Exception ex2)
            {
                XtraMessageBox.Show(ex2.Message);
                LoggingService.Error(ex2.Message + "\r\n" + ex2.StackTrace);
                result = num;
            }
            finally
            {
                if (fdeCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                    fdeCursor = null;
                }
            }
            return result;
        }
    }
}
