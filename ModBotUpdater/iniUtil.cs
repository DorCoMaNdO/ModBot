using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ModBotUpdater
{
    /// <summary>
    /// Class to read/write key values in a INI file
    /// </summary>
    public class iniUtil
    {
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSectionNames(byte[] lpszReturnBuffer, int nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, byte[] lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern bool WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        private const int VALUE_BUFFER = 511;
        private const int SECTION_BUFFER = (1024 * 16);
        public string m_sIniFile { get; private set; }

        /// <summary>
        /// .ctor with INI file name
        /// </summary>
        /// <param name="fileName">Fullpath to the INI file</param>
        public iniUtil(string fileName, string sDefault = "\r\n")
        {
            Initialize(fileName, sDefault);
        }

        public iniUtil(string path, string fileName, string sDefault = "\r\n")
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            Initialize(path + fileName, sDefault);
        }

        private void Initialize(string fileName, string sDefault = "\r\n")
        {
            //if (Program.ini != null && fileName == Program.ini.m_sIniFile) throw new Exception("An unauthorized application/extension has attempted to access the bot's settings.");
            if (Program.ini != null && fileName == Program.ini.m_sIniFile)
            {
                System.Windows.Forms.MessageBox.Show("An unauthorized application/extension has attempted to access the bot's settings.\r\nThe bot will now close.", "Restricted Access", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            m_sIniFile = fileName;
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, sDefault);
            }
        }

        /// <summary>
        /// Set the value for a specific key in a section
        /// </summary>
        /// <param name="section">Section containing the key to write to</param>
        /// <param name="key">Key to insert/update</param>
        /// <param name="keyvalue">Value for the key</param>
        /// <returns>True if OK</returns>
        public bool SetValue(string section, string key, string keyvalue)
        {
            return WritePrivateProfileString(section, key, keyvalue, m_sIniFile);
        }

        /// <summary>
        /// Gets the value of the specidied key in the specified section, 
        /// If the key doesn't exists returns the default value
        /// </summary>
        /// <param name="section">Section containing the key to read from</param>
        /// <param name="key">Required key</param>
        /// <param name="ifMissing">Value to return in case the key is missing</param>
        /// <returns>string value of the key or missing value</returns>
        public string GetValue(string section, string key, string ifMissing)
        {
            byte[] by = new byte[VALUE_BUFFER];
            int n = GetPrivateProfileString(section, key, ifMissing, by, VALUE_BUFFER, m_sIniFile);
            string s = Encoding.ASCII.GetString(by);
            return s.Substring(0, n);
        }

        /// <summary>
        /// Returns the NameValueCollection for every key in the section
        /// </summary>
        /// <param name="section">Section name</param>
        /// <returns>NameValueCollection with nake=Key and value=value</returns>
        public NameValueCollection GetSectionKeysvalues(string section)
        {
            NameValueCollection n = new NameValueCollection();
            if (section.Length > 0)
            {
                byte[] by = new byte[SECTION_BUFFER];
                int x = GetPrivateProfileSection(section, by, SECTION_BUFFER, m_sIniFile);
                if (x > 0) x--;
                string keysvalues = Encoding.ASCII.GetString(by, 0, x);
                string[] temp = keysvalues.Split('\0');
                foreach (string s in temp)
                {
                    string[] t = s.Split('=');
                    n.Add(t[0], t[1]);
                }
            }
            return n;
        }

        /// <summary>
        /// Get the names of all sections in .INI
        /// </summary>
        /// <returns>string array with all the key names</returns>
        public string[] GetSectionNames()
        {
            byte[] by = new byte[SECTION_BUFFER];
            int x = GetPrivateProfileSectionNames(by, SECTION_BUFFER, m_sIniFile);
            if (x > 0) x--;
            string keys = Encoding.ASCII.GetString(by, 0, x);
            return keys.Split('\0');
        }
    }
}