using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
namespace ICSharpCode.Core
{
	public sealed class AddIn
	{
		private Properties properties = new Properties();
		private System.Collections.Generic.List<Runtime> runtimes = new System.Collections.Generic.List<Runtime>();
		private System.Collections.Generic.List<string> bitmapResources = new System.Collections.Generic.List<string>();
		private System.Collections.Generic.List<string> stringResources = new System.Collections.Generic.List<string>();
		internal string addInFileName;
		private AddInManifest manifest = new AddInManifest();
		private System.Collections.Generic.Dictionary<string, ExtensionPath> paths = new System.Collections.Generic.Dictionary<string, ExtensionPath>();
		private AddInAction action = AddInAction.Disable;
		private bool enabled;
		private static bool hasShownErrorMessage;
		private bool dependenciesLoaded;
		private string customErrorMessage;
		public string CustomErrorMessage
		{
			get
			{
				return this.customErrorMessage;
			}
			internal set
			{
				if (value != null)
				{
					this.Enabled = false;
					this.Action = AddInAction.CustomError;
				}
				this.customErrorMessage = value;
			}
		}
		public AddInAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}
		public System.Collections.Generic.List<Runtime> Runtimes
		{
			get
			{
				return this.runtimes;
			}
		}
		public System.Version Version
		{
			get
			{
				return this.manifest.PrimaryVersion;
			}
		}
		public string FileName
		{
			get
			{
				return this.addInFileName;
			}
		}
		public string Name
		{
			get
			{
				return this.properties["name"];
			}
		}
		public AddInManifest Manifest
		{
			get
			{
				return this.manifest;
			}
		}
		public System.Collections.Generic.Dictionary<string, ExtensionPath> Paths
		{
			get
			{
				return this.paths;
			}
		}
		public Properties Properties
		{
			get
			{
				return this.properties;
			}
		}
		public System.Collections.Generic.List<string> BitmapResources
		{
			get
			{
				return this.bitmapResources;
			}
			set
			{
				this.bitmapResources = value;
			}
		}
		public System.Collections.Generic.List<string> StringResources
		{
			get
			{
				return this.stringResources;
			}
			set
			{
				this.stringResources = value;
			}
		}
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				this.Action = (value ? AddInAction.Enable : AddInAction.Disable);
			}
		}
		public object CreateObject(string className)
		{
			this.LoadDependencies();
			foreach (Runtime current in this.runtimes)
			{
				object obj = current.CreateInstance(className);
				if (obj != null)
				{
					object result = obj;
					return result;
				}
			}
			if (AddIn.hasShownErrorMessage)
			{
				AddIn.hasShownErrorMessage = false;
				goto IL_59;
			}
			AddIn.hasShownErrorMessage = true;
			IL_59:
			return null;
		}
		public void LoadRuntimeAssemblies()
		{
			this.LoadDependencies();
			foreach (Runtime current in this.runtimes)
			{
				current.Load();
			}
		}
		private void LoadDependencies()
		{
			if (!this.dependenciesLoaded)
			{
				this.dependenciesLoaded = true;
				foreach (AddInReference current in this.manifest.Dependencies)
				{
					if (current.RequirePreload)
					{
						bool flag = false;
						foreach (AddIn current2 in AddInTree.AddIns)
						{
							if (current2.Manifest.Identities.ContainsKey(current.Name))
							{
								flag = true;
								current2.LoadRuntimeAssemblies();
							}
						}
						if (!flag)
						{
							throw new System.NotSupportedException("Cannot load run-time dependency for " + current.ToString());
						}
					}
				}
			}
		}
		public override string ToString()
		{
			return "[AddIn: " + this.Name + "]";
		}
		internal AddIn()
		{
		}
        private static void SetupAddIn(XmlReader reader, AddIn addIn, string hintPath)
        {
            while (reader.Read())
            {
                if ((reader.NodeType != XmlNodeType.Element) || !reader.IsStartElement())
                {
                    continue;
                }
                switch (reader.LocalName)
                {
                    case "StringResources":
                    case "BitmapResources":
                        if (reader.AttributeCount != 1)
                        {
                            throw new NotSupportedException("BitmapResources requires ONE attribute.");
                        }
                        break;

                    case "Runtime":
                        {
                            if (!reader.IsEmptyElement)
                            {
                                Runtime.ReadSection(reader, addIn, hintPath);
                            }
                            continue;
                        }
                    case "Include":
                        if (reader.AttributeCount != 1)
                        {
                            throw new NotSupportedException("Include requires ONE attribute.");
                        }
                        goto Label_0146;

                    case "Path":
                        goto Label_01A3;

                    case "Manifest":
                        {
                            addIn.Manifest.ReadManifestSection(reader, hintPath);
                            continue;
                        }
                    default:
                        throw new NotSupportedException("Unknown root path node:" + reader.LocalName);
                }
                string item = StringParser.Parse(reader.GetAttribute("file"));
                if (reader.LocalName == "BitmapResources")
                {
                    addIn.BitmapResources.Add(item);
                }
                else
                {
                    addIn.StringResources.Add(item);
                }
                continue;
            Label_0146:
                if (!reader.IsEmptyElement)
                {
                    throw new NotSupportedException("Include nodes must be empty!");
                }
                if (hintPath == null)
                {
                    throw new NotSupportedException("Cannot use include nodes when hintPath was not specified (e.g. when AddInManager reads a .addin file)!");
                }
                string inputUri = Path.Combine(hintPath, reader.GetAttribute(0));
                XmlReaderSettings settings = new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment
                };
                using (XmlReader reader2 = XmlReader.Create(inputUri, settings))
                {
                    SetupAddIn(reader2, addIn, Path.GetDirectoryName(inputUri));
                    continue;
                }
            Label_01A3:
                if (reader.AttributeCount != 1)
                {
                    throw new NotSupportedException("Import node requires ONE attribute.");
                }
                string attribute = reader.GetAttribute(0);
                ExtensionPath extensionPath = addIn.GetExtensionPath(attribute);
                if (!reader.IsEmptyElement)
                {
                    ExtensionPath.SetUp(extensionPath, reader, "Path");
                }
            }
        }

        public ExtensionPath GetExtensionPath(string pathName)
		{
			if (!this.paths.ContainsKey(pathName))
			{
				return this.paths[pathName] = new ExtensionPath(pathName, this);
			}
			return this.paths[pathName];
		}
		public static AddIn Load(System.IO.TextReader textReader)
		{
			return AddIn.Load(textReader, null);
		}
		public static AddIn Load(System.IO.TextReader textReader, string hintPath)
		{
			AddIn addIn = new AddIn();
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.IsStartElement())
					{
						string localName;
						if ((localName = xmlTextReader.LocalName) == null || !(localName == "AddIn"))
						{
							throw new System.NotSupportedException("Unknown add-in file.");
						}
						addIn.properties = Properties.ReadFromAttributes(xmlTextReader);
						AddIn.SetupAddIn(xmlTextReader, addIn, hintPath);
					}
				}
			}
			return addIn;
		}
		public static AddIn Load(string fileName)
		{
			AddIn result;
			try
			{
				using (System.IO.TextReader textReader = System.IO.File.OpenText(fileName))
				{
					AddIn addIn = AddIn.Load(textReader, System.IO.Path.GetDirectoryName(fileName));
					addIn.addInFileName = fileName;
					result = addIn;
				}
			}
			catch (System.Exception innerException)
			{
				throw new System.NotSupportedException("Can't load " + fileName, innerException);
			}
			return result;
		}
	}
}
