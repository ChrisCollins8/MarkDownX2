using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerConnector;
using WordPressSharp;


namespace WordPressFileManager
{
    public class FileManager : IFileManagerInterface
    {
        public string Name
        {
            get
            {
                return "Wordpress Filemanager";
            }
        }

        public bool AllowUpload
        {
            get
            {
                return true;
            }
        }
        public bool HasConfig
        {
            get
            {
                return true;
            }

        }
        public bool CanListFiles
        {
            get
            {
                return true;
            }
        }

        public List<FileItem> GetFiles()
        {
            return new List<FileItem>();
        }

        public void Upload()
        {

        }

        public void Settings()
        {

        }

        
    }
}
