using Common;
using Interfaces;
using RegexLexicalAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LexicalAnalyzerDemo
{

    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                IEnumerable<char> input = new ConsoleInput().ToList();

                Console.WriteLine();
                Console.Write("Input: ");
                Print(input, string.Empty);

                ILexicalAnalyzer lexer = CreateLexer();
                IEnumerable<IToken> tokens = lexer.Analyze(input);

                Console.WriteLine();
                Print(tokens, Environment.NewLine);

                if (!ReadMore())
                    break;

            }

        }

        static void Print<T>(IEnumerable<T> sequence, string delimiter)
        {
            Console.WriteLine(string.Join(delimiter, sequence.ToArray()));
        }

        static ILexicalAnalyzer CreateLexer()
        {

            RestartingRegexLexer lexer = new RestartingRegexLexer(new InvisibleStringClassToken("eof"));

            lexer.Add(new VisibleTokenPattern("[0-9]+", "number"));
            lexer.Add(new VisibleTokenPattern(@"[\(\)]", "parenthesis"));
            lexer.Add(new VisibleTokenPattern(@"[\+\-\*\/]", "operator"));
            lexer.Add(new InvisibleTokenPattern(@"\s+"));

            return lexer;

        }

        static bool ReadMore()
        {

            Console.WriteLine();

            while (true)
            {
                Option<bool> answer = TryAskToReadMore();
                if (answer.Any())
                    return answer.Single();
            }

        }

        static Option<bool> TryAskToReadMore()
        {

            Console.Write("Try again? (Y/N) ");

            string answer = Console.ReadLine().Trim().ToUpper();

            if (answer == "Y")
                return Option<bool>.Some(true);

            if (answer == "N")
                return Option<bool>.Some(false);

            return Option<bool>.None();

        }

    }
}
