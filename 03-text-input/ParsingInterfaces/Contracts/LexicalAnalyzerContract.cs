using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ParsingInterfaces.Contracts
{
    [ContractClassFor(typeof(ILexicalAnalyzer))]
    internal abstract class LexicalAnalyzerContract : ILexicalAnalyzer
    {
        IEnumerable<IToken> ILexicalAnalyzer.Analyze(ITextInput input)
        {

            Contract.Requires<ArgumentNullException>(input != null, "Text input must be non-null.");

            Contract.Ensures(Contract.Result<IEnumerable<IToken>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<IToken>>().All(token => token != null));

            return new IToken[0];

        }
    }
}
