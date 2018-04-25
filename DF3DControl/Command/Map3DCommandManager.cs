using System;
using System.Collections.Generic;
namespace DF3DControl.Command
{
    public static class Map3DCommandManager
    {
        private static Stack<IMap3DCommand> commandStack = new Stack<IMap3DCommand>();
        public static int CountCmd
        {
            get
            {
                return Map3DCommandManager.commandStack.Count;
            }
        }
        public static void Push(IMap3DCommand comObj)
        {
            if (comObj == null)
            {
                return;
            }
            while (Map3DCommandManager.commandStack.Count > 0)
            {
                Map3DCommandManager.Pop();
            }
            Map3DCommandManager.commandStack.Push(comObj);
            IMap3DCommand command = Map3DCommandManager.commandStack.Peek();
            if (command != null)
            {
                command.HighLight = true;
            }
        }
        public static IMap3DCommand Peek()
        {
            if (Map3DCommandManager.commandStack.Count > 0)
            {
                return Map3DCommandManager.commandStack.Peek();
            }
            return null;
        }
        public static IMap3DCommand Pop()
        {
            if (Map3DCommandManager.commandStack.Count > 0)
            {
                IMap3DCommand command = Map3DCommandManager.commandStack.Pop();
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
            if (Map3DCommandManager.commandStack.Count > 0)
            {
                Map3DCommandManager.commandStack.Clear();
            }
        }
    }
}
