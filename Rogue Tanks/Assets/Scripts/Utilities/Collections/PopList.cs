using System;
using System.Collections.Generic;

public class PopList<T>
{
    private List<T> items = new List<T>();

    public int Count { get => items.Count; }

    private PopList(T[] items)
    {
        this.items = new List<T>(items);
    }

    public void Pop(Action<T> onPop) 
    {
        if (items.Count == 0) return;
        onPop(items[0]);
        items.RemoveAt(0);
    }

    public IEnumerable<T> PopMore(int numberOfElements)
    {
        List<T> result = new List<T>();
        for (int i = 0; i < numberOfElements; i++)
            Pop(element => result.Add(element));
        return result;
    }

    public IEnumerable<T> Pop(int numberOfItems)
    {
        List<T> result = new List<T>();
        for(int i = 0; i < numberOfItems; i++)
        {
            if (items.Count == 0) break;
            result.Add(items[0]);
            items.RemoveAt(0);
        }
        return result;
    }


    public bool HasNoMoreElements() => items.Count == 0;
    public void OnEmpty(Action action)
    {
        if (HasNoMoreElements())
            action();
    }
    public static PopList<T> Create(params T[] items) => new PopList<T>(items);
    public static PopList<T> CreateEmpty() => new PopList<T>(new T[0]);
}
