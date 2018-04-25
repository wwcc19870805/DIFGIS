using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DevExpress.XtraEditors;

namespace DFDataConfig.Tree
{
    public class TreeNodeFacilityClass : BaseLayerClass
    {
        public TreeNodeFacilityClass()
            : this("")
        {
            base.ImageIndex = 2;
        }

        public TreeNodeFacilityClass(string ID)
            : base(ID)
        {
        }

        public override void InitPopMenu()
        {
            this.AddMenuItems(new string[] { "重命名", "删除" });
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
