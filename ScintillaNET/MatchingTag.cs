using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNET.Extensions;

namespace ScintillaNET
{
    public class TagMatch
    {
        public string TagName { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public TagMatch(string tagName, int start, int end)
        {
            TagName = tagName;
            Start = start;
            End = end;
        }
    }

    public struct XmlMatchedTagsPos
    {
        public int tagOpenStart;
        public int tagNameEnd;
        public int tagOpenEnd;
        public int tagCloseStart;
        public int tagCloseEnd;
    }

    public class FindResult
    {
        public int start = -1;
        public int end = -1;
        public bool success = false;
    }
    public static class MatchingTag
    {

        private static string tagName(TagMatch tag)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tag.TagName.Length; i++)
            {
                char ch = tag.TagName[i];
                if (ch == ' ' || ch == '\t' || ch == '>' || ch == '"' || ch == '\'')
                {
                    break;
                }
                if (ch != '<' && ch != '/')
                    sb.Append(ch);
            }
            return sb.ToString();
        }

        public static FindResult findText(string text, int start, int end, int flags, Scintilla scintilla)
        {
            FindResult returnValue = new FindResult();

            Range nResult = scintilla.FindReplace.Find(new Range(start, end, scintilla), text, SearchFlags.Empty, (start > end));

            if (nResult != null)
            {
                returnValue.success = true;
                returnValue.start = nResult.Start;
                returnValue.end = nResult.End;
            }

            return returnValue;
        }



        private static int findCloseAngle(int startPosition, int endPosition, Scintilla scintilla)
        {
            FindResult closeAngle = new FindResult();
            bool isValidClose = false;
            int returnPosition = -1;

            if (startPosition > endPosition)
            {
                int temp = endPosition;
                endPosition = startPosition;
                startPosition = temp;
            }

            do
            {
                isValidClose = false;
                closeAngle = findText(">", startPosition, endPosition, 0, scintilla);
                if (closeAngle.success)
                {
                    int style = scintilla.NativeInterface.GetStyleAt(closeAngle.start);
                    if (style != Constants.SCE_H_DOUBLESTRING && style != Constants.SCE_H_SINGLESTRING)
                    {
                        returnPosition = closeAngle.start;
                        isValidClose = true;
                    }
                    else
                    {
                        startPosition = closeAngle.end;
                    }
                }
            } while (closeAngle.success && isValidClose == false);
            return returnPosition;
        }

        private static FindResult findCloseTag(string tagName, int start, int end, Scintilla scintilla)
        {
            string search = "</" + tagName;
            FindResult closeTagFound = new FindResult();
            closeTagFound.success = false;
            FindResult result = new FindResult();
            char nextChar = '\0';
            int styleAt = 0;
            int searchStart = start;
            int searchEnd = end;
            bool forwardSearch = (start < end);
            bool validCloseTag = false;
            do
            {
                result = findText(search, searchStart, searchEnd, 0, scintilla);
                if (result.success)
                {
                    nextChar = scintilla.NativeInterface.GetCharAt(result.end);
                    styleAt = scintilla.NativeInterface.GetStyleAt(result.start);

                    if (forwardSearch)
                    {
                        searchStart = result.end + 1;
                    }
                    else
                    {
                        searchStart = result.start - 1;
                    }

                    if (styleAt != Constants.SCE_H_CDATA && styleAt != Constants.SCE_H_SINGLESTRING && styleAt != Constants.SCE_H_DOUBLESTRING)
                    {
                        if (nextChar == '>')
                        {
                            validCloseTag = true;
                            closeTagFound.start = result.start;
                            closeTagFound.end = result.end;
                            closeTagFound.success = true;
                        }
                        else if (nextChar.IsWhiteSpace())
                        {
                            int whitespacePoint = result.end;
                            do
                            {
                                whitespacePoint++;
                                nextChar = scintilla.NativeInterface.GetCharAt(whitespacePoint);
                            } while (nextChar.IsWhiteSpace());

                            if (nextChar == '>')
                            {
                                validCloseTag = true;
                                closeTagFound.start = result.start;
                                closeTagFound.end = whitespacePoint;
                                closeTagFound.success = true;
                            }
                        }
                    }
                }
            } while (result.success && !validCloseTag);
            return closeTagFound;
        }


