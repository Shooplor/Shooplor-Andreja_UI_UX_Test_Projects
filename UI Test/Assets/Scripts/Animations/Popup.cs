using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
   
    private void OnEnable() {
    
         transform.localScale = Vector3.zero;
         transform.LeanScale(Vector3.one, 0.5f).setEaseOutBack();
        
    }
    public void OnClose() {
        transform.LeanScale(Vector3.zero, 0.5f);

}
}
