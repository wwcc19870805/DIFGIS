using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DFUser.Class;

namespace DFUser.Service
{
    class AuthService
    {
        public static DataTable queryAllRoles(string systemtype, string systemclient)
        {
            DataTable dt = AuthDataOper.Instance.queryAllRoles(systemtype, systemclient);
            return dt;
        }

    }
}
