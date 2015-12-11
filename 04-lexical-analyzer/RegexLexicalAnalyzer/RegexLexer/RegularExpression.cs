using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer.RegexLexer
{
    public class RegularExpression
    {
        public string Pattern { get; }
        public string Class { get; }

        public RegularExpression(string pattern, string tokenClass)
        {

            Contract.Requires<ArgumentException>(pattern != null, "Regular expression pattern must be non-null.");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(tokenClass), "Token class must be non-empty.");

            this.Pattern = pattern;
            this.Class = tokenClass;

        }

        public IEnumerable<char> GetAlphabet() => new RegularExpressionAlphabet(this);

    }
}
