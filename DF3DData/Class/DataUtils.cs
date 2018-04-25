using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using DF3DData.Tree;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using DF3DControl.Base;
using Gvitech.CityMaker.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors;
using ICSharpCode.Core;
using DFWinForms.Class;
using DFWinForms.LogicTree;
using DFDataConfig.Logic;
using DF3DData.LogicTree;
using DFDataConfig.Class;
using DFCommon.Class;

namespace DF3DData.Class
{
    public static class DataUtils
    {
        #region 【原数据结构】管线数据添加并可视化
        public static Dictionary<string, string> FindChildRelationByPCode(string pcode, Dictionary<string, string> relation)
        {
            if (relation == null || relation.Count == 0) return null;
            Dictionary<string, string> childRelation = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> kv in relation)
            {
                if (kv.Value.ToString() == pcode) childRelation[kv.Key.ToString()] = kv.Value.ToString();
            }
            return childRelation;
        }
        public static Dictionary<string, IRowBuffer> GetPipeInfos(IConnectionInfo ci, string datasetName, string ocName)
        {
            Dictionary<string, IRowBuffer> infos = new Dictionary<string, IRowBuffer>();
            IFdeCursor o = null;
            IQueryFilter filter = null;
            IRowBuffer r = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                if (!dsFactory.HasDataSource(ci)) return null;
                IDataSource ds = dsFactory.OpenDataSource(ci);
                if (ds == null) return null;
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0) return null;
                for (int i = 0; i < setnames.Length; i++)
                {
                    if (setnames[i] != datasetName) continue;
                    IFeatureDataSet fds = ds.OpenFeatureDataset(setnames[i]);
                    IObjectClass oc = fds.OpenObjectClass(ocName);
                    if (oc == null) return null;
                    filter = new QueryFilterClass
                    {
                        WhereClause = ""
                    };
                    o = oc.Search(filter, false);
                    while ((r = o.NextRow()) != null)
                    {
                        string code = r.GetValue(r.FieldIndex("Code")).ToString();
                        infos[code] = r;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (filter != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(filter);
                    filter = null;
                }
                if (o != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (r != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(r);
                    r = null;
                }
            }
            return infos;
        }
        public static Dictionary<string, string> GetPipeCatelog(IConnectionInfo ci, string datasetName, string ocName)
        {
            Dictionary<string, string> catelog = new Dictionary<string, string>();
            IFdeCursor o = null;
            IQueryFilter filter = null;
            IRowBuffer r = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                if (!dsFactory.HasDataSource(ci)) return null;
                IDataSource ds = dsFactory.OpenDataSource(ci);
                if (ds == null) return null;
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0) return null;
                for (int i = 0; i < setnames.Length; i++)
                {
                    if (setnames[i] != datasetName) continue;
                    IFeatureDataSet fds = ds.OpenFeatureDataset(setnames[i]);
                    IObjectClass oc = fds.OpenObjectClass(ocName);
                    if (oc == null) return null;
                    filter = new QueryFilterClass
                    {
                        WhereClause = ""
                    };
                    o = oc.Search(filter, false);
                    while ((r = o.NextRow()) != null)
                    {
                        string code = r.GetValue(r.FieldIndex("Code")).ToString();
                        string pcode = r.GetValue(r.FieldIndex("PCode")).ToString();
                        catelog.Add(code, pcode);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (filter != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(filter);
                    filter = null;
                }
                if (o != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (r != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(r);
                    r = null;
                }
            }
            return catelog;
        }
        public static Dictionary<string, string> GetRelation(IConnectionInfo ci, string datasetName, string ocName)
        {
            Dictionary<string, string> relation = new Dictionary<string, string>();
            IFdeCursor o = null;
            IQueryFilter filter = null;
            IRowBuffer r = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                if (!dsFactory.HasDataSource(ci)) return null;
                IDataSource ds = dsFactory.OpenDataSource(ci);
                if (ds == null) return null;
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0) return null;
                for (int i = 0; i < setnames.Length; i++)
                {
                    if (setnames[i] != datasetName) continue;
                    IFeatureDataSet fds = ds.OpenFeatureDataset(setnames[i]);
                    IObjectClass oc = fds.OpenObjectClass(ocName);
                    if (oc == null) return null;
                    filter = new QueryFilterClass
                    {
                        WhereClause = ""
                    };
                    o = oc.Search(filter, false);
                    while ((r = o.NextRow()) != null)
                    {
                        string facClassCode = r.GetValue(r.FieldIndex("FacClassCode")).ToString();
                        string featureClassId = r.GetValue(r.FieldIndex("FeatureClassId")).ToString();
                        relation.Add(facClassCode, featureClassId);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (filter != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(filter);
                    filter = null;
                }
                if (o != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (r != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(r);
                    r = null;
                }
            }
            return relation;

        }
        public static void VisualizePipeData(Dictionary<string, IFeatureClass> fcs, Dictionary<string, string> relation, string geoColumnName, IGroupLayer groupLayer, Dictionary<string, string> pipeCatelog, Dictionary<string, string> childPipeCatelog, Dictionary<string, IRowBuffer> infos, bool bNeedFly = false, bool bTempData = true)
        {
            try
            {
                foreach (KeyValuePair<string, string> kv in childPipeCatelog)
                {
                    string code = kv.Key.ToString();
                    string pcode = kv.Value.ToString();
                    if (!relation.ContainsKey(code) && pcode != "-1")
                    {
                        Dictionary<string, string> cchildPipeCatelog = FindChildRelationByPCode(code, pipeCatelog);
                        if (cchildPipeCatelog == null || cchildPipeCatelog.Count == 0) continue;
                        IRowBuffer row = infos[code];
                        string name = row.GetValue(row.FieldIndex("Name")).ToString();
                        Group g = new Group()
                        {
                            Name = name,
                            CustomValue = row,
                            Temp = bTempData
                        };
                        g.Visible = true;
                        groupLayer.Add(g);
                        VisualizePipeData(fcs, relation, geoColumnName, g, pipeCatelog, cchildPipeCatelog, infos, bNeedFly, bTempData);
                    }
                    else if (relation.ContainsKey(code) && fcs.ContainsKey(relation[code]))
                    {
                        DF3DApplication app = DF3DApplication.Application;
                        if (app == null || app.Current3DMapControl == null) return;
                        AxRenderControl d3 = app.Current3DMapControl;
                        IRowBuffer row = infos[code];
                        string name = row.GetValue(row.FieldIndex("Name")).ToString();

                        IFeatureClass fc = fcs[relation[code]];
                        TreeNodeFeatureClass tnFeatureClass = new TreeNodeFeatureClass()
                        {
                            Name = name,
                            CustomValue = fc,
                            //Tag = row,
                            Temp = bTempData
                        };
                        groupLayer.Add(tnFeatureClass);

                        IFieldInfoCollection fieldinfos = fc.GetFields();
                        if (fieldinfos == null) continue;
                        int index = fieldinfos.IndexOf(geoColumnName);
                        if (index == -1) { }
                        else
                        {
                            IFieldInfo fieldinfo = fieldinfos.Get(index);
                            if (null == fieldinfo) continue;
                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                            IFeatureLayer fl = null;
                            if (null != geometryDef)
                            {
                                fl = d3.ObjectManager.CreateFeatureLayer(fc, fieldinfo.Name, null, null, d3.ProjectTree.RootID);
                                tnFeatureClass.SetVisualFeatureLayer(fl);
                                tnFeatureClass.Visible = true;
                                switch (geometryDef.GeometryColumnType)
                                {
                                    case gviGeometryColumnType.gviGeometryColumnModelPoint:
                                        tnFeatureClass.ImageIndex = 3;
                                        break;
                                    case gviGeometryColumnType.gviGeometryColumnPOI:
                                    case gviGeometryColumnType.gviGeometryColumnPoint:
                                        tnFeatureClass.ImageIndex = 4;
                                        break;
                                    case gviGeometryColumnType.gviGeometryColumnPolyline:
                                        tnFeatureClass.ImageIndex = 5;
                                        break;
                                    case gviGeometryColumnType.gviGeometryColumnPolygon:
                                        tnFeatureClass.ImageIndex = 6;
                                        break;
                                }
                            }
                            if (fl != null && !bTempData)
                            {
                                DF3DFeatureClass dfFC = new DF3DFeatureClass(fcs[relation[code]]);
                                if (dfFC == null) continue;
                                DF3DFeatureClassManager.Instance.Add(dfFC);
                            }
                            if (fl != null && bNeedFly)
                            {
                                IEulerAngle angle = new EulerAngle();
                                angle.Set(0, -20, 0);
                                d3.Camera.SetCamera(fl.Envelope.Center, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                                bNeedFly = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void AddAndVisualizePipeData(IConnectionInfo ci, string geoColumnName, TreeList parentTree, Dictionary<string, string> pipeCatelog, Dictionary<string, IRowBuffer> pipeInfos, bool bNeedFly = false, bool bTempData = true)
        {
            try
            {
                if (ci == null || parentTree == null || pipeCatelog == null || pipeCatelog.Count == 0 || pipeInfos == null || pipeInfos.Count == 0) return;
                //获取要素类对照表：string：guid
                Dictionary<string, IFeatureClass> fcs = DataUtils.GetFeatureClass(ci);
                if (fcs == null || fcs.Count == 0) return;
                //获取要素类与设施类的关联：string：code，string：guid
                Dictionary<string, string> relation = DataUtils.GetRelation(ci, "DataSet_BIZ", "OC_FacilityClass");
                if (relation == null || relation.Count == 0) return;

                foreach (KeyValuePair<string, string> kv in pipeCatelog)
                {
                    string code = kv.Key.ToString();
                    string pcode = kv.Value.ToString();

                    if (!relation.ContainsKey(code) && pcode == "-1")
                    {
                        Dictionary<string, string> childRelation = FindChildRelationByPCode(code, pipeCatelog);
                        if (childRelation == null || childRelation.Count == 0) continue;

                        IRowBuffer row = pipeInfos[code];
                        string name = row.GetValue(row.FieldIndex("Name")).ToString();
                        Group g = new Group()
                        {
                            Name = name,
                            CustomValue = row,
                            Temp = bTempData
                        };
                        g.OwnNode = parentTree.AppendNode(new object[] { g.Name }, (TreeListNode)null);
                        g.Visible = true;

                        VisualizePipeData(fcs, relation, geoColumnName, g, pipeCatelog, childRelation, pipeInfos, bNeedFly, bTempData);
                        g.CollapseAll();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 【逻辑分组】管线数据添加并可视化
        private static void AddSubClass(MajorClass mc, GroupLayerClass group, Dictionary<string, IFeatureClass> fcs, string geoColumnName)
        {
            if (mc == null || string.IsNullOrEmpty(mc.ClassifyField) || string.IsNullOrEmpty(mc.Fc3D)) return;
            string[] arrayFc3DGuids = mc.Fc3D.Split(';');
            if (arrayFc3DGuids == null || arrayFc3DGuids.Count() == 0) return;
            // 给每个大类节点关联上所有的要素类，并记录上所有要素类
            Dictionary<string, DF3DFeatureClass> dict = new Dictionary<string, DF3DFeatureClass>();
            foreach (string fc3DGuid in arrayFc3DGuids)
            {
                foreach (KeyValuePair<string, IFeatureClass> kv in fcs)
                {
                    if (kv.Key == fc3DGuid)
                    {
                        dict[kv.Key] = new DF3DFeatureClass(kv.Value);
                        DF3DFeatureClassManager.Instance.Add(dict[kv.Key]);
                        break;
                    }
                }
            }
            if (group is TreeNodeMajorClass) (group as TreeNodeMajorClass).FeatureClasses = dict;

            // 可视化所有的要素类
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            AxRenderControl d3 = app.Current3DMapControl;
            Dictionary<string, IFeatureLayer> dictLayers = new Dictionary<string, IFeatureLayer>();
            foreach (DF3DFeatureClass dffc in dict.Values)
            {
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) continue;
                IGeometryRender geoRender = new SimpleGeometryRender();
                geoRender.RenderGroupField = "GroupId";
                IFeatureLayer fl = d3.ObjectManager.CreateFeatureLayer(fc, geoColumnName, null, geoRender, d3.ProjectTree.RootID);
                if (fl != null)
                {
                    dictLayers[fl.Guid.ToString()] = fl;
                    dffc.SetFeatureLayer(fl);
                }
            }

            // 给每个大类节点划分二级子类
            foreach (SubClass sc in mc.SubClasses)
            {
                TreeNodeSubClass tnsc = new TreeNodeSubClass()
                {
                    Name = sc.Name,
                    CustomValue = sc
                };
                tnsc.FeatureLayers = dictLayers;
                tnsc.FeatureClasses = dict;
                tnsc.Visible = true;
                group.Add(tnsc);
            }
            group.Visible = true;
            group.CollapseAll();
        }

        private static void RecursiveAddAndVisualizePipeData(TreeList parentTree, List<LogicGroup> list, Dictionary<string, IFeatureClass> fcs, string geoColumnName)
        {
            if (parentTree == null || list == null) return;
            foreach (LogicGroup lg in list)
            {
                TreeNodeLogicGroup tnlg = new TreeNodeLogicGroup()
                {
                    Name = string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias
                };
                tnlg.OwnNode = parentTree.AppendNode(new object[] { tnlg.Name }, (TreeListNode)null);
                tnlg.Visible = true;
                RecursiveAddAndVisualizePipeData(tnlg.OwnNode.TreeList, lg.LogicGroups, fcs, geoColumnName);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    TreeNodeMajorClass tnmc = new TreeNodeMajorClass()
                    {
                        Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias,
                        CustomValue = mc
                    };
                    tnlg.Add(tnmc);
                    AddSubClass(mc, tnlg, fcs, geoColumnName);
                }
                tnlg.CollapseAll();
            }
        }

        public static void AddAndVisualizePipeData(IConnectionInfo ci, string geoColumnName, TreeList parentTree, bool bNeedFly = false, bool bTempData = true)
        {
            try
            {
                if (ci == null || parentTree == null) return;

                Dictionary<string, IFeatureClass> fcs = DataUtils.GetFeatureClass(ci);
                if (fcs == null || fcs.Count == 0) return;

                foreach (MajorClass mc in LogicDataStructureManage3D.Instance.RootMajorClasses)
                {
                    TreeNodeMajorClass tnmc = new TreeNodeMajorClass()
                    {
                        Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias,
                        CustomValue = mc
                    };
                    tnmc.OwnNode = parentTree.AppendNode(new object[] { tnmc.Name }, (TreeListNode)null);
                    AddSubClass(mc, tnmc, fcs, geoColumnName);
                }

                foreach (LogicGroup lg in LogicDataStructureManage3D.Instance.RootLogicGroups)
                {
                    TreeNodeLogicGroup tnlg = new TreeNodeLogicGroup()
                    {
                        Name = string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias
                    };
                    tnlg.OwnNode = parentTree.AppendNode(new object[] { tnlg.Name }, (TreeListNode)null);
                    tnlg.Visible = true;
                    RecursiveAddAndVisualizePipeData(tnlg.OwnNode.TreeList, lg.LogicGroups, fcs, geoColumnName);
                    foreach (MajorClass mc in lg.MajorClasses)
                    {
                        TreeNodeMajorClass tnmc = new TreeNodeMajorClass()
                        {
                            Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias,
                            CustomValue = mc
                        };
                        tnlg.Add(tnmc);
                        AddSubClass(mc, tnmc, fcs, geoColumnName);
                    }
                    tnlg.CollapseAll();
                }

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        public static void AddAndVisualizeBaseData(IConnectionInfo ci, string geoColumnName, TreeList parentTree, bool bNeedFly = false, bool bTempData = true)
        {
            try
            {
                if (ci == null || parentTree == null) return;
                if (bTempData) WaitForm.Start("正在加载三维数据，请稍后...", "提示");

                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                AxRenderControl d3 = app.Current3DMapControl;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                if (!dsFactory.HasDataSource(ci)) return;
                IDataSource ds = dsFactory.OpenDataSource(ci);
                if (ds == null) return;
                //TreeNodeDatabase tnDatabase = new TreeNodeDatabase()
                //{
                //    Name = ci.ConnectionType == gviConnectionType.gviConnectionFireBird2x ? System.IO.Path.GetFileNameWithoutExtension(ci.Database) : ci.Database,
                //    CustomValue = ds,
                //    Temp = bTempData
                //};
                //tnDatabase.OwnNode = parentTree.AppendNode(new object[] { tnDatabase.Name }, (TreeListNode)null);
                //tnDatabase.Visible = true;

                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0) return;
                for (int j = 0; j < setnames.Length; j++)
                {
                    IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[j]);

                    string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                    if (fcnames.Length == 0) continue;

                    TreeNodeDataset tnDataset = new TreeNodeDataset()
                    {
                        Name = dataset.Alias,
                        CustomValue = dataset,
                        Temp = bTempData
                    };
                    tnDataset.OwnNode = parentTree.AppendNode(new object[] { tnDataset.Name }, (TreeListNode)null);
                    //tnDatabase.Add(tnDataset);

                    foreach (string name in fcnames)
                    {
                        IFeatureClass fc = dataset.OpenFeatureClass(name);
                        if (fc == null) continue;
                        TreeNodeFeatureClass tnFeatureClass = new TreeNodeFeatureClass(fc.GuidString)
                        {
                            Name = fc.AliasName,
                            CustomValue = fc,
                            Temp = bTempData
                        };
                        tnDataset.Add(tnFeatureClass);

                        IFieldInfoCollection fieldinfos = fc.GetFields();
                        if (fieldinfos == null) continue;
                        int index = fieldinfos.IndexOf(geoColumnName);
                        if (index == -1) { }
                        else
                        {
                            IFieldInfo fieldinfo = fieldinfos.Get(index);
                            if (null == fieldinfo) continue;
                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                            IFeatureLayer fl = null;
                            if (null != geometryDef)
                            {
                                fl = d3.ObjectManager.CreateFeatureLayer(fc, fieldinfo.Name, null, null, d3.ProjectTree.RootID);
                                tnFeatureClass.SetVisualFeatureLayer(fl);
                                switch (geometryDef.GeometryColumnType)
                                {
                                    case gviGeometryColumnType.gviGeometryColumnModelPoint:
                                        tnFeatureClass.Visible = true;
                                        tnFeatureClass.ImageIndex = 3;
                                        break;
                                    case gviGeometryColumnType.gviGeometryColumnPOI:
                                    case gviGeometryColumnType.gviGeometryColumnPoint:
                                        tnFeatureClass.Visible = false;
                                        tnFeatureClass.ImageIndex = 4;
                                        break;
                                    case gviGeometryColumnType.gviGeometryColumnPolyline:
                                        tnFeatureClass.Visible = false;
                                        tnFeatureClass.ImageIndex = 5;
                                        break;
                                    case gviGeometryColumnType.gviGeometryColumnPolygon:
                                        tnFeatureClass.Visible = false;
                                        tnFeatureClass.ImageIndex = 6;
                                        break;
                                }
                            }

                            // 加入管理类
                            if (!bTempData && fl != null)
                            {
                                DF3DFeatureClass dfFC = new DF3DFeatureClass(fc);
                                dfFC.SetFeatureLayer(fl);
                                dfFC.SetTreeLayer(tnFeatureClass);
                                Render3D.Instance.SetRender(tnFeatureClass, dfFC);
                                DF3DFeatureClassManager.Instance.Add(dfFC);
                            }


                            if (fl != null && bNeedFly)
                            {
                                IEulerAngle angle = new EulerAngle();
                                angle.Set(0, -20, 0);
                                d3.Camera.SetCamera(fl.Envelope.Center, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                                bNeedFly = false;
                            }
                        }
                    }
                    tnDataset.CollapseAll();
                }
                //tnDatabase.CollapseAll();

                //ds.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("加载三维数据失败，请检查数据是否被占用或链接是否有错误。", "提示");
                LoggingService.Error("加载三维数据失败：" + ex.Message);
            }
            finally
            {
                if (bTempData) WaitForm.Stop();
            }
        }

        public static void AddAndVisualize3DTileData(string str3DTileConn, string str3DTilePwd, TreeList parentTree, bool bNeedFly = true)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                AxRenderControl d3 = app.Current3DMapControl;
                I3DTileLayer layer = d3.ObjectManager.Create3DTileLayer(str3DTileConn, str3DTilePwd, d3.ProjectTree.RootID);
                if (layer != null)
                {
                    TreeNode3DTile tn = new TreeNode3DTile()
                    {
                        Name = "瓦片数据",
                        CustomValue = layer,
                    };
                    tn.OwnNode = parentTree.AppendNode(new object[] { tn.Name }, (TreeListNode)null);
                    tn.Visible = true;
                    if (bNeedFly) d3.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void AddAndVisualizeTerrainData(string str3DTerrainConn, string str3DTerrainPwd, TreeList parentTree, bool bNeedFly = true)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                AxRenderControl d3 = app.Current3DMapControl;
                d3.Terrain.RegisterTerrain(str3DTerrainConn, str3DTerrainPwd);
                if(d3.Terrain.IsRegistered)
                {
                    TreeNodeTerrain tnTerrain = new TreeNodeTerrain()
                    {
                        Name = "基础地形",
                        CustomValue = d3.Terrain,
                    };
                    tnTerrain.OwnNode = parentTree.AppendNode(new object[] { tnTerrain.Name }, (TreeListNode)null);
                    tnTerrain.Visible = true;
                    if (bNeedFly) d3.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public static Dictionary<string, IFeatureClass> GetFeatureClass(IConnectionInfo ci)
        {
            try
            {
                Dictionary<string, IFeatureClass> fcs = new Dictionary<string, IFeatureClass>();
                IDataSourceFactory dsFactory = new DataSourceFactory();
                if(!dsFactory.HasDataSource(ci)) return null;
                IDataSource ds = dsFactory.OpenDataSource(ci);
                if (ds == null) return null;
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0) return null;
                for (int j = 0; j < setnames.Length; j++)
                {
                    IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[j]);

                    string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                    if (fcnames.Length == 0) continue;
                    foreach (string name in fcnames)
                    {
                        IFeatureClass fc = dataset.OpenFeatureClass(name);
                        if (fc == null) continue;
                        fcs[fc.GuidString] = fc;
                    }
                }
                return fcs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool CanConnection(IConnectionInfo connInfo)
        {
            try
            {
                IDataSourceFactory dsf = new DataSourceFactory();
                return dsf.HasDataSource(connInfo);
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public static void AddAndVisualize3DTileData(string str3DTileConn, string str3DTilePwd, TreeList parentTree,string treeNodeName, bool bNeedFly = false, bool bTempData = true)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                AxRenderControl d3 = app.Current3DMapControl;
                I3DTileLayer layer = d3.ObjectManager.Create3DTileLayer(str3DTileConn, str3DTilePwd, d3.ProjectTree.RootID);
                if (layer != null)
                {
                    TreeNode3DTile node = new TreeNode3DTile(layer.Guid.ToString())
                    {
                        Name = treeNodeName,
                        CustomValue = layer,
                        Temp = bTempData
                    };
                    node.OwnNode = parentTree.AppendNode(new object[] { node.Name }, (TreeListNode)null);
                    node.Visible = true;

                    if (bNeedFly) d3.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
                }
            }
            catch (Exception ex)
            {

            }

        }

       
    }
}
