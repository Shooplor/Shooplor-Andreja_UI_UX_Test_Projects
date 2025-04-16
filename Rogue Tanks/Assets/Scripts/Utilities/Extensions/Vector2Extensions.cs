using System;
using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 FacesUp(this Vector2 vector, Action onFaceUp)
    {
        if (vector == Vector2.up)
            onFaceUp();
        return vector;
    }

    public static Vector2 FacesDown(this Vector2 vector, Action onFaceDown)
    {
        if (vector == Vector2.down)
            onFaceDown();
        return vector;
    }

    public static Vector2 FacesLeft(this Vector2 vector, Action onFaceLeft)
    {
        if (vector == Vector2.left)
            onFaceLeft();
        return vector;
    }

    public static Vector2 FacesRight(this Vector2 vector, Action onFaceRight)
    {
        if (vector == Vector2.right)
            onFaceRight();
        return vector;  
    }
}
