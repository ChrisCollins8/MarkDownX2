using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerConnector;
using JoeBlogs;


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

        public void Execute(){
            WordPressWrapper wrapper = new WordPressWrapper("http://www.devnotepad.com/xmlrpc.php",
                "devnotepad", "j!>!^C9blCi%A(H;u8uq181FH");

            wrapper.UploadFile(new Data()
            {
                Name = "beach.jpg",
                Bits = System.IO.File.ReadAllBytes(@"c:\users\stewart\beach.jpg"),
                Overwrite = true,
                Type = "image/jpeg"
            });

            //wrapper.
            //WordPressSiteConfig config = new WordPressSiteConfig(){
            //    BaseUrl = "http://www.devnotepad.com",
            //    BlogId = 1,
            //    Password = "j!>!^C9blCi%A(H;u8uq181FH",
            //    Username = "devnotepad"
            //};
            //WordPressClient client = new WordPressClient(config);
            //string file = @"c:\users\stewart\beach.jpg";
            //string name = Path.GetFileName(file);

            //WordPressSharp.Models.MediaUpload upload = new WordPressSharp.Models.MediaUpload(config){
            //    data = new WordPressSharp.Models.Data(name, "image/jpeg", File.ReadAllBytes(file), true)
            //};

            //string result = client.NewUpload(upload);
            //Console.WriteLine(result);
            //client.GetMediaItems().
        }
    }
}
