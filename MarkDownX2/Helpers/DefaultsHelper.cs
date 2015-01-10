using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkDownX2.Models;

namespace MarkDownX2.Helpers
{
    public static class DefaultsHelper
    {

        public static Syntax DefaultSyntax = new Syntax()
        {
            Styles = new List<Style>()
            {
                new Style(){
                    Name = "Default",
                    Key = 0,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    FontName = "Consolas",
                    FontSize = 11             
                },
                new Style(){
                    Name = "Italic",
                    Key = 7,
                    ForeColor = Color.Black,
                    Italic = true,
                    FontSize = 11
                },
                new Style(){
                    Name = "Bold",
                    Key = 8,
                    ForeColor = Color.Black,
                    Bold = true,
                    FontSize = 11
                },
                new Style(){
                    Name = "Bold/Italic",
                    Key = 9,
                    Bold = true,
                    ForeColor = Color.Black,
                    Italic = true,
                    FontSize = 11
                },
                
                new Style(){
                    Name = "Hyperlink",
                    Key = 10,
                    ForeColor = Color.Blue,
                    //Underline = true,
                    FontSize = 11
                },
                new Style(){
                    Name = "Quote",
                    Key = 11,
                    ForeColor = Color.Green,
                    //Underline = true,
                    FontSize = 11
                },
                new Style(){
                    Name = "Code",
                    Key = 12,
                    ForeColor = Color.Black,
                    FontSize = 11,
                    BackColor = Color.FromArgb(248,248,248),
                    FillEOL = true
                },
                new Style(){
                    Name = "Unordered List",
                    Key = 13,
                    ForeColor = Color.DarkGray,
                    FontSize = 11,
                },
                new Style(){
                    Name = "Ordered List",
                    Key = 14,
                    ForeColor = Color.DarkGray,
                    FontSize = 11,
                },
                new Style(){
                    Name = "Header 1",
                    Key = 1,
                    ForeColor = Color.FromArgb(178, 34, 34),
                    FontSize = 11
                },
                new Style(){
                    Name = "Header 2",
                    Key = 2,
                    ForeColor = Color.FromArgb(178, 34, 34),
                    FontSize = 11
                },
                new Style(){
                    Name = "Header 3",
                    Key = 3,
                    ForeColor = Color.FromArgb(178, 34, 34),
                    FontSize = 11
                },
                new Style(){
                    Name = "Header 4",
                    Key = 4,
                    ForeColor = Color.FromArgb(178, 34, 34),
                    FontSize = 11
                },
                new Style(){
                    Name = "Header 5",
                    Key = 5,
                    ForeColor = Color.FromArgb(178, 34, 34),
                    FontSize = 11
                },
                new Style(){
                    Name = "Header 6",
                    Key = 6,
                    ForeColor = Color.FromArgb(178, 34, 34),
                    FontSize = 11
                },
                /*
                 *         // HTML Stuff
        Bracket = 20,
        Tag = 21,
        Attribute = 22,
        String = 23
                 * */
                new Style(){
                    Name = "HTML Bracket",
                    Key = 20,
                    ForeColor = Color.Red,
                    FontSize = 11
                },
                new Style(){
                    Name = "HTML Tag",
                    Key = 21,
                    ForeColor = Color.DarkGreen,
                    FontSize = 11
                },
                new Style(){
                    Name = "HTML Attribute",
                    Key = 22,
                    ForeColor = Color.Blue,
                    FontSize = 11
                },
                new Style(){
                    Name = "HTML String",
                    Key = 23,
                    ForeColor = Color.Maroon,
                    FontSize = 11
                }
                

            }
        };

        /// <summary>
        /// Defines the markdown key.
        /// </summary>
        public static int MarkdownKey = 98;

    }
}
