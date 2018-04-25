using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using DFAlgorithm.Network;

namespace DF3DPipeCreateTool.Class
{
    public class TopoClass
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

            if (DF3DPipeCreateApp.App.PipeLib == null) return null;
            TopoNetwork network = null;
            IFeatureClass class2 = null;
            IQueryFilter filter = null;
            IFdeCursor cursor = null;
            IRowBuffer buffer = null;
            try
            {

                IFeatureDataSet fds = DF3DPipeCreateApp.App.PipeLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                class2 = fds.OpenFeatureClass(this._topotable);
                filter = new QueryFilterClass
                {
                    SubFields = "A_FacClass,Edge,P_FacClass,PNode,E_FacClass,ENode,Geometry"
                };

                Dictionary<string, Node> dictNode = new Dictionary<string, Node>();  // 点字典
                int totalcount = class2.GetCount(null);
                int loop = (int)Math.Ceiling((decimal)(totalcount / 1000.0));
                for (int i = 1; i <= loop; i++)
                {
                    if (i == 1)
                    {
                        filter.ResultBeginIndex = 0;
                    }
                    else
                    {
                        filter.ResultBeginIndex = (i - 1) * 1000;
                    }
                    filter.ResultLimit = 1000;
                    cursor = class2.Search(filter, true);
                    while ((buffer = cursor.NextRow()) != null)
                    {
                        string edgeFC, edgeOid, snodeFC, snodeOid, enodeFC, enodeOid;
                        if (!buffer.IsNull(0))
                            edgeFC = buffer.GetValue(0).ToString();
                        else edgeFC = "0";

                        if (!buffer.IsNull(1))
                            edgeOid = buffer.GetValue(1).ToString();
                        else edgeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();

                        if (!buffer.IsNull(2))
                            snodeFC = buffer.GetValue(2).ToString();
                        else snodeFC = "0";

                        if (!buffer.IsNull(3))
                            snodeOid = buffer.GetValue(3).ToString();
                        else snodeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();

                        if (!buffer.IsNull(4))
                            enodeFC = buffer.GetValue(4).ToString();
                        else enodeFC = "0";

                        if (!buffer.IsNull(5))
                            enodeOid = buffer.GetValue(5).ToString();
                        else enodeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();

                        double edgeLength = double.MaxValue;
                        if (!buffer.IsNull(6) && buffer.GetValue(6) is IPolyline)
                        {
                            IPolyline line = buffer.GetValue(6) as IPolyline;
                            edgeLength = line.Length;
                        }

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
                        Edge e = new Edge(edgeFC, edgeOid, sn, en, edgeLength);
                        EdgeManager.Instance.Add(e);
                    }
                }
                network = new TopoNetwork(this._objectId, dictNode);
                TopoNetworkManager.Instance.Add(this.ObjectId, network);
                return network;
            }
            catch (Exception ex)
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
                if (buffer != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(buffer);
                    buffer = null;
                }
            }

        }

        //public TopoNetwork GetNetWork(IPolygon region)
        //{
        //    if (TopoNetworkManager.Instance.Exists(this._objectId)) return TopoNetworkManager.Instance.GetTopoNetWorkByObjectId(this._objectId);

        //    if (DF3DPipeCreateApp.App.PipeLib == null) return null;
        //    TopoNetwork network = null;
        //    IFeatureClass class2 = null;
        //    IQueryFilter filter = null;
        //    IFdeCursor cursor = null;
        //    IRowBuffer buffer = null;
        //    try
        //    {
                
        //        IFeatureDataSet fds = DF3DPipeCreateApp.App.PipeLib.OpenFeatureDataset("DataSet_BIZ");
        //        if (fds == null) return null;
        //        class2 = fds.OpenFeatureClass(this._topotable);
        //        if (region != null)
        //        {
        //            ISpatialFilter filter2 = null;
        //            filter2 = new SpatialFilterClass
        //            {
        //                SubFields = "A_FacClass,Edge,P_FacClass,PNode,E_FacClass,ENode",
        //                SpatialRel = gviSpatialRel.gviSpatialRelIntersects,
        //                GeometryField = "Geometry",
        //                Geometry = region.Clone2(gviVertexAttribute.gviVertexAttributeNone)
        //            };
        //            filter = filter2;
        //        }
        //        else
        //        {
        //            filter = new QueryFilterClass
        //            {
        //                SubFields = "A_FacClass,Edge,P_FacClass,PNode,E_FacClass,ENode"
        //            };
        //        }

        //        Dictionary<string, Edge> dictEdge = new Dictionary<string, Edge>();  // 边字典
        //        Dictionary<string, Vertex> dictVtx = new Dictionary<string, Vertex>();  // 点字典
        //        int totalcount = class2.GetCount(null);
        //        int loop = (int)Math.Ceiling((decimal)(totalcount / 1000.0));
        //        for (int i = 1; i <= loop; i++)
        //        {
        //            if (i == 1)
        //            {
        //                filter.ResultBeginIndex = 0;
        //            }
        //            else
        //            {
        //                filter.ResultBeginIndex = (i - 1) * 1000;
        //            }
        //            filter.ResultLimit = 1000;
        //            cursor = class2.Search(filter, true);
        //            while ((buffer = cursor.NextRow()) != null)
        //            {
        //                string edgeFC, edgeOid, snodeFC, snodeOid, enodeFC, enodeOid;
        //                if (!buffer.IsNull(0))
        //                    edgeFC = buffer.GetValue(0).ToString();
        //                else edgeFC = "0";

        //                if (!buffer.IsNull(1))
        //                    edgeOid = buffer.GetValue(1).ToString();
        //                else edgeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();

        //                if (!buffer.IsNull(2))
        //                    snodeFC = buffer.GetValue(2).ToString();
        //                else snodeFC = "0";

        //                if (!buffer.IsNull(3))
        //                    snodeOid = buffer.GetValue(3).ToString();
        //                else snodeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();

        //                if (!buffer.IsNull(4))
        //                    enodeFC = buffer.GetValue(4).ToString();
        //                else enodeFC = "0";

        //                if (!buffer.IsNull(5))
        //                    enodeOid = buffer.GetValue(5).ToString();
        //                else enodeOid = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();

        //                Vertex sv = null;
        //                Vertex ev = null;
        //                string key = snodeFC + "_" + snodeOid;
        //                if (!dictVtx.ContainsKey(key))
        //                {
        //                    sv = new Vertex(snodeFC, snodeOid);
        //                    dictVtx.Add(key, sv);
        //                }
        //                else sv = dictVtx[key];
        //                key = enodeFC + "_" + enodeOid;
        //                if (!dictVtx.ContainsKey(key))
        //                {
        //                    ev = new Vertex(enodeFC, enodeOid);
        //                    dictVtx.Add(key, ev);
        //                }
        //                else ev = dictVtx[key];
        //                key = edgeFC + "_" + edgeOid;
        //                if (!dictEdge.ContainsKey(key))
        //                {
        //                    Edge e = new Edge(edgeFC, edgeOid, sv, ev);
        //                    dictEdge.Add(key, e);
        //                }

        //            }
        //        }
        //        network = new TopoNetwork(dictVtx);
        //        if ((dictEdge != null) && (dictEdge.Count > 0))
        //        {
        //            foreach (KeyValuePair<string, Edge> pair in dictEdge)
        //            {
        //                network.SetEdge(pair.Value);
        //            }
        //        }
        //        TopoNetworkManager.Instance.Add(this.ObjectId, network);
        //        return network;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if (cursor != null)
        //        {
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
        //            cursor = null;
        //        }
        //        if (buffer != null)
        //        {
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(buffer);
        //            buffer = null;
        //        }
        //    }
        //}
        
    }
}
