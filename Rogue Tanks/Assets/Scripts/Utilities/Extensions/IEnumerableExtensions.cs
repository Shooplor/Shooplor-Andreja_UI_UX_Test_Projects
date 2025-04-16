using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions 
{
    public static void ForEach<T>(this IEnumerable<T> array, Action<T> onItem)
    {
        foreach (var item in array)
            onItem(item);
    }

    public static void For<T>(this IEnumerable<T> array, Action<int, T> onItem)
    {
        int index = 0;
        foreach(var item in array)
        {
            onItem(index, item);
            index++;
        }
    }

    public static void IsEmpty<T>(this IEnumerable<T> array, Action onIsEmpty)
    {
        if (array.Count() == 0)
            onIsEmpty();
    }
}
