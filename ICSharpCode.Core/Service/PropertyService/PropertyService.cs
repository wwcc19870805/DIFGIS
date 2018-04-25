using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
namespace ICSharpCode.Core
{
    public class PropertyService
    {
        private static string propertyFileName;
        private static string propertyXmlRootNodeName;
        private static string configDirectory;
        private static string dataDirectory;
        private static Properties properties;
        public static event PropertyChangedEventHandler PropertyChanged;
        public static Properties Properties
        {
            get
            {
                return PropertyService.properties;
            }
        }
        public static bool Initialized
        {
            get
            {
                return PropertyService.properties != null;
            }
        }
        public static string ConfigDirectory
        {
            get
            {
                return PropertyService.configDirectory;
            }
        }
        public static string DataDirectory
        {
            get
            {
                return PropertyService.dataDirectory;
            }
        }
        public static void InitializeService(string configDirectory, string dataDirectory, string propertiesName)
        {
            if (PropertyService.properties != null)
            {
                throw new System.InvalidOperationException("Service is already initialized.");
            }
            if (configDirectory == null || dataDirectory == null || propertiesName == null)
            {
                throw new System.ArgumentNullException();
            }
            PropertyService.properties = new Properties();
            PropertyService.configDirectory = configDirectory;
            PropertyService.dataDirectory = dataDirectory;
            PropertyService.propertyXmlRootNodeName = propertiesName;
            PropertyService.propertyFileName = propertiesName + ".xml";
            PropertyService.properties.PropertyChanged += new PropertyChangedEventHandler(PropertyService.PropertiesPropertyChanged);
        }
        public static string Get(string property)
        {
            return PropertyService.properties[property];
        }
        public static T Get<T>(string property, T defaultValue)
        {
            return PropertyService.properties.Get<T>(property, defaultValue);
        }
        public static void Set<T>(string property, T value)
        {
            PropertyService.properties.Set<T>(property, value);
        }
        public static void Load()
        {
            if (PropertyService.properties == null)
            {
                throw new System.InvalidOperationException("Service is not initialized.");
            }
            if (!System.IO.Directory.Exists(PropertyService.configDirectory))
            {
                System.IO.Directory.CreateDirectory(PropertyService.configDirectory);
            }
            string fileName = System.IO.Path.Combine(FileUtility.LocalUserAppDataPath, PropertyService.propertyFileName);
            PropertyService.LoadPropertiesFromStream(fileName);
            fileName = System.IO.Path.Combine(FileUtility.ApplicationRootPath, PropertyService.propertyFileName);
            PropertyService.LoadPropertiesFromStream(fileName);
        }
        public static bool LoadPropertiesFromStream(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return false;
            }
            try
            {
                using (PropertyService.LockPropertyFile())
                {
                    using (XmlTextReader xmlTextReader = new XmlTextReader(fileName))
                    {
                        while (xmlTextReader.Read())
                        {
                            if (xmlTextReader.IsStartElement() && xmlTextReader.LocalName == PropertyService.propertyXmlRootNodeName)
                            {
                                PropertyService.properties.ReadProperties(xmlTextReader, PropertyService.propertyXmlRootNodeName);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (XmlException)
            {
            }
            return false;
        }
        public static void Save()
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, System.Text.Encoding.UTF8);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.WriteStartElement(PropertyService.propertyXmlRootNodeName);
                PropertyService.properties.WriteProperties(xmlTextWriter);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.Flush();
                memoryStream.Position = 0L;
                string path = System.IO.Path.Combine(FileUtility.LocalUserAppDataPath, PropertyService.propertyFileName);
                using (PropertyService.LockPropertyFile())
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
                    {
                        memoryStream.WriteTo(fileStream);
                    }
                }
            }
        }
        public static System.IDisposable LockPropertyFile()
        {
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, "PropertyServiceSave-30F32619-F92D-4BC0-BF49-AA18BF4AC313");
            mutex.WaitOne();
            return new CallbackOnDispose(delegate
            {
                mutex.ReleaseMutex();
                mutex.Close();
            }
            );
        }
        public static string[] GetConfigInfo(string strConfig)
        {
            string[] result = null;
            try
            {
                if (!string.IsNullOrEmpty(strConfig))
                {
                    result = strConfig.Split(new char[]
					{
						'|'
					});
                }
            }
            catch (System.Exception)
            {
            }
            return result;
        }
        public static System.Collections.Generic.Dictionary<string, string[]> GetConfigInfo2(string strConfig)
        {
            System.Collections.Generic.Dictionary<string, string[]> dictionary = new System.Collections.Generic.Dictionary<string, string[]>();
            try
            {
                if (!string.IsNullOrEmpty(strConfig))
                {
                    string[] array = strConfig.Split(new string[]
					{
						"@dyia@"
					}, System.StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text = array[i];
                        string[] array2 = text.Split(new char[]
						{
							'|'
						});
                        if (array2 != null && array2.Length == 4)
                        {
                            dictionary.Add(array2[0], array2);
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return dictionary;
        }
        private static void PropertiesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyService.PropertyChanged != null)
            {
                PropertyService.PropertyChanged(null, e);
            }
        }
    }
}
