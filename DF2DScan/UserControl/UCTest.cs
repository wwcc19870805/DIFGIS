using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DF2DScan.UserControl
{
    public class UCTest : XtraUserControl
    {
        private SimpleButton simpleButton1;
    
        public UCTest()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(103, 136);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "simpleButton1";
            // 
            // UCTest
            // 
            this.Controls.Add(this.simpleButton1);
            this.Name = "UCTest";
            this.Size = new System.Drawing.Size(280, 354);
            this.ResumeLayout(false);

        }


    }
}
