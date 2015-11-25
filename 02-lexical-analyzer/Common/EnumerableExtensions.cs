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
    }
}
