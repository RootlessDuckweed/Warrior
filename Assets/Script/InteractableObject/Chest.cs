using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
//RootlessDuckweed��������
public class Chest : MonoBehaviour,IInteractable //ʵ��IInterractable �ɻ����ӿ�
{
    public SpriteRenderer render;
    public Sprite closed;
    public Sprite opened;
    private bool isOpened;
    public UnityEvent OnChestOpened;
    //�洢�����л�õ���Ʒ
    public List<Pair<PropSO,int>> itemList;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        OnChestOpened.AddListener(AddItemToInventory);
    }

    private void OnDisable()
    {
        OnChestOpened.RemoveListener(AddItemToInventory);
    }
    public void TriggerAction() //ʵ�ֽӿڴ��������߼�
    {
        if (!isOpened)
        {
            render.sprite = opened;
            gameObject.tag = "Untagged";
            isOpened = true;
            AudioManager.Instance.PlayFX(AudioPathGlobals.OpenChest, 0.2f);
            OnChestOpened?.Invoke();
        }
        
    }

    //Bocchi:����Ʒ��ӵ������в���ʾ��õ���Ʒ
    public void AddItemToInventory()
    {
        foreach (var item in itemList)
        {
            InventoryManager.Instance.AddProp(item.key,item.value);
        }

        UIManager.Instance.OpenPanel("ItemDescriptionPanel");
        UIManager.Instance.panelDict["ItemDescriptionPanel"].GetComponent<ItemDescriptonPanel>().GeneratePanel(itemList);
    }

    public bool RepeatInteraction()
    {
        return false;
    }
}
