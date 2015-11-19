using System;
using System.Collections;
using System.Collections.Generic;

namespace RecursiveDescentDemo.Implementation
{
    public class ConsoleInputStream: IEnumerable<Char>
    {

        private string Input { get; set; }

        private Func<string> GetInput { get; set; }

        public ConsoleInputStream()
        {
            this.GetInput = this.ReadNewInput;
        }

        private string ReadNewInput()
        {

            Console.WriteLine("Enter fully parenthesized expression:");
            this.Input = Console.ReadLine();

            this.GetInput = this.GetExistingInput;

            return this.Input;

        }

        private string GetExistingInput()
        {
            return this.Input;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return this.GetInput().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
