using System;
using System.Collections.Generic;
using ParsingInterfaces;
using System.Diagnostics.Contracts;
using Common;
using System.Linq;
using System.Text;

namespace RegexLexicalAnalyzer.TableLexer
{
    public class TableDrivenLexer : ILexicalAnalyzer
    {

        private TransitionTable Table { get; }

        public TableDrivenLexer(TransitionTable transitionTable)
        {
            Contract.Requires<ArgumentNullException>(transitionTable != null, "Transition table must be non-null.");
            this.Table = transitionTable;
        }

        public IEnumerable<IToken> Analyze(ITextInput input)
        {

            while (input.CharactersRemaining > 0)
            {

                Option<IToken> possibleToken = TryIdentifySingleToken(input);

                if (!possibleToken.Any())
                    yield break;

                IToken token = possibleToken.Single();

                input.Advance(token.Representation.Length);
                yield return token;

            }

        }

        private Option<IToken> TryIdentifySingleToken(ITextInput input)
        {

            StringBuilder recognizedInput = new StringBuilder();
            int state = 0;
            IEnumerator<char> lookahead = input.LookAhead.GetEnumerator();
            Option<string> longestTokenClass = Option<string>.None();
            int longestTokenLength = 0;

            while (lookahead.MoveNext() && this.Table.ContainsTransition(state, lookahead.Current))
            {

                recognizedInput.Append(lookahead.Current);
                state = this.Table.GetTransition(state, lookahead.Current);

                this
                    .Table
                    .TryGetReduction(state)
                    .ForEach(tokenClass =>
                        {
                            longestTokenClass = Option<string>.Some(tokenClass);
                            longestTokenLength = recognizedInput.Length;
                        });

            }

            recognizedInput.Length = longestTokenLength;

            return 
                longestTokenClass
                    .Select(tokenClass => new StringClassToken(recognizedInput.ToString(), tokenClass))
                    .Select(token => Option<IToken>.Some(token))
                    .DefaultIfEmpty(Option<IToken>.None())
                    .Single();

        }
    }
}
