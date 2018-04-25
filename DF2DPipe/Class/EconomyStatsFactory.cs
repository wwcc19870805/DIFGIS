using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DPipe.Class
{
    public class EconomyStatsFactory
    {
        public static EconomyStats CreateEconomyStats(string statsType)
        {
            EconomyStats economyStats = null;
            switch (statsType)
            {
                case"Building":
                    economyStats = new BuildingStats();
                    break;
                case "Structure":
                    economyStats = new StructureStats();
                    break;
                case "Road":
                    economyStats = new RoadStats();
                    break;
                case "Railway":
                    economyStats = new RailwayStats();
                    break;
                case "Green":
                    economyStats = new GreenStats();
                    break;
            }
            return economyStats;
        }

    }
}
