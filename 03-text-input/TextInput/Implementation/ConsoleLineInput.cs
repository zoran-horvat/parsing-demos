using System;

namespace TextInput.Implementation
{
    public class ConsoleLineInput: ConsoleInput
    {
        public ConsoleLineInput(string prompt) : base(prompt)
        {
        }

        protected override string ReadText()
        {
            return Console.ReadLine();
        }
    }
}
