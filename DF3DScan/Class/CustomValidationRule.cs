using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Windows.Forms;
using ICSharpCode.Core;

namespace DF3DScan.Class
{
    public class CustomValidationRule : ValidationRule
    {
        private string _fieldName;
        public CustomValidationRule(string fieldName)
        {
            this._fieldName = fieldName;
        }
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            if (this._fieldName == null || (this._fieldName != StringParser.Parse("${res:view_dlg_flytime}") && this._fieldName != StringParser.Parse("${res:view_dlg_flymode}")))
            {
                return false;
            }
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                base.ErrorText = StringParser.Parse("${res:view_alert_inputError1}");
                return false;
            }
            bool flag = true;
            if (this._fieldName == StringParser.Parse("${res:view_dlg_flytime}"))
            {
                double num;
                if (!double.TryParse(value.ToString(), out num))
                {
                    flag = false;
                }
            }
            else
            {
                if (this._fieldName == StringParser.Parse("${res:view_dlg_flymode}") && value.ToString() != "Smooth" && value.ToString() != "Bounce" && value.ToString() != "Linear")
                {
                    flag = false;
                }
            }
            if (!flag)
            {
                base.ErrorText = StringParser.Parse("${res:view_alert_inputError2}");
                return false;
            }
            return true;
        }
    }
}
