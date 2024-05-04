using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Receiver : MonoBehaviour
{
    public UnityEvent OnBulletShotIn;
    public UnityEvent OnNoBulletShotIn;
    public bool isShotIn;
    public float checkShotInCounter;
    public float checkShotInDuration = 1f;
    private void Awake()
    {
        checkShotInCounter = checkShotInDuration;
    }

    private void Update()
    {
        BulletShotCounter();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            OnBulletShotIn?.Invoke();
            isShotIn = true;
            checkShotInCounter = checkShotInDuration;
            ObjectPool<Bullet> bulletPool = other.GetComponentInParent<Launcher>().bulletPool;
            bulletPool.Release(other.gameObject.GetComponent<Bullet>());
        }  
    }
    public void BulletShotCounter()
    {
        if (isShotIn)
        {
            checkShotInCounter-=Time.deltaTime;
            if (checkShotInCounter <= 0)
            {
                isShotIn = false;
                OnNoBulletShotIn?.Invoke();
                checkShotInCounter = checkShotInDuration;
            }
        }
    }
}
