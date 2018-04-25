using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAlgorithm.Network
{
    public class ValveManager
    {
        private static ValveManager instance = null;
        private static readonly object syncRoot = new object();       
        private Dictionary<string, HashSet<string>> dictValveIds;

        private ValveManager()
        {
            dictValveIds = new Dictionary<string, HashSet<string>>();
           
        }
        public static ValveManager Instance
        {
            get
            {
                if (ValveManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (ValveManager.instance == null)
                        {
                            ValveManager.instance = new ValveManager();
                        }
                    }
                }
                return ValveManager.instance;
            }
        }


        public bool Exists(string fcId)
        {
            if (dictValveIds.ContainsKey(fcId) && dictValveIds[fcId] != null) return true;
            return false;
        }

        public void Add(string fcId, HashSet<string> valveIds)
        {
            if (string.IsNullOrEmpty(fcId)) return;
            if (!Exists(fcId))
            {
                dictValveIds[fcId] = valveIds;
            }
        }

        public HashSet<string> GetValveIds(string fcId)
        {
            if (this.dictValveIds.ContainsKey(fcId) && this.dictValveIds[fcId] != null)
                return this.dictValveIds[fcId];
            else
                return null;
        }

    }
}
