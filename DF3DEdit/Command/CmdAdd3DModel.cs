using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DEdit.Frm;
using DF3DEdit.Service;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DAuthority;

namespace DF3DEdit.Command
{
    public class CmdAdd3DModel : AbstractMap3DCommand
    {
        private FrmAdd3DModel dlg;
        public override void Run(object sender, EventArgs e)
        {
            bool _isAuth = Authority3DService.Instance.IsAuthorized;
            if (!_isAuth)
            {
                XtraMessageBox.Show("此功能需要USB Key。", "提示");
                return;
            }
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
            dlg = new FrmAdd3DModel();
            dlg.SetBirthDay(birthDay);
            dlg.Show();
            dlg.FormClosed += new FormClosedEventHandler(dlg_FormClosed);
        }

        private void dlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null) return;
            app.Workbench.SetMenuEnable(true);
            dlg.FormClosed -= new FormClosedEventHandler(dlg_FormClosed);
        }
    }
}
