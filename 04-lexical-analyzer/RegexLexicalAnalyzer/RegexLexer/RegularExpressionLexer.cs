using System;
using System.Collections.Generic;
using ParsingInterfaces;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;
using RegexLexicalAnalyzer.TableLexer;

namespace RegexLexicalAnalyzer.RegexLexer
{
    public class RegularExpressionLexer : ILexicalAnalyzer
    {

        private IEnumerable<RegularExpression> Rules { get; }

        private DottedItemSet InitialSet => this.Rules.SelectMany(DottedItem.InitialSetFor).Distinct().AsItemSet();

        private string CalculateAlphabet() => new string(this.Rules.SelectMany(rule => rule.GetAlphabet()).OrderBy(c => c).ToArray());

        private Token EndOfInputToken { get; }

        private Action EndOfRecognitionStep { get; set; }

        private Action<DottedItemSet>  PrintInitialItemSet { get; set; }

        private Action<char, DottedItemSet, Option<Token>> PrintInputAndItemSet { get; set; }

        private Action<Option<Token>> PrintReducedToken { get; set; }

        public RegularExpressionLexer(IEnumerable<RegularExpression> rules)
        {

            Contract.Requires<ArgumentNullException>(rules != null, "Lexical analyzer rules must not be null.");
            Contract.Requires<ArgumentException>(rules.All(rule => rule != null), "All rules for lexical analyzer must be non-null.");

            this.Rules = rules.ToList();

            this.EndOfInputToken = new Token(string.Empty, "end-of-input");

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

        public IEnumerable<Token> Analyze(ITextInput input)
        {

            while (input.CharactersRemaining > 0)
            {

                Option<Token> possibleToken = TryIdentifySingleToken(input);

                if (!possibleToken.Any())
                    yield break;

                Token token = possibleToken.Single();

                input.Advance(token.Representation.Length);
                yield return token;

            }

            yield return this.EndOfInputToken;

        }

        private Option<Token> TryIdentifySingleToken(ITextInput input)
        {

            DottedItemSet currentItemSet = this.InitialSet;

            IEnumerator<char> lookahead = input.LookAhead.GetEnumerator();

            PrintInitialItemSet(currentItemSet);

            Option<Token> outputToken = Option<Token>.None();

            while (currentItemSet.Any() && lookahead.MoveNext())
            {

                currentItemSet = currentItemSet.GetFollowingSet(lookahead.Current);

                currentItemSet
                    .TryReduce()
                    .ForEach(token => outputToken = Option<Token>.Some(token));

                PrintInputAndItemSet(lookahead.Current, currentItemSet, outputToken);

            }

            PrintReducedToken(outputToken);

            this.EndOfRecognitionStep();

            return outputToken;

        }

        public TransitionTable GetTransitionTable()
        {

            string alphabet = this.CalculateAlphabet();

            DottedItemSet currentState = this.InitialSet;

            DottedItemSetIndex states = new DottedItemSetIndex();

            states.Add(currentState);

            Queue<int> unexpandedStates = new Queue<int>();
            unexpandedStates.Enqueue(0);

            TransitionTable transitionTable = new TransitionTable();

            while (unexpandedStates.Count > 0)
            {

                int currentStateIndex = unexpandedStates.Dequeue();
                currentState = states.GetItemSet(currentStateIndex);

                foreach (char input in alphabet)
                {

                    DottedItemSet nextState = currentState.GetFollowingSet(input);

                    if (nextState.IsEmpty)
                        continue;

                    int nextStateIndex;

                    if (states.Contains(nextState))
                    {
                        nextStateIndex = states.GetIndexFor(nextState);
                    }
                    else
                    {
                        states.Add(nextState);
                        nextStateIndex = states.GetIndexFor(nextState);
                        unexpandedStates.Enqueue(nextStateIndex);
                    }

                    transitionTable.SetTransition(currentStateIndex, input, nextStateIndex);

                    nextState
                        .TryReduceTokenClass()
                        .ForEach((tokenClass) => transitionTable.SetReduction(nextStateIndex, tokenClass));

                }

            }

            return transitionTable;

        }

    }
}
