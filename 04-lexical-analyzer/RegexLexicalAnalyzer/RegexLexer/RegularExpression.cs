using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace RegexLexicalAnalyzer.RegexLexer
{
    public class RegularExpression
    {
        public string Pattern { get; }
        public string Class { get; }

        public RegularExpression(string pattern, string tokenClass)
        {

            Contract.Requires<ArgumentException>(pattern != null, "Regular expression pattern must be non-null.");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(tokenClass), "Token class must be non-empty.");

            this.Pattern = pattern;
            this.Class = tokenClass;

        }

        public IEnumerable<char> GetAlphabet()
        {

            int pos = 0;

            while (pos < this.Pattern.Length)
            {
                switch (this.Pattern[pos])
                {
                    case '[':   // Choice operator in form [xyz], only contains terminals
                        pos++;  // Skip opening [
                        while (this.Pattern[pos] != ']')
                            yield return this.Pattern[pos++];
                        pos++;  // Skip closing ]
                        break;
                    case '(':   // Parenthesized repetition operator in form (x)*
                        pos++;  // Skip opening (
                        foreach (char character in this.GetAlphabetForCharacterClass(this.Pattern[pos]))
                            yield return character;
                        pos += 2; // Skip )*
                        break;
                    default:
                        foreach (char character in this.GetAlphabetForCharacterClass(this.Pattern[pos]))
                            yield return character;
                        pos++;
                        break;
                }
            }

        }

        private IEnumerable<char> GetAlphabetForCharacterClass(char characterClass)
        {
            switch (characterClass)
            {
                case 'd':
                    for (char c = '0'; c <= '9'; c++)
                        yield return c;
                    break;
                case 'a':
                    for (char c = 'a'; c <= 'z'; c++)
                        yield return c;
                    for (char c = 'A'; c <= 'Z'; c++)
                        yield return c;
                    break;
                case 's':
                    yield return ' ';
                    break;
                case '.':
                    for (int i = 32; i < 127; i++)
                        yield return (char)i;
                    break;
            }
        }
    }
}
