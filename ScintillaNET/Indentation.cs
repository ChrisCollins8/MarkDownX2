#region Using Directives

using System;
using System.ComponentModel;
using ScintillaNET.Extensions;
#endregion Using Directives


namespace ScintillaNET
{
    [TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class Indentation : TopLevelHelper
    {
        #region Fields

        /// <summary>
        ///     Enables the Smart Indenter so that On enter, it indents the next line.
        /// </summary>
        private SmartIndent _smartIndentType = SmartIndent.None;

        /// <summary>
        ///     For Custom Smart Indenting, assign a handler to this delegate property.
        /// </summary>
        public EventHandler<CharAddedEventArgs> SmartIndentCustomAction;

        #endregion Fields


        #region Methods

        internal bool IsWhiteSpace(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != '\t' && text[i] != '\n' && text[i] != '\r' && text[i] != '\0' && text[i] != ' ')
                {
                    return false;
                }
                
            }
            return true;
        }

        /// <summary>
        ///     If Smart Indenting is enabled, this delegate will be added to the CharAdded multicast event.
        /// </summary>
        internal void CheckSmartIndent(char ch)
        {
            char newline = (Scintilla.EndOfLine.Mode == EndOfLineMode.CR) ? '\r' : '\n';

            switch (SmartIndentType)
            {
                case SmartIndent.None:
                    return;
                case SmartIndent.Simple:
                    if (ch == newline)
                    {
                        Line curLine = Scintilla.Lines.Current;
                        curLine.Indentation = curLine.Previous.Indentation;
                        if (IsWhiteSpace(curLine.Text))
                        {
                            Scintilla.CurrentPos = curLine.IndentPosition;
                        }
                    }
                    break;
                case SmartIndent.XML:
                    
                    if (ch == '}' || ch == ')' || ch == ']')
                    {
                        int styleBit = getPrevStyle(Scintilla.CurrentPos);
                        if (handleWithCpp(styleBit))
                            handleCpp(ch, newline);
                    }
                    if (ch == newline)
                    {
                        int cpos = Scintilla.CurrentPos;
                        int origPos = cpos;
                        if (Scintilla.EndOfLine.Mode == EndOfLineMode.Crlf)
                        {
                            cpos -= 2;
                        }
                        else
                        {
                            cpos -= 1;
                        }

                        int styleBit = getPrevStyle(cpos);




                        if (handleWithCpp(styleBit))
                        {
                            handleCpp(ch, newline);
                        }
                        else
                        {

                            
                            
                            Line curLine = Scintilla.Lines.FromPosition(cpos);
                            string line = curLine.Text;
                            int col = Scintilla.GetColumn(cpos);
                            int i = 0;
                            int openCnt = 0;
                            int closeCount = 0;
                            bool inTag = false;
                            string tagName = "";
                            bool buildingTag = false;
                            while (i < col && i < line.Length)
                            {
                                if (line[i] == '<')
                                {
                                    
                                    if (curLine.Length > i)
                                    {
                                        if (line[i + 1] != '/' && line[i + 1] != '!')
                                        {
                                            buildingTag = true;
                                            inTag = true;
                                        }
                                        else
                                        {
                                            closeCount++;
                                            inTag = false;
                                        }
                                    }
                                }


                                if (inTag && line[i] != '<')
                                {
                                    if (line[i].IsWordChar() && buildingTag)
                                    {
                                        tagName += line[i];
                                    }

                                    if (!line[i].IsWordChar())
                                    {
                                        buildingTag = false;
                                    }
                                       

                                    if (line[i] == '>' && inTag)
                                    {
                                        if (i > 0)
                                        {
                                            if (line[i - 1] != '/')
                                            {
                                                // Ignore br. May be others to ignore.
                                                if (tagName.ToLowerInvariant() != "br")
                                                {
                                                    openCnt++;
                                                }
                                                inTag = false;
                                                
                                            }
                                        }
                                    }

                                }
                                i++;
                            }

                            if (openCnt > closeCount)
                            {
                                cpos = Scintilla.CurrentPos;
                                string lnBreak = "";
                                if (Scintilla.EndOfLine.Mode == EndOfLineMode.CR)
                                {
                                    lnBreak = "\r";
                                }
                                else if (Scintilla.EndOfLine.Mode == EndOfLineMode.LF)
                                {
                                    lnBreak = "\n";
                                }
                                else
                                {
                                    lnBreak = "\r\n";
                                }
                                Scintilla.UndoRedo.BeginUndoAction();
                                // Grab the current line indent
                                int lineIndent = curLine.Indentation;

                                // add in a line break. This also puts cursor on the next line but only
                                // if auto closing html brackets.
                                if (Scintilla.AutoClose.AutoCloseHtmlTags)
                                    Scintilla.Selection.Text = lnBreak;

                                // Indent the next line back to the current line.
                                Scintilla.Lines.Current.Indentation = curLine.Indentation;

                                // Jump back to the original position (The new line which we want indented further
                                Scintilla.Selection.Start = origPos;
                                Scintilla.Selection.End = origPos;

                                // Set indentation.
                                Scintilla.Lines.Current.Indentation = curLine.Indentation + TabWidth;
                                Scintilla.CurrentPos = Scintilla.Lines.Current.EndPosition;
                                Scintilla.UndoRedo.EndUndoAction();
                            }
                            else
                            {
                                // maintain indentation
                                Scintilla.UndoRedo.BeginUndoAction();
                                Scintilla.Lines.Current.Indentation = curLine.Indentation;
                                Scintilla.CurrentPos = Scintilla.Lines.Current.EndPosition;
                                Scintilla.UndoRedo.EndUndoAction();
                            }
                        }
                    }
                    if (ch == '>')
                    {
                        XmlMatchedTagsPos xmlTag = new XmlMatchedTagsPos();
                        if (MatchingTag.getXmlMatchedTagsPos(Scintilla, ref xmlTag, true))
                        {
                            int indentSize = Scintilla.NativeInterface.GetLineIndentation(Scintilla.NativeInterface.LineFromPosition(xmlTag.tagOpenStart));
                            Scintilla.NativeInterface.SetLineIndentation(Scintilla.Lines.Current.Number, indentSize);
                        }
                    }
                    break;
                case SmartIndent.CPP:
                case SmartIndent.CPP2:
                    handleCpp(ch, newline);
                    break;
            }
        }

