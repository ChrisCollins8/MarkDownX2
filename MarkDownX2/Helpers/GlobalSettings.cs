using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MarkDownX2.GUI.Forms;
using MarkDownX2.Models;

namespace MarkDownX2.Helpers
{
    public static class GlobalSettings
    {

        public static SettingsModel Settings = null;

        public static void Initialize()
        {
            // Try to load settings
            if (File.Exists(PathHelper.SettingsFile))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SettingsModel));
                    using (StringReader reader = new StringReader(File.ReadAllText(PathHelper.SettingsFile)))
                    {
                        Settings = (SettingsModel)(serializer.Deserialize(reader));
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHelper.Process(ex);
                    // Failed so simply create default settings
                    WriteSettings(DefaultSettings);
                }
            }
            else
            {
                WriteSettings(DefaultSettings);
            }
        }

        private static SettingsModel DefaultSettings
        {
            get
            {
                return new SettingsModel()
                {
                    Id = Guid.NewGuid()
                };
            }
        }

        public static void WriteSettings(SettingsModel settings = null)
        {
            if (settings == null)
            {
                settings = Settings;
            }
            else
            {
                Settings = settings;
            }
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(SettingsModel));

            System.IO.StreamWriter file = new System.IO.StreamWriter(PathHelper.SettingsFile);
            writer.Serialize(file, settings);
            file.Close();

        }

        /// <summary>
        /// Update settings on all documents.
        /// </summary>
        public static void UpdateSettings()
        {
            DocumentsHelper.UpdateSettings();
        }

    }
}
