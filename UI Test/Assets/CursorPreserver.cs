using UnityEngine;

public class CursorPreserver : MonoBehaviour
{
    private static bool exists = false;

    void Awake()
    {
        if (exists)
        {
            Destroy(gameObject); // prevent duplicates if returning to this scene
            return;
        }

        exists = true;
        DontDestroyOnLoad(gameObject); // keep this object between scenes
    }
}