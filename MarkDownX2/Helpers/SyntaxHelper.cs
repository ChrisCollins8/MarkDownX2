using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScintillaNET;
using MarkDownX2.Extensions;

namespace MarkDownX2.Helpers
{
    public static class SyntaxHelper
    {
        /// <summary>
        /// Generic determination of if the cursor is currently inside of an html tag.
        /// </summary>
        /// <param name="Editor"></param>
        /// <returns></returns>
        public static bool InTag(this Scintilla Editor)
        {
            int cpos = Editor.CurrentPos;
            Line curLine = Editor.Lines.FromPosition(cpos);
            string line = curLine.Text;
            int col = Editor.GetColumn(cpos);
            int i = col;
            while (i > 0)
            {
                if (line[i] == '<')
                {
                    //for (int x = i; x < line.Length; x++)
                    //{
                    //    if (line[x] == '>')
                    //        return false;
                    //}
                    return true;
                }

                if (line[i] == '>')
                {
                    return false;
                }

                i--;
            }
            return false;
        }

    }
}
