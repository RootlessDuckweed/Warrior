using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    private static ShadowPool _instance;

    public static ShadowPool Instance => _instance;
    public GameObject shadowPrefab;
    public int shadowCount;
    
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (_instance == null)
        {
             _instance = this;
            FillPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FillPool()
    {
        for(int i=0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab,transform);
           Release(newShadow);
        }
    }

    private GameObject CreateObj()
    { 
        var newObj = Instantiate(shadowPrefab,transform);
        newObj.SetActive(false);
        return newObj;
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public GameObject Get()
    {
        if(pool.Count==0)
        {
           pool.Enqueue(CreateObj());
        }
        var outShadow = pool.Dequeue();
        outShadow.gameObject.SetActive(true);
        return outShadow;
    }
    
}
