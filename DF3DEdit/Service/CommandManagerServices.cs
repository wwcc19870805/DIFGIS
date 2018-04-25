using System;
using System.Collections.Generic;
using DF3DEdit.Class;
using DF3DEdit.Service;
using DF3DEdit.Delegate;


namespace DF3DEdit.Service
{
    public class CommandManagerServices
    {
        private static CommandManagerServices cmdMgr;
        private Stack<BaseCommand> stackUndo;
        private Stack<BaseCommand> stackRedo;
        private System.Collections.Generic.List<string> undoNameList;
        private System.Collections.Generic.List<string> redoNameList;
        private UndoRedoStateEventArgs eventArgs;
        public event UndoRedoStateInspectorEventHandler UndoRedoStateInspector;
        public System.Collections.Generic.List<string> UndoNameList
        {
            get
            {
                return this.undoNameList;
            }
            set
            {
                this.undoNameList = value;
            }
        }
        public System.Collections.Generic.List<string> RedoNameList
        {
            get
            {
                return this.redoNameList;
            }
            set
            {
                this.redoNameList = value;
            }
        }
        private CommandManagerServices()
        {
            this.stackUndo = new Stack<BaseCommand>();
            this.stackRedo = new Stack<BaseCommand>();
            this.undoNameList = new System.Collections.Generic.List<string>();
            this.redoNameList = new System.Collections.Generic.List<string>();
            this.eventArgs = new UndoRedoStateEventArgs();
        }
        public static CommandManagerServices Instance()
        {
            if (CommandManagerServices.cmdMgr == null)
            {
                CommandManagerServices.cmdMgr = new CommandManagerServices();
            }
            return CommandManagerServices.cmdMgr;
        }
        private void PushUndoCommand(BaseCommand cmd)
        {
            if (cmd != null)
            {
                this.stackUndo.Push(cmd);
                this.undoNameList.Insert(0, cmd.CommandName);
            }
        }
        private BaseCommand PopUndoCommand()
        {
            BaseCommand result = null;
            if (this.stackUndo.Count > 0)
            {
                result = this.stackUndo.Peek();
                this.stackUndo.Pop();
                this.undoNameList.RemoveAt(0);
            }
            return result;
        }
        private void PushRedoCommand(BaseCommand cmd)
        {
            if (cmd != null)
            {
                this.stackRedo.Push(cmd);
                this.redoNameList.Insert(0, cmd.CommandName);
            }
        }
        private BaseCommand PopRedoCommand()
        {
            BaseCommand result = null;
            if (this.stackRedo.Count > 0)
            {
                result = this.stackRedo.Peek();
                this.stackRedo.Pop();
                this.redoNameList.RemoveAt(0);
            }
            return result;
        }
        private void DeleteUndoCommands()
        {
            this.stackUndo.Clear();
            this.undoNameList.Clear();
        }
        private void DeleteRedoCommands()
        {
            this.stackRedo.Clear();
            this.redoNameList.Clear();
        }
        public void StartCommand()
        {
            CommonUtils.Instance().FdeUndoRedoManager.StartCommand();
        }
        public bool CallCommand(BaseCommand cmd)
        {
            if (cmd != null)
            {
                this.PushUndoCommand(cmd);
                this.DeleteRedoCommands();
                this.eventArgs.CanRedo = this.CanRedo();
                this.eventArgs.CanUndo = this.CanUndo();
                if (this.UndoRedoStateInspector != null)
                {
                    this.UndoRedoStateInspector(this.eventArgs);
                }
                //MainFrmService.UpdateMenu();
                return true;
            }
            return false;
        }
        public void ClearAllCommands()
        {
            this.DeleteUndoCommands();
            this.DeleteRedoCommands();
            this.eventArgs.CanRedo = this.CanRedo();
            this.eventArgs.CanUndo = this.CanUndo();
            if (this.UndoRedoStateInspector != null)
            {
                this.UndoRedoStateInspector(this.eventArgs);
            }
            //MainFrmService.UpdateMenu();
        }
        public void Undo()
        {
            BaseCommand baseCommand = this.PopUndoCommand();
            if (baseCommand != null)
            {
                if (baseCommand.Undo())
                {
                    this.PushRedoCommand(baseCommand);
                    this.eventArgs.CanRedo = this.CanRedo();
                    this.eventArgs.CanUndo = this.CanUndo();
                    if (this.UndoRedoStateInspector != null)
                    {
                        this.UndoRedoStateInspector(this.eventArgs);
                    }
                }
                //MainFrmService.UpdateMenu();
            }
        }
        public void Redo()
        {
            BaseCommand baseCommand = this.PopRedoCommand();
            if (baseCommand != null)
            {
                if (baseCommand.Redo())
                {
                    this.PushUndoCommand(baseCommand);
                    this.eventArgs.CanRedo = this.CanRedo();
                    this.eventArgs.CanUndo = this.CanUndo();
                    if (this.UndoRedoStateInspector != null)
                    {
                        this.UndoRedoStateInspector(this.eventArgs);
                    }
                }
                //MainFrmService.UpdateMenu();
            }
        }
        public bool CanUndo()
        {
            return CommonUtils.Instance().GetCurrentFeatureDataset() != null && !CommonUtils.Instance().EnableTemproalEdit && !CommonUtils.Instance().IsServerClientType && this.stackUndo.Count > 0;
        }
        public bool CanRedo()
        {
            return CommonUtils.Instance().GetCurrentFeatureDataset() != null && !CommonUtils.Instance().EnableTemproalEdit && !CommonUtils.Instance().IsServerClientType && this.stackRedo.Count > 0;
        }
    }
}
