using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCardManager : MonoBehaviour
{
    public TMP_Text nameText;
 public void UpdateName(string newName){
    nameText.text = newName;
 }
}
