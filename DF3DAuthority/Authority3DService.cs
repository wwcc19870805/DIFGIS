using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.Common;
using DFCommon.Class;
using DF3DAuthority.Frm;

namespace DF3DAuthority
{
    public class Authority3DService
    {
        private static Authority3DService _instance = null;
        private static readonly object syncRoot = new object();
        private bool _isAuthorized;
        private ILicenseServer _licenseServer;
        private Authority3DService()
        {
            try
            {
                this._licenseServer = new LicenseServer();
                long l1;
                this._licenseServer.InternalGetData(out l1, out this._isAuthorized);
                if (!this._isAuthorized)
                {
                    string ip = Config.GetConfigValue("Authority3DServerIP");
                    string port = Config.GetConfigValue("Authority3DServerPort");
                    string pwd = Config.GetConfigValue("Authority3DPwd");
                    uint uPort = 0;
                    bool bPortRes = uint.TryParse(port, out uPort);
                    if (bPortRes)
                    {
                        SetNetAuthority(ip, uPort, pwd);
                    }
                }
            }
            catch
            {

            }
        }

        public static Authority3DService Instance
        {
            get
            {
                if (Authority3DService._instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Authority3DService._instance == null)
                        {
                            Authority3DService._instance = new Authority3DService();
                        }
                    }
                }
                return Authority3DService._instance;
            }
        }
        public bool IsAuthorized
        {
            get { return this._isAuthorized; }
        }
        public void OpenAuthorizedDialog()
        {
            FrmSetAuthority dlg = new FrmSetAuthority();
            dlg.ShowDialog();
        }

        public bool SetNetAuthority(string ip, uint port, string pwd)
        {
            if (!this._isAuthorized)
            {
                this._licenseServer.SetHost(ip, port, pwd);
                long l1;
                this._licenseServer.InternalGetData(out l1, out this._isAuthorized);
                if (this._isAuthorized)
                {
                    Config.SetConfigValue("Authority3DServerIP",ip);
                    Config.SetConfigValue("Authority3DServerPort", port.ToString());
                    Config.SetConfigValue("Authority3DPwd", pwd);
                }
                return this._isAuthorized;
            }
            else return true;
        }


    }
}
