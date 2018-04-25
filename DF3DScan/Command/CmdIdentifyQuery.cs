using System;
using System.Collections.Generic;
using System.Linq;
using DF3DControl.Command;
using DF3DDraw;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Base;
using DF3DData.Class;
using DFDataConfig.Class;

namespace DF3DScan.Command
{
    class CmdIdentifyQuery : AbstractMap3DCommand
    {
        private DrawTool _drawTool;
        private List<ITableLabel> _listTLs = new List<ITableLabel>();
        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.SelectOne);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        public override void RestoreEnv()
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Close();
                this._drawTool.End();
            }
            Map3DCommandManager.Pop();
        }

        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                //Clear();
            }
        }

        private void OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.SelectOne
                && this._drawTool.GetSelectFeatureLayerPickResult() != null && this._drawTool.GetSelectPoint() != null)
            {
                ClickQuery();
            }
        }

        private void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            foreach (ITableLabel tl in this._listTLs)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(tl.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tl);
            }
            this._listTLs.Clear();
        }

        private Dictionary<string, string> GetQueryResult(DF3DFeatureClass dffc,int featureId)
        {
            if (dffc == null) return null;
            IFeatureClass fc = dffc.GetFeatureClass();
            if (fc == null) return null;

            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = fc.FidFieldName + "=" + featureId;
                if (fc.GetCount(filter) == 0) return null;
                cursor = fc.Search(filter, false);
                if (cursor == null) return null;
                row = cursor.NextRow();
                if (row == null) return null;

                FacilityClass facClass = dffc.GetFacilityClass();
                IFieldInfoCollection fiCol = row.Fields;
                Dictionary<string, string> dict = new Dictionary<string, string>();

                if (facClass == null)
                {
                    for (int i = 0; i < fiCol.Count; i++)
                    {
                        IFieldInfo fi = row.Fields.Get(i);
                        object obj = row.GetValue(i);
                        if (obj == null) continue;
                        string str = "";
                        switch (fi.FieldType)
                        {
                            case gviFieldType.gviFieldBlob:
                            case gviFieldType.gviFieldUnknown:
                            case gviFieldType.gviFieldGeometry:
                                break;
                            case gviFieldType.gviFieldFloat:
                            case gviFieldType.gviFieldDouble:
                                double d;
                                if (double.TryParse(obj.ToString(), out d))
                                {
                                    str = d.ToString("0.00");
                                }
                                break;
                            default:
                                str = obj.ToString();
                                break;
                        }
                        if (!string.IsNullOrEmpty(str.Trim()))
                        {
                            string temp = (string.IsNullOrEmpty(fi.Alias)) ? fi.Name : fi.Alias;
                            dict[temp] = str;
                        }
                    }
                }
                else
                {
                    foreach (DFDataConfig.Class.FieldInfo fi in facClass.FieldInfoCollection)
                    {
                        if (!fi.CanQuery) continue;
                        int index = row.Fields.IndexOf(fi.Name);
                        if (index == -1) continue;
                        object obj = row.GetValue(index);
                        if (obj == null) continue;
                        IFieldInfo fiFC = row.Fields.Get(index);
                        string str = "";
                        switch (fiFC.FieldType)
                        {
                            case gviFieldType.gviFieldBlob:
                            case gviFieldType.gviFieldUnknown:
                            case gviFieldType.gviFieldGeometry:
                                break;
                            case gviFieldType.gviFieldFloat:
                            case gviFieldType.gviFieldDouble:
                                double d;
                                if (double.TryParse(obj.ToString(), out d))
                                {
                                    str = d.ToString("0.00");
                                }
                                break;
                            default:
                                str = obj.ToString();
                                break;
                        }
                        if (!string.IsNullOrEmpty(str.Trim()))
                        {
                            string temp = (string.IsNullOrEmpty(fi.Alias)) ? fi.Name : fi.Alias;
                            dict[temp] = str;
                        }
                    }
                }
                return dict;
            }
            catch (Exception ex)
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
                return null;
            }
        }

        private Dictionary<string, string> GetQueryResult(IFeatureClassInfo fcInfo, int featureId)
        {
            if (fcInfo == null || featureId < 0) return null;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IDataSourceFactory dsf = new DataSourceFactory();
                if (!dsf.HasDataSourceByString(fcInfo.DataSourceConnectionString)) return null;
                IDataSource ds = dsf.OpenDataSourceByString(fcInfo.DataSourceConnectionString);
                if (ds == null) return null;
                IFeatureDataSet fds = ds.OpenFeatureDataset(fcInfo.DataSetName);
                if (fds == null) return null;
                IFeatureClass fc = fds.OpenFeatureClass(fcInfo.FeatureClassName);
                if (fc == null) return null;
                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = fc.FidFieldName + "=" + featureId;
                if (fc.GetCount(filter) == 0) return null;
                cursor = fc.Search(filter, false);
                if (cursor == null) return null;
                row = cursor.NextRow();
                if (row == null) return null;
                IFieldInfoCollection fiCol = row.Fields;
                Dictionary<string, string> dict = new Dictionary<string, string>();
                for (int i = 0; i < fiCol.Count; i++)
                {
                    IFieldInfo fi = row.Fields.Get(i);
                    object obj = row.GetValue(fiCol.IndexOf(fi.Name));
                    if (obj == null) continue;
                    string str = "";
                    switch (fi.FieldType)
                    {
                        case gviFieldType.gviFieldBlob:
                        case gviFieldType.gviFieldUnknown:
                        case gviFieldType.gviFieldGeometry:
                            break;
                        case gviFieldType.gviFieldFloat:
                        case gviFieldType.gviFieldDouble:
                            double d;
                            if (double.TryParse(obj.ToString(), out d))
                            {
                                str = d.ToString("0.00");
                            }
                            break;
                        default:
                            str = obj.ToString();
                            break;
                    }
                    if (!string.IsNullOrEmpty(str.Trim()))
                    {
                        string temp = (string.IsNullOrEmpty(fi.Alias)) ? fi.Name : fi.Alias;
                        dict[temp] = str;
                    }
                }
                return dict;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void ClickQuery()
        {
            DF3DApplication app = DF3DApplication.Application;
            if(app == null || app.Current3DMapControl == null) return;

            try
            {
                Guid guid = this._drawTool.GetSelectFeatureLayerPickResult().FeatureLayer.FeatureClassId;
                DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(guid.ToString());
                Dictionary<string,string> dict = null;
                if (dffc != null) dict = GetQueryResult(dffc, this._drawTool.GetSelectFeatureLayerPickResult().FeatureId);
                else dict = GetQueryResult(this._drawTool.GetSelectFeatureLayerPickResult().FeatureLayer.FeatureClassInfo, this._drawTool.GetSelectFeatureLayerPickResult().FeatureId);
                if (dict == null) return;

                #region
                ITableLabel tl = DrawTool.CreateTableLabel2(dict.Count);
                tl.TitleText = "属性查询";
                int num = 0;
                foreach (KeyValuePair<string, string> kv in dict)
                {
                    string k = kv.Key;
                    string v = kv.Value;
                    tl.SetRecord(num, 0, k);
                    tl.SetRecord(num, 1, v);
                    num++;
                }
                tl.Position = this._drawTool.GetSelectPoint();                
                _listTLs.Add(tl);
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

    }
}
