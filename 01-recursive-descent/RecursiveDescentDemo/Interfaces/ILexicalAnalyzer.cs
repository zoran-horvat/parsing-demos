using System.Collections.Generic;

namespace RecursiveDescentDemo.Interfaces
{
    public interface ILexicalAnalyzer
    {
        IEnumerable<IToken> Analyze(IEnumerable<char> input);
    }
}
