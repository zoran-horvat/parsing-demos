using Common;
using System;
using System.Linq;

namespace LexicalAnalyzerDemo
{
    class ConsoleReader
    {
        public bool AskYesNoQuestion(string question)
        {

            Console.WriteLine();

            while (true)
            {
                Option<bool> answer = TryAskYesNoQuestion(question);
                if (answer.Any())
                    return answer.Single();
            }

        }

        static Option<bool> TryAskYesNoQuestion(string question)
        {

            Console.Write("{0} (Y/N) ", question);

            string answer = Console.ReadLine().Trim().ToUpper();

            if (answer == "Y")
                return Option<bool>.Some(true);

            if (answer == "N")
                return Option<bool>.Some(false);

            return Option<bool>.None();

        }
    }
}
