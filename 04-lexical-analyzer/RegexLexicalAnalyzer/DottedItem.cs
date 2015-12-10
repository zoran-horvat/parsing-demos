using Common;
using ParsingInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace RegexLexicalAnalyzer
{
    internal class DottedItem
    {

        private string RecognizedInput { get; }

        private RegularExpression Expression { get; }

        private string Pattern => this.Expression.Pattern;

        private string TokenClass => this.Expression.Class;

        private int DotBefore { get; }

        private DottedItem(RegularExpression expression)
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

        public static IEnumerable<DottedItem> InitialSetFor(RegularExpression expression)
        {
            return new DottedItem(expression).EpsilonMove();
        }

        public IEnumerable<DottedItem> MoveOver(char input)
        {

            if (this.IsReduceItem)
                return EmptyDottedItemSet;

            return
                MoveOverCharacter(input)
                    .SelectMany(item => item.EpsilonMove())
                    .ToList();

        }

        private IEnumerable<DottedItem> MoveOverCharacter(char input)
        {
            switch (this.Pattern[this.DotBefore])
            {
                case 'd':
                    return this.MoveOver(input, char.IsDigit);
                case 'a':
                    return this.MoveOver(input, char.IsLetter);
                case 's':
                    return this.MoveOver(input, (c) => c == ' ');
                case '[':
                    return this.MoveChoiceOver(input);
                case '.':
                    return this.MoveAnyOver(input);
                default:
                    return EmptyDottedItemSet;
            }
        }

        private IEnumerable<DottedItem> MoveOver(char input, Func<char, bool> predicate)
        {

            if (!predicate(input))
                return EmptyDottedItemSet;

            return new[] {new DottedItem(this.Expression, this.DotBefore + 1, this.RecognizedInput + input)};

        } 

        private IEnumerable<DottedItem> MoveChoiceOver(char input)
        {

            int indexOfClosingSquareBracket = this.Pattern.IndexOf(']', this.DotBefore + 2);
            int range = indexOfClosingSquareBracket - this.DotBefore - 1;
            int indexOfCharacter = this.Pattern.IndexOf(input, this.DotBefore + 1, range);

            if (indexOfCharacter < 0)
                return EmptyDottedItemSet;

            return new[] {new DottedItem(this.Expression, indexOfClosingSquareBracket + 1, this.RecognizedInput + input)};

        }

        private IEnumerable<DottedItem> EpsilonMove()
        {

            if (this.IsReduceItem)
                return new[] {this};

            if (this.Pattern[this.DotBefore] == '(')
                return this.EpsilonMoveBeforeRepetition();

            if (this.Pattern[this.DotBefore] == ')')
                return this.EpsilonMoveAfterRepetition();

            return new[] {this};

        }

        private IEnumerable<DottedItem> EpsilonMoveBeforeRepetition()
        {
            // Covers subexpressions of form .(x)*, where dot is placed right before the opening bracket
            // This subexpression is turned into two dotted items: (.x)* and (x)*.

            return new[]
            {
                new DottedItem(this.Expression, this.DotBefore + 1, this.RecognizedInput),  // Jump overt opening bracket
                new DottedItem(this.Expression, this.DotBefore + 4, this.RecognizedInput)   // Jump over the entire repeated item
            };

        }

        private IEnumerable<DottedItem> EpsilonMoveAfterRepetition()
        {
            // Covers subexpressions of form (x.)*, where x has already been recognized
            // Turns subexpression into two dotted items: (.x)* and (x)*.

            return new[]
            {
                new DottedItem(this.Expression, this.DotBefore - 1, this.RecognizedInput),  // Jumps back before the item to recognize
                new DottedItem(this.Expression, this.DotBefore + 2, this.RecognizedInput)   // Jumps out of the repeated item
            };
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
