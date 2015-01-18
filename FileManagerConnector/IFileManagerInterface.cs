using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerConnector
{
    public class FileItem
    {
        public string Url { get; set; }
        public string Thumb { get; set; }

        public FileItem(string url, string thumb)
        {
            Url = url;
            Thumb = thumb;
        }
    }
    /// <summary>
    /// Acts as an interface for generating a file manager
    /// </summary>
    public interface IFileManagerInterface
    {
        /// <summary>
        /// The name of the file manager. Will be used when adding to the main
        /// MarkDown application.
        /// </summary>
        string Name { get; }
        bool AllowUpload { get; }
        bool HasConfig { get; }
        bool CanListFiles { get; }

        /// <summary>
        /// Retreive a list of files and return with the thumb and url.
        /// </summary>
        /// <returns></returns>
        List<FileItem> GetFiles();

        /// <summary>
        /// Upload a file.
        /// </summary>
        void Upload();

        /// <summary>
        /// Display settings dialog or similar. Will only be able to be called if HasConfig is
        /// set to true but must be defined even if it does nothing.
        /// </summary>
        void Settings();
        
    }
}
