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
    public class MenuSpinCommand : BarEditItem, IStatusUpdate
    {
        private object caller;
        private Codon codon;
        private ICommand menuCommand;
        private RepositoryItemSpinEdit repositoryItemSpinEdit;
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
        public MenuSpinCommand(Codon codon, object caller)
            : this(codon, caller, false)
        {
        }
        public MenuSpinCommand(Codon codon, object caller, bool createCommand)
		{
            this.repositoryItemSpinEdit = new RepositoryItemSpinEdit();
			this.caller = caller;
			this.codon = codon;
            this.Width = 80;
            base.Edit = this.repositoryItemSpinEdit;
			base.Name = codon.Id;
            this.repositoryItemSpinEdit.EditValueChanged += new EventHandler(repositoryItemSpinEdit_EditValueChanged);
            this.UpdateText();
            if (createCommand)
			{
				this.CreateCommand();
			}
		}


        public MenuSpinCommand(string label)
		{
            this.repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            this.codon = null;
			this.caller = null;
            base.Edit = this.repositoryItemSpinEdit;
            base.Name = this.codon.Id; 
            this.Width = 80;
			this.Caption = StringParser.Parse(label);
            this.repositoryItemSpinEdit.EditValueChanged += new EventHandler(repositoryItemSpinEdit_EditValueChanged);
        }

        private void repositoryItemSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit edit = sender as SpinEdit;
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

        private void ParsePropertys()
        {
            if (this.codon == null)
            {
                return;
            }
            this.Visibility = (BarItemVisibility)((StringParser.Parse(this.codon.Properties["visible"]) == "false") ? 1 : 0);
            if (this.codon.Properties.Contains("maximum"))
            {
                string text = StringParser.Parse(this.codon.Properties["maximum"]);
                if (!string.IsNullOrEmpty(text))
                {
                    int maximum;
                    bool bRes = int.TryParse(text, out maximum);
                    if(bRes) this.repositoryItemSpinEdit.MaxValue = maximum;
                }
            }
            if (this.codon.Properties.Contains("minimum"))
            {
                string text2 = StringParser.Parse(this.codon.Properties["minimum"]);
                if (!string.IsNullOrEmpty(text2))
                {
                    int minimum;
                    bool bRes = int.TryParse(text2, out minimum);
                    if(bRes) this.repositoryItemSpinEdit.MinValue = minimum;
                }
            }
            if (this.codon.Properties.Contains("increment"))
            {
                string text3 = StringParser.Parse(this.codon.Properties["increment"]);
                if (!string.IsNullOrEmpty(text3))
                {
                    int increment;
                    bool bRes = int.TryParse(text3, out increment);
                    if(bRes) this.repositoryItemSpinEdit.Increment = increment;
                }
            }
            if (this.codon.Properties.Contains("width"))
            {
                string text3 = StringParser.Parse(this.codon.Properties["width"]);
                if (!string.IsNullOrEmpty(text3))
                {
                    int width;
                    bool bRes = int.TryParse(text3, out width);
                    if (bRes) this.Width = width;
                    else this.Width = 80;
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
