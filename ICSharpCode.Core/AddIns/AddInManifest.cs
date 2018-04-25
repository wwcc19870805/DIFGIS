using System;
using System.Collections.Generic;
using System.Xml;
namespace ICSharpCode.Core
{
    public class AddInManifest
    {
        private System.Collections.Generic.List<AddInReference> dependencies = new System.Collections.Generic.List<AddInReference>();
        private System.Collections.Generic.List<AddInReference> conflicts = new System.Collections.Generic.List<AddInReference>();
        private System.Collections.Generic.Dictionary<string, System.Version> identities = new System.Collections.Generic.Dictionary<string, System.Version>();
        private System.Version primaryVersion;
        private string primaryIdentity;
        public string PrimaryIdentity
        {
            get
            {
                return this.primaryIdentity;
            }
        }
        public System.Version PrimaryVersion
        {
            get
            {
                return this.primaryVersion;
            }
        }
        public System.Collections.Generic.Dictionary<string, System.Version> Identities
        {
            get
            {
                return this.identities;
            }
        }
        public System.Collections.Generic.List<AddInReference> Dependencies
        {
            get
            {
                return this.dependencies;
            }
        }
        public System.Collections.Generic.List<AddInReference> Conflicts
        {
            get
            {
                return this.conflicts;
            }
        }
        private void AddIdentity(string name, string version, string hintPath)
        {
            if (name.Length == 0)
            {
                throw new System.ArgumentException("Identity needs a name");
            }
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                if (!char.IsLetterOrDigit(c) && c != '.' && c != '_')
                {
                    throw new System.ArgumentException("Identity name contains invalid character: '" + c + "'");
                }
            }
            System.Version value = AddInReference.ParseVersion(version, hintPath);
            if (this.primaryVersion == null)
            {
                this.primaryVersion = value;
            }
            if (this.primaryIdentity == null)
            {
                this.primaryIdentity = name;
            }
            this.identities.Add(name, value);
        }
        public void ReadManifestSection(XmlReader reader, string hintPath)
        {
            if (reader.AttributeCount != 0)
            {
                throw new System.ArgumentException("Manifest node cannot have attributes.");
            }
            if (reader.IsEmptyElement)
            {
                throw new System.ArgumentException("Manifest node cannot be empty.");
            }
            while (reader.Read())
            {
                XmlNodeType nodeType = reader.NodeType;
                if (nodeType == XmlNodeType.Element)
                {
                    string localName = reader.LocalName;
                    Properties properties = Properties.ReadFromAttributes(reader);
                    string a;
                    if ((a = localName) != null)
                    {
                        if (a == "Identity")
                        {
                            this.AddIdentity(properties["name"], properties["version"], hintPath);
                            continue;
                        }
                        if (a == "Dependency")
                        {
                            this.dependencies.Add(AddInReference.Create(properties, hintPath));
                            continue;
                        }
                        if (a == "Conflict")
                        {
                            this.conflicts.Add(AddInReference.Create(properties, hintPath));
                            continue;
                        }
                    }
                    throw new System.ArgumentException("Unknown node in Manifest section:" + localName);
                }
                if (nodeType == XmlNodeType.EndElement && reader.LocalName == "Manifest")
                {
                    return;
                }
            }
        }
    }
}
