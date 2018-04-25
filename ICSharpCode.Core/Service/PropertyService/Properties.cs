using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace ICSharpCode.Core
{
    public class Properties
    {
        private class SerializedValue
        {
            private string content;
            public string Content
            {
                get
                {
                    return this.content;
                }
            }
            public T Deserialize<T>()
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(new System.IO.StringReader(this.content));
            }
            public SerializedValue(string content)
            {
                this.content = content;
            }
        }
        private System.Collections.Generic.Dictionary<string, object> properties = new System.Collections.Generic.Dictionary<string, object>();
        public event PropertyChangedEventHandler PropertyChanged;
        public string this[string property]
        {
            get
            {
                return System.Convert.ToString(this.Get(property), System.Globalization.CultureInfo.InvariantCulture);
            }
            set
            {
                this.Set<string>(property, value);
            }
        }
        public string[] Elements
        {
            get
            {
                string[] result;
                lock (this.properties)
                {
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
                    foreach (System.Collections.Generic.KeyValuePair<string, object> current in this.properties)
                    {
                        list.Add(current.Key);
                    }
                    result = list.ToArray();
                }
                return result;
            }
        }
        public int Count
        {
            get
            {
                int count;
                lock (this.properties)
                {
                    count = this.properties.Count;
                }
                return count;
            }
        }
        public object Get(string property)
        {
            object result;
            lock (this.properties)
            {
                object obj2;
                this.properties.TryGetValue(property, out obj2);
                result = obj2;
            }
            return result;
        }
        public T Get<T>(string property, T defaultValue)
        {
            T result;
            lock (this.properties)
            {
                object obj2;
                if (!this.properties.TryGetValue(property, out obj2))
                {
                    this.properties.Add(property, defaultValue);
                    result = defaultValue;
                }
                else
                {
                    if (obj2 is string && typeof(T) != typeof(string))
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                        try
                        {
                            obj2 = converter.ConvertFromInvariantString(obj2.ToString());
                        }
                        catch (System.Exception)
                        {
                            obj2 = defaultValue;
                        }
                        this.properties[property] = obj2;
                    }
                    else
                    {
                        if (obj2 is System.Collections.ArrayList && typeof(T).IsArray)
                        {
                            System.Collections.ArrayList arrayList = (System.Collections.ArrayList)obj2;
                            System.Type elementType = typeof(T).GetElementType();
                            System.Array array = System.Array.CreateInstance(elementType, arrayList.Count);
                            TypeConverter converter2 = TypeDescriptor.GetConverter(elementType);
                            try
                            {
                                for (int i = 0; i < array.Length; i++)
                                {
                                    if (arrayList[i] != null)
                                    {
                                        array.SetValue(converter2.ConvertFromInvariantString(arrayList[i].ToString()), i);
                                    }
                                }
                                obj2 = array;
                            }
                            catch (System.Exception)
                            {
                                obj2 = defaultValue;
                            }
                            this.properties[property] = obj2;
                        }
                        else
                        {
                            if (!(obj2 is string) && typeof(T) == typeof(string))
                            {
                                TypeConverter converter3 = TypeDescriptor.GetConverter(typeof(T));
                                if (converter3.CanConvertTo(typeof(string)))
                                {
                                    obj2 = converter3.ConvertToInvariantString(obj2);
                                }
                                else
                                {
                                    obj2 = obj2.ToString();
                                }
                                this.properties[property] = obj2;
                            }
                            else
                            {
                                if (obj2 is Properties.SerializedValue)
                                {
                                    try
                                    {
                                        obj2 = ((Properties.SerializedValue)obj2).Deserialize<T>();
                                    }
                                    catch (System.Exception)
                                    {
                                        obj2 = defaultValue;
                                    }
                                    this.properties[property] = obj2;
                                }
                            }
                        }
                    }
                    try
                    {
                        result = (T)obj2;
                    }
                    catch (System.NullReferenceException)
                    {
                        result = defaultValue;
                    }
                }
            }
            return result;
        }
        public void Set<T>(string property, T value)
        {
            if (property == null)
            {
                throw new System.ArgumentNullException("property");
            }
            if (value == null)
            {
                throw new System.ArgumentNullException("value");
            }
            T t = default(T);
            lock (this.properties)
            {
                if (!this.properties.ContainsKey(property))
                {
                    this.properties.Add(property, value);
                }
                else
                {
                    t = this.Get<T>(property, value);
                    this.properties[property] = value;
                }
                this.OnPropertyChanged(new PropertyChangedEventArgs(this, property, t, value));
            }
        }
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
        public bool Contains(string property)
        {
            bool result;
            lock (this.properties)
            {
                result = this.properties.ContainsKey(property);
            }
            return result;
        }
        public bool Remove(string property)
        {
            bool result;
            lock (this.properties)
            {
                result = this.properties.Remove(property);
            }
            return result;
        }
        public override string ToString()
        {
            string result;
            lock (this.properties)
            {
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                stringBuilder.Append("[Properties:{");
                foreach (System.Collections.Generic.KeyValuePair<string, object> current in this.properties)
                {
                    stringBuilder.Append(current.Key);
                    stringBuilder.Append("=");
                    stringBuilder.Append(current.Value);
                    stringBuilder.Append(",");
                }
                stringBuilder.Append("}]");
                result = stringBuilder.ToString();
            }
            return result;
        }
        public static Properties ReadFromAttributes(XmlReader reader)
        {
            Properties properties = new Properties();
            if (reader.HasAttributes)
            {
                for (int i = 0; i < reader.AttributeCount; i++)
                {
                    reader.MoveToAttribute(i);
                    properties[reader.Name] = reader.Value;
                }
                reader.MoveToElement();
            }
            return properties;
        }
        internal void ReadProperties(XmlReader reader, string endElement)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }
            while (reader.Read())
            {
                XmlNodeType nodeType = reader.NodeType;
                if (nodeType != XmlNodeType.Element)
                {
                    if (nodeType == XmlNodeType.EndElement && reader.LocalName == endElement)
                    {
                        return;
                    }
                }
                else
                {
                    string text = reader.LocalName;
                    if (text == "Properties")
                    {
                        text = reader.GetAttribute(0);
                        Properties properties = new Properties();
                        properties.ReadProperties(reader, "Properties");
                        this.properties[text] = properties;
                    }
                    else
                    {
                        if (text == "Array")
                        {
                            text = reader.GetAttribute(0);
                            this.properties[text] = this.ReadArray(reader);
                        }
                        else
                        {
                            if (text == "SerializedValue")
                            {
                                text = reader.GetAttribute(0);
                                this.properties[text] = new Properties.SerializedValue(reader.ReadInnerXml());
                            }
                            else
                            {
                                this.properties[text] = (reader.HasAttributes ? reader.GetAttribute(0) : null);
                            }
                        }
                    }
                }
            }
        }
        private System.Collections.ArrayList ReadArray(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                return new System.Collections.ArrayList(0);
            }
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            while (reader.Read())
            {
                XmlNodeType nodeType = reader.NodeType;
                if (nodeType != XmlNodeType.Element)
                {
                    if (nodeType == XmlNodeType.EndElement && reader.LocalName == "Array")
                    {
                        return arrayList;
                    }
                }
                else
                {
                    arrayList.Add(reader.HasAttributes ? reader.GetAttribute(0) : null);
                }
            }
            return arrayList;
        }
        public static Properties Load(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return null;
            }
            using (XmlTextReader xmlTextReader = new XmlTextReader(fileName))
            {
                while (xmlTextReader.Read())
                {
                    string localName;
                    if (xmlTextReader.IsStartElement() && (localName = xmlTextReader.LocalName) != null && localName == "Properties")
                    {
                        Properties properties = new Properties();
                        properties.ReadProperties(xmlTextReader, "Properties");
                        return properties;
                    }
                }
            }
            return null;
        }
        public void WriteProperties(XmlWriter writer)
        {
            lock (this.properties)
            {
                System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object>> list = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object>>(this.properties);
                list.Sort((System.Collections.Generic.KeyValuePair<string, object> a, System.Collections.Generic.KeyValuePair<string, object> b) => System.StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key));
                foreach (System.Collections.Generic.KeyValuePair<string, object> current in list)
                {
                    object value = current.Value;
                    if (value != null)
                    {
                        if (value is Properties)
                        {
                            writer.WriteStartElement("Properties");
                            writer.WriteAttributeString("name", current.Key);
                            ((Properties)value).WriteProperties(writer);
                            writer.WriteEndElement();
                        }
                        else
                        {
                            if (value is System.Array || value is System.Collections.ArrayList)
                            {
                                writer.WriteStartElement("Array");
                                writer.WriteAttributeString("name", current.Key);
                                foreach (object current2 in (System.Collections.IEnumerable)value)
                                {
                                    writer.WriteStartElement("Element");
                                    this.WriteValue(writer, current2);
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();
                            }
                            else
                            {
                                if (TypeDescriptor.GetConverter(value).CanConvertFrom(typeof(string)))
                                {
                                    writer.WriteStartElement(current.Key);
                                    this.WriteValue(writer, value);
                                    writer.WriteEndElement();
                                }
                                else
                                {
                                    if (value is Properties.SerializedValue)
                                    {
                                        writer.WriteStartElement("SerializedValue");
                                        writer.WriteAttributeString("name", current.Key);
                                        writer.WriteRaw(((Properties.SerializedValue)value).Content);
                                        writer.WriteEndElement();
                                    }
                                    else
                                    {
                                        writer.WriteStartElement("SerializedValue");
                                        writer.WriteAttributeString("name", current.Key);
                                        XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());
                                        xmlSerializer.Serialize(writer, value, null);
                                        writer.WriteEndElement();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void WriteValue(XmlWriter writer, object val)
        {
            if (val != null)
            {
                if (val is string)
                {
                    writer.WriteAttributeString("value", val.ToString());
                    return;
                }
                TypeConverter converter = TypeDescriptor.GetConverter(val.GetType());
                writer.WriteAttributeString("value", converter.ConvertToInvariantString(val));
            }
        }
        public void Save(string fileName)
        {
            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, System.Text.Encoding.UTF8))
            {
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.WriteStartElement("Properties");
                this.WriteProperties(xmlTextWriter);
                xmlTextWriter.WriteEndElement();
            }
        }
    }
}
