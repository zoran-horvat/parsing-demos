using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ParsingInterfaces.Contracts
{
    [ContractClassFor(typeof(ILexicalAnalyzer))]
    internal abstract class LexicalAnalyzerContract : ILexicalAnalyzer
    {
        IEnumerable<Token> ILexicalAnalyzer.Analyze(ITextInput input)
        {

            Contract.Requires<ArgumentNullException>(input != null, "Text input must be non-null.");

            Contract.Ensures(Contract.Result<IEnumerable<Token>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<Token>>().All(token => token != null));

            return new Token[0];

        }
    }
}
