using UnityEngine;
using UnityEngine.UI;

public class ResetHorizontalScrollOnStart : MonoBehaviour
{
    public ScrollRect scrollRect;

    void Start()
    {
        if (scrollRect != null)
        {
            scrollRect.horizontalNormalizedPosition = 0f;
        }
        else
        {
            Debug.LogWarning("ScrollRect reference is missing!");
        }
    }
}