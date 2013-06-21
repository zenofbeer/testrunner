using System.Text.RegularExpressions;

namespace net.PaulChristensen.TestHarnessLib.Util
{
    public static class StringHelper
    {
        #region constants
        private const string VariableSearchPattern = "\\${[A-Za-z0-9]*}";
        #endregion constants

        /// <summary>
        /// check to see if the value is a variable
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsVariable(string value)
        {
            bool isMatch = false;

            var regex = new Regex(VariableSearchPattern);
            if(regex.IsMatch(value))
                isMatch = true;

            return isMatch;
        }

        /// <summary>
        /// strip off ${ and } from a variable
        /// </summary>
        /// <param name="value"></param>
        public static void StripVariableDelimiters(ref string value)
        {
            var regex = new Regex(VariableSearchPattern);
            value = regex.Replace(value, string.Empty);
        }

        /// <summary>
        /// trim leading back and forward slashes.
        /// </summary>
        /// <param name="value"></param>
        public static void TrimLeadingSlashes(ref string value)
        {
            value = value.TrimStart('\\');
            value = value.TrimStart('/');
        }

        /// <summary>
        /// Trim trailing back and forward slashes
        /// </summary>
        /// <param name="value"></param>
        public static void TrimTrailingSlashes(ref string value)
        {
            value = value.TrimEnd('\\');
            value = value.TrimEnd('/');
        }
    }
}