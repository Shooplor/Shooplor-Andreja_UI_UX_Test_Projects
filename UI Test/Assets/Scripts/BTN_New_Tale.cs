using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string In_Game_Screen;

    public void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}