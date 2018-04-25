using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace ICSharpCode.Core
{
    public static class FileUtility
    {
        private static string applicationRootPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string ApplicationRootPath
        {
            get
            {
                return FileUtility.applicationRootPath;
            }
            set
            {
                FileUtility.applicationRootPath = value;
            }
        }
        public static string LocalUserAppDataPath
        {
            get;
            set;
        }
        public static System.Collections.Generic.List<string> SearchDirectory(string directory, string filemask, bool searchSubdirectories, bool ignoreHidden)
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            FileUtility.SearchDirectory(directory, filemask, list, searchSubdirectories, ignoreHidden);
            return list;
        }
        public static System.Collections.Generic.List<string> SearchDirectory(string directory, string filemask, bool searchSubdirectories)
        {
            return FileUtility.SearchDirectory(directory, filemask, searchSubdirectories, true);
        }
        public static System.Collections.Generic.List<string> SearchDirectory(string directory, string filemask)
        {
            return FileUtility.SearchDirectory(directory, filemask, true, true);
        }
        private static void SearchDirectory(string directory, string filemask, System.Collections.Generic.List<string> collection, bool searchSubDiretories, bool ignoreHidden)
        {
            try
            {
                bool flag = Regex.IsMatch(filemask, "^\\*\\..{3}$");
                string b = null;
                string[] files = System.IO.Directory.GetFiles(directory, filemask);
                if (flag)
                {
                    b = filemask.Remove(0, 1);
                }
                string[] array = files;
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    if ((!ignoreHidden || (System.IO.File.GetAttributes(text) & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden) && (!flag || !(System.IO.Path.GetExtension(text) != b)))
                    {
                        collection.Add(text);
                    }
                }
                if (searchSubDiretories)
                {
                    string[] directories = System.IO.Directory.GetDirectories(directory);
                    string[] array2 = directories;
                    for (int j = 0; j < array2.Length; j++)
                    {
                        string text2 = array2[j];
                        if (!ignoreHidden || (System.IO.File.GetAttributes(text2) & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden)
                        {
                            FileUtility.SearchDirectory(text2, filemask, collection, searchSubDiretories, ignoreHidden);
                        }
                    }
                }
            }
            catch (System.UnauthorizedAccessException)
            {
            }
        }
        public static string Combine(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
            {
                return string.Empty;
            }
            string text = paths[0];
            for (int i = 1; i < paths.Length; i++)
            {
                text = System.IO.Path.Combine(text, paths[i]);
            }
            return text;
        }
    }
}
