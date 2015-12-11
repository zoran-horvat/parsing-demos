using Common;
using ParsingInterfaces;
using RegexLexicalAnalyzer.RegexLexer;
using RegexLexicalAnalyzer.TableLexer;
using System;
using System.Collections.Generic;
using TextInput;

namespace LexicalAnalyzerDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //GetExpressionTokens().Print(100);

            GetCSharpFunctionTokens().Print(100);

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
            lexer.Verbose();
            lexer.StepByStep();

            return lexer;
        }

        private static TableDrivenLexer CreateArithmeticExpressionTableLexer()
        {

            RegularExpressionLexer parentLexer = CreateArithmeticExpressionLexer();

            TransitionTable transitionTable = parentLexer.GetTransitionTable();
            TransitionTableVisualizer visualizer = new TransitionTableVisualizer(transitionTable);

            Console.WriteLine(visualizer.ToString());

            return new TableDrivenLexer(transitionTable);
        }

        private static IEnumerable<IToken> GetExpressionTokens()
        {

            ITextInput input = new ConsoleLineInput("Enter an arithmetic expression: ");

            ILexicalAnalyzer lexer = CreateArithmeticExpressionTableLexer();

            return lexer.Analyze(input);

        }

//string Report(IEnumerable<int> values)
//{   // Supply to C# lexer
//    int sum = 0;    // Initialize sum
//    foreach (int value in values)
//        sum += value * (value + 1) / 2;
//    return string.Format("Sum of sums is {0}", sum);
//}

        private static RegularExpressionLexer GetCSharpFunctionLexer()
        {

            RegularExpression[] rules = new[]
            {
                new RegularExpression("d(d)*", "number"),
                new RegularExpression("a(a)*", "symbol"),
                new RegularExpression("s(s)*", "space"),
                new RegularExpression("[\r][\n]", "new-line"),
                new RegularExpression("[\n]", "new-line"),
                new RegularExpression("[/][/](.)*", "comment"),
                new RegularExpression("[=]", "assignment"),
                new RegularExpression("[+-*/<>]", "operator"),
                new RegularExpression("[+][=]", "operator"),
                new RegularExpression("[.,;]", "punctuator"),
                new RegularExpression("[\"](.)*[\"]", "string"),
                new RegularExpression("[(){}]", "parenthesis")
            };

            return new RegularExpressionLexer(rules, "unexpected-input", "end-of-input");

        }

        private static TableDrivenLexer GetCSharpFunctionTableLexer()
        {

            RegularExpressionLexer parentLexer = GetCSharpFunctionLexer();

            TransitionTable transitionTable = parentLexer.GetTransitionTable();
            TransitionTableVisualizer visualizer = new TransitionTableVisualizer(transitionTable);

            Console.WriteLine(visualizer.ToString());

            return new TableDrivenLexer(transitionTable);

        }

        private static IEnumerable<IToken> GetCSharpFunctionTokens()
        {

            ILexicalAnalyzer lexer = GetCSharpFunctionTableLexer();

            ITextInput input = new ConsoleBlockInput("Enter block of C# code (end with line containing just -):\n", "-");

            return lexer.Analyze(input);

        } 
    }
}