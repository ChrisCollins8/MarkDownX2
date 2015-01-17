using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScintillaNET;
using MarkDownX2.Extensions;
using System.Text.RegularExpressions;

namespace MarkDownX2.Lexers
{
    public enum MarkdownStyles
    {
        Default = 0,
        Header1 = 1,
        Header2 = 2,
        Header3 = 3,
        Header4 = 4,
        Header5 = 5,
        Header6 = 6,
        
        Italic = 7,
        Bold = 8,
        BoldItalic = 9,

        Link = 10,
        Quote = 11,

        Code = 12,

        // Lists
        UnorderedList = 13,
        OrderedList = 14,
        // HTML Stuff
        Bracket = 20,
        Tag = 21,
        Attribute = 22,
        String = 23
    }
    public class MarkdownLexer
    {

        private Scintilla scintilla;
        private Range range;
        private int Pos = 0;
        private string Text = "";
        private int Length = 0;
        private int StartPos = 0;
        private string WordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int _tabWidth = 4;
        //public static Regex LinkRegex = new Regex(@"\[\s*([a-zA-Z 0-9_-]+)\s*\]\s*\:?\s*(\S+( \""[^\""]*\"")?)+\s*("".*?"")?", RegexOptions.Compiled);
        public static Regex LinkRegex = new Regex(@"\[\s*([a-zA-Z 0-9:.""'(())/_-]+)\s*\]\s*\:?\s*(\S+( \""[^\""]*\"")?)+\s*("".*?"")?", RegexOptions.Compiled);
        public MarkdownLexer(Range _range, Scintilla _scintilla)
        {
            range = _range;
            scintilla = _scintilla;
            Length = (range.End - range.Start)+1;
            //Length = scintilla.Text.Lenght
            StartPos = range.Start;
            Text = scintilla.Text.Substring(range.Start, range.End - range.Start);
        }
        private char getChar()
        {
            if (Text.Length > Pos)
            {
                return Text[Pos];
            }
            return '\0';
        }

        private char nextChar(int add = 1)
        {
            if (Pos + add < Text.Length)
            {
                return Text[Pos + add];
            }
            return '\0';
        }

        private char prevChar(int sub = 1)
        {
            if (Pos - sub >= 0)
            {
                return Text[Pos - sub];
            }
            return '\0';
        }

        private void StyleChar(MarkdownStyles style)
        {
            SetStyle(style, 1);
        }

        private void SetStyle(MarkdownStyles style, int length)
        {
            if (length > 0)
            {
                ((INativeScintilla)scintilla).SetStyling(length, (int)style);
            }
        }

        private void markStyle(MarkdownStyles style)
        {
            scintilla.NativeInterface.SetStyling(Pos, (int)style);
        }

        private void getLineEnd()
        {
            // in the case of headers the marking goes to the very end of the line.
            while (getChar() != '\n' && getChar() != '\r' && getChar() != '\0' && Pos < Length)
            {
                Pos++;
            }
        }

        #region Headers

        private void processHeader()
        {
            // Must be the start of a line for a header.
            char prch = prevChar();
            if (Pos != 0 && prch != '\n' && prch != '\r' && prch != '\0')
            {
                StyleChar(MarkdownStyles.Default);
                return;
            }

            int cnt = 0;
            StartPos = Pos;
            while (getChar() == '#')
            {
                cnt++;
                Pos++;
            }
            switch (cnt)
            {
                case 1:
                    markHeader1();
                    break;
                case 2:
                    markHeader2();
                    break;
                case 3:
                    markHeader3();
                    break;
                case 4:
                    markHeader4();
                    break;
                case 5:
                    markHeader5();
                    break;
                case 6:
                    markHeader6();
                    break;
                default:
                    SetStyle(MarkdownStyles.Default, cnt);
                    break;
            }
        }

        private void markHeader1()
        {
            getLineEnd();
            SetStyle(MarkdownStyles.Header1, Pos-StartPos);
        }

        private void markHeader2()
        {
            getLineEnd();
            SetStyle(MarkdownStyles.Header2, Pos - StartPos);
        }

        private void markHeader3()
        {
            getLineEnd();
            SetStyle(MarkdownStyles.Header3, Pos - StartPos);
        }

        private void markHeader4()
        {
            getLineEnd();
            SetStyle(MarkdownStyles.Header4, Pos - StartPos);
        }

