using System;
using System.Collections;
using ICSharpCode.Core;
namespace DFWinForms.Doozer 
{
    public class DockPanelDoozer : IDoozer
    {
        public bool HandleConditions
        {
            get
            {
                return false;
            }
        }
        public object BuildItem(object caller, Codon codon, System.Collections.ArrayList subItems)
        {
            return new DockPanelDescriptor(caller, codon);
        }
    }
}
