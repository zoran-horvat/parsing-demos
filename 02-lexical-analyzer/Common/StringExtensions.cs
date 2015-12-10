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
    }
}
