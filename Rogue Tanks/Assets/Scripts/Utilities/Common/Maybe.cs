using System;

public class Maybe<T>
{
    private T[] element;

    private Maybe(T[] element) => this.element = element;

    public void Do(Action<T> onItem) => element.ForEach(item => onItem(item));

    public static Maybe<T> CreateNone() => new Maybe<T>(new T[0]);
    public static Maybe<T> CreateSome(T element) => new Maybe<T>(new T[1] { element });
}
