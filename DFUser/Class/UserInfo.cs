using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DFUser.Class
{
    public class UserInfo
    {
        private static UserInfo instance = null;
        private static readonly object syncRoot = new object();

        public static UserInfo Instance
        {
            get
            {
                if (UserInfo.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (UserInfo.instance == null)
                        {
                            UserInfo.instance = new UserInfo();
                        }
                    }
                }
                return UserInfo.instance;
            }
        }

        private string userId;
        private string userName;
        private string pwd;
        private string depName;
        private string sex;
        private string mobile;
        private bool isSuperAdmin;
        public string UserID
        {
            get { return userId; }
            set { userId = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        public bool IsSuperAdmin
        {
            get { return isSuperAdmin; }
            set { isSuperAdmin = value; }
        }
    }
}
