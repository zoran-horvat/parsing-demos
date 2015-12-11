using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexLexicalAnalyzer.TableLexer
{
    public class TransitionTable
    {

        private Dictionary<Tuple<int, char>, int> StateTransitions = new Dictionary<Tuple<int, char>, int>();
        private Dictionary<int, string> Reductions = new Dictionary<int, string>();

        public void SetTransition(int state, char input, int newState)
        {
            this.StateTransitions[Tuple.Create(state, input)] = newState;
        }

        public void SetReduction(int state, string tokenClass)
        {
            this.Reductions[state] = tokenClass;
        }

        private IEnumerable<int> InputStatesFromTransitions => this.StateTransitions.Keys.Select(key => key.Item1).Distinct();

        private IEnumerable<int> OutputStatesFromTransitions => this.StateTransitions.Values.Distinct();

        public IEnumerable<int> GetStates() => this.InputStatesFromTransitions.Union(this.OutputStatesFromTransitions).Distinct();

        public IEnumerable<char> GetInputsForState(int state) => this.StateTransitions.Keys.Where(key => key.Item1 == state).Select(key => key.Item2).Distinct();

        public Option<int> TryGetTransition(int state, char input) => this.StateTransitions.TryGetValue(Tuple.Create(state, input));

        public bool ContainsTransition(int state, char input) => this.StateTransitions.ContainsKey(Tuple.Create(state, input));

        public int GetTransition(int state, char input) => this.TryGetTransition(state, input).Single();

        public Option<string> TryGetReduction(int state) => this.Reductions.TryGetValue(state);

        public string GetReduction(int state) => this.TryGetReduction(state).Single();

        public IEnumerable<Tuple<int, char, int>> GetAllTransitions() => this.StateTransitions.Keys.Select(key => Tuple.Create(key.Item1, key.Item2, this.StateTransitions[key])).ToList();

    }
}
