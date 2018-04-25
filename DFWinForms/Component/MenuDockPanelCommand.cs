using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Windows.Forms;
using ICSharpCode.Core;
using DFWinForms.Class;
using DFWinForms.Service;
namespace DFWinForms.Component
{
    public class MenuDockPanelCommand : BarButtonItem, IStatusUpdate
    {
        private object caller;
        private Codon codon;
        private DockPanel dockPanel;
        private IDockPanelContent dockPanelContent;
        public new string Category
        {
            get
            {
                return this.codon.Properties["category"];
            }
        }
        public IDockPanelContent DockPanelContent
        {
            get
            {
                if (this.dockPanelContent == null)
                {
                    this.CreateContent();
                }
                return this.dockPanelContent;
            }
        }
        public bool HighLight
        {
            get
            {
                return this.Down;
            }
            set
            {
                this.Down = value;
            }
        }
        public MenuDockPanelCommand(Codon codon, object caller, bool createCommand)
        {
            this.caller = caller;
            this.codon = codon;
            base.Name = codon.Id;
            if (createCommand)
            {
                this.CreateContent();
            }
            this.UpdateText();
        }
        private void CreateContent()
        {
            try
            {
                if (this.caller != null && this.caller is System.Windows.Forms.Control.ControlCollection)
                {
                    ControlContainer value = new ControlContainer();
                    this.dockPanel = new DockPanel();
                    this.dockPanel.Controls.Add(value);
                    this.dockPanel.Dock = DockingStyle.Right;
                    this.dockPanel.Text = this.codon.Properties["title"];
                    ((System.Windows.Forms.Control.ControlCollection)this.caller).Add(this.dockPanel);
                }
                this.dockPanelContent = (IDockPanelContent)this.codon.AddIn.CreateObject(this.codon.Properties["class"]);
                if (this.dockPanelContent != null && this.dockPanelContent.Control != null)
                {
                    this.dockPanel.Controls.Add(this.dockPanelContent.Control);
                    this.dockPanelContent.Control.Dock = System.Windows.Forms.DockStyle.Fill;
                }
            }
            catch (System.Exception)
            {
            }
        }
        protected override void OnClick(BarItemLink link)
        {
            base.OnClick(link);
            if (this.dockPanel != null)
            {
                this.dockPanel.Visibility = DockVisibility.AutoHide;
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
        private void ParseShortCut()
        {
            if (this.codon != null && this.codon.Properties.Contains("shortcut"))
            {
                string value = StringParser.Parse(this.codon.Properties["shortcut"]);
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                try
                {
                    System.Windows.Forms.Shortcut shortcut = (System.Windows.Forms.Shortcut)System.Enum.Parse(typeof(System.Windows.Forms.Shortcut), value);
                    if (shortcut != System.Windows.Forms.Shortcut.None)
                    {
                        this.ItemShortcut = new BarShortcut(shortcut);
                    }
                }
                catch (System.Exception)
                {
                    try
                    {
                        System.Windows.Forms.Keys keys = (System.Windows.Forms.Keys)System.Enum.Parse(typeof(System.Windows.Forms.Keys), value);
                        this.ItemShortcut = new BarShortcut(keys);
                    }
                    catch
                    {
                    }
                }
            }
        }
        private void ParseRibbonItemStyle()
        {
            RibbonItemStyles ribbonStyle = RibbonItemStyles.Default;
            if (this.codon != null && this.codon.Properties.Contains("style"))
            {
                string value = StringParser.Parse(this.codon.Properties["style"]);
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        ribbonStyle = (RibbonItemStyles)System.Enum.Parse(typeof(RibbonItemStyles), value);
                    }
                    catch (System.Exception)
                    {
                        ribbonStyle = RibbonItemStyles.Default;
                    }
                }
            }
            base.RibbonStyle = ribbonStyle;
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
                this.Caption = StringParser.Parse(this.codon.Properties["title"]);
                this.Glyph = WinFormsResourceService.GetBitmap(StringParser.Parse(this.codon.Properties["icon"]));
                this.ParseTooltips();
                this.ParseRibbonItemStyle();
                this.ParseShortCut();
            }
        }
    }
}
