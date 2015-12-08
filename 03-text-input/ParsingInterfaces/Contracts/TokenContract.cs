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

        int IToken.InputColumn
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return 0;
            }
        }

        int IToken.InputRow
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return 0;
            }
        }

    }
}
