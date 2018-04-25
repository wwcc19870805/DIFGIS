using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAlgorithm.Network
{
    public class Node
    {
        private string _id;
        private string _fcId;
        private string _featureId;
        private Dictionary<string, Node> _dictPreNodes;
        private Dictionary<string, Node> _dictNextNodes;
        private Dictionary<string, Edge> _dictPreEdges;
        private Dictionary<string, Edge> _dictNextEdges;
        public string ID
        {
            get { return this._id; }
        }
        public string FeatureClassId
        {
            get { return this._fcId; }
        }
        public string FeatureId
        {
            get { return this._featureId; }
        }
        public Node(string fcId, string featureId)
        {
            this._fcId = fcId;
            this._featureId = featureId;
            this._id = this._fcId + "_" + this._featureId;
            this._dictPreNodes = new Dictionary<string, Node>();
            this._dictNextNodes = new Dictionary<string, Node>();
            this._dictPreEdges = new Dictionary<string, Edge>();
            this._dictNextEdges = new Dictionary<string, Edge>();
        }

        public void AddPreNode(Node node)
        {
            if(!this._dictPreNodes.ContainsKey(node.ID))
                this._dictPreNodes[node.ID] = node;
        }

        public void AddNextNode(Node node)
        {
            if (!this._dictNextNodes.ContainsKey(node.ID))
                this._dictNextNodes[node.ID] = node;
        }

        public void AddPreEdge(Edge e)
        {
            if (!this._dictPreEdges.ContainsKey(e.ID))
                this._dictNextEdges[e.ID] = e;
        }

        public void AddNextEdge(Edge e)
        {
            if (!this._dictPreEdges.ContainsKey(e.ID))
                this._dictNextEdges[e.ID] = e;
        }

        public List<Node> GetPreNodes()
        {
            return this._dictPreNodes.Values.ToList<Node>();
        }

        public List<Node> GetNextNodes()
        {
            return this._dictNextNodes.Values.ToList<Node>();
        }

        public List<Edge> GetPreEdges()
        {
            return this._dictPreEdges.Values.ToList<Edge>();
        }

        public List<Edge> GetNextEdges()
        {
            return this._dictNextEdges.Values.ToList<Edge>();
        }
    }
}