        private static FindResult findOpenTag(string tagName, int start, int end, Scintilla scintilla)
        {
            string search = "<" + tagName;
            FindResult openTagFound = new FindResult();
            openTagFound.success = false;
            FindResult result = new FindResult();

            char nextChar = '\0';
            int styleAt = 0;
            int searchStart = start;
            int searchEnd = end;
            bool forwardSearch = (start < end);

            do
            {
                result = findText(search, searchStart, searchEnd, 0, scintilla);
                if (result.success)
                {
                    nextChar = scintilla.NativeInterface.GetCharAt(result.end);
                    styleAt = scintilla.NativeInterface.GetStyleAt(result.start);
                    if (styleAt != Constants.SCE_H_CDATA && styleAt != Constants.SCE_H_DOUBLESTRING && styleAt != Constants.SCE_H_SINGLESTRING)
                    {
                        if (nextChar == '>')
                        {
                            openTagFound.end = result.end;
                            openTagFound.success = true;
                        }
                        else if (nextChar.IsWhiteSpace())
                        {
                            int closeAnglePosition = findCloseAngle(result.end, forwardSearch ? end : start, scintilla);
                            if (closeAnglePosition != -1 && scintilla.NativeInterface.GetStyleAt(closeAnglePosition) != '/')
                            {
                                openTagFound.end = closeAnglePosition;
                                openTagFound.success = true;
                            }
                        }
                    }
                }
                if (forwardSearch)
                {
                    searchStart = result.end + 1;
                }
                else
                {
                    searchStart = result.start - 1;
                }
            } while (result.success && !openTagFound.success);
            openTagFound.start = result.start;
            return openTagFound;
        }



