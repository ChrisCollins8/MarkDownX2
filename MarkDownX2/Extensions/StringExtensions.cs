using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkDownX2.Extensions
{
    public static class StringExtensions
    {
        private static string WordChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static string NumberChars = "0123456789";

        /// <summary>
        /// Returns true if all characters in the string are a-z, A-Z, 0-9
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AllWordChars(this string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!WordChars.Contains(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the first non space char from string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char FirstNonSpaceChar(this string value)
        {
            char ch = '\0';
            for (int i = 0; i < value.Length; i++)
            {
                if (!value[i].IsWhiteSpace())
                {
                    return value[i];
                }
            }
            return ch;
        }

        public static int FirstNonSpaceCharIndex(this string value)
        {
            int ch = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (!value[i].IsWhiteSpace())
                {
                    return i;
                }
            }
            return ch;
        }

        public static bool IsWordChar(this char value)
        {
            return (WordChars.Contains(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!" \t\n\r".Contains(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsNumeric(this char value)
        {
            return (NumberChars.Contains(value));
        }

        /// <summary>
        /// Returns a number. If it is a numbered line beginning with number and period then
        /// the number is returned IE 7. would return 7. If not it returns -1
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static int IsNumberedList(this string line)
        {
            StringBuilder lineNumber = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\t')
                    continue;
                if (!line[i].IsNumeric() && line[i] != '.')
                {
                    return -1;
                }
                if (line[i].IsNumeric())
                {
                    lineNumber.Append(line[i]);
                }
                if (line[i] == '.' && lineNumber.Length > 0)
                {
                    int existingLine = -1;
                    if (line.Length - 1 > i)
                    {
                        if (line[i + 1] == ' ')
                        {
                            if (Int32.TryParse(lineNumber.ToString(), out existingLine))
                            {
                                return existingLine;
                            }
                        }
                    }
                    
                    return -1;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns if char is whitespace.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this char value)
        {
            if (!" \t\n\r".Contains(value))
                return false;
            return true;
        }

        /// <summary>
        /// Converts string litterls from user provided text to functional litterals
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToLiteral(this string input)
        {

            var literal = new StringBuilder(input.Length);
            input = input.Replace("\\n", "\n").Replace("\\0", "\0").Replace("\\b", "\b")
                    .Replace("\\f", "\f").Replace("\\r", "\r").Replace("\\t", "\t").Replace("\\v", "\v");
            return input;
            
        }
    }
}
