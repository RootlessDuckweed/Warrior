using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
//RootlessDuckweed��������
public class Chest : MonoBehaviour,IInteractable //ʵ��IInterractable �ɻ����ӿ�
{

    public SpriteRenderer render;

    public Sprite closed;

    public Sprite opened;

    public bool isOpened;

    public string ID;

    public UnityEvent OnChestOpened;
    //�洢�����л�õ���Ʒ
    public List<Pair<PropSO,int>> itemList;


    public class NeedToConvertJsonData
    {
        public NeedToConvertJsonData(bool isOpen)
        {
            isOpened = isOpen;
        }

        public bool isOpened; 
    }

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        ID = GetComponent<GenerateOnlyGuid>().ID;
        SaveInteractableObserver.AddObserver(ID, this);
    }

    private void OnDestroy()
    {
        SaveInteractableObserver.RemoveObserver(ID);
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

    //Bocchi:����Ʒ���ӵ������в���ʾ��õ���Ʒ
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

    // ��json�ļ��з����б������Ϣ
    public  void DeserializeFromJsonData(string jsonData)
    {
        var loaded = JsonConvert.DeserializeObject<NeedToConvertJsonData>(jsonData);
        if (loaded != null)
        {
            this.isOpened = loaded.isOpened;
            if (this.isOpened)
            {
                render.sprite = opened;
                gameObject.tag = "Untagged";
                OnChestOpened?.Invoke();
            }
        }
        else
        {
            print("loaded was null");
        }
        
    }

    // ��ȡ��Ҫ�����json���ݴӱ�����
    public string GetNeedToCovertJsonData()
    {
        return JsonConvert.SerializeObject(new NeedToConvertJsonData(isOpened));
    }


    
}
