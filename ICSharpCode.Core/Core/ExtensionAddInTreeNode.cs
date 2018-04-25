using System;
using System.Collections.Generic;
using System.Reflection;
namespace ICSharpCode.Core
{
    public class ExtensionAddInTreeNode
    {
        private string assemblyPath;
        private System.Collections.Generic.IList<ICommand> commandList;
        public string AssemblyPath
        {
            get
            {
                return this.assemblyPath;
            }
        }
        public System.Collections.Generic.IList<ICommand> CommandList
        {
            get
            {
                return this.commandList;
            }
        }
        private void GetCommands()
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(this.assemblyPath);
                if (assembly != null)
                {
                    System.Type[] types = assembly.GetTypes();
                    System.Type[] array = types;
                    for (int i = 0; i < array.Length; i++)
                    {
                        System.Type type = array[i];
                        if (type.GetInterface("ICommand") != null)
                        {
                            ICommand command = assembly.CreateInstance(type.FullName) as ICommand;
                            if (command != null)
                            {
                                this.commandList.Add(command);
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
        public ExtensionAddInTreeNode(string assembly)
        {
            this.assemblyPath = assembly;
            this.commandList = new System.Collections.Generic.List<ICommand>();
            this.GetCommands();
        }
    }
}
