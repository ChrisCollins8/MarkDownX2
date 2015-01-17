using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarkDownX2.Helpers
{
    public static class UpdateHelper
    {
        // Production
        //private static string UpdateUrl = "http://update.markedownx2.com/?Version={0}&id={1}";

        // Testing
        private static string UpdateUrl = "http://localhost:60543/?Version={0}&id={1}";
        
        private static string DownloadUrl = null;

        /// <summary>
        /// Checks to see if there is an update available. If an update is available then the
        /// DownloadUrl property is set to the url returned from the update check
        /// </summary>
        /// <returns></returns>
        public static bool CheckForUpdate()
        {
            string updateUrl = GetUpdateUrl();
            if (!String.IsNullOrEmpty(updateUrl))
            {
                DownloadUrl = updateUrl;
                return true;
            }
            return false;
        }

        private static string GetUpdateUrl()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string url = String.Format(UpdateUrl, version.ToString(), GlobalSettings.Settings.Id);
            string result = GetResult(url);
            return result;
        }

        private static string GetResult(string url)
        {
            try
            {
                WebRequest req = HttpWebRequest.Create(url);
                req.Method = "GET";

                string source = null;
                using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    source = reader.ReadToEnd();
                }
                return source;
            }
            catch
            {
                // A failed web request means 1 of several things and all of them are really 
                // meaningless.
                // 1) The internet is off, down, etc
                // 2) The web address (markdown domain) or app is down
                // 3) A DNS problem
                // 4) Something else.
                // Ultimately it's tough to predict is a web request will work perfectly
                // but if not best to just fail quietly and keep going and try again
                // next time.
            }
            return null;
        }

    }
}

