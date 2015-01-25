using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManagerConnector;
using WordPressSharp;
using WordPressSharp.Models;


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

        public bool RequiresUsername
        {
            get
            {
                return true;
            }
        }

        public bool RequiresPassword
        {
            get
            {
                return true;
            }
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

        public FileManager()
        {
        }

        public List<FileItem> GetFiles()
        {
            
            List<FileItem> items = new List<FileItem>();
            WordPressClient client = new WordPressClient(new WordPressSiteConfig()
            {
                BaseUrl = Url,
                Username = UserName,
                Password = Password
            });

            MediaItem[] mediaItems = client.GetMediaItems(new MediaFilter()
            {
                Number = 500
            });
            foreach (MediaItem item in mediaItems)
            {
                items.Add(new FileItem(item.Link, item.Thumbnail));
            }
            return items;
        }

        public void Upload()
        {
            WordPressClient client = new WordPressClient(new WordPressSiteConfig()
            {
                BaseUrl = Url,
                Username = UserName,
                Password = Password
            });
            using(OpenFileDialog openDialog = new OpenFileDialog()){
                openDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif|*.jpg;*.jpeg;*.png;*.gif|All Files (*.*)|*.*";
                openDialog.Multiselect = true;
                if (openDialog.ShowDialog() == DialogResult.OK && openDialog.FileNames.Length > 0)
                {
                    foreach (string fileName in openDialog.FileNames)
                    {
                        try
                        {
                            client.UploadMedia(new Data(){
                                Name = Path.GetFileName(fileName),
                                Overwrite = true,
                                Type = "image/jpeg",
                                Bits = File.ReadAllBytes(fileName)
                            });
                        }
                        catch { }
                    }
                }
            }
        }

        public void Settings()
        {

        }

        
    }
}
