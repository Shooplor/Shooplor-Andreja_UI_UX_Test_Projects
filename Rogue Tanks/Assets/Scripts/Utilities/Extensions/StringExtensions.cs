using System;
using System.Collections.Generic;

public static class StringExtensions
{
    public static void IfNotEqualTo(this string value, string otherValue, Action onNotEqual)
    {
        if (value.Equals(otherValue)) return;
        onNotEqual();
    }

    public static void IfEndsWith(this string value,string condition, Action onEndsWith)
    {
        if (value.EndsWith(condition))
            onEndsWith();
    }

    public static bool EndsWithAny(this string value, IEnumerable<string> array)
    {
        if (array == null) return false;
        foreach(var item in array)
        {
            if (value.EndsWith(item))
                return true;
        }
        return false;
    }

    public static int ToInt(this string value)
    {
        Int32.TryParse(value, out int result);
        return result;
    }
}
