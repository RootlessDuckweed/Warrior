using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此为测试类,作为测试使用
public class Item : MonoBehaviour
{
    public PropSO propSO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this != null)
        {
            InventoryManager.Instance.AddProp(propSO, 1);
            Destroy(gameObject);
        }
    }
}
