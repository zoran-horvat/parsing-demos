using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace TextInput.Implementation
{
    public class ConsoleBlockInput: ConsoleInput
    {
        private string EndOfInput { get; }

        public ConsoleBlockInput(string prompt, string endOfInput) : base(prompt)
        {
            Contract.Requires<ArgumentNullException>(endOfInput != null, "End of input indicator must not be null.");
            this.EndOfInput = endOfInput;
        }

        protected override string ReadText()
        {

            StringBuilder sb = new StringBuilder();

            foreach (string line in this.ReadInputLines())
                sb.AppendLine(line);

            return sb.ToString();

        }

        private IEnumerable<string> ReadInputLines()
        {
            while (true)
            {

                string line = Console.ReadLine();

                if (line == this.EndOfInput)
                    yield break;

                yield return line;

            }
        }
    }
}
