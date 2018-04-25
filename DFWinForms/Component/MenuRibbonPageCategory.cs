using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections;
using DFWinForms.Class;
using ICSharpCode.Core;
namespace DFWinForms.Component
{
    public class MenuRibbonPageCategory: RibbonPageCategory, IStatusUpdate
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
        public IList SubTtems
        {
            get
            {
                return this.subItems;
            }
        }
        public MenuRibbonPageCategory(Codon codon, object caller, System.Collections.IList subItems)
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
        public MenuRibbonPageCategory(string text, params RibbonPage[] subItems)
        {
            base.Text = StringParser.Parse(text);
            this.subItems = subItems;
            this.Pages.AddRange(subItems);
        }
        private void CreateRibbonPage()
        {
            this.Pages.Clear();
            foreach (object current in this.subItems)
            {
                if (current is RibbonPage)
                {
                    this.Pages.Add((RibbonPage)current);
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
