using System;
using System.Collections.Generic;
using ICSharpCode.Core;
namespace DFWinForms.Command
{
    public static class CommandManager
    {
        private static Stack<ICommand> commandStack = new Stack<ICommand>();
        public static int CountCmd
        {
            get
            {
                return CommandManager.commandStack.Count;
            }
        }
        public static void Push(ICommand comObj)
        {
            if (comObj == null)
            {
                return;
            }
            while (CommandManager.commandStack.Count > 0)
            {
                CommandManager.Pop();
            }
            CommandManager.commandStack.Push(comObj);
            ICommand command = CommandManager.commandStack.Peek();
            if (command != null)
            {
                command.HighLight = true;
            }
        }
        public static ICommand Peek()
        {
            if (CommandManager.commandStack.Count > 0)
            {
                return CommandManager.commandStack.Peek();
            }
            return null;
        }
        public static ICommand Pop()
        {
            if (CommandManager.commandStack.Count > 0)
            {
                ICommand command = CommandManager.commandStack.Pop();
                if (command != null)
                {
                    command.RestoreEnv();
                    command.HighLight = false;
                }
                return command;
            }
            return null;
        }
        public static void ClearCmd()
        {
            if (CommandManager.commandStack.Count > 0)
            {
                CommandManager.commandStack.Clear();
            }
        }
    }
}
