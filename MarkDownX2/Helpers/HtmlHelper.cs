using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkDownX2.Helpers
{
    public static class HtmlHelper
    {
        public static Dictionary<string, string> StyleSheets = new Dictionary<string, string>();
        public static string currentCss = "";
        public static void LoadStylesheets()
        {
            string[] files = Directory.GetFiles(PathHelper.StyleSheetPath, "*.css");
            foreach (string file in files)
            {
                StyleSheets.Add(Path.GetFileName(file), file);
            }

            string cssFilename = "github.css";
            if (!String.IsNullOrEmpty(GlobalSettings.Settings.DefaultCss))
                cssFilename = GlobalSettings.Settings.DefaultCss;

            SetStylesheet(cssFilename);

        }


        public static void SetStylesheet(string name)
        {
            if (StyleSheets.ContainsKey(name))
            {
                if (File.Exists(StyleSheets[name]))
                    currentCss = StyleSheets[name];
                else
                {
                    MessageBox.Show("The CSS being configured does not appear to exist.");
                }
            }
        }

        public static string GetHtml()
        {
            string path = "";
            if (File.Exists(currentCss)){

                path = String.Format("local://{0}", Path.GetFileName(currentCss));
                Uri uri = new Uri(currentCss, UriKind.Absolute);
                path = uri.AbsoluteUri.ToString();
            }
            //<link rel=\"stylesheet\" type=\"text/css\" href=\"" + path + "\" />
            return "<html><head><link rel=\"stylesheet\" type=\"text/css\" href=\"" + path + "\" /></head><body id=\"main\"><script>function updateHtml(html){ try { document.getElementById('main').innerHTML=html; } catch (e) { } } function ScrollWindow(val){ window.scrollTo(0, val * (document.body.scrollHeight - document.body.clientHeight)); }</script></body></html>";
        }
    }
}
