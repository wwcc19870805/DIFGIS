using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace DFCommon.Class
{
    public class ZipHelper
    {
        // Fields
        private static List<string[]> files;
        private static List<string[]> paths;
        private static string root = "";

        // Methods
        private static void GetAllDirectories(string rootPath)
        {
            string[] directories = Directory.GetDirectories(rootPath);
            foreach (string str in directories)
            {
                GetAllDirectories(str);
            }
            string[] files = Directory.GetFiles(rootPath);
            foreach (string str2 in files)
            {
                ZipHelper.files.Add(new string[] { str2, root });
            }
            if ((directories.Length == files.Length) && (files.Length == 0))
            {
                paths.Add(new string[] { rootPath, root });
            }
        }

        public static bool Pack(string[] filesOrDirectoriesPaths, string strZipPath, int intZipLevel, string strPassword, out string error)
        {
            files = new List<string[]>();
            paths = new List<string[]>();
            root = "";
            if (filesOrDirectoriesPaths.Length > 0)
            {
                foreach (string str in filesOrDirectoriesPaths)
                {
                    if (File.Exists(str))
                    {
                        files.Add(new string[] { str, str.Substring(0, str.LastIndexOf(@"\") + 1) });
                    }
                    else if (Directory.Exists(str))
                    {
                        root = str.Substring(0, str.LastIndexOf(@"\") + 1);
                        GetAllDirectories(str);
                    }
                    else
                    {
                        error = "请检查文件路径，某些文件不存在！";
                        return false;
                    }
                }
            }
            ZipOutputStream stream = new ZipOutputStream(File.Create(strZipPath));
            stream.SetLevel(intZipLevel);
            stream.Password = strPassword;
            foreach (string[] strArray in files)
            {
                try
                {
                    FileStream stream2 = File.OpenRead(strArray[0]);
                    byte[] buffer = new byte[stream2.Length];
                    stream2.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(strArray[0].Replace(strArray[1], string.Empty))
                    {
                        DateTime = DateTime.Now
                    };
                    stream.PutNextEntry(entry);
                    stream.Write(buffer, 0, buffer.Length);
                    stream2.Close();
                    stream2.Dispose();
                    continue;
                }
                catch (Exception)
                {
                    error = "文件读取错误";
                    return false;
                }
            }
            files.Clear();
            foreach (string[] strArray2 in paths)
            {
                ZipEntry entry2 = new ZipEntry(strArray2[0].Replace(strArray2[1], string.Empty) + "/");
                stream.PutNextEntry(entry2);
            }
            paths.Clear();
            stream.Finish();
            stream.Close();
            error = "";
            return true;
        }

        public static bool Unpack(string zipfilename, string UnZipDir, string password, out string error)
        {
            if (!File.Exists(zipfilename))
            {
                File.Delete(UnZipDir);
                error = "待解包文件路径不存在";
                return false;
            }
            ZipInputStream stream = new ZipInputStream(File.OpenRead(zipfilename));
            if ((password != null) && (password.Length > 0))
            {
                stream.Password = password;
            }
            try
            {
                ZipEntry entry;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    if (Directory.Exists(UnZipDir))
                    {
                        Directory.CreateDirectory(UnZipDir);
                    }
                    string path = UnZipDir;
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);
                    path = path + @"\" + directoryName;
                    Directory.CreateDirectory(path);
                    if (fileName != string.Empty)
                    {
                        FileStream stream2 = File.Create(path + @"\" + fileName);
                        int count = 0x800;
                        byte[] buffer = new byte[count];
                        while (true)
                        {
                            count = stream.Read(buffer, 0, buffer.Length);
                            if (count <= 0)
                            {
                                break;
                            }
                            stream2.Write(buffer, 0, count);
                        }
                        stream2.Close();
                    }
                }
                stream.Close();
            }
            catch (Exception exception)
            {
                error = exception.Message.ToString();
                return false;
            }
            finally
            {
                stream.Close();
            }
            error = "";
            return true;
        }
    }
}
