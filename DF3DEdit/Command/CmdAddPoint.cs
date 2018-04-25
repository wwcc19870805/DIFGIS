using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DEdit.Frm;
using DF3DEdit.Service;
using System.Windows.Forms;

namespace DF3DEdit.Command
{
    public class CmdAddPoint : AbstractMap3DCommand
    {
        private FrmAddPoint dlg;
        public override void Run(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null) return;
            Map3DCommandManager.Push(this);
            RenderControlEditServices.Instance().StopGeometryEdit(true);
            System.DateTime birthDay = System.DateTime.Now;
            if (CommonUtils.Instance().EnableTemproalEdit)
            {
                using (DateSettingDialog dateSettingDialog = new DateSettingDialog(true))
                {
                    if (dateSettingDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    birthDay = dateSettingDialog.Time;
                }
            }
            app.Workbench.SetMenuEnable(false);
            dlg = new FrmAddPoint();
            dlg.SetBirthDay(birthDay);
            dlg.Show();
            dlg.FormClosed += new FormClosedEventHandler(dlg_FormClosed);
        }

        private void dlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null) return;
            app.Workbench.SetMenuEnable(true);
            app.Workbench.UpdateMenu();
            dlg.FormClosed -= new FormClosedEventHandler(dlg_FormClosed);
        }
    }
}
