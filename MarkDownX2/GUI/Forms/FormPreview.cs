using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Awesomium.Core;
using MarkDownX2.Helpers;

namespace MarkDownX2.GUI.Forms
{
    public partial class FormPreview : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FormPreview()
        {
            InitializeComponent();
        }

        private void FormPreview_Load(object sender, EventArgs e)
        {

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

        public void RenderHtml(string Html, float scrollPoint)
        {
            string javaScript = "";
            if (scrollPoint > 0)
            {
                javaScript = String.Format("<script>window.scrollTo(0, {0} * (document.body.scrollHeight - document.body.clientHeight));</script>", scrollPoint.ToString());
            }
            Html = "<html><head><style>" + File.ReadAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "StyleSheets", "github.css")) + "</style></head><body>" + Html + javaScript + "</body></html>";
            browser.LoadHTML(Html);
            //browser.DocumentText = Html;
        }
    }
}
