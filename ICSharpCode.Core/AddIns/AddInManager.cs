using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
namespace ICSharpCode.Core
{
    public class AddInManager
    {
        private static string userAddInPath;
        private static string configurationFileName;
        private static string addInInstallTemp;
        public static string UserAddInPath
        {
            get
            {
                return AddInManager.userAddInPath;
            }
            set
            {
                AddInManager.userAddInPath = value;
            }
        }
        public static string AddInInstallTemp
        {
            get
            {
                return AddInManager.addInInstallTemp;
            }
            set
            {
                AddInManager.addInInstallTemp = value;
            }
        }
        public static string ConfigurationFileName
        {
            get
            {
                return AddInManager.configurationFileName;
            }
            set
            {
                AddInManager.configurationFileName = value;
            }
        }
        public static void LoadAddInConfiguration(System.Collections.Generic.List<string> addInFiles, System.Collections.Generic.List<string> disabledAddIns)
        {
            if (!System.IO.File.Exists(AddInManager.configurationFileName))
            {
                return;
            }
            using (XmlTextReader xmlTextReader = new XmlTextReader(AddInManager.configurationFileName))
            {
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlTextReader.Name == "AddIn")
                        {
                            string attribute = xmlTextReader.GetAttribute("file");
                            if (!string.IsNullOrEmpty(attribute))
                            {
                                addInFiles.Add(attribute);
                            }
                        }
                        else
                        {
                            if (xmlTextReader.Name == "Disable")
                            {
                                string attribute2 = xmlTextReader.GetAttribute("addin");
                                if (!string.IsNullOrEmpty(attribute2))
                                {
                                    disabledAddIns.Add(attribute2);
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void InstallAddIns(System.Collections.Generic.List<string> disabled)
        {
            if (!System.IO.Directory.Exists(AddInManager.addInInstallTemp))
            {
                return;
            }
            if (!System.IO.Directory.Exists(AddInManager.userAddInPath))
            {
                System.IO.Directory.CreateDirectory(AddInManager.userAddInPath);
            }
            string path = System.IO.Path.Combine(AddInManager.addInInstallTemp, "remove.txt");
            bool flag = true;
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(path))
                {
                    string text;
                    while ((text = streamReader.ReadLine()) != null)
                    {
                        text = text.Trim();
                        if (!string.IsNullOrEmpty(text))
                        {
                            string targetDir = System.IO.Path.Combine(AddInManager.userAddInPath, text);
                            if (!AddInManager.UninstallAddIn(disabled, text, targetDir))
                            {
                                list.Add(text);
                                flag = false;
                            }
                        }
                    }
                }
                if (list.Count == 0)
                {
                    System.IO.File.Delete(path);
                }
                else
                {
                    using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path))
                    {
                        list.ForEach(new System.Action<string>(streamWriter.WriteLine));
                    }
                }
            }
            string[] directories = System.IO.Directory.GetDirectories(AddInManager.addInInstallTemp);
            for (int i = 0; i < directories.Length; i++)
            {
                string text2 = directories[i];
                string fileName = System.IO.Path.GetFileName(text2);
                string text3 = System.IO.Path.Combine(AddInManager.userAddInPath, fileName);
                if (!list.Contains(fileName))
                {
                    if (AddInManager.UninstallAddIn(disabled, fileName, text3))
                    {
                        System.IO.Directory.Move(text2, text3);
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                try
                {
                    System.IO.Directory.Delete(AddInManager.addInInstallTemp, false);
                }
                catch (System.Exception)
                {
                }
            }
        }
        private static bool UninstallAddIn(System.Collections.Generic.List<string> disabled, string addInName, string targetDir)
        {
            if (System.IO.Directory.Exists(targetDir))
            {
                try
                {
                    System.IO.Directory.Delete(targetDir, true);
                }
                catch (System.Exception)
                {
                    disabled.Add(addInName);
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
