using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DPlanData.Enm;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPlanData.Class
{
    public class Project
    {
        // Fields
        private ProjectChangeState _changeStage;
        private string _description;
        private string _projectBound;
        private string _projectCode;
        private double _projectElevation;
        private int _projectID;
        private IGeometry _projectLandUse;
        private string _projectName;
        private string _projectOwner;
        private IBinaryBuffer _projectQuo;
        private string _projectRemark1;
        private string _projectRemark2;
        private DateTime _projectTime;
        private ProjectType _projectType;

        // Methods
        public static Project Clone(Project oldPrj)
        {
            if (oldPrj == null)
            {
                return null;
            }
            return new Project { ProjectOwner = oldPrj.ProjectOwner, ProjectRemark1 = oldPrj.ProjectRemark1, ProjectLocation = oldPrj.ProjectLocation, ProjectTime = oldPrj.ProjectTime, ProjectType = oldPrj.ProjectType, ProjectLandUse = oldPrj.ProjectLandUse, Description = oldPrj.Description, ProjectBound = oldPrj.ProjectBound, ProjectElevation = oldPrj.ProjectElevation, ProjectID = oldPrj.ProjectID, ProjectCode = oldPrj.ProjectCode, ProjectName = oldPrj.ProjectName };
        }

        public override string ToString()
        {
            return this._projectName;
        }

        // Properties
        public ProjectChangeState ChangeState
        {
            get
            {
                return this._changeStage;
            }
            set
            {
                this._changeStage = value;
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

        public string ProjectBound
        {
            get
            {
                return this._projectBound;
            }
            set
            {
                this._projectBound = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this._projectCode;
            }
            set
            {
                this._projectCode = value;
            }
        }

        public double ProjectElevation
        {
            get
            {
                return this._projectElevation;
            }
            set
            {
                this._projectElevation = value;
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

        public IGeometry ProjectLandUse
        {
            get
            {
                return this._projectLandUse;
            }
            set
            {
                this._projectLandUse = value;
            }
        }

        public string ProjectLocation
        {
            get
            {
                return this._projectRemark2;
            }
            set
            {
                this._projectRemark2 = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return this._projectName;
            }
            set
            {
                this._projectName = value;
            }
        }

        public string ProjectOwner
        {
            get
            {
                return this._projectOwner;
            }
            set
            {
                this._projectOwner = value;
            }
        }

        public IBinaryBuffer ProjectQuo
        {
            get
            {
                return this._projectQuo;
            }
            set
            {
                this._projectQuo = value;
            }
        }

        public string ProjectRemark1
        {
            get
            {
                return this._projectRemark1;
            }
            set
            {
                this._projectRemark1 = value;
            }
        }

        public DateTime ProjectTime
        {
            get
            {
                return this._projectTime;
            }
            set
            {
                this._projectTime = value;
            }
        }

        public ProjectType ProjectType
        {
            get
            {
                return this._projectType;
            }
            set
            {
                this._projectType = value;
            }
        }
    }
}
