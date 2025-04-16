using System;
using UnityEngine;

public static class RaycastHit2DExtensions
{
    public static RaycastHit2D AssignTo(this RaycastHit2D value, ref RaycastHit2D other)
    {
        other = value;
        return other;
    }

    public static RaycastHit2D HasHitObject(this RaycastHit2D value, Action hasHitObject)
    {
        if (value)
            hasHitObject();
        return value;
    }

    public static RaycastHit2D NoHit(this RaycastHit2D value, Action noHit)
    {
        if (!value)
            noHit();
        return value;
    }
}
