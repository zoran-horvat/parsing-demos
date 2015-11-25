using System;
using Interfaces;
using Common;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer
{
    public class VisibleTokenPattern : TokenPattern
    {
        public string ClassName { get; }

        public VisibleTokenPattern(string pattern, string className): base(pattern)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(className), "Token class name must be non-empty.");
            this.ClassName = className;
        }

        public override Func<string, Option<IToken>> TokenFactory => (representation) => Option<IToken>.Some(new StringClassToken(this.ClassName, representation));

    }
}
