using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TextInput.Interfaces;

namespace TextInput.Implementation
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

        public void Advance(int positionsToSkip)
        {
            Contract.Requires<ArgumentException>(positionsToSkip >= 0, "Number of positionsToSkip to advance must be non-negative.");
            Contract.Requires<ArgumentException>(this.Position + positionsToSkip <= this.Input.Length, "Must not advance beyond end of input.");

            this.Position += positionsToSkip;

        }
    }
}
