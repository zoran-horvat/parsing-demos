using ParsingInterfaces.Contracts;
using System.Diagnostics.Contracts;

namespace ParsingInterfaces
{
    [ContractClass(typeof(TokenContract))]
    public interface IToken
    {
        string Representation { get; }
        int InputRow { get; }
        int InputColumn { get; }
    }
}
