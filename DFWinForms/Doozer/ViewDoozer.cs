using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.UserControl;
using ICSharpCode.Core;

namespace DFWinForms.Doozer
{
    public class ViewDoozer : IDoozer
    {
        public bool HandleConditions
        {
            get
            {
                return true;
            }
        }
        public object BuildItem(object caller, Codon codon, System.Collections.ArrayList subItems)
        {
            IViewContent vc= (IViewContent)codon.AddIn.CreateObject(codon.Properties["class"]);
            if (vc == null) return null;
            vc.Title = codon.Properties["title"].ToString();
            vc.ID = codon.Properties["id"].ToString();
            if (codon.Properties.Contains("active") && codon.Properties["active"].ToLower() == "true")
                vc.IsActive = true;
            else vc.IsActive = false;
            if (codon.Properties.Contains("ShowCloseButton") && codon.Properties["ShowCloseButton"].ToLower() == "true")
                vc.ShowCloseButton = true;
            else vc.ShowCloseButton = false;
            return vc;
        }
    }
}
