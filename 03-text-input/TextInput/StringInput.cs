using ParsingInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace TextInput
{
    public class StringInput: ITextInput
    {

        private string Input { get; }
        private int Position { get; set; }

        public StringInput(string input)
        {
            Contract.Requires<ArgumentNullException>(input != null, "Input must not be null.");
            this.Input = input;
        }

        public IEnumerable<char> LookAhead
        {
            get
            {
                int curPos = this.Position;
                while (curPos < this.Input.Length)
                {
                    yield return this.Input[curPos];
                    curPos++;
                }
            }
        }

        public int CharactersRemaining => this.Input.Length - this.Position;

        public void Advance(int charactersToSkip) => this.Position += charactersToSkip;

    }
}
