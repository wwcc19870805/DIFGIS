using System;
using System.Collections;
using ICSharpCode.Core;
namespace DFWinForms.Doozer
{
    public sealed class MenuItemDescriptor
    {
        public readonly object Caller;
        public readonly Codon Codon;
        public readonly System.Collections.IList SubItems;
        public MenuItemDescriptor(object caller, Codon codon, System.Collections.IList subItems)
        {
            this.Caller = caller;
            this.Codon = codon;
            this.SubItems = subItems;
        }
    }
}
