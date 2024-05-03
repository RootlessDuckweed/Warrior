using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DoorTrap : MonoBehaviour
{
    public Animator anim;
    public bool isOpen;
    [Header("Type: Button or Launcher")]
    public bool isOpenByLauncher;

    [Header("LauncherStart")]
    public float closeTimeDuration;
    public float closeCounter;
    
    private void Awake()
    {
        closeCounter = closeTimeDuration;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CloseCounter();
    }

    private void CloseCounter()
    {
        if (!isOpenByLauncher) return;
        if (isOpen) 
        {
           closeCounter-= Time.deltaTime;
            if (closeCounter <= 0f)
            {
                isOpen = false;
                closeCounter = closeTimeDuration;
                anim.SetBool("isOpen", isOpen);
            }
        }
    }
    public void SetDoorOpen(bool isOpen)
    {
        this.isOpen = isOpen;
        anim.SetBool("isOpen", isOpen);
    }

    public void SetDoorOpen_Launcher(bool isHit)
    {
        if (isHit)
        {
            closeCounter = closeTimeDuration;
            this.isOpen = true;
            anim.SetBool("isOpen", isOpen);
        }
            
    }
}
