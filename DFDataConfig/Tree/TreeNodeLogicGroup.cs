using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DFDataConfig.Frm;
using DevExpress.XtraEditors;

namespace DFDataConfig.Tree
{
    public class TreeNodeLogicGroup : GroupLayerClass
    {
        public TreeNodeLogicGroup()
            : this("")
        {
            base.ImageIndex = 0;
        }
        public TreeNodeLogicGroup(string ID)
            : base(ID)
        {
        }
        public override void InitPopMenu()
        {
            base.AddMenuItems(new string[] { "创建组", "创建大类", "", "删除", "", "全部展开", "全部折叠" });
        }
        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "创建组":
                    (this.OwnNode.TreeList.Parent.Parent.Parent.Parent as FormLogicData2DManage).CreateLogicGroup();
                    break;
                case  "创建大类":
                    (this.OwnNode.TreeList.Parent.Parent.Parent.Parent as FormLogicData2DManage).CreateMajorClass();
                    break;
                case "删除":
                    if (XtraMessageBox.Show("确定删除吗？", "提示") == System.Windows.Forms.DialogResult.OK)
                    {
                        Release();
                    }
                    break;
                case "全部展开":
                    this.ExpandAll();
                    break;
                case "全部折叠":
                    this.CollapseAll();
                    break;
            }
        }
    }
}
