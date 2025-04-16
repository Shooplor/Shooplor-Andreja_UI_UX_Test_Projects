using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static List<T> GetAllComponents<T>(this Transform transform)
    {
        List<T> resultList = new List<T>();
        foreach (Transform child in transform)
        {
            var childComponent = child.GetComponent<T>();
            if (childComponent == null) continue;
            resultList.Add(childComponent);
        }
        return resultList;
    }

    public static Vector3 Offset(this Transform transform, int x, int y) => new Vector3(transform.position.x + x, transform.position.y + y, 0);
}
