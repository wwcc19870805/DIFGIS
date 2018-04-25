using System;
using System.Collections;
using ICSharpCode.Core;
using DFWinForms.Form;
using DFWinForms.UserControl;

namespace DFWinForms.Base
{
    /// <summary>
    /// DFApplication 的摘要说明。
    /// </summary>
    public class DFApplication : IDFApplication
    {
        private static DFApplication defaultDFApplication = null;
        private static readonly object syncRoot = new object();

        private DefaultWorkbench workbench = null;
        private IContent currentContent = null;

        private DFApplication()
        {

        }

        public static DFApplication Application
        {
            get
            {
                if (DFApplication.defaultDFApplication == null)
                {
                    lock (syncRoot)
                    {
                        if (DFApplication.defaultDFApplication == null)
                        {
                            DFApplication.defaultDFApplication = new DFApplication();
                        }
                    }
                }
                return DFApplication.defaultDFApplication;
            }
        }

        public DefaultWorkbench Workbench
        {
            get
            {
                return workbench;
            }
            set
            {
                workbench = value;
            }
        }

        public IContent CurrentContent
        {
            get
            {
                return currentContent;
            }
            set
            {
                currentContent = value;
            }
        }
    }

}