using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using DevExpress.XtraEditors;
using DF3DData.Class;
using Gvitech.CityMaker.RenderControl;
using System.Drawing;

namespace DF3DScan.Command
{
    public class CmdTerrainOpacity : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                TrackBarControl control = sender as TrackBarControl;
                if (control != null)
                {
                    double opac = double.Parse(control.EditValue.ToString()) / 100.0;
                    app.Current3DMapControl.Terrain.Opacity = opac;
                    string temp = (opac * 255.0).ToString();
                    int index = temp.IndexOf('.');
                    if (index == -1) return;
                    string stra = temp.Substring(0, index);
                    int a = int.Parse(stra);

                    //地形模型
                    List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName("DX3DMODEL");
                    if (list != null)
                    {
                        foreach (DF3DFeatureClass dffc in list)
                        {
                            IFeatureLayer fl = dffc.GetFeatureLayer();
                            if (fl == null) continue;

                            IModelPointSymbol mps = new ModelPointSymbol();
                            Color c = System.Drawing.Color.FromArgb(a, 255, 255, 255);
                            mps.Color = (uint)c.ToArgb();
                            mps.EnableColor = true;
                            ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                            geoRender.Symbol = mps;
                            fl.SetGeometryRender(geoRender);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
