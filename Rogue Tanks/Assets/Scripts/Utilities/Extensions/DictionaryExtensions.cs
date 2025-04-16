using System;
using System.Linq;
using System.Collections.Generic;

public static class DictionaryExtensions 
{
    public static void GetValue<TKey,TValue>(this Dictionary<TKey,TValue> dictionary, TKey key, Action<TValue> onItemFound)
    {
        if (dictionary.TryGetValue(key, out TValue value))
            onItemFound(value);
    }

    public static TValue GetRandom<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) =>
        dictionary.ElementAt(Probability.GenerateRandomNumberBetween(0, dictionary.Count)).Value;

    public static void AddValue<TKey>(this Dictionary<TKey, int> dictionary, TKey key, int value)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += value;
        else
            dictionary[key] = value;
    }

    public static Dictionary<int, int> Copy(this Dictionary<int, int> dictionary) => new Dictionary<int, int>(dictionary);
}
