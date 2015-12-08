using Common;
using ParsingInterfaces;
using RegexLexicalAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using TextInput;

namespace LexicalAnalyzerDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] rules = new[]
            {
                "d(d*)"
            };

            ILexicalAnalyzer lexer = new RegularExpressionLexer(rules.Select(rule => new RegularExpression(rule)));

            ITextInput input = new ConsoleLineInput("Enter arithmetic expression: ");

            IEnumerable<IToken> tokens = lexer.Analyze(input);

            tokens.Print(80);

            Console.Write("Press ENTER to exit... ");
            Console.ReadLine();

        }
    }
}