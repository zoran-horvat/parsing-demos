using System;
using System.Collections.Generic;
using ParsingInterfaces;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;

namespace RegexLexicalAnalyzer
{
    public class RegularExpressionLexer : ILexicalAnalyzer
    {

        private IEnumerable<RegularExpression> Rules { get; }

        public RegularExpressionLexer(IEnumerable<RegularExpression> rules)
        {

            Contract.Requires<ArgumentNullException>(rules != null, "Lexical analyzer rules must not be null.");
            Contract.Requires<ArgumentException>(rules.All(rule => rule != null), "All rules for lexical analyzer must be non-null.");

            this.Rules = new List<RegularExpression>(rules);

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

            IEnumerable<DottedItem> currentItemSet =
                this.Rules
                    .SelectMany(DottedItem.InitialSetFor)
                    .Distinct()
                    .ToList();

            IEnumerator<char> lookahead = input.LookAhead.GetEnumerator();

            Console.Write("Initial set: ");
            currentItemSet.Print(1000);

            Option<IToken> outputToken = Option<IToken>.None();

            while (currentItemSet.Any() && lookahead.MoveNext())
            {

                Console.WriteLine("Input {0} leads to item set:", lookahead.Current);

                currentItemSet =
                    currentItemSet
                        .SelectMany(item => item.MoveOver(lookahead.Current))
                        .Distinct()
                        .ToList();

                Option<IToken> newToken =
                    currentItemSet.SelectMany(item => item.TryReduce()).Take(1).AsOption();

                if (newToken.Any())
                    outputToken = newToken;

                currentItemSet.Print(80);
                Console.WriteLine("Current best: {0}", outputToken);
                
            }

            Console.WriteLine();
            Console.WriteLine("Reduced {0}", outputToken);
            Console.WriteLine("------------------");
            Console.WriteLine();

            //Console.Write("Press ENTER to continue to next step... ");
            //Console.ReadLine();

            return outputToken;

        }

    }
}
