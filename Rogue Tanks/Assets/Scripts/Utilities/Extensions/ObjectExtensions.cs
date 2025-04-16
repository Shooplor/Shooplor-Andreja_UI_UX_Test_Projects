using System;

public static class ObjectExtensions
{ 
    public static void Is<T>(this object obj, Action onType)
    {
        if (obj is T)
            onType();
    }
}
