using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Base;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using DevExpress.XtraEditors;
using DFWinForms.Form;
using ICSharpCode.Core;
using System.Collections;
using ESRI.ArcGIS.Geodatabase;
using DFWinForms.UserControl;

namespace DF2DControl.Base
{
    public class DF2DApplication : IDFApplication
    {
        private static DF2DApplication defaultDF2DApplication = null;
        private static readonly object syncRoot = new object();

        private IMapControl2 current2DMapControl;

        public IMapControl2 Current2DMapControl
        {
            get
            {
                return current2DMapControl;
            }
            set
            {
                current2DMapControl = value;
            }
        }

        public DefaultWorkbench Workbench
        {
            get
            {
                return DFApplication.Application.Workbench;
            }
        }

        public IContent CurrentContent
        {
            get
            {
                return DFApplication.Application.CurrentContent;
            }
        }

        private DF2DApplication()
        {
            if (!checkLicense())
            {
                XtraMessageBox.Show("初始化ArcGIS Engine License失败。不能使用该应用程序！");
                return;
            }
        }

        public static DF2DApplication Application
        {
            get
            {
                if (DF2DApplication.defaultDF2DApplication == null)
                {
                    lock (syncRoot)
                    {
                        if (DF2DApplication.defaultDF2DApplication == null)
                        {
                            DF2DApplication.defaultDF2DApplication = new DF2DApplication();
                        }
                    }
                }
                return DF2DApplication.defaultDF2DApplication;
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
                        else
                        {
                            licenseStatus = (esriLicenseStatus)m_AoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst);
                            if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                            {
                                XtraMessageBox.Show("Unable to check out the Designer extension. This application cannot run!");
                                return false;
                            }

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
