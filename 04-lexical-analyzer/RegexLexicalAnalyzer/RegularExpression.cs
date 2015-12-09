using System;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer
{
    public class RegularExpression
    {
        public string Pattern { get; }
        public string Class { get; }

        public RegularExpression(string pattern, string tokenClass)
        {

            Contract.Requires<ArgumentException>(RegularExpression.IsPatternValid(pattern), "Invalid regular expression pattern.");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(tokenClass), "Token class must be non-empty.");

            this.Pattern = pattern;
            this.Class = tokenClass;

        }

        [Pure]
        public static bool IsPatternValid(string pattern)
        {
            return pattern != null;
        }
    }
}
