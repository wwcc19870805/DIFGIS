using DevExpress.Utils;
using DevExpress.XtraBars;
using System;
using System.Collections;
using DFWinForms.Class;
using ICSharpCode.Core;
using DFWinForms.Service;
namespace DFWinForms.Component
{
    public class MenuSubItemCommand : BarSubItem, IStatusUpdate
    {
        private Codon codon;
        private object caller;
        private System.Collections.IList subItems;
        public IList SubItems
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
        public MenuSubItemCommand(Codon codon, object caller, System.Collections.IList subItems)
        {
            if (subItems == null)
            {
                subItems = new System.Collections.ArrayList();
            }

            this.codon = codon;
            this.caller = caller;
            this.subItems = subItems;
            base.Name = codon.Id;
            this.UpdateText();
        }

        public MenuSubItemCommand(string text, params BarItem[] subItems)
        {
            this.Caption = StringParser.Parse(text);
            this.subItems = subItems;
            this.ItemLinks.AddRange(subItems);
        }

        protected override void OnClick(BarItemLink link)
        {
            base.OnClick(link);
        }

        public void CreateItems()
        {
            this.ItemLinks.Clear();
            foreach (object obj2 in this.subItems)
            {
                if (obj2 is BarItem)
                {
                    this.ItemLinks.Add((BarItem)obj2);
                    if (obj2 is BarCheckItem)
                    {
                        ((BarCheckItem)obj2).CheckedChanged += new ItemClickEventHandler(this.MenuSubItemCommand_CheckedChanged);
                    }
                    if (obj2 is IStatusUpdate)
                    {
                        ((IStatusUpdate)obj2).UpdateStatus();
                        ((IStatusUpdate)obj2).UpdateText();
                    }
                }
            }
        }
        private void MenuSubItemCommand_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Glyph != null)
            {
                this.Glyph = e.Item.Glyph;
                this.Caption = e.Item.Caption;
            }
        }
        private void ParseTooltips()
        {
            if (this.codon != null && this.codon.Properties.Contains("tooltip"))
            {
                SuperToolTip superToolTip = new SuperToolTip();
                ToolTipItem toolTipItem = new ToolTipItem();
                ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem();
                string text = StringParser.Parse(this.codon.Properties["tooltip"]);
                if (text.Contains(":"))
                {
                    toolTipTitleItem.Text = text.Split(new char[]
					{
						':'
					})[0];
                    toolTipItem.Text = text.Split(new char[]
					{
						':'
					})[1];
                }
                else
                {
                    toolTipItem.Text = text;
                }
                superToolTip.Items.Add(toolTipTitleItem);
                superToolTip.Items.Add(toolTipItem);
                this.SuperTip = superToolTip;
            }
        }
        public virtual void UpdateStatus()
        {
            if (this.codon != null)
            {
                ConditionAction failedAction = this.codon.GetFailedAction(this.caller);
                bool enabled = failedAction != ConditionAction.Disable;
                this.Enabled=enabled;
                bool visibled = failedAction != ConditionAction.Exclude;
                if (visibled) this.Visibility = BarItemVisibility.Always;
                else this.Visibility = BarItemVisibility.Never;
            }
        }
        public virtual void UpdateText()
        {
            if (this.codon != null)
            {
                this.Caption=StringParser.Parse(this.codon.Properties["label"]);
                this.Glyph=WinFormsResourceService.GetBitmap(StringParser.Parse(this.codon.Properties["icon"]));
                base.RibbonStyle= DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
                this.ParseTooltips();
            }
        }
    }
}
