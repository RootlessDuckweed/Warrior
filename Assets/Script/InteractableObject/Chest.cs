using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//RootlessDuckweed：宝箱类 
public class Chest : MonoBehaviour,IInteractable //实现IInterractable 可互动接口
{
    public SpriteRenderer render;
    public Sprite closed;
    public Sprite opened;
    private bool isOpened;
    public UnityEvent OnChestOpened;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    public void TriggerAction() //实现接口触发互动逻辑
    {
        if (!isOpened)
        {
            render.sprite = opened;
            gameObject.tag = "Untagged";
            isOpened = true;
            OnChestOpened?.Invoke();
        }
        
    }
}
