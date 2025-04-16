using System;
using UnityEngine;

public static class FloatExtensions
{
    public static float Subtract(this float value, float anotherValue) => value - anotherValue;

    public static float RoundToInt(this float value) => (float)Math.Ceiling(value);

    public static float RoundUp(this float value) => (float)Math.Ceiling(value);

    public static float RoundDown(this float value) => (float)Math.Floor(value);

    public static float RoundToNearestEven(this float value) => (float)Math.Round(value / 2, MidpointRounding.ToEven) * 2;

    public static Vector3 ToYVector(this float value) => new Vector3(0, value);
    public static Vector3 ToXVector(this float value) => new Vector3(value, 0);

    public static void IfLessThanOrEqualTo(this float value, float otherValue, Action onLessThen)
    {
        if (value > otherValue) return;
        onLessThen();
    }

    public static void IsGreaterThanOrEqualTo(this float value, float otherValue, Action onGreaterThan)
    {
        if (value < otherValue) return;
        onGreaterThan();
    }

    public static bool IsGreaterThanOrEqualTo(this float value, float otherValue) => value >= otherValue;

    public static float DecreaseBy(this float value, float damage)
    {
        value -= damage;
        return value;
    }
}
