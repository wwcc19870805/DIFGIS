using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFWinForms.Service;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DF2DData.Class;
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
    public class CmdScale: AbstractMap2DCommand
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
                string[] scale = new string[] { "1:500", "1:1000", "1:2000", "1:5000", "1:10000", "1:20000" };
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
                    case "1:500":
                        app.Current2DMapControl.MapScale = 500;
                        break;
                    case "1:1000":
                        app.Current2DMapControl.MapScale = 1000;
                        break;
                    case "1:2000":
                        app.Current2DMapControl.MapScale = 2000;
                        break;
                    case "1:5000":
                        app.Current2DMapControl.MapScale = 5000;
                        break;
                    case "1:10000":
                        app.Current2DMapControl.MapScale = 10000;
                        break;
                    case "1:20000":
                        app.Current2DMapControl.MapScale = 20000;
                        break;
                }
                app.Current2DMapControl.Refresh();

            }
            else
            {
                
            }
            

                        
        }

        public override void OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            try
            {
                Layer2DTreePad wsPad = (Layer2DTreePad)UCService.GetContent(typeof(Layer2DTreePad));
                string lyrName;
                TreeListNodes treeListNodes = wsPad.TreeList.Nodes;
                IMap map = app.Current2DMapControl.Map;
                for (int i = 0; i < map.LayerCount;i++ )
                {
                    ILayer lyr = map.get_Layer(i);
                    lyrName = lyr.Name;
                    bool _visible = lyr.Visible;
                
                    UpdateTreeList(lyrName, treeListNodes,wsPad.TreeList,_visible);
                        
                    //TreeListNodes tlns = treeList.Nodes;
                    //foreach (TreeListNode tln in tlns)
                    //{
                    //    string tlnName = tln.ToString();
                    //    if (tlnName == lyrName)
                    //    {
                    //        tln.Visible = false;
                    //    }
                    //    if (tln.Nodes != null)
                    //    {

                    //    }
                                
                            
                    //}


                        
                  
                }
                
            }
            catch (System.Exception ex)
            {
            	
            }

        }

        private void UpdateTreeList(string lyrName,TreeListNodes treeListNodes,TreeList treeList,bool _visible)
        {


            if (_visible)
            {
                foreach (TreeListNode tln in treeListNodes)
                {
                    string tlnName = tln.ToString();
                    if (tlnName == lyrName)
                    {
                        tln.Visible = true;
                        treeList.RefreshNode(tln);
                    }
                    if (tln.Nodes != null)
                    {
                        TreeListNodes tlns = tln.Nodes;
                        UpdateTreeList(lyrName, tlns, treeList,true);
                    }
                    else return;

                }
            }
            else
            {
                foreach (TreeListNode tln in treeListNodes)
                {
                    string tlnName = tln.ToString();
                    if (tlnName == lyrName)
                    {
                        tln.Visible = false;
                        treeList.RefreshNode(tln);
                    }
                    if (tln.Nodes != null)
                    {
                        TreeListNodes tlns = tln.Nodes;
                        UpdateTreeList(lyrName, tlns, treeList, false);
                    }
                    else return;

                }
            }

           

        }


    }
}
