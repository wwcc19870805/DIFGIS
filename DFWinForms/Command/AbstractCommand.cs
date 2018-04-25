using System;
using DFWinForms.Base;
using ICSharpCode.Core;
namespace DFWinForms.Command
{
    public abstract class AbstractCommand : ICommand
    {
        private object hook = null;
        private bool highLight;
        private string commandName;
        private string commandID;
        public event StatusUpdateHandle StatusChanged;
        public object Hook
        {
            get
            {
                return hook;
            }
            set
            {
                hook = value;
            }
        }
        public bool HighLight
        {
            get
            {
                return this.highLight;
            }
            set
            {
                this.highLight = value;
                if (this.StatusChanged != null)
                {
                    this.StatusChanged(this.highLight);
                }
            }
        }
        public virtual string CommandName
        {
            get
            {
                if (string.IsNullOrEmpty(this.commandName))
                {
                    this.commandName = "Unknown";
                }
                return this.commandName;
            }
            set
            {
                this.commandName = value;
            }
        }
        public virtual string CommandID
        {
            get
            {
                if (string.IsNullOrEmpty(this.commandID))
                {
                    this.commandID = "Unknown";
                }
                return this.commandID;
            }
            set
            {
                this.commandID = value;
            }
        }
        public AbstractCommand()
        {
            this.hook = DFApplication.Application;
        }
        public virtual void Init(object sender)
        {

        }
        public abstract void Run(object sender, System.EventArgs e);
        public virtual void RestoreEnv()
        {

        }
        public virtual bool CheckPermission()
        {
            return true;
        }
    }
}
