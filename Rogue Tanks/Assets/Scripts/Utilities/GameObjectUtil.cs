using System;
using UnityEngine;

public static class GameObjectUtil
{
    public static void Destroy(UnityEngine.Object gameObject)
    {
        if (gameObject == null) return;
        GameObject.Destroy(gameObject);
    }

    public static void Destroy(UnityEngine.Object gameObject, Action onDestroy)
    {
        Destroy(gameObject);
        onDestroy();
    }
}
