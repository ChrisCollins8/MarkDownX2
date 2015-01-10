using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET.Extensions
{
    public static class StringExtensions
    {
        private static string WordChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

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

        public static bool IsWordChar(this char value)
        {
            return (WordChars.Contains(value));
        }

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

        public static bool IsWhiteSpace(this char value)
        {
            if (!" \t\n\r".Contains(value))
                return false;
            return true;
        }
    }
}
