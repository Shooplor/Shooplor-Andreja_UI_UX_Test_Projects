using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBloodScript : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(DeleteParticleSystem());
    }

    
    void Update()
    {
        
    }
    IEnumerator DeleteParticleSystem()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
