using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DAnalysis.Class
{
    public class FacClassReg2D
    {
        public string OID
        {
            get { return this._OID; }
            set { this._OID = value; }
        }
        public string TopoLayerName
        {
            get { return this._TopoLayerName; }
            set { this._TopoLayerName = value; }
        }
        public string Tolerance
        {
            get { return this._Tolerance; }
            set { this._Tolerance = value; }
        }
        public string ToleranceZ
        {
            get { return this._ToleranceZ; }
            set { this._ToleranceZ = value; }
        }
        public bool IgnoreZ
        {
            get { return this._IgnoreZ; }
            set { this._IgnoreZ = value; }
        }
        public string TopoTableName
        {
            get { return this._TopoTableName; }
            set { this._TopoTableName = value; }
        }
        public string Comment
        {
            get { return this._Comment; }
            set { this._Comment = value; }
        }
        public string FeatureClassID
        {
            get { return this._FeatureClassID; }
            set { this._FeatureClassID = value; }
        }
        public string ObjectID
        {
            get { return this._ObjectID; }
            set { this._ObjectID = value; }
        }
        string _OID;
        string _TopoLayerName;
        string _Tolerance;
        string _ToleranceZ;
        bool _IgnoreZ;
        string _TopoTableName;
        string _Comment;
        string _FeatureClassID;
        string _ObjectID;
    }
}
