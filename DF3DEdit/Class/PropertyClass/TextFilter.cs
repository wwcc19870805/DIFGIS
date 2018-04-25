using System;
using System.Text.RegularExpressions;

namespace DF3DEdit.Class
{
    public class TextFilter
    {
        private string pattern;
        public TextFilter(TextType t)
        {
            this.pattern = this.RegisterPattern(t);
        }
        public string Parsing(string value)
        {
            return System.Text.RegularExpressions.Regex.Replace(value, this.pattern, "");
        }
        public bool IsMarch(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, this.pattern);
        }
        private string RegisterPattern(TextType t)
        {
            if (t <= TextType.RS)
            {
                switch (t)
                {
                    case TextType.FileName:
                        {
                            return "[\\u4e00-\\u9fa5\\w\\s@]";
                        }
                    case TextType.PassWord:
                        {
                            return "^[\\w\\@]*$";
                        }
                    case TextType.IP:
                        {
                            return "[\\dlocalhost]";
                        }
                    case (TextType)3:
                    case (TextType)5:
                    case (TextType)6:
                    case (TextType)7:
                        {
                            break;
                        }
                    case TextType.Number:
                        {
                            return "^\\d$";
                        }
                    case TextType.Port:
                        {
                            return "^\\d{0,5}$";
                        }
                    default:
                        {
                            if (t == TextType.RS)
                            {
                                return "[\\u4e00-\\u9fa5\\w\\s[]\",]";
                            }
                            break;
                        }
                }
            }
            else
            {
                if (t == TextType.Node)
                {
                    return "[\\u4e00-\\u9fa5\\w\\s]";
                }
                switch (t)
                {
                    case TextType.Path:
                        {
                            return "[\\u4e00-\\u9fa5a-zA-Z0-9\\:\\\\]";
                        }
                    case TextType.FdeField:
                        {
                            return "^[\\u4e00-\\u9fa5\\w\\s]*$";
                        }
                }
            }
            return "";
        }
    }
}
