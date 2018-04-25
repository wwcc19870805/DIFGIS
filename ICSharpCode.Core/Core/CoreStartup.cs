using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.Core;
namespace ICSharpCode.Core
{
    public sealed class CoreStartup
    {
        private System.Collections.Generic.List<string> addInFiles = new System.Collections.Generic.List<string>();
        private System.Collections.Generic.List<string> disabledAddIns = new System.Collections.Generic.List<string>();
        private bool externalAddInsConfigured;
        private string propertiesName;
        private string configDirectory;
        private string dataDirectory;
        private string applicationName;
        public string PropertiesName
        {
            get
            {
                return this.propertiesName;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new System.ArgumentNullException();
                }
                this.propertiesName = value;
            }
        }
        public string ConfigDirectory
        {
            get
            {
                return this.configDirectory;
            }
            set
            {
                this.configDirectory = value;
            }
        }
        public string DataDirectory
        {
            get
            {
                return this.dataDirectory;
            }
            set
            {
                this.dataDirectory = value;
            }
        }
        public CoreStartup(string applicationName)
        {
            if (applicationName == null)
            {
                throw new System.ArgumentNullException();
            }
            this.applicationName = applicationName;
            this.propertiesName = applicationName + "Properties";
        }
        public void AddAddInsFromDirectory(string addInDir)
        {
            if (string.IsNullOrEmpty(addInDir))
            {
                throw new System.ArgumentNullException();
            }
            this.addInFiles.AddRange(FileUtility.SearchDirectory(addInDir, "*.addin"));
        }
        public void AddAddInFile(string addInFile)
        {
            if (addInFile == null)
            {
                throw new System.ArgumentNullException("addInFile");
            }
            this.addInFiles.Add(addInFile);
        }
        public void ConfigureExternalAddIns(string addInConfigurationFile)
        {
            this.externalAddInsConfigured = true;
            AddInManager.ConfigurationFileName = addInConfigurationFile;
            AddInManager.LoadAddInConfiguration(this.addInFiles, this.disabledAddIns);
        }
        public void ConfigureUserAddIns(string addInInstallTemp, string userAddInPath)
        {
            if (!this.externalAddInsConfigured)
            {
                throw new System.InvalidOperationException("ConfigureExternalAddIns must be called before ConfigureUserAddIns");
            }
            AddInManager.AddInInstallTemp = addInInstallTemp;
            AddInManager.UserAddInPath = userAddInPath;
            if (System.IO.Directory.Exists(addInInstallTemp))
            {
                AddInManager.InstallAddIns(this.disabledAddIns);
            }
            if (System.IO.Directory.Exists(userAddInPath))
            {
                this.AddAddInsFromDirectory(userAddInPath);
            }
        }
        public void RunInitialization()
        {
            AddInTree.Load(this.addInFiles, this.disabledAddIns);
            foreach (ICommand current in AddInTree.BuildItems<ICommand>("/Workspace/Autostart", null, false))
            {
                try
                {
                    current.Run(null, null);
                }
                catch (System.Exception ex)
                {
                    LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                }
            }
        }
        //public void RunAutostartCommand()
        //{
        //    int a_ = 6;
        //    using (System.Collections.Generic.List<ICommand>.Enumerator enumerator = AddInTree.BuildItems<ICommand>(ExtensionAddInTreeNode.b("쇭꟯鷱蛳鷵髷鿹鋻鷽棿ⴁ䔃猅簇攉缋稍焏怑怓", a_), null, false).GetEnumerator())
        //    {
        //        int num = 0;
        //        while (true)
        //        {
        //            switch (num)
        //            {
        //                case 1:
        //                    goto IL_96;
        //                case 2:
        //                    goto IL_8E;
        //                case 3:
        //                    {
        //                        if (true)
        //                        {
        //                        }
        //                        if (!enumerator.MoveNext())
        //                        {
        //                            num = 2;
        //                            continue;
        //                        }
        //                        ICommand current = enumerator.Current;
        //                        num = 4;
        //                        continue;
        //                    }
        //                case 4:
        //                    try
        //                    {
        //                        ICommand current;
        //                        current.Run(null, null);
        //                        break;
        //                    }
        //                    catch (System.Exception)
        //                    {
        //                        break;
        //                    }
        //                    goto IL_8E;
        //            }
        //        IL_5E:
        //            num = 3;
        //            continue;
        //            goto IL_5E;
        //        IL_8E:
        //            num = 1;
        //        }
        //    IL_96: ;
        //    }
        //}
        public void StartCoreServices()
        {
            if (this.configDirectory == null)
            {
                this.configDirectory = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), this.applicationName);
            }
            PropertyService.InitializeService(this.configDirectory, this.dataDirectory ?? System.IO.Path.Combine(FileUtility.ApplicationRootPath, "..\\"), this.propertiesName);
            PropertyService.Load();
            ResourceService.InitializeService(FileUtility.Combine(new string[]
			{
				PropertyService.DataDirectory, 
				"Resource"
			}));
            StringParser.Properties["AppName"] = this.applicationName;
        }
        public void EndCoreServices()
        {
            PropertyService.Save();
        }
    }
}
