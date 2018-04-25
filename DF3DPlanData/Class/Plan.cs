using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DPlanData.Enm;

namespace DF3DPlanData.Class
{
    public class Plan
    {
        // Fields
        private string _abandonReason;
        private string _bound;
        private PlanType _connType = PlanType.Network;
        private DateTime _createTime;
        private string _creator;
        private string _description;
        private string _planCode;
        private string _planDesigner;
        private string _planGuid;
        private int _planID;
        private string _planName;
        private PlanState _planState;
        private int _projectID;
        private double _xOffset;
        private double _yOffset;

        // Methods
        public Plan Clone()
        {
            if (this.ConnType == PlanType.Network)
            {
                return new NetWorkPlan(this);
            }
            return new LocalPlan(this);
        }

        // Properties
        public string AbandonReason
        {
            get
            {
                return this._abandonReason;
            }
            set
            {
                this._abandonReason = value;
            }
        }

        public string Bound
        {
            get
            {
                return this._bound;
            }
            set
            {
                this._bound = value;
            }
        }

        public PlanType ConnType
        {
            get
            {
                return this._connType;
            }
            set
            {
                this._connType = value;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return this._createTime;
            }
            set
            {
                this._createTime = value;
            }
        }

        public string Creator
        {
            get
            {
                return this._creator;
            }
            set
            {
                this._creator = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string PlanCode
        {
            get
            {
                return this._planCode;
            }
            set
            {
                this._planCode = value;
            }
        }

        public string PlanDesigner
        {
            get
            {
                return this._planDesigner;
            }
            set
            {
                this._planDesigner = value;
            }
        }

        public string PlanGuid
        {
            get
            {
                return this._planGuid;
            }
            set
            {
                this._planGuid = value;
            }
        }

        public int PlanID
        {
            get
            {
                return this._planID;
            }
            set
            {
                this._planID = value;
            }
        }

        public string PlanName
        {
            get
            {
                return this._planName;
            }
            set
            {
                this._planName = value;
            }
        }

        public PlanState PlanState
        {
            get
            {
                return this._planState;
            }
            set
            {
                this._planState = value;
            }
        }

        public int ProjectID
        {
            get
            {
                return this._projectID;
            }
            set
            {
                this._projectID = value;
            }
        }

        public double XOffset
        {
            get
            {
                return this._xOffset;
            }
            set
            {
                this._xOffset = value;
            }
        }

        public double YOffset
        {
            get
            {
                return this._yOffset;
            }
            set
            {
                this._yOffset = value;
            }
        }
    }
}
