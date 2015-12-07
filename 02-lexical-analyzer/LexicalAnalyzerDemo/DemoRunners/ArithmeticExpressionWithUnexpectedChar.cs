using Interfaces;
using RegexLexicalAnalyzer;

namespace LexicalAnalyzerDemo.DemoRunners
{
    class ArithmeticExpressionWithUnexpectedChar: ArithmeticExpressionWithRestarts
    {

        public override string Description => "Regex restarting lexer for arithm. expr. with dot pattern to catch errors";

        protected override ILexicalAnalyzer CreateLexer()
        {

            RestartingRegexLexer lexer = (RestartingRegexLexer)base.CreateLexer();

            lexer.Add(new VisibleTokenPattern(".", "unexpected-character"));

            return lexer;

        }
    }
}
