using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkDownX2.Helpers
{
    public static class TextHelper
    {
        public static string GetFormattedText(string text)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            int lineCnt = 1;

            for (int i = 0; i < text.Length; i++)
            {
                
                if (text[i] == '\r')
                {
                    lineCnt++;
                    builder.Append(String.Format(" <br id=\"{0}\">", lineCnt));
                }
                builder.Append(text[i]);
            }
            return builder.ToString();
        }
    }
}
