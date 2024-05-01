using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrap : MonoBehaviour
{
    public Animator anim;
    public bool isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void SetDoorOpen(bool isOpen)
    {
        this.isOpen = isOpen;
        anim.SetBool("isOpen", isOpen);
    }
}
