using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DEdit.Class
{
    public class CameraParamter
    {
        private double x;
        private double y;
        private double z;
        private double heading;
        private double tilt;
        private double roll;
        private bool isCameraChanged;
        public bool IsCameraChanged
        {
            get
            {
                return this.isCameraChanged;
            }
        }
        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.isCameraChanged = true;
                this.x = value;
            }
        }
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.isCameraChanged = true;
                this.y = value;
            }
        }
        public double Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.isCameraChanged = true;
                this.z = value;
            }
        }
        public double Heading
        {
            get
            {
                return this.heading;
            }
            set
            {
                this.isCameraChanged = true;
                this.heading = value;
            }
        }
        public double Tilt
        {
            get
            {
                return this.tilt;
            }
            set
            {
                this.isCameraChanged = true;
                this.tilt = value;
            }
        }
        public double Roll
        {
            get
            {
                return this.roll;
            }
            set
            {
                this.isCameraChanged = true;
                this.Roll = value;
            }
        }
        public CameraParamter()
        {
            this.isCameraChanged = false;
        }
        public CameraParamter(double x, double y, double z, double heading, double tilt, double roll)
        {
            this.isCameraChanged = true;
            this.x = x;
            this.y = y;
            this.z = z;
            this.heading = heading;
            this.tilt = tilt;
            this.roll = roll;
        }
    }
}
