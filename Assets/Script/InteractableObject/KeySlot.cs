using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class KeySlot : MonoBehaviour,IInteractable
{
    public PropSO needKey;
    public bool isOpened;
    public bool canRepeat;
    public string ID;
    public UnityEvent OnKeySlotOpened;
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
        canRepeat = true;
    }
    public bool RepeatInteraction()
    {
       
       return canRepeat;
        
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
    public void TriggerAction()
    {
         
        if (InventoryManager.Instance.inventorySO.propsPakage.ContainsKey(needKey.propName))
        {
            isOpened = true;
            InventoryManager.Instance.RemoveProp(needKey);
            OnKeySlotOpened?.Invoke();
            canRepeat = false;
            AudioManager.Instance.PlayFX(AudioPathGlobals.OpenChest, 0.2f);
        }
        
    }

    public void DeserializeFromJsonData(string json)
    {
        var data = JsonConvert.DeserializeObject<NeedToConvertJsonData>(json);
        isOpened = data.isOpened;
        if (isOpened)
        {
            OnKeySlotOpened?.Invoke();
            canRepeat = false;
            gameObject.tag="Untagged";
        }

    } // 从json文件中反序列保存的信息 并执行相关操作

    public string GetNeedToCovertJsonData()
    {
        return JsonConvert.SerializeObject(new NeedToConvertJsonData(isOpened));
    } // 获取需要保存的json数据从本类中
}
