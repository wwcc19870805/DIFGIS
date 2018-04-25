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
    public class MenuCheckCommand : BarCheckItem, IStatusUpdate
    {
        private object caller;
        private Codon codon;
        private ICommand menuCommand;
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
                //return this.get_Down();
                return false;
            }
            set
            {
                this.Checked = value;
                //this.set_Down(value);
            }
        }
        public MenuCheckCommand(Codon codon, object caller)
            : this(codon, caller, false)
        {
        }
        public MenuCheckCommand(Codon codon, object caller, bool createCommand)
        {
            this.caller = caller;
            this.codon = codon;
            base.Name = codon.Id;
            this.UpdateText();
            if (createCommand)
            {
                this.CreateCommand();
            }
        }
        public MenuCheckCommand(string label)
            : this()
        {
            this.Caption = StringParser.Parse(label);
        }
        public MenuCheckCommand()
        {
            this.codon = null;
            this.caller = null;
        }
        private void CreateCommand()
        {
            try
            {
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
            catch (System.Exception)
            {
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
                        if (this.Checked)
                        {
                            command.Run(this, null);
                            string functionInfo = this.Command.CommandID;
                            if (this.Command.CommandName != null && this.Command.CommandName != "") functionInfo = StringParser.Parse(this.Command.CommandName);
                            LoggingService.Info("Function " + functionInfo + " run.");
                            return;
                        }
                        command.RestoreEnv();
                        return;
                    }
                    else
                    {
                        XtraMessageBox.Show(StringParser.Parse("${res:ModeNoAuth}"), StringParser.Parse("${res:View_Prompt}"));
                    }
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
                System.Windows.Forms.Shortcut shortcut = (System.Windows.Forms.Shortcut)System.Enum.Parse(typeof(System.Windows.Forms.Shortcut), value);
                if (shortcut != System.Windows.Forms.Shortcut.None)
                {
                    this.ItemShortcut=new BarShortcut(shortcut);
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
        public void UpdateStatus()
        {
            if (this.codon != null)
            {
                ConditionAction failedAction = this.codon.GetFailedAction(this.caller);
                bool enabled = failedAction != ConditionAction.Disable;
                this.Enabled = enabled;                
                bool visibled = failedAction != ConditionAction.Exclude;
                if (visibled) this.Visibility = BarItemVisibility.Always;
                else this.Visibility = BarItemVisibility.Never;
                ConditionAction checkStateAction = this.codon.GetCheckStateAction(this.caller);
                if (checkStateAction == ConditionAction.Checked || checkStateAction == ConditionAction.UnChecked)
                {
                    bool @checked = checkStateAction == ConditionAction.Checked;
                    base.Checked = @checked;
                }
            }
        }
        public void UpdateText()
        {
            if (this.codon != null)
            {
                this.Glyph=WinFormsResourceService.GetBitmap(StringParser.Parse(this.codon.Properties["icon"]));
                this.Caption=StringParser.Parse(this.codon.Properties["label"]);
                this.Checked = (StringParser.Parse(this.codon.Properties["checked"]) == "true");
                int groupIndex = 0;
                bool bRes = int.TryParse(StringParser.Parse(this.codon.Properties["groupIndex"]), out groupIndex);
                if (bRes) this.GroupIndex = groupIndex;
                this.ParseTooltips();
                this.ParseRibbonItemStyle();
                this.ParseShortCut();
            }
        }
    }
}