        private bool handleWithCpp(int styleBit)
        {
            return ((styleBit >= 118 && styleBit < 128) || (styleBit >= 40 && styleBit < 61));
        }

        private int getPrevStyle(int pos)
        {
            int styleBit = 0;


            for (int i = pos; i > 0; i--)
            {
                char tmpCh = Scintilla.NativeInterface.GetCharAt(i);
                if (tmpCh != '\0' && tmpCh != '\n' && tmpCh != '\r' && tmpCh != '}' && tmpCh != '>' &&
                    tmpCh != '{' && tmpCh != ')' && tmpCh != ']')
                {
                    styleBit = Scintilla.NativeInterface.GetStyleAt(i);
                    break;
                }
            }
            return styleBit;
        }


        private void handleCpp(char ch, char newline)
        {
            string lnBreak = "";
                                if (Scintilla.EndOfLine.Mode == EndOfLineMode.CR)
                                {
                                    lnBreak = "\r";
                                }
                                else if (Scintilla.EndOfLine.Mode == EndOfLineMode.LF)
                                {
                                    lnBreak = "\n";
                                }
                                else
                                {
                                    lnBreak = "\r\n";
                                }
            if (ch == newline)
            {
                Line curLine = Scintilla.Lines.Current;
                Line tempLine = curLine;
                int previousIndent;
                string tempText;

                do
                {
                    tempLine = tempLine.Previous;
                    previousIndent = tempLine.Indentation;
                    tempText = tempLine.Text.Trim();
                    if (tempText.Length == 0) previousIndent = -1;
                }
                while ((tempLine.Number > 1) && (previousIndent < 0));

                bool addLineBreak = false;
                

                Scintilla.Lines.Current.Indentation = previousIndent;

                if (tempText.EndsWith("{") || tempText.EndsWith("(") || tempText.EndsWith("["))
                {
                    if (Scintilla.AutoClose.AutoCloseBraces)
                        Scintilla.Selection.Text = lnBreak;
                    int bracePos = Scintilla.CurrentPos - 1;
                    while (bracePos > 0 && Scintilla.CharAt(bracePos) != ch) bracePos--;
                    //if (bracePos > 0 && Scintilla.Styles.GetStyleAt(bracePos) == 10)
                    previousIndent += TabWidth;
                }
               
                curLine.Indentation = previousIndent;
                
                Scintilla.CurrentPos = curLine.IndentPosition;
            }
            else if (ch == '}' || ch == ']' || ch == ')')
            {
                int position = Scintilla.CurrentPos;
                Line curLine = Scintilla.Lines.Current;
                int previousIndent = curLine.Previous.Indentation;
                int match = Scintilla.SafeBraceMatch(position - 1);
                if (match != -1)
                {
                    previousIndent = Scintilla.Lines.FromPosition(match).Indentation;
                    curLine.Indentation = previousIndent;
                }
            }
        }



