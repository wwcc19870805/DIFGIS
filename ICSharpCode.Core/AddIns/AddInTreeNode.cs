using System;
using System.Collections;
using System.Collections.Generic;
namespace ICSharpCode.Core
{
    public sealed class AddInTreeNode
    {
        private sealed class TopologicalSort
        {
            private System.Collections.Generic.List<Codon> codons;
            private bool[] visited;
            private System.Collections.Generic.List<Codon> sortedCodons;
            private System.Collections.Generic.Dictionary<string, int> indexOfName;
            public TopologicalSort(System.Collections.Generic.List<Codon> codons)
            {
                this.codons = codons;
                this.visited = new bool[codons.Count];
                this.sortedCodons = new System.Collections.Generic.List<Codon>(codons.Count);
                this.indexOfName = new System.Collections.Generic.Dictionary<string, int>(codons.Count);
                for (int i = 0; i < codons.Count; i++)
                {
                    this.visited[i] = false;
                    this.indexOfName[codons[i].Id] = i;
                }
            }
            private void InsertEdges()
            {
                for (int i = 0; i < this.codons.Count; i++)
                {
                    string insertBefore = this.codons[i].InsertBefore;
                    if (insertBefore != null && insertBefore != "" && this.indexOfName.ContainsKey(insertBefore))
                    {
                        string insertAfter = this.codons[this.indexOfName[insertBefore]].InsertAfter;
                        if (insertAfter == null || insertAfter == "")
                        {
                            this.codons[this.indexOfName[insertBefore]].InsertAfter = this.codons[i].Id;
                        }
                        else
                        {
                            this.codons[this.indexOfName[insertBefore]].InsertAfter = insertAfter + ',' + this.codons[i].Id;
                        }
                    }
                }
            }
            public System.Collections.Generic.List<Codon> Execute()
            {
                this.InsertEdges();
                for (int i = 0; i < this.codons.Count; i++)
                {
                    this.Visit(i);
                }
                return this.sortedCodons;
            }
            private void Visit(int codonIndex)
            {
                if (this.visited[codonIndex])
                {
                    return;
                }
                string[] array = this.codons[codonIndex].InsertAfter.Split(new char[]
				{
					','
				});
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string text = array2[i];
                    if (text != null && text.Length != 0 && this.indexOfName.ContainsKey(text))
                    {
                        this.Visit(this.indexOfName[text]);
                    }
                }
                this.sortedCodons.Add(this.codons[codonIndex]);
                this.visited[codonIndex] = true;
            }
        }
        private System.Collections.Generic.Dictionary<string, AddInTreeNode> childNodes = new System.Collections.Generic.Dictionary<string, AddInTreeNode>();
        private System.Collections.Generic.List<Codon> codons = new System.Collections.Generic.List<Codon>();
        private bool isSorted;
        public System.Collections.Generic.Dictionary<string, AddInTreeNode> ChildNodes
        {
            get
            {
                return this.childNodes;
            }
        }
        public System.Collections.Generic.List<Codon> Codons
        {
            get
            {
                return this.codons;
            }
        }
        public System.Collections.Generic.List<T> BuildChildItems<T>(object caller)
        {
            System.Collections.Generic.List<T> list = new System.Collections.Generic.List<T>(this.codons.Count);
            if (!this.isSorted)
            {
                this.codons = new AddInTreeNode.TopologicalSort(this.codons).Execute();
                this.isSorted = true;
            }
            foreach (Codon current in this.codons)
            {
                System.Collections.ArrayList subItems = null;
                if (this.childNodes.ContainsKey(current.Id))
                {
                    subItems = this.childNodes[current.Id].BuildChildItems(caller);
                }
                object obj = current.BuildItem(caller, subItems);
                if (obj != null)
                {
                    IBuildItemsModifier buildItemsModifier = obj as IBuildItemsModifier;
                    if (buildItemsModifier != null)
                    {
                        buildItemsModifier.Apply(list);
                    }
                    else
                    {
                        if (!(obj is T))
                        {
                            throw new System.InvalidCastException(string.Concat(new string[]
							{
								"The AddInTreeNode <", 
								current.Name, 
								" id='", 
								current.Id, 
								"' returned an instance of ", 
								obj.GetType().FullName, 
								" but the type ", 
								typeof(T).FullName, 
								" is expected."
							}));
                        }
                        list.Add((T)obj);
                    }
                }
            }
            return list;
        }
        public System.Collections.ArrayList BuildChildItems(object caller)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList(this.codons.Count);
            if (!this.isSorted)
            {
                this.codons = new AddInTreeNode.TopologicalSort(this.codons).Execute();
                this.isSorted = true;
            }
            foreach (Codon current in this.codons)
            {
                System.Collections.ArrayList subItems = null;
                if (this.childNodes.ContainsKey(current.Id))
                {
                    subItems = this.childNodes[current.Id].BuildChildItems(caller);
                }
                object obj = current.BuildItem(caller, subItems);
                if (obj != null)
                {
                    IBuildItemsModifier buildItemsModifier = obj as IBuildItemsModifier;
                    if (buildItemsModifier != null)
                    {
                        buildItemsModifier.Apply(arrayList);
                    }
                    else
                    {
                        arrayList.Add(obj);
                    }
                }
            }
            return arrayList;
        }
        public object BuildChildItem(string childItemID, object caller, System.Collections.ArrayList subItems)
        {
            foreach (Codon current in this.codons)
            {
                if (current.Id == childItemID)
                {
                    return current.BuildItem(caller, subItems);
                }
            }
            return null;
        }
    }
}
