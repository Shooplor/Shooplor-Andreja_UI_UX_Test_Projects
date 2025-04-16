using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneLoader : MonoBehaviour
{
    public string backup;
void Start()
{
    Debug.Log("🎬 Intro Scene Started: " + SceneManager.GetActiveScene().name);
}
    public void LoadNextScene()
    {
        Debug.Log("⏩ LoadNextScene Triggered — loading: " + backup);
        SceneManager.LoadScene(backup);
    }
}