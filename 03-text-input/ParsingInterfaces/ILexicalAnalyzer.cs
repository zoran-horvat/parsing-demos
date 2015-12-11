using ParsingInterfaces.Contracts;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ParsingInterfaces
{
    [ContractClass(typeof(LexicalAnalyzerContract))]
    public interface ILexicalAnalyzer
    {
        IEnumerable<Token> Analyze(ITextInput input);
    }
}
