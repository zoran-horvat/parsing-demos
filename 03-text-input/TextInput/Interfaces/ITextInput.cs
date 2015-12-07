using System.Collections.Generic;

namespace TextInput.Interfaces
{
    public interface ITextInput
    {
        IEnumerable<char> LookAhead { get; }
        void Advance(int positionsToSkip);
    }
}
