using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// snippets of code: 
//SceneManager.LoadScene(SceneManager.GetActiveScene().name); - kod za restart na scena
//Application.Quit(); - gasenje na igra 


//Se' za UI managerot.
public class UI_Manager : MonoBehaviour

{
    public GameObject PauseScreen;
    public GameObject SplashScreen;
    public GameObject SettingsScreen;
   

   public void OnBTNSettingsPress() {
      
   }
   public void OnBTNPauseResumePress() {
             PauseScreen.SetActive(false);
   }
   public void OnBTNPauseQuitPress() {

   }
     public void OnEnterPausePress() {
          PauseScreen.SetActive(true);
   }
}
