using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
namespace ICSharpCode.Core
{
    public class Runtime
    {
        private string hintPath;
        private string assembly;
        private System.Reflection.Assembly loadedAssembly;
        private System.Collections.Generic.IList<LazyLoadDoozer> definedDoozers = new System.Collections.Generic.List<LazyLoadDoozer>();
        private System.Collections.Generic.IList<LazyConditionEvaluator> definedConditionEvaluators = new System.Collections.Generic.List<LazyConditionEvaluator>();
        private ICondition[] conditions;
        private bool isActive = true;
        private bool isAssemblyLoaded;
        public bool IsActive
        {
            get
            {
                if (this.conditions != null)
                {
                    this.isActive = (Condition.GetFailedAction(this.conditions, this) == ConditionAction.Nothing);
                    this.conditions = null;
                }
                return this.isActive;
            }
        }
        public string Assembly
        {
            get
            {
                return this.assembly;
            }
        }
        public System.Reflection.Assembly LoadedAssembly
        {
            get
            {
                this.Load();
                return this.loadedAssembly;
            }
        }
        public System.Collections.Generic.IList<LazyLoadDoozer> DefinedDoozers
        {
            get
            {
                return this.definedDoozers;
            }
        }
        public System.Collections.Generic.IList<LazyConditionEvaluator> DefinedConditionEvaluators
        {
            get
            {
                return this.definedConditionEvaluators;
            }
        }
        public Runtime(string assembly, string hintPath)
        {
            this.assembly = assembly;
            this.hintPath = hintPath;
        }
        public void Load()
        {
            if (!this.isAssemblyLoaded)
            {
                this.isAssemblyLoaded = true;
                try
                {
                    if (this.assembly[0] == ':')
                    {
                        this.loadedAssembly = System.Reflection.Assembly.Load(this.assembly.Substring(1));
                    }
                    else
                    {
                        if (this.assembly[0] == '$')
                        {
                            int num = this.assembly.IndexOf('/');
                            if (num < 0)
                            {
                                throw new System.ApplicationException("Expected '/' in path beginning with '$'!");
                            }
                            string text = this.assembly.Substring(1, num - 1);
                            foreach (AddIn current in AddInTree.AddIns)
                            {
                                if (current.Enabled && current.Manifest.Identities.ContainsKey(text))
                                {
                                    string assemblyFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(current.FileName), this.assembly.Substring(num + 1));
                                    this.loadedAssembly = System.Reflection.Assembly.LoadFrom(assemblyFile);
                                    break;
                                }
                            }
                            if (this.loadedAssembly == null)
                            {
                                throw new System.IO.FileNotFoundException("Could not find referenced AddIn " + text);
                            }
                        }
                        else
                        {
                            this.loadedAssembly = System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(this.hintPath, this.assembly));
                        }
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                }
                catch (System.IO.FileLoadException)
                {
                }
            }
        }
        public object CreateInstance(string instance)
        {
            if (!this.IsActive)
            {
                return null;
            }
            System.Reflection.Assembly assembly = this.LoadedAssembly;
            if (assembly == null)
            {
                return null;
            }
            return assembly.CreateInstance(instance);
        }
        internal static void ReadSection(XmlReader reader, AddIn addIn, string hintPath)
        {
            Stack<ICondition> stack = new Stack<ICondition>();
            while (reader.Read())
            {
                XmlNodeType nodeType = reader.NodeType;
                if (nodeType != XmlNodeType.Element)
                {
                    if (nodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "Condition" || reader.LocalName == "ComplexCondition")
                        {
                            stack.Pop();
                        }
                        else
                        {
                            if (reader.LocalName == "Runtime")
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    string localName;
                    if ((localName = reader.LocalName) != null)
                    {
                        if (!(localName == "Condition"))
                        {
                            if (!(localName == "ComplexCondition"))
                            {
                                if (!(localName == "Import"))
                                {
                                    if (localName == "DisableAddIn")
                                    {
                                        if (Condition.GetFailedAction(stack, addIn) == ConditionAction.Nothing)
                                        {
                                            addIn.CustomErrorMessage = reader.GetAttribute("message");
                                        }
                                    }
                                }
                                else
                                {
                                    addIn.Runtimes.Add(Runtime.Read(addIn, reader, hintPath, stack));
                                }
                            }
                            else
                            {
                                stack.Push(Condition.ReadComplexCondition(reader));
                            }
                        }
                        else
                        {
                            stack.Push(Condition.Read(reader));
                        }
                    }
                }
            }
        }
        internal static Runtime Read(AddIn addIn, XmlReader reader, string hintPath, Stack<ICondition> conditionStack)
        {
            int arg_08_0 = reader.AttributeCount;
            Runtime runtime = new Runtime(reader.GetAttribute(0), hintPath);
            if (conditionStack.Count > 0)
            {
                runtime.conditions = conditionStack.ToArray();
            }
            if (!reader.IsEmptyElement)
            {
                while (reader.Read())
                {
                    XmlNodeType nodeType = reader.NodeType;
                    if (nodeType != XmlNodeType.Element)
                    {
                        if (nodeType == XmlNodeType.EndElement && reader.LocalName == "Import")
                        {
                            return runtime;
                        }
                    }
                    else
                    {
                        string localName = reader.LocalName;
                        Properties properties = Properties.ReadFromAttributes(reader);
                        string a;
                        if ((a = localName) != null)
                        {
                            if (!(a == "Doozer"))
                            {
                                if (a == "ConditionEvaluator")
                                {
                                    bool arg_B3_0 = reader.IsEmptyElement;
                                    runtime.definedConditionEvaluators.Add(new LazyConditionEvaluator(addIn, properties));
                                }
                            }
                            else
                            {
                                bool arg_98_0 = reader.IsEmptyElement;
                                runtime.definedDoozers.Add(new LazyLoadDoozer(addIn, properties));
                            }
                        }
                    }
                }
            }
            runtime.definedDoozers = (runtime.definedDoozers as System.Collections.Generic.List<LazyLoadDoozer>).AsReadOnly();
            runtime.definedConditionEvaluators = (runtime.definedConditionEvaluators as System.Collections.Generic.List<LazyConditionEvaluator>).AsReadOnly();
            return runtime;
        }
    }
}
