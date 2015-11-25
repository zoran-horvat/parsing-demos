using System;
using System.Collections;
using System.Collections.Generic;

namespace LexicalAnalyzerDemo
{
    class ConsoleInput: IEnumerable<char>
    {

        private string Input { get; set; }

        private Action LoadInputIfNotLoaded { get; set; }
 
        public ConsoleInput()
        {
            this.LoadInputIfNotLoaded = this.PrepareInput;
        }

        private void PrepareInput()
        {

            Console.WriteLine("Enter full arithmetic expression.");
            Console.WriteLine("Allowed elements: integer numbers, parentheses, operators (+, -, *, /).");

            this.Input = Console.ReadLine();
            this.LoadInputIfNotLoaded = () => { };

        }

        public IEnumerator<char> GetEnumerator()
        {
            this.LoadInputIfNotLoaded();
            return this.Input.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }
}
