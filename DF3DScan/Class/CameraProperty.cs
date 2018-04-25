using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DScan.Class
{
    public class CameraProperty
    {
        public double X;
        public double Y;
        public double Z;
        public double Heading;
        public double Tilt;
        public double Roll;
        public bool SetCameraProperty(string str)
        {
            bool result;
            try
            {
                string[] array = str.Split(new char[]
				{
					';'
				});
                double.TryParse(array[0], out this.X);
                double.TryParse(array[1], out this.Y);
                double.TryParse(array[2], out this.Z);
                double.TryParse(array[3], out this.Heading);
                double.TryParse(array[4], out this.Tilt);
                double.TryParse(array[5], out this.Roll);
                result = true;
            }
            catch (System.Exception)
            {
                result = false;
            }
            return result;
        }
        public string PropertyStrings()
        {
            return string.Format("{0};{1};{2};{3};{4};{5}", new object[]
			{
				this.X, 
				this.Y, 
				this.Z, 
				this.Heading, 
				this.Tilt, 
				this.Roll
			});
        }
    }
}
