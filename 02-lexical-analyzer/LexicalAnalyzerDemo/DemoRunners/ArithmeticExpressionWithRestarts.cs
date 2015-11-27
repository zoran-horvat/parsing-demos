using System.Collections.Generic;
using Interfaces;
using RegexLexicalAnalyzer;

namespace LexicalAnalyzerDemo.DemoRunners
{
    class ArithmeticExpressionWithRestarts : DemoRunnerBase
    {

        public override string Description => "Regex restarting lexer for arithmetic expression";

        protected override IEnumerable<char> InputStream => 
            new ConsoleLineInput("",
                                 "Enter full arithmetic expression.",
                                 "Allowed elements: integer numbers, parentheses, operators (+, -, *, /).");

        protected override ILexicalAnalyzer CreateLexer()
        {

            RestartingRegexLexer lexer = new RestartingRegexLexer(new InvisibleStringClassToken("eof"));

            lexer.Add(new VisibleTokenPattern("[0-9]+", "number"));
            lexer.Add(new VisibleTokenPattern(@"[\(\)]", "parenthesis"));
            lexer.Add(new VisibleTokenPattern(@"[\+\-\*\/]", "operator"));
            lexer.Add(new InvisibleTokenPattern(@"\s+"));

            return lexer;

        }
    }
}
