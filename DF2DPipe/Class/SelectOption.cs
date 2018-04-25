using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
namespace DF2DPipe.Class
{
    public class SelectOption
    {
        public SelectOption()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private int m_SelectRelection;
        private bool m_ClearInVisible;
        private int m_Tolerate;
        private int m_DefaultColorRGB;
        private bool m_ShowWarning;
        private int m_WarningThreshold;
        private int m_ResultMethod;

        public int SelectRelection
        {
            get { return m_SelectRelection; }
            set { m_SelectRelection = value; }
        }
        public bool ClearInVisible
        {
            get { return m_ClearInVisible; }
            set { m_ClearInVisible = value; }
        }
        public int Tolerate
        {
            get { return m_Tolerate; }
            set { m_Tolerate = value; }
        }
        public int DefaultColorRGB
        {
            get { return m_DefaultColorRGB; }
            set { m_DefaultColorRGB = value; }
        }
        public bool ShowWarning
        {
            get { return m_ShowWarning; }
            set { m_ShowWarning = value; }
        }
        public int WarningThreshold
        {
            get { return m_WarningThreshold; }
            set { m_WarningThreshold = value; }
        }
        public int ResultMethod
        {
            get { return m_ResultMethod; }
            set { m_ResultMethod = value; }
        }

        //[NonSerialized]
        public static string SYSTEM_SELECTOPTION_FILE = @"\..\option\SelOpt.xml"; //修改 袁怀月 2007－09-29

        public static SelectOption GetSelectOption()
        {
            SelectOption result = GetSelectOptionFromFile();
            if (result == null)
            {
                result = new SelectOption();
                result.SelectRelection = 1;
                result.ClearInVisible = true;
                result.Tolerate = 3;
                result.DefaultColorRGB = -12517377;
                result.ShowWarning = true;
                result.WarningThreshold = 2000;
            }
            result.ResultMethod = 0;
            return result;
        }

        private static SelectOption GetSelectOptionFromFile()//读取文件中保存的选择设置
        {
            SelectOption result = null;
            string optFile = Application.StartupPath + SYSTEM_SELECTOPTION_FILE;
            Stream stream;
            if (File.Exists(optFile))
            {
                XmlSerializer ser = new XmlSerializer(typeof(SelectOption));
                stream = new FileStream(optFile, FileMode.Open);
                try
                {
                    SelectOption tmp = (SelectOption)ser.Deserialize(stream);
                    result = tmp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SelectionOption反序列化出错！" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
                }
                finally
                {
                    if (stream != null) stream.Close();
                }
            }
            return result;
        }

        public static bool SaveSelectOption(SelectOption selOpt)
        {
            try
            {
                string optFile = Application.StartupPath + SYSTEM_SELECTOPTION_FILE;
                XmlSerializer ser = new XmlSerializer(typeof(SelectOption));
                if (!Directory.Exists(Path.GetDirectoryName(optFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(optFile));
                }
                Stream stream = new FileStream(optFile, FileMode.Create);
                ser.Serialize(stream, selOpt);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SelectionOption序列化出错！" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }
    }
}
