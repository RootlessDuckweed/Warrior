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
    }

    public bool RepeatInteraction()
    {
        return true;
    }
}
