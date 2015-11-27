using Interfaces;
using System.Collections.Generic;

namespace LexicalAnalyzerDemo
{
    abstract class DemoRunnerBase
    {

        public abstract string Description { get; }
        protected abstract IEnumerable<char> InputStream { get; }

        public IEnumerable<IToken> Run()
        {
            return this.CreateLexer().Analyze(this.InputStream);
        }

        protected abstract ILexicalAnalyzer CreateLexer();

    }
}
