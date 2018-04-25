using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DevExpress.XtraEditors;

namespace DFDataConfig.Tree
{
    public class TreeNodeMajorClass : GroupLayerClass
    {
        public TreeNodeMajorClass()
            : this("")
        {
            base.ImageIndex = 3;
        }
        public TreeNodeMajorClass(string ID)
            : base(ID)
        {
        }
        public override void InitPopMenu()
        {
            this.AddMenuItems(new string[] { "删除" });
        }
        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "重命名":
                    Rename();
                    break;
                case "删除":
                    if (XtraMessageBox.Show("确定删除吗？", "提示") == System.Windows.Forms.DialogResult.OK)
                    {
                        Release();
                    }
                    break;
            }
        }
    }
}
