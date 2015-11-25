using System;
using System.Diagnostics.Contracts;

namespace Interfaces
{
    public class StringClassToken: IToken
    {

        public string Class { get; }
        public string Representation { get; }

        private string PrintableRepresentation => this.Representation.Replace("\n", "\\n").Replace("\r", "\\r");

        public StringClassToken(string tokenClass, string representation)
        {

            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(tokenClass), "Token class must be non-empty.");
            Contract.Requires<ArgumentNullException>(representation != null, "Token representation must be non-null");

            this.Class = tokenClass;
            this.Representation = representation;

        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.Class, this.PrintableRepresentation);
        }
    }
}
