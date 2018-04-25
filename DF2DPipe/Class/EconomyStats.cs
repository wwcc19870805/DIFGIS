using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using DF2DPipe.Stats.UC;

namespace DF2DPipe.Class
{
    public class EconomyStats
    {
        private DataTable _dt;
        //private IFeatureClass _fc;
        //private IFeatureClass _district;
        private int indexOfDisName = -1;
        private int indexOfDisArea = -1;
        private int indexOfFcArea = -1;
        private int indexOfFloorArea = -1;
        private int indexOfLength = -1;
        private Dictionary<string, List<IFeature>> _dict;
        public int IndexOfDisName
        {
            get { return this.indexOfDisName; }
            set { this.indexOfDisName = value; }
        }
        public int IndexOfDisArea
        {
            get { return this.indexOfDisArea; }
            set { this.indexOfDisArea = value; }
        }
        public int IndexOfFcArea
        {
            get { return this.indexOfFcArea; }
            set { this.indexOfFcArea = value; }
        }
        public int IndexOfFloorArea
        {
            get { return this.indexOfFloorArea; }
            set { this.indexOfFloorArea = value; }
        }
        public int IndexOfLength
        {
            get { return this.indexOfLength; }
            set { this.indexOfLength = value; }
        }
       
        
      
        public EconomyStats()
        {

        }
       
        public virtual Dictionary<string,List<IFeature>> GetStatsResult()
        {
            return _dict;
        }
        public virtual void InitDataTable()
        {

        }
        public virtual void SetFeatureClass(IFeatureClass district,IFeatureClass fc)
        {

        }
        public virtual void SetFieldInfo(List<FieldInfo> listfield) { }
        public virtual void InitUserControl(UCEconomyStatsOutput2D uc) { }
    }
}
