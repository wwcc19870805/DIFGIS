using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using DFWinForms.UserControl;
using DF2DControl.Base;
using ICSharpCode.Core;
using DF2DData.Class;
using DFWinForms.LogicTree;
using DF2DData.Tree;
using DF2DData.LogicTree;
using ESRI.ArcGIS.DataSourcesFile;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;

namespace DF2DData.UserControl.Pad
{
    public class Layer2DTreePad : LogicBaseTree
    {
    
        public Layer2DTreePad()
        {
            this.OnLayerDoubleClick += new OnLayerDoubleClickHandler<IBaseLayer>(Layer2DTreePad_OnLayerDoubleClick);
            this.OnLayerCheckedStateChangedByMouseClick += new OnLayerCheckedStateChangedByMouseClickHandler<IBaseLayer>(Layer2DTreePad_OnLayerCheckedStateChangedByMouseClick);
        }

        private void Layer2DTreePad_OnLayerCheckedStateChangedByMouseClick(IBaseLayer layer)
        {           
               LayerCheckOnOffControl(layer);
        
        }
        private void LayerCheckOnOffControl(IBaseLayer layer)
        {
            if (layer is TreeNodeComLayer)
            {
                TreeNodeComLayer node = layer as TreeNodeComLayer;
                node.CheckOn = node.Visible;
                List<IBaseLayer> childNodes = node.SelectAllSubLayers();
                if (childNodes.Count > 0)
                {
                    foreach (IBaseLayer cn in childNodes)
                    {
                        LayerCheckOnOffControl(cn);
                    }
                }           
            }
            else if (layer is TreeNodeLogicGroup2D)
            {
                TreeNodeLogicGroup2D lg = layer as TreeNodeLogicGroup2D;
                List<IBaseLayer> listBL = lg.SelectAllSubLayers();
                if (listBL.Count > 0)
                {
                    foreach (IBaseLayer bl in listBL)
                    {
                        LayerCheckOnOffControl(bl);
                    }
                }
            }
            else if (layer is TreeNodeMajorClass2D)
            {
                TreeNodeMajorClass2D node = layer as TreeNodeMajorClass2D;
                node.CheckOn = node.Visible;
                List<IBaseLayer> childNodes = node.SelectAllSubLayers();
                if (childNodes.Count > 0)
                {
                    foreach (IBaseLayer cn in childNodes)
                    {
                        LayerCheckOnOffControl(cn);
                    }
                }
            }
            else if (layer is TreeNodeSubClass2D)
            {
                TreeNodeSubClass2D node = layer as TreeNodeSubClass2D;
                node.CheckOn = node.Visible;
            }
        }
        private void Layer2DTreePad_OnLayerDoubleClick(IBaseLayer layer)
        {
            //if (layer is DF3DData.Tree.ICamera) (layer as DF3DData.Tree.ICamera).FlyTo();
        }
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.preViewPictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // preViewPictureEdit
            // 
            this.preViewPictureEdit.Location = new System.Drawing.Point(5, 341);
            this.preViewPictureEdit.Size = new System.Drawing.Size(217, 107);
            // 
            // treeListColumn_color
            // 
            this.treeListColumn_color.OptionsColumn.AllowEdit = false;
            this.treeListColumn_color.OptionsColumn.AllowSize = false;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
            // 
            // Layer2DTreePad
            // 
            this.AllowDragLayer = false;
            this.Name = "Layer2DTreePad";
            this.Size = new System.Drawing.Size(227, 484);
            this.OnLayerCheckedStateChangedByMouseClick += new DFWinForms.LogicTree.OnLayerCheckedStateChangedByMouseClickHandler<DFWinForms.LogicTree.IBaseLayer>(this.Layer2DTreePad_OnLayerCheckedStateChangedByMouseClick);          
            ((System.ComponentModel.ISupportInitialize)(this.preViewPictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        public override void InitPopMenu()
        {
            //base.AddMenuItem("新建组");
            //base.AddMenuItem("");
            base.AddMenuItem("加载临时数据", new string[] { "加载Cad图层..."});
            base.AddMenuItems(new string[] { "", "全部展开", "全部折叠" });
            base.AddMenuItem("移除Cad图层");
        }

        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "新建组":
                    CreateGroup();
                    break;
                case "加载FDB...":
                    break;
                case "加载数据库...":
                    OpenDB();
                    break;
                case "加载要素图层...":
                    break;
                case "加载影像图层...":
                    break;
                case "加载Cad图层...":
                    AddCadLayer();
                    break;
                case "移除Cad图层":
                    RemoveCadLayers();
                    break;
                //case "加载3D瓦片...":
                //    Open3DTile();
                //    break;
                case "全部展开":
                    base._baseTree.ExpandAll();
                    break;
                case "全部折叠":
                    base._baseTree.CollapseAll();
                    break;
            }
        }

