using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;
using DFWinForms.LogicTree;
using ESRI.ArcGIS.Geodatabase;
using DF2DData.Tree;
using ESRI.ArcGIS.Carto;
namespace DF2DData.Class
{
    class DF2DMajorClassManager
    {
        private static DF2DMajorClassManager instance = null;
        private static readonly object syncRoot = new object();
        private List<DF2DMajorClass> listMC;


        private DF2DMajorClassManager()
        {
            this.listMC = new List<DF2DMajorClass>();            
        }

        public static DF2DMajorClassManager Instance
        {
            get
            {
                if (DF2DMajorClassManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (DF2DMajorClassManager.instance == null)
                        {
                            DF2DMajorClassManager.instance = new DF2DMajorClassManager();
                        }
                    }
                }
                return DF2DMajorClassManager.instance;
            }
        }
        public bool Exists(string name)
        {
            foreach (DF2DMajorClass mc in this.listMC)
            {
                if (mc.GetMajorClass().Name == name)
                {
                    return true;
                }

            }
            return false;
        }
        public void Add(DF2DMajorClass mc)
        {
            if (this.Exists(mc.GetMajorClass().Name)) return;
            this.listMC.Add(mc);
        }

        public List<DF2DMajorClass> GetAllMajorClass()
        {
            return this.listMC;
        }

        public DF2DMajorClass GetDFMCByFeatureClassID(string id)
        {
            foreach (DF2DMajorClass dfmc in listMC)
            {
                string[] arrayFc2D = dfmc.GetMajorClass().Fc2D.Split(';');
                foreach (string fcID in arrayFc2D)
                {
                    if (fcID == id) return dfmc;
                }
            }
            return null;
        }
    }
}
