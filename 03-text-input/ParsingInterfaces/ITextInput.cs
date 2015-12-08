using ParsingInterfaces.Contracts;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ParsingInterfaces
{
    [ContractClass(typeof(TextInputContract))]
    public interface ITextInput
    {
        IEnumerable<char> LookAhead { get; }
        void Advance(int charactersToSkip);
        int CharactersRemaining { get; }
    }
}
