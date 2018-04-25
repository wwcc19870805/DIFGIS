using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DFUser.Class;
using DevExpress.XtraEditors;
using System.Data.SqlTypes;

namespace DFUser.Class
{
    public class AuthDataOper
    {
        private static AuthDataOper _do;
        private static readonly object syncRoot = new object();

        private AuthDataOper()
        {
        }

        public static AuthDataOper Instance
        {
            get
            {
                if (AuthDataOper._do == null)
                {
                    lock (syncRoot)
                    {
                        if (AuthDataOper._do == null)
                        {
                            AuthDataOper._do = new AuthDataOper();
                        }
                    }
                }
                return AuthDataOper._do;
            }
        }

        public DataTable queryAllUsers()
        {
            return queryInfo(2, null);
        }

        public DataTable queryAllRoles(string systemtype, string systemclient)
        {
            return queryInfo(3, new object[] { systemtype, systemclient });
        }

        public DataTable queryAllDeps()
        {
            return queryInfo(4, null);
        }

        public DataTable queryUsersByDepID(string depid)
        {
            return queryInfo(5, new object[] { depid });
        }

        public DataTable queryUserInfoByUserID(string userid)
        {
            return queryInfo(6, new object[] { userid });
        }

        public DataTable queryFuncsByRoleID(string roleid, string systemtype, string systemclient)
        {
            return queryInfo(7, new object[] { roleid, systemtype, systemclient });
        }


        public int getMaxRoleID(string systemtype, string systemclient)
        {
            DataTable dt = queryInfo(8, new object[] { systemtype, systemclient });
            if (dt == null || dt.Rows.Count == 0) return 0;
            return int.Parse(dt.Rows[0]["num"].ToString());
        }
        public int getMaxDepID()
        {
            DataTable dt = queryInfo(12, null);
            if (dt == null || dt.Rows.Count == 0) return 0;
            return int.Parse(dt.Rows[0]["num"].ToString());
        }
        public DataTable queryMyInfo()
        {
            return queryInfo(9, null);
        }
        public DataTable queryRoleByRoleID(string roleID)
        {
            DataTable dt =  queryInfo(13, new object[] { roleID });
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt;
        }
        public string queryCurrentFunCodes(string systemtype, string systemclient)
        {
            DataTable dt = queryInfo(10, new object[] { systemtype, systemclient });
            if (dt == null || dt.Rows.Count == 0) return "";
            return dt.Rows[0]["FunCodes"].ToString();
        }
       
        private DataTable queryInfo(int type, params object[] strs)
        {
            if (string.IsNullOrEmpty(UserInfo.Instance.UserID)) return null;
            DataTable dt = new DataTable();
            string sql = "";
            switch (type)
            {
                case 1:
                    sql = "select * from UserInfo where UserID='" + strs[0] + "'";
                    break;
                case 2:
                    sql = "select * from UserInfo";
                    break;
                case 3:
                    sql = "select * from RoleFunc where SystemType = '" + strs[0] + "' and SystemClient ='" + strs[1] + "'";
                    break;
                case 4:
                    sql = "select * from Dep";
                    break;
                case 5:
                    sql = "select * from UserInfo where DepID = '" + strs[0] + "'";
                    break;
                case 6:
                    sql = "select * from UserInfo where UserID = '" + strs[0] + "'";
                    break;
                case 7:
                    sql = "select FunCodes from RoleFunc where RoleID='" + strs[0] + "' and SystemType ='" + strs[1] + "' and SystemClient = '" + strs[2] + "'";
                    break;
                case 8:
                    sql = "select top (1) num from (select cast(RoleID as int) as num  from RoleFunc where SystemType ='" + strs[0] + "' and SystemClient = '" + strs[1] + "') as A order by num desc";
                    break;
                case 9:
                    sql = "select UserID,UserName,Company,DepName,Phone,Email from UserInfo left JOIN Dep on UserInfo.DepID=Dep.DepID where UserID = '" + UserInfo.Instance.UserID + "'";
                    break;
                case 10:
                    sql = "select FunCodes from UserInfo LEFT OUTER JOIN RoleFunc on RoleFunc.RoleID = UserInfo.RoleID" + strs[0] + strs[1] + " where UserID = '" + UserInfo.Instance.UserID + "' and SystemType ='" + strs[0] + "' and SystemClient = '" + strs[1] + "'";
                    break;
                case 11:
                    sql = "select * from Dep where DepID ='" + strs[0] + "'";
                    break;
                case 12:
                    sql = "select top (1) num from (select cast(DepID as int) as num  from Dep ) as A order by num desc";
                    break;
                case 13:
                    sql = "select * from RoleFunc where RoleID ='" + strs[0] + "'";
                    break;
            }
            bool bQuery = DB.Instance.queryDB(sql, ref dt);
            if (!bQuery) return null;
            return dt;
        }
        public bool editUser1(string userid, string pwd, string username,  string company, string dep, string phone, string email, out string msg)
        {
            msg = "网络错误，添加失败。";
            DataTable dt = queryInfo(1, new object[] { userid });
            if (dt == null || dt.Rows.Count == 0)
            {
                msg = "该用户不存在。";
                return false;
            }
            return updateInfo(6, new object[] { userid, pwd, username, company, dep, phone, email });
        }


