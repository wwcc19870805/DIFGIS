using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFWinForms.UserControl;

namespace DFWinForms.Doozer
{
    public class PadDoozer : IDoozer
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
            IPadContent pc = (IPadContent)codon.AddIn.CreateObject(codon.Properties["class"]);
            if (pc == null) return null;
            pc.Title = codon.Properties["title"].ToString();
            pc.ID = codon.Properties["id"].ToString();
            pc.Pos = codon.Properties["pos"].ToString();
            if (codon.Properties.Contains("active") && codon.Properties["active"].ToLower() == "true")
                pc.IsActive = true;
            else pc.IsActive = false;
            if (codon.Properties.Contains("ShowCloseButton") && codon.Properties["ShowCloseButton"].ToLower() == "true")
                pc.ShowCloseButton = true;
            else pc.ShowCloseButton = false;
            if (codon.Properties.Contains("AutoHide") && codon.Properties["AutoHide"].ToLower() == "true")
                pc.AutoHide = true;
            else pc.AutoHide = false;
            if (codon.Properties.Contains("height"))
            {
                int height = 500;
                bool bHeight = int.TryParse(codon.Properties["height"].ToString(), out height);
                pc.PHeight = height;
            }
            else pc.PHeight = 500;

            return pc;
        }
    }
}
