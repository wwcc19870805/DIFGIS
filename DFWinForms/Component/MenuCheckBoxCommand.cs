using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Windows.Forms;
using ICSharpCode.Core;
using DFWinForms.Class;
using DFWinForms.Service;
namespace DFWinForms.Component
{
    public class MenuCheckBoxCommand : BarEditItem, IStatusUpdate
    {
        private object caller;
        private Codon codon;
        private ICommand menuCommand;
        private RepositoryItemCheckEdit repositoryItemCheckEdit;
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
        public MenuCheckBoxCommand(Codon codon, object caller)
            : this(codon, caller, false)
        {
        }
        public MenuCheckBoxCommand(Codon codon, object caller, bool createCommand)
		{
			this.repositoryItemCheckEdit = new RepositoryItemCheckEdit();
			this.caller = caller;
			this.codon = codon;
			base.Edit = this.repositoryItemCheckEdit;
            this.Width = 20;
			base.Name = codon.Id;
			this.repositoryItemCheckEdit.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit_CheckedChanged);
            this.UpdateText();
            if (createCommand)
			{
				this.CreateCommand();
			}
		}
        public MenuCheckBoxCommand(string label)
		{
			this.repositoryItemCheckEdit = new RepositoryItemCheckEdit();
			this.codon = null;
			this.caller = null;
			base.Edit = this.repositoryItemCheckEdit;
            this.Width = 60;
            base.Name = this.codon.Id;
			this.Caption = StringParser.Parse(label);
			this.repositoryItemCheckEdit.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit_CheckedChanged);
		}
        private void repositoryItemCheckEdit_CheckedChanged(object sender, System.EventArgs e)
        {
            CheckEdit checkEdit = sender as CheckEdit;
            if (this.codon != null && checkEdit != null)
            {
                ICommand command = this.Command;
                if (command != null)
                {
                    if (command.CheckPermission())
                    {
                        if (checkEdit.Checked)
                        {
                            command.Run(sender, e);
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
            if (this.codon.Properties.Contains("width"))
            {
                string text3 = StringParser.Parse(this.codon.Properties["width"]);
                if (!string.IsNullOrEmpty(text3))
                {
                    int width;
                    bool bRes = int.TryParse(text3, out width);
                    if (bRes) this.Width = width;
                    else this.Width = 60;
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
                ConditionAction checkStateAction = this.codon.GetCheckStateAction(this.caller);
                if (checkStateAction == ConditionAction.Checked || checkStateAction == ConditionAction.UnChecked)
                {
                    bool @checked = checkStateAction == ConditionAction.Checked;
                    base.EditValue = @checked;
                }
            }
        }
        public virtual void UpdateText()
        {
            if (this.codon != null)
            {
                this.Glyph=WinFormsResourceService.GetBitmap(StringParser.Parse(this.codon.Properties["icon"]));
                this.Caption=StringParser.Parse(this.codon.Properties["label"]);
                base.RibbonStyle= DevExpress.XtraBars.Ribbon.RibbonItemStyles.All;
                base.CaptionAlignment= HorzAlignment.Far;
                this.ParsePropertys();
                this.ParseTooltips();
                this.ParseShortCut();
            }
        }
    }
}
