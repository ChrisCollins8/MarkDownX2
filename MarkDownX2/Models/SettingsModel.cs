using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkDownX2.Models
{
    public class SettingsModel
    {
        private Guid? _Id = null;
        public Guid Id
        {
            get
            {
                if (_Id == null)
                {
                    _Id = Guid.NewGuid();
                }
                return (Guid)_Id;
            }
            set
            {
                _Id = value;
            }
        }

        private bool _FullScreen = false;
        /// <summary>
        /// If app should run in full screen mode or not.
        /// </summary>
        public bool FullScreen
        {
            get
            {
                return _FullScreen;
            }
            set
            {
                _FullScreen = value;
            }
        }

        private bool _DisplayLineNumbers = true;
        /// <summary>
        /// If line numbers should be displayed.
        /// </summary>
        public bool DisplayLineNumbers
        {
            get
            {
                return _DisplayLineNumbers;
            }
            set
            {
                _DisplayLineNumbers = value;
            }
        }

        private bool _DisplayFormattingMarks = false;
        /// <summary>
        /// iF editor should display formatting marks (space, tab, enter, etc)
        /// </summary>
        public bool DisplayFormattingMarks
        {
            get
            {
                return _DisplayFormattingMarks;
            }
            set
            {
                _DisplayFormattingMarks = value;
            }
        }

        private bool _WordWrap = true;
        /// <summary>
        /// Enables or disables wordwrap on the editor.
        /// </summary>
        public bool WordWrap
        {
            get
            {
                return _WordWrap;
            }
            set
            {
                _WordWrap = value;
            }
        }

        private bool _SyntaxHighlighting = true;
        /// <summary>
        /// Enables or disables SyntaxHighlighting in the editor
        /// </summary>
        public bool SyntaxHighlighting
        {
            get
            {
                return _SyntaxHighlighting;
            }
            set
            {
                _SyntaxHighlighting = value;
            }
        }

        private int _ShowColumnGuide = 0;
        /// <summary>
        /// Enables or disables column guide (Right bar, usually gray or similar) typically
        /// spaced at around 80 characters.
        /// </summary>
        public int ShowColumnGuide
        {
            get
            {
                return _ShowColumnGuide;
            }
            set
            {
                _ShowColumnGuide = value;
            }
        }

        private bool _Toolbar = true;
        /// <summary>
        /// Hide or show the toolbar
        /// </summary>
        public bool Toolbar
        {
            get
            {
                return _Toolbar;
            }
            set
            {
                _Toolbar = value;
            }
        }

        private bool _StatusBar = true;
        /// <summary>
        /// Hide or display the status bar.
        /// </summary>
        public bool StatusBar
        {
            get
            {
                return _StatusBar;
            }
            set
            {
                _StatusBar = value;
            }
        }

        private string _DefaultCss = "github.css";
        /// <summary>
        /// Default stylesheet to use.
        /// </summary>
        public string DefaultCss
        {
            get
            {
                return _DefaultCss;
            }
            set
            {
                _DefaultCss = value;
            }
        }

    }
}
