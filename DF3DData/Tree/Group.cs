using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DF3DData.Frm;
using Gvitech.CityMaker.FdeCore;
using DFWinForms.Class;
using ICSharpCode.Core;
using DF3DData.UserControl.Pad;
using DFWinForms.Service;
using DevExpress.XtraEditors;
using DF3DData.Class;

namespace DF3DData.Tree
{
    public class Group : GroupLayerClass
    {
        private bool _bTemp;
        public bool Temp
        {
            get { return _bTemp; }
            set { this._bTemp = value; }
        }
        public Group()
        {
            base.ImageIndex = 0;
        }

        public override void InitPopMenu()
        {
            base.AddMenuItems(new string[] {"新建组", ""});
            base.AddMenuItem("加载临时数据", new string[] { "加载数据库...", "加载3D瓦片...", "", "加载要素图层...", "加载影像图层...", "", "创建新的矢量图层..." });
            if(_bTemp) base.AddMenuItems(new string[] { "", "重命名", "移除"});
            base.AddMenuItems(new string[] {"", "全部展开", "全部折叠" });
        }

        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "新建组":
                    CreateGroup();
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
                case "重命名":
                    Rename();
                    break;
                case "移除":
                    Release();
                    break;
                case "全部展开":
                    this.ExpandAll();
                    break;
                case "全部折叠":
                    this.CollapseAll();
                    break;
            }
        }

        private void CreateGroup()
        {
            Group g = new Group();
            g.Name = "组";
            g.Visible = true;
            g.CustomValue = "组";
            g.Temp = true;
            this.Add(g);
        }

        private void OpenDB()
        {
            FormAdd3DDatabase dialog = new FormAdd3DDatabase();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            DataUtils.AddAndVisualizeBaseData(dialog.ConnInfo, "Geometry", this.OwnNode.TreeList, true);
        }

        private void Open3DTile()
        {
            FormAdd3DTile dlg = new FormAdd3DTile();
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            DataUtils.AddAndVisualize3DTileData(dlg.ConnInfo, dlg.Pwd, this.OwnNode.TreeList, dlg.TreeNodeName, true);
        }

    }
}
