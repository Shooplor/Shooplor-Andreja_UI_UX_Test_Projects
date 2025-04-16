using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float waitForDelete = 0.5f;
    void Start()
    {
        StartCoroutine(DeleteBullet());
    }

    IEnumerator DeleteBullet()
    {
        yield return new WaitForSeconds(waitForDelete);
        Destroy(gameObject);
    }
}
