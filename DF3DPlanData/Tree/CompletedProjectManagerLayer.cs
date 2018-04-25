using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DF3DPlanData.Class;

namespace DF3DPlanData.Tree
{
    //竣工项目组
    public class CompletedProjectManagerLayer
        : GroupLayerClass
    {
        public CompletedProjectManagerLayer()
        {
            base.ImageIndex = 13;
        }
        public  ProjectGroupLayer CreateProject(Project project)
        {
            ProjectGroupLayer layer = new ProjectGroupLayer(project.ProjectID.ToString())
            {
                Name = project.ProjectName,
                Tag = project
            };
            this.Add2(layer);
            //layer.AddPlanThemes();
            return layer;
        }

    }
}
