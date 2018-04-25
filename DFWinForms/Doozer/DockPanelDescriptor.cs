using System;
using ICSharpCode.Core;
namespace DFWinForms.Doozer
{
    public class DockPanelDescriptor
    {
        public readonly object Caller;
        public readonly Codon Codon;
        public DockPanelDescriptor(object caller, Codon codon)
        {
            this.Codon = codon;
            this.Caller = caller;
        }
    }
}
