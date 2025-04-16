using System;
using System.Collections.Generic;

public static class ArrayExtensions
{
    private static Random rnd = new Random();
    public static void For<T>(this T[] array,Action<int,T> onItem)
    {
        for (int i = 0; i < array.Length; i++)
            onItem(i, array[i]);
    }

    public static IEnumerable<T> GetRandomItems<T>(this T[] array)
    {
        var result = new T[rnd.Next(0, array.Length)];
        for (int i = 0; i < result.Length; i++)
            result[i] = array.GetRandomItem();
        return result;
    }

    public static void For<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2> onItem)
    {
        if (array1.Length != array2.Length) throw new Exception("Array1 and Array2 must be of same length!");
        for (int i = 0; i < array1.Length; i++)
            onItem(array1[i], array2[i]);
    }

    public static T GetRandomItem<T>(this T[] array)
    {
        return array[rnd.Next(0, array.Length)];
    }

    public static void OnEmpty<T>(this T[] array, Action onEmpty)
    {
        if (array.Length != 0) return;
        onEmpty();
    }

    public static T[] Get<T>(this T[] array, params int[] indexes)
    {
        List<T> result = new List<T>();
        indexes.ForEach(index => result.Add(array[index]));
        return result.ToArray();
    }
}
