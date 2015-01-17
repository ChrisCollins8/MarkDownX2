using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkDownX2.Helpers;
using MarkDownX2.Lexers;
using MarkDownX2.Models;
using MarkDownX2.Extensions;
using ScintillaNET;

namespace MarkDownX2.GUI.Forms
{
    public partial class FormDocument : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private FormMain MainForm;
        private Syntax Styling;
        private DateTime lastUpdate = DateTime.UtcNow;
        private int prevTopLine = 0;

        private string FileName = "";

        public FormDocument(Syntax syntax, FormMain mainForm, string fileName = "")
        {

            Styling = syntax;
            MainForm = mainForm;
            // Set the filename
            FileName = fileName;

            InitializeComponent();
            Configure();
        }


        #region Configuration

        private void ConfigureEditor()
        {
            
            if (!String.IsNullOrEmpty(FileName) && File.Exists(FileName))
            {
                Editor.Text = File.ReadAllText(FileName);
                Editor.Modified = false;
                Editor.UndoRedo.EmptyUndoBuffer();
            }



            Editor.NativeInterface.UpdateUI += NativeInterface_UpdateUI;
        }

        /// <summary>
        /// Configures the editor/document settings based on the global settings.
        /// Can be called externally so available to the GlobalSettings static
        /// class.
        /// </summary>
        public void ReadSettings()
        {
            // Display or hide line numbers.
            Editor.Margins[0].Width = (GlobalSettings.Settings.DisplayLineNumbers ? 40 : 0);

            #region Formatting Marks (Whitespace/EOL Visibility)
            if (GlobalSettings.Settings.DisplayFormattingMarks){
                Editor.Whitespace.Mode = WhitespaceMode.VisibleAlways;
                Editor.EndOfLine.IsVisible = true;
            }
            else
            {
                Editor.Whitespace.Mode = WhitespaceMode.Invisible;
                Editor.EndOfLine.IsVisible = false;
            }
            #endregion

            // Enable/disable line wrapping.
            Editor.LineWrapping.Mode = (GlobalSettings.Settings.WordWrap ? LineWrappingMode.Word : LineWrappingMode.None);

            SetStyle();
            

        }

        /// <summary>
        /// Call necessary configuration methods.
        /// </summary>
        private void Configure()
        {
            ConfigureEditor();
            ReadSettings();
        }

        #endregion

        private void FormDocument_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Gets a very generic word count based on instances of spaces in document.
        /// </summary>
        /// <returns></returns>
        private int GetWordCount()
        {
            return Editor.Text.Split(' ').Length;
        }

        #region Scintilla Update UI 

        void NativeInterface_UpdateUI(object sender, NativeScintillaEventArgs e)
        {
            
            
            #region Match Scrolling

            if (prevTopLine != Editor.Lines.FirstVisible.Number){


                PreviewHelper.SetScroll(GetScrollPercentage());
            }
            

            #endregion



            #region Set statusbar info

            if (Editor.Selection.Length > 0 && !ButtonCut.Enabled) {
                ButtonCut.Enabled = true;
            }
            else if (Editor.Selection.Length == 0 && ButtonCut.Enabled)
            {
                ButtonCut.Enabled = false;
            }

            if (Editor.Selection.Length > 0 && !ButtonCopy.Enabled)
            {
                ButtonCopy.Enabled = true;
            }
            else if (Editor.Selection.Length == 0 && ButtonCopy.Enabled)
            {
                ButtonCopy.Enabled = false;
            }

            MainForm.SetStatus(Editor.TextLength, Editor.Lines.Count, Editor.Lines.Current.Number + 1, Editor.GetColumn(Editor.CurrentPos),
                Editor.Selection.Start, Editor.Selection.End, GetWordCount());

            #endregion


            #region Highlight current word in find box

            
            #endregion

            #region Highlight Selected word throughout document

            if ((String.IsNullOrEmpty(EditFind.Text) || ToolbarSearch.Visible == false) && !String.IsNullOrEmpty(Editor.Selection.Text))
            {
                Editor.Indicators[8].Style = IndicatorStyle.RoundBox;
                XmlMatchedTagHighlighter.tagMatch(true, Editor);
            }

            if ((String.IsNullOrEmpty(EditFind.Text) || ToolbarSearch.Visible == false) && !String.IsNullOrEmpty(Editor.Selection.Text))
            {
                Editor.NativeInterface.IndicatorClearRange(0, Editor.Text.Length);



                Editor.Indicators[8].Style = IndicatorStyle.RoundBox;

                

                //Editor.GetRange(0, Editor.Text.Length).SetIndicator(8, (int)Constants.INDIC_ROUNDBOX);
                //Editor.NativeInterface.Style, Constants.INDIC_ROUNDBOX);
                //Editor.
                Editor.NativeInterface.SetIndicatorCurrent(8);
                string selText = Editor.Selection.Text;
                if (!String.IsNullOrEmpty(selText) && selText.Length > 0)
                {
                    if (selText.AllWordChars())
                    {
                        int first = 0;

                        int last = Editor.TextLength;

                        Editor.NativeInterface.SetTargetStart(first);
                        Editor.NativeInterface.SetTargetEnd(last);

                        int i = Editor.NativeInterface.SearchInTarget(selText.Length, selText);

                        int selStart = Editor.NativeInterface.GetSelectionStart();
                        int selEnd = Editor.NativeInterface.GetSelectionEnd();

                        int tmpStart = 0;


                        if (selStart > selEnd)
                        {
                            tmpStart = selStart;
                            selStart = selEnd;
                            selEnd = tmpStart;
                        }
                        while (i > -1)
                        {
                            if (i + selText.Length < selStart || i - selText.Length > selEnd)
                            {
                                Editor.NativeInterface.SetIndicatorCurrent(8);
                                Editor.NativeInterface.IndicatorStart(8, i);
                                Editor.NativeInterface.IndicatorEnd(8, i + selText.Length);
                                Editor.NativeInterface.IndicatorFillRange(i, selText.Length);
                            }
                            first = i + selText.Length;
                            Editor.NativeInterface.SetTargetStart(first);
                            Editor.NativeInterface.SetTargetEnd(last);
                            i = Editor.NativeInterface.SearchInTarget(selText.Length, selText);
                        }
                    }
                }
            }
            #endregion
        }

