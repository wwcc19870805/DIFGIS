using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Windows.Forms;
using ICSharpCode.Core;
using DFWinForms.Class;
namespace DFWinForms.Component
{
    public class ContextMenuCommand : System.Windows.Forms.ToolStripMenuItem, IStatusUpdate
    {
        private Codon codon;
        private object caller;
        private System.Collections.IList subItems;
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
                return false;
            }
            set
            {
            }
        }
        public ContextMenuCommand(Codon codon, object caller, System.Collections.IList subItems)
        {
            if (subItems == null)
            {
                subItems = new System.Collections.ArrayList();
            }
            this.codon = codon;
            this.caller = caller;
            this.subItems = subItems;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            base.Name = codon.Id;
            this.UpdateText();
        }
        public ContextMenuCommand(string text, params System.Windows.Forms.ToolStripItem[] subItems)
        {
            this.Text = StringParser.Parse(text);
            base.Name = this.codon.Id;
            base.DropDownItems.AddRange(subItems);
        }
        private void CreateDropDownItems()
        {
            base.DropDownItems.Clear();
            foreach (object current in this.subItems)
            {
                if (current is System.Windows.Forms.ToolStripItem)
                {
                    base.DropDownItems.Add((System.Windows.Forms.ToolStripItem)current);
                    if (current is IStatusUpdate)
                    {
                        //((IStatusUpdate)current).UpdateStatus();
                        ((IStatusUpdate)current).UpdateText();
                    }
                }
            }
        }
        protected override void OnDropDownShow(System.EventArgs e)
        {
            if (this.codon != null && !base.DropDown.Visible)
            {
                this.CreateDropDownItems();
            }
            base.OnDropDownShow(e);
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

                }
            }
            catch (System.Exception)
            {
            }
        }
        protected override void OnClick(System.EventArgs e)
        {
            base.OnClick(e);
            if (this.codon != null)
            {
                ICommand command = this.Command;
                if (command != null)
                {
                    if (command.CheckPermission())
                    {
                        command.Run(this, e);
                        string functionInfo = this.Command.CommandID;
                        if (this.Command.CommandName != null && this.Command.CommandName != "") functionInfo = this.Command.CommandName;
                        LoggingService.Info("Function " + functionInfo + " run.");
                        return;
                    }
                    XtraMessageBox.Show(StringParser.Parse("${res:ModeNoAuth}"), StringParser.Parse("${res:View_Prompt}"));
                }
            }
        }
        public void UpdateStatus()
        {
            if (this.codon != null)
            {
                ConditionAction failedAction = this.codon.GetFailedAction(this.caller);
                bool enabled = failedAction != ConditionAction.Disable;
                this.Enabled = enabled;
                bool visibled = failedAction != ConditionAction.Exclude;
                this.Visible = visibled;
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
                this.Text = StringParser.Parse(this.codon.Properties["label"]);
            }
        }
    }
}
