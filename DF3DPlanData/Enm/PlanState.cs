using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DPlanData.Enm
{
    public enum PlanState
    {
        None = 0,
        Abandon = -1,
        AllowPublic = 10,
        Completed = 9,
        Formally = 8,
        New = 1,
        Next = 3,
        Pass = 4,
        Reject = 6,
        Repairing = 2,
        Revoke = 7,
        ShowtoPublic = 11,
        Tentative = 5
    }
}
