using System.Text;
using DFWinForms.LogicTree;
using ESRI.ArcGIS.Carto;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;
using DF2DData.Class;

namespace DF2DDataTopological.LogicTree
{
    class TreeNodeSubClassTopo : BaseLayerClass
    {
        private DF2DFeatureClass dffc;
        public DF2DFeatureClass Dffc
        {
            get { return this.dffc; }
            set { this.dffc = value; }
        }
        public TreeNodeSubClassTopo()
            : this("")
        {
            base.ImageIndex = 3;
        }
        public TreeNodeSubClassTopo(string ID)
            : base(ID)
        {
        }
    }
}
