using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace RegexLexicalAnalyzer.RegexLexer
{
    internal class DottedItemSetIndex
    {

        private List<DottedItemSet> ItemSets { get; } = new List<DottedItemSet>();

        public Option<int> TryGetIndexFor(DottedItemSet set)
        {

            Contract.Requires<ArgumentNullException>(set != null, "Dotted item set must be non-null.");

            return 
                ItemSets
                    .Select((state, index) => new { StateIndex = index, ComparedToNext = state.CompareTo(set) })
                    .Where(pair => pair.ComparedToNext == 0)
                    .Select(pair => pair.StateIndex)
                    .Take(1)
                    .AsOption();

        }

        [Pure]
        public bool Contains(DottedItemSet set) => this.TryGetIndexFor(set).Any();

        public int GetIndexFor(DottedItemSet set) => this.TryGetIndexFor(set).Single();

        public DottedItemSet GetItemSet(int index)
        {
            Contract.Requires<IndexOutOfRangeException>(index >= 0 && index < this.ItemSets.Count, "Item set index is outside of bounds.");
            return this.ItemSets[index];
        }

        public void Add(DottedItemSet set)
        {

            Contract.Requires<ArgumentNullException>(set != null, "Dotted item set must be non-null.");
            Contract.Requires<ArgumentException>(!this.Contains(set), "Already contains specified dotted item set.");

            this.ItemSets.Add(set);

        }
    }
}
