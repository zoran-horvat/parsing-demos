using System.Collections.Generic;
using Interfaces;
using System.Diagnostics.Contracts;
using System;
using System.Linq;
using Common;

namespace RegexLexicalAnalyzer
{
    public class RestartingRegexLexer : ILexicalAnalyzer
    {
        private IToken EndOfFile { get; }
        private IList<TokenPattern> Patterns { get; } = new List<TokenPattern>();

        public RestartingRegexLexer(IToken eof)
        {
            Contract.Requires<ArgumentNullException>(eof != null);
            this.EndOfFile = eof;
        }

        public void Add(TokenPattern pattern)
        {
            Contract.Requires<ArgumentNullException>(pattern != null);
            this.Patterns.Add(pattern);
        }

        public IEnumerable<IToken> Analyze(IEnumerable<char> source)
        {

            string input = new string(source.ToArray());
            int currentPos = 0;

            while (input.Length > 0)
            {

                Option<TokenMatch> potentialMatch = TryParseNext(input);

                if (!potentialMatch.Any())
                {
                    Console.WriteLine("Failed to process input at position {0}.", currentPos);
                    yield break;
                }

                TokenMatch match = potentialMatch.Single();

                currentPos += match.RepresentationLength;
                input = input.Substring(match.RepresentationLength);

                foreach (IToken token in match.CreateToken())
                    yield return token;

            }

            yield return this.EndOfFile;

        }

        private Option<TokenMatch> TryParseNext(string input)
        {
            return
                this.Patterns
                .SelectMany(
                    (pattern, ordinal) =>
                    pattern
                        .TryMatch(input)
                        .Select(match => new
                        {
                            Match = match,
                            Ordinal = ordinal,
                            Length = match.RepresentationLength
                        }))
                .OrderByDescending(tuple => tuple.Length)
                .ThenBy(tuple => tuple.Ordinal)
                .Take(1)
                .Select(tuple => tuple.Match)
                .AsOption();

        }
    }
}