        private void markHeader5()
        {
            getLineEnd();
            SetStyle(MarkdownStyles.Header5, Pos - StartPos);
        }

        private void markHeader6()
        {
            getLineEnd();
            SetStyle(MarkdownStyles.Header6, Pos - StartPos);
        }


        #endregion

        private int findClose(string find)
        {
            if (String.IsNullOrEmpty(find))
            {
                return 0;
            }
            bool found = false;
            while (Pos < Length && !found && getChar() != '\n' && getChar() != '\r')
            {
                Pos++;
                if (getChar() == find[0])
                {
                    found = true;
                    for (int i = 1; i < find.Length; i++)
                    {
                        if (nextChar(i) != find[i])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        Pos += find.Length;
                        return Pos;
                    }
                }
            }
            return 0;
        }

        private void StartStyling(int startPos = -1)
        {
            if (startPos == -1)
                scintilla.NativeInterface.StartStyling(range.Start + Pos, 0x1F);
            else
                scintilla.NativeInterface.StartStyling(range.Start + startPos, 0x1F);
        }

        #region Ateriks *

        private void processAsterik()
        {
            // Must be the start of a line for a header.
            

            int cnt = 0;
            StartPos = Pos;
            while (getChar() == '*')
            {
                cnt++;
                Pos++;
            }
            switch (cnt)
            {
                case 1:
                    markItalic();
                    break;
                case 2:
                    markBold();
                    break;
                case 3:
                    markBoldItalic();
                    break;
                default:
                    SetStyle(MarkdownStyles.Default, cnt);
                    break;
            }
        }

        private void markItalic()
        {
            int end = findClose("*");
            if (end != 0)
            {
                SetStyle(MarkdownStyles.Italic, end - StartPos);
            }
        }

        private void markBold()
        {
            int end = findClose("**");
            if (end != 0)
            {
                SetStyle(MarkdownStyles.Bold, end - StartPos);
            }
        }

        private void markBoldItalic()
        {
            int end = findClose("***");
            if (end != 0)
            {
                SetStyle(MarkdownStyles.BoldItalic, end - StartPos);
            }
        }

        #endregion

        #region Html Processing

        private void processTag()
        {
            if (WordChars.Contains(getChar())){
                StartPos = Pos;
                StartStyling();
                while(WordChars.Contains(getChar()) && Pos < Length){
                    Pos++;
                }
                SetStyle(MarkdownStyles.Tag, Pos-StartPos);
                Pos--;
            }
        }

        private void processAttribute()
        {
            if (WordChars.Contains(getChar()))
            {
                StartPos = Pos;
                StartStyling();
                while (WordChars.Contains(getChar()) && Pos < Length)
                {
                    Pos++;
                }
                SetStyle(MarkdownStyles.Attribute, Pos - StartPos);
            }
        }

        private void processString()
        {
            if (getChar() == '"')
            {
                StartPos = Pos;
                StartStyling();
                Pos++;
                while (getChar() != '"' && Pos < Length)
                {
                    Pos++;
                }
                Pos++;
                SetStyle(MarkdownStyles.String, Pos - StartPos);
                Pos--;
            }
        }

        private void ColouriseInHtml(int endPos)
        {
            
        
            while (Pos < endPos)
            {
                
                StartStyling();
                switch (getChar())
                {
                    case '#':
                        processHeader();
                        break;
                    case '*':
                        if (nextChar() == ' ' || nextChar() == '\t')
                            StyleChar(MarkdownStyles.UnorderedList);
                        else
                            processAsterik();
                        break;
                    // HTML handling
                    case '<':
                        processHtml();
                        break;
                    case '>':
                        StyleChar(MarkdownStyles.Bracket);
                        break;
                    case '[':
                        processLink();
                        break;
                    case '`':
                        processQuote();
                        break;
                    
                    default:
                        StyleChar(MarkdownStyles.Default);
                        break;
                }
                Pos++;
            }


        
        }

