using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace ICSharpCode.Core
{
    public static class ResourceService
    {
        private class ResourceAssembly
        {
            private System.Reflection.Assembly assembly;
            private string baseResourceName;
            private bool isIcons;
            public ResourceAssembly(System.Reflection.Assembly assembly, string baseResourceName, bool isIcons)
            {
                this.assembly = assembly;
                this.baseResourceName = baseResourceName;
                this.isIcons = isIcons;
            }
            private System.Resources.ResourceManager TrySatellite(string language)
            {
                string text = System.IO.Path.GetFileNameWithoutExtension(this.assembly.Location) + ".resources.dll";
                text = System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.assembly.Location), language), text);
                if (System.IO.File.Exists(text))
                {
                    return new System.Resources.ResourceManager(this.baseResourceName, System.Reflection.Assembly.LoadFrom(text));
                }
                return null;
            }
            public void Load()
            {
                System.Resources.ResourceManager resourceManager = null;
                if (this.assembly.GetManifestResourceInfo(this.baseResourceName + "." + ResourceService.currentLanguage + ".resources") != null)
                {
                    resourceManager = new System.Resources.ResourceManager(this.baseResourceName + "." + ResourceService.currentLanguage, this.assembly);
                }
                else
                {
                    if (ResourceService.currentLanguage.IndexOf('-') > 0 && this.assembly.GetManifestResourceInfo(this.baseResourceName + "." + ResourceService.currentLanguage.Split(new char[]
					{
						'-'
					})[0] + ".resources") != null)
                    {
                        resourceManager = new System.Resources.ResourceManager(this.baseResourceName + "." + ResourceService.currentLanguage.Split(new char[]
						{
							'-'
						})[0], this.assembly);
                    }
                    else
                    {
                        resourceManager = this.TrySatellite(ResourceService.currentLanguage);
                        if (resourceManager == null && ResourceService.currentLanguage.IndexOf('-') > 0)
                        {
                            resourceManager = this.TrySatellite(ResourceService.currentLanguage.Split(new char[]
							{
								'-'
							})[0]);
                        }
                    }
                }
                if (resourceManager != null)
                {
                    if (this.isIcons)
                    {
                        ResourceService.localIconsResMgrs.Add(resourceManager);
                        return;
                    }
                    ResourceService.localStringsResMgrs.Add(resourceManager);
                }
            }
        }
        private const string uiLanguageProperty = "language";
        private const string imageResources = "BitmapResources";
        private const string stringResources = "StringResources";
        private static System.Collections.Hashtable localStrings = null;
        private static System.Collections.Hashtable localIcons = null;
        private static System.Collections.Generic.List<System.Resources.ResourceManager> localStringsResMgrs = new System.Collections.Generic.List<System.Resources.ResourceManager>();
        private static System.Collections.Generic.List<System.Resources.ResourceManager> localIconsResMgrs = new System.Collections.Generic.List<System.Resources.ResourceManager>();
        private static System.Collections.Generic.List<System.Resources.ResourceManager> strings = new System.Collections.Generic.List<System.Resources.ResourceManager>();
        private static System.Collections.Generic.List<System.Resources.ResourceManager> icons = new System.Collections.Generic.List<System.Resources.ResourceManager>();
        private static string resourceDirectory;
        private static string currentLanguage;
        private static System.Collections.Generic.List<ResourceService.ResourceAssembly> resourceAssemblies = new System.Collections.Generic.List<ResourceService.ResourceAssembly>();
        public static string ResourceDirectory
        {
            get
            {
                return ResourceService.resourceDirectory;
            }
            set
            {
                ResourceService.resourceDirectory = value;
            }
        }
        public static string Language
        {
            get
            {
                return PropertyService.Get<string>("language", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
            }
            set
            {
                PropertyService.Set<string>("language", value);
            }
        }
        public static void InitializeService(string resourceDirectory)
        {
            if (ResourceService.resourceDirectory != null)
            {
                throw new System.InvalidOperationException("Service is already initialized.");
            }
            if (resourceDirectory == null)
            {
                throw new System.ArgumentNullException("resourceDirectory");
            }
            if (!System.IO.Directory.Exists(resourceDirectory))
            {
                System.IO.Directory.CreateDirectory(resourceDirectory);
            }
            ResourceService.resourceDirectory = resourceDirectory;
            ResourceService.LoadLanguageResources(ResourceService.Language);
        }
        private static void LoadLanguageResources(string language)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            }
            catch (System.Exception)
            {
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language.Split(new char[]
					{
						'-'
					})[0]);
                }
                catch (System.Exception)
                {
                }
            }
            ResourceService.localStrings = ResourceService.Load("StringResources", language);
            if (ResourceService.localStrings == null && language.IndexOf('-') > 0)
            {
                ResourceService.localStrings = ResourceService.Load("StringResources", language.Split(new char[]
				{
					'-'
				})[0]);
            }
            ResourceService.localIcons = ResourceService.Load("BitmapResources", language);
            if (ResourceService.localIcons == null && language.IndexOf('-') > 0)
            {
                ResourceService.localIcons = ResourceService.Load("BitmapResources", language.Split(new char[]
				{
					'-'
				})[0]);
            }
            ResourceService.localStringsResMgrs.Clear();
            ResourceService.localIconsResMgrs.Clear();
            ResourceService.currentLanguage = language;
            foreach (ResourceService.ResourceAssembly current in ResourceService.resourceAssemblies)
            {
                current.Load();
            }
        }
        private static System.Collections.Hashtable Load(string[] fielNames)
        {
            System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
            for (int i = 0; i < fielNames.Length; i++)
            {
                string text = fielNames[i];
                if (System.IO.File.Exists(text))
                {
                    System.Resources.ResourceReader resourceReader = new System.Resources.ResourceReader(text);
                    foreach (System.Collections.DictionaryEntry dictionaryEntry in resourceReader)
                    {
                        if (!hashtable.ContainsKey(dictionaryEntry.Key))
                        {
                            hashtable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
                        }
                    }
                    resourceReader.Close();
                }
            }
            return hashtable;
        }
        private static System.Collections.Hashtable Load(string directoryName, string lang)
        {
            string searchPattern = "*." + lang + ".resources";
            string path = System.IO.Path.Combine(ResourceService.resourceDirectory, directoryName);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string[] files = System.IO.Directory.GetFiles(System.IO.Path.Combine(ResourceService.resourceDirectory, directoryName), searchPattern, System.IO.SearchOption.AllDirectories);
            return ResourceService.Load(files);
        }
        public static object GetImageResource(string name)
        {
            object obj = null;
            if (string.IsNullOrEmpty(name))
            {
                return obj;
            }
            if (ResourceService.localIcons != null && ResourceService.localIcons[name] != null)
            {
                obj = ResourceService.localIcons[name];
            }
            else
            {
                foreach (System.Resources.ResourceManager current in ResourceService.localIconsResMgrs)
                {
                    try
                    {
                        obj = current.GetObject(name);
                    }
                    catch (System.Exception)
                    {
                        obj = null;
                    }
                    if (obj != null)
                    {
                        break;
                    }
                }
            }
            if (obj == null)
            {
                foreach (System.Resources.ResourceManager current2 in ResourceService.icons)
                {
                    try
                    {
                        obj = current2.GetObject(name);
                    }
                    catch (System.Exception)
                    {
                        obj = null;
                    }
                    if (obj != null)
                    {
                        break;
                    }
                }
            }
            return obj;
        }
        public static string GetString(string name)
        {
            if (ResourceService.localStrings != null && ResourceService.localStrings[name] != null)
            {
                return ResourceService.localStrings[name].ToString();
            }
            string text = null;
            foreach (System.Resources.ResourceManager current in ResourceService.localStringsResMgrs)
            {
                try
                {
                    text = current.GetString(name);
                }
                catch (System.Exception)
                {
                }
                if (text != null)
                {
                    break;
                }
            }
            if (text == null)
            {
                foreach (System.Resources.ResourceManager current2 in ResourceService.strings)
                {
                    try
                    {
                        text = current2.GetString(name);
                    }
                    catch (System.Exception)
                    {
                    }
                    if (text != null)
                    {
                        break;
                    }
                }
            }
            if (text == null)
            {
                throw new ResourceNotFoundException("string >" + name + "<");
            }
            return text;
        }
        public static void RegisterImages(string baseResourceName, System.Reflection.Assembly assembly)
        {
            ResourceService.RegisterNeutralImages(new System.Resources.ResourceManager(baseResourceName, assembly));
            ResourceService.ResourceAssembly resourceAssembly = new ResourceService.ResourceAssembly(assembly, baseResourceName, true);
            ResourceService.resourceAssemblies.Add(resourceAssembly);
            resourceAssembly.Load();
        }
        public static void RegisterNeutralImages(System.Resources.ResourceManager imageManager)
        {
            ResourceService.icons.Add(imageManager);
        }
        public static void RegisterStrings(string baseResourceName, System.Reflection.Assembly assembly)
        {
            ResourceService.RegisterNeutralStrings(new System.Resources.ResourceManager(baseResourceName, assembly));
            ResourceService.ResourceAssembly resourceAssembly = new ResourceService.ResourceAssembly(assembly, baseResourceName, false);
            ResourceService.resourceAssemblies.Add(resourceAssembly);
            resourceAssembly.Load();
        }
        public static void RegisterNeutralStrings(System.Resources.ResourceManager stringManager)
        {
            ResourceService.strings.Add(stringManager);
        }
    }
}
