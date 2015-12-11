using ParsingInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace TextInput
{
    public abstract class ConsoleInput: ITextInput
    {

        private ITextInput basicInput;
        private string Prompt { get; }

        private Func<ITextInput> GetBasicTextInput { get; set; }

        protected ConsoleInput(string prompt)
        {
            Contract.Requires<ArgumentNullException>(prompt != null, "Prompt must not be null.");
            this.Prompt = prompt;
            this.GetBasicTextInput = this.InitializeInput;
        }

        private ITextInput InitializeInput()
        {

            this.PromptUser();

            string text = this.ReadText();
            this.basicInput = new StringInput(text);

            this.GetBasicTextInput = () => this.basicInput;

            return this.basicInput;

        }

        private void PromptUser()
        {
            if (!string.IsNullOrEmpty(this.Prompt))
                Console.Write(this.Prompt);
        }

        public IEnumerable<char> LookAhead => this.GetBasicTextInput().LookAhead;

        public int CharactersRemaining => this.GetBasicTextInput().CharactersRemaining;

        public void Advance(int positionsToSkip) => this.GetBasicTextInput().Advance(positionsToSkip);

        protected abstract string ReadText();

    }
}
