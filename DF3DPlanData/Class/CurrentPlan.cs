using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DPlanData.Class
{
    public class CurrentPlan
    {
        // Methods
        public CurrentPlan()
        {
        }

        public CurrentPlan(string datasourceGuid, string projectId, string planId)
        {
            this.DataSourceGuid = datasourceGuid;
            this.ProjectId = projectId;
            this.PlanId = planId;
        }

        // Properties
        public string DataSourceGuid { get; set; }

        public string PlanId { get; set; }

        public string ProjectId { get; set; }
    }
}
