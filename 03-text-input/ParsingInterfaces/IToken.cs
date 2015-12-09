using ParsingInterfaces.Contracts;
using System.Diagnostics.Contracts;

namespace ParsingInterfaces
{
    [ContractClass(typeof(TokenContract))]
    public interface IToken
    {
        string Representation { get; }
        string Class { get; }
    }
}
