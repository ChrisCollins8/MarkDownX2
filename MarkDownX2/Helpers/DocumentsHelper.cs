using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkDownX2.GUI.Forms;

namespace MarkDownX2.Helpers
{
    public static partial class DocumentsHelper
    {
        private static List<FormDocument> Documents = new List<FormDocument>();

        public static int ActiveDocs {
            get
            {
                return Documents.Count;
            }
        }

        public static bool NewDocument(FormMain mainForm)
        {
            FormDocument newDoc = CreateDoc(mainForm);
            newDoc.Text = String.Format("New Document {0}", (Documents.Count + 1));
            newDoc.TabText = newDoc.Text;
            Documents.Add(newDoc);
            newDoc.Show(mainForm.DockingPanel);
            
            mainForm.Refresh();
            return true;
        }

        /// <summary>
        /// Create new FormDocument object. Takes care of handling some of the universal settings.
        /// </summary>
        /// <returns></returns>
        private static FormDocument CreateDoc(FormMain mainForm, string fileName = "")
        {
            FormDocument frmDoc = new FormDocument(DefaultsHelper.DefaultSyntax, mainForm, fileName);
            //frmDoc.TabPageContextMenuStrip = Global.FormMain.MenuDocs;
            return frmDoc;
        }

        private static string[] GetFiles()
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Multiselect = true;
                openDialog.Filter = "Markdown Files (*.md)|*.md|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    return openDialog.FileNames;
                }
            }
            return null;
        }

        /// <summary>
        /// Opens filename
        /// </summary>
        /// <param name="fileName"></param>
        public static void OpenFile(FormMain mainForm, string fileName)
        {
            if (!String.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                FormDocument newDoc = CreateDoc(mainForm, fileName);
                newDoc.Text = Path.GetFileName(fileName);
                newDoc.Icon = Icon.ExtractAssociatedIcon(fileName);
                newDoc.TabText = newDoc.Text;
                Documents.Add(newDoc);
                newDoc.Show(mainForm.DockingPanel);
            }
            mainForm.Refresh();
        }

        /// <summary>
        /// Opens 1 or more files.
        /// </summary>
        public static void Open(FormMain mainForm)
        {
            string[] files = GetFiles();
            if (files != null)
            {
                foreach (string file in files)
                {
                    OpenFile(mainForm, file);
                }
            }
        }

    }
}
