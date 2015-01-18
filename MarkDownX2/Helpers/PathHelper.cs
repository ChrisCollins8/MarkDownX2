using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkDownX2.Helpers
{
    public static class PathHelper
    {
        private static string _LocalAppData = "";
        /// <summary>
        /// Local application data path IE c:\users\name\appdata\local
        /// </summary>
        public static string LocalAppData
        {
            get
            {
                if (String.IsNullOrEmpty(_LocalAppData))
                {
                    _LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                }
                return _LocalAppData;
            }
        }

        private static string _StoragePath = "";
        /// <summary>
        /// Path where application settings are stored
        /// </summary>
        public static string StoragePath
        {
            get
            {
                if (String.IsNullOrEmpty(_StoragePath))
                {
                    _StoragePath = Path.Combine(LocalAppData, "MarkDownX2");
                    if (!Directory.Exists(_StoragePath))
                    {
                        try
                        {
                            Directory.CreateDirectory(_StoragePath);
                        }
                        catch(Exception ex) {
                            ExceptionHelper.Process(ex);
                        }
                    }
                }
                return _StoragePath;
            }
        }

        private static string _SettingsFile { get; set; }
        public static string SettingsFile
        {
            get
            {
                if (String.IsNullOrEmpty(_SettingsFile))
                {
                    _SettingsFile = Path.Combine(StoragePath, "Settings.xml");
                }
                return _SettingsFile;
            }
        }

        private static string _ParserPath = "";
        /// <summary>
        /// Directory where parsers are stored.
        /// </summary>
        public static string ParserPath
        {
            get
            {
                if (String.IsNullOrEmpty(_ParserPath))
                {
                    _ParserPath = Path.Combine(StoragePath, "Parsers");
                    if (!Directory.Exists(_ParserPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(_ParserPath);
                        }
                        catch (Exception ex)
                        {
                            ExceptionHelper.Process(ex);
                        }
                    }
                }
                return _ParserPath;
            }
        }

        private static string _AppPath = "";
        /// <summary>
        /// Directory where exe resides
        /// </summary>
        public static string AppPath
        {
            get
            {
                if (String.IsNullOrEmpty(_AppPath))
                {
                    _AppPath = Path.GetDirectoryName(Application.ExecutablePath);
                }
                return _AppPath;
            }
        }

        private static string _ReadmeFile = "";
        public static string ReadmeFile
        {
            get
            {
                if (String.IsNullOrEmpty(_ReadmeFile))
                {
                    _ReadmeFile = Path.Combine(AppPath, "Readme.md");
                }
                return _ReadmeFile;
            }
        }

        private static string _StyleSheetPath = null;
        public static string StyleSheetPath
        {
            get
            {
                if (_StyleSheetPath == null)
                {
                    _StyleSheetPath = Path.Combine(StoragePath, "StyleSheets");
                    if (!Directory.Exists(_StyleSheetPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(_StyleSheetPath);
                        }
                        catch (Exception ex)
                        {
                            ExceptionHelper.Process(ex);
                        }
                    }
                    
                }
                return _StyleSheetPath;
            }
        }
    }
}
