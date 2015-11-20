using System;
using System.Collections.Generic;
using System.Linq;
using RecursiveDescentDemo.Common;
using RecursiveDescentDemo.Implementation;
using RecursiveDescentDemo.Interfaces;
using RecursiveDescentDemo.Implementation.Generators;

namespace RecursiveDescentDemo
{
    class Program
    {

        static void Main(string[] args)
        {

            while (true)
            {
                SingleRun();
                if (!Continue())
                    break;
            }

        }

        private static void SingleRun()
        {
            try
            {

                IEnumerable<char> input = new ConsoleInputStream();
                ILexicalAnalyzer lexer = new SimpleLexicalAnalyzer();
                IParser parser = new RecursiveDescentParser();

                IEnumerable<IToken> tokens = lexer.Analyze(input);

                Option<IAbstractSyntaxTree> ast = parser.TryParse(tokens);

                ProcessTree(ast);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {0}", ex.Message);
            }
        }

        private static void ProcessTree(Option<IAbstractSyntaxTree> ast)
        {
            ProcessTree(ast, new TextGenerator());
            ProcessTree(ast, new ExpressionValueCalculator());

        }

        private static void ProcessTree(Option<IAbstractSyntaxTree> ast, IGenerator generator)
        {

            if (!ast.Any())
                return;

            generator.Process(ast.Single());

        }

        private static bool Continue()
        {
            Console.Write("Repeat? (Y/N): ");
            return GetYesNoAnswer();
        }

        private static bool GetYesNoAnswer()
        {
            while (true)
            {

                string input = Console.ReadLine().Trim().ToUpper();

                if (input == "Y")
                    return true;

                if (input == "N")
                    return false;

            }
        }
    }
}
