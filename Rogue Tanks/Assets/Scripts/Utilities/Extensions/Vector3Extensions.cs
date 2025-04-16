using System;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 AdjustForTank(this Vector3 value)
    {
        return new Vector3(value.x+1, value.y-1, value.z);
    }

    public static Vector3 OnVertical(this Vector3 value, Action onVertical)
    {
        if (value == Vector3.up || value == Vector3.down)
            onVertical();
        return value;
    }

    public static Vector3 OnHorizontal(this Vector3 value, Action onHorizontal)
    {
        if (value == Vector3.left || value == Vector3.right)
            onHorizontal();
        return value;
    }

    public static Vector3 Increment(this Vector3 value, float increment) =>
        new Vector3(value.x + increment, value.y + increment, value.z);
}
