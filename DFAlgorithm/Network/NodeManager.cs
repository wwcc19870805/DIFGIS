using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAlgorithm.Network
{
    public class NodeManager
    {
        private static NodeManager instance = null;
        private static readonly object syncRoot = new object();
        private Dictionary<string, Node> dict;

        private NodeManager()
        {
            dict = new Dictionary<string, Node>();
        }

        public static NodeManager Instance
        {
            get
            {
                if (NodeManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (NodeManager.instance == null)
                        {
                            NodeManager.instance = new NodeManager();
                        }
                    }
                }
                return NodeManager.instance;
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

        public void Add(Node e)
        {
            if (e == null) return;
            if (!Exists(e.ID))
                dict[e.ID] = e;
        }

        public Node GetNodeByID(string id)
        {
            if (!Exists(id)) return null;
            else return this.dict[id];
        }

        public List<Node> GetPreNodesByNodeID(string id)
        {
            if (!Exists(id)) return null;
            Node node = GetNodeByID(id);
            return node.GetPreNodes();
        }

        public List<Node> GetNextNodesByNodeID(string id)
        {
            if (!Exists(id)) return null;
            Node node = GetNodeByID(id);
            return node.GetNextNodes();
        }

        public List<Edge> GetEdgesByPreNodeID(string id)
        {
            if (!Exists(id)) return null;
            Node node = GetNodeByID(id);
            return node.GetPreEdges();
        }

        public List<Edge> GetEdgesByNextNodeID(string id)
        {
            if (!Exists(id)) return null;
            Node node = GetNodeByID(id);
            return node.GetNextEdges();
        }
    }
}
