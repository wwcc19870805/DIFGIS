using System;
using System.Collections.Generic;
namespace DF2DControl.Command
{
    public static class Map2DCommandManager
    {
        private static Stack<IMap2DCommand> commandStack = new Stack<IMap2DCommand>();
        public static int CountCmd
        {
            get
            {
                return Map2DCommandManager.commandStack.Count;
            }
        }
        public static void Push(IMap2DCommand comObj)
        {
            if (comObj == null)
            {
                return;
            }
            while (Map2DCommandManager.commandStack.Count > 0)
            {
                Map2DCommandManager.Pop();
            }
            Map2DCommandManager.commandStack.Push(comObj);
            IMap2DCommand command = Map2DCommandManager.commandStack.Peek();
            if (command != null)
            {
                command.HighLight = true;
            }
        }
        public static IMap2DCommand Peek()
        {
            if (Map2DCommandManager.commandStack.Count > 0)
            {
                return Map2DCommandManager.commandStack.Peek();
            }
            return null;
        }
        public static IMap2DCommand Pop()
        {
            if (Map2DCommandManager.commandStack.Count > 0)
            {
                IMap2DCommand command = Map2DCommandManager.commandStack.Pop();
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
            if (Map2DCommandManager.commandStack.Count > 0)
            {
                Map2DCommandManager.commandStack.Clear();
            }
        }
    }
}
