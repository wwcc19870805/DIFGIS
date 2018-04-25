using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;

namespace DFWinForms.Doozer
{
    public class ClassDoozer : IDoozer
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
            return codon.AddIn.CreateObject(codon.Properties["class"]);
        }
    }
}
