using System.Collections.Generic;

namespace LexicalAnalyzerDemo.DemoRunners
{
    abstract class ArithmeticExpressionLexerBase: DemoRunnerBase
    {
        protected override IEnumerable<char> InputStream =>
            new ConsoleLineInput("",
                                 "Enter full arithmetic expression.",
                                 "Allowed elements: integer numbers, parentheses, operators (+, -, *, /).");
    }
}
