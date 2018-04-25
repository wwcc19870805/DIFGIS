using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Alerter;
using System.Drawing;
namespace DFWinForms.Class
{
    public class AlertPage
    {
        // Fields
        private static AlertControl _alert;

        // Methods
        public static void Show(System.Windows.Forms.Form owner)
        {
            Alert.Show(owner);
        }

        public static void Show(System.Windows.Forms.Form owner, AlertInfo info)
        {
            Alert.Show(owner, info);
        }

        public static void Show(System.Windows.Forms.Form owner, string caption, string text)
        {
            Alert.Show(owner, caption, text);
        }

        public static void Show(System.Windows.Forms.Form owner, string caption, string text, Image image)
        {
            Alert.Show(owner, caption, text, image);
        }

        public static void Show(System.Windows.Forms.Form owner, string caption, string text, string hotTrackedText)
        {
            Alert.Show(owner, caption, text, hotTrackedText);
        }

        public static void Show(System.Windows.Forms.Form owner, string caption, string text, string hotTrackedText, Image image)
        {
            Alert.Show(owner, caption, text, hotTrackedText, image);
        }

        public static void Show(System.Windows.Forms.Form owner, string caption, string text, string hotTrackedText, Image image, object tag)
        {
            Alert.Show(owner, caption, text, hotTrackedText, image, tag);
        }

        // Properties
        private static AlertControl Alert
        {
            get
            {
                if (_alert == null)
                {
                    _alert = new AlertControl();
                    _alert.AutoFormDelay = 0xbb8;
                }
                return _alert;
            }
        }
    }
}
