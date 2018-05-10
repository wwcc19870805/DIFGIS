using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Geodatabase;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using DF2DTool.Frm;

namespace DF2DTool.Command
{
    public class CmdUpdataLineMerge : AbstractMap2DCommand
    {

        IFeatureClass fc;
        IFeatureWorkspace pCurWorkspace;
        public override void Run(object sender, System.EventArgs e)
        {
            fc = GetUpdateRegionFC();
            FrmUpDatalineMerge dialog = new FrmUpDatalineMerge(pCurWorkspace as IWorkspace, fc);
            dialog.ShowDialog();
        }

        private IFeatureClass GetUpdateRegionFC()
        {
            try
            {
                string path = Config.GetConfigValue("2DMdbPipe");
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
                this.pCurWorkspace = pFWs;
                IFeatureDataset pFDs = pFWs.OpenFeatureDataset("Assi_10");
                if (pFDs == null) return null;
                IEnumDataset pEnumDs = pFDs.Subsets;
                IDataset fDs;
                IFeatureClass fc = null;
                while ((fDs = pEnumDs.Next()) != null)
                {
                    if (fDs.Name == "UpdataRegionPLY500")
                    {
                        fc = fDs as IFeatureClass;
                    }

                }
                return fc;
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }
    }
}
