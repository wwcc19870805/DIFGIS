using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using ICSharpCode.Core;
using System.Collections;

namespace DFCommon.Class
{
    public static class WebApp
    {
        public static string GetNetIP()
        {
            try
            {
                string url = "http://1212.ip138.com/ic.asp";
                Uri uri = new Uri(url);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                req.Method = "get";
                using (Stream s = req.GetResponse().GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(s, Encoding.GetEncoding("gb2312")))
                    {
                        char[] ch = { '[', ']' };
                        string str = reader.ReadToEnd();
                        Match m = Regex.Match(str, @"\[(?<IP>[0-9\.]*)\]");
                        return m.Value.Trim(ch);
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string CallWebService(string url, string method, string[] keys, string[] values)
        {
            try
            {
                string param = "";
                if (keys != null && values != null && keys.Count() == values.Count()) 
                {
                    param = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                          <soap:Body>
                                            <" + method + @" xmlns=""http://tempuri.org/"">";
                    for (int i = 0; i < keys.Count(); i++)
                    {
                        param += "<" + keys[i] + ">" + values[i] + "</" + keys[i] + ">";
                    }
                    param += "</" + method + @"></soap:Body></soap:Envelope>";
                }
                else
                {
                    param = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                          <soap:Body>
                                            <" + method + @" xmlns=""http://tempuri.org/"">"
                                            + @"</" + method + @"></soap:Body></soap:Envelope>"; ;
                }

                byte[] bs = Encoding.UTF8.GetBytes(param);
                //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://192.168.11.157:7004/SystemLog.asmx");
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.Headers.Add("SOAPAction", "http://tempuri.org/" + method);
                req.ContentType = "text/xml; charset=utf-8";
                req.ContentLength = bs.Length;
                req.Timeout = 5000;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }

                using (HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse())
                {
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string CallWebHandle(string url, string[] keys, string[] values)
        {
            try
            {
                string temp = "";
                if (keys != null && values != null && keys.Count() == values.Count())
                {
                    for (int i = 0; i < keys.Count(); i++)
                    {
                        temp += keys[i] + "=" + values[i] + "&";
                    }
                }
                if (temp != "") temp = temp.Substring(0, temp.Length - 1);
                byte[] bs = Encoding.UTF8.GetBytes(temp);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bs.Length;
                req.Timeout = 5000;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }
                using (HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse())
                {
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    return sr.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
