using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace RegexLexicalAnalyzer.RegexLexer
{
    internal class RegularExpressionAlphabet: IEnumerable<char>
    {

        private string Pattern { get; }

        public RegularExpressionAlphabet(RegularExpression expression)
        {
            Contract.Requires<ArgumentNullException>(expression != null, "Regular expression must be non-null.");
            this.Pattern = expression.Pattern;
        }

        private IEnumerable<char> GetAlphabet() => GetAlphabetWithDuplicates().Distinct();

        private IEnumerable<char> GetAlphabetWithDuplicates()
        {

            int pos = 0;

            while (pos < this.Pattern.Length)
            {
                string segment = this.ExtractSegment(pos);
                foreach (char c in GetSegmentAlphabet(segment))
                    yield return c;
                pos += segment.Length;
            }

        }

        private static IEnumerable<char> GetSegmentAlphabet(string segment)
        {
            switch (segment[0])
            {
                case '[':   // Choice operator in form [xyz], only contains terminals
                    return segment.Substring(1, segment.Length - 2).ToCharArray();
                case '(':   // Parenthesized repetition operator in form (x)*
                    return GetAlphabetForCharacterClass(segment[1]);
                default:
                    return GetAlphabetForCharacterClass(segment[0]);
            }
        }

        private string ExtractSegment(int pos)
        {
            switch (this.Pattern[pos])
            {
                case '[':   // Choice operator in form [xyz], only contains terminals
                    int endPos = this.Pattern.IndexOf(']', pos + 1);
                    return this.Pattern.Substring(pos, endPos - pos + 1);
                case '(':   // Parenthesized repetition operator in form (x)*
                    return this.Pattern.Substring(pos, 4);
                default:
                    return this.Pattern.Substring(pos, 1);
            }
        }

        private static IEnumerable<char> GetAlphabetForCharacterClass(char characterClass)
        {
            switch (characterClass)
            {
                case 'd':
                    return GetAlphabetForDigitClass();
                case 'a':
                    return GetAlphabetForLetterClass();
                case 's':
                    return new[] { ' ' };
                case '.':
                    return GetPrintableAsciiSet();
                default:
                    return new char[0];
            }
        }

        private static IEnumerable<char> GetAlphabetForLetterClass()
        {
            for (char c = 'a'; c <= 'z'; c++)
                yield return c;
            for (char c = 'A'; c <= 'Z'; c++)
                yield return c;
        }

        private static IEnumerable<char> GetAlphabetForDigitClass()
        {
            for (char c = '0'; c <= '9'; c++)
                yield return c;
        }

        private static IEnumerable<char> GetPrintableAsciiSet()
        {
            for (int i = 32; i < 127; i++)
                yield return (char)i;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return this.GetAlphabet().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