        #endregion

        void UpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {



        }

        #region SetStyle

        public void SetStyle()
        {
            Editor.NativeInterface.SetLexer(0);//DefaultsHelper.MarkdownKey);




            if (Styling != null)
            {

                #region Set Defaults
                Editor.Styles[32].ForeColor = Styling.Styles[0].ForeColor;
                Editor.Styles[32].BackColor = Styling.Styles[0].BackColor;

                // Bold, italic, underline

                Editor.Styles[32].Bold = Styling.Styles[0].Bold;
                Editor.Styles[32].Italic = Styling.Styles[0].Italic;
                Editor.Styles[32].Underline = Styling.Styles[0].Underline;

                // Font/Size
                Editor.Styles[32].FontName = Styling.Styles[0].FontName;
                Editor.Styles[32].Size = Styling.Styles[0].FontSize;
                Editor.NativeInterface.StyleClearAll();
                #endregion

                if (GlobalSettings.Settings.SyntaxHighlighting)
                {
                    foreach (MarkDownX2.Models.Style style in Styling.Styles)
                    {


                        // Colors
                        Editor.Styles[style.Key].ForeColor = style.ForeColor;
                        Editor.Styles[style.Key].BackColor = style.BackColor;

                        // Bold, italic, underline

                        Editor.Styles[style.Key].Bold = style.Bold;
                        Editor.Styles[style.Key].Italic = style.Italic;
                        Editor.Styles[style.Key].Underline = style.Underline;

                        // Font/Size
                        Editor.Styles[style.Key].FontName = style.FontName;
                        Editor.Styles[style.Key].Size = style.FontSize;

                        Editor.NativeInterface.StyleSetEOLFilled(style.Key, style.FillEOL);
                        //Editor.Styles[style.Key].IsSelectionEolFilled = style.FillEOL;

                    }
                }
            }


            Editor.NativeInterface.StyleSetFore(Constants.STYLE_LINENUMBER, Utilities.ColorToRgb(Color.Black));
            Editor.NativeInterface.StyleSetBack(Constants.STYLE_LINENUMBER, Utilities.ColorToRgb(Color.White));
            Editor.NativeInterface.StyleSetFont(Constants.STYLE_LINENUMBER, "Consolas");
            Editor.NativeInterface.StyleSetSize(Constants.STYLE_LINENUMBER, 8);

            Editor.NativeInterface.Colourise(0, -1);

            Editor.NativeInterface.SetTabWidth(4);
        }

        #endregion

        private void Editor_DocumentChange(object sender, NativeScintillaEventArgs e)
        {
            
            //if ((DateTime.UtcNow - lastUpdate).TotalSeconds >= 1)
            //{
            if (!Editor.InTag())
            {
                
               // PreviewHelper.UpdateHtml(Editor.Text.Substring(Editor.Lines.FirstVisible.StartPosition, Editor.Lines.VisibleLines.LastOrDefault().EndPosition - Editor.Lines.FirstVisible.StartPosition), GetScrollPercentage());
            }
            //}



        }

