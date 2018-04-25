using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAlgorithm.Network
{
    public class EdgeManager
    {
        private static EdgeManager instance = null;
        private static readonly object syncRoot = new object();
        private Dictionary<string, Edge> dict;
        private Dictionary<string, Edge> dictFCIDAndOID;
        private EdgeManager()
        {
            dict = new Dictionary<string, Edge>();
            dictFCIDAndOID = new Dictionary<string, Edge>();
        }

        public static EdgeManager Instance
        {
            get
            {
                if (EdgeManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (EdgeManager.instance == null)
                        {
                            EdgeManager.instance = new EdgeManager();
                        }
                    }
                }
                return EdgeManager.instance;
            }
        }
        public int Count
        {
            get { return this.dict.Count; }
        }

        public bool Exists(string id)
        {
            if (dict.ContainsKey(id) && dict[id] != null) return true;
            return false;
        }

        public bool Exists(string fcID, string oid)
        {
            string temp = fcID + "_" + oid;
            if (dictFCIDAndOID.ContainsKey(temp) && dictFCIDAndOID[temp] != null) return true;
            return false;
        }

        public void Add(Edge e)
        {
            if (e == null) return;
            if (!Exists(e.ID))
            {
                dict[e.ID] = e;
            }
            if (!Exists(e.FeatureClassId, e.FeatureId))
            {
                string temp = e.FeatureClassId + "_" + e.FeatureId;
                dictFCIDAndOID[temp] = e;
            }
        }

        public Edge GetEdgeByID(string fcID, string oid)
        {
            if (!Exists(fcID, oid)) return null;
            else return this.dictFCIDAndOID[fcID + "_" + oid];
        }

        public Edge GetEdgeByID(string id)
        {
            if (!Exists(id)) return null;
            else return this.dict[id];
        }

        public Node GetPreNodeByEdgeID(string id)
        {
            if (!Exists(id)) return null;
            else return this.dict[id].PreNode;
        }

        public Node GetNextNodeByEdgeID(string id)
        {
            if (!Exists(id)) return null;
            else return this.dict[id].NextNode;
        }

        public Edge GetEdgeByPreAndNextNode(Node preNode, Node nextNode)
        {
            if (this.dict == null || preNode == null || nextNode == null) return null;
            if (!this.dict.ContainsKey(preNode.ID + "_" + nextNode.ID)) return null;
            return this.dict[preNode.ID + "_" + nextNode.ID];
        }

        public Edge GetEdgeByPreAndNextNodeID(string preNodeID, string nextNodeID)
        {
            if (this.dict == null) return null;
            if (!this.dict.ContainsKey(preNodeID + "_" + nextNodeID)) return null;
            return this.dict[preNodeID + "_" + nextNodeID];
        }

        public void GetDirDistanceAndWeight(string startNodeID, string endNodeID, out double dis, out double weight)//有向边
        {
            dis = double.MaxValue;
            weight = 1.0f;
            if (startNodeID == endNodeID)
            {
                dis = 0.0;
                return;
            }
            Edge e = GetEdgeByPreAndNextNodeID(startNodeID, endNodeID);
            if (e != null) {  dis = e.EdgeLength; weight = e.Weight; } 

        }

        public void GetNoneDirDistanceAndWeight(string nodeID1, string nodeID2, out double dis, out double weight)//无向边
        {
            dis = double.MaxValue;
            weight = 1.0f;
            if (nodeID1 == nodeID2)
            {
                dis = 0.0;
                return;
            }
            bool bHave1 = false;
            bool bHave2 = false;
            double disTemp1 = double.MaxValue;
            double disTemp2 = double.MaxValue;
            double weight1 = 1.0f;
            double weight2 = 1.0f;
            Edge e1 = GetEdgeByPreAndNextNodeID(nodeID1, nodeID2);
            if (e1 != null) { bHave1 = true; if (disTemp1 > e1.EdgeLength) { disTemp1 = e1.EdgeLength; weight1 = e1.Weight; } }
            Edge e2 = GetEdgeByPreAndNextNodeID(nodeID2, nodeID1);
            if (e2 != null) { bHave2 = true; if (disTemp2 > e2.EdgeLength) { disTemp2 = e2.EdgeLength; weight2 = e2.Weight; } }

            if (bHave1 && bHave2 && Math.Abs(disTemp1 - disTemp2) < 0.0001)
            {
                dis = disTemp1;
                weight = weight1;
            }
            else
            {
                dis = Math.Min(disTemp1, disTemp2);
                weight = Math.Min(weight1, weight2);
            }
        }
    }
}
