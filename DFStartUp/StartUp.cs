using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using ICSharpCode.Core;
using System.Reflection;
using System.Windows.Forms;
using System.Resources;
using System.Xml;
using DFCommon.Class;
using DFUser.Frm;

namespace DFStartUp
{
    static class StartUp
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Add           
            if (args != null && args.Length > 0 && !string.IsNullOrEmpty(args[0])) Config.SetConfigPath(args[0]);
            bool flag = false;
            using (new Mutex(true, SystemInfo.Instance.SystemFullName, out flag))
            {
                if (flag)
                {
                    try
                    {
                        FormLogin dialog = new FormLogin();
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            Assembly assembly = typeof(StartUp).Assembly;
                            FileUtility.ApplicationRootPath = Path.GetDirectoryName(assembly.Location);
                            FileUtility.LocalUserAppDataPath = System.Windows.Forms.Application.LocalUserAppDataPath;
                            ResourceService.RegisterNeutralImages(new ResourceManager("DFStartUp.Resource", assembly));
                            CoreStartup coreStartup = new CoreStartup("DIFGIS");
                            coreStartup.PropertiesName = "BuilderConfig";
                            coreStartup.StartCoreServices();
                            coreStartup.AddAddInsFromDirectory(Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns"));
                            coreStartup.ConfigureExternalAddIns(Path.Combine(PropertyService.ConfigDirectory, "AddIns.xml"));
                            coreStartup.ConfigureUserAddIns(Path.Combine(PropertyService.ConfigDirectory, "AddInInstallTemp"), Path.Combine(PropertyService.ConfigDirectory, "AddIns"));
                            coreStartup.RunInitialization();
                        }
                        else System.Environment.Exit(0);
                        
                    }
                    catch (XmlException ex)
                    {
                        MessageBox.Show("读取XML文件错误 :" + Environment.NewLine + ex.Message);
                        LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                        Thread.Sleep(0x3e8);
                        Environment.Exit(1);
                    }
                    catch (Exception ex)
                    {
                        LoggingService.Fatal(ex.Message + "\r\n" + ex.StackTrace);
                        Thread.Sleep(0x3e8);
                        Environment.Exit(1);
                    }
                }
                else
                {
                    MessageBox.Show(SystemInfo.Instance.SystemFullName + "已经在运行中！");
                    Thread.Sleep(0x3e8);
                    Environment.Exit(1);
                }
            }
        }
    }
}
