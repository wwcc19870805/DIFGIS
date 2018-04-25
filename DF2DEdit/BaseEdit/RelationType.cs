/*----------------------------------------------------------------
            // Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：RelationType.cs
            // 文件功能描述：相交类型
            //               
            // 
            // 创建标识：LuoXuan
            //
            // 修改描述：

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;

namespace DF2DEdit.BaseEdit
{
    public class RelationType : AbstractMap2DCommand
    {
        public override void Init(object sender)
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;

            //base.Init(sender);
            BarEditItem item = sender as BarEditItem;
            if (item.Edit is RepositoryItemComboBox)
            {
                RepositoryItemComboBox ricb = item.Edit as RepositoryItemComboBox;
                string[] scale = new string[] { "相交", "包含" };
                ricb.Items.AddRange(scale);
                //item.EditValue = ricb.Items[0];

            }
        }

        public override void Run(object sender, System.EventArgs e)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;

            ComboBoxEdit cbEdit = sender as ComboBoxEdit;
            string type = cbEdit.SelectedItem.ToString();
            if (type != null)
            {
                switch (type)
                {
                    case "相交":
                        Class.SelectionEnv.SelectRelation = 1;
                        break;
                    case "包含":
                        Class.SelectionEnv.SelectRelation = 8;
                        break;
                }
            }
            else
            {

            }
        }

    }
}
