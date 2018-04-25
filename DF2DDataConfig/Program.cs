using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using ESRI.ArcGIS.esriSystem;
using DevExpress.XtraEditors;
using DFCommon.Class;


namespace DF2DDataConfig
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                BonusSkins.Register();
                SkinManager.EnableFormSkins();
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
                if (args != null && args.Length > 0 && !string.IsNullOrEmpty(args[0])) Config.SetConfigPath(args[0]);
                if (checkLicense())
                {
                    Application.Run(new MainForm());
                }
                else Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Environment.Exit(1);
            }
        }

        private static bool checkLicense()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            IAoInitialize m_AoInitialize = new AoInitialize();

            esriLicenseStatus licenseStatus = (esriLicenseStatus)m_AoInitialize.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeEngine);
            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
            {
                licenseStatus = (esriLicenseStatus)m_AoInitialize.IsExtensionCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst);
                if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
                {
                    licenseStatus = (esriLicenseStatus)m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);//esriLicenseProductCodeEngine);
                    if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                    {
                        XtraMessageBox.Show("The initialization failed. This application cannot run!");
                        return false;
                    }
                    else
                    {
                        licenseStatus = (esriLicenseStatus)m_AoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeDesigner);
                        if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                        {
                            XtraMessageBox.Show("Unable to check out the Designer extension. This application cannot run!");
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                XtraMessageBox.Show("The ArcGIS Engine product is unavailable. This application cannot run!");
                return false;
            }
        }
    }
}
