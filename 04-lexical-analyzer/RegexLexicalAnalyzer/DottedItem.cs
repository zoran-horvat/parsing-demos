using Common;
using ParsingInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer
{
    internal class DottedItem
    {

        private string RecognizedInput { get; }

        private RegularExpression Expression { get; }

        private string Pattern => this.Expression.Pattern;

        private string TokenClass => this.Expression.Class;

        private int DotBefore { get; }

        public DottedItem(RegularExpression expression)
        {
            Contract.Requires<ArgumentNullException>(expression != null, "Regular expression must not be null.");
            this.Expression = expression;
            this.RecognizedInput = string.Empty;
        }

        private DottedItem(RegularExpression expression, int dotBefore, string recognizedInput)
        {
            this.Expression = expression;
            this.DotBefore = dotBefore;
            this.RecognizedInput = recognizedInput;
        }

        public IEnumerable<DottedItem> MoveOver(char input)
        {

            if (this.IsReduceItem)
                return EmptyDottedItemSet;

            switch (this.Pattern[this.DotBefore])
            {
                case 'd': return this.MoveDigitOver(input);
                case '.': return this.MoveAnyOver(input);
                case '[': return this.MoveChoiceOver(input);
                default: return EmptyDottedItemSet;
            }
        }

        private IEnumerable<DottedItem> MoveDigitOver(char input)
        {

            if (!Char.IsDigit(input))
                return EmptyDottedItemSet;

            return new[] { new DottedItem(this.Expression, this.DotBefore + 1, this.RecognizedInput + input) };

        }

        private IEnumerable<DottedItem> MoveChoiceOver(char input)
        {

            int indexOfClosingSquareBracket = this.Pattern.IndexOf(']', this.DotBefore + 1);
            int indexOfCharacter = this.Pattern.IndexOf(input, 1, indexOfClosingSquareBracket - 1);

            if (indexOfCharacter < 0)
                return EmptyDottedItemSet;

            return new[] {new DottedItem(this.Expression, indexOfClosingSquareBracket + 1, this.RecognizedInput + input)};

        } 

        private IEnumerable<DottedItem> MoveAnyOver(char input)
        {

            if (input == '\r' || input == '\n')
                return EmptyDottedItemSet;

            return new[] { new DottedItem(this.Expression, this.DotBefore + 1, this.RecognizedInput + input) };

        }

        private bool IsReduceItem =>  this.DotBefore >= this.Pattern.Length;

        public Option<IToken> TryReduce()
        {

            if (!this.IsReduceItem)
                return Option<IToken>.None();

            return Option<IToken>.Some(new StringClassToken(this.RecognizedInput, this.TokenClass));

        }

        private IEnumerable<DottedItem> EmptyDottedItemSet => new DottedItem[0];

        private string PatternBeforeDot => this.Pattern.Substring(0, this.DotBefore);

        private string PatternAfterDot => this.Pattern.Substring(this.DotBefore);

        public override string ToString() => $"\"{RecognizedInput}\"|{PatternBeforeDot}°{PatternAfterDot}";

        public override int GetHashCode()
        {
            return this.Pattern.GetHashCode() ^ this.DotBefore.GetHashCode() ^ this.RecognizedInput.GetHashCode();
        }

        public override bool Equals(object obj)
        {

            DottedItem item = obj as DottedItem;

            if (item == null)
                return false;

            return this.Pattern == item.Pattern && this.DotBefore == item.DotBefore && this.RecognizedInput == item.RecognizedInput;

        }

    }
}
