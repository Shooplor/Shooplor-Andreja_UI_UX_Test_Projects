using System;

public static class BoolExtensions
{
    public static bool OnFalse(this bool value, Action onFalse)
    {
        if (value) return value;
        onFalse();
        return value;
    }

    public static bool OnTrue(this bool value, Action onTrue)
    {
        if (!value) return value;
        onTrue();
        return value;
    }
}
