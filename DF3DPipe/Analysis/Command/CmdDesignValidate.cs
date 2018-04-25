using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DPipe.Analysis.Frm;
using DevExpress.XtraEditors;
using DF3DAuthority;

namespace DF3DPipe.Analysis.Command
{
    public class CmdDesignValidate : AbstractMap3DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            bool _isAuth = Authority3DService.Instance.IsAuthorized;
            if (!_isAuth)
            {
                XtraMessageBox.Show("此功能需要USB Key。", "提示");
                return;
            }
            FrmDesignValidate dlg = new FrmDesignValidate();
            dlg.Show();
        }
    }
}
