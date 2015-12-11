using System;

namespace Common
{
    public static class StringExtensions
    {
        public static string ToPrintableString(this string value)
        {
            return
                value
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
        }

        public static string ToPrintableString(this char character)
        {
            return character.ToString().ToPrintableString();
        }

        public static string Center(this string value, int fieldWidth)
        {
            fieldWidth = Math.Max(fieldWidth, value.Length);
            return value.PadLeft((fieldWidth + value.Length + 1) / 2).PadRight(fieldWidth);
        }
    }
}
