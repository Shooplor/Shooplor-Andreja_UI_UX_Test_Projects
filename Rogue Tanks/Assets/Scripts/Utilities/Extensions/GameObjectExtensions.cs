using System;
using UnityEngine;

public static class GameObjectExtensions
{ 
    public static OperationResult<T> GetComponentAsResult<T>(this GameObject gameObject)
    {
        var component = gameObject.GetComponent<T>();
        if (component == null) return OperationResult<T>.CreateError($"Can't get component: {typeof(T)}");
        return OperationResult<T>.CreateSuccess(component);
    }
    
    public static OperationResult<T> GetComponentInChildrenAsResult<T>(this GameObject gameObject)
    {
        var component = gameObject.GetComponentInChildren<T>();
        if (component == null) return OperationResult<T>.CreateError($"Can't get component: {typeof(T)}");
        return OperationResult<T>.CreateSuccess(component);
    }

    public static void OnGetComponentInParent<T>(this GameObject gameObject, Action<T> onComponent)
    {
        var component = gameObject.GetComponentInParent<T>();
        if (component == null) return;
        onComponent(component);
    }

    public static void IfNotSameAs(this GameObject value, GameObject gameObject, Action onNotSame)
    {
        if (value == gameObject) return;
        onNotSame();
    }

    public static void SetActiveAt(this GameObject gameObject, Vector3 position)
    {
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }

    public static void Deactivate(this GameObject gameObject, Action onDeactivate)
    {
        gameObject.SetActive(false);
        onDeactivate();
    }

    public static void Deactivate(this GameObject gameObject) => gameObject.SetActive(false);
}
