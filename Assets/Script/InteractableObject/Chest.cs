using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//RootlessDuckweed�������� 
public class Chest : MonoBehaviour,IInteractable //ʵ��IInterractable �ɻ����ӿ�
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
    public void TriggerAction() //ʵ�ֽӿڴ��������߼�
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
