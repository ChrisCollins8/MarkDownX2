using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkDownX2.GUI.Forms;
using MarkdownDeep;
using ScintillaNET;
using System.Threading;
using MarkDownX2.ParserFramework;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
namespace MarkDownX2.Helpers
{
    public class Previewer{
        private string Source = "";
        private FormPreview PreviewForm;        

        private float ScrollPercentage;
        public Previewer(string source, FormPreview previewForm, float scrollPercentage)
        {
            Source = source;
            ScrollPercentage = scrollPercentage;
            PreviewForm = previewForm;
        }

        public void Update()
        {
            Markdown markDown = new Markdown();
            PreviewForm.RenderHtml(markDown.Transform(Source), ScrollPercentage);
        }
    }
    public static partial class PreviewHelper
    {
        private static FormPreview previewForm = null;
        private static Markdown markDown = new Markdown();
        public static List<IMarkdownParser> Parsers = new List<IMarkdownParser>();
        public static IMarkdownParser CurrentParser = null;

        private static Thread UpdateThread = null;

        public static void Initialize()
        {
            string parserPath = PathHelper.ParserPath;
            if (Directory.Exists(parserPath))
            {
                string[] Files = Directory.GetFiles(parserPath, "*.dll");
                if (Files.Length > 0)
                {
                    
                    foreach (string file in Files)
                    {
                        Type ObjType = null;
                        try
                        {
                            // Try to load it up
                            Assembly nAssembly = null;
                            nAssembly = Assembly.Load(File.ReadAllBytes(file));
                            if (nAssembly != null)
                            {
                                string parserType = Path.GetFileName(file).Replace(".dll", "") + ".Parser";
                                ObjType = nAssembly.GetType(parserType);
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionHelper.Process(ex);
                        }
                        if (ObjType != null)
                        {
                            try
                            {
                                IMarkdownParser parser = (IMarkdownParser)Activator.CreateInstance(ObjType);
                                Parsers.Add(parser);
                            }
                            catch (Exception ex)
                            {
                                ExceptionHelper.Process(ex);
                            }
                        }
                    }
                    if (Parsers.Count == 0)
                    {
                        MessageBox.Show("MarkDownX2 was unable to find any installed parsers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    CurrentParser = FindParserByName("Standard Markdown Parser");
                    return;
                }
            }
            MessageBox.Show("MarkDownX2 was unable to find any installed parsers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static IMarkdownParser FindParserByName(string name)
        {
            foreach (IMarkdownParser parser in Parsers)
            {
                if (parser.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return parser;
                }
            }
            return null;
        }

        public static FormPreview PreviewForm
        {
            get
            {
                if (previewForm == null)
                {
                    previewForm = new FormPreview();
                }
                return previewForm;
            }
        }

        public static void UpdateHtml(string source, float scrollPercentage)
        {
            previewForm.RenderHtml(CurrentParser.Parse(source), scrollPercentage);
            //if (UpdateThread == null || !UpdateThread.IsAlive)
            //{
            //    previewForm.RenderHtml(CurrentParser.Parse(source), scrollPercentage);
            //    Previewer preview = new Previewer(source, previewForm, scrollPercentage);
            //    UpdateThread = new Thread(preview.Update);
            //    UpdateThread.Start();
            //}
        }

        public static void SetScroll(float pos)
        {
            previewForm.ScrollPoint(pos);
        }

        public static string GetMarkdown(string source)
        {
            return CurrentParser.Parse(source); // markDown.Transform(source);
        }

        public static void CopyPreviewContents(){
            previewForm.CopyBrowserContent();
        }
    }
}
