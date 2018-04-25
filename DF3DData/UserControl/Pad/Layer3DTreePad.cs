using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DFWinForms.LogicTree;
using ICSharpCode.Core;
using DFWinForms.UserControl;
using DF3DData.Tree;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.FdeCore;
using DF3DData.Class;
using DF3DControl.Base;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using DevExpress.XtraTreeList;
using Gvitech.CityMaker.Math;
using DF3DData.Frm;
using DFWinForms.Class;
using DevExpress.XtraEditors;

namespace DF3DData.UserControl.Pad
{
    public class Layer3DTreePad : LogicBaseTree
    {
        public Layer3DTreePad()
        {
            this.OnLayerDoubleClick += new OnLayerDoubleClickHandler<IBaseLayer>(Layer3DTreePad_OnLayerDoubleClick);
        }

        private void Layer3DTreePad_OnLayerDoubleClick(IBaseLayer layer)
        {
            if (layer is DF3DData.Tree.ICamera) (layer as DF3DData.Tree.ICamera).FlyTo();
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
            // Layer3DTreePad
            // 
            this.Name = "Layer3DTreePad";
            this.Size = new System.Drawing.Size(227, 484);
            ((System.ComponentModel.ISupportInitialize)(this.preViewPictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        public override void InitPopMenu()
        {
            base.AddMenuItem("新建组");
            base.AddMenuItem("");
            //base.AddMenuItem("加载临时数据", new string[] { "加载数据库...", "加载3D瓦片...", "", "加载要素图层...", "加载影像图层...", "", "创建新的矢量图层..." });
            base.AddMenuItem("加载临时数据", new string[] { "加载数据库...", "加载3D瓦片..." });
            base.AddMenuItems(new string[] { "", "全部展开", "全部折叠" });
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
                case "加载3D瓦片...":
                    Open3DTile();
                    break;
                case "全部展开":
                    base._baseTree.ExpandAll();
                    break;
                case "全部折叠":
                    base._baseTree.CollapseAll();
                    break;
            }
        }

        private void CreateGroup()
        {
            Group g = new Group();
            g.LogicTree = this;
            g.OwnNode = base._baseTree.AppendNode(new object[] { "新建组" }, (TreeListNode)null);
            g.Name = "组";
            g.Visible = true;
            g.CustomValue = "组";
        }

        private void OpenDB()
        {
            FormAdd3DDatabase dialog = new FormAdd3DDatabase();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            DataUtils.AddAndVisualizeBaseData(dialog.ConnInfo, "Geometry", this._baseTree, true);
        }
        private void Open3DTile()
        {
            FormAdd3DTile dlg = new FormAdd3DTile();
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            DataUtils.AddAndVisualize3DTileData(dlg.ConnInfo, dlg.Pwd, this._baseTree, dlg.TreeNodeName, true);
        }

    }
}
