using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICSharpCode.Core;

namespace DFWinForms.UserControl
{
    public class BaseViewContent : BaseContent, IViewContent
    {
        public BaseViewContent()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseViewContent
            // 
            this.Name = "BaseViewContent";
            this.Size = new System.Drawing.Size(153, 150);
            this.ResumeLayout(false);

        }

        public virtual bool Bind(ICommand cmd)
        {
            return false;
        }
        public virtual void UnBind(ICommand cmd)
        {

        }

    }
}
