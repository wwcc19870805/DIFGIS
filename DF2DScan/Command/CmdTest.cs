using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DF2DData.Class;
using DFWinForms.Service;
using DFDataConfig.Class;
using System.Windows.Forms;
using DFWinForms.Component;
using DevExpress.XtraEditors;
using DF2DData.UserControl.Pad;
using ESRI.ArcGIS.Carto;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;

namespace DF2DScan.Command
{
    public class CmdTest : AbstractMap2DCommand
    {
        public override void Init(object sender)
        {
            BarEditItem item = sender as BarEditItem;
            if (item.Edit is RepositoryItemComboBox)
            {
                RepositoryItemComboBox ricb = item.Edit as RepositoryItemComboBox;
                string[] scale = new string[] { "1:500", "1:1000", "1:2000", "1:5000", "1:10000", "1:20000" };
                ricb.Items.AddRange(scale);
                item.EditValue = ricb.Items[0];

            }

            Map2DView mapView = (Map2DView)UCService.GetContent(typeof(Map2DView));
            mapView.Bind(this);
        }

        public override void Run(object sender, System.EventArgs e)
        {
        }

        public override void OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
        }
    }
}
