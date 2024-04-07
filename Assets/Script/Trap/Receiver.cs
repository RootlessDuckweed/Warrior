using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Receiver : MonoBehaviour
{
    public UnityEvent OnBulletShotIn;


    private void OnTriggerEnter2D(Collider2D other)
    {
        OnBulletShotIn?.Invoke();
        Destroy(other.gameObject);
    }
}
