using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public static class DictionaryExtensions
    {
        public static Dictionary<T, int> Merge<T>(this Dictionary<T, int> first, Dictionary<T, int> second)
        {
            var result = new Dictionary<T, int>();

            foreach (var keyValuePair in first.ToList())
            {
                result.Add(keyValuePair.Key, keyValuePair.Value);
            }

            foreach (var keyValuePair in second.ToList())
            {
                if (result.TryGetValue(keyValuePair.Key, out var currentValue))
                {
                    result[keyValuePair.Key] = currentValue + keyValuePair.Value;
                }
                else
                {
                    result[keyValuePair.Key] = keyValuePair.Value;
                }
            }

            return result;
        }

        public static Dictionary<T, int> Intersection<T>(this Dictionary<T, int> first, Dictionary<T, int> second)
        {
            var result = new Dictionary<T, int>();

            foreach (var keyValuePair in first.ToList())
            {
                if (second.ContainsKey(keyValuePair.Key))
                {
                    result.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return result;
        }
    }
}