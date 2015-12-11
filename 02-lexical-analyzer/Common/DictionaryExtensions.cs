using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common
{
    public static class DictionaryExtensions
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {

            Contract.Ensures(Contract.Result<Option<TValue>>() != null);

            TValue value;

            if (!dictionary.TryGetValue(key, out value))
                return Option<TValue>.None();

            return Option<TValue>.Some(value);

        }
    }
}
