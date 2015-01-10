#region Using Directives

using System;
using System.ComponentModel;

#endregion Using Directives

namespace ScintillaNET
{
    [TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class AutoClose : TopLevelHelper
    {
        #region Properties

        private bool autoCloseBraces = false;
        public bool AutoCloseBraces
        {
            get
            {
                return autoCloseBraces; 
            }
            set
            {
                autoCloseBraces = value;
            }
        }
        private bool autoCloseHtmlTags = false;

        public bool AutoCloseHtmlTags
        {
            get
            {
                return autoCloseHtmlTags;
            }
            set
            {
                autoCloseHtmlTags = value;
            }
        }

        private bool autoCloseQuotes = false;
        public bool AutoCloseQuotes {
            get
            {
                return autoCloseQuotes;
            }
            set
            {
                autoCloseQuotes = value;
            }
        }

        #endregion

        public void HandleAutoClose(char ch)
        {
            int cPos = Scintilla.NativeInterface.GetCurrentPos();
            if (autoCloseQuotes)
            {
                handleQuotes(ch, cPos);
            }
            if (autoCloseBraces)
            {
                handleBraces(ch, cPos);
            }
            if (autoCloseHtmlTags)
            {
                handleHtmlTags(ch, cPos);
            }
        }

        private void AddChar(char ch, int cPos)
        {
            Scintilla.NativeInterface.AddText(1, ch.ToString());
            Scintilla.NativeInterface.SetAnchor(cPos);
            Scintilla.NativeInterface.SetCurrentPos(cPos);
            
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

        private void handleHtmlTags(char ch, int cPos)
        {
            if (ch == '>')
            {
                int styleBit = 0;
                int nCaret = Scintilla.CurrentPos;
                styleBit = getPrevStyle(nCaret);
                int nStyleBit = Scintilla.NativeInterface.GetStyleAt(nCaret);

                if (styleBit > 31 || nStyleBit == Constants.SCE_H_CDATA || nStyleBit == Constants.SCE_H_DOUBLESTRING || nStyleBit == Constants.SCE_H_SINGLESTRING)
                    return;

                int nMin = nCaret - 511;
                if (nMin < 0)
                    nMin = 0;

                // Minimum of 4 chars for a tag </a></b>etc
                if ((nCaret - nMin) < 3)
                    return;

                string xmlTag = "";
                // Loop backwards from 

                for (int i = nCaret - 1; i >= Scintilla.Lines.Current.StartPosition; i--)
                {
                    if (Scintilla.Text[i] == '>')
                        if (i > (Scintilla.Lines.Current.StartPosition - 1))
                        {
                            // Self closing html tag don't close it
                            if (Scintilla.Text[i - 1] == '/')
                            {
                                return;
                            }
                        }

                    if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789<".IndexOf(Scintilla.Text[i]) > -1)/*Scintilla.Text[i] != ' ' && Scintilla.Text[i] != '=' && Scintilla.Text[i] != '\t' &&
                        Scintilla.Text[i] != '>' && Scintilla.Text[i] != '/' && Scintilla.Text[i] != '<')*/
                    {
                        xmlTag = Scintilla.Text[i] + xmlTag;
                        if (Scintilla.Text[i] == '<')
                        {
                            break;
                        }
                    }
                    else
                    {
                        xmlTag = "";
                    }


                }

                

                // Must start with an opening brace or it's meaningless.5
                if (!xmlTag.StartsWith("<"))
                    return;

                xmlTag = xmlTag.Trim('<');

                string xmlTagLower = xmlTag.ToLowerInvariant();
                if (xmlTagLower == "br")
                    return;

                // xmlTag is not null and xmlTag is a word (Ignore things like <?php
                if (!string.IsNullOrEmpty(xmlTag) && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".IndexOf(xmlTag[0]) > -1)
                {
                   
                    Scintilla.UndoRedo.BeginUndoAction();
                    Scintilla.Selection.Text = "</" + xmlTag + ">";
                    Scintilla.Selection.Start = nCaret;
                    Scintilla.Selection.End = nCaret;
                    Scintilla.UndoRedo.EndUndoAction();
                }

            }
        }

        private void handleBraces(char ch, int cPos)
        {
            switch (ch)
            {
                case '(':
                    AddChar(')', cPos);
                    break;
                case '[':
                    AddChar(']', cPos);
                    break;
                case '{':
                    AddChar('}', cPos);
                    break;
                case ']':
                case '}':
                case ')':
                    cleanupBraces(ch, cPos);
                    break;
            }

        }

        private void cleanupBraces(char ch, int cPos)
        {
            char curChar = Scintilla.NativeInterface.GetCharAt(Scintilla.NativeInterface.GetCurrentPos());

            if (ch == curChar)
            {
                Scintilla.NativeInterface.DeleteBack();
                Scintilla.NativeInterface.SetSel(cPos, cPos);
            }
        }

        private void handleQuotes(char ch, int cPos)
        {
            if (Scintilla.GetSelections() > 1)
                return;
            if (ch == '"'){
                if (Scintilla.NativeInterface.GetCharAt(cPos) == '"'){
                    if (cPos > 1){
                        if (Scintilla.NativeInterface.GetCharAt(cPos-2) != '\\'){
                            Scintilla.UndoRedo.BeginUndoAction();
                            Scintilla.NativeInterface.DeleteBack();
                            Scintilla.NativeInterface.SetSel(cPos, cPos);
                            Scintilla.UndoRedo.EndUndoAction();
                        }
                    }
                }
                else
                {
                    string selText = Scintilla.Selection.Text;
                    if (String.IsNullOrEmpty(selText)){
                        Scintilla.NativeInterface.AddText(1, "\"");
                        
                    }
                    else
                    {
                        Scintilla.Selection.Text += "\"";

                    }
                    Scintilla.NativeInterface.SetAnchor(cPos);
                    Scintilla.NativeInterface.SetCurrentPos(cPos);
                }
            }

            if (ch == '\'')
            {
                if (Scintilla.NativeInterface.GetCharAt(cPos) == '\'')
                {
                    if (cPos > 1)
                    {
                        if (Scintilla.NativeInterface.GetCharAt(cPos - 2) != '\\')
                        {
                            Scintilla.UndoRedo.BeginUndoAction();
                            Scintilla.NativeInterface.DeleteBack();
                            Scintilla.NativeInterface.SetSel(cPos, cPos);
                            Scintilla.UndoRedo.EndUndoAction();
                        }
                    }
                }
                else
                {
                    string selText = Scintilla.Selection.Text;
                    if (String.IsNullOrEmpty(selText))
                    {
                        Scintilla.NativeInterface.AddText(1, "'");

                    }
                    else
                    {
                        Scintilla.Selection.Text += "'";

                    }
                    Scintilla.NativeInterface.SetAnchor(cPos);
                    Scintilla.NativeInterface.SetCurrentPos(cPos);
                }
            }
        }

        internal AutoClose(Scintilla scintilla) : base(scintilla) { }
    }
}
