using System;
using ParsingInterfaces;
using System.Diagnostics.Contracts;
using Common;

namespace RegexLexicalAnalyzer
{
    public class StringClassToken : IToken
    {

        public string Representation { get; }

        public string Class { get; }

        public StringClassToken(string representation, string tokenClass)
        {

            Contract.Requires(representation != null, "Token representation must be non-null.");
            Contract.Requires(!string.IsNullOrWhiteSpace(tokenClass), "Token class must be non-empty.");

            this.Representation = representation;
            this.Class = tokenClass;

        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.Class, this.Representation.ToPrintableString());
        }

    }
}