        private void processHtml()
        {
            while (getChar() != '>' && Pos < Length)
            {
                if (getChar() == '<' && ((WordChars.Contains(nextChar())) || nextChar() == '/')){
                    StyleChar(MarkdownStyles.Bracket);
                    Pos++;
                    processTag();
                }
                else
                {
                    char ch = getChar();
                    if (ch == '"')
                    {
                        processString();
                    }
                    else if (WordChars.Contains(nextChar()) && prevChar() != '/')
                    {
                        processAttribute();
                    }
                    else if (WordChars.Contains(nextChar()) && prevChar() == '/')
                    {
                        processTag();
                    }
                    
                }
                Pos++;
            }
            Pos--;
            //XmlMatchedTagsPos xmlPos = new XmlMatchedTagsPos();
            //if (Pos > 0){
            //    MatchingTag.getXmlMatchedTagsPos(scintilla, ref xmlPos, false, Pos - 1);
            //    if (xmlPos.tagCloseEnd > 0)
            //    {
            //        ColouriseInHtml(xmlPos.tagCloseEnd);
            //    }
            //}
            //if (getChar() == '>')
            //{
                
            //    StartStyling();
            //    StyleChar(MarkdownStyles.Bracket);
            //}
        }

        #endregion

        #region Link processing

        private void processLink()
        {
            if (getChar() == '[')
            {
                StartPos = Pos;
                StartStyling();
                StringBuilder sb = new StringBuilder();
                while (getChar() != ')' && Pos < Length && getChar() != '\n' && getChar() != '\t'
                    && getChar() != '\r')
                {
                    sb.Append(getChar());
                    Pos++;
                }
                sb.Append(getChar());
                string textFind = sb.ToString();
                if (MarkdownLexer.LinkRegex.IsMatch(textFind))
                {
                    SetStyle(MarkdownStyles.Link, textFind.Length);
                }
            }
        }

        #endregion


        private void processQuote()
        {
            if (getChar() == '`')
            {
                StartPos = Pos;
                bool foundClose = false;
                StartStyling();
                Pos++;
                while (foundClose == false && getChar() != '\n' && getChar() != '\r' && getChar() != '\0' && Pos < Length)
                {
                    Pos++;
                    if (getChar() == '`')
                    {
                        foundClose = true;
                    }
                }
                if (foundClose)
                {
                    SetStyle(MarkdownStyles.Quote, Pos - StartPos);
                }

            }
        }

        private bool CheckInHtml(int startPos)
        {
            string innerText = scintilla.Text;
            for (int i = startPos; i >= 0; i--)
            {
                if (innerText[i] == '<')
                {
                    char ch = '\0';
                    if (i > 0)
                        ch = innerText[i - 1];
                    if (ch != '\t')
                    {
                        XmlMatchedTagsPos tagPos = new XmlMatchedTagsPos();
                        if (MatchingTag.getXmlMatchedTagsPos(scintilla, ref tagPos, false, i + 1))
                        {
                            if (tagPos.tagCloseStart >= startPos)
                                return true;
                        }

                    }
                }

            }
            return false;
        }

