using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFWinForms.UserControl
{
    public interface IContent
    {
        string ID
        {
            get;
            set;
        }
        string Title
        {
            get;
            set;
        }
        object CurrentRibbonPage
        {
            get;
        }
        bool IsActive { get; set; }
        bool ShowCloseButton { get; set; }
        void Activate();
        void SetCurrentRibbonPage(object ribbonPage);
    }
}
