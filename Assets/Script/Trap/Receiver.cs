using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Receiver : MonoBehaviour
{
    public UnityEvent OnBulletShotIn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            OnBulletShotIn?.Invoke();
            ObjectPool<Bullet> bulletPool = other.GetComponentInParent<Launcher>().bulletPool;
            bulletPool.Release(other.gameObject.GetComponent<Bullet>());
        }
        
    }
}
