using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DPlanData.Enm;

namespace DF3DPlanData.Class
{
    public class NetWorkPlan : Plan
    {
        // Methods
        public NetWorkPlan()
        {
            base.ConnType = PlanType.Network;
        }

        public NetWorkPlan(Plan plan)
        {
            base.PlanCode = plan.PlanCode;
            base.ConnType = PlanType.Network;
            base.PlanDesigner = plan.PlanDesigner;
            base.PlanGuid = plan.PlanGuid;
            base.PlanID = plan.PlanID;
            base.PlanName = plan.PlanName;
            base.PlanState = plan.PlanState;
            base.ProjectID = plan.ProjectID;
            base.XOffset = plan.XOffset;
            base.YOffset = plan.YOffset;
            base.Description = plan.Description;
            base.Creator = plan.Creator;
            base.CreateTime = plan.CreateTime;
            base.AbandonReason = plan.AbandonReason;
            base.Bound = plan.Bound;
        }
    }
}
