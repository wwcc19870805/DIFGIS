using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using ICSharpCode.Core;
using DFWinForms.Class;
using DFWinForms.Service;
namespace DFWinForms.Component
{
    public class MenuButtonCommand : BarButtonItem, IStatusUpdate
    {
        private object caller;
        private Codon codon;
        private ICommand menuCommand;
        private object groupObj;
        public object GroupObj
        {
            get
            {
                return this.groupObj;
            }
            set
            {
                this.groupObj = value;
            }
        }
        public ICommand Command
        {
            get
            {
                if (this.menuCommand == null)
                {
                    this.CreateCommand();
                }
                return this.menuCommand;
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
        public MenuButtonCommand(Codon codon, object caller)
            : this(codon, caller, false)
        {
        }
        public MenuButtonCommand(Codon codon, object caller, bool createCommand)
        {
            this.caller = caller;
            this.codon = codon;
            base.Name = codon.Id;
            this.UpdateText();
            if (createCommand)
            {
                this.CreateCommand();
            }
            this.groupObj = null;
        }
        public MenuButtonCommand(string label)
        {
            this.codon = null;
            this.caller = null;
            this.Caption = StringParser.Parse(label);
        }
        private void CreateCommand()
        {
            try
            {
                if (this.codon.Properties["menustyle"] == null) this.ButtonStyle = BarButtonStyle.DropDown;
                switch (this.codon.Properties["menustyle"].ToString().Trim().ToLower())
                {
                    case "check":
                        this.ButtonStyle = BarButtonStyle.Check;
                        break;
                    case "dropdown":
                        this.ButtonStyle = BarButtonStyle.DropDown;
                        break;
                    case "default":
                        this.ButtonStyle = BarButtonStyle.Default;
                        break;
                    default:
                        this.ButtonStyle = BarButtonStyle.Default;
                        break;

                }
                this.menuCommand = (ICommand)this.codon.AddIn.CreateObject(this.codon.Properties["class"]);
                if (this.menuCommand != null)
                {
                    this.menuCommand.CommandName = this.codon.Properties["label"];
                    this.menuCommand.CommandID = this.codon.Properties["id"];
                    this.menuCommand.Init(this);
                    this.menuCommand.StatusChanged += delegate(bool b)
                    {
                        this.HighLight = b;
                    };
                }
            }
            catch (System.Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        protected override void OnClick(BarItemLink link)
        {
            base.OnClick(link);
            if (this.codon != null)
            {
                ICommand command = this.Command;
                if (command != null)
                {
                    if (command.CheckPermission())
                    {
                        command.Run(this, null);
                        string functionInfo = this.Command.CommandID;
                        if (this.Command.CommandName != null && this.Command.CommandName != "") functionInfo = StringParser.Parse(this.Command.CommandName);
                        LoggingService.Info("Function " + functionInfo + " run.");
                        if (this.ButtonStyle == BarButtonStyle.Check)
                        {
                            if (this.groupObj != null && this.groupObj is MenuButtonGroupCommand)
                            {
                                MenuButtonGroupCommand mbgc = this.groupObj as MenuButtonGroupCommand;
                                foreach (object itemObj in mbgc.SubItems)
                                {
                                    if (itemObj is MenuButtonCommand && !itemObj.Equals(this))
                                    {
                                        (itemObj as MenuButtonCommand).Down = false;
                                    }
                                    else (itemObj as MenuButtonCommand).Down = true;
                                }
                            }
                        }
                        return;
                    }
                    XtraMessageBox.Show(StringParser.Parse("${res:ModeNoAuth}"), StringParser.Parse("${res:View_Prompt}"));
                }
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
                    toolTipTitleItem.Text=text.Split(new char[]
					{
						':'
					})[0];
                    toolTipItem.Text=text.Split(new char[]
					{
						':'
					})[1];
                }
                else
                {
                    toolTipItem.Text=text;
                }
                superToolTip.Items.Add(toolTipTitleItem);
                superToolTip.Items.Add(toolTipItem);
                this.SuperTip=superToolTip;
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
                        this.ItemShortcut=new BarShortcut(shortcut);
                    }
                }
                catch (System.Exception)
                {
                    try
                    {
                        System.Windows.Forms.Keys keys = (System.Windows.Forms.Keys)System.Enum.Parse(typeof(System.Windows.Forms.Keys), value);
                        this.ItemShortcut=new BarShortcut(keys);
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
            base.RibbonStyle=ribbonStyle;
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
                this.Caption=StringParser.Parse(this.codon.Properties["label"]);
                this.Glyph=WinFormsResourceService.GetBitmap(StringParser.Parse(this.codon.Properties["icon"]));
                this.ParseTooltips();
                this.ParseRibbonItemStyle();
                this.ParseShortCut();
            }
        }
    }
}
