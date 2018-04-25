using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Windows.Forms;
using ICSharpCode.Core;
using DFWinForms.Class;
using DFWinForms.Service;
using DevExpress.XtraBars.Ribbon;
namespace DFWinForms.Component
{
    public class MenuTextCommand : BarEditItem, IStatusUpdate
    {
        private object caller;
        private Codon codon;
        private ICommand menuCommand;
        private RepositoryItemTextEdit repositoryItemTextEdit;
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
                return false;
            }
            set
            {
            }
        }
        public MenuTextCommand(Codon codon, object caller)
            : this(codon, caller, false)
        {
        }
        public MenuTextCommand(Codon codon, object caller, bool createCommand)
		{
			this.repositoryItemTextEdit = new RepositoryItemTextEdit();
			this.caller = caller;
			this.codon = codon;
			base.Edit = this.repositoryItemTextEdit;
			base.Name = codon.Id;
			this.repositoryItemTextEdit.EditValueChanged +=new EventHandler(repositoryItemTextEdit_EditValueChanged);
            this.UpdateText();
            if (createCommand)
			{
				this.CreateCommand();
			}
		}

        public MenuTextCommand(string label)
		{
            this.repositoryItemTextEdit = new RepositoryItemTextEdit();
            this.codon = null;
			this.caller = null;
            base.Edit = this.repositoryItemTextEdit;
            base.Name = this.codon.Id;
            this.Width = 80;
			this.Caption = StringParser.Parse(label);
			this.repositoryItemTextEdit.EditValueChanged +=new EventHandler(repositoryItemTextEdit_EditValueChanged); 
        }

        private void repositoryItemTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            if (this.codon != null && edit != null)
            {
                ICommand command = this.Command;
                if (command != null)
                {
                    if (command.CheckPermission())
                    {
                        command.Run(sender, e);
                        string functionInfo = this.Command.CommandID;
                        if (this.Command.CommandName != null && this.Command.CommandName != "") functionInfo = StringParser.Parse(this.Command.CommandName);
                        LoggingService.Info("Function " + functionInfo + " run.");
                        return;
                    }
                    else
                    {
                        XtraMessageBox.Show(StringParser.Parse("${res:ModeNoAuth}"), StringParser.Parse("${res:View_Prompt}"));
                    }
                }
            }
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
        private void ParsePropertys()
        {
            if (this.codon == null)
            {
                return;
            }
            this.Visibility = (BarItemVisibility)((StringParser.Parse(this.codon.Properties["visible"]) == "false") ? 1 : 0);
            if (this.codon.Properties.Contains("width"))
            {
                string text3 = StringParser.Parse(this.codon.Properties["width"]);
                if (!string.IsNullOrEmpty(text3))
                {
                    int width;
                    bool bRes= int.TryParse(text3, out width);
                    if (bRes) this.Width = width;
                    else this.Width = 80;
                }
            }
        }

        public virtual void UpdateText()
        {
            if (this.codon != null)
            {
                this.Glyph = WinFormsResourceService.GetBitmap(StringParser.Parse(this.codon.Properties["icon"]));
                this.Caption = StringParser.Parse(this.codon.Properties["label"]);
                this.EditValue = StringParser.Parse(this.codon.Properties["defaultValue"]);
                base.CaptionAlignment = HorzAlignment.Near;
                this.ParsePropertys();
                this.ParseRibbonItemStyle();
                this.ParseTooltips();
            }
        }
    }
}
