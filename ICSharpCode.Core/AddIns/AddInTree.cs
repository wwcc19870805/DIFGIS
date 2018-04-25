using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace ICSharpCode.Core
{
    public static class AddInTree
    {
        private static System.Collections.Generic.List<AddIn> addIns;
        private static System.Collections.Generic.Dictionary<string, IDoozer> doozers;
        private static AddInTreeNode rootNode;
        private static System.Collections.Generic.Dictionary<string, IConditionEvaluator> conditionEvaluators;
        private static System.Collections.Generic.List<string> disableMenuItem;
        private static System.Collections.Generic.List<string> quickToolbarItem;
        public static System.Collections.Generic.List<string> QuickToolbarItem
        {
            get
            {
                string path = System.IO.Path.Combine(PropertyService.ConfigDirectory, "QuickToolbarAddIns.addin");
                if (!System.IO.File.Exists(path))
                {
                    path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\AddIns\\DefaultQuickToolBar.ini");
                }
                if (System.IO.File.Exists(path))
                {
                    string[] array = System.IO.File.ReadAllLines(path);
                    AddInTree.quickToolbarItem.Clear();
                    string[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        string item = array2[i];
                        AddInTree.quickToolbarItem.Add(item);
                    }
                }
                return AddInTree.quickToolbarItem;
            }
        }
        public static System.Collections.Generic.List<string> DisableMenuItem
        {
            get
            {
                string path = System.IO.Path.Combine(PropertyService.ConfigDirectory, "UnVisibleAddIns.addin");
                if (System.IO.File.Exists(path))
                {
                    string[] array = System.IO.File.ReadAllLines(path);
                    AddInTree.disableMenuItem.Clear();
                    string[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        string item = array2[i];
                        AddInTree.disableMenuItem.Add(item);
                    }
                }
                return AddInTree.disableMenuItem;
            }
        }
        public static System.Collections.Generic.IList<AddIn> AddIns
        {
            get
            {
                return AddInTree.addIns.AsReadOnly();
            }
        }
        public static System.Collections.Generic.Dictionary<string, IDoozer> Doozers
        {
            get
            {
                return AddInTree.doozers;
            }
        }
        public static System.Collections.Generic.Dictionary<string, IConditionEvaluator> ConditionEvaluators
        {
            get
            {
                return AddInTree.conditionEvaluators;
            }
        }
        static AddInTree()
        {
            AddInTree.addIns = new System.Collections.Generic.List<AddIn>();
            AddInTree.doozers = new System.Collections.Generic.Dictionary<string, IDoozer>();
            AddInTree.rootNode = new AddInTreeNode();
            AddInTree.conditionEvaluators = new System.Collections.Generic.Dictionary<string, IConditionEvaluator>();
            AddInTree.disableMenuItem = new System.Collections.Generic.List<string>();
            AddInTree.quickToolbarItem = new System.Collections.Generic.List<string>();
        }
        public static void Load(System.Collections.Generic.List<string> addInFiles, List<string> disabledAddIns)
        {
            System.Collections.Generic.List<AddIn> list = new System.Collections.Generic.List<AddIn>();
            System.Collections.Generic.Dictionary<string, System.Version> dictionary = new System.Collections.Generic.Dictionary<string, System.Version>();
            System.Collections.Generic.Dictionary<string, AddIn> dictionary2 = new System.Collections.Generic.Dictionary<string, AddIn>();
            foreach (string current in addInFiles)
            {
                AddIn addIn;
                try
                {
                    addIn = AddIn.Load(current);
                }
                catch (System.Exception ex)
                {
                    addIn = new AddIn();
                    addIn.addInFileName = current;
                    addIn.CustomErrorMessage = ex.Message;
                }
                if (addIn.Action == AddInAction.CustomError)
                {
                    list.Add(addIn);
                }
                else
                {
                    addIn.Enabled = true;
                    if (disabledAddIns != null && disabledAddIns.Count > 0)
                    {
                        foreach (string current2 in addIn.Manifest.Identities.Keys)
                        {
                            if (disabledAddIns.Contains(current2))
                            {
                                addIn.Enabled = false;
                                break;
                            }
                        }
                    }
                    if (addIn.Enabled)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, System.Version> current3 in addIn.Manifest.Identities)
                        {
                            if (dictionary.ContainsKey(current3.Key))
                            {
                                addIn.Enabled = false;
                                addIn.Action = AddInAction.InstalledTwice;
                                break;
                            }
                            dictionary.Add(current3.Key, current3.Value);
                            dictionary2.Add(current3.Key, addIn);
                        }
                    }
                    list.Add(addIn);
                }
            }
            while (true)
            {
            IL_175:
                for (int i = 0; i < list.Count; i++)
                {
                    AddIn addIn2 = list[i];
                    if (addIn2.Enabled)
                    {
                        foreach (AddInReference current4 in addIn2.Manifest.Conflicts)
                        {
                            System.Version version;
                            if (current4.Check(dictionary, out version))
                            {
                                AddInTree.DisableAddin(addIn2, dictionary, dictionary2);
                                goto IL_175;
                            }
                        }
                        foreach (AddInReference current5 in addIn2.Manifest.Dependencies)
                        {
                            System.Version version;
                            if (!current5.Check(dictionary, out version))
                            {
                                AddInTree.DisableAddin(addIn2, dictionary, dictionary2);
                                goto IL_175;
                            }
                        }
                    }
                }
                break;
            }
            foreach (AddIn current6 in list)
            {
                try
                {
                    AddInTree.InsertAddIn(current6);
                }
                catch (System.Exception ex2)
                {
                    MessageBox.Show(ex2.Message);
                }
            }
        }
        private static void DisableAddin(AddIn addIn, System.Collections.Generic.Dictionary<string, System.Version> dict, System.Collections.Generic.Dictionary<string, AddIn> addInDict)
        {
            addIn.Enabled = false;
            addIn.Action = AddInAction.DependencyError;
            foreach (string current in addIn.Manifest.Identities.Keys)
            {
                dict.Remove(current);
                addInDict.Remove(current);
            }
        }
        public static void InsertAddIn(AddIn addIn)
        {
            if (addIn.Enabled)
            {
                foreach (ExtensionPath current in addIn.Paths.Values)
                {
                    AddInTree.AddExtensionPath(current);
                }
                foreach (Runtime current2 in addIn.Runtimes)
                {
                    if (current2.IsActive)
                    {
                        foreach (LazyLoadDoozer current3 in current2.DefinedDoozers)
                        {
                            AddInTree.Doozers.ContainsKey(current3.Name);
                            AddInTree.Doozers.Add(current3.Name, current3);
                        }
                        foreach (LazyConditionEvaluator current4 in current2.DefinedConditionEvaluators)
                        {
                            AddInTree.ConditionEvaluators.ContainsKey(current4.Name);
                            AddInTree.ConditionEvaluators.Add(current4.Name, current4);
                        }
                    }
                }
            }
            AddInTree.addIns.Add(addIn);
        }
        public static void RemoveAddIn(AddIn addIn)
        {
            if (addIn.Enabled)
            {
                throw new System.ArgumentException("Cannot remove enabled AddIns at runtime.");
            }
            AddInTree.addIns.Remove(addIn);
        }
        private static AddInTreeNode CreatePath(AddInTreeNode localRoot, string path)
        {
            if (path == null || path.Length == 0)
            {
                return localRoot;
            }
            string[] array = path.Split(new char[]
			{
				'/'
			});
            AddInTreeNode addInTreeNode = localRoot;
            for (int i = 0; i < array.Length; i++)
            {
                if (!addInTreeNode.ChildNodes.ContainsKey(array[i]))
                {
                    addInTreeNode.ChildNodes[array[i]] = new AddInTreeNode();
                }
                addInTreeNode = addInTreeNode.ChildNodes[array[i]];
            }
            return addInTreeNode;
        }
        private static void AddExtensionPath(ExtensionPath path)
        {
            AddInTreeNode addInTreeNode = AddInTree.CreatePath(AddInTree.rootNode, path.Name);
            foreach (Codon current in path.Codons)
            {
                addInTreeNode.Codons.Add(current);
            }
        }
        public static bool ExistsTreeNode(string path)
        {
            if (path == null || path.Length == 0)
            {
                return true;
            }
            string[] array = path.Split(new char[]
			{
				'/'
			});
            AddInTreeNode addInTreeNode = AddInTree.rootNode;
            for (int i = 0; i < array.Length; i++)
            {
                if (!addInTreeNode.ChildNodes.TryGetValue(array[i], out addInTreeNode))
                {
                    return false;
                }
            }
            return true;
        }
        public static AddInTreeNode GetTreeNode(string path)
        {
            return AddInTree.GetTreeNode(path, true);
        }
        public static AddInTreeNode GetTreeNode(string path, bool throwOnNotFound)
        {
            if (path == null || path.Length == 0)
            {
                return AddInTree.rootNode;
            }
            string[] array = path.Split(new char[]
			{
				'/'
			});
            AddInTreeNode addInTreeNode = AddInTree.rootNode;
            for (int i = 0; i < array.Length; i++)
            {
                if (!addInTreeNode.ChildNodes.TryGetValue(array[i], out addInTreeNode))
                {
                    return null;
                }
            }
            return addInTreeNode;
        }
        public static object BuildItem(string path, object caller)
        {
            int num = path.LastIndexOf('/');
            string path2 = path.Substring(0, num);
            string childItemID = path.Substring(num + 1);
            AddInTreeNode treeNode = AddInTree.GetTreeNode(path2);
            return treeNode.BuildChildItem(childItemID, caller, new System.Collections.ArrayList(AddInTree.BuildItems<object>(path, caller, false)));
        }
        public static System.Collections.Generic.List<T> BuildItems<T>(string path, object caller)
        {
            return AddInTree.BuildItems<T>(path, caller, true);
        }
        public static System.Collections.Generic.List<T> BuildItems<T>(string path, object caller, bool throwOnNotFound)
        {
            AddInTreeNode treeNode = AddInTree.GetTreeNode(path, throwOnNotFound);
            if (treeNode == null)
            {
                return new System.Collections.Generic.List<T>();
            }
            return treeNode.BuildChildItems<T>(caller);
        }
    }
}
