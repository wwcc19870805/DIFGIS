using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAlgorithm.Network
{
    public class TopoNetworkManager
    {
        private static TopoNetworkManager instance = null;
        private static readonly object syncRoot = new object();
        private Dictionary<string, TopoNetwork> dictTopoNetwork;
        private TopoNetworkManager()
        {
            this.dictTopoNetwork = new Dictionary<string, TopoNetwork>();
        }

        public static TopoNetworkManager Instance
        {
            get
            {
                if (TopoNetworkManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (TopoNetworkManager.instance == null)
                        {
                            TopoNetworkManager.instance = new TopoNetworkManager();
                        }
                    }
                }
                return TopoNetworkManager.instance;
            }
        }

        public bool Exists(string topoObjectId)
        {
            if (dictTopoNetwork.ContainsKey(topoObjectId) && dictTopoNetwork[topoObjectId] != null) return true;
            return false;
        }

        public void Add(string topoObjectId, TopoNetwork tnw)
        {
            if (tnw == null) return;
            if (!Exists(topoObjectId)) 
            dictTopoNetwork[topoObjectId] = tnw;
        }

        public TopoNetwork GetTopoNetWorkByObjectId(string topoObjectId)
        {
            if (!Exists(topoObjectId)) return null;
            else return this.dictTopoNetwork[topoObjectId];
        }

    }
}
