using System;

public static class IntExtensions
{
    public static void Equals(this int value, int otherValue, Action onEqual)
    {
        if (value != otherValue) return;
        onEqual();
    }
    public static int Condition(this int value, Func<int, bool> predicate, Action onTrue)
    {
        if (predicate(value))
            onTrue();
        return value;
    }
    public static void IfLessThanOrEqualTo(this int value, int otherValue, Action onLarger)
    {
        if (value > otherValue) return;
        onLarger();
    }

    public static void IsGreaterThanOrEqualTo(this int value, int otherValue, Action onGreater)
    {
        if (value < otherValue) return;
        onGreater();
    }

    public static int DeductDamage(this int value, int otherValue)
    {
        value -= otherValue;
        return value;
    }

    public static int DecreaseByOne(this int value)
    {
        value--;
        return value;
    }

    public static int Normalize(this int x, float a, float b, float min, float max) => Convert.ToInt32((b - a) * ((x - min) / (max - min)) + a);

    public static void IsBetween(this int value, int minInclusive, int maxInclusive, Action onIsBetween)
    {
        if (minInclusive <= value && value <= maxInclusive)
            onIsBetween();
    }

    public static void If(this int value, Func<int,bool> predicate, Action onTrue)
    {
        if (predicate(value))
            onTrue();
    }
}