        public bool editUser(string userid, string pwd, string username, string roleid, string company, string dep, string phone, string email, string systemtype,string systemclient, out string msg)
        {
            msg = "网络错误，添加失败。";
            DataTable dt = queryInfo(1, new object[] { userid });
            if (dt == null || dt.Rows.Count == 0)
            {
                msg = "该用户不存在。";
                return false;
            }
            return updateInfo(1, new object[] { userid, pwd, username, roleid, company, dep, phone, email , systemtype, systemclient});
        }

        public bool editRole(string roleid, string funcodes, string systemtype, string systemclient, string addin2D, string addin3D, string addinGen)
        {
            return updateInfo(8, new object[] { roleid, funcodes, systemtype, systemclient ,addin2D,addin3D,addinGen});
        }

       

        public bool changeMyPwd(string oldPwd, string newPwd1, string newPwd2, ref string msg)
        {
            if (string.IsNullOrEmpty(UserInfo.Instance.UserID))
            {
                msg = "用户未登录！";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            string pwd = UserInfo.Instance.Pwd;
            if (pwd != oldPwd)
            {
                msg = "旧的密码输入错误！";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            else if (newPwd1 != newPwd2)
            {
                msg = "新的密码两次输入不一致！";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            else
            {
                bool bRet = updateInfo(3, new object[] { newPwd1 });
                if (bRet) UserInfo.Instance.Pwd = newPwd1;
                return bRet;
            }
        }

        public bool updateMyInfo(string username, string phone, string email)
        {
            return updateInfo(4, new object[] { username, phone, email });
        }

        public bool editDep(string depid,string depname,out string msg)
        {
            msg = "网络错误，添加失败。";
            DataTable dt = queryInfo(11, new object[] { depid });
            if (dt == null || dt.Rows.Count == 0)
            {
                msg = "该部门不存在。";
                return false;
            }
            return updateInfo(5, new object[] { depid, depname });
        }

        private bool updateInfo(int type, params object[] strs)
        {
            if (string.IsNullOrEmpty(UserInfo.Instance.UserID)) return false;
            string sql = "";
            switch (type)
            {
                case 1:
                    sql = "update UserInfo set Pwd='" + strs[1] + "',UserName='" + strs[2] + "',RoleIDCS ='" + strs[3]
                        + "',Company='" + strs[4] + "',DepID='" + strs[5] + "',Phone='" + strs[6] + "',Email='" + strs[7]
                        + "',IsSuperAdmin=0 where UserID='" + strs[0] + "'";
                    break;
                case 2:
                    sql = "update RoleFunc set FunCodes = '" + strs[1] +
                        "' where RoleID = '" + strs[0] + "' and SystemType = '" + strs[2] + "' and SystemClient = '" + strs[3] + "'";
                    break;
                case 3:
                    sql = "update UserInfo set Pwd='" + strs[0].ToString() + "' where UserID = '" + UserInfo.Instance.UserID + "'";
                    break;
                case 4:
                    sql = "update UserInfo set UserName = '" + strs[0].ToString() + "',Phone='" + strs[1].ToString() + "',Email='" + strs[2].ToString() + "'  where UserID = '" + string.IsNullOrEmpty(UserInfo.Instance.UserID) + "'";
                    break;
                case 5:
                    sql = "update Dep set DepName ='" + strs[1].ToString() + "' where DepID='" + strs[0].ToString() + "'";
                    break;
                case 6:
                    sql = "update UserInfo set Pwd='" + strs[1] + "',UserName='" + strs[2] + "',Company='" + strs[3] + "',DepID='" + strs[4] + "',Phone='" + strs[5] + "',Email='" + strs[6]
                        + "',IsSuperAdmin=1,RoleID2DBS=null,RoleID3DBS=null,RoleIDCS=null where UserID='" + strs[0] + "'";
                    break;
                case 7:
                    sql = "update UserInfo set RoleID" + strs[1] + strs[2] + "=NULL where RoleID" + strs[1] + strs[2] + "='" + strs[0]+"'";
                    break;
                case 8:
                    sql = "update RoleFunc set Menu2D = '" + strs[4].ToString() + "',Menu3D = '" + strs[5].ToString() + "',General = '" + strs[6].ToString() + "' where RoleID = '" + strs[0].ToString() + "'";
                    break;
            }
            return DB.Instance.noqueryDB(sql);
        }
        public bool addUser1(string userid, string pwd, string username, string company, string dep, string phone, string email, out string msg)
        {
            msg = "网络错误，添加失败。";
            DataTable dt = queryInfo(1, new object[] { userid });
            if (dt != null && dt.Rows.Count > 0)
            {
                msg = "该用户已存在。";
                return false;
            }
            return insertInfo(4, new object[] { userid, pwd, username, company, dep, phone, email });
        }
        public bool addUser(string userid, string pwd, string username, string roleid, string company, string dep, string phone, string email,string systemtype, string systemclient, out string msg)
        {
            msg = "网络错误，添加失败。";
            DataTable dt = queryInfo(1, new object[] { userid });
            if (dt != null && dt.Rows.Count > 0)
            {
                msg = "该用户已存在。";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            return insertInfo(1, new object[] { userid, pwd, username, roleid, company, dep, phone, email, systemtype, systemclient });
        }

        public bool addRoleFunc(string roleid, string rolename, string funcodes, string systemtype, string systemclient,string addin2D,string addin3D,string addinGen)
        {
            return insertInfo(2, new object[] { roleid, rolename, funcodes, systemtype, systemclient, addin2D,addin3D,addinGen });
        }
        public bool addDep(string depid, string depname)
        {
            return insertInfo(3, new object[] { depid, depname });
        }
        private bool insertInfo(int type, params object[] strs)
        {
            if (string.IsNullOrEmpty(UserInfo.Instance.UserID)) return false;
            string sql = "";
            switch (type)
            {
                case 1:
                    sql = "insert into UserInfo(UserID,Pwd,UserName,RoleIDCS,Company,DepID,Phone,Email,IsSuperAdmin) values ('"
                        + strs[0] + "','" + strs[1] + "','" + strs[2] + "','" + strs[3] + "','" + strs[4] + "','"
                        + strs[5] + "','" + strs[6] + "','" + strs[7] + "',0)";
                    break;
                case 2:
                    sql = "insert into RoleFunc(RoleID,RoleName,FunCodes,SystemType,SystemClient,Menu2D,Menu3D,General) values ('"
                        + strs[0] + "','" + strs[1] + "','" + strs[2] + "','" + strs[3] + "','" + strs[4] + "','" + strs[5] + "','" + strs[6] + "','" + strs[7] +  "')";
                    break;
                case 3:
                    sql = "insert into Dep(DepID,DepName) values ('"
                        + strs[0] + "','" + strs[1] + "')";
                    break;
                case 4:
                    sql = "insert into UserInfo(UserID,Pwd,UserName,IsSuperAdmin,Company,DepID,Phone,Email,RoleIDCS) values ('"
                        + strs[0] + "','" + strs[1] + "','" + strs[2] + "','" + "1" + "','" + strs[3] + "','"
                        + strs[4] + "','" + strs[5] + "','" + strs[6] + "','" + null  + "')";             
                    break;
            }
            return DB.Instance.noqueryDB(sql);
        }

        public bool deleteUser(string userid, out string msg)
        {
            msg = "网络错误，添加失败。";
            DataTable dt = queryInfo(1, new object[] { userid });
            if (dt == null || dt.Rows.Count == 0)
            {
                msg = "该用户不存在。";
                return false;
            }
            return deleteInfo(1, new object[] { userid });
        }

        public bool deleteRoleFunc(string roleid, string systemtype, string systemclient)
        {
            return deleteInfo(2, new object[] { roleid, systemtype, systemclient });
        }
        public bool deleteRoleIDInUserInfo(string roleid, string systemtype, string systemclient)
        {
            return updateInfo(7, new object[] { roleid, systemtype, systemclient });
        }
        public bool deleteDep(string depid, out string msg)
        {
            msg = "网络错误，添加失败。";
            DataTable dt = queryInfo(11, new object[] { depid });
            if (dt == null || dt.Rows.Count == 0)
            {
                msg = "该部门不存在。";
                XtraMessageBox.Show(msg, "提示");
                return false;
            }
            return deleteInfo(3, new object[] { depid });
        }

        public bool deleteInfo(int type, params object[] strs)
        {
            if (string.IsNullOrEmpty(UserInfo.Instance.UserID)) return false;
            string sql = "";
            switch (type)
            {
                case 1:
                    sql = "delete from UserInfo where UserID = '" + strs[0] + "'";
                    break;
                case 2:
                    sql = "delete from RoleFunc where RoleID = '" + strs[0] + "' and SystemType = '" + strs[1] + "' and SystemClient = '" + strs[2] + "'";
                    break;
                case 3:
                    sql="delete from Dep where DepID = '" + strs[0] + "'";
                    break;
                default: break;
            }
            return DB.Instance.noqueryDB(sql);
        }
    }
}
