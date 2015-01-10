using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkDownX2.Models
{
    public class Style
    {
        public string Name { get; set; }

        private Color _ForeColor = Color.Black;
        /// <summary>
        /// Set foreground color for the defined style.
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
            }
        }

        private Color _BackColor = Color.White;
        /// <summary>
        /// Set the background color for the defined style.
        /// </summary>
        public Color BackColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                _BackColor = value;
            }
        }

        private bool _Bold = false;
        /// <summary>
        /// Set bold for the defined style.
        /// </summary>
        public bool Bold
        {
            get
            {
                return _Bold;
            }
            set
            {
                _Bold = value;
            }
        }

        private bool _Italic = false;
        /// <summary>
        /// Set italic for the defined style.
        /// </summary>
        public bool Italic
        {
            get
            {
                return _Italic;
            }
            set
            {
                _Italic = value;
            }
        }

        private bool _Underline = false;
        /// <summary>
        /// Set underline for the defined style.
        /// </summary>
        public bool Underline
        {
            get
            {
                return _Underline;
            }
            set
            {
                _Underline = value;
            }
        }

        private bool _Strike = false;
        /// <summary>
        /// Set strikethrough for the defined style.
        /// </summary>
        public bool Strike
        {
            get
            {
                return _Strike;
            }
            set
            {
                _Strike = value;
            }
        }

        private string _FontName = "Consolas";
        /// <summary>
        /// Set the font name for the defined style.
        /// </summary>
        public string FontName
        {
            get
            {
                return _FontName;
            }
            set
            {
                _FontName = value;
            }
        }

        private int _FontSize = 12;
        /// <summary>
        /// Fontsize defines the font size.
        /// </summary>
        public int FontSize
        {
            get
            {
                return _FontSize;
            }
            set
            {
                _FontSize = value;
            }
        }

        private bool _FillEOL = false;
        public bool FillEOL
        {
            get
            {
                return _FillEOL;
            }
            set
            {
                _FillEOL = value;
            }
        }
       
        /// <summary>
        /// Key value matching the key defined in MarkdownLexer.cs
        /// </summary>
        public int Key { get; set; }
    }
    public class Syntax
    {

        public List<Style> Styles { get; set; }

    }
    
}
