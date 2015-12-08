using System;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer
{
    internal class DottedItem
    {
        private string Pattern { get; }

        private DottedItem(RegularExpression expression)
        {
            Contract.Requires<ArgumentNullException>(expression != null, "Regular expression must not be null.");
            this.Pattern = expression.Pattern;
        }

        public static DottedItem Initialize(RegularExpression expression)
        {
            return new DottedItem(expression);
        }
    }
}
