using System;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer
{
    public class RegularExpression
    {
        public string Pattern { get; }

        public RegularExpression(string pattern)
        {
            Contract.Requires<ArgumentException>(RegularExpression.IsPatternValid(pattern), "Invalid regular expression pattern.");
            this.Pattern = pattern;
        }

        public static bool IsPatternValid(string pattern)
        {
            return pattern != null;
        }
    }
}
