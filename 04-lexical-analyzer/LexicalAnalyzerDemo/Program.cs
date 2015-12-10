using Common;
using ParsingInterfaces;
using RegexLexicalAnalyzer.RegexLexer;
using RegexLexicalAnalyzer.TableLexer;
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

            GetExpressionTokens().Print(100);

            //GetCSharpFunctionTokens().Print(100);

            Console.Write("Press ENTER to exit... ");
            Console.ReadLine();

        }

        private static IEnumerable<RegularExpression> ArithmeticExpressionRules
        {
            get
            {
                return new[]
                {
                    new RegularExpression("d(d)*", "number"),
                    new RegularExpression("[()]", "parenthesis"),
                    new RegularExpression("[+-*/]", "operator")
                };
            }
        }

        private static RegularExpressionLexer CreateArithmeticExpressionLexer()
        {
            RegularExpressionLexer lexer = new RegularExpressionLexer(ArithmeticExpressionRules, "unexpected-input", "end-of-input");

            TransitionTable transitionTable = lexer.GetTransitionTable();
            transitionTable.GetAllTransitions().OrderBy(entry => entry.Item1).ThenBy(entry => entry.Item2).Select(entry => $"({entry.Item1}, {entry.Item2}) -> {entry.Item3}").Print(1);

            return lexer;
        }

        private static IEnumerable<IToken> GetExpressionTokens()
        {

            RegularExpressionLexer lexer = CreateArithmeticExpressionLexer();
            lexer.Verbose();
            lexer.StepByStep();

            ITextInput input = new ConsoleLineInput("Enter an arithmetic expression: ");

            return lexer.Analyze(input);

        }

string Report(IEnumerable<int> values)
{   // Supply to C# lexer
    int sum = 0;    // Initialize sum
    foreach (int value in values)
        sum += value * (value + 1) / 2;
    return string.Format("Sum of sums is {0}", sum);
}

        private static IEnumerable<IToken> GetCSharpFunctionTokens()
        {

            RegularExpression[] rules = new[]
            {
                new RegularExpression("d(d)*", "number"), 
                new RegularExpression("a(a)*", "symbol"), 
                new RegularExpression("s(s)*", "space"), 
                new RegularExpression("[\r][\n]", "new-line"),
                new RegularExpression("[\n]", "new-line"),
                new RegularExpression("[/][/](.*)", "comment"),
                new RegularExpression("[=]", "assignment"),
                new RegularExpression("[+-*/<>]", "operator"),
                new RegularExpression("[+][=]", "operator"),
                new RegularExpression("[.,;]", "punctuator"),
                new RegularExpression("[\"](.*)[\"]", "string"), 
                new RegularExpression("[(){}]", "parenthesis")
            };

            RegularExpressionLexer lexer = new RegularExpressionLexer(rules, "unexpected-input", "end-of-input");

            TransitionTable transitionTable = lexer.GetTransitionTable();
            transitionTable.GetAllTransitions().OrderBy(entry => entry.Item1).ThenBy(entry => entry.Item2).Select(entry => $"({entry.Item1}, {entry.Item2}) -> {entry.Item3}").Print(1);

            ITextInput input = new ConsoleBlockInput("Enter block of C# code (end with line containing just -):\n", "-");

            input.LookAhead.Select(c => c.ToPrintableString()).Print(80);
            return lexer.Analyze(input);

        } 
    }
}