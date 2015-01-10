using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkDownX2.ParserFramework;

namespace StandardMarkDown
{
    public class Parser : IMarkdownParser
    {
        public string Name
        {
            get
            {
                return "Standard Markdown Parser";
            }
        }

        public string Parse(string source)
        {
            MarkdownDeep.Markdown markDown = new MarkdownDeep.Markdown();
            markDown.ExtraMode = false;
            return markDown.Transform(source);
        }
    }
}
