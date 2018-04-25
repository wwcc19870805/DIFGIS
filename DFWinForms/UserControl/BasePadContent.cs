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
    public class BasePadContent : BaseContent, IPadContent
    {
        protected string pos = null;
        public string Pos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }
        protected bool bAutoHide = false;
        public bool AutoHide
        {
            get
            {
                return bAutoHide;
            }
            set
            {
                bAutoHide = value;
            }
        }
        protected int pheight = 200;
        public int PHeight
        {
            get
            {
                return pheight;
            }
            set
            {
                pheight = value;
            }
        }

        public BasePadContent()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BasePadContent
            // 
            this.Name = "BasePadContent";
            this.Size = new System.Drawing.Size(153, 150);
            this.ResumeLayout(false);

        }
    }
}
