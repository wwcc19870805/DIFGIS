using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections;
using DFWinForms.Class;
using ICSharpCode.Core;
using DFWinForms.Service;
namespace DFWinForms.Component
{
    public class MenuRibbonPageGroup : RibbonPageGroup, IStatusUpdate
    {
        private Codon codon;
        private object caller;
        private System.Collections.IList subItems;
        public IList SubTtems
        {
            get
            {
                return this.subItems;
            }
        }
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
        public MenuRibbonPageGroup(Codon codon, object caller, System.Collections.IList subItems)
        {
            if (subItems == null)
            {
                subItems = new System.Collections.ArrayList();
            }
            this.codon = codon;
            this.caller = caller;
            this.subItems = subItems;
            this.Name = codon.Id;
            this.ShowCaptionButton = false;
            this.UpdateText();
        }
        public MenuRibbonPageGroup(string text, params BarItem[] subItems)
        {
            this.Text = StringParser.Parse(text);
            this.subItems = subItems;
            base.ItemLinks.AddRange(subItems);
            this.ShowCaptionButton = false;
        }

        public virtual void UpdateStatus()
        {
            if (this.codon != null)
            {
                ConditionAction failedAction = this.codon.GetFailedAction(this.caller);
                bool enabled = failedAction != ConditionAction.Disable;
                this.Enabled = enabled;
                bool visibled = failedAction != ConditionAction.Exclude;
                this.Visible = visibled;
            }
        }
        public virtual void UpdateText()
        {
            if (this.codon != null)
            {
                this.Text = StringParser.Parse(this.codon.Properties["label"]);
                this.Glyph = WinFormsResourceService.GetBitmap(StringParser.Parse(this.codon.Properties["icon"]));
            }
        }
    }
}
