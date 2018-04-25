using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DF3DPlanData.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;

namespace DF3DPlanData.Tree
{
    //建设项目组
    public class PlanGroupLayer
        : GroupLayerClass
    {
        protected UrbanPlan _Plan;
        public UrbanPlan UPlan
        {
            get
            {
                return this._Plan;
            }
            set
            {
                this._Plan = value;
            }
        }

        public PlanGroupLayer(Plan plan)
            : base(plan.PlanGuid.ToString())
        {
            base.ImageIndex = 13;
        }

        public PlanGroupLayer(string guid) : base(guid) { }

    }
}