        private char nextNonTabChar(){
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] != '\t')
                {
                    return Text[i];
                }
            }
            return '\0';
        }

        private int lengthNonTabChar()
        {
            int cnt = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                cnt++;
                if (Text[i] != '\t')
                {
                    return cnt;
                }

            }
            return cnt;
        }

        /// <summary>
        /// Process a code block (Beginning with tab. Important, need to verify that
        /// it is not currently inside of an html block which would allow tabs)
        /// </summary>
        private void processCode()
        {

            StartPos = Pos;
            char ch = prevChar();
            // Deal with incorrect positioning 
            if (getChar() != '\t')
                return;
            if (ch == '\n' || ch == '\r' || ch == '\0')
            {

                
                
                char charNext = nextNonTabChar();
                // Block out lists, html or line ends.
                // The html blocking is technically incorrect for markdown. It should only apply if
                // currently in a valid Html tag but the performance impact was pretty significant when 
                // working on larger files and trying to figure out if the code was inside of html so
                // opted to treat all html as highlighted as html even if in code block. Rendering will
                // display it as code, however. This could be optimized in the future.
                if (charNext == '*' || charNext == '<' || charNext == '\r' || charNext == '\n' || charNext.IsNumeric() || charNext == '-')
                {
                    StartStyling();
                    SetStyle(MarkdownStyles.Default, lengthNonTabChar());
                    Pos = StartPos;
                    return;
                }

                
                    

                StringBuilder numTest = new StringBuilder();



                

                
                StartStyling();
                
                while (Pos < Length && getChar() != '\n' && getChar() != '\r' && getChar() != '\0' && !getChar().IsWordChar() && getChar() != '<')
                {
                    numTest.Append(getChar());
                    Pos++;
                }

                // This is to ensure the style extends to the end of the line.
                if (scintilla.EndOfLine.Mode == EndOfLineMode.CR || scintilla.EndOfLine.Mode == EndOfLineMode.LF)
                    Pos += 1;
                else
                    Pos += 2;
                if (numTest.ToString().Trim().IsNumberedList() == -1)
                    SetStyle(MarkdownStyles.Code, (Pos - StartPos));
                else
                {
                    // Need to reset back to the startpos
                    Pos = StartPos;
                }
                    
            }
        }

       

        private void processOrderedList()
        {
            char ch = prevChar();
            if (ch == '\n' || ch == '\0' || ch == '\r' || ch == '\t')
            {
                StartStyling(Pos-1);
                StartPos = Pos;
                StringBuilder numChar = new StringBuilder();
                while (getChar().IsNumeric() || getChar() == '\t')
                {
                    numChar.Append(getChar());
                    Pos++;
                }
                if (getChar() == '.' && numChar.Length > 0)
                {
                    numChar.Append('.');
                    if (nextChar() == ' ')
                    {
                        SetStyle(MarkdownStyles.OrderedList, numChar.Length+1);
                        numChar.Clear();
                    }
                }
            }
        }

        public void Colourise()
        {
            while (Pos < Length)
            {
                
                StartStyling();
                switch (getChar())
                {
                    
                    case '#':
                        processHeader();
                        break;
                    case '*':
                        if (nextChar() == ' ' || nextChar() == '\t')
                            StyleChar(MarkdownStyles.UnorderedList);
                        else
                            processAsterik();
                        break;
                    // HTML handling
                    case '<':
                        processHtml();
                        break;
                    case '>':
                        StyleChar(MarkdownStyles.Bracket);
                        break;
                    case '[':
                        processLink();
                        break;
                    case '`':
                        processQuote();
                        break;
                    
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        processOrderedList();
                        break;
                    case '\t':
                        if (!CheckInHtml(Pos))
                            processCode();
                        break;
                    default:
                        StyleChar(MarkdownStyles.Default);
                        break;
                }
                Pos++;
            }


        }
    }
    //public class MarkdownLexer
    //{
    //    private const int EndOfLine = -1;
    //    enum eSTYLES
    //    {
    //        Default = 32,
    //        Bold = 1,
    //        Italic = 2,
    //        Underline = 3,
    //        StrikeOut = 4,
    //        H1 = 5,
    //        H2 = 6,
    //        H3 = 7,
    //        H4 = 8,
    //        H6 = 9,
    //        Link = 10,
    //        UnorderedList = 11,
    //        OrderedList = 12,
    //        Code = 13,
    //        LineBegin = 14,
    //        EM = 15
    //    }
    //    private Scintilla _boundControl;
    //    /// <summary>
    //    /// The Scintilla control that is having its content lexed by this lexer.
    //    /// </summary>
    //    public Scintilla BoundControl
    //    {
    //        get
    //        {
    //            return _boundControl;
    //        }
    //        set
    //        {
    //            _boundControl = value;
    //            InitialiseLexing();
    //        }
    //    }

    //    private int _lineIndex;
    //    private string _currentLine;

    //    private void SetStyleValue(eSTYLES index, Color? colour = null, int? size = null,
    //    Color? background = null, Font font = null, bool? bold = null,
    //    bool? italic = null, bool? underline = null, bool? visible = null)
    //    {
    //        if (colour != null)
    //            BoundControl.Styles[(int)index].ForeColor = (Color)colour;
    //        if (bold != null)
    //            BoundControl.Styles[(int)index].Bold = (bool)bold;
    //        if (italic != null)
    //            BoundControl.Styles[(int)index].Italic = (bool)italic;
    //        if (underline != null)
    //            BoundControl.Styles[(int)index].Underline = (bool)underline;
    //        if (background != null)
    //            BoundControl.Styles[(int)index].BackColor = (Color)background;
    //        if (font != null)
    //            BoundControl.Styles[(int)index].Font = font;
    //        if (size != null)
    //            BoundControl.Styles[(int)index].Size = (int)size;
    //        if (visible != null)
    //            BoundControl.Styles[(int)index].IsVisible = (bool)visible;
    //    }

    //    /// <summary>
    //    /// Enables custom lexing on the currently bound Scintilla control.
    //    /// </summary>
    //    public void InitialiseLexing()
    //    {
    //        if (BoundControl != null)
    //        {
    //            BoundControl.Margins[0].Width = 30;
    //            BoundControl.Indentation.IndentWidth = 4;
    //            BoundControl.Indentation.SmartIndentType = SmartIndent.CPP;

    //            BoundControl.ConfigurationManager.Language = "";
    //            BoundControl.Lexing.LexerName = "container";
    //            BoundControl.Lexing.Lexer = Lexer.Container;

    //            InitialiseStyles();
    //        }
    //    }

    //    /// <summary>
    //    /// Reads a character from the current line of the Scintilla text.
    //    /// </summary>
    //    /// <returns>An integer representing the character (or the End of Line value).</returns>
    //    private int Read()
    //    {
    //        if (_lineIndex < _currentLine.Length)
    //            return _currentLine[_lineIndex];
    //        return EndOfLine;
    //    }

    //    /// <summary>
    //    /// This will style the length of chars and advance the style pointer.
    //    /// </summary>
    //    /// <param name="style">The style to use for the styling.</param>
    //    /// <param name="length">The length of characters to style.</param>
    //    private void SetStyle(eSTYLES style, int length)
    //    {
    //        if (length > 0)
    //        {
    //            ((INativeScintilla)BoundControl).SetStyling(length, (int)style);
    //            _lineIndex += length;
    //        }
    //    }

    //    /// <summary>
    //    /// Sets the formatting for the styles of the currently bound Scintilla control.
    //    /// </summary>
    //    public void InitialiseStyles()
    //    {
    //        //SetStyleValue(eSTYLES.DEFAULT, Color.RoyalBlue);
    //        //SetStyleValue(eSTYLES.COMMENT, Color.Green);
    //        //SetStyleValue(eSTYLES.STRING, Color.Red);
    //        //SetStyleValue(eSTYLES.NUMBER, Color.Red);
    //        //SetStyleValue(eSTYLES.OPERATOR, Color.Black, null, null, null, true);
    //    }

    //    /// <summary>
    //    /// Styles just 1 character and advances the index.
    //    /// </summary>
    //    /// <param name="style"></param>
    //    private void StyleChar(eSTYLES style)
    //    {
    //        SetStyle(style, 1);
    //    }

    //    private void ReadHeader()
    //    {
    //        while(Read() != )
    //    }

    //    /// <summary>
    ///// Runs this lexer on the bound control and styles its content.
    ///// </summary>
    //    public void RunLexer()
    //    {

    //        if (BoundControl == null)
    //            return;

    //        // Start styling from 0 (can change to other if you feel sure of where
    //        //   changes have occured).
    //        ((INativeScintilla)BoundControl).StartStyling(0, 0xfa);

    //        string[] lines = BoundControl.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
    //        if (lines.Length == 0)
    //            return;

    //        for (int i = 0; i < lines.Length; i++)
    //        {
    //            _currentLine = lines[i];

    //            for (_lineIndex = 0; _lineIndex < _currentLine.Length; )
    //            {
    //                if (i > 0)
    //                    SetStyle(eSTYLES.Default, 2);

    //                switch (Read())
    //                {
    //                    case '#':
    //                        ReadHeader();
    //                        break;
    //                    case '\'':
    //                        StyleUntilMatch(eSTYLES.STRING, new char[] { '\'' });
    //                        StyleChar(eSTYLES.STRING);
    //                        break;
    //                    case '=':
    //                    case '>':
    //                    case '<':
    //                    case '!':
    //                    case '(':
    //                    case ')':
    //                    case '[':
    //                    case ']':
    //                    case ';':
    //                        StyleChar(eSTYLES.OPERATOR);
    //                        break;
    //                    case '#':
    //                        SetStyle(eSTYLES.COMMENT, _currentLine.Length - _lineIndex);
    //                        break;
    //                    case EndOfLine:
    //                        break;
    //                    default:
    //                        StyleChar(eSTYLES.DEFAULT);
    //                        break;
    //                }
    //            }
    //        }
    //    }
    //}
}
