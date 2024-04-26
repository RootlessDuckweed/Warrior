using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
//RootlessDuckweed：宝箱类
public class Chest : MonoBehaviour,IInteractable //实现IInterractable 可互动接口
{
    public SpriteRenderer render;
    public Sprite closed;
    public Sprite opened;
    private bool isOpened;
    public UnityEvent OnChestOpened;
    //存储宝箱中获得的物品
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
    public void TriggerAction() //实现接口触发互动逻辑
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

    //Bocchi:将物品添加到背包中并显示获得的物品
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
