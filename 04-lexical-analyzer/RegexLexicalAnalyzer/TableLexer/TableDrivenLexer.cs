using System;
using System.Collections.Generic;
using ParsingInterfaces;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer
{
    public class TableDrivenLexer : ILexicalAnalyzer
    {

        private Dictionary<Tuple<int, char>, int> TransitionTable { get; }

        public TableDrivenLexer(Dictionary<Tuple<int, char>, int> transitionTable)
        {
            Contract.Requires<ArgumentNullException>(transitionTable != null, "Transition table must be non-null.");
            this.TransitionTable = transitionTable;
        }

        public IEnumerable<IToken> Analyze(ITextInput input)
        {

            return new IToken[0];

        }
    }
}
