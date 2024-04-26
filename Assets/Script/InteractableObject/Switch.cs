using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, IInteractable
{
    public UnityEvent OnSwitchOpened;
    public UnityEvent OnSwitchClosed;
    public Animator anim;
    public bool isOpen;
    public string ID;
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
        anim = GetComponent<Animator>();
    }
    public void TriggerAction()
    {
        isOpen = !isOpen;
        anim.SetBool("isOpen",isOpen);
        if(isOpen == true) 
            OnSwitchOpened?.Invoke();
        else 
            OnSwitchClosed?.Invoke();
        AudioManager.Instance.PlayFX(AudioPathGlobals.SwitchAction, 0.2f);
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
    public bool RepeatInteraction()
    {
        return true;
    }

    public void DeserializeFromJsonData(string obj)
    {
        var loaded= JsonConvert.DeserializeObject<NeedToConvertJsonData>(obj);
        if (isOpen = loaded.isOpened)
        {
            anim.SetBool("isOpen", isOpen);
            OnSwitchOpened?.Invoke();
        }
        else
        {
            anim.SetBool("isOpen", isOpen);
            OnSwitchClosed?.Invoke();
        }
    }
    public string GetNeedToCovertJsonData()
    {
        return JsonConvert.SerializeObject(new NeedToConvertJsonData(isOpen));
    }

}
