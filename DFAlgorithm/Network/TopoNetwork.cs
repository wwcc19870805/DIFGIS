using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAlgorithm.Network
{
    public class TopoNetwork
    {
        private string _id;
        private Dictionary<string, Node> _dictNodes;

        public TopoNetwork(string id, Dictionary<string, Node> dictNodes)
        {
            this._id = id;
            this._dictNodes = dictNodes;
        }

        public double SPFA(string startId, string endId, out List<string> shortestPath)
        {
            shortestPath = new List<string>();

            if (this._dictNodes == null) return double.MaxValue;
            if (!this._dictNodes.ContainsKey(startId) || !this._dictNodes.ContainsKey(endId)) return double.MaxValue;

            Dictionary<string, bool> dictMark = new Dictionary<string, bool>();
            Dictionary<string, double> dictDis = new Dictionary<string, double>();
            Dictionary<string, int> dictNum = new Dictionary<string, int>();
            Dictionary<string, string> dictPre = new Dictionary<string, string>();
            foreach (string nodeid in this._dictNodes.Keys)
            {
                dictMark[nodeid] = false;
                dictDis[nodeid] = double.MaxValue;
                dictNum[nodeid] = 0;
                dictPre[nodeid] = "";
            }

            dictMark[startId] = true;
            dictDis[startId] = 0.0f;
            dictNum[startId]++;

            Queue<string> queue = new Queue<string>();
            queue.Enqueue(startId);

            while (queue.Count != 0)
            {
                string nodeId = queue.Dequeue();
                dictMark[nodeId] = false;
                dictNum[nodeId]++;
                if (dictNum[nodeId] > this._dictNodes.Count)
                    return double.MaxValue;

                Node nn = NodeManager.Instance.GetNodeByID(nodeId);
                if (nn == null) return double.MaxValue;
                List<Node> nextNodes = nn.GetNextNodes();
                foreach (Node nextNode in nextNodes)
                {
                    string nodeidtemp = nextNode.ID;
                    double d12, w12;
                    EdgeManager.Instance.GetNoneDirDistanceAndWeight(nodeId, nodeidtemp, out d12, out w12);
                    if (d12 == double.MaxValue) continue;
                    if (dictDis[nodeidtemp] > dictDis[nodeId] + w12 * d12)
                    {
                        dictDis[nodeidtemp] = dictDis[nodeId] + w12 * d12;
                        dictPre[nodeidtemp] = nodeId;
                        if (!dictMark[nodeidtemp])
                        {
                            queue.Enqueue(nodeidtemp);
                            dictMark[nodeidtemp] = true;
                        }
                    }
                }
                List<Node> preNodes = nn.GetPreNodes();
                foreach (Node nextNode in preNodes)
                {
                    string nodeidtemp = nextNode.ID;
                    double d12, w12;
                    EdgeManager.Instance.GetNoneDirDistanceAndWeight(nodeId, nodeidtemp, out d12, out w12);
                    if (d12 == double.MaxValue) continue;
                    if (dictDis[nodeidtemp] > dictDis[nodeId] + w12 * d12)
                    {
                        dictDis[nodeidtemp] = dictDis[nodeId] + w12 * d12;
                        dictPre[nodeidtemp] = nodeId;
                        if (!dictMark[nodeidtemp])
                        {
                            queue.Enqueue(nodeidtemp);
                            dictMark[nodeidtemp] = true;
                        }
                    }
                }
            }

            if (dictDis[endId] == double.MaxValue) return dictDis[endId];
            shortestPath.Add(endId);
            string now = dictPre[endId];
            while(true)
            {
                shortestPath.Add(now);
                if (now == startId)
                    break;
                now = dictPre[now];
            }
            return dictDis[endId];
        }

        private void RecursiveBGFX(Node node, HashSet<string> valveIds, ref HashSet<string> record, Dictionary<string, bool> dictMark,int type)
        {
            try
            {
                if (node == null || valveIds == null || valveIds.Count == 0) return;
                TimeSpan ts = DateTime.Now.Subtract(this._startBGFX);
                if (ts.TotalSeconds > 60) return;
                if (type == 0)
                {
                    if (dictMark[node.ID]) return;
                    foreach (Node n in node.GetPreNodes())
                    {
                        if (valveIds.Contains(n.ID))
                        {
                            record.Add(n.ID);
                            dictMark[n.ID] = true;
                        }
                        else RecursiveBGFX(n, valveIds, ref record, dictMark, 0);
                    }
                    dictMark[node.ID] = true;
                }
                if (type == 1)
                {
                    if (dictMark[node.ID]) return;
                    foreach (Node n in node.GetNextNodes())
                    {
                        if (valveIds.Contains(n.ID))
                        {
                            record.Add(n.ID);
                            dictMark[n.ID] = true;
                        }
                        else RecursiveBGFX(n, valveIds, ref record, dictMark, 1);
                    }
                    dictMark[node.ID] = true;
                }

            }
            catch (System.Exception ex)
            {
                return;
            }
           
        }
        private DateTime _startBGFX;
        public void BGFX(string startId, string endId, HashSet<string> valveIds, ref HashSet<string> recordPre, ref HashSet<string> recordNext)
        {
            if (string.IsNullOrEmpty(startId) || string.IsNullOrEmpty(endId) || valveIds == null || valveIds.Count == 0) return;
            this._startBGFX = DateTime.Now;
            
            Dictionary<string, bool> dictPreMark = new Dictionary<string, bool>();
            foreach (string nodeid in this._dictNodes.Keys)
            {
                dictPreMark[nodeid] = false;
            }
            Dictionary<string, bool> dictNextMark = new Dictionary<string, bool>();
            foreach (string nodeid in this._dictNodes.Keys)
            {
                dictNextMark[nodeid] = false;
            }
            Node nodeStart = NodeManager.Instance.GetNodeByID(startId);
            if (valveIds.Contains(nodeStart.ID)) recordPre.Add(nodeStart.ID);
            else RecursiveBGFX(nodeStart, valveIds, ref recordPre, dictPreMark, 0);
            Node nodeEnd = NodeManager.Instance.GetNodeByID(endId);
            if (valveIds.Contains(nodeEnd.ID)) recordNext.Add(nodeEnd.ID);
            else RecursiveBGFX(nodeEnd, valveIds, ref recordNext, dictNextMark, 1);
        }

    }
}
