using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarkDownX2.Helpers
{
    public static class RunHelper
    {
        private static string ParsersNamespace = "MarkDownX2.DefaultParsers";
        private static string StyleSheetsNamespace = "MarkDownX2.StyleSheets";

        private static byte[] ReadResource(string name){
            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            using (Stream resFilestream = a.GetManifestResourceStream(name))
            {
                if (resFilestream == null) return null;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
            
        }

        public static void StartApp()
        {
            // Copy in default stylesheets if they don't exist
            CopyCSS();

            // Copy in default parsers if they don't exist
            CopyParsers();
        }

        /// <summary>
        /// Copies all embedded css styles into the stylesheets directory. This is to ensure the expected
        /// stylesheets exist.
        /// </summary>
        private static void CopyCSS()
        {
            List<string> styleSheets = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(
                name => name.StartsWith(StyleSheetsNamespace) && name.EndsWith(".css")).ToList();
            foreach (string styleSheet in styleSheets)
            {
                string shortName = styleSheet.Replace(StyleSheetsNamespace, "").Trim('.');
                string finalPath = Path.Combine(PathHelper.StyleSheetPath, shortName);
                if (!File.Exists(finalPath))
                {
                    try
                    {
                        File.WriteAllBytes(finalPath, ReadResource(styleSheet));
                    }
                    catch { }
                }
            }
        }


        /// <summary>
        /// Copies all the embedded default parsers into the parsers directory so app can run for
        /// first time. 
        /// </summary>
        private static void CopyParsers()
        {
            List<string> parsers = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(
                name => name.StartsWith(ParsersNamespace) && name.EndsWith(".dll")).ToList();
            foreach (string parser in parsers)
            {
                string shortName = parser.Replace(ParsersNamespace, "").Trim('.');
                string finalPath = Path.Combine(PathHelper.ParserPath, shortName);
                if (!File.Exists(finalPath))
                {
                    try
                    {
                        File.WriteAllBytes(finalPath, ReadResource(parser));
                    }
                    catch { }
                }
            }
        }

    }
}
