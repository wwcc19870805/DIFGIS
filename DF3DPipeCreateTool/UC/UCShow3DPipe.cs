using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.RenderControl;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeCore;
using DevExpress.XtraTreeList.Nodes;
using DF3DPipeCreateTool.Frm;
using DFDataConfig.Class;
using DFWinForms.Class;
using DF3DDraw;

namespace DF3DPipeCreateTool.UC
{
    public class UCShow3DPipe : XtraUserControl
    {
        private SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraBars.BarManager barManager1;
        private IContainer components;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbiAddTerrain;
        private DevExpress.XtraBars.BarButtonItem bbiWander;
        private DevExpress.XtraBars.BarButtonItem bbiHDistance;
        private DevExpress.XtraBars.BarButtonItem bbiClickQuery;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem bbiVDistance;
        private DevExpress.XtraBars.BarButtonItem bbiDistance;
        private DevExpress.XtraBars.BarEditItem beiTerrainOpac;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
        private DevExpress.XtraBars.BarButtonItem bbiFlyToTerrain;
        private DevExpress.XtraBars.BarButtonItem bbiClear;
        private DevExpress.XtraBars.BarButtonItem bbiCoord;
        private Gvitech.CityMaker.Controls.AxRenderControl AxRenderControl3D;

        public UCShow3DPipe()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCShow3DPipe));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.AxRenderControl3D = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiAddTerrain = new DevExpress.XtraBars.BarButtonItem();
            this.beiTerrainOpac = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.bbiFlyToTerrain = new DevExpress.XtraBars.BarButtonItem();
            this.bbiWander = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClickQuery = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClear = new DevExpress.XtraBars.BarButtonItem();
            this.bbiHDistance = new DevExpress.XtraBars.BarButtonItem();
            this.bbiVDistance = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDistance = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bbiCoord = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AxRenderControl3D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 55);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.AxRenderControl3D);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(560, 392);
            this.splitContainerControl1.SplitterPosition = 193;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.Size = new System.Drawing.Size(193, 392);
            this.treeList1.StateImageList = this.imageCollection1;
            this.treeList1.TabIndex = 0;
            this.treeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterCheckNode);
            this.treeList1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDoubleClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "名称";
            this.treeListColumn1.FieldName = "Name";
            this.treeListColumn1.MinWidth = 49;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 91;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "对象";
            this.treeListColumn2.FieldName = "Object";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Database.png");
            this.imageCollection1.Images.SetKeyName(1, "Dataset.png");
            this.imageCollection1.Images.SetKeyName(2, "FeatureLayer_model.png");
            // 
            // AxRenderControl3D
            // 
            this.AxRenderControl3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AxRenderControl3D.Enabled = true;
            this.AxRenderControl3D.Location = new System.Drawing.Point(0, 0);
            this.AxRenderControl3D.Name = "AxRenderControl3D";
            this.AxRenderControl3D.Size = new System.Drawing.Size(362, 392);
            this.AxRenderControl3D.TabIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiAddTerrain,
            this.bbiWander,
            this.bbiHDistance,
            this.bbiClickQuery,
            this.bbiVDistance,
            this.bbiDistance,
            this.beiTerrainOpac,
            this.bbiFlyToTerrain,
            this.bbiClear,
            this.bbiCoord});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTrackBar1});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAddTerrain),
            new DevExpress.XtraBars.LinkPersistInfo(this.beiTerrainOpac),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiFlyToTerrain),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiWander, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClickQuery, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClear),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCoord, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiHDistance),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiVDistance),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDistance)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.Text = "Main menu";
            // 
            // bbiAddTerrain
            // 
            this.bbiAddTerrain.Caption = "添加地形";
            this.bbiAddTerrain.Id = 0;
            this.bbiAddTerrain.Name = "bbiAddTerrain";
            this.bbiAddTerrain.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAddTerrain_ItemClick);
            // 
            // beiTerrainOpac
            // 
            this.beiTerrainOpac.Caption = "地形透明度";
            this.beiTerrainOpac.Edit = this.repositoryItemTrackBar1;
            this.beiTerrainOpac.EditValue = "100";
            this.beiTerrainOpac.Enabled = false;
            this.beiTerrainOpac.Id = 6;
            this.beiTerrainOpac.Name = "beiTerrainOpac";
            this.beiTerrainOpac.Width = 100;
            this.beiTerrainOpac.EditValueChanged += new System.EventHandler(this.beiTerrainOpac_EditValueChanged);
            // 
            // repositoryItemTrackBar1
            // 
            this.repositoryItemTrackBar1.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemTrackBar1.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTrackBar1.Maximum = 100;
            this.repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
            this.repositoryItemTrackBar1.EditValueChanged += new System.EventHandler(this.repositoryItemTrackBar1_EditValueChanged);
            // 
            // bbiFlyToTerrain
            // 
            this.bbiFlyToTerrain.Caption = "飞向地形";
            this.bbiFlyToTerrain.Id = 7;
            this.bbiFlyToTerrain.Name = "bbiFlyToTerrain";
            this.bbiFlyToTerrain.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiFlyToTerrain_ItemClick);
            // 
            // bbiWander
            // 
            this.bbiWander.Caption = "漫游模式";
            this.bbiWander.Id = 1;
            this.bbiWander.Name = "bbiWander";
            this.bbiWander.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiWander_ItemClick);
            // 
            // bbiClickQuery
            // 
            this.bbiClickQuery.Caption = "点击查询";
            this.bbiClickQuery.Id = 3;
            this.bbiClickQuery.Name = "bbiClickQuery";
            this.bbiClickQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClickQuery_ItemClick);
            // 
            // bbiClear
            // 
            this.bbiClear.Caption = "清除标签";
            this.bbiClear.Id = 8;
            this.bbiClear.Name = "bbiClear";
            this.bbiClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClear_ItemClick);
            // 
            // bbiHDistance
            // 
            this.bbiHDistance.Caption = "水平距离";
            this.bbiHDistance.Id = 2;
            this.bbiHDistance.Name = "bbiHDistance";
            this.bbiHDistance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiHDistance_ItemClick);
            // 
            // bbiVDistance
            // 
            this.bbiVDistance.Caption = "垂直距离";
            this.bbiVDistance.Id = 4;
            this.bbiVDistance.Name = "bbiVDistance";
            this.bbiVDistance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiVDistance_ItemClick);
            // 
            // bbiDistance
            // 
            this.bbiDistance.Caption = "直线距离";
            this.bbiDistance.Id = 5;
            this.bbiDistance.Name = "bbiDistance";
            this.bbiDistance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDistance_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(560, 55);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 447);
            this.barDockControlBottom.Size = new System.Drawing.Size(560, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 55);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 392);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(560, 55);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 392);
            // 
            // bbiCoord
            // 
            this.bbiCoord.Caption = "坐标";
            this.bbiCoord.Id = 9;
            this.bbiCoord.Name = "bbiCoord";
            this.bbiCoord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCoord_ItemClick);
            // 
            // UCShow3DPipe
            // 
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCShow3DPipe";
            this.Size = new System.Drawing.Size(560, 447);
            this.Load += new System.EventHandler(this.UCShow3DPipe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AxRenderControl3D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).EndInit();
            this.ResumeLayout(false);

        }


        private bool Init3DControl()
        {
            try
            {
                IPropertySet ps = new PropertySet();
                ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
                if (!this.AxRenderControl3D.Initialize(true, ps)) return false;
                this.AxRenderControl3D.Camera.FlyTime = 0;
                this.AxRenderControl3D.Camera.AutoClipPlane = false;
                this.AxRenderControl3D.Camera.NearClipPlane = 0.01f;
                this.AxRenderControl3D.Camera.FarClipPlane = 100000;
                string tmpSkyboxPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\Resource\Skybox");
                ISkyBox skybox = this.AxRenderControl3D.ObjectManager.GetSkyBox(0);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\13_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\13_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\13_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\13_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\13_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\13_UP.jpg");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private IDataSource _ds;
        private List<Guid> allFLGuid = new List<Guid>();
        private Dictionary<Guid, IFeatureClass> _dictFeatureClass = new Dictionary<Guid, IFeatureClass>();
        private List<Guid> allTLGuid = new List<Guid>();
        public void Init()
        {
            try
            {
                this._ds = DF3DPipeCreateApp.App.PipeLib;
                if (this._ds == null) { this.Enabled = false; return; }
                this.treeList1.ClearNodes();
                if (!this._init3DControl) this._init3DControl = Init3DControl();
                if (!this._init3DControl) { this.Enabled = false; return; }
                if (this._init3DControl)
                {
                    foreach (Guid guid in allFLGuid)
                    {
                        this.AxRenderControl3D.ObjectManager.DeleteObject(guid);
                    }
                    this.allFLGuid.Clear();
                    foreach (Guid guid in allTLGuid)
                    {
                        this.AxRenderControl3D.ObjectManager.DeleteObject(guid);
                    }
                    this.allTLGuid.Clear();

                    this._dictFeatureClass.Clear();

                    WaitForm.Start("正在加载数据...", "请稍后");

                    bool bHaveFly = false;
                    Guid gd = Guid.NewGuid();
                    string[] fdsNames = this._ds.GetFeatureDatasetNames();
                    if (fdsNames == null) return;
                    foreach (string fdsName in fdsNames)
                    {
                        IFeatureDataSet fds = this._ds.OpenFeatureDataset(fdsName);
                        if (fds == null) continue;
                        TreeListNode fdsNode = this.treeList1.AppendNode(new object[] { fds.Name, fds }, null);
                        fdsNode.StateImageIndex = 0;
                        string[] fcNames = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                        if (fcNames == null) continue;
                        foreach (string fcName in fcNames)
                        {
                            IFeatureClass fc = fds.OpenFeatureClass(fcName);
                            if (fc == null) continue;
                            this._dictFeatureClass[fc.Guid] = fc;
                            string name = string.IsNullOrEmpty(fc.AliasName) ? fc.Name : fc.AliasName;
                            TreeListNode fcNode = this.treeList1.AppendNode(new object[] { name, fc }, fdsNode);
                            fcNode.StateImageIndex = 1;
                            IFieldInfoCollection fiCol = fc.GetFields();
                            int indexGeo = fiCol.IndexOf("Geometry");
                            if (indexGeo != -1)
                            {
                                IFieldInfo fi = fiCol.Get(indexGeo);
                                if (fi.GeometryDef != null)
                                {
                                    IFeatureLayer fl = this.AxRenderControl3D.ObjectManager.CreateFeatureLayer(fc, fi.Name, null, null, this.AxRenderControl3D.ProjectTree.RootID);
                                    if (fl == null) continue;
                                    fl.MaxVisibleDistance = 100000;
                                    TreeListNode flNode = this.treeList1.AppendNode(new object[] { fi.Name, fl }, fcNode);
                                    flNode.StateImageIndex = 2;
                                    if (fi.GeometryDef.GeometryColumnType == gviGeometryColumnType.gviGeometryColumnModelPoint)
                                    {
                                        fl.VisibleMask = gviViewportMask.gviViewAllNormalView;
                                        fl.ForceCullMode = true;
                                        fl.CullMode = Gvitech.CityMaker.Resource.gviCullFaceMode.gviCullNone;
                                        flNode.Checked = true;
                                    }
                                    else
                                    {
                                        fl.VisibleMask = gviViewportMask.gviViewNone;
                                        flNode.Checked = false;
                                    }
                                    bHaveFly = true;
                                    gd = fl.Guid;
                                    this.allFLGuid.Add(fl.Guid);
                                }
                            }
                            int indexShp = fiCol.IndexOf("Shape");
                            if (indexShp != -1)
                            {
                                IFieldInfo fi = fiCol.Get(indexShp);
                                if (fi.GeometryDef != null)
                                {
                                    IFeatureLayer fl = this.AxRenderControl3D.ObjectManager.CreateFeatureLayer(fc, fi.Name, null, null, this.AxRenderControl3D.ProjectTree.RootID);
                                    if (fl == null) continue;
                                    fl.MaxVisibleDistance = 100000;
                                    TreeListNode flNode = this.treeList1.AppendNode(new object[] { fi.Name, fl }, fcNode);
                                    flNode.StateImageIndex = 2;
                                    if (fi.GeometryDef.GeometryColumnType == gviGeometryColumnType.gviGeometryColumnModelPoint)
                                    {
                                        fl.VisibleMask = gviViewportMask.gviViewAllNormalView;
                                        flNode.Checked = true;
                                    }
                                    else
                                    {
                                        fl.VisibleMask = gviViewportMask.gviViewNone;
                                        flNode.Checked = false;
                                    }
                                    bHaveFly = true;
                                    gd = fl.Guid;
                                    this.allFLGuid.Add(fl.Guid);
                                }
                            }
                            int indexFoot = fiCol.IndexOf("FootPrint");
                            if (indexFoot != -1)
                            {
                                IFieldInfo fi = fiCol.Get(indexFoot);
                                if (fi.GeometryDef != null)
                                {
                                    IFeatureLayer fl = this.AxRenderControl3D.ObjectManager.CreateFeatureLayer(fc, fi.Name, null, null, this.AxRenderControl3D.ProjectTree.RootID);
                                    fl.MaxVisibleDistance = 100000;
                                    if (fl == null) continue;
                                    TreeListNode flNode = this.treeList1.AppendNode(new object[] { fi.Name, fl }, fcNode);
                                    flNode.StateImageIndex = 2;
                                    if (fi.GeometryDef.GeometryColumnType == gviGeometryColumnType.gviGeometryColumnModelPoint)
                                    {
                                        fl.VisibleMask = gviViewportMask.gviViewAllNormalView;
                                        flNode.Checked = true;
                                    }
                                    else
                                    {
                                        fl.VisibleMask = gviViewportMask.gviViewNone;
                                        flNode.Checked = false;
                                    }
                                    bHaveFly = true;
                                    gd = fl.Guid;
                                    this.allFLGuid.Add(fl.Guid);
                                }
                            }
                        }
                    }
                    this.treeList1.ExpandAll();
                    if(bHaveFly) this.AxRenderControl3D.Camera.FlyToObject(gd, gviActionCode.gviActionFlyTo);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                WaitForm.Stop();
            }
        }

        private bool _init3DControl;
        private void UCShow3DPipe_Load(object sender, EventArgs e)
        {
            this._init3DControl = Init3DControl();
        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            TreeListNode checkNode = e.Node;
            if (checkNode == null) return;
            object obj = checkNode.GetValue("Object");
            if (obj != null && obj is IFeatureLayer)
            {
                if (!checkNode.Checked)
                    (obj as IFeatureLayer).VisibleMask = gviViewportMask.gviViewNone;
                else (obj as IFeatureLayer).VisibleMask = gviViewportMask.gviViewAllNormalView;
            }
            else if (obj != null && obj is IFeatureClass)
            {
                foreach (TreeListNode cnode in checkNode.Nodes)
                {
                    object obj1 = cnode.GetValue("Object");
                    if (obj1 != null && obj1 is IFeatureLayer)
                    {
                        if (!checkNode.Checked)
                            (obj1 as IFeatureLayer).VisibleMask = gviViewportMask.gviViewNone;
                        else (obj1 as IFeatureLayer).VisibleMask = gviViewportMask.gviViewAllNormalView;
                    }
                }
            }
            else if (obj != null && obj is IFeatureDataSet)
            {
                foreach (TreeListNode fcnode in checkNode.Nodes)
                {
                    foreach (TreeListNode cnode in fcnode.Nodes)
                    {
                        object obj1 = cnode.GetValue("Object");
                        if (obj1 != null && obj1 is IFeatureLayer)
                        {
                            if (!checkNode.Checked)
                                (obj1 as IFeatureLayer).VisibleMask = gviViewportMask.gviViewNone;
                            else (obj1 as IFeatureLayer).VisibleMask = gviViewportMask.gviViewAllNormalView;
                        }
                    }
                }
            }
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 2 && this._init3DControl && this.treeList1.FocusedNode != null)
            {
                object obj = this.treeList1.FocusedNode.GetValue("Object");
                if (obj != null && obj is IFeatureLayer)
                {
                    this.AxRenderControl3D.Camera.FlyToObject((obj as IFeatureLayer).Guid, gviActionCode.gviActionFlyTo);
                }
            }
        }

        private void bbiCoord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AxRenderControl3D.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.AxRenderControl3D.MeasurementMode = gviMeasurementMode.gviMeasureCoordinate;
        }

        private void bbiHDistance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AxRenderControl3D.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.AxRenderControl3D.MeasurementMode = gviMeasurementMode.gviMeasureHorizontalDistance;
        }
        private void bbiVDistance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AxRenderControl3D.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.AxRenderControl3D.MeasurementMode = gviMeasurementMode.gviMeasureVerticalDistance;
        }
        private void bbiDistance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AxRenderControl3D.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.AxRenderControl3D.MeasurementMode = gviMeasurementMode.gviMeasureAerialDistance;
        }

        private void bbiWander_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AxRenderControl3D.InteractMode = gviInteractMode.gviInteractNormal;
            this.AxRenderControl3D.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.AxRenderControl3D.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            if (this.bMouseClick) this.AxRenderControl3D.RcMouseClickSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(AxRenderControl3D_RcMouseClickSelect);
            this.bMouseClick = false;

            
        }

        private bool bMouseClick;
        private void bbiClickQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AxRenderControl3D.InteractMode = gviInteractMode.gviInteractSelect;
            this.AxRenderControl3D.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.AxRenderControl3D.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.AxRenderControl3D.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(AxRenderControl3D_RcMouseClickSelect);
            this.bMouseClick = true;
        }

        private void AxRenderControl3D_RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IPickResult pr = e.pickResult;
                if (pr.Type == gviObjectType.gviObjectFeatureLayer &&  pr is IFeatureLayerPickResult)
                {
                    IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                    int oid = flpr.FeatureId;
                    IFeatureLayer fl = flpr.FeatureLayer;
                    if (this._dictFeatureClass.ContainsKey(fl.FeatureClassId))
                    {
                        IFeatureClass fc = this._dictFeatureClass[fl.FeatureClassId];
                        if (fc == null) return;
                        IQueryFilter filter = new QueryFilter();
                        filter.WhereClause = "oid = " + oid;
                        cursor = fc.Search(filter,true);
                        IFieldInfoCollection fiCol = fc.GetFields();
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        if ((row = cursor.NextRow()) != null)
                        {
                            for (int i = 0; i < fiCol.Count; i++)
                            {
                                IFieldInfo fi = fiCol.Get(i);
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
                                    string temp = fi.Name + "(" + fi.Alias + ")";
                                    dict[temp] = str;
                                }
                            }
                        }
                        #region
                        ITableLabel tl =  DrawTool.CreateTableLabel2(dict.Count);
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
                        tl.Position = e.intersectPoint;                        
                        allTLGuid.Add(tl.Guid);
                        #endregion

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bbiAddTerrain_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormChangeTerrain dlg = new FormChangeTerrain();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.AxRenderControl3D.Terrain.RegisterTerrain(dlg.ConnInfo, dlg.Pwd);
                if (this.AxRenderControl3D.Terrain.IsRegistered)
                {
                    this.AxRenderControl3D.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
                    this.beiTerrainOpac.Enabled = true;
                    this.AxRenderControl3D.Terrain.Opacity = double.Parse(this.beiTerrainOpac.EditValue.ToString()) / 100;
                }
                else
                {
                    this.beiTerrainOpac.Enabled = false;
                }
            }
        }

        private void repositoryItemTrackBar1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AxRenderControl3D.Terrain.IsRegistered)
            {
                this.AxRenderControl3D.Terrain.Opacity = double.Parse(this.beiTerrainOpac.EditValue.ToString()) / 100;
            }
        }

        private void bbiFlyToTerrain_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.AxRenderControl3D.Terrain.IsRegistered)
            {
                this.AxRenderControl3D.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
            }
        }

        private void beiTerrainOpac_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AxRenderControl3D.Terrain.IsRegistered)
            {
                this.AxRenderControl3D.Terrain.Opacity = double.Parse(this.beiTerrainOpac.EditValue.ToString()) / 100;
            }
        }

        private void bbiClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (Guid guid in allTLGuid)
            {
                this.AxRenderControl3D.ObjectManager.DeleteObject(guid);
            }
            this.allTLGuid.Clear();
        }

    }
}
