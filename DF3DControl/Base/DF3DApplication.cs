using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Base;
using DFWinForms.Form;
using DFWinForms.UserControl;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Controls;

namespace DF3DControl.Base
{
    public class DF3DApplication : IDFApplication
    {
        private static DF3DApplication defaultDF3DApplication = null;
        private static readonly object syncRoot = new object();

        private AxRenderControl current3DMapControl;
        private bool isNetData;
        private bool isInit3DControl;
        public AxRenderControl Current3DMapControl
        {
            get
            {
                return current3DMapControl;
            }
            set
            {
                current3DMapControl = value;
            }
        }

        public bool IsNetData
        {
            get
            {
                return isNetData;
            }
            set
            {
                isNetData = value;
            }
        }

        public bool IsInit3DControl
        {
            get
            {
                return isInit3DControl;
            }
            set
            {
                isInit3DControl = value;
            }
        }

        public DefaultWorkbench Workbench
        {
            get
            {
                return DFApplication.Application.Workbench;
            }
        }

        public IContent CurrentContent
        {
            get
            {
                return DFApplication.Application.CurrentContent;
            }
        }
        private DF3DApplication()
        {
        }

        public static DF3DApplication Application
        {
            get
            {
                if (DF3DApplication.defaultDF3DApplication == null)
                {
                    lock (syncRoot)
                    {
                        if (DF3DApplication.defaultDF3DApplication == null)
                        {
                            DF3DApplication.defaultDF3DApplication = new DF3DApplication();
                        }
                    }
                }
                return DF3DApplication.defaultDF3DApplication;
            }
        }


    }
}
