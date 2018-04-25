using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
namespace ICSharpCode.Core
{
    public static class StringParser
    {
        private static readonly System.Collections.Generic.Dictionary<string, string> properties;
        private static readonly System.Collections.Generic.Dictionary<string, IStringTagProvider> stringTagProviders;
        private static readonly System.Collections.Generic.Dictionary<string, object> propertyObjects;
        public static System.Collections.Generic.Dictionary<string, string> Properties
        {
            get
            {
                return StringParser.properties;
            }
        }
        public static System.Collections.Generic.Dictionary<string, object> PropertyObjects
        {
            get
            {
                return StringParser.propertyObjects;
            }
        }
        static StringParser()
        {
            StringParser.properties = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase);
            StringParser.stringTagProviders = new System.Collections.Generic.Dictionary<string, IStringTagProvider>(System.StringComparer.OrdinalIgnoreCase);
            StringParser.propertyObjects = new System.Collections.Generic.Dictionary<string, object>();
            System.Reflection.Assembly entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                string location = entryAssembly.Location;
                StringParser.propertyObjects["exe"] = FileVersionInfo.GetVersionInfo(location);
            }
            StringParser.properties["User"] = System.Environment.UserName;
            StringParser.properties["Version"] = "3.2.1.6466";
            if (System.IntPtr.Size == 4)
            {
                StringParser.properties["Platform"] = "win32";
                return;
            }
            if (System.IntPtr.Size == 8)
            {
                StringParser.properties["Platform"] = "win64";
                return;
            }
            StringParser.properties["Platform"] = "unknown";
        }
        private static string Get(object obj, string name)
        {
            System.Type type = obj.GetType();
            System.Reflection.PropertyInfo property = type.GetProperty(name);
            if (property != null)
            {
                return property.GetValue(obj, null).ToString();
            }
            System.Reflection.FieldInfo field = type.GetField(name);
            if (field != null)
            {
                return field.GetValue(obj).ToString();
            }
            return null;
        }
        private static string GetProperty(string propertyName)
        {
            string defaultValue = "";
            int i = propertyName.LastIndexOf("??", System.StringComparison.Ordinal);
            if (i >= 0)
            {
                defaultValue = propertyName.Substring(i + 2);
                propertyName = propertyName.Substring(0, i);
            }
            i = propertyName.IndexOf('/');
            if (i >= 0)
            {
                Properties properties = PropertyService.Get<Properties>(propertyName.Substring(0, i), new Properties());
                propertyName = propertyName.Substring(i + 1);
                i = propertyName.IndexOf('/');
                while (i >= 0)
                {
                    properties = properties.Get<Properties>(propertyName.Substring(0, i), new Properties());
                    propertyName = propertyName.Substring(i + 1);
                }
                return properties.Get<string>(propertyName, defaultValue);
            }
            return PropertyService.Get<string>(propertyName, defaultValue);
        }
        public static string Parse(string input)
        {
            return StringParser.Parse(input, null);
        }
        public static void Parse(string[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = StringParser.Parse(inputs[i], null);
            }
        }
        public static void RegisterStringTagProvider(IStringTagProvider tagProvider)
        {
            string[] tags = tagProvider.Tags;
            for (int i = 0; i < tags.Length; i++)
            {
                string key = tags[i];
                StringParser.stringTagProviders[key] = tagProvider;
            }
        }
        public static string Parse(string input, string[,] customTags)
        {
            if (input == null)
            {
                return null;
            }
            int num = 0;
            System.Text.StringBuilder stringBuilder = null;
            int num2;
            while (true)
            {
                num2 = num;
                num = input.IndexOf("${", num, System.StringComparison.Ordinal);
                if (num < 0)
                {
                    break;
                }
                if (stringBuilder == null)
                {
                    if (num == 0)
                    {
                        stringBuilder = new System.Text.StringBuilder();
                    }
                    else
                    {
                        stringBuilder = new System.Text.StringBuilder(input, 0, num, num + 16);
                    }
                }
                else
                {
                    if (num > num2)
                    {
                        stringBuilder.Append(input, num2, num - num2);
                    }
                }
                int num3 = input.IndexOf('}', num + 1);
                if (num3 < 0)
                {
                    stringBuilder.Append("${");
                    num += 2;
                }
                else
                {
                    string text = input.Substring(num + 2, num3 - num - 2);
                    string value = StringParser.GetValue(text, customTags);
                    if (value == null)
                    {
                        stringBuilder.Append("${");
                        stringBuilder.Append(text);
                        stringBuilder.Append('}');
                    }
                    else
                    {
                        stringBuilder.Append(value);
                    }
                    num = num3 + 1;
                }
                if (num >= input.Length)
                {
                    goto Block_10;
                }
            }
            if (stringBuilder == null)
            {
                return input;
            }
            if (num2 < input.Length)
            {
                stringBuilder.Append(input, num2, input.Length - num2);
            }
            return stringBuilder.ToString();
        Block_10:
            return stringBuilder.ToString();
        }
        private static string GetValue(string propertyName, string[,] customTags)
        {
            if (propertyName.StartsWith("res:", System.StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string result = StringParser.Parse(ResourceService.GetString(propertyName.Substring(4)), customTags);
                    return result;
                }
                catch (ResourceNotFoundException)
                {
                    string result = null;
                    return result;
                }
            }
            if (propertyName.StartsWith("DATE:", System.StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string result = System.DateTime.Now.ToString(propertyName.Split(new char[]
					{
						':'
					})[1]);
                    return result;
                }
                catch (System.Exception ex)
                {
                    string result = ex.Message;
                    return result;
                }
            }
            if (propertyName.Equals("DATE", System.StringComparison.OrdinalIgnoreCase))
            {
                return System.DateTime.Today.ToShortDateString();
            }
            if (propertyName.Equals("TIME", System.StringComparison.OrdinalIgnoreCase))
            {
                return System.DateTime.Now.ToShortTimeString();
            }
            if (propertyName.Equals("ProductName", System.StringComparison.OrdinalIgnoreCase))
            {
                return "ProductName";
            }
            if (propertyName.Equals("GUID", System.StringComparison.OrdinalIgnoreCase))
            {
                return System.Guid.NewGuid().ToString().ToUpperInvariant();
            }
            if (customTags != null)
            {
                for (int i = 0; i < customTags.GetLength(0); i++)
                {
                    if (propertyName.Equals(customTags[i, 0], System.StringComparison.OrdinalIgnoreCase))
                    {
                        return customTags[i, 1];
                    }
                }
            }
            if (StringParser.properties.ContainsKey(propertyName))
            {
                return StringParser.properties[propertyName];
            }
            if (StringParser.stringTagProviders.ContainsKey(propertyName))
            {
                return StringParser.stringTagProviders[propertyName].Convert(propertyName);
            }
            int num = propertyName.IndexOf(':');
            if (num <= 0)
            {
                return null;
            }
            string text = propertyName.Substring(0, num);
            propertyName = propertyName.Substring(num + 1);
            string a;
            if ((a = text.ToUpperInvariant()) != null)
            {
                if (a == "ADDINPATH")
                {
                    foreach (AddIn current in AddInTree.AddIns)
                    {
                        if (current.Manifest.Identities.ContainsKey(propertyName))
                        {
                            string result = System.IO.Path.GetDirectoryName(current.FileName);
                            return result;
                        }
                    }
                    return null;
                }
                if (a == "ENV")
                {
                    return System.Environment.GetEnvironmentVariable(propertyName);
                }
                if (a == "PROPERTY")
                {
                    return StringParser.GetProperty(propertyName);
                }
            }
            if (StringParser.propertyObjects.ContainsKey(text))
            {
                return StringParser.Get(StringParser.propertyObjects[text], propertyName);
            }
            return null;
        }
    }
}
