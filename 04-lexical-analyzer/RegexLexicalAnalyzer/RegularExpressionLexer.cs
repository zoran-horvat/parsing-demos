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

        private IToken EndOfInputToken { get; }

        private Action EndOfRecognitionStep { get; set; }

        private Action<IEnumerable<DottedItem>>  PrintInitialItemSet { get; set; }

        private Action<char, IEnumerable<DottedItem>, Option<IToken>> PrintInputAndItemSet { get; set; }

        private Action<Option<IToken>> PrintReducedToken { get; set; }

        public RegularExpressionLexer(IEnumerable<RegularExpression> rules, string unexpectedInputClass, string endOfInputClass)
        {

            Contract.Requires<ArgumentNullException>(rules != null, "Lexical analyzer rules must not be null.");
            Contract.Requires<ArgumentException>(rules.All(rule => rule != null), "All rules for lexical analyzer must be non-null.");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(unexpectedInputClass), "Class of unexpected input token must be non-empty.");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(endOfInputClass), "Class of end of input token must be non-empty.");

            this.Rules =
                rules
                    .Concat(new RegularExpression[] {new RegularExpression(".", unexpectedInputClass)})
                    .ToList();

            this.EndOfInputToken = new StringClassToken(string.Empty, endOfInputClass);

            this.EndOfRecognitionStep = () => { };
            this.PrintInitialItemSet = (itemSet) => { };
            this.PrintInputAndItemSet = (input, itemSet, token) => { };
            this.PrintReducedToken = (token) => { };

        }

        public void StepByStep()
        {
            this.EndOfRecognitionStep = () =>
            {
                Console.Write("Press ENTER to continue to next step... ");
                Console.ReadLine();
            };

        }

        public void Verbose()
        {

            this.PrintInitialItemSet = (itemSet) =>
                {
                    Console.Write("Initial set: ");
                    itemSet.Print(1000);
                };

            this.PrintInputAndItemSet = (input, currentItemSet, outputToken) =>
                {
                    Console.WriteLine("Input {0} leads to item set:", input);
                    currentItemSet.Print(80);
                    Console.WriteLine("Current best: {0}", outputToken);
                };

            this.PrintReducedToken = (token) =>
                {
                    Console.WriteLine();
                    Console.WriteLine("Reduced {0}", token);
                    Console.WriteLine("------------------");
                    Console.WriteLine();
                };

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

            yield return this.EndOfInputToken;

        }

        private Option<IToken> TryIdentifySingleToken(ITextInput input)
        {

            IEnumerable<DottedItem> currentItemSet =
                this.Rules
                    .SelectMany(DottedItem.InitialSetFor)
                    .Distinct()
                    .ToList();

            IEnumerator<char> lookahead = input.LookAhead.GetEnumerator();

            PrintInitialItemSet(currentItemSet);

            Option<IToken> outputToken = Option<IToken>.None();

            while (currentItemSet.Any() && lookahead.MoveNext())
            {

                currentItemSet =
                    currentItemSet
                        .SelectMany(item => item.MoveOver(lookahead.Current))
                        .Distinct()
                        .ToList();

                Option<IToken> newToken =
                    currentItemSet.SelectMany(item => item.TryReduce()).Take(1).AsOption();

                if (newToken.Any())
                    outputToken = newToken;

                PrintInputAndItemSet(lookahead.Current, currentItemSet, outputToken);

            }

            PrintReducedToken(outputToken);

            this.EndOfRecognitionStep();

            return outputToken;

        }

    }
}
