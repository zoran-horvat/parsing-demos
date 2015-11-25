using Common;
using Interfaces;
using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace RegexLexicalAnalyzer
{
    public abstract class TokenPattern
    {
        private Regex Pattern { get; }

        public TokenPattern(string pattern)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(pattern), "Token pattern must be non-empty.");
            this.Pattern = new Regex("^" + pattern);
        }

        public Option<TokenMatch> TryMatch(string input)
        {

            Match match = this.Pattern.Match(input);

            if (match.Success)
                return Option<TokenMatch>.Some(new TokenMatch(match.Value.Length, () => this.TokenFactory(match.Value)));

            return Option<TokenMatch>.None();

        }

        public abstract Func<string, Option<IToken>> TokenFactory { get; }

    }
}
