using System;
using Interfaces;
using Common;

namespace RegexLexicalAnalyzer
{
    public class InvisibleTokenPattern : TokenPattern
    {
        public InvisibleTokenPattern(string pattern): base(pattern)
        {
        }

        public override Func<string, Option<IToken>> TokenFactory => (representation) => Option<IToken>.None();

    }
}
