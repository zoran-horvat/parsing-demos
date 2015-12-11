using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace RegexLexicalAnalyzer.TableLexer
{
    public class TransitionTableVisualizer
    {
        private TransitionTable Table { get; }

        public TransitionTableVisualizer(TransitionTable table)
        {
            Contract.Requires<ArgumentNullException>(table != null, "Transition table must be non-null.");
            this.Table = table;
        }

        private IEnumerable<int> Rows => 
            this
                .Table
                .GetStates()
                .OrderBy(state => state)
                .ToList();

        private IEnumerable<char> InputColumns => 
            this
                .Table
                .GetAllTransitions()
                .Select(transition => transition.Item2)
                .Distinct()
                .OrderBy(input => input)
                .ToList();

        private string GetTransitionRepresentation(int state, char input) =>
            this
                .Table
                .TryGetTransition(state, input)
                .Select(transition => transition.ToString("0"))
                .DefaultIfEmpty(string.Empty)
                .Single()
                .ToPrintableString();

        private string GetProductionRepresentation(int state) =>
            this
                .Table
                .TryGetReduction(state)
                .DefaultIfEmpty(string.Empty)
                .Single()
                .ToPrintableString();

        private IEnumerable<string> AllProductionRepresentations =>
            this
                .Rows
                .Select(state => this.GetProductionRepresentation(state))
                .ToList();

        private IEnumerable<string> GetTransitionsFor(char input) =>
            this
                .Rows
                .Select(state => GetTransitionRepresentation(state, input))
                .ToList();

        private bool AreColumnsEqual(char input1, char input2) =>
            this
                .GetTransitionsFor(input1)
                .Zip(this.GetTransitionsFor(input2), (trans1, trans2) => trans1 == trans2)
                .All(comparison => comparison);

        private string MergeColumnHeader(char firstInput, char lastInput)
        {
            if (firstInput == lastInput)
                return firstInput.ToString().ToPrintableString();
            return $"{firstInput}-{lastInput}".ToPrintableString();
        }

        private bool CanMerge(char c1, char c2)
        {

            if (char.IsDigit(c1) && char.IsDigit(c2))
                return true;

            if (!char.IsLetter(c1) || !char.IsLetter(c2))
                return false;

            if (char.IsLower(c1) && char.IsLower(c2))
                return true;

            if (char.IsUpper(c1) && char.IsUpper(c2))
                return true;

            return false;

        }

        private IEnumerable<Tuple<char, string>> GetMergedColumnHeaders()
        {

            bool first = true;
            char firstInput = '\0';
            char lastInput = '\0';

            foreach (char input in this.InputColumns)
            {
                if (first)
                {
                    firstInput = lastInput = input;
                }
                else if (this.CanMerge(firstInput, input) && this.AreColumnsEqual(firstInput, input))
                {
                    lastInput = input;
                }
                else
                {
                    yield return Tuple.Create(firstInput, MergeColumnHeader(firstInput, lastInput));
                    firstInput = lastInput = input;
                }

                first = false;

            }

            if (!first)
                yield return Tuple.Create(firstInput, MergeColumnHeader(firstInput, lastInput));

        }

        public override string ToString()
        {

            IEnumerable<Tuple<char, string>> columnHeaders = this.GetMergedColumnHeaders().ToList();
            int columnWidth = columnHeaders.Select(header => header.Item2.Length).DefaultIfEmpty(0).Max() + 2;

            columnHeaders = columnHeaders.Select(header => Tuple.Create(header.Item1, header.Item2.Center(columnWidth))).ToArray();

            int stateColumnWidth = this.Rows.Select(row => row.ToString("0").Length).Concat(new[] { 5 }).Max();

            string producesColumnHeader = "Produces";
            int producesColumnWidth = this.AllProductionRepresentations.Concat(new[] { producesColumnHeader }).Max(value => value.Length);

            int totalWidth = stateColumnWidth + columnHeaders.Count() * (columnWidth + 1) + producesColumnWidth + 3;
            string line = new string('-', totalWidth);

            StringBuilder table = new StringBuilder();
            table.AppendLine(line);

            table.AppendFormat("|{0}", new string(' ', stateColumnWidth));

            foreach (Tuple<char, string> columnHeader in columnHeaders)
                table.AppendFormat("|{0}", columnHeader.Item2.ToPrintableString());

            table.AppendFormat("|{0}", producesColumnHeader.PadRight(producesColumnWidth));

            table.AppendLine("|");

            foreach (int state in this.Rows)
            {
                table.AppendLine(line);
                table.AppendFormat("|{0}", state.ToString("0").Center(stateColumnWidth));
                foreach (Tuple<char, string> columnHeader in columnHeaders)
                    table.AppendFormat("|{0}", this.GetTransitionRepresentation(state, columnHeader.Item1).Center(columnWidth));
                table.AppendFormat("|{0}", this.GetProductionRepresentation(state).PadRight(producesColumnWidth));
                table.AppendLine("|");
            }

            table.AppendLine(line);

            return table.ToString();

        }
    }
}
