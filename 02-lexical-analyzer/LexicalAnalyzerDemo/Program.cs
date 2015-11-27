using Common;
using System;
using System.Linq;

namespace LexicalAnalyzerDemo
{

    class Program
    {

        static void Main(string[] args)
        {

            RunnerSelector selector = new RunnerSelector();
            ConsoleReader reader = new ConsoleReader();

            while (true)
            {

                selector.SelectRunner().Run().Print(80);

                if (!reader.AskYesNoQuestion("Try again?"))
                    break;

            }

        }

    }
}
