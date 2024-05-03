using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeySlot : MonoBehaviour,IInteractable
{
    public PropSO needKey;
    public bool isOpened;
    public bool canRepeat;
    public UnityEvent OnKeySlotOpened;
    private void Awake()
    {
        canRepeat = true;
    }
    public bool RepeatInteraction()
    {
       
       return canRepeat;
        
    }

    public void TriggerAction()
    {
        
            
        if (InventoryManager.Instance.inventorySO.propsPakage.ContainsKey(needKey.propName))
        {
            isOpened = true;
            InventoryManager.Instance.RemoveProp(needKey);
            OnKeySlotOpened?.Invoke();
            canRepeat = false;
        }
        
    }
}
