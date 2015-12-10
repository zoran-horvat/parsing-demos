using System.Collections.Generic;

namespace RegexLexicalAnalyzer.RegexLexer
{
    internal static class DottedItemExtensions
    {
        public static DottedItemSet AsItemSet(this IEnumerable<DottedItem> sequence)
        {
            return new DottedItemSet(sequence);
        }
    }
}
