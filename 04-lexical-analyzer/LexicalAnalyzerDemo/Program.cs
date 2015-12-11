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
    static class Program
    {
        static void Main(string[] args)
        {

            do
            {
                SelectGeneratingFunctions()
                    .Select(tuple => GetTokens(tuple.Item1, tuple.Item2))
                    .ForEach(sequence => sequence.Print(100));
            }
            while (AskForMore());

        }

        private static bool AskForMore()
        {
            Console.Write("Repeat? (Y/N) ");
            return Console.ReadLine().ToUpper() == "Y";
        }

        private static Option<Tuple<Func<ILexicalAnalyzer>, Func<ITextInput>>>  SelectGeneratingFunctions()
        {

            Console.Clear();

            Console.WriteLine("1. Arithmetic expression (driven by regular expressions)");
            Console.WriteLine("2. Arithmetic expression (driven by table)");
            Console.WriteLine("3. C# function (driven by regular expressions)");
            Console.WriteLine("4. C# function (driven by table)");

            Console.WriteLine();
            Console.Write("Enter selection: ");

            string answer = Console.ReadLine();

            Func<ILexicalAnalyzer> lexerFactory;
            Func<ITextInput> inputFactory;
            switch (answer)
            {
                case "1":
                    lexerFactory = CreateArithmeticLexer;
                    inputFactory = CreateArithmeticExpressionInput;
                    break;
                case "2":
                    lexerFactory = CreateArithmeticTableLexer;
                    inputFactory = CreateArithmeticExpressionInput;
                    break;
                case "3":
                    lexerFactory = CreateCSharpFunctionLexer;
                    inputFactory = CreateCSharpFunctionInput;
                    break;
                case "4":
                    lexerFactory = CreateCSharpFunctionTableLexer;
                    inputFactory = CreateCSharpFunctionInput;
                    break;
                default:
                    return Option<Tuple<Func<ILexicalAnalyzer>, Func<ITextInput>>>.None();
            }

            Tuple<Func<ILexicalAnalyzer>, Func<ITextInput>> factories = Tuple.Create(lexerFactory, inputFactory);

            return Option<Tuple<Func<ILexicalAnalyzer>, Func<ITextInput>>>.Some(factories);

        }

        private static IEnumerable<RegularExpression> ArithmeticExpressionRules
        {
            get
            {
                return new[]
                {
                    new RegularExpression("d(d)*", "number"),
                    new RegularExpression("[()]", "parenthesis"),
                    new RegularExpression("[+-*/]", "operator"),
                    new RegularExpression(".", "unexpected-input"), 
                };
            }
        }

        private static RegularExpressionLexer CreateArithmeticLexer()
        {
            RegularExpressionLexer lexer = new RegularExpressionLexer(ArithmeticExpressionRules);
            lexer.Verbose();
            lexer.StepByStep();

            return lexer;
        }

        private static TableDrivenLexer CreateArithmeticTableLexer()
        {

            RegularExpressionLexer parentLexer = CreateArithmeticLexer();

            TransitionTable transitionTable = parentLexer.GetTransitionTable();
            TransitionTableVisualizer visualizer = new TransitionTableVisualizer(transitionTable);

            Console.WriteLine(visualizer.ToString());

            return new TableDrivenLexer(transitionTable);

        }

        private static ITextInput CreateArithmeticExpressionInput() => new ConsoleLineInput("Enter an arithmetic expression: ");

        private static IEnumerable<Token> GetTokens(Func<ILexicalAnalyzer> createLexer, Func<ITextInput> createInput) => 
            createLexer().Analyze(createInput()); 

//string Report(IEnumerable<int> values)
//{   // Supply to C# lexer
//    int sum = 0;    // Initialize sum
//    foreach (int value in values)
//        sum += value * (value + 1) / 2;
//    return string.Format("Sum of sums is {0}", sum);
//}

        private static RegularExpression[] CSharpFunctionRules =>
            new[]
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
                new RegularExpression("[(){}]", "parenthesis"),
                new RegularExpression(".", "unexpected-input"), 
            };

        private static RegularExpressionLexer CreateCSharpFunctionLexer() => new RegularExpressionLexer(CSharpFunctionRules);

        private static TableDrivenLexer CreateCSharpFunctionTableLexer()
        {

            RegularExpressionLexer parentLexer = CreateCSharpFunctionLexer();

            TransitionTable transitionTable = parentLexer.GetTransitionTable();
            TransitionTableVisualizer visualizer = new TransitionTableVisualizer(transitionTable);

            Console.WriteLine(visualizer.ToString());

            return new TableDrivenLexer(transitionTable);

        }

        private static ITextInput CreateCSharpFunctionInput() => new ConsoleBlockInput("Enter block of C# code (end with line containing just -):\n", "-");

    }
}