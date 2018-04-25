using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.RenderControl;

namespace DF3DScan.UC
{
    public class UCCameraTour : XtraUserControl
    {
        private UCAnimationNav ucAnimationNav1;
        private UCFrameEdit ucFrameEdit1;
        public UCCameraTour()
        {
            InitializeComponent();
            this.ucAnimationNav1.AttachEvent();
        }

        private void InitializeComponent()
        {
            this.ucAnimationNav1 = new DF3DScan.UC.UCAnimationNav();
            this.SuspendLayout();
            // 
            // ucAnimationNav1
            // 
            this.ucAnimationNav1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAnimationNav1.Location = new System.Drawing.Point(0, 0);
            this.ucAnimationNav1.Name = "ucAnimationNav1";
            this.ucAnimationNav1.Size = new System.Drawing.Size(323, 410);
            this.ucAnimationNav1.TabIndex = 0;
            // 
            // UCCameraTour
            // 
            this.Controls.Add(this.ucAnimationNav1);
            this.Name = "UCCameraTour";
            this.Size = new System.Drawing.Size(323, 410);
            this.ResumeLayout(false);

        }

        public void AddUCFrameEdit(ICameraTour tour)
        {

            this.ucFrameEdit1 = new UCFrameEdit(tour);
            this.ucFrameEdit1.Dock = DockStyle.Fill;
           
            this.Controls.Clear();
            this.Controls.Add(this.ucFrameEdit1);
        }

        public void ReLoadCameraTourpanel()
        {
            this.Controls.Clear();
            this.ucAnimationNav1.AttachEvent();
            this.ucAnimationNav1.UpdateButtonState();
            this.Controls.Add(this.ucAnimationNav1);
        }

        public void RestoreEnv()
        {
            this.ucAnimationNav1.RestoreEnv();
        }
    }
}
