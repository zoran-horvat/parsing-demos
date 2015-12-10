using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexLexicalAnalyzer.TableLexer
{
    public class TransitionTable
    {

        private Dictionary<Tuple<int, char>, int> StateTransitions = new Dictionary<Tuple<int, char>, int>();

        public void SetTransition(int state, char input, int newState)
        {
            this.StateTransitions[Tuple.Create(state, input)] = newState;
        }

        public IEnumerable<int> GetStates() => this.StateTransitions.Keys.Select(key => key.Item1).Distinct();

        public IEnumerable<char> GetInputsForState(int state) => this.StateTransitions.Keys.Where(key => key.Item1 == state).Select(key => key.Item2).Distinct();

        public Option<int> TryGetTransition(int state, char input) => this.StateTransitions.TryGetValue(Tuple.Create(state, input));

        public int GetTransition(int state, char input) => this.TryGetTransition(state, input).Single();

        public IEnumerable<Tuple<int, char, int>> GetAllTransitions() => this.StateTransitions.Keys.Select(key => Tuple.Create(key.Item1, key.Item2, this.StateTransitions[key])).ToList();

    }
}
