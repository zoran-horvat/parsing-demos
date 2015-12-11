using System;
using System.Diagnostics.Contracts;

namespace TextInput
{
    public class ConsoleLineInput: ConsoleInput
    {
        public ConsoleLineInput(string prompt) : base(prompt)
        {
        }

        protected override string ReadText()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return Console.ReadLine();
        }
    }
}
