using System;
using System.Collections.Generic;
using ParsingInterfaces;
using System.Diagnostics.Contracts;
using System.Linq;

namespace RegexLexicalAnalyzer
{
    public class RegularExpressionLexer : ILexicalAnalyzer
    {

        private IEnumerable<RegularExpression> Rules { get; }

        public RegularExpressionLexer(IEnumerable<RegularExpression> rules)
        {

            Contract.Requires<ArgumentNullException>(rules != null, "Lexical analyzer rules must not be null.");
            Contract.Requires<ArgumentException>(rules.All(rule => rule != null), "All rules for lexical analyzer must be non-null.");

            this.Rules = new List<RegularExpression>(rules);

        }

        public IEnumerable<IToken> Analyze(ITextInput input)
        {
            return new IToken[0];
        }
    }
}
