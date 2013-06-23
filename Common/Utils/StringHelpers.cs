using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace net.PaulChristensen.Common.Utils
{
    public class StringHelpers
    {
        /// <summary>
        /// Fix a path
        /// </summary>
        /// <param name="input"></param>
        public static void FixPath(ref string input)
        {
            input = input.TrimEnd('\\') + '\\';
        }

        public static int GetIntFromString(string input)
        {
            int output;
            if (!int.TryParse(input, out output))
            {
                throw new ApplicationException("Input cannot be converted to an int");
            }
            return output;
        }

        public static bool IsVariable(string value)
        {
            var isMatch = false;
            var regex = new Regex(Properties.Settings.Default.VariableSearchPattern);
            if (regex.IsMatch(value))
                isMatch = true;

            return isMatch;
        }

        public static string StripVariableDelimiters(string value)
        {
            if (!IsVariable(value))
            {
                throw new ApplicationException("Value is not a variable");
            }
            value = value.Substring(2);
            value = value.Substring(0, value.Length - 1);
            return value;
        }
    }
}
