using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using DevExpress.XtraTreeList;
using Gvitech.CityMaker.Controls;
using DF3DControl.Base;
using DF3DPlanData.Enm;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;
using DF3DPlanData.Tree;
using DevExpress.XtraTreeList.Nodes;
using DF3DPlanData.UC;

namespace DF3DPlanData.Class
{
    public static class DataUtils
    {
        public static void Add3DPlanData(IConnectionInfo ci,Layer3DPlanTreePad pad, TreeList parentTree)
        {
            try
            {
                if (ci == null || parentTree == null) return;
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                //AxRenderControl d3 = app.Current3DMapControl;
                IDataSourceFactory  dsFactory = new DataSourceFactory();
                if (!dsFactory.HasDataSource(ci)) return;
                IDataSource ds = dsFactory.OpenDataSource(ci);
                if (ds == null) return;
                List<Plan> allPlan = SelectPlans(ds, "1=1");
                Dictionary<int, int> planIds = new Dictionary<int, int>();
                if ((allPlan != null) && (allPlan.Count > 0))
                {
                    for (int i = 0; i < allPlan.Count; i++)
                    {
                        planIds.Add(allPlan[i].PlanID, (int)allPlan[i].ConnType);
                    }
                }

                //**********加权限 控制可见性 修改**********

                //****************************************
                InitTree(pad, parentTree, ds, allPlan, planIds);
            }
            catch (Exception ex)
            {
            }
        }

