using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections;
using DFWinForms.Class;
using ICSharpCode.Core;
namespace DFWinForms.Component
{
    public class MenuRibbonPage : RibbonPage, IStatusUpdate
    {
        private Codon codon;
        private object caller;
        private System.Collections.IList subItems;
        public bool HighLight
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
        public Codon Codon
        {
            get { return codon; }
        }
        public IList SubTtems
        {
            get
            {
                return this.subItems;
            }
        }
        public MenuRibbonPage(Codon codon, object caller, System.Collections.IList subItems)
        {
            if (subItems == null)
            {
                subItems = new System.Collections.ArrayList();
            }
            this.codon = codon;
            this.caller = caller;
            this.subItems = subItems;
            this.Name = codon.Id;
            this.UpdateText();
        }
        public MenuRibbonPage(string text, params RibbonPageGroup[] subItems)
        {
            base.Text = StringParser.Parse(text);
            this.subItems = subItems;
            this.Groups.AddRange(subItems);
        }
        private void CreateRibbonPageGroup()
        {
            this.Groups.Clear();
            foreach (object current in this.subItems)
            {
                if (current is RibbonPageGroup)
                {
                    this.Groups.Add((RibbonPageGroup)current);
                    if (current is IStatusUpdate)
                    {
                        //((IStatusUpdate)current).UpdateStatus();
                        ((IStatusUpdate)current).UpdateText();
                    }
                }
            }
        }
        public virtual void UpdateStatus()
        {
            if (this.codon != null)
            {
                ConditionAction failedAction = this.codon.GetFailedAction(this.caller);
                bool visibled = failedAction != ConditionAction.Exclude;
                this.Visible = visibled;
            }
        }
        public virtual void UpdateText()
        {
            if (this.codon != null)
            {
                base.Text = StringParser.Parse(this.codon.Properties["label"]);
            }
        }
    }
}
