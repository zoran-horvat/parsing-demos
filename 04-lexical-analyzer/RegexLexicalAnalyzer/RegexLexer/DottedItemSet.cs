using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace RegexLexicalAnalyzer.RegexLexer
{
    internal class DottedItemSet : IEnumerable<DottedItem>, IComparable<DottedItemSet>
    {
        private IEnumerable<DottedItem> Items { get; }

        public DottedItemSet(IEnumerable<DottedItem> items)
        {

            Contract.Requires<ArgumentNullException>(items != null, "Sequence of dotted items must be non-null.");
            Contract.Requires<ArgumentException>(items.All(item => item != null), "All items in the dotted item set must be non-null.");

            this.Items = items.ToList();

        }

        public DottedItemSet(DottedItem item)
        {
            this.Items = new[] { item };
        }

        public IEnumerator<DottedItem> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int CompareTo(DottedItemSet other)
        {
            return new DottedItemSequenceComparer().Compare(this, other);
        }
    }
}
