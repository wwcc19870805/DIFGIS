using System;
namespace ICSharpCode.Core
{
    public class PropertyChangedEventArgs : System.EventArgs
    {
        private Properties properties;
        private string key;
        private object newValue;
        private object oldValue;
        public Properties Properties
        {
            get
            {
                return this.properties;
            }
        }
        public string Key
        {
            get
            {
                return this.key;
            }
        }
        public object NewValue
        {
            get
            {
                return this.newValue;
            }
        }
        public object OldValue
        {
            get
            {
                return this.oldValue;
            }
        }
        public PropertyChangedEventArgs(Properties p, string key, object oldValue, object newValue)
        {
            this.properties = p;
            this.key = key;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    }
}
