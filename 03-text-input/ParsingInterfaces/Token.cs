using ParsingInterfaces.Contracts;
using System.Diagnostics.Contracts;
using Common;

namespace ParsingInterfaces
{
    public class Token
    {

        private readonly string representation;
        private readonly string tokenClass;

        public string Representation
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.representation;
            }
        }

        public string Class
        {
            get
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
                return this.tokenClass;
            }
        }

        public Token(string representation, string tokenClass)
        {

            Contract.Requires(representation != null, "Token representation must be non-null.");
            Contract.Requires(!string.IsNullOrWhiteSpace(tokenClass), "Token class must be non-empty.");

            this.representation = representation;
            this.tokenClass = tokenClass;

        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.Class, this.Representation.ToPrintableString());
        }
    }
}
