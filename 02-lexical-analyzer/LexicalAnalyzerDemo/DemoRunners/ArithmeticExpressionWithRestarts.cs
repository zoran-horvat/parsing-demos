using Interfaces;
using RegexLexicalAnalyzer;

namespace LexicalAnalyzerDemo.DemoRunners
{
    class ArithmeticExpressionWithRestarts : ArithmeticExpressionLexerBase
    {

        public override string Description => "Regex restarting lexer for arithmetic expression";

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
