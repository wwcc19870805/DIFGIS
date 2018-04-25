using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DScan.Class
{
    public class PropertyObject
    {
        private string name;
        private string comment;
        private double duration;
        private double x;
        private double y;
        private double z;
        private double heading;
        private double roll;
        private double tilt;
        public string Property1
        {
            get
            {
                return "";
            }
        }
        public string Property2
        {
            get
            {
                return "";
            }
        }
        [System.ComponentModel.Category("编辑"), System.ComponentModel.Description("特定场景名称字符数范围:1-100"), System.ComponentModel.DisplayName("名称")]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        [System.ComponentModel.Description("字符个数应在0-256之间"), System.ComponentModel.Category("编辑"), System.ComponentModel.DisplayName("说明")]
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }
        [System.ComponentModel.DisplayName("播放间隔(秒)"), System.ComponentModel.Description("取值范围:1-65535"), System.ComponentModel.Category("编辑")]
        public double Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }
        [System.ComponentModel.Category("相机位置"), System.ComponentModel.ReadOnly(true)]
        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        [System.ComponentModel.ReadOnly(true), System.ComponentModel.Category("相机位置")]
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        [System.ComponentModel.Category("相机位置"), System.ComponentModel.ReadOnly(true)]
        public double Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }
        [System.ComponentModel.ReadOnly(true), System.ComponentModel.Category("相机位置")]
        public double Heading
        {
            get
            {
                return this.heading;
            }
            set
            {
                this.heading = value;
            }
        }
        [System.ComponentModel.Category("相机位置"), System.ComponentModel.ReadOnly(true)]
        public double Roll
        {
            get
            {
                return this.roll;
            }
            set
            {
                this.roll = value;
            }
        }
        [System.ComponentModel.ReadOnly(true), System.ComponentModel.Category("相机位置")]
        public double Tilt
        {
            get
            {
                return this.tilt;
            }
            set
            {
                this.tilt = value;
            }
        }
    }
}
