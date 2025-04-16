using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    Coroutine movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (movement==null)
                movement = StartCoroutine(Move(Vector3.left));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (movement == null)
                movement = StartCoroutine(Move(Vector3.right));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (movement == null)
                movement = StartCoroutine(Move(Vector3.up));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (movement == null)
                movement = StartCoroutine(Move(Vector3.down));
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (movement != null)
            {
                StopCoroutine(movement);
                movement = null;
            }
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        while (true)
        {
            transform.Translate(direction * Time.fixedDeltaTime * 100);
            yield return new WaitForSeconds(0.09f);
        }
    }
}