        /// <summary>
        ///     Smart Indenting helper method
        /// </summary>
        private void IndentLine(int line, int indent)
        {
            if (indent < 0)
            {
                return;
            }

            int selStart = Scintilla.Selection.Start;
            int selEnd = Scintilla.Selection.End;

            Line l = Scintilla.Lines[line];
            int posBefore = l.IndentPosition;
            l.Indentation = indent;

            int posAfter = l.IndentPosition;
            int posDifference = posAfter - posBefore;

            if (posAfter > posBefore)
            {
                // Move selection on
                if (selStart >= posBefore)
                {
                    selStart += posDifference;
                }

                if (selEnd >= posBefore)
                {
                    selEnd += posDifference;
                }
            }
            else if (posAfter < posBefore)
            {
                // Move selection back
                if (selStart >= posAfter)
                {
                    if (selStart >= posBefore)
                        selStart += posDifference;
                    else
                        selStart = posAfter;
                }
                if (selEnd >= posAfter)
                {
                    if (selEnd >= posBefore)
                        selEnd += posDifference;
                    else
                        selEnd = posAfter;
                }
            }

            Scintilla.Selection.Start = selStart;
            Scintilla.Selection.End = selEnd;
        }


        private void ResetBackspaceUnindents()
        {
            BackspaceUnindents = false;
        }


        private void ResetIndentWidth()
        {
            IndentWidth = 0;
        }


        private void ResetShowGuides()
        {
            ShowGuides = false;
        }


        private void ResetSmartIndentType()
        {
            _smartIndentType = SmartIndent.None;
        }


        private void ResetTabIndents()
        {
            TabIndents = false;
        }


        private void ResetTabWidth()
        {
            TabWidth = 8;
        }


        private void ResetUseTabs()
        {
            UseTabs = true;
        }


        internal bool ShouldSerialize()
        {
            return ShouldSerializeBackspaceUnindents() ||
                ShouldSerializeIndentWidth() ||
                ShouldSerializeShowGuides() || 
                ShouldSerializeTabIndents() ||
                ShouldSerializeTabWidth() ||
                ShouldSerializeUseTabs();
        }


        private bool ShouldSerializeBackspaceUnindents()
        {
            return BackspaceUnindents;

        }


        private bool ShouldSerializeIndentWidth()
        {
            return IndentWidth != 0;
        }


        private bool ShouldSerializeShowGuides()
        {
            return ShowGuides;
        }


        private bool ShouldSerializeSmartIndentType()
        {
            return _smartIndentType != SmartIndent.None;
        }


        private bool ShouldSerializeTabIndents()
        {
            return !TabIndents;
        }


        private bool ShouldSerializeTabWidth()
        {
            return TabWidth != 8;
        }


        private bool ShouldSerializeUseTabs()
        {
            return !UseTabs;
        }

        #endregion Methods


        #region Properties

        public bool BackspaceUnindents
        {
            get
            {
                return NativeScintilla.GetBackSpaceUnIndents();
            }
            set
            {
                NativeScintilla.SetBackSpaceUnIndents(value);
            }
        }


        public int IndentWidth
        {
            get
            {
                return NativeScintilla.GetIndent();
            }
            set
            {
                NativeScintilla.SetIndent(value);
            }
        }


        public bool ShowGuides
        {
            get
            {
                return NativeScintilla.GetIndentationGuides();
            }
            set
            {
                NativeScintilla.SetIndentationGuides(value);
            }
        }


        public SmartIndent SmartIndentType
        {
            get { return _smartIndentType; }
            set
            {
                _smartIndentType = value;
            }
        }


        public bool TabIndents
        {
            get
            {
                return NativeScintilla.GetTabIndents();
            }
            set
            {
                NativeScintilla.SetTabIndents(value);
            }
        }


        public int TabWidth
        {
            get
            {
                return NativeScintilla.GetTabWidth();
            }
            set
            {
                NativeScintilla.SetTabWidth(value);
            }
        }


        public bool UseTabs
        {
            get
            {
                return NativeScintilla.GetUseTabs();
            }
            set
            {
                NativeScintilla.SetUseTabs(value);
            }
        }

        #endregion Properties


        #region Constructors

        internal Indentation(Scintilla scintilla) : base(scintilla) { }

        #endregion Constructors
    }
}
