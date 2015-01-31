using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkDownX2.Models
{
    /// <summary>
    /// Generic result object to obtain the results.
    /// </summary>
    public class FileResult
    {
        public string ThumbUrl { get; set; }
        public string DisplayName { get; set; }
        public string FullUrl { get; set; }
        public Image image { get; set; }

        public FileResult(string thumbUrl, string fullUrl)
        {
            ThumbUrl = thumbUrl;
            FullUrl = fullUrl;
            Uri uri = new Uri(FullUrl);
            DisplayName = Path.GetFileName(uri.LocalPath);
            GetImage();
        }

        private void GetImage()
        {
            image = LoadImage(ThumbUrl);
        }

        private Image LoadImage(string url)
        {

            System.Net.WebRequest request = System.Net.WebRequest.Create(url);

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();

            Bitmap bmp = new Bitmap(responseStream);

            responseStream.Dispose();

            return bmp;
        }
    }
}
