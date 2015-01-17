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
        private string result = null;
        public FormPreview()
        {
            InitializeComponent();
        }

        private void FormPreview_Load(object sender, EventArgs e)
        {
            string Html = "<html><head><style>" + File.ReadAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "StyleSheets", "github.css")) + "</style></head><body><span id=\"main\"></span><script>function updateHtml(html){ try { document.getElementById('main').innerHTML=html; } catch (e) { } } function ScrollWindow(val){ window.scrollTo(0, val * (document.body.scrollHeight - document.body.clientHeight)); }</script></body></html>";

            browser.LoadHTML(Html);
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
            string javaScript = "";
            if (scrollPoint > 0)
            {
                javaScript = String.Format("<script>window.scrollTo(0, {0} * (document.body.scrollHeight - document.body.clientHeight));</script>", scrollPoint.ToString());
            }
            //Html = "<html><head><style>" + File.ReadAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "StyleSheets", "github.css")) + "</style></head><body>" + Html + "</body></html>";
            //File.WriteAllText("test.html", Html);

            //browser.ExecuteJavascript(String.Format("updateHtml('{0}')", Html.Replace("'", "\\'")));
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
    }
}
