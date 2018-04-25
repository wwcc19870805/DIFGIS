using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DPlanData.Tree
{
    public class ProjectLayerService
    {
        private static ProjectLayerService defaultDF3DApplication = null;
        private static readonly object syncRoot = new object();
        private ProjectLayerService()
        {
        }

        public static ProjectLayerService Instance
        {
            get
            {
                if (ProjectLayerService.defaultDF3DApplication == null)
                {
                    lock (syncRoot)
                    {
                        if (ProjectLayerService.defaultDF3DApplication == null)
                        {
                            ProjectLayerService.defaultDF3DApplication = new ProjectLayerService();
                        }
                    }
                }
                return ProjectLayerService.defaultDF3DApplication;
            }
        }
        private ProjectManagerLayer _pml;
        public ProjectManagerLayer ProjectsRoot
        {
            get { return this._pml; }
            set { this._pml = value; }
        }
        private FormallyProjectManagerLayer _fpml;
        public FormallyProjectManagerLayer FormallyProjectRoot
        {
            get { return this._fpml; }
            set { this._fpml = value; }
        }
        private CompletedProjectManagerLayer _cpml;
        public CompletedProjectManagerLayer CompletedProjectRoot
        {
            get { return this._cpml; }
            set { this._cpml = value; }
        }
    }
}