        private void RemoveCadLayers()
        {
            if(_dictCadLyr == null||_dictCadLyr.Count == 0)return;
            DF2DApplication app = DF2DApplication.Application;
            foreach (KeyValuePair<TreeListNode,ICadLayer> kv in _dictCadLyr)
            {
                _baseTree.Nodes.Remove(kv.Key);
                app.Current2DMapControl.Map.DeleteLayer(kv.Value);
            }
            _dictCadLyr.Clear();
           
        }
        private void CreateGroup()
        {
            //Group g = new Group();
            //g.LogicTree = this;
            //g.OwnNode = base._baseTree.AppendNode(new object[] { "新建组" }, (TreeListNode)null);
            //g.Name = "组";
            //g.Visible = true;
            //g.CustomValue = "组";
        }

        private void OpenDB()
        {
            //    FormAdd3DDatabase dialog = new FormAdd3DDatabase();
            //    if (dialog.ShowDialog() != DialogResult.OK)
            //        return;
            //    DataUtils.AddAndVisualizeBaseData(dialog.ConnInfo, "Geometry", this._baseTree, true);
            //}
            //private void Open3DTile()
            //{
            //    FormAdd3DTile dlg = new FormAdd3DTile();
            //    if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //        return;
            //    DataUtils.AddAndVisualize3DTileData(dlg.ConnInfo, dlg.Pwd, this._baseTree, dlg.TreeNodeName, true);
            //}
        }

        Dictionary<TreeListNode, ICadLayer> _dictCadLyr;
        private void AddCadLayer()
        {           
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "AutoCad file(*.dxf)|*.dxf";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string cadFile = ofd.FileName;
                string cadName = System.IO.Path.GetFileName(cadFile);
                string cadDirec = System.IO.Path.GetDirectoryName(cadFile);
                try
                {
                    ICadDrawingDataset cadDs = GetCadDataset(cadDirec, cadName);
                    if (cadDs == null)
                    {
                        XtraMessageBox.Show("打开Cad文件失败", "提示");
                        return;
                    } 
                    ICadLayer cadLayer = new CadLayerClass();
                    cadLayer.CadDrawingDataset = cadDs;
                    cadLayer.Name = cadName;
                    DF2DApplication app = DF2DApplication.Application;
                    if (_dictCadLyr == null)
                    {
                        _dictCadLyr = new Dictionary<TreeListNode, ICadLayer>();
                    }
                    TreeListNode node = AddCadNodeToTreeList(cadLayer);
                    if (node == null) return;
                    _dictCadLyr[node] = cadLayer;     
                    app.Current2DMapControl.Map.AddLayer(cadLayer);
                    app.Current2DMapControl.ActiveView.Refresh();              
                }
                catch (System.Exception ex)
                {
                	
                }
            }
        }

        private ICadDrawingDataset GetCadDataset(string cadWorkspacePath, string cadFileName)
        {
            //Create a WorkspaceName object
            IWorkspaceName workspaceName = new WorkspaceNameClass();
            workspaceName.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory";
            workspaceName.PathName = cadWorkspacePath;

            //Create a CadDrawingName object
            IDatasetName cadDatasetName = new CadDrawingNameClass();
            cadDatasetName.Name = cadFileName;
            cadDatasetName.WorkspaceName = workspaceName;

            //Open the CAD drawing
            ESRI.ArcGIS.esriSystem.IName name = (ESRI.ArcGIS.esriSystem.IName)cadDatasetName;
            return (ICadDrawingDataset)name.Open();
        }
        
        private TreeListNode AddCadNodeToTreeList(ICadLayer layer)
        {
            if (_dictCadLyr.ContainsValue(layer)) { XtraMessageBox.Show("该图层已添加，请勿重复添加该图层!"); return null; }
            TreeNodeComLayer comLayerNode = new TreeNodeComLayer() { Name = layer.Name, CustomValue = layer };
            comLayerNode.OwnNode = _baseTree.AppendNode(new object[] { comLayerNode.Name }, null);//为树创建根节点
            comLayerNode.ImageIndex = 0;
            comLayerNode.Visible = true;
            return comLayerNode.OwnNode;
  
        }
  

      

    }
}
