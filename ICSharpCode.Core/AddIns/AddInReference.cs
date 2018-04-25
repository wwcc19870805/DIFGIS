using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace ICSharpCode.Core
{
    public class AddInReference : System.ICloneable
    {
        private string name;
        private System.Version minimumVersion;
        private System.Version maximumVersion;
        private bool requirePreload;
        private static System.Version entryVersion;
        public System.Version MinimumVersion
        {
            get
            {
                return this.minimumVersion;
            }
        }
        public System.Version MaximumVersion
        {
            get
            {
                return this.maximumVersion;
            }
        }
        public bool RequirePreload
        {
            get
            {
                return this.requirePreload;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new System.ArgumentNullException();
                }
                this.name = value;
            }
        }
        public bool Check(System.Collections.Generic.Dictionary<string, System.Version> addIns, out System.Version versionFound)
        {
            return addIns.TryGetValue(this.name, out versionFound) && this.CompareVersion(versionFound, this.minimumVersion) >= 0 && this.CompareVersion(versionFound, this.maximumVersion) <= 0;
        }
        private int CompareVersion(System.Version a, System.Version b)
        {
            if (a.Major != b.Major)
            {
                if (a.Major <= b.Major)
                {
                    return -1;
                }
                return 1;
            }
            else
            {
                if (a.Minor != b.Minor)
                {
                    if (a.Minor <= b.Minor)
                    {
                        return -1;
                    }
                    return 1;
                }
                else
                {
                    if (a.Build < 0 || b.Build < 0)
                    {
                        return 0;
                    }
                    if (a.Build != b.Build)
                    {
                        if (a.Build <= b.Build)
                        {
                            return -1;
                        }
                        return 1;
                    }
                    else
                    {
                        if (a.Revision < 0 || b.Revision < 0)
                        {
                            return 0;
                        }
                        if (a.Revision == b.Revision)
                        {
                            return 0;
                        }
                        if (a.Revision <= b.Revision)
                        {
                            return -1;
                        }
                        return 1;
                    }
                }
            }
        }
        public AddInReference(string name)
            : this(name, new System.Version(0, 0, 0, 0), new System.Version(2147483647, 2147483647))
        {
        }
        public AddInReference(string name, System.Version specificVersion)
            : this(name, specificVersion, specificVersion)
        {
        }
        public AddInReference(string name, System.Version minimumVersion, System.Version maximumVersion)
        {
            this.Name = name;
            if (minimumVersion == null)
            {
                throw new System.ArgumentNullException("minimumVersion");
            }
            if (maximumVersion == null)
            {
                throw new System.ArgumentNullException("maximumVersion");
            }
            this.minimumVersion = minimumVersion;
            this.maximumVersion = maximumVersion;
        }
        public static AddInReference Create(Properties properties, string hintPath)
        {
            AddInReference addInReference = new AddInReference(properties["addin"]);
            string text = properties["version"];
            if (text != null && text.Length > 0)
            {
                int num = text.IndexOf('-');
                if (num > 0)
                {
                    addInReference.minimumVersion = AddInReference.ParseVersion(text.Substring(0, num), hintPath);
                    addInReference.maximumVersion = AddInReference.ParseVersion(text.Substring(num + 1), hintPath);
                }
                else
                {
                    addInReference.maximumVersion = (addInReference.minimumVersion = AddInReference.ParseVersion(text, hintPath));
                }
                if (addInReference.Name == "SharpDevelop" && (addInReference.maximumVersion == new System.Version("3.0") || addInReference.maximumVersion == new System.Version("3.1")))
                {
                    addInReference.maximumVersion = new System.Version("3.2");
                }
            }
            addInReference.requirePreload = string.Equals(properties["requirePreload"], "true", System.StringComparison.OrdinalIgnoreCase);
            return addInReference;
        }
        internal static System.Version ParseVersion(string version, string hintPath)
        {
            if (version == null || version.Length == 0)
            {
                return new System.Version(0, 0, 0, 0);
            }
            if (!version.StartsWith("@"))
            {
                return new System.Version(version);
            }
            if (version == "@SharpDevelopCoreVersion")
            {
                if (AddInReference.entryVersion == null)
                {
                    AddInReference.entryVersion = new System.Version("3.2.1.6466");
                }
                return AddInReference.entryVersion;
            }
            if (hintPath != null)
            {
                string fileName = System.IO.Path.Combine(hintPath, version.Substring(1));
                try
                {
                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fileName);
                    return new System.Version(versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart, versionInfo.FilePrivatePart);
                }
                catch (System.IO.FileNotFoundException)
                {
                }
            }
            return new System.Version(0, 0, 0, 0);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is AddInReference))
            {
                return false;
            }
            AddInReference addInReference = (AddInReference)obj;
            return this.name == addInReference.name && this.minimumVersion == addInReference.minimumVersion && this.maximumVersion == addInReference.maximumVersion;
        }
        public override int GetHashCode()
        {
            return this.name.GetHashCode() ^ this.minimumVersion.GetHashCode() ^ this.maximumVersion.GetHashCode();
        }
        public override string ToString()
        {
            if (this.minimumVersion.ToString() == "0.0.0.0")
            {
                if (this.maximumVersion.Major == 2147483647)
                {
                    return this.name;
                }
                return this.name + ", version <" + this.maximumVersion.ToString();
            }
            else
            {
                if (this.maximumVersion.Major == 2147483647)
                {
                    return this.name + ", version >" + this.minimumVersion.ToString();
                }
                if (this.minimumVersion == this.maximumVersion)
                {
                    return this.name + ", version " + this.minimumVersion.ToString();
                }
                return string.Concat(new string[]
				{
					this.name, 
					", version ", 
					this.minimumVersion.ToString(), 
					"-", 
					this.maximumVersion.ToString()
				});
            }
        }
        public AddInReference Clone()
        {
            return new AddInReference(this.name, this.minimumVersion, this.maximumVersion);
        }
        object System.ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
