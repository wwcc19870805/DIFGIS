using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFAlgorithm.Network;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using DFCommon.Class;
using DF2DAnalysis.Class;
using ESRI.ArcGIS.Geometry;


namespace DF2DAnalysis.Class
{
    public class TopoClass2D
    {
        private string _comment;
        private int _id;
        private bool _ignoreZ;         // 是否启用Z容差值 
        private string _name;
        private string _objectId;    // 编码
        private double _tolerance;     // XY容差值
        private double _toleranceZ;    // Z容差值
        private string _topotable;     // 拓扑表名

        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        public string ObjectId
        {
            get
            {
                return this._objectId;
            }
            set
            {
                this._objectId = value;
            }
        }
        public bool IgnoreZ
        {
            get
            {
                return this._ignoreZ;
            }
            set
            {
                this._ignoreZ = value;
            }
        }
        public double Tolerance
        {
            get
            {
                return this._tolerance;
            }
            set
            {
                this._tolerance = value;
            }
        }
        public double ToleranceZ
        {
            get
            {
                return this._toleranceZ;
            }
            set
            {
                this._toleranceZ = value;
            }
        }
        public string Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }
        public string TopoTable
        {
            get
            {
                return this._topotable;
            }
            set
            {
                this._topotable = value;
            }
        }
        public override string ToString()
        {
            return this._name;
        }

        public TopoNetwork GetNetwork()
        {
            if (TopoNetworkManager.Instance.Exists(this._objectId)) return TopoNetworkManager.Instance.GetTopoNetWorkByObjectId(this._objectId);
            string path = Config.GetConfigValue("2DMdbTopo");
            TopoNetwork network = null;
            IFeatureClass featureClass = null;
            IQueryFilter filter = null;
            IFeatureCursor cursor = null;
            IFeature feature = null;
            try
            {
                
                IWorkspaceFactory pWsFt = new AccessWorkspaceFactory();
                IFeatureWorkspace pWs = pWsFt.OpenFromFile(path, 0) as IFeatureWorkspace;
                featureClass = pWs.OpenFeatureClass(_name);
                filter = new QueryFilter();
                filter.SubFields = "A_FC,Edge,P_FC,PNode,E_FC,ENode,Geometry_Length";
                int index1 = featureClass.Fields.FindField("A_FC");
                int index2 = featureClass.Fields.FindField("Edge");
                int index3 = featureClass.Fields.FindField("P_FC");
                int index4 = featureClass.Fields.FindField("PNode");
                int index5 = featureClass.Fields.FindField("E_FC");
                int index6 = featureClass.Fields.FindField("ENode");
                int index7 = featureClass.Fields.FindField("Geometry_Length");

                Dictionary<string, Edge> dictEdge = new Dictionary<string, Edge>();  // 边字典
                Dictionary<string, Node> dictNode = new Dictionary<string, Node>();  // 点字典
                cursor = featureClass.Search(filter, false);
                while ((feature = cursor.NextFeature()) != null)
                {
                    string edgeFC, edgeOid, snodeFC, snodeOid, enodeFC, enodeOid;

                    if (feature.get_Value(index1) != null)
                        edgeFC = feature.get_Value(index1).ToString();
                    else edgeFC = "0";
                    if (feature.get_Value(index2) != null)
                        edgeOid = feature.get_Value(index2).ToString();
                    else edgeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                    if (feature.get_Value(index3) != null)
                        snodeFC = feature.get_Value(index3).ToString();
                    else snodeFC = "0";
                    if (feature.get_Value(index4) != null)
                        snodeOid = feature.get_Value(index4).ToString();
                    else snodeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                    if (feature.get_Value(index5) != null)
                        enodeFC = feature.get_Value(index5).ToString();
                    else enodeFC = "0";
                    if (feature.get_Value(index6) != null)
                        enodeOid = feature.get_Value(index6).ToString();
                    else enodeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();

                    double edgeLength = double.MaxValue;
                    if (feature.get_Value(index7) != null)
                    {
                        edgeLength = (double)feature.get_Value(index7);
                    }
                    //if (feature.get_Value(index7) != null && feature.get_Value(index7) is IPolyline)
                    //{
                    //    IPolyline line = feature.get_Value(index7) as IPolyline;
                    //    edgeLength = line.Length;
                    //}

                    Node sn = null;
                    Node en = null;
                    string key = snodeFC + "_" + snodeOid;
                    if (!dictNode.ContainsKey(key))
                    {
                        if (NodeManager.Instance.GetNodeByID(key) == null)
                        {
                            sn = new Node(snodeFC, snodeOid);
                            NodeManager.Instance.Add(sn);
                        }
                        else { sn = NodeManager.Instance.GetNodeByID(key); }
                        dictNode.Add(key, sn);
                    }
                    else sn = dictNode[key];
                    key = enodeFC + "_" + enodeOid;
                    if (!dictNode.ContainsKey(key))
                    {
                        if (NodeManager.Instance.GetNodeByID(key) == null)
                        {
                            en = new Node(enodeFC, enodeOid);
                            NodeManager.Instance.Add(en);
                        }
                        else { en = NodeManager.Instance.GetNodeByID(key); }
                        dictNode.Add(key, en);
                    }
                    else en = dictNode[key];
                    if (sn == null || en == null) continue;
                    key = edgeFC + "_" + edgeOid;
                    if (!dictEdge.ContainsKey(key))
                    {
                        Edge e = null;
                        if (EdgeManager.Instance.GetEdgeByID(key) == null)
                        {
                            e = new Edge(edgeFC, edgeOid, sn, en, edgeLength);
                            EdgeManager.Instance.Add(e);
                        }
                        else { e = EdgeManager.Instance.GetEdgeByID(key); }
                        dictEdge.Add(key, e);
                    }
                }
                network = new TopoNetwork(this._objectId, dictNode);
                TopoNetworkManager.Instance.Add(this.ObjectId, network);
                return network;
            }
                
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (feature != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(feature);
                    feature = null;
                }
            }
           
        }
    }
}
