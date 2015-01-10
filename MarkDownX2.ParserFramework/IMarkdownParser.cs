using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkDownX2.ParserFramework
{
    public interface IMarkdownParser
    {
        string Name { get; }

        string Parse(string source);
    }
}
