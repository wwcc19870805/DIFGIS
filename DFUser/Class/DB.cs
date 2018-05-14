using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using DFCommon.Class;

namespace DFUser.Class
{
    public class DB
    {
        private static DB _do;
        private static readonly object syncRoot = new object();
        private string constr;

        private DB()
        {
            constr = Config.GetConfigValue("DBConnString");
        }

        public static DB Instance
        {
            get
            {
                if (DB._do == null)
                {
                    lock (syncRoot)
                    {
                        if (DB._do == null)
                        {
                            DB._do = new DB();
                        }
                    }
                }
                return DB._do;
            }
        }

        public bool queryDB(string sql, ref DataTable dt)
        
        {
            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
                    if (dt == null) dt = new DataTable();
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }

        public bool noqueryDB(string sql)
        {
            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    int lineNum = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }
    }
}
