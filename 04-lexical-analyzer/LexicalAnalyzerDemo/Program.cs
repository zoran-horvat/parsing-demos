using Common;
using ParsingInterfaces;
using RegexLexicalAnalyzer;
using System;
using System.Collections.Generic;
using TextInput;

namespace LexicalAnalyzerDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            RegularExpression[] rules = new[]
            {
                new RegularExpression("d", "digit"),
                new RegularExpression("[()]", "parenthesis"),
                new RegularExpression("[+-*/]", "operator"), 
                new RegularExpression(".", "unexpected-input")
            };

            ILexicalAnalyzer lexer = new RegularExpressionLexer(rules);

            ITextInput input = new ConsoleLineInput("Enter a digit: ");

            IEnumerable<IToken> tokens = lexer.Analyze(input);

            tokens.Print(80);

            Console.Write("Press ENTER to exit... ");
            Console.ReadLine();

        }
    }
}