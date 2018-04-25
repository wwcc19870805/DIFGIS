using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DPlanData.Class
{
    public class UrbanPlan
    {
        protected Plan _property;
        protected bool _visible;

        public Plan Property
        {
            get
            {
                return this._property;
            }
            set
            {
                this._property = value;
            }
        }
        public bool Visible
        {
            get
            {
                return this._visible;
            }
            set
            {
                this._visible = value;
            }
        }

        public UrbanPlan(ref Plan plan)
        {
            this._property = plan;

        }
    }
}
