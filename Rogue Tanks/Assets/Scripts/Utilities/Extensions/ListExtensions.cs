using System;
using System.Collections.Generic;

public static class ListExtensions
{
    public static List<T> Select<T>(this List<T> list, Action<T> onItem)
    {
        foreach (var item in list)
            onItem(item);
        return list;
    }

    public static void OnEmpty<T>(this List<T> list, Action onEmpty)
    {
        if (list.Count != 0) return;
        onEmpty();
    }

    public static void Add<T>(this List<T> list,T item, Action onAdd)
    {
        list.Add(item);
        onAdd();
    }

    public static T[,] ToNormalArray<T>(this List<T[]> list)
    {
        var first = list[0];
        T[,] result = new T[list.Count, first.Length];
        for(int i = 0; i < list.Count; i++)
        {
            var array = list[i];
            for(int j=0;j<array.Length;j++)
            {
                result[i, j] = array[j];
            }
        }
        return result;
    }
}
