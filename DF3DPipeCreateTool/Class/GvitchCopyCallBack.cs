using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;

namespace DF3DPipeCreateTool.Class
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class GvitchCopyCallBack
    {
        private CopyReplicatingHandler _RepHandler;
        public event CopyReplicatingHandler Replicating
        {
            add
            {
                this._RepHandler = (CopyReplicatingHandler)System.Delegate.Combine(this._RepHandler, new CopyReplicatingHandler(value.Invoke));
            }
            remove
            {
                this._RepHandler = (CopyReplicatingHandler)System.Delegate.Remove(this._RepHandler, new CopyReplicatingHandler(value.Invoke));
            }
        }
        public bool OnReplicating(IFeatureProgress Progress)
        {
            return this._RepHandler != null && this._RepHandler(new GvitechFeatureProgress(Progress));
        }
        public bool OnProcessing(IFeatureProgress Progress)
        {
            return this._RepHandler != null && this._RepHandler(new GvitechFeatureProgress(Progress));
        }
    }
}
