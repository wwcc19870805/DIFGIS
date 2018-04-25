using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraEditors;
using System.Data.SqlTypes;


namespace DFUser.Class
{
    public class AuthService
    {
        public DataTable queryAllUsers()
        {
            DataTable dt = AuthDataOper.Instance.queryAllUsers();
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            return dt;
        }

        public DataTable queryUsersByDepID(string depid)
        {
            DataTable dt = AuthDataOper.Instance.queryUsersByDepID(depid);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            return dt ;
        }

        public DataTable queryUserInfoByUserID(string userid)
        {
            DataTable dt = AuthDataOper.Instance.queryUserInfoByUserID(userid);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            return dt ;
        }
        public DataTable queryMyInfo()
        {
            DataTable dt = AuthDataOper.Instance.queryMyInfo();
            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }
            return dt;
        }

        public bool updateMyInfo(string username, string phone, string email)
        {
            username = username.Trim();
            phone = phone.Trim();
            email = email.Trim();
            bool bUpdate = AuthDataOper.Instance.updateMyInfo(username, phone, email);
            if (bUpdate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool addUser(string userid, string pwd, string username, string roleid, string company, string dep, string phone, string email, string systemtype, string systemclient,out string msg)
        {
            //string msg = string.Empty;
            if (string.IsNullOrEmpty(userid))
            {
                msg = "用户名不能为空。";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                msg = "密码不能为空。";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            if (roleid == "IsSuperAdmin")
            {
                bool bRes = AuthDataOper.Instance.addUser1(userid, pwd, username, company, dep, phone, email, out msg);
                if (bRes)
                {
                    msg = "添加用户成功";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                bool bRes = AuthDataOper.Instance.addUser(userid, pwd, username, roleid, company, dep, phone, email, systemtype, systemclient, out msg);
                if (bRes)
                {
                    msg = "添加用户成功";
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool deleteUser(string userid,out string msg)
        {
            msg = string.Empty;
            bool bRes = AuthDataOper.Instance.deleteUser(userid, out msg);
            if (bRes)
            {
                msg = "删除用户成功";
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool editUser(string userid, string pwd, string username, string roleid, string company, string dep, string phone, string email, string systemtype, string systemclient,out string msg)
        {
            //string msg = string.Empty;
            if (string.IsNullOrEmpty(userid))
            {
                msg = "用户名不能为空。";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                msg = "密码不能为空。";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            if (roleid == "IsSuperAdmin")
            {
                bool bRes = AuthDataOper.Instance.editUser1(userid, pwd, username, company, dep, phone, email, out msg);
                if (bRes)
                {
                    msg = "修改用户成功";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                bool bRes = AuthDataOper.Instance.editUser(userid, pwd, username, roleid, company, dep, phone, email, systemtype, systemclient, out msg);
                if (bRes)
                {
                    msg = "修改用户成功";
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DataTable queryAllRoles(string systemtype, string systemclient)
        {
            DataTable dt = AuthDataOper.Instance.queryAllRoles(systemtype, systemclient);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            return dt;
        }

        public DataTable queryRoleByRoleID(string roleID)
        {
            DataTable dt = AuthDataOper.Instance.queryRoleByRoleID(roleID);
            if(dt == null || dt.Rows.Count == 0) return null;
            return dt;
        }
      

        public string queryFuncsByRoleID(string roleid, string systemtype, string systemclient)
        {
            DataTable dt = AuthDataOper.Instance.queryFuncsByRoleID(roleid, systemtype, systemclient);
            if (dt == null || dt.Rows.Count != 1)
            {
                return string.Empty;
            }
            return dt.Rows[0]["FunCodes"].ToString();
        }

        public bool addRole(string rolename, string funcodes, string systemtype, string systemclient, string addin2D, string addin3D, string addinGen,out string msg)
        {
            msg = string.Empty;
            try
            {              
                if (string.IsNullOrEmpty(rolename))
                {
                    msg = "角色名不能为空。";
                    //XtraMessageBox.Show(msg, "提示");
                    return false;
                }

                int maxRoleID = AuthDataOper.Instance.getMaxRoleID(systemtype, systemclient);
                bool res1 = AuthDataOper.Instance.addRoleFunc((maxRoleID + 1).ToString(), rolename, funcodes, systemtype, systemclient,addin2D,addin3D,addinGen);
                if (res1)
                {
                    msg = "添加角色成功";
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool deleteRole(string roleid, string systemtype, string systemclient,out string msg)
        {
            msg = string.Empty;
            bool bRes = AuthDataOper.Instance.deleteRoleFunc(roleid, systemtype, systemclient);
            if (bRes)
            {
                AuthDataOper.Instance.deleteRoleIDInUserInfo(roleid, systemtype, systemclient);
                msg = "删除角色成功";
                return true;
            }
            else
            {
                msg = "删除角色失败";
                return false;
            }
        }

        public bool editRole(string roleid, string funcodes, string systemtype, string systemclient,string addin2D, string addin3D, string addinGen,out string msg)
        {
            msg = string.Empty;
            bool bRes = AuthDataOper.Instance.editRole(roleid, funcodes, systemtype, systemclient,addin2D,addin3D,addinGen);
            if (bRes)
            {
                msg = "编辑角色成功";
                return true;
            }
            else
            {
                msg = "编辑角色失败";
                return false;
            }
        }

        public DataTable queryAllDeps()
        {
            DataTable dt = AuthDataOper.Instance.queryAllDeps();
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            return dt;
        }

        public bool addDep(string depname,out string msg)
        {
            msg = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(depname))
                {
                    msg = "部门名称不能为空。";
                    //XtraMessageBox.Show(msg, "提示");
                    return false;
                }

                int maxDepID = AuthDataOper.Instance.getMaxDepID();
                bool res1 = AuthDataOper.Instance.addDep((maxDepID + 1).ToString(), depname);
                if (res1)
                {
                    msg = "添加部门成功";
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool editDep(string depid, string depname,out string msg)
        {
            msg = string.Empty;
            if (string.IsNullOrEmpty(depname))
            {
                msg = "部门名称不能为空。";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            bool bRes = AuthDataOper.Instance.editDep(depid, depname, out msg);
            if (bRes)
            {
                msg = "部门修改成功";
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteDep(string depid,out string msg)
        {
            msg = string.Empty;
            bool bRes = AuthDataOper.Instance.deleteDep(depid, out msg);
            if (bRes)
            {
                msg = "删除部门成功";
                return true;
            }
            else
            {
                return false;
            }
        }
       
        public bool changeMyPwd(string oldPwd, string newPwd1, string newPwd2,out string msg)
        {
            msg = string.Empty;
            if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd1) || string.IsNullOrEmpty(newPwd2))
            {
                msg = "密码不能为空。";
                //XtraMessageBox.Show(msg, "提示");
                return false;
            }
            bool bChangePwd = AuthDataOper.Instance.changeMyPwd(oldPwd, newPwd1, newPwd2, ref msg);
            if (bChangePwd)
            {
                msg = "修改密码成功";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
