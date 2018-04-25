using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using DevExpress.Utils;
using System.Collections;
using ICSharpCode.Core;
using DFWinForms.Class;
using DFWinForms.Service;
namespace DFWinForms.Component
{
    public class MenuButtonGroupCommand : BarButtonGroup, IStatusUpdate
    {
        private Codon codon;
        private object caller;
        private IList subItems;

        public IList SubItems
        {
            get
            {
                return this.subItems;
            }
            set
            {
                this.subItems = value;
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
        public MenuButtonGroupCommand(Codon codon, object caller, System.Collections.IList subItems)
        {
            if (subItems == null)
            {
                subItems = new System.Collections.ArrayList();
            }
            this.codon = codon;
            this.caller = caller;
            this.subItems = subItems;
            foreach (object subItem in subItems)
            {
                if (subItem is MenuButtonCommand)
                {
                    MenuButtonCommand mbc = subItem as MenuButtonCommand;
                    mbc.GroupObj = this;
                }
            }
            base.Name = codon.Id;
            this.UpdateText();
        }

        public MenuButtonGroupCommand(string text, params BarItem[] subItems)
        {
            this.Caption = StringParser.Parse(text);
            this.subItems = subItems;
            this.ItemLinks.AddRange(subItems);
        }

        private void MenuButtonGroupCommand_CheckedChanged(object sender, ItemClickEventArgs e)
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
                this.Enabled = enabled;
                bool visibled = failedAction != ConditionAction.Exclude;
                if (visibled) this.Visibility = BarItemVisibility.Always;
                else this.Visibility = BarItemVisibility.Never;
            }
        }

        public virtual void UpdateText()
        {
            if (this.codon != null)
            {
                this.Caption = StringParser.Parse(this.codon.Properties["label"]);
                this.Glyph = WinFormsResourceService.GetBitmap(StringParser.Parse(this.codon.Properties["icon"]));
                base.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
                this.ParseTooltips();
            }
        }

    }
}
