using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Awesomium.Core;
using MarkDownX2.Helpers;

namespace MarkDownX2.GUI.Forms
{
    public partial class FormPreview : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private ManualResetEvent sync = new ManualResetEvent(false);
        private float LastScrollPoint = 0;
        private string LastHtml = "";
        private string result = null;
        public FormPreview()
        {
            InitializeComponent();

            browser.WebSession = WebCore.CreateWebSession(new WebPreferences()
            {
                FileAccessFromFileURL = true,
                UniversalAccessFromFileURL = true
            });
            WebSesession.DataPath = PathHelper.StyleSheetPath;
        }

        private void FormPreview_Load(object sender, EventArgs e)
        {
            //string Html = "<html><head><style>" + File.ReadAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "StyleSheets", "github.css")) + "</style></head><body><span id=\"main\"></span><script>function updateHtml(html){ try { document.getElementById('main').innerHTML=html; } catch (e) { } } function ScrollWindow(val){ window.scrollTo(0, val * (document.body.scrollHeight - document.body.clientHeight)); }</script></body></html>";
            LoadHtml();
            SetupMenu();


        }

        private void SetupMenu()
        {
            foreach (string name in HtmlHelper.StyleSheets.Keys)
            {                              
                PreviewMenu.Items.Add(name, null, item_Click);
            }
        }
        public void LoadHtml(string name = ""){

            if (!String.IsNullOrEmpty(name))
            {
                GlobalSettings.Settings.DefaultCss = name;
            }

            
            HtmlHelper.SetStylesheet(name);

            if (File.Exists(HtmlHelper.currentCss))
            {
            //    browser.WebSession = WebCore.CreateWebSession(new WebPreferences()
            //{
            //    FileAccessFromFileURL = true,
            //    UniversalAccessFromFileURL = true,
            //    CustomCSS = File.ReadAllText(HtmlHelper.currentCss)
            //});
                //browser.WebSession.Preferences.
            }

            string Html = HtmlHelper.GetHtml();
            browser.LoadHTML(Html);
            if (!String.IsNullOrEmpty(LastHtml))
            {
                RenderHtml(LastHtml, LastScrollPoint);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem item = ((System.Windows.Forms.ToolStripMenuItem)sender);

            if (item != null)
            {
                LoadHtml(item.Text);
            }
            
        }

        public void CopyBrowserContent()
        {
            SuspendUpdate.Suspend(browser);

            browser.SelectAll();
            browser.Copy();
            //browser.StopFind(true);
            browser.ExecuteJavascript("window.getSelection().empty();"); 

            SuspendUpdate.Resume();


        }

        public void ScrollPoint(float pos)
        {
            JSObject div = browser.ExecuteJavascriptWithResult("window");
            div.Invoke("ScrollWindow", pos);
        }

        public void RenderHtml(string Html, float scrollPoint)
        {
            LastHtml = Html;
            LastScrollPoint = scrollPoint;
            Invoke(new MethodInvoker(() =>
                {
                    JSObject runner = browser.ExecuteJavascriptWithResult("window");
                    
                    runner.Invoke("updateHtml", Html.Replace("'", "\\'"));
                }));
            

            //browser.LoadHTML(Html);
            //browser.DocumentText = Html;
        }

        private void Awesomium_Windows_Forms_WebControl_ShowJavascriptDialog(object sender, JavascriptDialogEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Awesomium_Windows_Forms_WebControl_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void FormPreview_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void Awesomium_Windows_Forms_WebControl_ShowContextMenu(object sender, ContextMenuEventArgs e)
        {

            PreviewMenu.Show(Cursor.Position);
            e.Handled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            File.WriteAllText(@"c:\users\stewart\desktop\rendered.html", browser.HTML);
        }
    }
}
