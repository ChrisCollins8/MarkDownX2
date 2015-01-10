using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkDownX2.GUI.Forms;
using MarkDownX2.Helpers;

namespace MarkDownX2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //WordPressFileManager.FileManager fl = new WordPressFileManager.FileManager();
            //fl.Execute();

            InitParsers();

            Application.Run(new FormMain());
        }

        private static void InitParsers()
        {
            PreviewHelper.Initialize();
        }
    }
}
