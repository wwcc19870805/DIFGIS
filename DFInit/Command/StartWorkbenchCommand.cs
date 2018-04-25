using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DFWinForms.Base;
using DFWinForms.Command;
using ICSharpCode.Core;
using DFWinForms.Form;
using System.Resources;
using DFCommon.Class;
using DFUser.Frm;

namespace DFInit.Command
{

    public class StartWorkbenchCommand : AbstractCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                string systemType = Config.GetConfigValue("SystemType");
                if (systemType != "2D" && systemType != "3D") systemType = "2D3D";
                string startTime = DateTime.Now.ToString();
                //string res = WebApp.CallWebService(SystemInfo.Instance.ZYWKIPPORT + "SystemLog.asmx", "WriteSystemInfo",
                //    new string[] { "systemname", "location", "dimension", "version", "clienttype", "starttime" },
                //    new string[] { SystemInfo.Instance.SystemName, SystemInfo.Instance.Location, systemType, SystemInfo.Instance.Version, "CS", startTime });
                //string res = WebApp.CallWebService(SystemInfo.Instance.ZYWKIPPORT + "SystemLog.asmx", "WriteSystemInfo",
                //    new string[] { "str" },
                //    new string[] { "232" });

                DefaultWorkbench form = new DefaultWorkbench();
                ResourceManager resource = new ResourceManager(typeof(DefaultWorkbench));
                DFApplication.Application.Workbench = form;
                form.Text = SystemInfo.Instance.SystemFullName;
                //form.Icon = (Icon)resource.GetObject("$this.Icon");
                form.Initialize();

                LoggingService.Info(form.Text + "Æô¶¯£¡");
                Application.AddMessageFilter(new FormKeyHandler());
                Application.DoEvents();
                Application.Run((Form)DFApplication.Application.Workbench);

            }
            catch (SEHException se)
            {
                LoggingService.Fatal(se.Message + "\r\n" + se.StackTrace);
            }
            catch (Exception ex)
            {
                LoggingService.Fatal(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                System.Environment.Exit(0);
            }
        }

        private class FormKeyHandler : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == keyPressedMessage)
                {
                    Keys keys1 = ((Keys)m.WParam.ToInt32()) | Control.ModifierKeys;
                    if (keys1 == Keys.Escape)
                    {
                    }
                }
                return false;
            }

            private const int keyPressedMessage = 0x100;
        }
    }
}

