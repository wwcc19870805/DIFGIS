using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFUser.Class;
using System.Data;
using System.IO;
using ICSharpCode.Core;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

namespace DFUser.Class
{
    public static class AddInReplace
    {
      
        public static void Replace(string userID)
        {
            try
            {
                string xmlPath2D = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\Menu2D.addin");
                string xmlPath3D = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\Menu3D.addin");
                string xmlPathGeneral = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\General.addin");
                AuthService authService = new AuthService();
                DataTable dtUser = authService.queryUserInfoByUserID(userID);
                if (dtUser == null || dtUser.Rows.Count == 0) return;
                string roleID = dtUser.Rows[0]["RoleIDCS"].ToString();
                DataTable dtRole = authService.queryRoleByRoleID(roleID);
                if (dtRole == null || dtRole.Rows.Count == 0) return;
                string addin2D = dtRole.Rows[0]["Menu2D"].ToString();
                string addin3D = dtRole.Rows[0]["Menu3D"].ToString();
                string addinGen = dtRole.Rows[0]["General"].ToString();
                //SqlXml[] sqlXml = authService.querySqlXmlByRoleID(roleID);
                //if (sqlXml == null) return;
                //string addin2D = sqlXml[0].Value;
                //string addin3D = sqlXml[1].Value;
                //string addinGen = sqlXml[2].Value;
                CreateAddIn(addin2D, xmlPath2D);
                CreateAddIn(addin3D, xmlPath3D);
                CreateAddIn(addinGen, xmlPathGeneral);
               

            }
            catch (System.Exception ex)
            {
            	
            }
            

            
        }
        private static void CreateAddIn(string strxml,string xmlpath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strxml);
            if (File.Exists(xmlpath)) File.Delete(xmlpath);
            doc.Save(xmlpath);
            
        }
        public static void Copy()
        {
            try
            {
                string[] addins = Directory.GetFiles(Path.Combine(FileUtility.ApplicationRootPath, "..\\Resource\\AddIns\\"));
                if (addins != null)
                {
                    foreach (string str in addins)
                    {
                        string temp = str.Replace("Resource\\", "");
                        File.Copy(str, temp, true);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
