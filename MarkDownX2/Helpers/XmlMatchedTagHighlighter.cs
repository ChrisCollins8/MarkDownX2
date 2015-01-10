// This file is part of DevNotePad
// Modified to C# source from C++ from Notepad++ project.
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkDownX2.Extensions;

namespace MarkDownX2.Helpers
{
    
    public class XmlMatchedTagHighlighter
    {

        public static void tagMatch(bool doHiliteAttr, Scintilla scintilla, bool prevChar = false)
        {
            int docStart = 0;
            int docEnd = scintilla.NativeInterface.GetLength();
            scintilla.NativeInterface.SetIndicatorCurrent(8);
            scintilla.NativeInterface.IndicatorClearRange(docStart, docEnd - docStart);

            string[] codeBeginTag = { "<?", "<%" };
            string[] codeEndTag = { "?>", "%>" };
            for (int i = 0; i < codeBeginTag.Length; i++)
            {
                int caret = scintilla.NativeInterface.GetCurrentPos() + 1;
                FindResult startFound = MatchingTag.findText(codeBeginTag[i], caret, 0, 0, scintilla);
                FindResult endFound = MatchingTag.findText(codeEndTag[i], caret, 0, 0, scintilla);
                if (startFound.success)
                {
                    if (!endFound.success)
                        return;
                    else if (endFound.success && endFound.start <= startFound.end)
                        return;
                }
            }
            int originalStartPos = scintilla.NativeInterface.GetTargetStart();
            int originalEndPos = scintilla.NativeInterface.GetTargetEnd();
            int originalSearchFlags = scintilla.NativeInterface.GetSearchFlags();

            XmlMatchedTagsPos xmlTags = new XmlMatchedTagsPos();
            if (MatchingTag.getXmlMatchedTagsPos(scintilla, ref xmlTags, prevChar))
            {

                scintilla.NativeInterface.SetIndicatorCurrent(8);
                
                if (xmlTags.tagCloseStart != -1 && xmlTags.tagCloseEnd != -1)
                {
                    scintilla.NativeInterface.IndicatorFillRange(xmlTags.tagCloseStart, xmlTags.tagCloseEnd - xmlTags.tagCloseStart);
                }
                // Modified to highlight the entire tag instead of just the tagname on the start tag.
                scintilla.NativeInterface.IndicatorFillRange(xmlTags.tagOpenStart, xmlTags.tagOpenEnd - xmlTags.tagOpenStart);

                // Modified to highlight the entire tag from start to finish.
                //scintilla.NativeInterface.IndicatorFillRange(xmlTags.tagOpenStart, xmlTags.tagOpenEnd); // - xmlTags.tagOpenStart);
                //scintilla.NativeInterface.IndicatorFillRange(xmlTags.tagOpenEnd - openTagTailLen, openTagTailLen);

            }
            scintilla.NativeInterface.SetTargetStart(originalStartPos);
            scintilla.NativeInterface.SetTargetEnd(originalEndPos);
            scintilla.NativeInterface.SetSearchFlags(originalSearchFlags);
        }
    }
}