        public static bool getXmlMatchedTagsPos(Scintilla scintilla, ref XmlMatchedTagsPos xmlTags, bool prevChar)
        {

            bool tagFound = false;
            int caret = scintilla.NativeInterface.GetCurrentPos();
            if (prevChar)
            {
                if (caret > 0)
                {
                    caret--;
                }
            }
            int searchStartPoint = caret;
            int styleAt = -1;
            // search back for the previous open angle bracket.
            // Keep looking whilst the angle bracked found is inside an XML attribute.
            FindResult openFound = new FindResult();
            do
            {
                openFound = findText("<", searchStartPoint, 0, 0, scintilla);
                styleAt = scintilla.NativeInterface.GetStyleAt(openFound.start);
                searchStartPoint = openFound.start - 1;
            } while (openFound.success && (styleAt == Constants.SCE_H_DOUBLESTRING || styleAt == Constants.SCE_H_SINGLESTRING) && searchStartPoint > 0);

            if (openFound.success && styleAt != Constants.SCE_H_CDATA)
            {
                FindResult closeFound = new FindResult();
                searchStartPoint = openFound.start;
                do
                {
                    closeFound = findText(">", searchStartPoint, caret, 0, scintilla);
                    styleAt = scintilla.NativeInterface.GetStyleAt(closeFound.start);
                    searchStartPoint = closeFound.end;
                } while (closeFound.success && (styleAt == Constants.SCE_H_DOUBLESTRING || styleAt == Constants.SCE_H_SINGLESTRING) && searchStartPoint <= caret);

                if (!closeFound.success)
                {
                    char nextChar = scintilla.NativeInterface.GetCharAt(openFound.start + 1);
                    if (nextChar == '/')
                    {
                        xmlTags.tagCloseStart = openFound.start;
                        int docLength = scintilla.NativeInterface.GetLength();
                        FindResult endCloseTag = findText(">", caret, docLength, 0, scintilla);
                        if (endCloseTag.success)
                        {
                            xmlTags.tagCloseEnd = endCloseTag.end;
                        }


                        // Find the tag name.
                        int position = openFound.start + 2;
                        string tagName = "";
                        nextChar = scintilla.NativeInterface.GetCharAt(position);
                        while (position < docLength && !nextChar.IsWhiteSpace() && nextChar != '/' && nextChar != '>' && nextChar != '"' && nextChar != '\'')
                        {
                            tagName += nextChar; // tagName;
                            position++;
                            nextChar = scintilla.NativeInterface.GetCharAt(position);
                        }
                        if (!String.IsNullOrEmpty(tagName))
                        {
                            /* Now we need to find the open tag.  The logic here is that we search for "<TAGNAME",
			        		 * then check the next character - if it's one of '>', ' ', '\"' then we know we've found 
		    		    	 * a relevant tag. 
	    				     * We then need to check if either
        					 *    a) this tag is a self-closed tag - e.g. <TAGNAME attrib="value" />
	    				     * or b) this tag has another closing tag after it and before our closing tag
		    		    	 *       e.g.  <TAGNAME attrib="value">some text</TAGNAME></TAGNA|ME>
			        		 *             (cursor represented by |)
		    	    		 * If it's either of the above, then we continue searching, but only up to the
	    			    	 * the point of the last find. (So in the (b) example above, we'd only search backwards 
    					     * from the first "<TAGNAME...", as we know there's a close tag for the opened tag.
                                
    			    		 * NOTE::  NEED TO CHECK THE ROTTEN CASE: ***********************************************************
	    	    			 * <TAGNAME attrib="value"><TAGNAME>something</TAGNAME></TAGNAME></TAGNA|ME>
	        				 * Maybe count all closing tags between start point and start of our end tag.???
    		    			 */
                            int currentEndPoint = xmlTags.tagCloseStart;
                            int openTagsRemaining = 1;
                            FindResult nextOpenTag = new FindResult();
                            do
                            {
                                nextOpenTag = findOpenTag(tagName, currentEndPoint, 0, scintilla);
                                if (nextOpenTag.success)
                                {
                                    openTagsRemaining--;

                                    FindResult inbetweenCloseTag = new FindResult();
                                    int currentStartPosition = nextOpenTag.end;
                                    int closeTagsFound = 0;
                                    bool forwardSearch = (currentStartPosition < currentEndPoint);

                                    do
                                    {
                                        inbetweenCloseTag = findCloseTag(tagName, currentStartPosition, currentEndPoint, scintilla);
                                        if (inbetweenCloseTag.success)
                                        {
                                            closeTagsFound++;
                                            if (forwardSearch)
                                            {
                                                currentStartPosition = inbetweenCloseTag.end;
                                            }
                                            else
                                            {
                                                currentStartPosition = inbetweenCloseTag.start - 1;
                                            }
                                        }
                                    } while (inbetweenCloseTag.success);

                                    if (closeTagsFound == 0 && openTagsRemaining == 0)
                                    {
                                        xmlTags.tagOpenStart = nextOpenTag.start;
                                        xmlTags.tagOpenEnd = nextOpenTag.end + 1;
                                        xmlTags.tagNameEnd = nextOpenTag.start + tagName.Length + 1;
                                        tagFound = true;
                                    }
                                    else
                                    {
                                        openTagsRemaining += closeTagsFound;
                                        currentEndPoint = nextOpenTag.start;
                                    }
                                }
                            } while (!tagFound && openTagsRemaining > 0 && nextOpenTag.success);
                        }
                    }

                    else
                    {
                        // Cursor in open tag
                        int position = openFound.start + 1;
                        int docLength = scintilla.NativeInterface.GetLength();
                        xmlTags.tagOpenStart = openFound.start;

                        string tagName = "";
                        nextChar = scintilla.NativeInterface.GetCharAt(position);
                        while (position < docLength && !nextChar.IsWhiteSpace() && nextChar != '/' && nextChar != '>' && nextChar != '"' && nextChar != '\'')
                        {
                            tagName = tagName + nextChar;
                            position++;
                            nextChar = scintilla.NativeInterface.GetCharAt(position);
                        }

                        if (!String.IsNullOrEmpty(tagName))
                        {
                            xmlTags.tagNameEnd = openFound.start + tagName.Length + 1;
                            int closeAnglePosition = findCloseAngle(position, docLength, scintilla);
                            if (closeAnglePosition != -1)
                            {
                                xmlTags.tagOpenEnd = closeAnglePosition + 1;
                                if (scintilla.NativeInterface.GetCharAt(closeAnglePosition - 1) == '/')
                                {
                                    xmlTags.tagCloseEnd = -1;
                                    xmlTags.tagCloseStart = -1;
                                    tagFound = true;
                                }
                                else
                                {
                                    int currentStartPosition = xmlTags.tagOpenEnd;
                                    int closeTagsRemaining = 1;
                                    FindResult nextCloseTag = new FindResult();

                                    do
                                    {
                                        nextCloseTag = findCloseTag(tagName, currentStartPosition, docLength, scintilla);
                                        if (nextCloseTag.success)
                                        {
                                            closeTagsRemaining--;

                                            FindResult inbetweenOpenTag = new FindResult();
                                            int currentEndPosition = nextCloseTag.start;
                                            int openTagsFound = 0;
                                            do
                                            {
                                                inbetweenOpenTag = findOpenTag(tagName, currentStartPosition, currentEndPosition, scintilla);
                                                if (inbetweenOpenTag.success)
                                                {
                                                    openTagsFound++;
                                                    currentStartPosition = inbetweenOpenTag.end;
                                                }
                                            } while (inbetweenOpenTag.success);

                                            if (openTagsFound == 0 && closeTagsRemaining == 0)
                                            {
                                                xmlTags.tagCloseStart = nextCloseTag.start;
                                                xmlTags.tagCloseEnd = nextCloseTag.end + 1;
                                                tagFound = true;
                                            }
                                            else
                                            {
                                                closeTagsRemaining += openTagsFound;
                                                currentStartPosition = nextCloseTag.end;
                                            }
                                        }
                                    } while (!tagFound && closeTagsRemaining > 0 && nextCloseTag.success);
                                }
                            }
                        }
                    }
                }
            }
            return tagFound;
        }

        


        private static TagMatch getHtmlTag(Scintilla scintilla, int pos)
        {

            int startPos = 0;
            int stopPos = 0;
            bool inTag = false;
            for (int i = pos; i >= 0; i--)
            {
                char ch = scintilla.NativeInterface.GetCharAt(i);
                if (ch == '<')
                {
                    // In a tag
                    inTag = true;
                    startPos = i;
                    break;
                }
                if (ch == '\n' || ch == '\r' || ch == '>')
                {
                    break;
                }
            }

            if (inTag)
            {
                for (int i = startPos + 1; i < scintilla.TextLength; i++)
                {
                    char ch = scintilla.NativeInterface.GetCharAt(i);
                    if (ch == '>')
                    {
                        stopPos = i + 1;
                        break;
                    }
                }
            }
            if (inTag && stopPos > startPos)
            {
                return new TagMatch(scintilla.GetRange(startPos, stopPos).Text, startPos, stopPos);
            }

            return null;

        }

    }
}