        private static List<Plan> SelectPlans(IDataSource ds, string WhereClause)
        {
            List<Plan> list = new List<Plan>();
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                if(ds == null) return list;
                IFeatureDataSet fds = ds.OpenFeatureDataset("UP_PlanLibDs_Indication");
                if (fds == null) return list;
                IObjectClass class2 = fds.OpenObjectClass("UP_Plan");
                if (class2 != null)
                {
                    IQueryFilter filter = new QueryFilter
                    {
                        WhereClause = WhereClause
                    };
                    IFieldInfoCollection fiCol = class2.GetFields();
                    cursor = class2.Search(filter, true);
                    while ((row = cursor.NextRow()) != null)
                    {
                        Plan plan = new Plan
                        {
                            PlanID = (row.IsNull(fiCol.IndexOf("oid"))) ? -1 : Convert.ToInt32(row.GetValue(fiCol.IndexOf("oid"))),
                            PlanGuid = (row.IsNull(fiCol.IndexOf("planGuid"))) ? "" : row.GetValue(fiCol.IndexOf("planGuid")).ToString(),
                            PlanName = (row.IsNull(fiCol.IndexOf("planName"))) ? "" : row.GetValue(fiCol.IndexOf("planName")).ToString(),
                            PlanCode = (row.IsNull(fiCol.IndexOf("planCode"))) ? "" : row.GetValue(fiCol.IndexOf("planCode")).ToString(),
                            PlanDesigner = (row.IsNull(fiCol.IndexOf("planDesigner"))) ? "" : row.GetValue(fiCol.IndexOf("planDesigner")).ToString(),
                            PlanState = (row.IsNull(fiCol.IndexOf("planState"))) ? PlanState.None : (PlanState)row.GetValue(fiCol.IndexOf("planState")),
                            ProjectID = (row.IsNull(fiCol.IndexOf("projectID"))) ? -1 : Convert.ToInt32(row.GetValue(fiCol.IndexOf("projectID"))),
                            XOffset = (row.IsNull(fiCol.IndexOf("xOffset"))) ? double.NaN : Convert.ToDouble(row.GetValue(fiCol.IndexOf("xOffset"))),
                            YOffset = (row.IsNull(fiCol.IndexOf("yOffset"))) ? double.NaN : Convert.ToDouble(row.GetValue(fiCol.IndexOf("yOffset"))),
                            Creator = (row.IsNull(fiCol.IndexOf("creator"))) ? "" : row.GetValue(fiCol.IndexOf("creator")).ToString(),
                            CreateTime = (row.IsNull(fiCol.IndexOf("createTime"))) ? DateTime.MinValue : (DateTime)row.GetValue(fiCol.IndexOf("createTime")),
                            AbandonReason = (row.IsNull(fiCol.IndexOf("abandonReason"))) ? "" : row.GetValue(fiCol.IndexOf("abandonReason")).ToString(),
                            Description = (row.IsNull(fiCol.IndexOf("description"))) ? "" : row.GetValue(fiCol.IndexOf("description")).ToString(),
                            Bound = (row.IsNull(fiCol.IndexOf("Bound"))) ? "" : row.GetValue(fiCol.IndexOf("Bound")).ToString(),
                            ConnType = (row.IsNull(fiCol.IndexOf("ConnType"))) ? PlanType.None : (PlanType)row.GetValue(fiCol.IndexOf("ConnType"))
                        };
                        if (plan.ConnType == PlanType.Network)
                        {
                            list.Add(new NetWorkPlan(plan));
                        }
                        else
                        {
                            list.Add(new LocalPlan(plan));
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
            }
            finally
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
            }
            return list;
        }

        private static List<Project> SelectProjects(IDataSource ds, string WhereClause)
        {
            List<Project> list = new List<Project>();
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                if (ds == null) return list;
                IFeatureDataSet fds = ds.OpenFeatureDataset("UP_PlanLibDs_Indication");
                if (fds == null) return list;
                IFeatureClass class2 = fds.OpenFeatureClass("UP_Project");
                if (class2 != null)
                {
                    IQueryFilter filter = new QueryFilter
                    {
                        WhereClause = WhereClause
                    };
                    IFieldInfoCollection fiCol = class2.GetFields();
                    cursor = class2.Search(filter, true);
                    while ((row = cursor.NextRow()) != null)
                    {
                        Project item = new Project
                        {
                            ProjectID = (row.IsNull(fiCol.IndexOf("oid"))) ? -1 : Convert.ToInt32(row.GetValue(fiCol.IndexOf("oid"))),
                            ProjectName = (row.IsNull(fiCol.IndexOf("projectName"))) ? "" : row.GetValue(fiCol.IndexOf("projectName")).ToString(),
                            ProjectCode = (row.IsNull(fiCol.IndexOf("projectCode"))) ? "" : row.GetValue(fiCol.IndexOf("projectCode")).ToString(),
                            ProjectType = (row.IsNull(fiCol.IndexOf("projectType"))) ? ProjectType.None : (ProjectType)row.GetValue(fiCol.IndexOf("projectType")),
                            ChangeState = (row.IsNull(fiCol.IndexOf("changeState"))) ? ProjectChangeState.None : (ProjectChangeState)Convert.ToInt32(row.GetValue(fiCol.IndexOf("changeState"))),
                            ProjectTime = (row.IsNull(fiCol.IndexOf("projectTime"))) ? DateTime.MinValue : (DateTime)row.GetValue(fiCol.IndexOf("projectTime")),
                            ProjectOwner = (row.IsNull(fiCol.IndexOf("projectOwner"))) ? "" : row.GetValue(fiCol.IndexOf("projectOwner")).ToString(),
                            Description = (row.IsNull(fiCol.IndexOf("description"))) ? "" : row.GetValue(fiCol.IndexOf("description")).ToString(),
                            ProjectBound = (row.IsNull(fiCol.IndexOf("projectBound"))) ? "" : row.GetValue(fiCol.IndexOf("projectBound")).ToString(),
                            ProjectLandUse = (row.IsNull(fiCol.IndexOf("projectLandUse"))) ? null : (row.GetValue(fiCol.IndexOf("projectLandUse")) as IGeometry),
                            ProjectElevation = (row.IsNull(fiCol.IndexOf("projectElevation"))) ? double.NaN : Convert.ToDouble(row.GetValue(fiCol.IndexOf("projectElevation"))),
                            ProjectRemark1 = (row.IsNull(fiCol.IndexOf("projectRemark1"))) ? "" : row.GetValue(fiCol.IndexOf("projectRemark1")).ToString(),
                            ProjectLocation = (row.IsNull(fiCol.IndexOf("projectRemark2"))) ? "" : row.GetValue(fiCol.IndexOf("projectRemark2")).ToString(),
                            ProjectQuo = (row.IsNull(fiCol.IndexOf("projectQuo"))) ? null : (row.GetValue(fiCol.IndexOf("projectQuo")) as IBinaryBuffer)
                        };
                        list.Add(item);
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
            }
            finally
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
            }
            return list;

        
            }

        private static void InitTree(Layer3DPlanTreePad pad, TreeList parentTree, IDataSource ds, List<Plan> allPlans, Dictionary<int, int> ViewPlanIds)
        {
            List<Plan> list = new List<Plan>();
            List<Plan> list2 = new List<Plan>();
            if ((allPlans != null) && (allPlans.Count > 0))
            {
                List<int> list3 = ViewPlanIds.Keys.ToList<int>();
                if ((list3 != null) && (list3.Count > 0))
                {
                    foreach (Plan plan in allPlans)
                    {
                        if (list3.Contains(plan.PlanID))
                        {
                            list2.Add(plan);
                        }
                        list.Add(plan);
                    }
                }
                else
                {
                    foreach (Plan plan2 in allPlans)
                    {
                        list.Add(plan2);
                    }
                }
            }
            List<Project> list4 = SelectProjects(ds, "1=1");
            List<int> projIds = new List<int>();
            foreach (Project project in list4)
            {
                projIds.Add(project.ProjectID);
            }
            //**********加权限 控制可见性 修改**********

            //****************************************
            List<Project> list6 = new List<Project>();//当前用户
            if ((projIds != null) && (projIds.Count > 0))
            {
                foreach (Project project2 in list4)
                {
                    if (projIds.Contains(project2.ProjectID))
                    {
                        list6.Add(project2);
                    }
                }
            }
            ProjectManagerLayer pml = new ProjectManagerLayer()
            {
                Name = "建设项目"
            };
            TreeListNode nodepml = parentTree.AppendNode(new object[] { pml.Name }, (TreeListNode)null);
            if (pml != null)
            {
                pml.LogicTree = pad;
                pml.OwnNode = nodepml;
                pml.Visible = nodepml.Checked = true;
            }
            ProjectLayerService.Instance.ProjectsRoot = pml;
            CompletedProjectManagerLayer cpml = new CompletedProjectManagerLayer()
            {
                Name = "竣工项目"
            };
            TreeListNode nodecpml = parentTree.AppendNode(new object[] { cpml.Name }, (TreeListNode)null);
            if (cpml != null)
            {
                cpml.LogicTree = pad;
                cpml.OwnNode = nodecpml;
                cpml.Visible = nodecpml.Checked = true;
            }
            ProjectLayerService.Instance.CompletedProjectRoot = cpml;
            FormallyProjectManagerLayer fpml = new FormallyProjectManagerLayer()
            {
                Name = "正式项目"
            };
            TreeListNode nodefpml = parentTree.AppendNode(new object[] { fpml.Name }, (TreeListNode)null);
            if (fpml != null)
            {
                fpml.LogicTree = pad;
                fpml.OwnNode = nodefpml;
                fpml.Visible = nodefpml.Checked = true;
            }
            ProjectLayerService.Instance.FormallyProjectRoot = fpml;

            if ((list6 != null) && (list6.Count > 0))
            {
                foreach (Project project3 in list6)
                {
                    ProjectGroupLayer class2 = null;
                    List<Plan> list7 = new List<Plan>();
                    PlanState formally = PlanState.New;
                    foreach (Plan plan4 in list)
                    {
                        if (plan4.ProjectID == project3.ProjectID)
                        {
                            if (plan4.PlanState == PlanState.Formally)
                            {
                                formally = PlanState.Formally;
                            }
                            else if (plan4.PlanState == PlanState.Completed)
                            {
                                formally = PlanState.Completed;
                            }
                            if (list2.Contains(plan4))
                            {

                                list7.Add(plan4);
                            }
                        }
                    }
                    foreach (Plan plan6 in list7)
                    {
                        list2.Remove(plan6);
                    }
                    foreach (Plan plan7 in list7)
                    {
                        list.Remove(plan7);
                    }
                    switch (formally)
                    {
                        case PlanState.New:
                            class2 = ProjectLayerService.Instance.ProjectsRoot.CreateProject(project3);
                            break;

                        case PlanState.Formally:
                            class2 = ProjectLayerService.Instance.FormallyProjectRoot.CreateProject(project3);
                            break;

                        case PlanState.Completed:
                            class2 = ProjectLayerService.Instance.CompletedProjectRoot.CreateProject(project3);
                            break;
                    }
                    if (class2 != null)
                    {
                        class2.SetTerrainModifier(project3.ProjectRemark1, project3.ProjectElevation);
                        foreach (Plan plan8 in list7)
                        {
                            Plan plan = plan8.Clone();
                            UrbanPlan plan10 = new UrbanPlan(ref plan);
                            PlanGroupLayer class3 = class2.CreatePlan(ref plan10);
                        }
                    }
                }
            }
        }

    }
}
