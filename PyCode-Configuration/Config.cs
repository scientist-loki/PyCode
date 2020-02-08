using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PyCode_Configuration
{
    public static class Config
    {
        // Import kernel API
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        // Written
        public static void writeConfiguration(string Path, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

        // Reader
        public static string readConfiguration(string path, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            GetPrivateProfileString(Section, Key, "", temp, 500, path);
            return temp.ToString();
        }

        // Finding
        public static bool findConfiguration(string path)
        {
            return File.Exists(path);
        }
    }
}
