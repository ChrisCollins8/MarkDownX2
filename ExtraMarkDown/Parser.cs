using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkDownX2.ParserFramework;

namespace ExtraMarkDown
{
    public class Parser : IMarkdownParser
    {
        public string Name
        {
            get
            {
                return "Markdown with Extended Syntax";
            }
        }

        public string Parse(string source)
        {
            MarkdownDeep.Markdown markDown = new MarkdownDeep.Markdown();
            markDown.ExtraMode = true;
            return markDown.Transform(source);
        }
    }
}
