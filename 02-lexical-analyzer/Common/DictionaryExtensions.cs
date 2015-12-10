using System.Collections.Generic;

namespace Common
{
    public static class DictionaryExtensions
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {

            TValue value;

            if (!dictionary.TryGetValue(key, out value))
                return Option<TValue>.None();

            return Option<TValue>.Some(value);

        }
    }
}
