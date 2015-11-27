using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static Option<T> AsOption<T>(this IEnumerable<T> sequence)
        {

            if (!sequence.Any())
                return Option<T>.None();

            return Option<T>.Some(sequence.Single());

        }

        public static void Print<T>(this IEnumerable<T> sequence, int lineWidth)
        {

            IEnumerable<string> fields = sequence.Select(el => el.ToString()).ToList();
            int fieldsCount = fields.Count();
            int fieldWidth = fields.DefaultIfEmpty(string.Empty).Max(field => field.Length);
            int fieldsPerLine = (lineWidth + 1) / (fieldWidth + 1);

            string[] delimitedFields =
                fields
                .Select((field, index) => new
                {
                    PaddedField = field.PadRight(fieldWidth),
                    IsEndOfFile = index == fieldsCount - 1,
                    IsEndOfLine = index % fieldsPerLine == 0
                })
                .Select(tuple => tuple.PaddedField + ((tuple.IsEndOfFile || tuple.IsEndOfLine) ? Environment.NewLine : " "))
                .ToArray();

            string block = string.Join(string.Empty, delimitedFields);

            Console.Write(block);
            if (delimitedFields.Length % fieldsPerLine != 0)
                Console.WriteLine();

        }
    }
}
