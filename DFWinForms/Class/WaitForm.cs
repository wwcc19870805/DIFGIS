using DevExpress.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace DFWinForms.Class
{
    public class WaitForm
    {
        // Fields
        private static WaitDialogForm _waitFrm;
        public static ShowOperateInfo ShowOperateInfo;

        // Methods
        public static void SetCaption(string caption)
        {
            if (_waitFrm != null)
            {
                _waitFrm.SetCaption(caption);
            }
        }

        public static void SetCaption(string caption,string title)
        {
            if (_waitFrm != null)
            {
                _waitFrm.SetCaption(caption);
                _waitFrm.Text = title;
            }
        }

        public static void Start()
        {
            if (_waitFrm != null)
            {
                _waitFrm.Close();
            }
            _waitFrm = new WaitDialogForm();
            _waitFrm.Show();
        }

        public static void Start(string caption)
        {
            if (_waitFrm != null)
            {
                _waitFrm.Close();
            }
            _waitFrm = new WaitDialogForm(caption);
            _waitFrm.Show();
        }

        public static void Start(string caption, Size size)
        {
            if (_waitFrm != null)
            {
                _waitFrm.Close();
            }
            _waitFrm = new WaitDialogForm(caption, size);
            _waitFrm.Show();
        }

        public static void Start(string caption, string title)
        {
            if (_waitFrm != null)
            {
                _waitFrm.Close();
            }
            if (ShowOperateInfo != null)
            {
                ShowOperateInfo();
            }
            _waitFrm = new WaitDialogForm(caption, title);
            _waitFrm.Show();
        }

        public static void Start(string caption, string title, bool flag)
        {
            if (_waitFrm != null)
            {
                _waitFrm.Close();
            }
            if (flag && (ShowOperateInfo != null))
            {
                ShowOperateInfo();
            }
            _waitFrm = new WaitDialogForm(caption, title);
            _waitFrm.Show();
        }

        public static void Start(string caption, string title, Size size)
        {
            if (_waitFrm != null)
            {
                _waitFrm.Close();
            }
            _waitFrm = new WaitDialogForm(caption, title, size);
            _waitFrm.Show();
        }

        public static void Start(string caption, string title, Size size, System.Windows.Forms.Form parent)
        {
            if (_waitFrm != null)
            {
                _waitFrm.Close();
            }
            _waitFrm = new WaitDialogForm(caption, title, size, parent);
            _waitFrm.Show();
        }

        public static void Stop()
        {
            if (_waitFrm != null)
            {
                _waitFrm.Close();
                _waitFrm = null;
            }
        }
    }

 

}
