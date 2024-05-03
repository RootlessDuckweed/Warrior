using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, Time.deltaTime * 300 + transform.localEulerAngles.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
    }
}
