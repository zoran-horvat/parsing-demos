using System.Collections.Generic;

namespace Interfaces
{
    public interface ILexicalAnalyzer
    {
        IEnumerable<IToken> Analyze(IEnumerable<char> source);
    }
}
