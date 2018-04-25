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
using DFWinForms.Service;
using DFWinForms.Base;

namespace DFWinForms.UserControl
{
    public class BaseContent : XtraUserControl, IContent
    {
        private string id = null;
        private string title = null;
        protected bool isActive = false;
        protected bool showCloseButton = false;
        private object currentRibbonPage;
        public bool ShowCloseButton
        {
            get
            {
                return showCloseButton;
            }
            set
            {
                showCloseButton = value;
            }
        }
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public virtual bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        public object CurrentRibbonPage
        {
            get { return currentRibbonPage; }
        }

        public BaseContent()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseContent
            // 
            this.Name = "BaseContent";
            this.Size = new System.Drawing.Size(153, 150);
            this.ResumeLayout(false);

        }

        public virtual void Activate()
        {
            DFApplication.Application.Workbench.ActivateContent(this);
        }

        public virtual void SetCurrentRibbonPage(object ribbonPage)
        {
            this.currentRibbonPage = ribbonPage;
        }

    }
}
