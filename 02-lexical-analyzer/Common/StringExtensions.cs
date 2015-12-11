using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common
{
    public static class StringExtensions
    {
        public static string ToPrintableString(this string value)
        {

            Contract.Ensures(Contract.Result<string>() != null);

            return
                value
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");

        }

        public static string ToPrintableString(this char character)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return character.ToString().ToPrintableString();
        }

        public static string Center(this string value, int fieldWidth)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            fieldWidth = Math.Max(fieldWidth, value.Length);
            return value.PadLeft((fieldWidth + value.Length + 1) / 2).PadRight(fieldWidth);
        }
    }
}
