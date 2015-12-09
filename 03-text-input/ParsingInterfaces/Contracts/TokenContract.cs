using System;
using System.Diagnostics.Contracts;

namespace ParsingInterfaces.Contracts
{
    [ContractClassFor(typeof(IToken))]
    internal abstract class TokenContract : IToken
    {
        string IToken.Representation
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return string.Empty;
            }
        }

        public string Class
        {
            get
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
                return string.Empty;
            }
        }

    }
}