        private void InsertWrapped(string begin, string end)
        {
            string insertText = begin + "" + end;
            if (!String.IsNullOrEmpty(Editor.Selection.Text))
            {
                insertText = begin + Editor.Selection.Text + end;
            }
            Editor.Selection.Text = insertText;
            Editor.Selection.Start -= end.Length;
            Editor.Selection.End = Editor.Selection.Start;
        }

        private void Editor_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            

            Range nRange = new Range(0, Editor.TextLength, Editor);
            MarkdownLexer lexer = new MarkdownLexer(e.Range, Editor);
            lexer.Colourise();
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("**", "**");
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("*", "*");
        }

        private void heading1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("#", "#");
        }

        private void heading2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("##", "##");
        }

        private void heading3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("###", "###");
        }

        private void heading4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("####", "####");
        }

        private void heading5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("#####", "#####");
        }

        private void heading6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("######", "######");
        }

        private void iMageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportToPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Inserts a hyperlink.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hyperlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormLink formLink = new FormLink(Editor.Selection.Text))
            {
                if (formLink.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    if (!String.IsNullOrEmpty(formLink.Url))
                    {

                        StringBuilder buildLink = new StringBuilder();
                        string anchorText = "";
                        string titleText = "";
                        if (!String.IsNullOrEmpty(formLink.Anchor))
                        {
                            anchorText = formLink.Anchor;
                        }
                        else
                        {
                            anchorText = formLink.Url;
                        }

                        if (!String.IsNullOrEmpty(formLink.Title))
                        {
                            titleText = String.Format(" \"{0}\"", formLink.Title);
                        }

                        buildLink.Append((String.Format("[{0}]", anchorText)));

                        buildLink.Append(String.Format("({0}{1})", formLink.Url, titleText));

                        Editor.Selection.Text = buildLink.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Saves the document to a specific filename.
        /// </summary>
        /// <param name="fileName"></param>
        private void Save(string fileName) {

            try
            {
                // Using a raw binary stream because it ensures proper writing when dealing
                // unicode files.
                using (FileStream fs = File.Create(fileName))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Editor.RawText, 0, Editor.RawText.Length - 1);
                    Text = fileName;
                    TabText = Path.GetFileName(fileName);
                    FileName = fileName;
                }
            }                
            catch (Exception ex)
            {
                // We are using the exception here because there is a chance that an exception is raised due
                // when trying to write. If this happens display an error message and ask if they would like
                // to save as.
                DialogResult result = MessageBox.Show(String.Format("An error occurred when trying to write your file to \"{1}\"\nWould you like to change your save location?", fileName), "Save Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                    SaveAs();
            }
                            
        }

        private void SaveAs()
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "MarkDown Files (*.md)|*.md|Text File (*.txt)|*.txt|All Files (*.*)|*.*";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrEmpty(saveDialog.FileName))
                    {
                        Save(saveDialog.FileName);
                    }
                }
            }
        }

        private void Save()
        {
            if (!String.IsNullOrEmpty(FileName) && Directory.Exists(Path.GetDirectoryName(FileName)))
            {
                Save(FileName);
            }
            else
            {
                SaveAs();
            }
        }

        private void quoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWrapped("`", "`");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void HandleLists()
        {
            // Get last line text
            Editor.UndoRedo.BeginUndoAction();
            string text = Editor.Lines.Current.Previous.Text;
            
            int firstCharPos = text.FirstNonSpaceCharIndex();
            char firstChar = text[firstCharPos];
            if (text.Length > firstCharPos + 1)
            {
                if ((firstChar == '*' || firstChar == '-') && text[firstCharPos + 1] == ' ')
                {
                    // Unordered list.
                    string trimmedText = text.Trim();
                    if (trimmedText != "*" && trimmedText != "-")
                    {
                        Editor.Indentation.SmartIndentType = SmartIndent.None;
                        string baseText = Editor.Lines.Current.Text.Trim();
                        Editor.Selection.Text = String.Format("{0} {1}", firstChar.ToString(), baseText);

                        Editor.Selection.Start = Editor.Lines.Current.StartPosition + 4;
                        Editor.Selection.End = Editor.Selection.Start;
                        Editor.Indentation.SmartIndentType = SmartIndent.Simple;
                    }
                    else
                    {
                        Editor.Lines.Current.Previous.Text = "";
                    }
                    Editor.UndoRedo.EndUndoAction();
                    return;
                }
            }

            int prevNumber = Editor.Lines.Current.Previous.Text.IsNumberedList();
            if (prevNumber > -1)
            {
                string trimmedText = text.Trim();
                if (trimmedText == String.Format("{0}.", prevNumber))
                {
                    Editor.Lines.Current.Previous.Text = "";
                }
                else
                {
                    string baseText = Editor.Lines.Current.Text;
                    string number = (prevNumber + 1).ToString();
                    Editor.Lines.Current.Text = String.Format("{0}. {1}", number, baseText);
                    Editor.Selection.Start = Editor.Lines.Current.StartPosition + number.Length + 2;
                    Editor.Selection.End = Editor.Selection.Start;
                }
            }
            Editor.UndoRedo.EndUndoAction();
        }

        private void Editor_CharAdded(object sender, CharAddedEventArgs e)
        {
            #region Deal with lists.
            char checkChar = '\0';
            if (Editor.EndOfLine.Mode == EndOfLineMode.Crlf || Editor.EndOfLine.Mode == EndOfLineMode.LF)
                checkChar = '\n';
            else
                checkChar = '\r';
            if (e.Ch == checkChar)
            {
                HandleLists();
            }


            #endregion
        }

        private float GetScrollPercentage()
        {
            
            //int finalLastLine = Editor.Lines.VisibleLines.Last().Number;
            int firstLine = Editor.Lines.FirstVisible.Number;

            if (firstLine == 0)
                return 0;

            int linesVisible = Editor.Lines.VisibleCount;
            int finalLastLine = firstLine + linesVisible;

            int midLine = firstLine + (int)((finalLastLine - firstLine) / 2);
            midLine = firstLine;
            if (finalLastLine == linesVisible)
                midLine = linesVisible;

            //int lastLine = 0;
            //if (Editor.Lines.VisibleLines.First().Number == 0)
            //    lastLine = 0;
            //else if (Editor.Lines.VisibleLines.Last().Number >= Editor.Lines.Count-1)
            //    lastLine = Editor.Lines.VisibleLines.Last().Number;
            //else 
            //    lastLine = Editor.Lines.VisibleLines.First().Number + Editor.Lines.VisibleLines.Length;
            //int totalLines = Editor.Lines.Count;

            float result = ((float)((float)midLine / (float)Editor.Lines.Count));
            //if (result > .5)
            //{
            //    lastLine = Editor.Lines.VisibleLines.Last().Number;
            //    result = ((float)((float)lastLine / (float)totalLines));
            //}
            return result;
        }

        private void Editor_Scroll(object sender, ScrollEventArgs e)
        {
            //string text = Editor.Lines.VisibleLines[Editor.Lines.VisibleLines.Length - 1].Text;
            //PreviewHelper.UpdateHtml(Editor.Text, GetScrollPercentage());           
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                int number = Editor.Lines.Current.Text.IsNumberedList();
                if (number > -1)
                {
                    // Knock out the char we just added
                    int pos = Editor.CurrentPos;
                    Editor.Lines.Current.Indentation+=Editor.NativeInterface.GetTabWidth();
                    Editor.Lines.Current.Text = Editor.Lines.Current.Text.Replace(number.ToString(), "1");
                    int len = number.ToString().Length;
                    int dif = len - 1;
                    Editor.CurrentPos = pos;
                    // Stop the key press
                    e.SuppressKeyPress = true;
                }
                
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.UndoRedo.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.UndoRedo.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Clipboard.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Clipboard.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (!String.IsNullOrEmpty(Editor.Selection.Text))
                System.Windows.Forms.Clipboard.SetText(PreviewHelper.GetMarkdown(Editor.Selection.Text));
        }

        private void copyPreviewContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewHelper.CopyPreviewContents();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Editor.Clipboard.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Editor.NativeInterface.
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Selection.SelectAll();
        }

        private void timestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Selection.Text = DateTime.Now.ToString();
        }

        private void ButtonBold_Click(object sender, EventArgs e)
        {
            boldToolStripMenuItem_Click(null, e);
        }

        private void ButtonItalic_Click(object sender, EventArgs e)
        {
            italicToolStripMenuItem_Click(null, e);
        }

        private void ButtonQuote_Click(object sender, EventArgs e)
        {
            quoteToolStripMenuItem_Click(null, e);
        }

        private void ButtonCode_Click(object sender, EventArgs e)
        {
            codeToolStripMenuItem_Click(null, e);
        }

        private void ButtonH1_Click(object sender, EventArgs e)
        {
            heading1ToolStripMenuItem_Click(null, e);
        }

        private void ButtonH2_Click(object sender, EventArgs e)
        {
            heading2ToolStripMenuItem_Click(null, e);
        }

        private void heading3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            heading3ToolStripMenuItem_Click(null, e);
        }

        private void heading4ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            heading4ToolStripMenuItem_Click(null, e);
        }

        private void heading5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            heading5ToolStripMenuItem_Click(null, e);
        }

        private void heading6ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            heading6ToolStripMenuItem_Click(null, e);
        }

        private void ButtonLink_Click(object sender, EventArgs e)
        {
            
            hyperlinkToolStripMenuItem_Click(null, e);
        }

        private void ButtonLine_Click(object sender, EventArgs e)
        {
            horizontalRuleToolStripMenuItem_Click(null, e);
        }

        private void ButtonTimestamp_Click(object sender, EventArgs e)
        {
            timestampToolStripMenuItem_Click(null, e);
        }

        private void codeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void horizontalRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ButtonCut_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(null, e);
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(null, e);
        }

        private void ButtonPaste_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(null, e);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(null, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem_Click(null, e);
        }

        #region Find/Replace

        private void EditFind_TextChanged(object sender, EventArgs e)
        {

            // Nothing to do if null.
            if (String.IsNullOrEmpty(EditFind.Text))
                return;

            Editor.NativeInterface.IndicatorClearRange(0, Editor.Text.Length);

            

            Editor.Indicators[8].Style = IndicatorStyle.RoundBox;
            string selText = EditFind.Text;
            int first = 0;

            int last = Editor.TextLength;

            Editor.NativeInterface.SetTargetStart(first);
            Editor.NativeInterface.SetTargetEnd(last);

            int i = Editor.NativeInterface.SearchInTarget(selText.Length, selText);

            int selStart = Editor.NativeInterface.GetSelectionStart();
            int selEnd = Editor.NativeInterface.GetSelectionEnd();

            int tmpStart = 0;


            if (selStart > selEnd)
            {
                tmpStart = selStart;
                selStart = selEnd;
                selEnd = tmpStart;
            }
            while (i > -1)
            {
                if (i + selText.Length < selStart || i - selText.Length > selEnd)
                {
                    Editor.NativeInterface.SetIndicatorCurrent(8);
                    Editor.NativeInterface.IndicatorStart(8, i);
                    Editor.NativeInterface.IndicatorEnd(8, i + selText.Length);
                    Editor.NativeInterface.IndicatorFillRange(i, selText.Length);
                }
                first = i + selText.Length;
                Editor.NativeInterface.SetTargetStart(first);
                Editor.NativeInterface.SetTargetEnd(last);
                i = Editor.NativeInterface.SearchInTarget(selText.Length, selText);
            }
        }


        #endregion

        private void EditFind_Click(object sender, EventArgs e)
        {

        }

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            if (!Editor.InTag())
                PreviewHelper.UpdateHtml(Editor.Text, GetScrollPercentage());
        
        }

        private void Editor_Paint(object sender, PaintEventArgs e)
        {
 
        }

        private void EditFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                EditFind.SelectAll();
            }
        }

        #region Handle shortcuts for main edit and find box.

        /// <summary>
        /// Override the ProcessCmdKey so the expected hot keys can be trapped
        /// on the find edit when it has focus and process those commands for
        /// EditFind instead of the shortcuts defined in the main menus.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (EditFind.Focused)
            {
                if (keyData.HasFlag(Keys.Control))
                {
                    // Ctrl + C (Copy)
                    if (keyData.HasFlag(Keys.C))
                    {
                        EditFind.Copy();
                        return true;
                    }
                    // Ctrl + A (Select All)
                    if (keyData.HasFlag(Keys.A))
                    {
                        EditFind.SelectAll();
                        return true;
                    }
                    // Ctrl + V (Paste)
                    if (keyData.HasFlag(Keys.V))
                    {
                        EditFind.Paste();
                        return true;
                    }
                    // Ctrl + X (Cut)
                    if (keyData.HasFlag(Keys.X))
                    {
                        
                        EditFind.Cut();
                        EditFind.SelectedText = "";
                        return true;
                    }
                    // Ctrl + Z (Undo)
                    if (keyData.HasFlag(Keys.Z))
                    {
                        EditFind.Undo();
                        return true;
                    }
                    
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion
       

        private void ButtonCloseSearch_Click(object sender, EventArgs e)
        {
            ToolbarSearch.Visible = false;
        }

        private void EditFind_Click_1(object sender, EventArgs e)
        {

        }

        private void EditFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                MessageBox.Show("Finding");
            }
        }

        private void PanelHold_Click(object sender, EventArgs e)
        {
            Editor.Focus();
        }
    }
}