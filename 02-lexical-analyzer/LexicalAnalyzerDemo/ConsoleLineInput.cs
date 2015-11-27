using System;
using System.Collections;
using System.Collections.Generic;

namespace LexicalAnalyzerDemo
{
    class ConsoleLineInput: IEnumerable<char>
    {

        private string Input { get; set; }
        private IEnumerable<string> QuestionLines { get; }

        private Action LoadInputIfNotLoaded { get; set; }
 
        public ConsoleLineInput(params string[] questionLines)
        {
            this.LoadInputIfNotLoaded = this.PrepareInput;
            this.QuestionLines = new List<string>(questionLines);
        }

        private void PrepareInput()
        {

            foreach (string questionLine in this.QuestionLines)
                Console.WriteLine(questionLine);

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
