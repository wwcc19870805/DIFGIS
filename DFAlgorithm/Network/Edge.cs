using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAlgorithm.Network
{
    public class Edge
    {
        private string _fcId;
        private string _featureId;
        private string _id;
        private Node _preNode;
        private Node _nextNode;
        private double _edgeLength;
        private double _weight;
        
        public string ID
        {
            get { return this._id; }
        }
        public double EdgeLength
        {
            get { return this._edgeLength; }
        }
        public double Weight
        {
            get { return this._weight; }
        }
        public Node PreNode
        {
            get { return this._preNode; }
        }
        public Node NextNode
        {
            get { return this._nextNode; }
        }
        public string FeatureClassId
        {
            get { return this._fcId; }
        }
        public string FeatureId
        {
            get { return this._featureId; }
        }

        public Edge(string fcId, string featureId,Node preNode, Node nextNode, double edgeLength, double weight = 1.0f)
        {
            this._fcId = fcId;
            this._featureId = featureId;
            this._id = preNode.ID + "_" + nextNode.ID;
            this._preNode = preNode;
            this._nextNode = nextNode;
            this._edgeLength = edgeLength;
            this._weight = weight;
            this._preNode.AddNextNode(nextNode);
            this._preNode.AddNextEdge(this);
            this._nextNode.AddPreNode(preNode);
            this._nextNode.AddPreEdge(this);
        }

    }
}
