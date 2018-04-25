using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DF3DPlanData.Class;

namespace DF3DPlanData.Tree
{
    //建设项目组
    public class ProjectManagerLayer
        : GroupLayerClass
    {
        public ProjectManagerLayer()
        {
            base.ImageIndex = 13;
        }

        public virtual ProjectGroupLayer CreateProject(Project project)
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
