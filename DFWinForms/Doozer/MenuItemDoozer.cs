using System;
using System.Collections;
using ICSharpCode.Core;
namespace DFWinForms.Doozer
{
    public class MenuItemDoozer : IDoozer
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
            return new MenuItemDescriptor(caller, codon, subItems);
        }
    }
}
