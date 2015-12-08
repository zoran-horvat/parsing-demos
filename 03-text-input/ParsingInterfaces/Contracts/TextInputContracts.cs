using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ParsingInterfaces.Contracts
{
    [ContractClassFor(typeof(ITextInput))]
    internal abstract class TextInputContracts : ITextInput
    {

        void ITextInput.Advance(int charactersToSkip)
        {
            Contract.Requires<ArgumentException>(charactersToSkip >= 0, "Number of characters to skip must be non-negative.");
            Contract.Requires<ArgumentException>(charactersToSkip <= this.CharactersRemaining, "Cannot skip beyond end of input.");
        }

        IEnumerable<char> ITextInput.LookAhead => new char[0];

        public int CharactersRemaining => 0;

    }
}
