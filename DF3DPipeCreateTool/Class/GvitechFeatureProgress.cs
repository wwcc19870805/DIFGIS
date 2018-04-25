using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;

namespace DF3DPipeCreateTool.Class
{
    public class GvitechFeatureProgress
    {
        // Fields
        private IFeatureProgress _iFeatureProgress;

        // Methods
        public GvitechFeatureProgress(IFeatureProgress iFeatureProgress)
        {
            this._iFeatureProgress = iFeatureProgress;
        }

        // Properties
        public int CurrentFeatureCount
        {
            get
            {
                return this._iFeatureProgress.CurrentFeatureCount;
            }
        }

        public string FeatureOwner
        {
            get
            {
                return this._iFeatureProgress.FeatureOwner;
            }
        }

        public IFeatureProgress IFeatureProgress
        {
            get
            {
                return this._iFeatureProgress;
            }
            set
            {
                this._iFeatureProgress = value;
            }
        }

        public long InternalObject
        {
            get
            {
                return this._iFeatureProgress.InternalObject;
            }
            set
            {
                this._iFeatureProgress.InternalObject = value;
            }
        }

        public bool IsNull
        {
            get
            {
                if (this._iFeatureProgress != null)
                {
                    return false;
                }
                return true;
            }
        }

        public int OperationCount
        {
            get
            {
                return this._iFeatureProgress.OperationCount;
            }
        }

        public gviReplicateOperation[] Operations
        {
            get
            {
                return this._iFeatureProgress.Operations;
            }
        }

        public int TotalFeatureCount
        {
            get
            {
                return this._iFeatureProgress.TotalFeatureCount;
            }
        }
    }
}
