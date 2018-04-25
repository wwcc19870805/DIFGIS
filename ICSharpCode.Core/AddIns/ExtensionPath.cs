using System;
using System.Collections.Generic;
using System.Xml;
namespace ICSharpCode.Core
{
    public class ExtensionPath
    {
        private string name;
        private AddIn addIn;
        private System.Collections.Generic.List<Codon> codons = new System.Collections.Generic.List<Codon>();
        public AddIn AddIn
        {
            get
            {
                return this.addIn;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public System.Collections.Generic.List<Codon> Codons
        {
            get
            {
                return this.codons;
            }
        }
        public ExtensionPath(string name, AddIn addIn)
        {
            this.addIn = addIn;
            this.name = name;
        }
        public static void SetUp(ExtensionPath extensionPath, XmlReader reader, string endElement)
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
                            if (reader.LocalName == endElement)
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    string localName = reader.LocalName;
                    if (localName == "Condition")
                    {
                        stack.Push(Condition.Read(reader));
                    }
                    else
                    {
                        if (localName == "ComplexCondition")
                        {
                            stack.Push(Condition.ReadComplexCondition(reader));
                        }
                        else
                        {
                            Codon codon = new Codon(extensionPath.AddIn, localName, Properties.ReadFromAttributes(reader), stack.ToArray());
                            extensionPath.codons.Add(codon);
                            if (!reader.IsEmptyElement)
                            {
                                ExtensionPath extensionPath2 = extensionPath.AddIn.GetExtensionPath(extensionPath.Name + "/" + codon.Id);
                                ExtensionPath.SetUp(extensionPath2, reader, localName);
                            }
                        }
                    }
                }
            }
        }
    }
}
