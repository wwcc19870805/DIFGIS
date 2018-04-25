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
    public class BufferUnit : AbstractMap2DCommand
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
                string[] scale = new string[] { "米", "千米", "英尺", "英里" };
                ricb.Items.AddRange(scale);
                //item.EditValue = ricb.Items[0];

            }
        }

        public override void Run(object sender, System.EventArgs e)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;

            ComboBoxEdit cbEdit = sender as ComboBoxEdit;
            string scale = cbEdit.SelectedItem.ToString();
            if (scale != null)
            {
                switch (scale)
                {
                    case "米":
                        app.Current2DMapControl.MapScale = 500;
                        break;
                    case "千米":
                        app.Current2DMapControl.MapScale = 1000;
                        break;
                    case "英尺":
                        app.Current2DMapControl.MapScale = 2000;
                        break;
                    case "英里":
                        app.Current2DMapControl.MapScale = 5000;
                        break;
                }
            }
            else
            {

            }
        }
    }
}
