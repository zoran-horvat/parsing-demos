using Common;
using Interfaces;
using System;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer
{
    public class TokenMatch
    {
        public int RepresentationLength { get; }
        private Func<Option<IToken>> TokenFactory { get; }

        public TokenMatch(int representationLength, Func<Option<IToken>> tokenFactory)
        {

            Contract.Requires<ArgumentException>(representationLength >= 0, "Representation length must not be negative.");
            Contract.Requires<ArgumentNullException>(tokenFactory != null, "Token factory must be non-null.");

            this.RepresentationLength = representationLength;
            this.TokenFactory = tokenFactory;

        }

        public Option<IToken> CreateToken()
        {
            return this.TokenFactory();
        }
    }
}
