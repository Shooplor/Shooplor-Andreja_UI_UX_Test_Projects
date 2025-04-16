using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("🚪 Quitting the game...");

#if UNITY_EDITOR
        // Stop play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the game in a built version
        Application.Quit();
#endif
    }
}