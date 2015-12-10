using System.Collections.Generic;
using System.Linq;

namespace RegexLexicalAnalyzer.RegexLexer
{
    internal class DottedItemSequenceComparer : IComparer<IEnumerable<DottedItem>>
    {
        public int Compare(IEnumerable<DottedItem> x, IEnumerable<DottedItem> y)
        {

            int result = this.CompareNulls(x, y);
            if (result != 0)
                return result;

            result = this.CompareCounts(x, y);
            if (result != 0)
                return result;

            return this.CompareContent(x, y);

        }

        private int CompareNulls(IEnumerable<DottedItem> x, IEnumerable<DottedItem> y)
        {

            bool nullX = object.ReferenceEquals(null, x);
            bool nullY = object.ReferenceEquals(null, y);

            return nullY.CompareTo(nullY);

        }

        private int CompareCounts(IEnumerable<DottedItem> x, IEnumerable<DottedItem> y)
        {
            return x.Count().CompareTo(y.Count());
        }

        private int CompareContent(IEnumerable<DottedItem> x, IEnumerable<DottedItem> y)
        {

            IEnumerable<DottedItem> sortedX = x.OrderBy(item => item).ToList();
            IEnumerable<DottedItem> sortedY = y.OrderBy(item => item).ToList();

            return 
                sortedX
                .Zip(sortedY, (a, b) => a.CompareTo(b))
                .Where(comparison => comparison != 0)
                .Take(1)
                .DefaultIfEmpty(0)
                .Single();

        }
    }
}
